namespace ScooterRental.Models
{
    public class ScooterRequest
    {
        public int Id { get; set; }

        public decimal PricePerMinute { get; set; }

        public bool IsRented { get; set; }
    }
}