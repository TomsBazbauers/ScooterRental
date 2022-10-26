using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Models
{
    public class IncomeReport : Entity
    {
        public IncomeReport(int periodYear, decimal rentalIncome)
        {
            PeriodYear = periodYear;
            RentalIncome = rentalIncome;
        }

        public IncomeReport() 
        { }

        public int PeriodYear { get; set; } 

        public decimal RentalIncome { get; set; }
    }
}