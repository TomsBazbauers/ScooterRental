using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Services
{
    public interface IReportService : IEntityService<RentalReport>
    {
        List<RentalReport> FilterReports(bool includeRunningRentals, int year);

        RentalReport GetReport(int scooterId);

        decimal GetIncomeForPeriod(bool includeRunningRentals, int year);

        List<RentalReport> MockRentalEnd(List<RentalReport> reports);
    }
}