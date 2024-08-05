using FileManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public class FuncAge : IFunction
    {
        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int) TypeFunction.Age,
            Description = "Function to calculate the age according to the date of birth",
            Name = "Age",
            Parameters = new List<ParameterDTO> {
                 new ParameterDTO{
                     Name =  "DoB",
                     Description =  "Date of Birth in yyyyMMdd or  MMddyyyy Format",
                     Order = 1,
                     IsRequired = true,
                     DataType = nameof(String)
                 }
             },
            TypeReturn = nameof(DateTime)
        };

        /// <summary>
        /// Return Age in funtion of day of Birth
        /// </summary>
        /// <param name="Args">DoB in yyyyMMdd or  MMddyyyy</param>
        /// <returns>Int - Age in years </returns>
        public object CallFunction(string[] Args)
        {
            DateTime dateOfBirth;

            if (DateTime.TryParseExact(Args.FirstOrDefault(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth) ||
                DateTime.TryParseExact(Args.FirstOrDefault(), "MMddyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
            {

                return (DateTime.Now.Year - dateOfBirth.Date.Year);
            }
            else
            {
                return null;
            }
        }

    }
}
