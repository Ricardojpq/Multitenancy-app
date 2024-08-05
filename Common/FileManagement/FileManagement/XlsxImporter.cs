using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FileManagement
{
    public class XlsxImporter
    {
        public DataTable ToDataTable(byte[] data)
        {
            DataTable dt = new DataTable();

            MemoryStream memoryStream = new MemoryStream(data);
            using (var package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                //check if the worksheet is completely empty
                if (worksheet.Dimension == null)
                {
                    return dt;
                }

                //create a list to hold the column names
                List<string> columnNames = new List<string>();

                //needed to keep track of empty column headers
                int currentColumn = 1;

                //loop all columns in the sheet and add them to the datatable
                foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    string columnName = cell.Text.Trim();

                    //check if the previous header was empty and add it if it was
                    if (cell.Start.Column != currentColumn)
                    {
                        columnNames.Add("Header_" + currentColumn);
                        dt.Columns.Add("Header_" + currentColumn);
                        currentColumn++;
                    }

                    //add the column name to the list to count the duplicates
                    columnNames.Add(columnName);

                    //count the duplicate column names and make them unique to avoid the exception
                    //A column named 'Name' already belongs to this DataTable
                    int occurrences = columnNames.Count(x => x.Equals(columnName));
                    if (occurrences > 1)
                    {
                        columnName = columnName + "_" + occurrences;
                    }

                    //add the column to the datatable
                    dt.Columns.Add(columnName);

                    currentColumn++;
                }

                //start adding the contents of the excel file to the datatable
                for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                {
                    var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                    DataRow newRow = dt.NewRow();

                    //loop all cells in the row
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }

                    dt.Rows.Add(newRow);
                }
            }

            return dt;
        }
    }

    public static class DataTableImportExtensions
    {
        public static List<object> ToDinamycEntities(this DataTable dataTable, Type tableType)
        {
            List<object> entities = new List<object>();
            string[] ignoredColumns = { Constants.ERRORS };
            var props = tableType.GetProperties();
            var properties = tableType.GetProperties()
            .Where(p => p.IsDefined(typeof(DisplayAttribute), false))
            .Select(p => new
            {
                PropertyName = p.Name,
                p.GetCustomAttributes(typeof(DisplayAttribute),
                        false).Cast<DisplayAttribute>().Single().Name
            });
            foreach (DataRow row in dataTable.Rows)
            {
                if (row.ItemArray.Any(x => x.ToString() != string.Empty && x != DBNull.Value))
                {
                    var dynamicObject = new ExpandoObject() as IDictionary<string, Object>;
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        var currentColumn = dataTable.Columns[i];
                        string columnName = currentColumn.ToString();
                        if (!ignoredColumns.Contains(columnName))
                        {
                            var propName = properties.FirstOrDefault(p => p.PropertyName == columnName || p.Name == columnName)?.PropertyName;
                            var prop = props.FirstOrDefault(x => x.Name == columnName);
                            if (!string.IsNullOrEmpty(propName))
                            {
                                prop = props.FirstOrDefault(x => x.Name == propName);
                            }
                            if (prop != null)
                            {
                                var value = row.ItemArray[i].ToString();
                                object convertedValue;
                                bool typeAllowNulls = Nullable.GetUnderlyingType(prop.PropertyType) == null ? false : true;
                                if (typeAllowNulls && value == string.Empty)
                                    convertedValue = null;
                                else
                                {
                                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                    if (type == typeof(bool))
                                    {
                                        value = (value.ToLower() == "yes") ? bool.TrueString : bool.FalseString;
                                    }
                                    if (type == typeof(int) && string.IsNullOrEmpty(value))
                                    {
                                        value = "0";
                                    }
                                    convertedValue = Convert.ChangeType(value, type);
                                }
                                dynamicObject.Add(prop.Name, convertedValue);
                            }
                        }
                    }
                    entities.Add(dynamicObject);
                }
            }
            return entities;

        }

        public static void CreateUniqueColumn(this DataTable dataTable, string columnName)
        {
            if (!dataTable.Columns.Contains(columnName))
                dataTable.Columns.Add(columnName);
        }

        public static void PrintErrors(this DataTable dataTable, List<FileValidationError> validationErrors)
        {
            foreach (FileValidationError validationError in validationErrors)
            {
                dataTable.Rows[validationError.Row].SetColumnError(dataTable.Columns[1], validationError.Description);
            }
        }
    }

    public static class ExcelPackageExtensions
    {
        public static void PrintErrorsAtWorksheet(this ExcelWorksheet worksheet, List<FileValidationError> validationErrorList)
        {
            worksheet.CreateColumnErrorIfNotExist();
            var errorGroup = validationErrorList.GroupBy(x => x.Row);
            var errorColumnIndex = worksheet.Cells["1:1"].First(x => x.Value.ToString() == Constants.ERRORS).Start.Column;
            foreach (var row in errorGroup)
            {
                var last = row.Last();
                string concatenatedErrors = string.Empty;
                var currentCell = worksheet.Cells[row.Key, errorColumnIndex];
                foreach (var error in row)
                {
                    concatenatedErrors += "-" + error.Description + (last.Description != error.Description ? "\r\n" : "");
                }
                currentCell.Style.WrapText = true;
                //currentCell.Style.ShrinkToFit = true;
                //currentCell.Style.Font.Name = "Calibri";
                //currentCell.Style.Font.Size = 11;

                currentCell.Value = concatenatedErrors;
            }
            //worksheet.Cells.AutoFitColumns();
            worksheet.Column(errorColumnIndex).SetTrueColumnWidth(30);
        }
        public static void SetCustomStyle(this ExcelWorksheet excelWorksheet)
        {
            bool isColumnErrorCreated = excelWorksheet.IsColumnErrorCreated();
            int pivot1Row = excelWorksheet.Dimension.Start.Row;
            int pivot1Column = excelWorksheet.Dimension.Start.Column;
            int pivot2Row = excelWorksheet.Dimension.End.Row;
            int pivot2Column = excelWorksheet.Dimension.End.Column;
            var bodyCellRange = excelWorksheet.Cells[pivot1Row + 1, pivot1Column, pivot2Row, pivot2Column];

            //Header
            excelWorksheet.Cells[pivot1Row, pivot1Column, pivot1Row, pivot2Column].Style.Font.Bold = true;

            excelWorksheet.Cells[pivot1Row, pivot1Column].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            excelWorksheet.Cells[pivot1Row, pivot1Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            excelWorksheet.Cells[pivot1Row, pivot1Column].Style.Border.Left.Style = ExcelBorderStyle.Thick;

            excelWorksheet.Cells[pivot1Row, pivot1Column + 1, pivot1Row, pivot2Column - 1].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            excelWorksheet.Cells[pivot1Row, pivot1Column + 1, pivot1Row, pivot2Column - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            excelWorksheet.Cells[pivot1Row, pivot1Column + 1, pivot1Row, pivot2Column - 1].Style.Border.Left.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot1Row, pivot1Column + 1, pivot1Row, pivot2Column - 1].Style.Border.Right.Style = ExcelBorderStyle.None;

            excelWorksheet.Cells[pivot1Row, pivot2Column].Style.Border.Top.Style = ExcelBorderStyle.Thick;
            excelWorksheet.Cells[pivot1Row, pivot2Column].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            excelWorksheet.Cells[pivot1Row, pivot2Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

            //Body
            excelWorksheet.Cells[pivot1Row + 1, pivot1Column, pivot2Row - 1, pivot1Column].Style.Border.Top.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot1Row + 1, pivot1Column, pivot2Row - 1, pivot1Column].Style.Border.Bottom.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot1Row + 1, pivot1Column, pivot2Row - 1, pivot1Column].Style.Border.Left.Style = ExcelBorderStyle.Thick;

            excelWorksheet.Cells[pivot1Row + 1, pivot1Column + 1, pivot2Row, pivot2Column - 1].Style.Border.Top.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot1Row + 1, pivot1Column + 1, pivot2Row, pivot2Column - 1].Style.Border.Bottom.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot1Row + 1, pivot1Column + 1, pivot2Row, pivot2Column - 1].Style.Border.Left.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot1Row + 1, pivot1Column + 1, pivot2Row, pivot2Column - 1].Style.Border.Right.Style = ExcelBorderStyle.None;

            excelWorksheet.Cells[pivot1Row + 1, pivot2Column, pivot2Row - 1, pivot2Column].Style.Border.Top.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot1Row + 1, pivot2Column, pivot2Row - 1, pivot2Column].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            excelWorksheet.Cells[pivot1Row + 1, pivot2Column, pivot2Row - 1, pivot2Column].Style.Border.Bottom.Style = ExcelBorderStyle.None;

            //Bottom
            excelWorksheet.Cells[pivot2Row, pivot1Column].Style.Border.Top.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot2Row, pivot1Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            excelWorksheet.Cells[pivot2Row, pivot1Column].Style.Border.Left.Style = ExcelBorderStyle.Thick;

            excelWorksheet.Cells[pivot2Row, pivot1Column + 1, pivot2Row, pivot2Column - 1].Style.Border.Top.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot2Row, pivot1Column + 1, pivot2Row, pivot2Column - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
            excelWorksheet.Cells[pivot2Row, pivot1Column + 1, pivot2Row, pivot2Column - 1].Style.Border.Left.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot2Row, pivot1Column + 1, pivot2Row, pivot2Column - 1].Style.Border.Right.Style = ExcelBorderStyle.None;

            excelWorksheet.Cells[pivot2Row, pivot2Column].Style.Border.Top.Style = ExcelBorderStyle.None;
            excelWorksheet.Cells[pivot2Row, pivot2Column].Style.Border.Right.Style = ExcelBorderStyle.Thick;
            excelWorksheet.Cells[pivot2Row, pivot2Column].Style.Border.Bottom.Style = ExcelBorderStyle.Thick;

            //Errors
            if (isColumnErrorCreated)
            {
                for (int x = pivot1Row + 1; x <= pivot2Row; x++)
                {
                    if (excelWorksheet.Cells[x, pivot2Column].Text != string.Empty)
                    {
                        excelWorksheet.Cells[x, pivot2Column].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        excelWorksheet.Cells[x, pivot2Column].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ffc7ce"));
                        excelWorksheet.Cells[x, pivot2Column].Style.Font.Color.SetColor(ColorTranslator.FromHtml("#be0006"));
                    }
                }
            }
        }
        private static void SetTrueColumnWidth(this ExcelColumn column, double width)
        {
            // Deduce what the column width would really get set to.
            var z = width >= (1 + 2 / 3)
                ? Math.Round((Math.Round(7 * (width - 1 / 256), 0) - 5) / 7, 2)
                : Math.Round((Math.Round(12 * (width - 1 / 256), 0) - Math.Round(5 * width, 0)) / 12, 2);

            // How far off? (will be less than 1)
            var errorAmt = width - z;

            // Calculate what amount to tack onto the original amount to result in the closest possible setting.
            var adj = width >= 1 + 2 / 3
                ? Math.Round(7 * errorAmt - 7 / 256, 0) / 7
                : Math.Round(12 * errorAmt - 12 / 256, 0) / 12 + (2 / 12);

            // Set width to a scaled-value that should result in the nearest possible value to the true desired setting.
            if (z > 0)
            {
                column.Width = width + adj;
                return;
            }

            column.Width = 0d;
        }
        private static void CreateColumnErrorIfNotExist(this ExcelWorksheet worksheet)
        {
            bool isColumnErrorCreated = worksheet.IsColumnErrorCreated();
            if (!isColumnErrorCreated)
            {
                var errorCell = worksheet.Cells[worksheet.Dimension.Start.Column, worksheet.Dimension.End.Column + 1];
                errorCell.Value = Constants.ERRORS;
                errorCell.Style.Font.Bold = true;
                //errorCell.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                //errorCell.Style.Border.Top.Style = ExcelBorderStyle.Thick;
                //errorCell.Style.Border.Left.Style = ExcelBorderStyle.None;
                //errorCell.Style.Border.Right.Style = ExcelBorderStyle.Thick;
            }
        }

        private static bool IsColumnErrorCreated(this ExcelWorksheet worksheet)
        {
            bool isColumnErrorCreated = false;
            foreach (var firstRowCell in worksheet.Cells[worksheet.Dimension.Start.Row, worksheet.Dimension.Start.Column, 1, worksheet.Dimension.End.Column])
                if (firstRowCell.Text == Constants.ERRORS)
                    isColumnErrorCreated = true;
            return isColumnErrorCreated;
        }
    }

}
