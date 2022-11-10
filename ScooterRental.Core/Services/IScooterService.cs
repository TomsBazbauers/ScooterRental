using ScooterRental.Core.Models;

namespace ScooterRental.Core.Services
{
    public interface IScooterService : IEntityService<Scooter>
    {
        Scooter GetScooterById(long id);

        ServiceResult CreateScooter(Scooter scooter);

        ServiceResult DeleteScooter(Scooter scooter);

        ServiceResult StartRental(long id);

        ServiceResult EndRental(long id);
    }
}