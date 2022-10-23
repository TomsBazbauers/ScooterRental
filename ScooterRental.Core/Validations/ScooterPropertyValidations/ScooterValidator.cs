using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Validations
{
    public class ScooterValidator : IScooterValidator
    {
        public bool IsValid(string id, decimal pricePerMinute)
        {
            return !string.IsNullOrEmpty(id) && pricePerMinute > 0;
        }
    }
}