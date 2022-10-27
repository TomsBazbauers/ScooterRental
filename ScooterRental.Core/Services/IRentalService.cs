using System;

namespace ScooterRental.Core.Services
{
    public interface IRentalService
    {
        void StartRental(int id);

        void EndRental(int id, DateTime rentalEnd);
    }
}