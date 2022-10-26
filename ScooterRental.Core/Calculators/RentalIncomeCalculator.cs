using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Calculators
{
    public class RentalIncomeCalculator : IRentalIncomeCalculator
    {
        public decimal maxDailyCharge = 20m;
        public const int MINUTES_IN_DAY = 1440;

        public decimal MaxDailyCharge
        {
            get => maxDailyCharge;
            set => maxDailyCharge = value;
        }

        public decimal CalculateIncome(List<RentalReport> reports)
        {
            throw new NotImplementedException();
        }
    }
}