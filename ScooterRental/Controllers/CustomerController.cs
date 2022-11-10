using Microsoft.AspNetCore.Mvc;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using System;

namespace ScooterRental.Controllers
{
    [Route("rental-customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IScooterService _scooterService;
        private readonly IReportService _reportService;

        public CustomerController(IScooterService scooterService, IReportService reportService)
        {
            _scooterService = scooterService;
            _reportService = reportService;
        }

        [Route("rent-scooter/start/{id}")]
        [HttpPut]
        public IActionResult StartRental(long id)
        {
            var scooter = _scooterService.GetScooterById(id);
            
            if(scooter == null)
            {
                return NotFound(id);
            }

            var result = _scooterService.StartRental(id);
            
            if (result.Success)
            {
                _reportService.CreateReport(scooter);
                
                return Ok(id);
            }

            return BadRequest(id);
        }

        [Route("rent-scooter/end/{id}")]
        [HttpPut]
        public IActionResult EndRental(long id)
        {
            var result = _scooterService.EndRental(id);

            if (result.Success)
            {
                var report = _reportService.GetSingleReport(id);

                return Ok(report);
            }

            return NotFound(id);
        }
    }
}