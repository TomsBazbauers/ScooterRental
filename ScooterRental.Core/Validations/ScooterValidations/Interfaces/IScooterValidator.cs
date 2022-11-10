using ScooterRental.Core.Models;

namespace ScooterRental.Core.Validations
{
    public interface IScooterValidator
    {
        public bool IsValid(Scooter scooter);
    }
}