using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FileManagement.DTOs
{
    public class EntityDescriptorDTO
    {
        [MaxLength(80)]
        public string Name { get; set; }
        public List<PropertyDescriptorDTO> Properties { get; set; }
    }

    public class PropertyDescriptorDTO
    {
        [MaxLength(80)]
        public string Name { get; set; }
        [MaxLength(80)]
        public string Type { get; set; }
        public bool IsRequired { get; set; }
    }
}
