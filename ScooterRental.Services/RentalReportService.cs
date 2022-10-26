using ScooterRental.Core.Calculators;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental.Services
{
    public class RentalReportService : IRentalReportService
    {
        private readonly IScooterRentalDbContext _context;
        private readonly IRentalIncomeCalculator _calculator;


        public RentalReportService(IScooterRentalDbContext context, IRentalIncomeCalculator calculator)
        {
            _context = context;
            _calculator = calculator;
        }

        public List<RentalReport> FilterReports(int year, bool includeRunningRentals)
        {
            return includeRunningRentals
                ? _context.RentalReports
                .Where(report => report.RentalStart.Year == year).ToList()
                : _context.RentalReports
                .Where(report => report.RentalStart.Year == year && report.RentalEnd != DateTime.MinValue).ToList();
        }

        public decimal GetIncomeForPeriod(int year, bool includeRunningRentals)
        {
            List<RentalReport> reports = new();

            if (includeRunningRentals)
            {
                reports = MockRentalEndTime(FilterReports(year, includeRunningRentals));
            }
            else
            {
                reports = FilterReports(year, includeRunningRentals);
            }

            return _calculator.CalculateIncome(reports);
        }

        public List<RentalReport> MockRentalEndTime(List<RentalReport> reports)
        {
            reports.Where(report => report.RentalEnd == DateTime.MinValue).ToList().ForEach(report => report.RentalEnd = DateTime.Now);

            return reports;
        }
    }
}