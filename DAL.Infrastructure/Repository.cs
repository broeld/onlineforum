using DAL.Domain;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected DbSet<T> dbset { get; }

        public Repository(DbContext context)
        {
            dbset = context.Set<T>();
            dbset.AsNoTracking();
        }

        public abstract IQueryable<T> DbsetWithProperties();

        public async Task CreateAsync(T entity)
        {
            if (entity != null)
            {
                await dbset.AddAsync(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await DbsetWithProperties().AsNoTracking().ToListAsync();

            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await DbsetWithProperties().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            return entity;
        }

        public void Remove(T entity)
        {
            if (entity != null)
            {
                dbset.Remove(entity);
            }
        }

        public void Update(T entity)
        {
            if (entity != null)
            {
                dbset.Update(entity);
            }
        }

        public async Task<IEnumerable<T>> GetWithIncludeAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            return await IncludeProperties(includeProperties).ToListAsync();
        }

        public IQueryable<T> IncludeProperties(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbset;

            foreach (var property in includeProperties)
            {
                if (property != null)
                {
                    query = query.Include(property);
                }
            }

            return query.AsNoTracking();
        }

    }
}
