using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental.Controllers
{
    [Route("rental-customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IScooterService _scooterService;
        private readonly IEnumerable<IScooterValidator> _scooterValidators;
        private readonly IRentalService _rentalService;
        private readonly IReportService _reportService;

        public CustomerController(IScooterService scooterService,
            IEnumerable<IScooterValidator> scooterValidators, IRentalService rentalService, IReportService reportService)
        {
            _scooterService = scooterService;
            _scooterValidators = scooterValidators;
            _rentalService = rentalService;
            _reportService = reportService;
        }

        [Route("rent-scooter/start/{id}")]
        [HttpPut]
        public IActionResult StartRental(int id, DateTime rentalStart)
        {
            var request = _scooterService.GetScooterById(id);

            if (!_scooterValidators.All(v => v.IsValid(request)))
            {
                return BadRequest();
            }
            
            _rentalService.StartRental(request.Id);
            _reportService.Create(new RentalReport(request.Id, request.PricePerMinute, rentalStart));

            return Ok();
        }

        [Route("rent-scooter/end/{id}")]
        [HttpPut]
        public IActionResult EndRental(int id, DateTime rentalEnd) //
        {
            var request = _scooterService.GetScooterById(id);

            if (request == null || !request.IsRented)
            {
                return BadRequest();
            }

            _rentalService.EndRental(request.Id, rentalEnd);
            var report = _reportService.GetSingleReport(request.Id, rentalEnd);

            return Ok(report);
        }
    }
}