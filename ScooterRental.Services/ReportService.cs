using ScooterRental.Core.Calculators;
using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental.Services
{
    public class ReportService : EntityService<RentalReport>, IReportService
    {
        private readonly IRentalIncomeCalculator _calculator;

        public ReportService(IScooterRentalDbContext context) : base(context)
        {
            _calculator = new RentalIncomeCalculator();
        }

        public RentalReport GetSingleReport(int scooterId)
        {
            return _context.RentalReports
                .First(report => report.ScooterId == scooterId
                && report.RentalEnd == DateTime.MinValue);
        }

        public List<RentalReport> FilterReports(bool includeRunningRentals, int year = 0)
        {
            List<RentalReport> filteredReports = new();

            if (includeRunningRentals && year == 0)
            {
                filteredReports = _context.RentalReports.ToList();
            }
            else if (includeRunningRentals && year != 0)
            {
                filteredReports = _context.RentalReports
                    .Where(report => report.RentalStart.Year == year).ToList();
            }
            else if (!includeRunningRentals && year != 0)
            {
                filteredReports = _context.RentalReports
                    .Where(report => report.RentalStart.Year == year && report.RentalEnd != DateTime.MinValue).ToList();
            }
            else
            {
                filteredReports = _context.RentalReports.Where(report => report.RentalEnd != DateTime.MinValue).ToList();
            }

            return filteredReports;
        }

        public decimal GetIncomeForPeriod(bool includeRunningRentals, int year = 0)
        {
            List<RentalReport> reports = new();

            if (includeRunningRentals)
            {
                reports = MockRentalEnd(FilterReports(includeRunningRentals, year));
            }
            else
            {
                reports = FilterReports(includeRunningRentals, year);
            }

            return _calculator.CalculateIncome(reports);
        }

        public List<RentalReport> MockRentalEnd(List<RentalReport> reports)
        {
            reports.Where(report => report.RentalEnd == DateTime.MinValue).ToList().ForEach(report => report.RentalEnd = DateTime.Now);

            return reports;
        }
    }
}