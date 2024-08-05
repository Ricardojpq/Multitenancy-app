using FileManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public class FuncSplit : IFunction
    {
        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int)TypeFunction.Split,

            Description = "Applies split to a field",
            Name = "Split",
            Parameters = new List<ParameterDTO> {
                 new ParameterDTO{
                     Name =  "Field",
                     Description =  "Field to apply split",
                     Order = 1,
                     IsRequired = true,
                     DataType = nameof(String)
                 },
                 new ParameterDTO{
                     Name =  "Separator",
                     Description = "Character separator",
                     Order = 2,
                     IsRequired = true,
                     DataType = nameof(String)
                 },
                 new ParameterDTO{
                     Name =  "ElementAt",
                     Description = "Element to be returned after split. Begining at 1",
                     Order = 3,
                     IsRequired = true,
                     DataType = nameof(Int16)
                 }
             },

            TypeReturn = nameof(String)
        };


        /// <summary>
        /// Return an array with separated elements
        /// </summary>
        /// <param name="Args">String to apply split function. String character separator</param>
        /// <returns>Array string</returns>
        public object CallFunction(string[] Args)
        {
            if (Args.FirstOrDefault().Contains(Args[1]))
            {
                var elements = Args.FirstOrDefault().Split(Args[1]);

                int elementAt = int.Parse(Args[2]);

                if (elements.Length >= elementAt)
                {
                    return elements[elementAt - 1];
                }

                return null;
            }
            else
            {
                return null;
            }

        }
    }
}
