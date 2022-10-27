using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Models
{
    public class RentalReport : Entity
    {
        public RentalReport(int id, decimal rate, DateTime rentalStart)
        {
            ScooterId = id;
            PricePerMinute = rate;
            RentalStart = rentalStart;
            RentalEnd = DateTime.MinValue;
        }

        public RentalReport() { }

        public int ScooterId { get; set; }

        public decimal PricePerMinute { get; set; }

        public DateTime RentalStart { get; set; }

        public DateTime RentalEnd { get; set; }

        public decimal RentalIncome { get; set; }
    }
}