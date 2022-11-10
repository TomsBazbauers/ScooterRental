namespace ScooterRental.Models
{
    public class ScooterRequest
    {
        public long Id { get; set; }

        public decimal PricePerMinute { get; set; }

        public bool IsRented { get; set; }
    }
}