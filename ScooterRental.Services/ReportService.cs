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

        public List<RentalReport> FilterReportsByYear(int? year)
        {
            return year != 0
                ? _context.RentalReports.Where(report => report.RentalStart.Year == year).ToList()
                : _context.RentalReports.ToList();
        }

        public List<RentalReport> FilterReportsByRentalStatus(List<RentalReport> reports, bool includeRunningRentals = false)
        {
            return includeRunningRentals
                ? MockRentalEnd(reports)
                : reports.Where(report => report.RentalEnd != DateTime.MinValue).ToList();
        }

        public IncomeReport GetIncomeForPeriod(int year = 0, bool includeRunningRentals = false)
        {
            var filteredReports = FilterReportsByYear(year);
            filteredReports = FilterReportsByRentalStatus(filteredReports, includeRunningRentals);

            var incomePerPeriod = _calculator.CalculateIncome(filteredReports);

            return new IncomeReport(year, incomePerPeriod);
        }

        public List<RentalReport> MockRentalEnd(List<RentalReport> reports)
        {
            reports.Where(report => report.RentalEnd == DateTime.MinValue)
                .ToList().ForEach(report => report.RentalEnd = DateTime.Now);

            return reports;
        }

        public RentalReport GetSingleReport(int id, DateTime rentalEnd)
        {
            var report = _context.RentalReports.First(report => report.Id == id && report.RentalEnd == rentalEnd);
            report.RentalIncome = _calculator.CalculateIncome(new List<RentalReport>() { report });

            return report;
        }
    }
}