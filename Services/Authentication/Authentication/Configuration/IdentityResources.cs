

using Duende.IdentityServer.Models;

namespace Authentication.Configuration
{
    public static partial class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                // --- Custom identity resources
            };
    }
}
