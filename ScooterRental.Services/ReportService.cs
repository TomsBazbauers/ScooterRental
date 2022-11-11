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
        { }

        public ServiceResult CreateReport(Scooter scooter, DateTime? rentalStart = null)
        {
            return Create(new RentalReport(scooter.Id, scooter.PricePerMinute, rentalStart));
        }

        public List<RentalReport> FilterReportsByYear(int year)
        {
            return year != 0
                ? Query<RentalReport>().Where(report => report.RentalStart.Year == year).ToList()
                : Query<RentalReport>().ToList();
        }

        public List<RentalReport> FilterReportsByRentalStatus(List<RentalReport> reports, bool includeRunningRentals)
        {
            return includeRunningRentals
                ? reports
                : Query<RentalReport>().Where(report => report.RentalEnd != DateTime.MinValue).ToList();
        }

        public IncomeReport GetIncomeForPeriod(int year = 0, bool includeRunningRentals = false)
        {
            var filteredReports = FilterReportsByYear(year);
            filteredReports = FilterReportsByRentalStatus(filteredReports, includeRunningRentals);

            var incomePerPeriod = _calculator.CalculateIncome(filteredReports);

            return new IncomeReport(year, incomePerPeriod, filteredReports.Count);
        }

        public RentalReport GetSingleReport(long id)
        {
            var report = Query<RentalReport>()
                .FirstOrDefault(report => report.ScooterId == id && report.RentalEnd == DateTime.MinValue);

            report.RentalEnd = DateTime.Now;

            var income = _calculator.CalculatePerReport(report);
            report.RentalIncome = income;
            _context.RentalReports.Update(report);

            return report;
        }
    }
}