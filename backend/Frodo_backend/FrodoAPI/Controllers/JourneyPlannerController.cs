using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrodoAPI.Contract;
using FrodoAPI.Domain;
using FrodoAPI.JourneyRepository;

namespace FrodoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JourneyPlannerController : ControllerBase
    {
        

        private readonly ILogger<JourneyPlannerController> _logger;
        private IJourneyRepository _journeyRepository;
        private IStationRepository _stationRepository;

        public JourneyPlannerController(ILogger<JourneyPlannerController> logger)
        {
            _logger = logger;
        }

        // GET: api/<StationsController>
        [HttpGet]
        public IEnumerable<Journey> Get(JourneyRequest request)
        {
            var journey1 = CreateRandomJourney(request);
            _journeyRepository.Add(journey1);
            var journey2 = CreateRandomJourney(request);
            _journeyRepository.Add(journey2);
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
            return new Journey()
            {
                GUID = Guid.NewGuid(),
                Stages = new List<JourneyStage>()
                {
                    new JourneyStage()
                    {
                        From = new GeoPoint() { Coordinates = request.StartingPoint },
                        MeanOfTransportation = "Uber",
                        

                    },
                    new JourneyStage() { },
                    new JourneyStage() { },
                }
            };

        }
    }
}
