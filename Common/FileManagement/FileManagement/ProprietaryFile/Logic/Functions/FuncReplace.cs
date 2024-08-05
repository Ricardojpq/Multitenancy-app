using FileManagement.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public class FuncReplace : IFunction
    {
        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int)TypeFunction.Replace,
            Description = "The function is used to replace one character with another.",
            Name = nameof(TypeFunction.Replace),
            Parameters = new List<ParameterDTO> {
                new ParameterDTO{
                     Name =  "Value",
                     Description =  "Original string",
                     Order = 1,
                     IsRequired = true,
                     DataType = nameof(String)
                 },
                 new ParameterDTO{
                     Name =  "SearchValue",
                     Description =  "The value, or regular expression, that will be replaced by the new value",
                     Order = 2,
                     IsRequired = true,
                     DataType = nameof(String)
                 },
                 new ParameterDTO{
                     Name =  "NewValue",
                     Description = "The value to replace searchValue. In the case that null comes, it is considered as an empty string",
                     Order = 3,
                     IsRequired = false,
                     DataType = nameof(String)
                 }                 
             },
            TypeReturn = nameof(String)
        };

        public object CallFunction(string[] Args)
        {
            try
            {
                if (Args.Count()<2)
                {
                    Log.Information($"Bad configuration for Replace Function {String.Join(',', Args)}");
                    return null;
                }
                if (Args[2].ToLower() == "null")
                {
                    Args[2] = string.Empty;
                }
                return Args[0].Replace(Args[1], Args[2]);                
            }
            catch (Exception ex)
            {
                Log.Error(ex,ex.Message);
                return null;
            }
        }
    }
}
