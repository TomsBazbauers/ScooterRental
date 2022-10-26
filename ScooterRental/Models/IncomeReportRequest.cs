namespace ScooterRental.Models
{
    public class IncomeReportRequest
    {
        public int? Year { get; set; }

        public bool IncludeRunningRentals { get; set; }
    }
}