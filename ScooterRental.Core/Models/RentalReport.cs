using System;

namespace ScooterRental.Core.Models
{
    public class RentalReport : Entity
    {
        public RentalReport(long id, decimal rate, DateTime rentalStart)
        {
            ScooterId = id;
            PricePerMinute = rate;
            RentalStart = rentalStart;
            RentalEnd = DateTime.MinValue;
        }

        public RentalReport() { }

        public long ScooterId { get; set; }

        public decimal PricePerMinute { get; set; }

        public DateTime RentalStart { get; set; }

        public DateTime RentalEnd { get; set; }

        public decimal RentalIncome { get; set; }
    }
}