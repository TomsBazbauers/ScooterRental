using ScooterRental.Core.Models;

namespace ScooterRental.Core.Validations
{
    public class ScooterPropertyValidator : IScooterValidator
    {
        public bool IsValid(Scooter scooter)
        {
            return scooter != null;
        }
    }
}
