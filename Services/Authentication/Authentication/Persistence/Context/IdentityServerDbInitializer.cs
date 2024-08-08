using Authentication.Configuration;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Persistence.Context
{
    internal static class IdentityServerDbInitializer
    {
        internal static void Initialize(IApplicationBuilder app, IConfiguration configuration)
        {
            using var scope = app.ApplicationServices
                .GetService<IServiceScopeFactory>()
                .CreateScope();

            scope.ServiceProvider
                .GetRequiredService<PersistedGrantDbContext>()
                .Database.Migrate();

            var configurationContext = scope.ServiceProvider
                .GetRequiredService<ConfigurationDbContext>();

            configurationContext.Database.Migrate();

            // Clients
            var clientsConfig = Config.Clients(configuration).Select(x => x.ToEntity()).ToList();
            var existingClients = configurationContext.Clients.ToList();

            var clientsToAdd = clientsConfig
                .Where(x => existingClients.All(_ => _.ClientId != x.ClientId))
                .ToList();

            if (clientsToAdd.Any())
            {

                configurationContext.Clients.AddRange(clientsToAdd);
                configurationContext.SaveChanges();
            }
            
            // Identity Resources
            var existingIdentityResources = configurationContext.IdentityResources.ToList();
            var identityResources = Config.Ids.Select(x => x.ToEntity()).ToList();
            var identityResourcesToAdd = identityResources
                .Where(x => existingIdentityResources.All(_ => _.Name != x.Name))
                .ToList();

            if (identityResourcesToAdd.Any())
            {
                configurationContext.IdentityResources.AddRange(identityResourcesToAdd);
                configurationContext.SaveChanges();
            }

            // Api Resources
            var existingApiResources = configurationContext.ApiResources.ToList();
            var apiResources = Config.Apis.Select(x => x.ToEntity()).ToList();
            var apiResourcesToAdd = apiResources
                .Where(x => existingApiResources.All(_ => _.Name != x.Name))
                .ToList();

            if (apiResourcesToAdd.Any())
            {
                configurationContext.ApiResources.AddRange(apiResourcesToAdd);
                configurationContext.SaveChanges();
            }

            // Api Scopes
            var existingApiScope = configurationContext.ApiScopes.ToList();
            var apiScope = Config.ApisScopes.Select(x => x.ToEntity()).ToList();
            var apiScopeToAdd = apiScope
                .Where(x => existingApiScope.All(_ => _.Name != x.Name))
                .ToList();

            if (apiScopeToAdd.Any())
            {
                configurationContext.ApiScopes.AddRange(apiScopeToAdd);
                configurationContext.SaveChanges();
            }

            SeedMissingClientClaimsAndScopes(clientsConfig, configurationContext);

        }

        private static void SeedMissingClientClaimsAndScopes(
            IReadOnlyCollection<Client> clientsConfig, ConfigurationDbContext configurationContext)
        {
            var existingClients = configurationContext.Clients
                .Include(x => x.Claims)
                .Include(x => x.AllowedScopes)
                .ToList();

            foreach (var existingClient in existingClients)
            {
                var clientConfig = clientsConfig
                    .FirstOrDefault(x => x.ClientId == existingClient.ClientId);

                if (clientConfig is null) continue;

                var missingClaims = clientConfig.Claims
                    .Where(m => existingClient.Claims.All(e => e.Type != m.Type && e.Value != m.Value))
                    .Select(x => new ClientClaim
                    {
                        ClientId = existingClient.Id,
                        Type = x.Type,
                        Value = x.Value
                    })
                    .ToList();

                var missingScopes = clientConfig.AllowedScopes
                    .Where(x => existingClient.AllowedScopes.All(_ => _.Scope != x.Scope))
                    .Select(x => new ClientScope
                    {
                        ClientId = existingClient.Id,
                        Scope = x.Scope
                    })
                    .ToList();

                existingClient.AllowedScopes.AddRange(missingScopes);
                existingClient.Claims.AddRange(missingClaims);
                configurationContext.SaveChanges();
            }
        }
        
    }
}
