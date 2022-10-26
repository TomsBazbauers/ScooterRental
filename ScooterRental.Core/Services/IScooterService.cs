using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Services
{
    public interface IScooterService : IEntityService<Scooter>
    {
        Scooter GetScooterById(int id);

        public bool StartRental(int id);

        public bool EndRental(int id);

        bool IsFound(Scooter scooter);

        void Clear();
    }
}