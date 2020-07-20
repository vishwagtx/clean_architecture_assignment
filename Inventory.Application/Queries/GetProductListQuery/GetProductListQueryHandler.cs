using Inventory.Application.Intefaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Queries
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, GetProductListQueryResult>
    {
        private readonly IUnitOfWork _uow;

        public GetProductListQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<GetProductListQueryResult> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entities = await _uow.Proudcts.GetAll();

            return new GetProductListQueryResult
            {
                Products = entities.Select(s => new ProductListDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    NoOfUnit = s.NoOfUnit,
                    ReOrderLevel = s.ReOrderLevel,
                    UnitPrice = s.UnitPrice
                }).ToList()
            };
        }
    }
}
