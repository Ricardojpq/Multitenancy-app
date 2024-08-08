
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using static Authentication.Util.Claims;

namespace Authentication.Configuration
{
    public static partial class Config
    { 
        public static IEnumerable<Client> Clients(IConfiguration config)
        {
            var AngularSecret = config["Identity:AngularClient:Secret"];
            return new[]
            {
                #region Angular  client

                new Client
                {
                    ClientId = Configuration.Clients.AngularClient,
                    ClientName = Configuration.Clients.AngularClient,
                    ClientSecrets =
                    {
                        new Secret(AngularSecret.Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = { "http://localhost:4200/auth/signIn" },
                    PostLogoutRedirectUris = { "http://localhost:4200/auth/signOut" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AllowedScopes =
                    {
                        ApiResources.ApiAuth,
                        ApiResources.ApiLookupTables,
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                    },
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 86400, // 1 day
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 43200, // 12 hours
                    IdentityTokenLifetime = 300,
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                },

                #endregion

                #region Swagger Client

                new Client
                {
                    ClientId = "swaggerUI",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        ApiResources.ApiAuth,
                        ApiResources.ApiLookupTables,
                    },
                    AuthorizationCodeLifetime = 3600,
                    AccessTokenLifetime = 3600,
                    IdentityTokenLifetime = 3600,
                    ClientClaimsPrefix = string.Empty,
                    Claims =
                    {
                        new ClientClaim(Names.Permission, Names.AngularClient.AuthorizationSwagger)
                    }
                },

                #endregion

            };
        }
    }
}