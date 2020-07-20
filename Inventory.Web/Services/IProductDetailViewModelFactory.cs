using Inventory.Web.Models;
using System.Threading.Tasks;

namespace Inventory.Web.Services
{
    public interface IProductDetailViewModelFactory
    {
        Task<ProductDetailViewModel> Create(int id);
    }
}
