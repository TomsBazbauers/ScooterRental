using ScooterRental.Core.Models;
using ScooterRental.Core.Services;
using ScooterRental.Data;
using System;

namespace ScooterRental.Services
{
    public class ScooterService : EntityService<Scooter>, IScooterService
    {
        public ScooterService(IScooterRentalDbContext context) : base(context)
        { }

        public ServiceResult CreateScooter(Scooter scooter)
        {
            return Create(scooter);
        }

        public Scooter GetScooterById(long id)
        {
            return GetById(id);
        }

        public ServiceResult DeleteScooter(Scooter scooter)
        {
            return Delete(scooter);
        }

        public ServiceResult StartRental(long id)
        {
            var scooter = GetById(id);

            if(!scooter.IsRented)
            {
                scooter.IsRented = true;
                return Update(scooter);
            }

            return new ServiceResult(false);
        }

        public ServiceResult EndRental(long id)
        {
            var scooter = GetById(id);

            if (scooter == null || !scooter.IsRented)
            {
                return new ServiceResult(false);
            }

            scooter.IsRented = false;

            return Update(scooter);
        }
    }
}