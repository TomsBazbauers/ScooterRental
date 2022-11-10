using ScooterRental.Core.Calculators;
using ScooterRental.Core.Models;
using System.Collections.Generic;

namespace ScooterRental.Core.Services
{
    public interface IReportService : IEntityService<RentalReport>
    {
        List<RentalReport> FilterReportsByYear(int year);

        List<RentalReport> FilterReportsByRentalStatus(List<RentalReport> reports, bool includeRunningRentals);

        IncomeReport GetIncomeForPeriod(int year = 0, bool includeRunningRentals = false);

        RentalReport GetSingleReport(long id);
    }
}