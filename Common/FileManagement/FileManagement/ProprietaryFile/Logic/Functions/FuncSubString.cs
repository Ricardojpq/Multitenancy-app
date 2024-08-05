using FileManagement.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public class FuncSubString : IFunction
    {
        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int)TypeFunction.SubString,
            Description = "The function is used to extract the characters from a string, between two specified indices, and returns the new sub string.",
            Name = nameof(TypeFunction.SubString),
            Parameters = new List<ParameterDTO> {
                 new ParameterDTO{
                     Name =  "Value",
                     Description =  "Original string",
                     Order = 1,
                     IsRequired = true,
                     DataType = nameof(String)
                 },
                 new ParameterDTO{
                     Name =  "Start",
                     Description = "The position where to start the extraction. First character is at index 0",
                     Order = 2,
                     IsRequired = true,
                     DataType = nameof(Int32)
                 },
                 new ParameterDTO{
                     Name =  "End",
                     Description = "The position (up to, but not including) where to end the extraction. If omitted, it extracts the rest of the string",
                     Order = 3,
                     IsRequired = false,
                     DataType = nameof(Int16)
                 }
             },
            TypeReturn = nameof(String)
        };

        public object CallFunction(string[] Args)
        {
            try
            {
                if (Args.Length < 2)
                {
                    return null;
                }
                var length = Args[0].Length;
                var start = CalculateValueExpresion(Args[1], length);
                if (start < 0) { start = 0; }
                if (start >= length) { return Args[0]; }
                if (Args.Length > 2)
                {
                    if (Args[2].ToLower() == "null" || string.IsNullOrEmpty(Args[2]))
                    {
                        return Args[0].Substring(start);
                    }
                    var end = CalculateValueExpresion(Args[2], length);
                    if (end > length) { return Args[0]; }
                    if (end < 0) { end = 0; }
                    if (start > end) // swap
                    {
                        var aux = start;
                        start = end;
                        end = aux;
                    }
                    return Args[0].Substring(start, end - start);
                }
                return Args[0].Substring(start);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return null;
            }
        }

        private int CalculateValueExpresion(string AritmeticExpresion, int length)
        {
            try
            {
                DataTable dt = new DataTable();
                AritmeticExpresion = AritmeticExpresion.Replace("length", length.ToString());
                int answer = (int)dt.Compute(AritmeticExpresion, "");
                return answer;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return 0;
            }
        }
    }
}
