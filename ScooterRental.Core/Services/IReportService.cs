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
        List<RentalReport> FilterReportsByYear(int year);

        public List<RentalReport> FilterReportsByRentalStatus(List<RentalReport> reports, bool includeRunningRentals);

        public IncomeReport GetIncomeForPeriod(int year = 0, bool includeRunningRentals = false);

        //public List<RentalReport> MockRentalEnd(List<RentalReport> reports);

        public RentalReport GetSingleReport(int id);
    }
}