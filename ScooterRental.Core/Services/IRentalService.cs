using System;

namespace ScooterRental.Core.Services
{
    public interface IRentalService
    {
        void StartRental(long id);

        void EndRental(long id, DateTime rentalEnd);
    }
}