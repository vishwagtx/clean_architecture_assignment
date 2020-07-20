using Inventory.Application.Intefaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Identity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserIdentity _identity;

        public CreateProductCommandHandler(IUnitOfWork uow, IUserIdentity identity)
        {
            _uow = uow;
            _identity = identity;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(_uow.Proudcts.HasName(request.Name))
                return new CreateProductCommandResponse
                {
                    Id = 0,
                    ErrorMessage = "Product name is already exist"
                };

            Prodcut product = new Prodcut
            {
                Name = request.Name,
                NoOfUnit = request.NoOfUnit,
                ReOrderLevel = request.ReOrderLevel,
                UnitPrice = request.UnitPrice,
                CreatedBy = _identity.Name,
                CreatedDateTime = DateTimeOffset.Now
            };

            product = _uow.Proudcts.Save(product);
            await _uow.SaveAsync();

            return new CreateProductCommandResponse
            {
                Id = product.Id
            };
        }
    }
}
