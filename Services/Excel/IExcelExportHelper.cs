using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace Services
{
    public interface IExcelExportHelper
    {
        public string ExcelContentType { get; }
        public DataTable ListToDataTable<T>(List<T> data, string[] columnNames);
        public byte[] ExportExcel(DataTable dataTable, string templatePath, int startRowFrom, bool showSrNo = false);
        public Task<byte[]> ExportExcel<T>(List<T> data, string[] columnNames, string templatePath, int startRowFrom = 2, bool isShowSlNo = false);
        public Task<List<T>> ImportExcel<T>(IFormFile file, int startRowSkip = 1, string columnRow = "1:1") where T : new();
        public Task<List<T>> ImportExcel<T>(ExcelWorksheet sheet, int startRowSkip = 1, string columnRow = "1:1") where T : new();
        public Task<List<string>> GetPDFText(IFormFile file);
    }
}
