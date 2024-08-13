using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookupTables.Domain
{
    public class LookupTableTenantBaseEntity : BaseEntity
    {
        [Required]
        [BsonRepresentation(BsonType.String)]
        public Guid TenantId { get; set; }

        public override string ToString()
        {
            return $"Entity Info: PK: {_Id}, TenantId:{TenantId}, Active: {IsActive}, Created: {CreatedDate}";
        }
    }
}
