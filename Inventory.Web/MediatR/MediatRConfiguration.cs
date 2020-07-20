using Inventory.Application.Commands;
using Inventory.Application.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Web.MediatR
{
    internal static class MediatRConfiguration
    {
        internal static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup),
                typeof(GetProductListQueryHandler),
                typeof(GetProductByIdQueryHandler),
                typeof(CreateProductCommandHandler),
                typeof(UpdateProductCommandHandler),
                typeof(DeleteProductCommandHandler)
             );

            return services;
        }
    }
}
