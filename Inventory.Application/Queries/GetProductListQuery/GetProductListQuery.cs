using MediatR;

namespace Inventory.Application.Queries
{
    public class GetProductListQuery : IRequest<GetProductListQueryResult>
    {
    }
}
