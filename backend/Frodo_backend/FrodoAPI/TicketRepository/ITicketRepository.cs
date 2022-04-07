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


        void Persist(Guid	 bundleId);
    }

    public interface ITicketProvider
    {
        Ticket GetTicketForStage(JourneyStage stage);
    }

    public class DummyTicketProvider : ITicketProvider
    {
        public Ticket GetTicketForStage(JourneyStage stage)
        {
            return new Ticket
            {
                Price = 10,
                Product = $"Happy Hour {stage.From}",
                Stage = stage
            };
        }
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

            if (journey == null)
                return null;

            var currentTicket = journey.Tickets.FirstOrDefault(t => t.Stage.StartingTime < currentTime && currentTime.Subtract(t.Stage.StartingTime) < t.Stage.TravelTime);

            return new ValidateableTicket
            {
                BarcodeData = currentTicket.Product + currentTicket.Price
            };
        }

        public Guid Add( in Guid journeyGuid, Ticket[] results)
        {
            var id = new Guid();
            
            _bundles.Add	(id, new TicketBundle
            {
                JourneyId = journeyGuid,
                Sold = false,
                Tickets = results
            });

            return id;
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