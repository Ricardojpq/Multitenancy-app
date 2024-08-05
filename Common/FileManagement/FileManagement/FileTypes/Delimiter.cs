using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using FileManagement.ProprietaryFile.Logic.Functions;
using Microsoft.AspNetCore.Http;
using FileManagement.DTOs;
using Serilog;
using System.Globalization;
using System.Text.Json;

namespace FileManagement.FileTypes
{
    public class Delimiter : BaseFile
    {
        public char Separator { get; set; }

        public Delimiter(PFFormatDTO Conf, List<VM.PayerCrossReferenceDTO> CrossReferenceValues)
        {
            this.Conf = Conf;
            if (Conf != null)
            {
                Separator = Conf.FieldSeparator;
                Data = new List<object>();
                DataMultiTable = new List<Dictionary<string, object>>();
                JsonTemplate = string.IsNullOrEmpty(Conf.JsonConfiguration) ? new List<object>() :
                               JsonSerializer.Deserialize<IEnumerable<object>>(Conf.JsonConfiguration);
            }
            if (CrossReferenceValues != null)
            {
                _CrossReferenceValues = CrossReferenceValues;
            }
        }

        public override bool LoadFields(List<string> Lines)
        {
            errorList = new List<FileValidationError>();
            int iterator = 0;
            try
            {
                var functionB = new FunctionBase(Conf.ConfigParameters, _CrossReferenceValues);
                for (int i = 0; i < Lines.Count(); i++)
                {
                    var RowData = GetInstances(Conf.TargetEntity);
                    iterator = i;
                    foreach (var item in RowData)
                    {
                        var objData = item.Value;//GetInstance(Conf.TargetEntity);
                        var properties = objData.GetType().GetProperties().ToDictionary(p => p.Name, p => p);
                        var propertyJsonData = properties.FirstOrDefault(prop => prop.Key == "JsonData");
                        propertyJsonData.Value.SetValue(objData, JsonSerializer.Serialize(Lines[i].Split(Separator)));
                        var propertyId = properties.FirstOrDefault(prop => prop.Key == "PFFormatId");
                        propertyId.Value.SetValue(objData, Conf.Id);
                    }

                    foreach (var field in JsonTemplate)
                    {
                        var json = JsonSerializer.Serialize(field);
                        var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                        var Name = dictionary.First().Key;
                        var Expression = dictionary.First().Value;
                        var UseFunction = Convert.ToBoolean(dictionary.ElementAt(1).Value);
                        var DefaultValue = dictionary.ElementAtOrDefault(2).Value;
                        var Entity = dictionary.ElementAtOrDefault(3).Value;
                        var properties = RowData[Entity].GetType().GetProperties().ToDictionary(p => p.Name, p => p);
                        var property = properties.FirstOrDefault(prop => prop.Key.ToLower() == Name.ToLower());
                        var inputValue = new object();
                        var parameterConf = new DelimiterConfg { LineValue = string.Join(Separator, Lines[i]), Separator = Separator, RowLine = i + 1 };
                        if (!UseFunction)
                        {
                            var resultExp = ExtractFormat(Expression, false, parameterConf);
                            inputValue = string.IsNullOrEmpty(resultExp) ? DefaultValue : resultExp;
                        }
                        else
                        {
                            var f = functionB.GetFunction(Expression);
                            var args = ConvertParameters(Expression, parameterConf);
                            inputValue = functionB.CallFunction(f, args) ?? (!string.IsNullOrEmpty(DefaultValue) ? DefaultValue : "");
                        }
                        SetValueDestiny(inputValue, RowData[Entity], i + 1, property);
                    }
                    addData(RowData, i + 1);


                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                errorList.Add(new FileValidationError
                {
                    Row = iterator,
                    Description = ex.Message
                });
                return false;
            }
        }

        public string[] ConvertParameters(string frmt, DelimiterConfg delimiterConfg = null)
        {
            int pFrom = frmt.IndexOf("(") + 1;
            int pTo = frmt.LastIndexOf(")");
            String paramaters = frmt.Substring(pFrom, pTo - pFrom);
            paramaters = paramaters.Replace(",,,",",Aux2452,");
            var processParam = paramaters.Split(',');
            for (int i = 0; i < processParam.Count(); i++)
            {
                processParam[i] = processParam[i].Replace("Aux2452", ",");
                processParam[i] = ExtractFormat(processParam[i],false,delimiterConfg);
            }
            return processParam;
        }

        public override string CreateJsonConfig(IFormFile File)
        {
            errorList = new List<FileValidationError>();
            int i = 1;
            try
            {
                List<dynamic> dynamicEntities = new List<dynamic>();
                DataTable dataTable = File.ToDataTable();
                dynamicEntities = dataTable.ToDinamycEntities(typeof(TemplateDelimiter));
                var Json = $"[";
                foreach (var row in dynamicEntities)
                {
                    if (string.IsNullOrEmpty(row.Positions) && string.IsNullOrEmpty(row.Function)) { i++; }
                    else
                    {
                        Json += $"{{\"{row.Field}\":\"{BuildExpression(row.Function, row.Positions, ++i)}\",";
                        if (string.IsNullOrEmpty(row.Function))
                        {
                            Json += "\"UseFunction\":false,";
                        }
                        else
                        {
                            Json += "\"UseFunction\":true,";
                        }
                        Json += $"\"Entity\":\"{Convert.ToString(row.Entity)}\",";
                        Json += $"\"DefaultValue\":\"{Convert.ToString(row.DefaultValue)}\"}},";
                    }

                }
                Json = Json.TrimEnd(',');
                return Json + "]";
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                errorList.Add(new FileValidationError
                {
                    Row = i,
                    Description = ex.Message
                });
                return string.Empty;
            }
        }

        public string BuildExpression(string nameFunction, string pos, int row)
        {
            try
            {
                if (string.IsNullOrEmpty(nameFunction))
                {
                    return BuildPosition(pos,"");
                }
                else
                {
                    var functionB = new FunctionBase(Conf.ConfigParameters, _CrossReferenceValues);
                    if (functionB.CheckFunctionName(nameFunction))
                    {
                        var f = (TypeFunction)Enum.Parse(typeof(TypeFunction), nameFunction);
                        var fdes = functionB.DescriptionFunction(f);
                        if (fdes.Parameters == null)
                        {
                            return $"${nameFunction}()";
                        }
                        pos = pos.Replace(",,,",",lasd1213,");
                        var NumParam = pos.Split(',').Count();
                        var ParamReq = fdes.Parameters.Where(p => p.IsRequired == true).Count();
                        if (NumParam >= ParamReq)
                        {
                            var expression = $"${nameFunction}(";
                            int orderParam = 0;
                            foreach (var item in pos.Split(','))
                            {
                                var parameterValue = item.Replace("lasd1213",",");
                                if (f == TypeFunction.FindCrossReference && orderParam == 1)
                                {
                                    var CrossReferenceTypes = JsonSerializer.Deserialize<List<VM.PayerCrossReferenceTypeDTO>>(Conf.ConfigParameters["CrossReferenceTypes"]);
                                    var ParameterCamelCase = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item);//Char.ToUpperInvariant(parameterValue[0]) + parameterValue.Substring(1).ToLower();
                                    if (CrossReferenceTypes.Any(c => c.Name.ToLower() == item.ToLower()))
                                    {
                                        expression += BuildPosition(ParameterCamelCase, fdes.Parameters[orderParam].DataType) + ",";
                                    }
                                    else
                                    {
                                        errorList.Add(new FileValidationError
                                        {
                                            Row = row,
                                            Description = $"CrossReferenceType Invalid! Posibles values are {string.Join(",", CrossReferenceTypes.Select(c => c.Name))}"
                                        });
                                    }

                                }
                                else
                                {
                                    expression += BuildPosition(parameterValue, fdes.Parameters[orderParam].DataType) + ",";
                                }
                                orderParam++;
                            }
                            expression = expression.TrimEnd(',');
                            return expression + ")";
                        }
                        else
                        {
                            throw new Exception($"Number parameter Requiered for this function is {ParamReq}");
                        }
                    }
                    else
                    {
                        throw new Exception($"Function = {nameFunction} not exist!!");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                errorList.Add(new FileValidationError
                {
                    Row = row,
                    Description = ex.Message
                });
                return string.Empty;
            }
        }

        public string BuildPosition(string pos,string dataType)
        {
            int position = 0;
            if (int.TryParse(pos, out position) && dataType != nameof(Int16))
            {
                return $"#F[{position}]";
            }
            else
            {
                return pos;
                // throw new Exception($"Position must be a Number {pos}");
            }
        }

        public override string ExtractFormat(string frmt, bool useFunction, object confValue = null)
        {
            if (!useFunction)
            {
                var resp = Regex.Replace(frmt, @"#F[[]\d+]", match =>
                {
                    if (confValue == null)
                    {
                        return match.Value.Substring(3).TrimEnd(']');
                    }
                    else
                    {
                        return ProccessExpression(match.Value, (DelimiterConfg)confValue);
                    }
                });
                return resp;
            }
            else
            {
                return string.Join(',', ConvertParameters(frmt, (DelimiterConfg)confValue));
            }

        }

        public string ProccessExpression(string matchValue, DelimiterConfg dc)
        {

            var pos = Convert.ToInt16(matchValue.Substring(3).TrimEnd(']'));
            if (pos > dc.LineValue.Split(dc.Separator).Count())
            {
                errorList.Add(new FileValidationError
                {
                    Row = dc.RowLine,
                    Description = $"Position Out of Range!!"
                });
            }
            return dc.LineValue.Split(dc.Separator)[pos - 1];

        }

        public class DelimiterConfg
        {
            public string LineValue { get; set; }
            public int RowLine { get; set; }
            public int Position { get; set; }
            public char Separator { get; set; }
        }

        public class TemplateDelimiter
        {
            public string Entity { get; set; }
            public string Field { get; set; }
            public string Function { get; set; }
            public string FuncDescription { get; set; }
            public string Positions { get; set; }

            [Display(Name = "Default Value")]
            public string  DefaultValue{ get; set; }
        }

    }
}
