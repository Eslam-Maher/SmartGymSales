using OfficeOpenXml;
using SmartGymSales.enums;
using SmartGymSales.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SmartGymSales.Services
{
    public class CustomerService
    {
        private SmartGymSalesEntities db = new SmartGymSalesEntities();

        private DataTable getDTfromExel(HttpPostedFile postedFile)
        {
            var tbl = new DataTable();

            using (var excel = new ExcelPackage(postedFile.InputStream))
            {

                var ws = excel.Workbook.Worksheets.First();
                var hasHeader = true;  // adjust accordingly
                                       // add DataColumns to DataTable
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text
                        : String.Format("Column {0}", firstRowCell.Start.Column));

                // add DataRows to DataTable
                int startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.NewRow();
                    foreach (var cell in wsRow)
                        row[cell.Start.Column - 1] = cell.Text;
                    tbl.Rows.Add(row);
                }
            }
            return tbl;
        }

        public List<String> insertExcelToCustomers(HttpPostedFile postedFile)
        {
            List<String> errors = new List<string>();
            if (postedFile == null) {
                errors.Add("failed to parse youe excel file ");
                return errors;
            }
            DataTable excelCustomers = getDTfromExel(postedFile);
            if (excelCustomers.Rows.Count == 0) { errors.Add("failed to parse youe excel file "); }
            foreach (DataColumn column in excelCustomers.Columns)
            {
                foreach (DataRow row in excelCustomers.Rows)
                {
                    if (column.ColumnName == customerSheetHeadersEnum.Name.ToDescriptionString()) {
                        Console.WriteLine(row[column]);
                    }
                    if (column.ColumnName == customerSheetHeadersEnum.Mobile.ToDescriptionString())
                    {
                        Console.WriteLine(row[column]);
                    }
                    if (column.ColumnName == customerSheetHeadersEnum.Email.ToDescriptionString())
                    {
                        Console.WriteLine(row[column]);
                    }
                    if (column.ColumnName == customerSheetHeadersEnum.StartDate.ToDescriptionString())
                    {
                        Console.WriteLine(row[column]);
                    }
                    if (column.ColumnName == customerSheetHeadersEnum.EndDate.ToDescriptionString())
                    {
                        Console.WriteLine(row[column]);
                    }
                }
            }
            return errors;

        }
    }
}