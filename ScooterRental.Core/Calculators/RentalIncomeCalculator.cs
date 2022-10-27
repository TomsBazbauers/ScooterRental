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
        private decimal _maxDailyCharge = 20m;

        public decimal MaxDailyCharge
        {
            get => _maxDailyCharge;
            set => _maxDailyCharge = value;
        }

        public decimal CalculateIncome(List<RentalReport> reports)
        {
            decimal total = reports.Select(report => CalculatePerReport(report).PricePerMinute).ToList().Sum();

            return total;
        }

        public RentalReport CalculatePerReport(RentalReport report)
        {
            TimeSpan rentalPeriod = report.RentalEnd.Subtract(report.RentalStart);
            decimal incomePerDay = rentalPeriod.Days * _maxDailyCharge;
            decimal incomePerMinutes = rentalPeriod.Minutes * report.PricePerMinute;
            
            decimal total = Math.Round(incomePerDay + incomePerMinutes, 2);
            report.RentalIncome = total;

            return report;
        }
    }
}