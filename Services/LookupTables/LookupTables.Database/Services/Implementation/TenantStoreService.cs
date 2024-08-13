using LookupTables.Database.Persistence;
using LookupTables.Database.Services.Interfaces;
using LookupTables.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupTables.Database.Services.Implementation
{
    public class TenantStoreService : ITenantStoreService<Tenant>
    {
        private readonly TenantDbContext _context;
        private readonly IMemoryCache _cache;

        public TenantStoreService(TenantDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Tenant?> GetTenantAsync(Guid id)
        {
            var tenant = new Tenant();
            try
            {
                var cacheKey = $"Tenant_{id}";
                tenant = _cache.Get<Tenant>(cacheKey);

                if (tenant != null)
                    return tenant;

                var entity = await _context.Tenants.FirstOrDefaultAsync(x => x._Id == id)
                                ?? throw new ArgumentException($"Tenant \"{id}\" not found");

                tenant = entity;
                _cache.Set(cacheKey, tenant);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return tenant;
        }
    }
}
