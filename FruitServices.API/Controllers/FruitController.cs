
using FruitServices.Application.Services.Contracts;
using FruitServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fruit.API.Controllers
{
    [ApiController]
    [Route("api/FruitServicesAPI/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    //[Authorize] //Todo: Uncomment after adding Auth Server code
    public class FruitController : ControllerBase
    {

        private readonly ILogger<FruitController> _logger;
        private readonly IFruitService _fruitService;
        public FruitController(ILogger<FruitController> logger, IFruitService fruitService, IConfiguration config)
        {
            _logger = logger;
            _fruitService = fruitService;
        }

         

        [HttpGet("GetAllFruits")]
        public async Task<IActionResult> GetAllFruits()
        {
            try
            {
                var result = await _fruitService.GetAllFruits();
                if (result == null)
                {
                   return BadRequest(new ApiResponse(HttpStatusCode.InternalServerError, "Something went wrong OR Please check the input."));
                }
                return Ok(new ApiResponse(HttpStatusCode.OK, null, null, result));
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, "Error in FruitController.GetAllFruits");
               return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, "Error in FruitController.GetAllFruits."));
            }
        }

        [HttpPost("GetAllFruitsByFamily")]
        public async Task<IActionResult> GetAllFruitsByFamily(string fruitFamily)
        {
            try
            {
                var result = await _fruitService.GetAllFruitsByFamily(fruitFamily);
                if (result == null)
                {
                    return BadRequest(new ApiResponse(HttpStatusCode.InternalServerError, "Something went wrong OR Please check the input."));
                }
                return Ok(new ApiResponse(HttpStatusCode.OK, null, null, result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in FruitController.GetAllFruitsByFamily");
                return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, "Error in FruitController.GetAllFruitsByFamily."));
            }
        }

    }
}
