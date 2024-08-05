using FileManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public class FuncDateTimeFormat : IFunction
    {
        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int)TypeFunction.DateTimeFormat,
            Description = "Function to format Date",
            Name = nameof(TypeFunction.DateTimeFormat),
            Parameters = new List<ParameterDTO> {
                 new ParameterDTO{
                     Name =  "input",
                     Description =  "Value",
                     Order = 1,
                     IsRequired = true,
                     DataType = nameof(String)
                 },
                 new ParameterDTO{
                     Name =  "Format Date",
                     Description =  "dd for day, MM for Month, yyyy for Year",
                     Order = 2,
                     IsRequired = true,
                     DataType = nameof(String)
                 }
             },
            TypeReturn = nameof(DateTime)
        };
        public object CallFunction(string[] Args)
        {
            DateTime date;
            if (DateTime.TryParseExact(Args[0].Trim(), Args[1].Trim(),
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out date))
            {
                return date;
            }
            else
            {
                return null;
            }
        }
    }
}
