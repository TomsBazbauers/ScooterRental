using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Data;
using System.Linq;

namespace ScooterRental.Services
{
    public class ScooterService : EntityService<Scooter>, IScooterService
    {
        public ScooterService(IScooterRentalDbContext context) : base(context)
        { }

        public Scooter GetScooterById(int id)
        {
            return _context.Scooters.First(scooter => scooter.Id == id);
        }

        public Scooter UpdateScooter(Scooter scooterToUpdate, Scooter scooterToMatch)
        {
            scooterToUpdate.PricePerMinute = scooterToMatch.PricePerMinute;
            scooterToUpdate.IsRented = scooterToMatch.IsRented;

            return scooterToUpdate;
        }
    }
}