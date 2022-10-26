using ScooterRental.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Core.Models
{
    public abstract class Entity : IEntity
    {
        public int Id {get; set;}
    }
}