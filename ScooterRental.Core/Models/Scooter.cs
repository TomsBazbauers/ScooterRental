using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Models
{
    public class Scooter : Entity
    {
        public decimal PricePerMinute { get; set; }

        public bool IsRented { get; set; }
    }
}