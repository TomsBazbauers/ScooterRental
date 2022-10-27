using ScooterRental.Core.Models;

namespace ScooterRental.Core.Validations
{
    public class ScooterPriceValidator : IScooterValidator
    {
        public bool IsValid(Scooter scooter)
        {
            return scooter.PricePerMinute >= 0;
        }
    }
}