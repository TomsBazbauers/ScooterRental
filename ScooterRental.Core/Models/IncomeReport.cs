namespace ScooterRental.Core.Models
{
    public class IncomeReport
    {
        public IncomeReport(int periodYear, decimal rentalIncome, int rentals)
        {
            PeriodYear = periodYear;
            RentalIncome = rentalIncome;
            Rentals = rentals;
        }

        public IncomeReport()
        { }

        public int Rentals { get; set; }

        public int PeriodYear { get; set; }

        public decimal RentalIncome { get; set; }
    }
}