using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Information")]
        public IActionResult LogInfo()
        {
            _logger.LogInformation("Info {0}", Guid.NewGuid().ToString());
            return Ok();
        }

        [HttpGet("Error")]
        public IActionResult LogError()
        {
            try
            {
                throw new Exception("Illegal state exception");
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message, e);
            }

            return Ok();
        }
    }
}
