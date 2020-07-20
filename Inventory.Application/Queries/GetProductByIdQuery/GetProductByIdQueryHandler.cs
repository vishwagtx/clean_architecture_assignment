using Inventory.Application.Intefaces;
using Inventory.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
    {
        private readonly IUnitOfWork _uow;

        public GetProductByIdQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Prodcut product = await _uow.Proudcts.GetById(request.Id);

            if (product == null)
                return null;

            return new GetProductByIdQueryResponse
            {
                Id = product.Id,
                Name = product.Name,
                NoOfUnit = product.NoOfUnit,
                UnitPrice = product.UnitPrice,
                ReOrderLevel = product.ReOrderLevel,
                CreatedBy = product.CreatedBy,
                CreatedDateTime = product.CreatedDateTime,
                ModifiedBy = product.ModifiedBy,
                ModifiedDateTime = product.ModifiedDateTime
            };
        }
    }
}
