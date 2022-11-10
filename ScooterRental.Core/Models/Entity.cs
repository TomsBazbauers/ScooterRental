using ScooterRental.Core.Interfaces;

namespace ScooterRental.Core.Models
{
    public abstract class Entity : IEntity
    {
        public long Id { get; set; }
    }
}