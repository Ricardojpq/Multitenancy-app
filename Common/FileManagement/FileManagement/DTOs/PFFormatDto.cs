using Utils.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileManagement.DTOs
{
    public enum TypeFile
    {
        Delimited,
        Fixed
    }

    public enum StatusFormat
    {
        init = 0,
        step_1,
        step_2,
        step_3
    }

    public enum OriginTypeFormat
    {
        init = 0,
        enter_fields,
        import_file,
        clone_from_profile
    }

    public class PFFormatDTO : DateRangeBaseDTO
    {
        public int PFProfileId { get; set; }
        public int PayerId { get; set; }
        public int FileFormatType { get; set; }
        public Dictionary<string, string> ConfigParameters { get; set; }

        [CustomRequired]
        public int FormatFileTypeId { get; set; } //LUT
        public string FormatFileTypeName { get; set; }
        public string FormatFileTypeDescription { get; set; }
        public string FileFormatTypeString
        {
            get
            {

                return Enum.GetName(typeof(TypeFile), FileFormatType);
            }
        }
        public string Description { get; set; }
        public int HeadersRowsToSkip { get; set; }
        public string JsonConfiguration { get; set; }
        public char FieldSeparator { get; set; }
        public string TargetEntity { get; set; }
        public int Status { get; set; }
        public string StatusString
        {
            get
            {

                return Enum.GetName(typeof(StatusFormat), Status);
            }
        }
        public int? OriginType { get; set; }

        public List<PFImportFileHistoryDTO> PFImportFileHistory { get; set; }
        public List<FunctionDTO> Functions { get; set; }
        public List<string> HeaderTemplate { get; set; }

        public List<PropertyDescriptorDTO> TargetEntityFields { get; set; } // Obsolete when end Pf new Mapping Multiple tables

        public List<EntityDescriptorDTO> TargetEntitiesFields { get; set; }


    }
}
