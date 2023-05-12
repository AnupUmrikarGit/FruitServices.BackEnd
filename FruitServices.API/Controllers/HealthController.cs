
using FruitServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FruitServices.API.Controllers
{
    [ApiController]
    [Route("api/FruitServicesAPI/[controller]")]
    [ApiVersion("1.0")]
    public class HealthController : ControllerBase
    {

        private readonly ILogger<HealthController> _logger;
        private string _env = "";
        public HealthController(ILogger<HealthController> logger, IConfiguration config)
        {
            _logger = logger;
            _env = config.GetValue<string>("env");
        }

        [HttpGet("Ping")]
        [AllowAnonymous]
        public async Task<IActionResult> Ping()
        {
            try
            {
                return Ok(new ApiResponse(HttpStatusCode.OK, null, null, "Ping: "+ _env));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Health.Ping");
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, "Error in Health.Ping."));
            }
        }
    }
}
