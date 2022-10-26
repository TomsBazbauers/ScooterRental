using ScooterRental.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Validations
{
    public class ScooterStatusValidator : IScooterValidator
    {
        public bool IsValid(Scooter scooter)
        {
            return !scooter.IsRented;
        }
    }
}