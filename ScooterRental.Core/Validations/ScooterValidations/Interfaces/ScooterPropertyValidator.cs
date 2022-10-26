using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Validations.ScooterValidations.Interfaces
{
    public class ScooterPropertyValidator : IScooterValidator
    {
        public bool IsValid(Scooter scooter)
        {
            return scooter != null;
        }
    }
}
