namespace ScooterRental.Core.Models
{
    public class Scooter : Entity
    {
        public decimal PricePerMinute { get; set; }

        public bool IsRented { get; set; }
    }
}