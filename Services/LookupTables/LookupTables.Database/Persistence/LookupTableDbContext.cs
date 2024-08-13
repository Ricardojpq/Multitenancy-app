using LookupTables.Database.Helpers;
using LookupTables.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Utils.Extensions;

namespace LookupTables.Database.Persistence
{
    public class LookupTableDbContext : DbContext
    {
        public readonly string tenantId = "";

        public LookupTableDbContext(IHttpContextAccessor httpContextAccessor) 
        {
            tenantId = httpContextAccessor.HttpContext!.GetTenantId() ?? "";
        }
        public LookupTableDbContext(DbContextOptions<LookupTableDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            if (httpContextAccessor.HttpContext != null)
            {
              tenantId = httpContextAccessor.HttpContext!.GetTenantId() ?? "";
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is LookupTableTenantBaseEntity))
            {
                if (string.IsNullOrEmpty(tenantId))
                {
                    throw new Exception("Tenant not found when registering");
                }

                var entity = item.Entity as LookupTableTenantBaseEntity;
                entity!.TenantId = Guid.Parse(tenantId);
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            LookupTableDbContextHelper.ApplyFilterByTenant(this, modelBuilder.Model.GetEntityTypes());
        }

        public DbSet<Gender> Gender { get; set; }
    }
}
