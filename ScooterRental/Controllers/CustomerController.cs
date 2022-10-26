using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ScooterRental.Controllers
{
    [Route("rental-customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //rent scooter

        [Route("rent-scooter/{id}")]
        [HttpPut]
        public IActionResult StartRental(int id)
        {
            return Ok();
        }

        [Route("rent-scooter/{id}")]
        [HttpPut]
        public IActionResult EndRental(int id)
        {
            
        }


    }
}