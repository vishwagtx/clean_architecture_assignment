using Inventory.Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Identity
{
    public class IdentityMiddlewareExtensions
    {
        private readonly RequestDelegate _next;

        public IdentityMiddlewareExtensions(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IUserIdentity userIdentity)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                userIdentity.Name = httpContext.User.Identity.Name;
                userIdentity.Id = httpContext.User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
                userIdentity.Email = httpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            }

            return _next(httpContext);
        }
    }

    public static class IdentityMiddlewareExtensionsExtensions
    {
        public static IApplicationBuilder UseUserIdentityMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdentityMiddlewareExtensions>();
        }
    }
}
