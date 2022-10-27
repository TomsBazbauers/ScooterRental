using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Services
{
    public interface IRentalService
    {
        void StartRental(int id);

        void EndRental(int id, DateTime rentalEnd);
    }
}