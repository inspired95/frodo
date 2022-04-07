using System.Collections.Generic;
using FrodoAPI.Domain;
using FrodoAPI.JourneyRepository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FrodoAPI.Controllers
{


    [ApiController]
    [Route("api/[controller]")]

    public class StationsController : ControllerBase
    {
        private readonly IStationRepository _stationRepository;
        private readonly ILogger<StationsController> _logger;

        public StationsController(IStationRepository stationRepository, ILogger<StationsController> logger)
        {
            _stationRepository = stationRepository;
            _logger = logger;
        }

        // GET: api/<StationsController>
        

        [HttpGet]
        public IEnumerable<Station> Get()
        {
            _logger.LogCritical("test");
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return _stationRepository.GetAllStations();

        }


    }
}
