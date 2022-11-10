﻿using ScooterRental.Core.Services;
using System;

namespace ScooterRental.Services
{
    public class RentalService : IRentalService
    {
        private readonly IScooterService _scooterService;

        public RentalService(IScooterService scooterService)
        {
            _scooterService = scooterService;
        }

        public void StartRental(long id)
        {
            var scooterToRent = _scooterService.GetScooterById(id);
            scooterToRent.IsRented = true;

            _scooterService.Update(scooterToRent);
        }

        public void EndRental(long id, DateTime rentalEnd)
        {
            var scooterToRent = _scooterService.GetScooterById(id);
            scooterToRent.IsRented = false;

            _scooterService.Update(scooterToRent);
        }
    }
}