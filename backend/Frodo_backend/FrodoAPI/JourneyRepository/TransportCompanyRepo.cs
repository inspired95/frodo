    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FrodoAPI.Domain;

    namespace FrodoAPI.JourneyRepository
    {
        public interface ITransportCompanyRepo
        {
            IEnumerable<ITransportCompany> GetAll();

            ITransportCompany Get(Guid id);
        }

        public class TransportCompanyRepo : ITransportCompanyRepo
        {
            private TransportCompany _uber;

            public TransportCompanyRepo()
            {
                _uber = new TransportCompany(
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
                            Price = 10,
                            Product = $"Happy Hour {from.GetName()}",
                            Stage = new JourneyStage
                            {
                                From = from,
                                To = to,
                            }
                        };

                    }
                    )
                {
                    Id = Guid.Parse("BC262847-27DD-45A8-AE6F-F879BD3D48CA"),
                    Name = "Uber",
                    IsTaxi = true,
                    ServedStations = new List<Station>(),
                };
            }

            public IEnumerable<ITransportCompany> GetAll()
            {
                yield return _uber;
            }

            public ITransportCompany Get(Guid id)
            {
                return GetAll().FirstOrDefault(t => t.Id == id);
            }
        }
    }
