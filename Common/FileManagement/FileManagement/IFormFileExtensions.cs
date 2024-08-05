using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace FileManagement
{
    public static class IFormFileExtensions
    {
        public static DataTable ToDataTable(this IFormFile formFile)
        {
            DataTable dataTable = new DataTable();
            using (var ms = new MemoryStream())
            {
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                XlsxImporter xlsxImporter = new XlsxImporter();
                dataTable = xlsxImporter.ToDataTable(fileBytes);
            }
            return dataTable;
        }

        public static ExcelPackage GetExcelPackageFromFile(this IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            ExcelPackage excelPackage = new ExcelPackage(memoryStream);
            return excelPackage;
        }

        public static async Task<string> ReadAsStringAsync(this IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());
            }
            return result.ToString();
        }

        public static async Task<List<string>> ReadAsList(this IFormFile file)
        {
            var result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.Add(await reader.ReadLineAsync());
            }
            return result;
        }
    }
}
