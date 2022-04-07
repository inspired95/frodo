using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using FrodoAPI.Contract;
using FrodoAPI.Domain;
using FrodoAPI.JourneyRepository;
using Microsoft.VisualBasic;

namespace FrodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JourneyPlannerController : ControllerBase
    {
        

        private readonly ILogger<JourneyPlannerController> _logger;
        private readonly IJourneyRepository _journeyRepository;
        private readonly IStationRepository _stationRepository;

        public JourneyPlannerController(ILogger<JourneyPlannerController> logger, IStationRepository stationRepository, IJourneyRepository journeyRepository)
        {
            _logger = logger;
            _stationRepository = stationRepository;
            _journeyRepository = journeyRepository;
        }

        // GET: api/<StationsController>
        [HttpGet]
        public IEnumerable<Journey> Get(JourneyRequest request)
        {
            _logger.LogCritical($"Get {request}");
            var journey1 = CreateRandomJourney(request);
            _journeyRepository.AddJourney(journey1);
            var journey2 = CreateRandomJourney(request);
            _journeyRepository.AddJourney(journey2);
            return new List<Journey>()
                {journey1, journey2};
        }

        private Journey CreateRandomJourney(JourneyRequest request)
        {
            var random = new Random();
            var stops = _stationRepository.GetAllStations();
            var index1 = random.Next(stops.Count);
            var index2 = random.Next(stops.Count);
            if (index2 == index1) index2 = (index2 + 1) % stops.Count;
            var rand_speed = 15 + random.Next(10);
            var traveltime1 =
                TimeSpan.FromHours(request.StartingPoint.DistanceTo(stops[index1].Coordinate) / rand_speed);
            var traveltime2 =
                TimeSpan.FromHours(stops[index1].Coordinate.DistanceTo(stops[index2].Coordinate) / rand_speed);
            var traveltime3 =
                TimeSpan.FromHours(request.EndingPoint.DistanceTo(stops[index2].Coordinate) / rand_speed);
            return new Journey()
            {
                GUID = Guid.NewGuid(),
                Stages = new List<JourneyStage>()
                {
                    new JourneyStage()
                    {
                        From = new GeoPoint() { Coordinates = request.StartingPoint },
                        MeanOfTransportation = "Uber",
                        StartingTime = request.StartingDate,
                        TransportCompanyId = 1,
                        To = new JourneyPoint(stops[index1]),
                        TravelTime = traveltime1,
                        WaitingTime = TimeSpan.FromMinutes(5),
                        Price = 100,

                    },
                    new JourneyStage()
                    {
                        From = new JourneyPoint(stops[index1]),
                        MeanOfTransportation = "Bus Line " + (random.Next(3) + 1).ToString(),
                        StartingTime = request.StartingDate + traveltime1 + TimeSpan.FromMinutes(5),
                        TransportCompanyId = 1,
                        To = new JourneyPoint(stops[index2]),
                        TravelTime = traveltime2,
                        WaitingTime = TimeSpan.FromMinutes(5),
                        Price = 20,

                    },
                    new JourneyStage() {                       
                        From = new JourneyPoint(stops[index2]),
                        MeanOfTransportation = "Bike",
                        StartingTime = request.StartingDate + traveltime1 + traveltime2 + TimeSpan.FromMinutes(10),
                        TransportCompanyId = 1,
                        To = new JourneyPoint() {Coordinates = request.EndingPoint},
                        TravelTime = traveltime3,
                        WaitingTime = TimeSpan.FromMinutes(5),
                        Price = 15,

                    },
                }
            };

        }
    }
}
