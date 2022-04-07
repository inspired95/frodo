using System;
using System.Collections.Generic;
using System.Linq;
using FrodoAPI.Domain;

namespace FrodoAPI.TicketRepository
{
    public interface ITicketRepository 
    {
        ValidateableTicket GetForCurrentStage(Guid journeyId, DateTime currentTime);
        Guid Add(in Guid journeyGuid, Ticket[]	 results);
        ValidateableTicket Get(Guid journeyId, Guid ticketId);

        IEnumerable<ValidateableTicket> GetAllTickets(Guid journeyId);
        void Persist(Guid	 bundleId);
    }

    public class DummyTicketRepository : ITicketRepository
    {
        private class TicketBundle
        {
            public bool Sold { get; set; }
            public Guid JourneyId { get; set; }
            public Ticket[] Tickets { get; set; }
        }

        private readonly Dictionary<Guid, TicketBundle> _bundles = new Dictionary<Guid, TicketBundle>();

        public ValidateableTicket GetForCurrentStage(Guid journeyId, DateTime currentTime)
        {
            var journey = _bundles.Values.FirstOrDefault(b => b.JourneyId == journeyId);

            if (journey == null || !journey.Sold)
                return null;

            var currentTicket = journey.Tickets.FirstOrDefault(t => t.Stage.StartingTime < currentTime && currentTime.Subtract(t.Stage.StartingTime) < t.Stage.TravelTime);

            if (currentTicket == null)
                return null;

            return new ValidateableTicket
            {
                TicketId = currentTicket.Id,
                BarcodeData = currentTicket.Product + currentTicket.Price,
                StartingTime = currentTicket.Stage.StartingTime
            };
        }

        public ValidateableTicket Get(Guid journeyId, Guid ticketId)
        {
            var journey = _bundles.Values.FirstOrDefault(b => b.JourneyId == journeyId);

            if (journey == null || !journey.Sold)
                return null;

            var currentTicket = journey.Tickets.FirstOrDefault(t => t.Id == ticketId);

            if (currentTicket == null)
                return null;

            return new ValidateableTicket
            {
                TicketId = currentTicket.Id,
                BarcodeData = currentTicket.Product + currentTicket.Price,
                StartingTime = currentTicket.Stage.StartingTime
            };
        }

        public IEnumerable<ValidateableTicket> GetAllTickets(Guid journeyId)
        {
            var journey = _bundles.Values.FirstOrDefault(b => b.JourneyId == journeyId);

            if (journey == null || !journey.Sold)
                yield break;
            
            foreach (var currentTicket in journey.Tickets)
            {
                yield return new ValidateableTicket
                {
                    TicketId = currentTicket.Id,
                    BarcodeData = currentTicket.Product + currentTicket.Price,
                    StartingTime = currentTicket.Stage.StartingTime
                };
            }
        }

        public Guid Add( in Guid journeyGuid, Ticket[] results)
        {
            _bundles.Add	(journeyGuid, new TicketBundle
            {
                JourneyId = journeyGuid,
                Sold = false,
                Tickets = results
            });

            return journeyGuid;
        }

        public void Persist(Guid bundleId)
        {
            if (_bundles.ContainsKey(bundleId))
            {
                var bundle = _bundles[bundleId];
                bundle.Sold = true;
            }
        }
    }
}