
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using FileManagement.VM;
using FileManagement.DTOs;
using Serilog;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
   
    public class FuncFindCrossReference : IFunction
    {
        Dictionary<string, string> ConfigParameters { get; set; }
        List<VM.PayerCrossReferenceDTO> _CrossReferenceValues { get; set; }
        public FuncFindCrossReference(Dictionary<string, string> Conf, List<VM.PayerCrossReferenceDTO> CrossReferenceValues)
        {
             ConfigParameters = Conf;
            _CrossReferenceValues = CrossReferenceValues;
        }

        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int)TypeFunction.FindCrossReference,

            Description = "Find Cross Reference.",
            Name = "FindCrossReference",
            Parameters = new List<ParameterDTO> {                 
                 new ParameterDTO{
                     Name =  "PayerCrossReferenceInfo",
                     Description = "Value to match Corss reference Value",
                     Order = 2,
                     IsRequired = true,
                     DataType = nameof(String)
                 },
                 new ParameterDTO{
                     Name =  "PayerCrossReferenceTypeName",
                     Description =  "Reference Type to find Value",
                     Order = 1,
                     IsRequired = true,
                     DataType = "SelectLUT,PayerCrossReferenceType"
                 },
             },
            TypeReturn = nameof(String)
        };

        public object CallFunction(string[] Args)
        {
            if (Args.Length >= 2)
            {
                var PayerCrossReferenceType = Args[1];
                var PayerCrossReferenceInfo = Args[0];
                try
                {
                    return _CrossReferenceValues.FirstOrDefault(c => c.PayerCrossReferenceTypeName == Args[1] && c.PayerCrossReferenceInfo == Args[0])?.PayerCrossReferenceValue;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, ex.Message);
                    return null;
                }             
            }
            else
            {
                return null;
            }            
        }
    }
}
