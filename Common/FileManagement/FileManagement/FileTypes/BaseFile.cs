using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FileManagement.DTOs;
using FileManagement.VM;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace FileManagement.FileTypes
{
    public abstract class BaseFile : IProprietaryFile
    {
        protected IEnumerable<IDictionary<string,string>> JsonTemplateDictionary;
        public IEnumerable<object> JsonTemplate { get; set; }
        public PFFormatDTO Conf { get; set; }
        public List<VM.PayerCrossReferenceDTO> _CrossReferenceValues { get; set; }
        public TypeFile TypeFile { get; set; }

        public List<object> Data { get; set; }

        public List<Dictionary<string,object>> DataMultiTable { get; set; }

        public List<string> HeaderTemplate { get; set; }

        public List<FileValidationError> errorList { get; set; }

        public abstract bool LoadFields(List<string> contentTextFile);

        public bool IsEntityMapping(string nameEntity)
        {
            var resp = false;
            foreach (var dictionary in JsonTemplateDictionary)
            {
                var Entity = dictionary.ElementAtOrDefault(3).Value;
                if(Entity == nameEntity)
                {
                    resp = true;
                    break;
                }
            }
            return resp;
        }

        public object GetInstance(string strFullyQualifiedName)
        {
            Type type = Type.GetType($"TradingPartner.Domain.{strFullyQualifiedName}");
            if (type != null)
                return Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType($"TradingPartner.Domain.{strFullyQualifiedName}");
                if (type != null)
                    return Activator.CreateInstance(type);
            }
            return null;
        }

        public Dictionary<string, object> GetInstances(string Tables)
        {
            var resp = new Dictionary<string,object>();
            foreach (string strFullyQualifiedName in Tables.Split(','))
            {
                Type type = Type.GetType($"TradingPartner.Domain.{strFullyQualifiedName}");
                if (type != null)
                {
                    resp.Add(strFullyQualifiedName, Activator.CreateInstance(type));
                }
                else
                {
                    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        type = asm.GetType($"TradingPartner.Domain.{strFullyQualifiedName}");
                        if (type != null)
                        {
                            resp.Add(strFullyQualifiedName, Activator.CreateInstance(type));
                            break;
                        }
                    }
                }
                          
            }
            return resp;
        }

        public bool SetValueDestiny(object inputValue, object objData, int row,KeyValuePair<string,PropertyInfo> property)
        {
            try
            {
                if (inputValue is string)
                {
                    if (string.IsNullOrEmpty(inputValue.ToString())) return true;
                }
                if (property.Value.PropertyType == typeof(bool) || property.Value.PropertyType == typeof(bool?))
                {
                    if (inputValue.ToString().ToLower() == "y" || inputValue.ToString().ToLower() == "t" || inputValue.ToString().ToLower() == "1" || inputValue.ToString().ToLower() == "yes" ) {
                        inputValue = "true";
                    }
                    else if (inputValue.ToString().ToLower() == "n" || inputValue.ToString().ToLower() == "f" || inputValue.ToString().ToLower() == "0" || inputValue.ToString().ToLower() == "no")
                    {
                        inputValue = "false";
                    }
                }
                if (property.Value != null)
                {                    
                    Type t = Nullable.GetUnderlyingType(property.Value.PropertyType) ?? property.Value.PropertyType;                   
                    object safeValue = (inputValue == null) ? null: Convert.ChangeType(inputValue, t);
                    property.Value.SetValue(objData, safeValue, null);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                errorList.Add(new FileValidationError { Row = row, Description = $"{property.Key} :  {ex.Message}" });
                return false;
            }
            
        }

        public void addData(Dictionary<string, object> rowData,int row)
        {
            foreach (var item in rowData)
            {
                var context = new ValidationContext(item.Value);
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(item.Value, context, results, true);
                if(!isValid)
                {
                    foreach (var r in results)
                    {
                        errorList.Add(new FileValidationError { Row = row, Description = $"{r.ErrorMessage}" });
                    }                    
                }
            }            
            DataMultiTable.Add(rowData);
        }

        public abstract string CreateJsonConfig(IFormFile File);

        public abstract string ExtractFormat(string frmt, bool useFunction, object confValue=null);

    }

   
}
