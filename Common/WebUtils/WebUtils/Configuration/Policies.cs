namespace WebUtils.Configuration
{
    public static class Policies
    {
        public class Authorization
        {
            public const string AuthorizationsPolicy = "AuthorizationsPolicy";
            public const string AuthorizationViewAuthorizationPolicy= "AuthorizationViewAuthorizationPolicy";
            public const string AuthorizationEditAuthorizationPolicy = "AuthorizationEditAuthorizationPolicy";

            public static string[] GetPolicies()
            {
                return new[]
                {
                    AuthorizationViewAuthorizationPolicy, AuthorizationEditAuthorizationPolicy
                };
            }
        }
    }
}
