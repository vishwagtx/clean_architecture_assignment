using Inventory.Application.Commands;
using Inventory.Application.Queries;
using Inventory.Web.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Web.Services
{
    public class ProductViewModelFactory : IProductViewModelFactory
    {
        private readonly IMediator _bus;

        public ProductViewModelFactory(IMediator bus)
        {
            _bus = bus;
        }

        public ProductViewModel Create()
        {
            return new ProductViewModel();
        }

        public async Task<ProductViewModel> Create(int id)
        {
            var product = await _bus.Send(new GetProductByIdQuery { Id = id });
            if (product == null)
                return new ProductViewModel { };

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                NoOfUnit = product.NoOfUnit,
                ReOrderLevel = product.ReOrderLevel,
                UnitPrice = product.UnitPrice
            };
        }

        public async Task<(ProductViewModel Model, List<string> Errors)> ExceuteSave(ProductViewModel model)
        {
            CreateProductCommand command = new CreateProductCommand()
            {
                Name = model.Name,
                NoOfUnit = model.NoOfUnit,
                ReOrderLevel = model.ReOrderLevel,
                UnitPrice = model.UnitPrice
            };

            var result = await _bus.Send(command);

            model.Id = result.Id;

            return (model, !string.IsNullOrEmpty(result.ErrorMessage) ? new List<string> { result.ErrorMessage } : new List<string>());
        }


        public async Task<(ProductViewModel Model, List<string> Errors)> ExceuteUpdate(ProductViewModel model)
        {
            UpdateProductCommand command = new UpdateProductCommand()
            {
                Id = model.Id,
                Name = model.Name,
                NoOfUnit = model.NoOfUnit,
                ReOrderLevel = model.ReOrderLevel,
                UnitPrice = model.UnitPrice
            };

            var result = await _bus.Send(command);

            return (model, !string.IsNullOrEmpty(result.ErrorMessage) ? new List<string> { result.ErrorMessage } : new List<string>());
        }
    }
}
