using FileManagement.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public class FuncInsert : IFunction
    {
        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int)TypeFunction.Insert,
            Description = "The function is used to insert characters into the string.",
            Name = nameof(TypeFunction.Insert),
            Parameters = new List<ParameterDTO> {
                 new ParameterDTO{
                     Name =  "Value",
                     Description =  "Original string",
                     Order = 1,
                     IsRequired = true,
                     DataType = nameof(String)
                 },
                 new ParameterDTO{
                     Name =  "Position",
                     Description = "The position where to insert. First character is at index 0",
                     Order = 2,
                     IsRequired = true,
                     DataType = nameof(Int32)
                 },
                 new ParameterDTO{
                     Name =  "New Value",
                     Description = "The value to insert",
                     Order = 3,
                     IsRequired = true,
                     DataType = nameof(String)
                 }
             },
            TypeReturn = nameof(String)
        };

        public object CallFunction(string[] Args)
        {
            try
            {
                if (Args.Count()<3)
                {
                    Log.Information($"Bad configuration for Insert Function {String.Join(',', Args)}");
                    return null;
                }
                var length = Args[0].Length;
                var position = Convert.ToInt16(Args[1]);
                var valueToInsert = Args[2];

                if(position <= 0)
                {
                    return Args[0].Insert(0, valueToInsert);
                }
                else if (position > length)
                {
                    return Args[0].Insert(length, valueToInsert);
                }
                else
                {
                    return Args[0].Insert(position, valueToInsert);
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
