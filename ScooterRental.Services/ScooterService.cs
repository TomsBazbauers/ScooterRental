using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Data;

namespace ScooterRental.Services
{
    public class ScooterService : EntityService<Scooter>, IScooterService
    {
        public ScooterService(IScooterRentalDbContext context) : base(context)
        { }

        public Scooter GetScooterById(int id)
        {
            Scooter scooter;

            try
            {
                scooter = GetById(id);
            }
            catch
            {
                scooter = null;
            }

            return scooter;
        }

        public ServiceResult CreateScooter(Scooter scooter)
        {
            return Create(scooter);
        }

        public ServiceResult DeleteScooter(Scooter scooter)
        {
            return Delete(scooter);
        }

        public ServiceResult UpdateScooter(Scooter scooterToUpdate, Scooter scooterToMatch)
        {
            scooterToUpdate.PricePerMinute = scooterToMatch.PricePerMinute;
            scooterToUpdate.IsRented = scooterToMatch.IsRented;

            return Update(scooterToUpdate);
        }
    }
}