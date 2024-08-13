using SharedKernel.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupTables.Database.DTOs
{
    public class TenantDTO : BaseModel
    {
        public string Identifier { get; }
        public TenantDTO(Guid id, string identifier)
        {
            Id = id;
            Identifier = identifier;
        }
    }
}
