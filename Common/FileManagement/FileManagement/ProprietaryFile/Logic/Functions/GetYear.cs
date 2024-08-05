using FileManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public class GetYear : IFunction
    {
        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int)TypeFunction.GetYear,
            Description = "Function to extract the Year according to the date",
            Name = nameof(TypeFunction.GetYear),
            Parameters = new List<ParameterDTO> {
                 new ParameterDTO{
                     Name = "Date",
                     Description = "Date in yyyyMMdd or  MMddyyyy format",
                     Order = 1,
                     IsRequired = true,
                     DataType = nameof(String)
                 }
             },
            TypeReturn = nameof(Int32)
        };

        public object CallFunction(string[] Args)
        {
            DateTime dateOfBirth;

            if (DateTime.TryParseExact(Args.FirstOrDefault(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth) ||
                DateTime.TryParseExact(Args.FirstOrDefault(), "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
            {
                return dateOfBirth.Date.Year;
            }
            else
            {
                return null;
            }
        }
    }
}
