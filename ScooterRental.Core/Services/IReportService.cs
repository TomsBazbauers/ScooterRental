using ScooterRental.Core.Models;
using System.Collections.Generic;

namespace ScooterRental.Core.Services
{
    public interface IReportService : IEntityService<RentalReport>
    {
        List<RentalReport> FilterReportsByYear(int year);

        public List<RentalReport> FilterReportsByRentalStatus(List<RentalReport> reports, bool includeRunningRentals);

        public IncomeReport GetIncomeForPeriod(int year = 0, bool includeRunningRentals = false);

        public RentalReport GetSingleReport(int id);
    }
}