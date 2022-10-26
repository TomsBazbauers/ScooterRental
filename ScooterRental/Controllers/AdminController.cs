﻿using AutoMapper;
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

        // add > update > delete > get reports

        [Route("add-scooter")]
        [HttpPost]
        public IActionResult AddScooter(ScooterRequest scooter) // automapper conv
        {
            var scooterToAdd = _mapper.Map<Scooter>(scooter);//

            if (!_scooterValidators.All(v => v.IsValid(scooterToAdd)))
            {
                return BadRequest(); // 400
            }

            if (_scooterService.IsFound(scooterToAdd)) // ja mappo tad nevajag
            {
                return Conflict(); // 409
            }

            _scooterService.Create(scooterToAdd);
            var result = _mapper.Map<ScooterRequest>(scooterToAdd);

            return Created("", result);
        }

        [Route("update-scooter/{id}")]
        [HttpPut]
        public IActionResult UpdateScooter(ScooterRequest request)
        {
            var scooterToMatch = _mapper.Map<Scooter>(request);
            var scooterToUpdate = _scooterService.GetById(scooterToMatch.Id);

            if (scooterToUpdate != null)
            {
                var scooter = _scooterService.UpdateScooter(scooterToUpdate, scooterToMatch);
                _scooterService.Update(scooter);
                var result = _mapper.Map<ScooterRequest>(scooter);

                return Ok(result);
            }

            return Problem();
        }

        [Route("delete-scooter/{id}")]
        [HttpDelete]
        public IActionResult DeleteScooter(int id)
        {
            var scooter = _scooterService.GetScooterById(id);

            if (_scooterValidators.All(v => v.IsValid(scooter)))
            {
                _scooterService.Delete(scooter);
                return Ok();
            }

            return Problem();
        }

        [Route("scooter/{id}")]//// remove when done
        [HttpGet]
        public IActionResult GetScooter(int id)
        {


            var s = _scooterService.GetScooterById(id);

            if (s == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ScooterRequest>(s);

            return Ok(response);

        }
    }
}