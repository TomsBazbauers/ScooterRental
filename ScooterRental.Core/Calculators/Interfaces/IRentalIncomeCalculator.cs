using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Calculators
{
    public interface IRentalIncomeCalculator
    {
        public decimal MaxDailyCharge { get; set; }

        decimal CalculateIncome(List<RentalReport> reports);
    }
}