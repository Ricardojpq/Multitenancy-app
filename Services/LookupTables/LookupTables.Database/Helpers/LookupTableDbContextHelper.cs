using LookupTables.Database.Persistence;
using LookupTables.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;
using Utils.Extensions;

namespace LookupTables.Database.Helpers
{
    public static class LookupTableDbContextHelper
    {


        public static void ApplyFilterByTenant(LookupTableDbContext context, IEnumerable<IMutableEntityType> entities)
        {
            foreach (var entity in entities)
            {
                var type = entity.ClrType;
                if (typeof(LookupTableTenantBaseEntity).IsAssignableFrom(type))
                {
                    var method = typeof(LookupTableDbContextHelper).
                        GetMethod(nameof(BuildFilterForTenant), BindingFlags.NonPublic | BindingFlags.Static)?
                        .MakeGenericMethod(type);

                    var filter = method?.Invoke(null, new object[] { context })!;
                    entity.SetQueryFilter((LambdaExpression)filter);
                    entity.AddIndex(entity.FindProperty(nameof(LookupTableTenantBaseEntity.TenantId))!);
                }
                else if (type.SkipValidation(AssingTypesToSkipValidation()))
                {
                    continue;
                }
                else
                {
                    throw new Exception($"The entity {entity} has not been marked as Tenant or Common");
                }
            }
        }

        private static LambdaExpression BuildFilterForTenant<TEntity>(LookupTableDbContext context) where TEntity : LookupTableTenantBaseEntity
        {
            Expression<Func<TEntity, bool>> filter = (x) => x.TenantId == Guid.Parse(context.tenantId);
            return filter;
        }

        private static List<Type> AssingTypesToSkipValidation()
        {
            return new List<Type>()
            {
                typeof(LookupTableCommonBaseEntity)
            };
        }
    }
}
