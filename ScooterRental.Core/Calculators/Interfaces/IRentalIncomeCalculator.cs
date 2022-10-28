using ScooterRental.Core.Models;
using System.Collections.Generic;

namespace ScooterRental.Core.Calculators
{
    public interface IRentalIncomeCalculator
    {
        public decimal MaxDailyCharge { get; set; }

        decimal CalculateIncome(List<RentalReport> reports);

        decimal CalculatePerReport(RentalReport report);
    }
}