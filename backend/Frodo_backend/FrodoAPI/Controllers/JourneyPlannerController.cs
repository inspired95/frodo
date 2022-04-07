using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrodoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JourneyPlannerController : ControllerBase
    {


        private readonly ILogger<JourneyPlannerController> _logger;

        public JourneyPlannerController(ILogger<JourneyPlannerController> logger)
        {
            _logger = logger;
        }



    }
}
