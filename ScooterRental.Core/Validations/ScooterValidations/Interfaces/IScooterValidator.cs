using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Validations
{
    public interface IScooterValidator
    {
        public bool IsValid(Scooter scooter);
    }
}