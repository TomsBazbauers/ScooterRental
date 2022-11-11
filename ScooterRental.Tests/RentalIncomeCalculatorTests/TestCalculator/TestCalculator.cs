using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Tests
{
    public static class TestCalculator
    {
        public static decimal SumTotal(List<RentalReport> reports)
        {
            decimal total = reports
                .Select(report => SumPerReport(report)).ToList().Sum();

            return total;
        }

        public static decimal SumPerReport(RentalReport report)
        {
            TimeSpan rentalPeriod = report.RentalEnd == DateTime.MinValue
                ? DateTime.Now.Subtract(report.RentalStart)
                : report.RentalEnd.Subtract(report.RentalStart);

            decimal totalIncome =
                (rentalPeriod.Days * 20) + (rentalPeriod.Minutes * report.PricePerMinute);

            return Math.Round(totalIncome, 2);
        }
    }
}