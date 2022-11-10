using ScooterRental.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        ServiceResult Create(T entity);

        ServiceResult Delete(T entity);

        ServiceResult Update(T entity);

        List<T> GetAll();

        T GetById(long id);

        IQueryable<T> Query();
    }
}