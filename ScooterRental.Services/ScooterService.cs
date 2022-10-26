using ScooterRental.Core.Models;
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

        public Scooter UpdateScooter(Scooter scooterToUpdate, Scooter scooterToMatch)
        {
            scooterToUpdate.PricePerMinute = scooterToMatch.PricePerMinute;
            scooterToUpdate.IsRented = scooterToMatch.IsRented;

            return scooterToUpdate;
        }
    }
}