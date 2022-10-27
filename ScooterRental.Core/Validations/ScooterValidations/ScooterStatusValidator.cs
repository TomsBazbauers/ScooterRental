using ScooterRental.Core.Models;

namespace ScooterRental.Core.Validations
{
    public class ScooterStatusValidator : IScooterValidator
    {
        public bool IsValid(Scooter scooter)
        {
            return !scooter.IsRented;
        }
    }
}