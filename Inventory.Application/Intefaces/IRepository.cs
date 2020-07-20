using Inventory.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Application.Intefaces
{
    public interface IRepository<T> where T : IEntity
    {
        T Save(T entity);
        T Update(T entity);
        Task<T> GetById(object id);
        Task<List<T>> GetAll();
        Task<bool> Delete(object id);
    }
}
