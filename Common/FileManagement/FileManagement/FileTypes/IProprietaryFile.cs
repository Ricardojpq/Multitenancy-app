using System.Collections.Generic;
using FileManagement.DTOs;
using Microsoft.AspNetCore.Http;

namespace FileManagement.FileTypes
{
    public interface IProprietaryFile
    {
         PFFormatDTO Conf { get; set; }
         TypeFile TypeFile { get; set; }
         List<object> Data { get; set; }
         List<Dictionary<string, object>> DataMultiTable { get; set; }
         List<string> HeaderTemplate { get; set; }
         bool LoadFields(List<string> contentTextFile);
         bool IsEntityMapping(string nameEntity);
         string CreateJsonConfig(IFormFile File);
         List<FileValidationError> errorList { get; set; }
         string ExtractFormat(string frmt, bool useFunction, object confValue = null);         
    }

    public class Record
    {
        public IEnumerable<Field> Fields { get; set; }
    }

    public class Field
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
