using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http;
using System.Threading.Tasks;
using test.Controllers;
using test.Entidades;

namespace test.APIManager
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly FlightBusiness _flightBusiness;

        public JourneyController(FlightBusiness flightBusiness)
        {
            _flightBusiness = flightBusiness;
        }
        [HttpGet]
        public async Task<ActionResult<Journey>> GetTravelRoute(string origin, string destination)
        {
            try
            {
                var travelRoute = await _flightBusiness.GetTravelRoutes(origin, destination);

                if (travelRoute != null)
                {
                    return Ok(travelRoute);
                }
                else
                {
                    return NotFound("La ruta no puede ser calculada.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it according to your application's requirements.
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }

}
