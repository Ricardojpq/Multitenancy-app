using FileManagement.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public class FuncNumber : IFunction
    {
        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int)TypeFunction.Number,
            Description = "The function is used to convert to a number of type integer or decimal indicating which part should be taken as decimal if applicable.",
            Name = nameof(TypeFunction.Number),
            Parameters = new List<ParameterDTO> {
                 new ParameterDTO{
                     Name =  "Value",
                     Description =  " indicates the value that needs to be converted to integer or decimal format",
                     Order = 1,
                     IsRequired = true,
                     DataType = nameof(String)
                 },
                 new ParameterDTO{
                     Name =  "Decimals",
                     Description = "Indicates how many decimal places the mapping will force",
                     Order = 2,
                     IsRequired = false,
                     DataType = nameof(Int32)
                 }
             },
            TypeReturn = nameof(String)
        };

        public object CallFunction(string[] Args)
        {
            try
            {
                var isNotNumber = false;
                //Remove 0 to left
                Args[0] = Args[0].TrimStart('0');
                var decimals = 0;
                if (Args[1].ToLower() == "null")
                {
                    if (Args[0].Contains("."))
                    {
                        decimals = Args[0].Split('.')[1].Length;
                    }
                }
                else
                {
                    if(int.TryParse(Args[1], out decimals))
                    {
                        decimals = Convert.ToInt32(Args[1]);
                        if (decimals < 0) { decimals = 0; }                       
                    }
                    else
                    {
                        isNotNumber = true;
                    }                                       
                }                                
                if (Args[0].Contains("."))
                {
                    var decimalPart = Args[0].Split('.')[1];
                    if(isNotNumber) { decimals = decimalPart.Length; }
                    if(decimalPart.Length >= decimals && decimals != 0)
                    {
                        return Args[0].Split('.')[0] + '.' + decimalPart.Substring(0, decimals);
                    }
                    else if (decimals == 0)
                    {
                        return (Args[0].Split('.')[0]);
                    }
                    else
                    {                        
                        return Args[0].Split('.')[0] + '.' + decimalPart.PadRight(decimals, '0');
                    }                    
                }
                else
                {
                    if(Args[0].Length > decimals && decimals != 0)
                    {
                        return Args[0].Insert(Args[0].Length-decimals, ".");
                    }
                    else if (decimals == 0 )
                    {
                        return (Args[0]);
                    }
                    else
                    {
                        return "0."+ new String('0', decimals - Args[0].Length) + Args[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,ex.Message);
                return null;
            }
        }
    }
}
