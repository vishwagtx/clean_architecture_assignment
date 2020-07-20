using System.Threading.Tasks;

namespace Inventory.Application.Intefaces
{
    public interface IUnitOfWork
    {
        IProudctRepository Proudcts { get; }

        Task SaveAsync();
        void Save();
    }
}
