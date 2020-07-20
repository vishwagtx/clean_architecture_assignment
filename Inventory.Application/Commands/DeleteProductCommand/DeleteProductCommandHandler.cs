using Inventory.Application.Intefaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _uow;

        public DeleteProductCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            bool result = await _uow.Proudcts.Delete(request.Id);
            await _uow.SaveAsync();

            return result;
        }
    }
}
