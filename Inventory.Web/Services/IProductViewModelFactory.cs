using Inventory.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Web.Services
{
    public interface IProductViewModelFactory
    {
        ProductViewModel Create();
        Task<ProductViewModel> Create(int id);
        Task<(ProductViewModel Model, List<string> Errors)> ExceuteSave(ProductViewModel model);
        Task<(ProductViewModel Model, List<string> Errors)> ExceuteUpdate(ProductViewModel model);
    }
}
