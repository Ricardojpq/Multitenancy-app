using System.Reflection;
using System.Security.Claims;

namespace Authentication.Util
{
    public static class Claims
    {
        public static readonly Claim AuthorizationProfile = new Claim(Names.Permission, Names.AngularClient.AuthorizationProfile);
        public static readonly Claim AuthorizationHttpClient = new Claim(Names.Permission, Names.AngularClient.AuthorizationHttpClient);
        public static readonly Claim AuthorizationSwagger = new Claim(Names.Permission, Names.AngularClient.AuthorizationSwagger);

        public static class Names
        {
            public const string Permission = "Permission";
            public static class AngularClient
            {
                public const string AuthorizationProfile = "Profile";
                public const string AuthorizationHttpClient = "HttpClientAuthorization";
                public const string AuthorizationSwagger = "SwaggerAuthorization";

                public static string?[] GetAll()
                {
                    return typeof(AngularClient)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(x => x.IsLiteral && x.FieldType == typeof(string))
                        .Select(x => x.GetValue(null)?.ToString())
                        .ToArray();
                }
            }
        }
    }
}
