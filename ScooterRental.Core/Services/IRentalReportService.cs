using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Services
{
    public interface IRentalReportService
    {
        public List<RentalReport> FilterReports(int year, bool includeRunningRentals);

        public decimal GetIncomeForPeriod(int year, bool includeRunningRentals);

        public List<RentalReport> MockRentalEndTime(List<RentalReport> reports);
    }
}