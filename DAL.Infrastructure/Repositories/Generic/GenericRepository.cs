using DAL.Domain;
using DAL.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure.Repositories.Generic
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public Task CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
