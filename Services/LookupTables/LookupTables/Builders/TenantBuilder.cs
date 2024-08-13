using LookupTables.Database.Services.Interfaces;
using LookupTables.Domain;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LookupTables.Api.Builders
{
    public class TenantBuilder<T>  where  T : Tenant
    {
        private readonly IServiceCollection _services;

        public TenantBuilder(IServiceCollection services)
        {
            _services = services;
        }

        /// <summary>
        /// Register the implementation of Tenant Resolution
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public TenantBuilder<T> WithResolutionStrategy<V>(ServiceLifetime lifetime = ServiceLifetime.Transient)
            where V : class, ITenantService
        {
            _services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            _services.Add(ServiceDescriptor.Describe(typeof(ITenantService), typeof(V), lifetime));
            return this;
        }

        /// <summary>
        /// Register Tenant Repository implementation
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public TenantBuilder<T> WithStore<V>(ServiceLifetime lifetime = ServiceLifetime.Transient)
            where V : class, ITenantStoreService<T>
        {
            _services.Add(ServiceDescriptor.Describe(typeof(ITenantStoreService<T>), typeof(V), lifetime));
            return this;
        }
    }
}
