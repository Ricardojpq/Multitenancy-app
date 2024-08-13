using LookupTables.Database.Services.Interfaces;
using LookupTables.Domain;
using Utils.Shared;
namespace LookupTables.Api.Middlewares
{
    public class TenantMiddleware<T> where T : Tenant
    {
        private readonly RequestDelegate next;

        public TenantMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Items.ContainsKey(Constants.TenantId)) 
            {
                var tenantStore = context.RequestServices.GetService(typeof(ITenantStoreService<T>)) as ITenantStoreService<T>;
                var resolutionStrategy = context.RequestServices.GetService(typeof(ITenantService)) as ITenantService;

                var tenantId = resolutionStrategy!.GetTenantId();

                if (string.IsNullOrEmpty(tenantId))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var tenant = await tenantStore!.GetTenantAsync(Guid.Parse(tenantId));

                if (tenant == null)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("TenantId is not valid");
                    return;
                }

                context.Items.Add(Constants.TenantId, tenantId);
            }

            //Continue processing
            if (next != null)
                await next(context);
        }
    }
}
