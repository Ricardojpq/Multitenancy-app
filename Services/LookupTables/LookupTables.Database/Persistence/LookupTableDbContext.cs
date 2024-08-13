using LookupTables.Database.Helpers;
using LookupTables.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Utils.Extensions;

namespace LookupTables.Database.Persistence
{
    public class LookupTableDbContext : DbContext
    {
        public readonly IHttpContextAccessor _httpContextAccessor;

        public LookupTableDbContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public LookupTableDbContext(DbContextOptions<LookupTableDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            string tenantId = "";
            if (_httpContextAccessor.HttpContext != null)
            {
                tenantId = _httpContextAccessor.HttpContext.GetTenantId()!;
            }

            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is LookupTableTenantBaseEntity))
            {
                var entity = item.Entity as LookupTableTenantBaseEntity;
                if (!string.IsNullOrEmpty(tenantId))
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
