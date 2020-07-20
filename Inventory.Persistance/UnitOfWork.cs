using Inventory.Application.Intefaces;
using Inventory.Persistance.Repositories;
using System.Threading.Tasks;

namespace Inventory.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProudctRepository _proudcts = null;

        private readonly InventoryDbContext _dbContext;

        public UnitOfWork(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IProudctRepository Proudcts
        {
            get
            {
                if (_proudcts == null)
                    _proudcts = new ProudctRepository(_dbContext);
                return _proudcts;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
