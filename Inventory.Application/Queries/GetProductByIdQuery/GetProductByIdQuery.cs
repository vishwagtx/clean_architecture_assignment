using MediatR;

namespace Inventory.Application.Queries
{
    public class GetProductByIdQuery : IRequest<GetProductByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
