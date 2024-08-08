using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace Authentication.Configuration
{
    public static partial class Config
    {
        private static class ApiResources
        {
            internal const string ApiLookupTables = "APILookupTables";
            internal const string ApiAuth = "APIAuth";
        }

        public static IEnumerable<ApiResource> Apis =>
            new[]
            {
                new ApiResource(ApiResources.ApiAuth, "Auth API"),
                new ApiResource(ApiResources.ApiLookupTables, "API LookupTables"),
            };

        public static IEnumerable<ApiScope> ApisScopes =>
           new[]
           {
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
                new ApiScope(ApiResources.ApiAuth, "Auth API"),
                new ApiScope(ApiResources.ApiLookupTables,"API LookupTables"),
           };
    }
}
