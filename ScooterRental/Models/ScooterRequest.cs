namespace ScooterRental.Models
{
    public class ScooterRequest
    {
        public ScooterRequest(decimal pricePerMinute, bool isRented)
        {
            PricePerMinute = pricePerMinute;
            IsRented = isRented;
        }

        public ScooterRequest()
        { }

        public decimal PricePerMinute { get; set; }

        public bool IsRented { get; set; }
    }
}