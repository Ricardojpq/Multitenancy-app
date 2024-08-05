using FileManagement.DTOs;
using System;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public class FuncCurrentDate : IFunction
    {
        public FunctionDTO Description => new FunctionDTO
        {
            Id = (int)TypeFunction.CurrentDate,
            Description = "Function that returns the current date",
            Name = "CurrentDate",
            Parameters = null,
            TypeReturn = nameof(DateTime)
        };

        public object CallFunction(string[] Args)
        {
            return (DateTime.Now);
        }
    }
}
