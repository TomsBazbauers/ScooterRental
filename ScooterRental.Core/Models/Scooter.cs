namespace ScooterRental.Core.Models
{
    public class Scooter : Entity
    {
        public Scooter(decimal pricePerMinute, bool isRented)
        {
            PricePerMinute = pricePerMinute;
            IsRented = isRented;
        }

        public Scooter()
        { }

        public decimal PricePerMinute { get; set; }

        public bool IsRented { get; set; }
    }
}