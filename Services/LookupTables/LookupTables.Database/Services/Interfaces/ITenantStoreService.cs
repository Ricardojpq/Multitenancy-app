using LookupTables.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupTables.Database.Services.Interfaces
{
    public interface ITenantStoreService <T> where T : Tenant
    {

        Task<Tenant?> GetTenantAsync(Guid id);
    }
}
