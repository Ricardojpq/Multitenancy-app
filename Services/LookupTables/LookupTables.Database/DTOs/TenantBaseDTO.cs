using SharedKernel.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupTables.Database.DTOs
{
    internal class TenantBaseDTO : BaseModel
    {
        public Guid TenantId { get; set; }
    }
}
