using Inventory.Application.Intefaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Identity;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserIdentity _identity;

        public UpdateProductCommandHandler(IUnitOfWork uow, IUserIdentity identity)
        {
            _uow = uow;
            _identity = identity;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Prodcut product = await _uow.Proudcts.GetById(request.Id);

            if (product == null)
                return new UpdateProductCommandResponse
                {
                    ErrorMessage = "Product is not availble for given id"
                };

            if (_uow.Proudcts.HasName(request.Id, request.Name))
                return new UpdateProductCommandResponse
                {
                    ErrorMessage = "Product name is already exist"
                };

            product.Name = request.Name;
            product.NoOfUnit = request.NoOfUnit;
            product.ReOrderLevel = request.ReOrderLevel;
            product.UnitPrice = request.UnitPrice;

            product.ModifiedBy = _identity.Name;
            product.ModifiedDateTime = DateTimeOffset.Now;

            _uow.Proudcts.Update(product);
            await _uow.SaveAsync();

            return new UpdateProductCommandResponse
            {
            };
        }
    }
}
