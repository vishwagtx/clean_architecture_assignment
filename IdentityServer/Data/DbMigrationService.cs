using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer.Data
{
    public static class DbMigrationService
    {
        public static void AddDbMigrate(this IServiceCollection service)
        {
            var sp = service.BuildServiceProvider();
            var persistedGContext = sp.GetRequiredService<PersistedGrantDbContext>();
            persistedGContext.Database.Migrate();

            var configurationContext = sp.GetRequiredService<ConfigurationDbContext>();
            configurationContext.Database.Migrate();

            var identityContext = sp.GetService<ApplicationDbContext>();
            identityContext.Database.Migrate();

            Migrate(sp, configurationContext, identityContext);
        }

        private static void Migrate(ServiceProvider sp, ConfigurationDbContext configurationContext, ApplicationDbContext identityContext)
        {
            if (!configurationContext.IdentityResources.AnyAsync().Result)
            {
                configurationContext.IdentityResources.Add(new IdentityResources.OpenId().ToEntity());
                configurationContext.IdentityResources.Add(new IdentityResources.Email().ToEntity());
                configurationContext.IdentityResources.Add(new IdentityResources.Profile().ToEntity());

                configurationContext.SaveChanges();
            }

            if (!configurationContext.ApiResources.AnyAsync().Result)
            {
                var api = new ApiResource("i_api", "Web Api call")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) },
                    Scopes =
                        {
                            "i_api.full",
                            "i_api.read"
                        },
                    UserClaims =
                    {
                        ClaimTypes.NameIdentifier,
                        ClaimTypes.Name,
                        ClaimTypes.Email,
                        ClaimTypes.Role,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    }
                };
                configurationContext.ApiResources.Add(api.ToEntity());

                configurationContext.SaveChanges();
            }

            if (!configurationContext.Clients.AnyAsync().Result)
            {
                List<Client> clients = new List<Client>() {
                     new Client
                    {
                        ClientId = "mvc",
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = GrantTypes.Code,

                        // where to redirect to after login
                        RedirectUris = { "http://localhost:52484/signin-oidc" },

                        // where to redirect to after logout
                        PostLogoutRedirectUris = { "http://localhost:52484/signout-callback-oidc" },

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Email,
                            "i_api"
                        }
                    }
                };

                foreach (var client in clients)
                {
                    configurationContext.Clients.Add(client.ToEntity());
                }

                configurationContext.SaveChanges();
            }
        }
    }
}
