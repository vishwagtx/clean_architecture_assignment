using Inventory.Domain.Entities;

namespace Inventory.Application.Intefaces
{
    public interface IProudctRepository : IRepository<Prodcut>
    {
        bool HasName(string name);
        bool HasName(int id, string name);
    }
}
