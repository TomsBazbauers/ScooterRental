using ScooterRental.Core.Models;

namespace ScooterRental.Core.Services
{
    public interface IScooterService : IEntityService<Scooter>
    {
        Scooter GetScooterById(long id);

        ServiceResult UpdateScooter(Scooter scooterToUpdate, Scooter scooterToMatch);

        ServiceResult CreateScooter(Scooter scooter);

        ServiceResult DeleteScooter(Scooter scooter);
    }
}