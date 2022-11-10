namespace ScooterRental.Models
{
    public class ScooterRequest
    {
        public decimal PricePerMinute { get; set; }

        public bool IsRented { get; set; }
    }
}