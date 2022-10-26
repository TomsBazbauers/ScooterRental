using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Services
{
    public interface IRentalService
    {
        public void StartRental(int id);

        public void EndRental(int id);
    }
}