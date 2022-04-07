﻿using System.Collections.Generic;
using FrodoAPI.Domain;
using FrodoAPI.JourneyRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FrodoAPI.Controllers
{
    [EnableCors("MyAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private IStationRepository _stationRepository;

        public StationsController(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        // GET: api/<StationsController>
        
        [EnableCors("MyAllowSpecificOrigins")]
        [HttpGet]
        public IEnumerable<Station> Get()
        {
            return _stationRepository.GetAllStations();
        }


    }
}
