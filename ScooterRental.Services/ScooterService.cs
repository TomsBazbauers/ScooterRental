﻿using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Services
{
    public class ScooterService : EntityService<Scooter>, IScooterService
    {
        public ScooterService(IScooterRentalDbContext context) : base(context)
        {}

        public bool StartRental(int id)
        {
            bool rentalStarted = false;
            var scooterToRent = GetScooterById(id);

            if(scooterToRent != null)
            {
                scooterToRent.IsRented = true;
                rentalStarted = true;
            }

            return rentalStarted;
        }

        public Scooter GetScooterById(int id)
        {
            return _context.Scooters.First(scooter => scooter.Id == id);
        }

        public bool IsFound(Scooter scooter)
        {
            return _context.Scooters.Any(s => s.Id == scooter.Id);
        }

        public void Clear()
        {
            _context.Scooters.RemoveRange(_context.Scooters);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var isRemoved = false;
            var scooter = GetScooterById(id);

            if(scooter != null && !scooter.IsRented)
            {
                _context.Scooters.Remove(scooter);
                _context.SaveChanges();
                isRemoved = true;
            }

            return isRemoved;  
        }

        public bool EndRental(int id)////
        {
            throw new NotImplementedException();
        }
    }
}