using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.LookupTables
{
    public class LookupTableFileVM
    {
        public int Id { get; set; }
        public bool ToUpdate { get; set; }
        public bool ToExpire { get; set; }
        [CustomRequired]
        [MaxLength(80)]
        public string MaintenanceUser { get; set; }
        public IFormFile File { get; set; }
    }

    public class LookupTableBase64FileResponseVM
    {
        public string base64 { get; set; }
        public string fileName { get; set; }
    }
}
