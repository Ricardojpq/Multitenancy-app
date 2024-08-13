using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LookupTables.Domain
{
    public class Tenant :LookupTableCommonBaseEntity
    {
        public string? Identifier { get; set; }
    }
}
