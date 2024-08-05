using FileManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public class FuncConcat : IFunction
    {
        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int)TypeFunction.Concat,

            Description = "Concats a string on other string",
            Name = "Concat",
            Parameters = new List<ParameterDTO> {
                 new ParameterDTO{
                     Name =  "Field",
                     Description =  "Field to apply concat",
                     Order = 1,
                     IsRequired = true,
                     DataType = nameof(String)
                 },
                 new ParameterDTO{
                     Name =  "Element",
                     Description = "Element to be added",
                     Order = 2,
                     IsRequired = true,
                     DataType = nameof(String)
                 }
             },
            TypeReturn = nameof(String)
        };


        /// <summary>
        /// Return a concatenated string
        /// </summary>
        /// <param name="Args"></param>
        /// <returns>Array string</returns>
        public object CallFunction(string[] Args)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(Args[1]))
            {         
               result = string.Concat(Args.FirstOrDefault(), Args[1]);
            }
            return result;
        }
    }
}
