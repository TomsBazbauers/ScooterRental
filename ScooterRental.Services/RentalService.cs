using ScooterRental.Core.Services;
using ScooterRental.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Services
{
    public class RentalService : IRentalService
    {
        private readonly IScooterService _scooterService;

        public RentalService(IScooterService scooterService)
        {
            _scooterService = scooterService;
        }

        public void StartRental(int id)
        {
            var scooterToRent = _scooterService.GetScooterById(id);
            
            scooterToRent.IsRented = true;
            _scooterService.Update(scooterToRent);
        }

        public void EndRental(int id)
        {
            throw new NotImplementedException();
        }
    }
}
