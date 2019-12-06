using OfficeOpenXml;
using SmartGymSales.enums;
using SmartGymSales.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
 
        public List<String> insertExcelToCustomers(HttpPostedFile postedFile,string user_name,string password)
        {
           
            UtilsService US = new UtilsService();
            UsersService userService = new UsersService();
            UserRolesService userRolesService = new UserRolesService();


           User currentUser= userService.GetUserbyUser_name(user_name);
            List<String> errors = new List<string>();
            if (!userService.checkUserCred(user_name, password)) {
                errors.Add("please Login and try again");
                return errors;
            }
            if (!userRolesService.isUserAdmin(user_name)) {
                errors.Add("You are not authorized");
                return errors;
            }
            if (postedFile == null)
            {
                errors.Add("failed to parse youe excel file ");
                return errors;
            }
            DataTable excelCustomers = getDTfromExel(postedFile);
            if (excelCustomers.Rows.Count == 0) { errors.Add("failed to parse youe excel file "); }
            int rowIndex = 0;
            foreach (DataRow row in excelCustomers.Rows)
            {
                rowIndex++;
                customer newCustomer = new customer();
                bool customerError = false;
                foreach (DataColumn column in excelCustomers.Columns)
                {
                    if (column.ColumnName == customerSheetHeadersEnum.Name.ToDescriptionString())
                    {
                        Console.WriteLine(row[column]);
                        if (!String.IsNullOrEmpty(row[column].ToString()))
                        {
                            newCustomer.name = row[column].ToString();
                        }
                        else
                        {
                            errors.Add("Name is Empty at row : " + rowIndex);
                            customerError = true;
                        }
                    }
                    if (column.ColumnName == customerSheetHeadersEnum.Mobile.ToDescriptionString())
                    {
                        if (String.IsNullOrEmpty(row[column].ToString()))
                        {
                            errors.Add("Mobile Phone is Empty at row : " + rowIndex);
                            customerError = true;
                        }
                        else if (!US.checkPhoneNumberVaildaty(row[column].ToString()))
                        {
                            errors.Add("Mobile Phone is not correct at row : " + rowIndex);
                            customerError = true;
                        }
                        else if (!US.checkPhoneNumberRedundancy(row[column].ToString()))
                        {
                            errors.Add("Mobile Phone is already found at row : " + rowIndex);
                            customerError = true;
                        }
                        else
                        {
                            newCustomer.mobile = int.Parse(row[column].ToString());
                        }
                        Console.WriteLine(row[column]);
                    }
                    if (column.ColumnName == customerSheetHeadersEnum.Email.ToDescriptionString())
                    {
                        if (String.IsNullOrEmpty(row[column].ToString()))
                        {
                            errors.Add("Email is Empty at row : " + rowIndex);
                        }
                        else if (!String.IsNullOrEmpty(row[column].ToString())&&!US.checkEmailVaildaty(row[column].ToString()))
                        {
                            errors.Add("Email is not correct at row : " + rowIndex);
                            customerError = true;
                        }
                        else if (!String.IsNullOrEmpty(row[column].ToString()) && !US.checkEmailRedundancy(row[column].ToString()))
                        {
                            errors.Add("Email is already found at row : " + rowIndex);
                            customerError = true;
                        }
                        else
                        {
                            newCustomer.email = row[column].ToString();
                        }
                    }
                    if (column.ColumnName == customerSheetHeadersEnum.StartDate.ToDescriptionString())
                    {
                        DateTime startDate = new DateTime();
                            bool sucess= DateTime.TryParseExact(row[column].ToString(), "dd-MM-yyyy",
                                System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
                        if (!sucess)
                        {
                            errors.Add("Couldn't convert start date at row : " + rowIndex);
                            customerError = true;
                        }
                        else {
                            newCustomer.subscription_start_date = startDate;
                        }
                    }
                    if(column.ColumnName == customerSheetHeadersEnum.discont_percentage.ToDescriptionString()) {
                        if (!String.IsNullOrEmpty(row[column].ToString()))
                        {
                            int percentage = 0;
                            if (int.TryParse(row[column].ToString(), out percentage))
                            {
                                newCustomer.discont_percentage = percentage;
                            }
                            else
                            {
                                errors.Add("please enter discont percentage in numbers only at row : " + rowIndex);
                                customerError = true;
                            }
                        }

                    }
                        
                    if (column.ColumnName == customerSheetHeadersEnum.EndDate.ToDescriptionString())
                    {
                        DateTime endDate = new DateTime();
                        bool sucess = DateTime.TryParseExact(row[column].ToString(), "dd-MM-yyyy",
                            System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);
                        if (!sucess)
                        {
                            errors.Add("Couldn't convert start date at row : " + rowIndex);
                            customerError = true;
                        }
                        else
                        {
                            if (newCustomer.subscription_start_date > endDate)
                            {
                                errors.Add("End Date can't be earlier than start date at row : " + rowIndex);
                                customerError = true;
                            }
                            else
                            {
                                newCustomer.subscription_start_date = endDate;
                            }
                        }
                    }
                }

                #region fill cutomerData
                newCustomer.added_By_id = currentUser.id;
                newCustomer.addition_type_id = (int)enums.AddtionalLookupEnm.Sheet;
                newCustomer.is_called = false;
                newCustomer.calles_count = 0;
                newCustomer.men_forign_Key = null;
                newCustomer.women_forign_key=null;
                newCustomer.creation_date = DateTime.Now;
                if (newCustomer.subscription_start_date < DateTime.Now &&
                    DateTime.Now < newCustomer.subscription_end_date)
                {
                    newCustomer.is_active = true;
                }
                else {
                    newCustomer.is_active = false;
                }

                #endregion


                if (!customerError)
                {
                    db.customers.Add(newCustomer);

                }
            }
            if (errors.Count == 0)
            {
                db.SaveChanges();
            }
            return errors;

        }
    }
}