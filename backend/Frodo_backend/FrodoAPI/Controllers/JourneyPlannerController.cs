﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private readonly ITransportCompanyRepo _transportCompanyRepo;

        public JourneyPlannerController(ILogger<JourneyPlannerController> logger, IStationRepository stationRepository, IJourneyRepository journeyRepository, ITransportCompanyRepo transportCompanyRepo)
        {
            _logger = logger;
            _stationRepository = stationRepository;
            _journeyRepository = journeyRepository;
            _transportCompanyRepo = transportCompanyRepo;
        }

        // GET: api/<StationsController>
        [HttpPost]
        public IEnumerable<Journey> Post(JourneyRequest request)
        {
            _logger.LogCritical($"Get {request}");
            var journey1 = CreateRandomJourney(request);
            _journeyRepository.AddJourney(journey1);
   
            var journey2 = CreateRandomJourney(request);
            _journeyRepository.AddJourney(journey2);
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
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

            var transportCompanies = _transportCompanyRepo.GetAll().ToArray();

            var transportcompany1 = transportCompanies[0];
            var transportcompany2 = transportCompanies[1];
            var transportcompany3 = transportCompanies[3];
            return new Journey()
            {
                GUID = Guid.NewGuid(),
                Stages = new List<JourneyStage>()
                {
                    new JourneyStage()
                    {
                        From = new JourneyPoint() { Coordinates = request.StartingPoint },
                        MeanOfTransportation = transportcompany1.Name,
                        StartingTime = request.StartingDate,
                        TransportCompanyId = transportcompany1.Id,
                        To = new JourneyPoint(stops[index1]),
                        TravelTime = traveltime1,
                        WaitingTime = TimeSpan.FromMinutes(5),
                        Price = transportcompany1.GetTicket(new JourneyPoint() { Coordinates = request.StartingPoint }, new JourneyPoint(stops[index1]), "").Price

                    },
                    new JourneyStage()
                    {
                        From = new JourneyPoint(stops[index1]),
                        MeanOfTransportation = transportcompany2.Name + (random.Next(3) + 1),
                        StartingTime = request.StartingDate + traveltime1 + TimeSpan.FromMinutes(5),
                        TransportCompanyId = transportcompany2.Id,
                        To = new JourneyPoint(stops[index2]),
                        TravelTime = traveltime2,
                        WaitingTime = TimeSpan.FromMinutes(5),
                        Price = transportcompany2.GetTicket(new JourneyPoint(stops[index2]) ,new JourneyPoint(stops[index1]),"" ).Price

                    },
                    new JourneyStage() {                       
                        From = new JourneyPoint(stops[index2]),
                        MeanOfTransportation = transportcompany3.Name,
                        StartingTime = request.StartingDate + traveltime1 + traveltime2 + TimeSpan.FromMinutes(10),
                        TransportCompanyId = transportcompany3.Id,
                        To = new JourneyPoint() {Coordinates = request.EndingPoint},
                        TravelTime = traveltime3,
                        WaitingTime = TimeSpan.FromMinutes(5),
                        Price = transportcompany3.GetTicket(new JourneyPoint { Coordinates = request.EndingPoint }, new JourneyPoint(stops[index2]), "").Price
                    },
                }
            };

        }
    }
}
