﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;

namespace Services
{
    public class ExcelExportHelper : IExcelExportHelper
    {
        public string ExcelContentType
        {
            get
            {
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
        }
        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable ListToDataTable<T>(List<T> data, string[] columnNames)
        {

            List<PropertyInfo> properties = new List<PropertyInfo>();
            DataTable dataTable = new DataTable();
            foreach (string colName in columnNames)
            {
                PropertyInfo property = typeof(T).GetProperty(colName);
                properties.Add(property);
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }


        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="heading">工作簿Worksheet</param>
        /// <param name="showSrNo">//是否显示行编号</param>
        /// <param name="columnsToTake">要导出的列</param>
        /// <returns></returns>
        public byte[] ExportExcel(DataTable dataTable, string templatePath, int startRowFrom, bool showSrNo = false)
        {
            byte[] result = null;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            FileInfo existingFile = new FileInfo(templatePath);
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                //是否显示行编号
                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }

                //Add Content Into the Excel File
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, false);
                // autofit width of cells with small content  
                int columnIndex = 1;
                foreach (DataColumn item in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];

                    workSheet.Column(columnIndex).AutoFit();
                    //}
                    columnIndex++;
                }

                if (dataTable.Rows.Count > 0)
                {
                    using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom + dataTable.Rows.Count - 1, dataTable.Columns.Count])
                    {
                        r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                    }
                }

                result = package.GetAsByteArray();

            }
            return result;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="heading"></param>
        /// <param name="isShowSlNo"></param>
        /// <param name="ColumnsToTake"></param>
        /// <returns></returns>
        public Task<byte[]> ExportExcel<T>(List<T> data, string[] columnNames, string templatePath, int startRowFrom = 2, bool isShowSlNo = false)
        {
            return Task.Run(() => ExportExcel(ListToDataTable<T>(data, columnNames), templatePath, startRowFrom, isShowSlNo));
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public Task<List<T>> ImportExcel<T>(IFormFile file, int startRowSkip = 1, string columnRow = "1:1") where T : new()
        {
            return Task.Run(() =>
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                var package = new ExcelPackage(file.OpenReadStream());
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets.First();
                if (worksheet == null) return new List<T>();
                worksheet.TrimLastEmptyRows();
                return worksheet.ConvertSheetToObjects<T>(startRowSkip, columnRow).ToList();
            });
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public Task<List<T>> ImportExcel<T>(ExcelWorksheet sheet, int startRowSkip = 1, string columnRow = "1:1") where T : new()
        {
            return Task.Run(() =>
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                if (sheet == null) return new List<T>();
                sheet.TrimLastEmptyRows();
                return sheet.ConvertSheetToObjects<T>(startRowSkip, columnRow).ToList();
            });
        }

        public Task<List<string>> GetPDFText(IFormFile file)
        {
            return Task.Run(() => PDFTextExtractor.ExtractText(file.OpenReadStream()));
        }
    }
}
