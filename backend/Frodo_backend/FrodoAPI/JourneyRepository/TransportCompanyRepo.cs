    using System;
    using System.Collections.Generic;
    using System.Reflection.Metadata.Ecma335;
    using System.Runtime.InteropServices.ComTypes;
    using FrodoAPI.Domain;

    namespace FrodoAPI.JourneyRepository
    {
        public class TransportCompanyRepo
        {
            public TransportCompanyRepo()
            {
                var Uber = new TransportCompany(
                    canGetFromTo: (from, to) =>
                    {
                        return true;
                    },
                    costFun: (from, to) =>
                    {
                        return from.GetCoordinate().DistanceTo(to.GetCoordinate()) * 50.0;
                    },
                    getTicketFun: (from, to, passenger) =>
                    {
                        return new Ticket()
                        {

                            Id = Guid.NewGuid(),

                        };
                    }                   
                    )
                {
                    Id = Guid.NewGuid(),
                    Name = "Uber",
                    IsTaxi = true,
                    ServedStations = new List<Station>(),
                };
                
                

            }
        }
    }
