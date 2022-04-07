using System.Collections.Generic;
using FrodoAPI.Contract;
using FrodoAPI.Domain;

namespace FrodoAPI.JourneyRepository
{
    public class StationsRepo : IStationRepository
    {
        private List<Station>_stations = new List<Station>();

        public StationsRepo()
        {
            var stationA = new Station()
            {
                Coordinate = new GeoCoordinate()
                {
                    Latitude = 55.73161777005428,
                    Longitude = 9.118239985885578
                },
                Name = "Billund Center",
                Id =1,
            };

            var stationB = new Station()
            {
                Coordinate = new GeoCoordinate()
                {
                    Latitude = 55.746716687223234,
                    Longitude= 9.14737961324253
                },
                Name = "Billund Airport",
                Id =2,
            };

            var stationC = new Station()
            {
                Coordinate = new GeoCoordinate()
                {
                    Latitude = 55.73513738609845,
                    Longitude = 9.132380035343376
                },
                Name = "Billund Legoland",
                Id =3,  
            };
            _stations = new List<Station>() { stationA, stationB, stationC };
        }

        public List<Station> GetAllStations()
        {
            return _stations;
        }
    }

    public interface IStationRepository
    {
        List<Station> GetAllStations();
    }
}