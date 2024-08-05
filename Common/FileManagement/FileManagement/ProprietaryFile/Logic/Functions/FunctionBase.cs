using System;
using System.Collections.Generic;
using FileManagement.DTOs;
using FileManagement.VM;
using Serilog;

namespace FileManagement.ProprietaryFile.Logic.Functions
{
    public  class FunctionBase
    {
        Dictionary<string, string> ConfigParameters { get; set; }        
        public FunctionBase(Dictionary<string, string> Conf = null, List<VM.PayerCrossReferenceDTO> CrossReferenceValues = null)
        {
            if(Conf != null)
            {
                ConfigParameters = Conf;
                functionFindCrossReference = new FuncFindCrossReference(ConfigParameters, CrossReferenceValues);
            }                       
        }

        IFunction functionAge = new FuncAge();
        IFunction functionYear = new GetYear();
        IFunction functionDay = new GetDay();        
        IFunction functionCurrentDate = new FuncCurrentDate();
        IFunction functionDateFormat = new FuncDateTimeFormat();
        IFunction functionConcat = new FuncConcat();
        IFunction functionSplit = new FuncSplit();
        IFunction functionInsert = new FuncInsert();
        IFunction functionReplace = new FuncReplace();
        IFunction functionSubString = new FuncSubString();
        IFunction functionNumber= new FuncNumber();
        IFunction functionFindCrossReference;

        public object CallFunction(TypeFunction f , string[]  Args)
        {
            var resp = new object();            
            switch (f)
            {
                case TypeFunction.Age:
                     resp = functionAge.CallFunction(Args);
                     break;
                case TypeFunction.GetYear:
                    resp = functionYear.CallFunction(Args);
                    break;
                case TypeFunction.GetDay:
                    resp = functionDay.CallFunction(Args);
                    break;
                case TypeFunction.CurrentDate:
                    resp = functionCurrentDate.CallFunction(Args);
                    break;
                case TypeFunction.DateTimeFormat:
                    resp = functionDateFormat.CallFunction(Args);
                    break;
                case TypeFunction.Concat:
                    resp = functionConcat.CallFunction(Args);
                    break;   
                case TypeFunction.Split:
                    resp = functionSplit.CallFunction(Args);
                    break;
                case TypeFunction.FindCrossReference:
                    resp = functionFindCrossReference.CallFunction(Args);
                    break;
                case TypeFunction.Insert:
                    resp = functionInsert.CallFunction(Args);
                    break;
                case TypeFunction.Replace:
                    resp = functionReplace.CallFunction(Args);
                    break;
                case TypeFunction.SubString:
                    resp = functionSubString.CallFunction(Args);
                    break;
                case TypeFunction.Number:
                    resp = functionNumber.CallFunction(Args);
                    break;
            }
            return resp;
        }

        public FunctionDTO DescriptionFunction(TypeFunction f)
        {
            var resp = new FunctionDTO();
            switch (f)
            {
                case TypeFunction.Age:
                    resp = functionAge.Description;
                    break;
                case TypeFunction.GetYear:
                    resp = functionYear.Description;
                    break;
                case TypeFunction.GetDay:
                    resp = functionDay.Description;
                    break;
                case TypeFunction.CurrentDate:
                    resp = functionCurrentDate.Description;
                    break;
                case TypeFunction.DateTimeFormat:
                    resp = functionDateFormat.Description;
                    break;
                case TypeFunction.Concat:
                    resp = functionConcat.Description;
                    break;
                case TypeFunction.Split:
                    resp = functionSplit.Description;
                    break;
                case TypeFunction.FindCrossReference:
                    resp = functionFindCrossReference.Description;
                    break;
                case TypeFunction.Insert:
                    resp = functionInsert.Description;
                    break;
                case TypeFunction.Replace:
                    resp = functionReplace.Description;
                    break;
                case TypeFunction.SubString:
                    resp = functionSubString.Description;
                    break;
                case TypeFunction.Number:
                    resp = functionNumber.Description;
                    break;
            }

            return resp;
        }

        public TypeFunction GetFunction(string expression)
        {            
            int pFrom = expression.IndexOf("$")+1;
            int pTo = expression.IndexOf("(");
            String functionName = expression.Substring(pFrom, pTo - pFrom);            
            return (TypeFunction)Enum.Parse(typeof(TypeFunction), functionName);
        }

        public bool CheckFunctionName(string functionName)
        {
            try
            {
                Enum.Parse(typeof(TypeFunction), functionName);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return false;              
            }
        }
    }
}
