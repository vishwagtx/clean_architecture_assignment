using Inventory.Application.Queries;
using Inventory.Web.Models;
using MediatR;
using System.Threading.Tasks;

namespace Inventory.Web.Services
{
    public class ProductDetailViewModelFactory : IProductDetailViewModelFactory
    {
        private readonly IMediator _bus;

        public ProductDetailViewModelFactory(IMediator bus)
        {
            _bus = bus;
        }

        public async Task<ProductDetailViewModel> Create(int id)
        {
            var product = await _bus.Send(new GetProductByIdQuery { Id = id });
            if (product == null)
                return new ProductDetailViewModel { };

            return new ProductDetailViewModel
            {
                Id = product.Id,
                Name = product.Name,
                NoOfUnit = product.NoOfUnit,
                ReOrderLevel = product.ReOrderLevel,
                UnitPrice = product.UnitPrice,
                CreatedBy = product.CreatedBy,
                CreatedDateTime = product.CreatedDateTime,
                ModifiedBy = product.ModifiedBy,
                ModifiedDateTime = product.ModifiedDateTime
            };
        }
    }
}
