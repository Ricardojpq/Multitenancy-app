using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using CsvHelper;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FileManagement
{
    public static class XlsxExporterExtentions
    {
        public static byte[] ToExcelPackageByteArray(this DataTable dt, bool addHeader = true)
        {
            //create a new ExcelPackage
            ExcelPackage excelPackage = new ExcelPackage();

            if (String.IsNullOrEmpty(dt.TableName))
                dt.TableName = "Report";

            //create a WorkSheet
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(dt.TableName);

            //add all the content from the DataTable, starting at cell A1
            worksheet.Cells["A1"].LoadFromDataTable(dt, true);

            int col = 1;
            foreach (DataColumn currentColumn in dt.Columns)
            {
                worksheet.Cells[1, col].Value = currentColumn.ColumnName;
                worksheet.Cells[1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[1, col].Style.WrapText = true;
                worksheet.Cells[1, col].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[1, col].Style.Font.Bold = true;
                worksheet.Column(col).Style.Numberformat.Format = FormatDataTableToExcel(currentColumn.DataType);
                col++;
            }
            if (!addHeader)
            {
                worksheet.DeleteRow(1);
            }

            //Make all text fit the cells
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return excelPackage.GetAsByteArray();

        }

        private static string FormatDataTableToExcel(Type type)
        {

            string format = String.Empty;
            switch (type.Name)
            {
                case "int":
                case "short":
                case "long":
                    format = "0";
                    break;
                case "double":
                case "decimal":
                case "float":
                    format = "#,##0.00";
                    break;
                case "DateTime":
                    format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    break;
                case "TimeSpan":
                    format = "h:mm:ss";
                    break;
            }

            return format;
        }

        public static byte[] ConvertToCsv(this ICollection<dynamic> list, bool hasHeader = false)
        {

            try
            {
                using var memoryStream = new MemoryStream();
                using (var streamWriter = new StreamWriter(memoryStream))
                //using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                //{
                //    csvWriter.Configuration.HasHeaderRecord = hasHeader;
                //    csvWriter.WriteRecords(list);
                //}

                return memoryStream.ToArray();
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }
    }
}
