using Inventory.Application.Intefaces;
using Inventory.Domain.Entities;
using System.Linq;

namespace Inventory.Persistance.Repositories
{
    public class ProudctRepository : Repository<Prodcut>, IProudctRepository
    {
        private readonly InventoryDbContext _dbContext;
        public ProudctRepository(InventoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool HasName(string name)
        {
            return _dbContext.Prodcuts.Any(x => x.Name == name);
        }

        public bool HasName(int id, string name)
        {
            return _dbContext.Prodcuts.Any(x=> x.Id != id  && x.Name == name);
        }
    }
}
