using System;
using System.Collections.Generic;
using System.Text;

namespace FileManagement.VM
{
    public class PayerCrossReferenceTypeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string MaintenanceUser { get; set; }
    }
   
}
