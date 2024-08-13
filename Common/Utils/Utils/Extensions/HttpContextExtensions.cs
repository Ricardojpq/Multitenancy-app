using Microsoft.AspNetCore.Http;
using Utils.Shared;

namespace Utils.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Get Current TenantId
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string? GetTenantId(this HttpContext context)
        {
            if (!context.Items.ContainsKey(Constants.TenantId))
                return null;

            return context.Items[Constants.TenantId]!.ToString();
        }
    }
}
