using FileManagement.DTOs;
using System.Collections.Generic;

namespace FileManagement.ProprietaryFile.Logic.Functions
{   
    public interface IFunction
    {
        FunctionDTO Description { get; }
        object CallFunction(string[] Args);
    }

    public enum TypeFunction
    {
        Age,
        GetYear,
        GetDay,        
        CurrentDate,
        DateTimeFormat,
        Concat,
        Split,
        FindCrossReference,
        Insert,
        Replace,
        SubString,
        Number
    }
}
