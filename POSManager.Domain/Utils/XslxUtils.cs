using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace POSManager.Domain.Utils
{

    public class XslxUtils
    {
        public DataTable ReadFromStream(DataTable dt, Stream stream, bool hasHeader)
        {
            using (ExcelPackage xsl = new ExcelPackage(stream))
            {
                if (xsl.Workbook.Worksheets.Count == 0)
                {
                    throw new ArgumentException("Excel file does not contain any worksheet.");
                }
                var worksheet = xsl.Workbook.Worksheets.First();

                int startRow = hasHeader ? 2 : 1;
                ValidateExcel(worksheet, dt);
                for (int rowNum = startRow; rowNum <= worksheet.Dimension.Rows; rowNum++)
                {
                    var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.Columns];
                    DataRow row = dt.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return dt;
            }
        }

        private static void ValidateExcel(ExcelWorksheet worksheet, DataTable dt)
        {
            if (worksheet.Dimension.Columns != dt.Columns.Count)
            {
                throw new ArgumentException(string.Format("Excel sheet should not have more or less than {0} columns.", dt.Columns.Count));
            }
            var headerRow = worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns];
            int count = headerRow.Columns;
            for (int i = 1; i <= count; i++)
            {
                var headerToCompare = worksheet.Cells[1, 1, 1, worksheet.Dimension.Columns];
                if (dt.Columns[i - 1].ColumnName.ToUpper() != headerToCompare[1, i].Text.ToUpper())
                {
                    throw new ArgumentException(string.Format("Excel file uploaded not in correct format, Please upload excel sheet in the correct format as advised"));
                }
            }
        }

        public void ExportToResponse(HttpResponse response, DataTable dt, string fileName)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
                ws.Cells["A1"].LoadFromDataTable(dt, true);
                string cellRange = "A1:" + Convert.ToChar('A' + dt.Columns.Count - 1) + 1;
                using (ExcelRange rng = ws.Cells[cellRange])
                {
                    rng.Style.WrapText = false;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid; //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                string rowsCellRange = "A2:" + Convert.ToChar('A' + dt.Columns.Count - 1) + dt.Rows.Count * dt.Columns.Count;
                using (ExcelRange rng = ws.Cells[rowsCellRange])
                {
                    rng.Style.WrapText = false;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                }
                Byte[] fileBytes = pck.GetAsByteArray();
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Cookies.Clear();

                response.Cache.SetCacheability(HttpCacheability.Private);
                response.CacheControl = "private";
                response.Charset = System.Text.UTF8Encoding.UTF8.WebName;
                response.ContentEncoding = System.Text.UTF8Encoding.UTF8;
                response.AppendHeader("Content-Length", fileBytes.Length.ToString());
                response.AppendHeader("Pragma", "cache");
                response.AppendHeader("Expires", "60");
                response.AppendHeader("Content-Disposition",
                    "attachment; " +
                    string.Format("filename=\"{0}.xlsx\"; ", fileName) +
                    "size=" + fileBytes.Length.ToString() + "; " +
                    "creation-date=" + DateTime.Now.ToString("R") + "; " +
                    "modification-date=" + DateTime.Now.ToString("R") + "; " +
                    "read-date=" + DateTime.Now.ToString("R")
                );
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.BinaryWrite(fileBytes);
                response.End();
            }
        }

        public void ExportToResponse<T>(HttpResponse response, IEnumerable<T> collection, string fileName) where T : new()
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
                ws.Cells["A1"].LoadFromCollection(collection, true);
                //XlsxWriter writer = new XlsxWriter();

                int rows = collection.Count();
                int columns = new T().GetType().GetProperties().Count();

                string cellRange = "A1:" + Convert.ToChar('A' + columns - 1) + 1;
                using (ExcelRange rng = ws.Cells[cellRange])
                {
                    rng.Style.WrapText = false;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid; //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                string rowsCellRange = "A2:" + Convert.ToChar('A' + columns - 1) + rows * columns;
                using (ExcelRange rng = ws.Cells[rowsCellRange])
                {
                    rng.Style.WrapText = false;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                }
                Byte[] fileBytes = pck.GetAsByteArray();
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Cookies.Clear();

                response.Cache.SetCacheability(HttpCacheability.Private);
                response.CacheControl = "private";
                response.Charset = System.Text.UTF8Encoding.UTF8.WebName;
                response.ContentEncoding = System.Text.UTF8Encoding.UTF8;
                response.AppendHeader("Content-Length", fileBytes.Length.ToString());
                response.AppendHeader("Pragma", "cache");
                response.AppendHeader("Expires", "60");
                response.AppendHeader("Content-Disposition",
                    "attachment; " +
                    string.Format("filename=\"{0}.xlsx\"; ", fileName) +
                    "size=" + fileBytes.Length.ToString() + "; " +
                    "creation-date=" + DateTime.Now.ToString("R") + "; " +
                    "modification-date=" + DateTime.Now.ToString("R") + "; " +
                    "read-date=" + DateTime.Now.ToString("R")
                );
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.BinaryWrite(fileBytes);
                response.End();
            }
        }
    }
}
