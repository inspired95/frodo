using System;
using System.Collections.Generic;

namespace FrodoAPI.Domain
{
    public class TransportCompany : ITransportCompany
    {
        private readonly CanGetFromToFun _canGetFromTo;
        private readonly CostFromToFun _costFun;
        private readonly GetTicketFun _getTicketFun;
        public bool IsTaxi;
        public List<Station> ServedStations;
        public delegate bool CanGetFromToFun(JourneyPoint from, JourneyPoint to);
        public delegate double CostFromToFun(JourneyPoint from, JourneyPoint to);

        public delegate Ticket GetTicketFun(JourneyPoint from, JourneyPoint to, string Passenger);

        public string Name { get; set; }

        public Guid Id { get; set; }

        public bool CanGetFromTo(JourneyPoint from, JourneyPoint to)
        {
            return _canGetFromTo.Invoke(from, to);
        }

        public double CostFromTo(JourneyPoint from, JourneyPoint to)
        {
            return _costFun.Invoke(from, to);
        }

        public Ticket GetTicket(JourneyPoint from, JourneyPoint to, string passenger)
        {
            var ticket = _getTicketFun.Invoke(from, to, passenger);

            ticket.Price = _costFun(from, to);

            return ticket;
        }
        public TransportCompany(CanGetFromToFun canGetFromTo, CostFromToFun costFun, GetTicketFun getTicketFun)
        {
            _canGetFromTo = canGetFromTo;
            _costFun = costFun;
            _getTicketFun = getTicketFun;
        }

    }


}