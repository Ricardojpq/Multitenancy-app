using System.Reflection;

namespace Authentication.Util
{
    public static class Roles
    {
        // Important!: Use the same pattern to add more roles so the GetAll methods doesnt brake
        // pattern: public const string RoleName = "Role Name";
        public class AngularClient
        {
            public const string SysAdmin = "Sys Admin";
            public const string Administrator = "Administrator";
            public const string CommonUser = "CommonUser";
            public static string[] GetAll() => Roles.GetAll(typeof(AngularClient))!;
        };

        public static string?[] GetAll(System.Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.IsLiteral && x.FieldType == typeof(string))
                .Select(x => x.GetValue(null)?.ToString())
                .ToArray();
        }
    }
}
