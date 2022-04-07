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
        public readonly TransportCompany _uber;
        public readonly TransportCompany _billundBus;
        public readonly TransportCompany _cityBike;
        public TransportCompanyRepo()
        {
            _uber = new TransportCompany(
                canGetFromTo: (from, to) =>
                {
                    return true;
                },
                costFun: (from, to) =>
                {
                    return from.Coordinates.DistanceTo(to.Coordinates) * 50.0;
                },
                getTicketFun: (from, to, passenger) =>
                {
                    return new Ticket()
                    {

                        Id = Guid.NewGuid(),
                        Price = 10,
                        Product = $"Happy Hour {from.StopName}",
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

            };

            _billundBus = new TransportCompany(
                canGetFromTo: (from, to) =>
                {
                    return true;
                },
                costFun: (from, to) =>
                {
                    return from.Coordinates.DistanceTo(to.Coordinates) * 20.0;
                },
                getTicketFun: (from, to, passenger) =>
                {
                    return new Ticket()
                    {

                        Id = Guid.NewGuid(),
                        Price = 10,
                        Product = $"Normal ticket {from.StopName}",
                        Stage = new JourneyStage
                        {
                            From = from,
                            To = to,
                        }
                    };

                }
            )
            {
                Id = Guid.Parse("C33D3567-8BF2-42B4-A4F5-670ECE919429"),
                Name = "Billund Buses",
                IsTaxi = true,

            };

            _cityBike = new TransportCompany(
                canGetFromTo: (from, to) =>
                {
                    return true;
                },
                costFun: (from, to) =>
                {
                    return 20; //fixed price
                },
                getTicketFun: (from, to, passenger) =>
                {
                    return new Ticket()
                    {

                        Id = Guid.NewGuid(),
                        Price = 20,
                        Product = $"Flat rate",
                        Stage = new JourneyStage
                        {
                            From = from,
                            To = to,
                        }
                    };

                }
            )
            {
                Id = Guid.Parse("77AC6F8C-7D54-4BA8-BFB1-9567DB4CEDB4"),
                Name = "CityBike",
                IsTaxi = true,

            };

        }

        public IEnumerable<ITransportCompany> GetAll()
        {
            yield return _uber;
            yield return _cityBike;
            yield return _billundBus;

        }

        public ITransportCompany Get(Guid id)
        {
            return GetAll().FirstOrDefault(t => t.Id == id);
        }
    }
}
