using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels.LookupTables
{
    public class LookupTableDto
    {
        [MaxLength(256)]
        public string ProviderSpecialtyTaxonomyDescription { get; set; }
        [MaxLength(80)]
        public string ProviderSpecialtyTaxonomyName { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        [MaxLength(80)]
        public string Name { get; set; }
        [MaxLength(80)]
        public string Code { get; set; }
        public int Id { get; set; }
    }
}
