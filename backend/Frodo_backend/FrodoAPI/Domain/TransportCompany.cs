﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace FrodoAPI.Domain
{
    public class TransportCompany : ITransportCompany
    {
        private readonly CanGetFromToFun _canGetFromTo;
        private readonly CostFromToFun _costFun;
        private readonly GetTicketFun _getTicketFun;
        public Guid Id;
        public string Name;
        public bool IsTaxi;
        public List<Station> ServedStations;
        public delegate bool CanGetFromToFun(JourneyPoint from, JourneyPoint to);
        public delegate double CostFromToFun(JourneyPoint from, JourneyPoint to);

        public delegate Ticket GetTicketFun(JourneyPoint from, JourneyPoint to, string Passenger);

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
            return _getTicketFun.Invoke(from, to, passenger);
        }
        public TransportCompany(CanGetFromToFun canGetFromTo, CostFromToFun costFun, GetTicketFun getTicketFun)
        {
            _canGetFromTo = canGetFromTo;
            _costFun = costFun;
            _getTicketFun = getTicketFun;
        }

    }

    public class Ticket
    {
        public string IssuedFor;
        public JourneyPoint From;
        public JourneyPoint To;
        public double Price;
        public Guid Id;
    }
}