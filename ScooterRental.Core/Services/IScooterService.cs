using ScooterRental.Core.Models;

namespace ScooterRental.Core.Services
{
    public interface IScooterService : IEntityService<Scooter>
    {
        Scooter GetScooterById(int id);

        public Scooter UpdateScooter(Scooter scooterToUpdate, Scooter scooterToMatch);
    }
}