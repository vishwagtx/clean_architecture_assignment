using Inventory.Application.Intefaces;
using Inventory.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Persistance.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly InventoryDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<bool> Delete(object id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;
            _dbSet.Remove(entity);

            return true;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T Save(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public T Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
