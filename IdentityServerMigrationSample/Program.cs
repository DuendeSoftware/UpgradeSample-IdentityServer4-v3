using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Security.Claims;

namespace IdentityServerMigrationSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var provider = CreateHostBuilder(args).Build().Services;
            using (var scope = provider.CreateScope())
            {
                var grants = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                grants.Database.Migrate();
                
                var config = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                config.Database.Migrate();

                // this populates an IS4 v3 DB with test data to migrate
#if false
                if (!grants.DeviceFlowCodes.Any())
                {
                    grants.DeviceFlowCodes.Add(new IdentityServer4.EntityFramework.Entities.DeviceFlowCodes
                    {
                        DeviceCode = "device",
                        UserCode = "user",
                        CreationTime = DateTime.UtcNow,
                        Expiration = DateTime.UtcNow.AddMinutes(1),
                        ClientId = "client",
                        SubjectId = "sub",
                        Data = "data",
                    });
                }

                if (!grants.PersistedGrants.Any())
                {
                    grants.PersistedGrants.Add(new IdentityServer4.EntityFramework.Entities.PersistedGrant()
                    {
                        Key = "key",
                        CreationTime = DateTime.UtcNow,
                        ClientId = "client",
                        SubjectId = "sub",
                        Type = "type",
                        Data = "data",
                    });
                }
                grants.SaveChanges();


                if (!config.Clients.Any())
                {
                    config.Clients.Add(new Client
                    {
                        ClientId = "client",
                        AllowedGrantTypes = GrantTypes.Code,
                        ClientSecrets = { new Secret("secret") },
                        RedirectUris = { "https://sample" },
                        PostLogoutRedirectUris = { "https://sample" },
                        AllowedCorsOrigins = { "https://sample" },
                        AllowedScopes = { "openid", "custom", "scope" },
                        Properties = { { "prop", "client" } },
                        Claims = { new Claim("claim", "client") },
                        IdentityProviderRestrictions = { "idp" }
                    }.ToEntity());
                }

                if (!config.IdentityResources.Any())
                {
                    config.IdentityResources.Add(new IdentityResources.OpenId().ToEntity());
                    config.IdentityResources.Add(new IdentityResource
                    { 
                        Name = "custom",
                        UserClaims = { "foo" },
                        Properties = { { "prop", "client" } }
                    }.ToEntity());
                }

                if (!config.ApiResources.Any())
                {
                    config.ApiResources.Add(new ApiResource
                    {
                        Name = "api",
                        DisplayName = "Api Name",
                        UserClaims = { "name" },
                        Scopes =
                        {
                            new Scope("scope")
                            {
                                UserClaims = { "email" },
                            }
                        },
                        ApiSecrets = { new Secret("xoxo") },
                        Properties = { { "prop", "api" } }
                    }.ToEntity());
                }

                config.SaveChanges();
#endif
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
