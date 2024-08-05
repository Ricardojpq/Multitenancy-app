using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using FileManagement.ProprietaryFile.Logic.Functions;
using FileManagement.DTOs;
using Serilog;
using System.Globalization;
using Newtonsoft.Json;

namespace FileManagement.FileTypes
{
    public class Fixed : BaseFile
    {

        public Fixed(PFFormatDTO Conf, List<VM.PayerCrossReferenceDTO> CrossReferenceValues )
        {
            this.Conf = Conf;
            if (Conf != null)
            {
                Data = new List<object>();
                DataMultiTable = new List<Dictionary<string, object>>();
                JsonTemplateDictionary = string.IsNullOrEmpty(Conf.JsonConfiguration)
                    ? new List<IDictionary<string,string>>()
                    : JsonConvert.DeserializeObject<IEnumerable<IDictionary<string,string>>>(Conf.JsonConfiguration);
            }
            if(CrossReferenceValues != null)
            {
                _CrossReferenceValues = CrossReferenceValues;
            }
        }

        public override bool LoadFields(List<string> lines)
        {
            errorList = new List<FileValidationError>();
            int iterator = 0;
            try
            {
                var functionB = new FunctionBase(Conf.ConfigParameters,_CrossReferenceValues);
                for (int i = 0; i < lines.Count(); i++)
                {
                    var rowData = GetInstances(Conf.TargetEntity);
                    iterator = i;
                    //var lineSerialized = JsonConvert.SerializeObject(lines[i]);
                    foreach (var item in rowData)
                    {
                        var objData = item.Value;//GetInstance(Conf.TargetEntity);
                        var properties = objData.GetType().GetProperties().ToDictionary(p => p.Name, p => p);
                        //var propertyJsonData = properties.FirstOrDefault(prop => prop.Key == "JsonData");
                        //propertyJsonData.Value.SetValue(objData, lineSerialized);
                        var propertyId = properties.FirstOrDefault(prop => prop.Key == "PFFormatId");
                        propertyId.Value.SetValue(objData, Conf.Id);
                    }

                    foreach (var dictionary in JsonTemplateDictionary)
                    {
                        //var json = JsonConvert.SerializeObject(field);
                        //var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                        var Name = dictionary.First().Key;
                        var Expression = dictionary.First().Value;
                        var UseFunction = Convert.ToBoolean(dictionary.ElementAt(1).Value);
                        var DefaultValue = dictionary.ElementAtOrDefault(2).Value;
                        var Entity = dictionary.ElementAtOrDefault(3).Value;
                        var properties = rowData[Entity].GetType().GetProperties().ToDictionary(p => p.Name, p => p);
                        var property = properties.FirstOrDefault(prop => prop.Key.ToLower() == Name.ToLower());
                        var inputValue = new object();
                        var parameterConf = new FixedConfg
                        {
                            LineValue = lines.ElementAt(i),
                            RowLine = i + 1
                        };
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
                        SetValueDestiny(inputValue, rowData[Entity], i + 1, property);
                    }
                    addData(rowData, i + 1);
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

        public string[] ConvertParameters(string frmt, FixedConfg fixedConfg = null)
        {
            int pFrom = frmt.IndexOf("(") + 1;
            int pTo = frmt.LastIndexOf(")");
            String paramaters = frmt.Substring(pFrom, pTo - pFrom);
            paramaters = paramaters.Replace(",,,", ",Aux2452,");
            var processParam = paramaters.Split(',');
            for (int i = 0; i < processParam.Count(); i++)
            {
                processParam[i] = processParam[i].Replace("Aux2452", ",");
                processParam[i] = ExtractFormat(processParam[i], false, fixedConfg);
            }
            return processParam;
        }

        public static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public override string CreateJsonConfig(IFormFile File)
        {
            errorList = new List<FileValidationError>();
            int i = 1;
            try
            {
                List<dynamic> dynamicEntities = new List<dynamic>();
                DataTable dataTable = File.ToDataTable();
                dynamicEntities = dataTable.ToDinamycEntities(typeof(TemplateFixed));
                var Json = $"[";
                foreach (var row in dynamicEntities)
                {
                    if (string.IsNullOrEmpty(row.Settings) && string.IsNullOrEmpty(row.Function)) { i++; }
                    else
                    {
                        Json += $"{{\"{row.Field}\":\"{BuildExpression(row.Function, row.Settings, ++i)}\",";
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

        public string BuildExpression(string nameFunction, string settings, int row)
        {
            try
            {
                if (string.IsNullOrEmpty(nameFunction))
                {
                    return BuildPosition(settings);
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
                        var NumParam = settings.Split(',').Count();
                        var ParamReq = fdes.Parameters.Where(p => p.IsRequired == true).Count();
                        if (NumParam >= ParamReq)
                        {
                            var expression = $"${nameFunction}(";
                            int orderParam = 0;
                            foreach (var item in settings.Split(','))
                            {
                                if(f == TypeFunction.FindCrossReference && orderParam == 1)
                                {
                                    var CrossReferenceTypes = JsonConvert.DeserializeObject<List<VM.PayerCrossReferenceTypeDTO>>( Conf.ConfigParameters["CrossReferenceTypes"]);
                                    var ParameterCamelCase = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item);//Char.ToUpperInvariant(item[0]) + item.Substring(1).ToLower();
                                    if (CrossReferenceTypes.Any(c=> c.Name.ToLower() == item.ToLower()))
                                    {
                                        expression += BuildPosition(ParameterCamelCase) + ",";
                                    }
                                    else
                                    {
                                        errorList.Add(new FileValidationError
                                        {
                                            Row = row,
                                            Description = $"CrossReferenceType Invalid! Posibles values are {string.Join(",", CrossReferenceTypes.Select(c=> c.Name))}"
                                        });
                                    }

                                }
                                else
                                {
                                    expression += BuildPosition(item) + ",";
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

        public string BuildPosition(string settings)
        {
            if (!settings.Contains(';'))
            {
                return settings;
            }
            int Begin;
            int End;
            char FillValue;
            if (int.TryParse(settings.Split(';')[0], out Begin))
            {
                //set begin
            }
            else
            {
                throw new Exception($"Begin must be a Number {settings}");
            }
            if (int.TryParse(settings.Split(';')[1], out End))
            {
                //set end
            }
            else
            {
                throw new Exception($"Begin must be a Number {settings}");
            }
            if (char.TryParse(settings.Split(';')[2], out FillValue))
            {
                //set FillValue
            }
            else
            {
                throw new Exception($"Begin must be a Character {settings}");
            }
            return $"#F[{Begin};{End};{FillValue}]";
        }

        public override string ExtractFormat(string frmt, bool useFunction, object confValue = null)
        {
            if (!useFunction)
            {
                var resp = Regex.Replace(frmt, @"#F[[]\d+;\d+;.+]", match =>
                {
                    if (confValue == null)
                    {
                        return match.Value.Substring(3).TrimEnd(']');
                    }
                    else
                    {
                        return ProccessExpression(match.Value, (FixedConfg)confValue);
                    }
                });
                return resp;
            }
            else
            {
                return string.Join(',', ConvertParameters(frmt, (FixedConfg)confValue));
            }
        }

        public string ProccessExpression(string matchValue, FixedConfg fc)
        {

            var pos = matchValue.Substring(3).TrimEnd(']').Split(';');
            var Begin = Convert.ToInt16(pos[0]) - 1;
            var End = Convert.ToInt16(pos[1]);
            var FillValue = Convert.ToChar(pos[2]);
            var size = End - Begin;
            if (End > fc.LineValue.Length)
            {
                errorList.Add(new FileValidationError
                {
                    Row = fc.RowLine,
                    Description = $"Position Out of Range!!"
                });
            }
            return fc.LineValue.Substring(Begin, size).Replace(FillValue, ' ').Trim();
        }
    }

    public class FixedConfg
    {
        public string LineValue { get; set; }
        public int RowLine { get; set; }
        public int Begin { get; set; }
        public int End { get; set; }
        public char FillValue { get; set; }

    }

    public class TemplateFixed
    {
        public string Entity { get; set; }
        public string Field { get; set; }
        public string Function { get; set; }
        public string FuncDescription { get; set; }
        [Display(Name = "Begin;End;FillValue")]
        public string Settings { get; set; }
        [Display(Name = "Default Value")]
        public string DefaultValue { get; set; }
    }

}
