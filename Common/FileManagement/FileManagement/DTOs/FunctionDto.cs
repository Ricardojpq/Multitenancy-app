using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FileManagement.DTOs
{
    public class FunctionDTO
    {
        public int Id { get; set; }
        [MaxLength(80)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        public string TypeReturn { get; set; }
        public List<ParameterDTO> Parameters { get; set; }
    }

    public class ParameterDTO
    {
        [MaxLength(80)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsRequired { get; set; }
        public string DataType { get; set; }
    }
}
