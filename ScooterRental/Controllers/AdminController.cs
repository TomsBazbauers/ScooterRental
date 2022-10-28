using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Core.Validations;
using ScooterRental.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental.Controllers
{
    [Route("rental-admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IScooterService _scooterService;
        private readonly IReportService _reportService;
        private readonly IEnumerable<IScooterValidator> _scooterValidators;
        private readonly IMapper _mapper;

        public AdminController(IScooterService scooterService,
            IReportService reportService, IEnumerable<IScooterValidator> scooterValidators, IMapper mapper)
        {
            _scooterService = scooterService;
            _reportService = reportService;
            _scooterValidators = scooterValidators;
            _mapper = mapper;
        }

        [Route("add-scooter")]
        [HttpPost]
        public IActionResult AddScooter(ScooterRequest request)
        {
            var scooter = _mapper.Map<Scooter>(request);

            if (!_scooterValidators.All(v => v.IsValid(scooter)))
            {
                return BadRequest();
            }

            var result = _scooterService.Create(scooter);

            if (result.Success)
            {
                request = _mapper.Map<ScooterRequest>(scooter);
                return Created("", request);
            }

            return Problem(result.FormattedErrors);
        }

        [Route("update-scooter/{id}")]
        [HttpPut]
        public IActionResult UpdateScooter(ScooterRequest request)
        {
            var scooterToMatch = _mapper.Map<Scooter>(request);
            var scooterToUpdate = _scooterService.GetScooterById(request.Id);

            if (_scooterValidators.All(v => v.IsValid(scooterToUpdate)))
            {
                var scooter = _scooterService.UpdateScooter(scooterToUpdate, scooterToMatch);
                var result = _scooterService.Update(scooter);

                if (result.Success)
                {
                    var response = _mapper.Map<ScooterRequest>(scooter);
                    return Ok(response);
                }

                return Problem(result.FormattedErrors);
            }

            return BadRequest();
        }

        [Route("scooter/{id}")]
        [HttpGet]
        public IActionResult GetScooter(int id)
        {
            var request = _scooterService.GetScooterById(id);

            if (request == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ScooterRequest>(request);

            return Ok(response);
        }

        [Route("delete-scooter/{id}")]
        [HttpDelete]
        public IActionResult DeleteScooter(int id)
        {
            var scooter = _scooterService.GetScooterById(id);

            if (scooter != null)
            {
                var result = _scooterService.Delete(scooter);
                if (result.Success)
                {
                    return Ok();
                }

                return Problem(result.FormattedErrors);
            }

            return BadRequest();
        }

        [Route("report")]
        [HttpGet]
        public IActionResult GetIncomeReport(int year, bool includeRunningRentals)
        {
            var report = _reportService.GetIncomeForPeriod(year, includeRunningRentals);

            return Ok(report);
        }
    }
}