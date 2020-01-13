using OfficeOpenXml;
using SmartGymSales.enums;
using SmartGymSales.Models;
using SmartGymSales.Models.SmartGymMen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web;
namespace SmartGymSales.Services
{
    public class CustomerService
    {

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
                        row[cell.Start.Column - 1] = cell.Value;
                    tbl.Rows.Add(row);
                }
            }
            return tbl;
        }

        public void UpdateCustomersCalledByUser(User user)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            List<SalesCustomer> SCList = db.SalesCustomers.Where(x => x.is_called_by == user.id).ToList();
            foreach (SalesCustomer element in SCList)
            {
                element.is_called_by = null;
                db.SalesCustomers.AddOrUpdate(element);
            }
            db.SaveChanges();
        }

        public List<String> UpdatePossibleCustomerFromdb(string user_name, string password, string sourceDbString)
        {

            using (var db = new SmartGymSalesEntities())
            {
                UtilsService US = new UtilsService();
                UsersService userService = new UsersService();
                UserRolesService userRolesService = new UserRolesService();


                User currentUser = userService.GetUserbyUser_name(user_name);
                List<String> errors = new List<string>();
                if (!userService.checkUserCred(user_name, password))
                {
                    errors.Add("please Login and try again");
                    return errors;
                }
                if (!userRolesService.isUserManger(user_name))
                {
                    errors.Add("You are not authorized");
                    return errors;
                }
                if (sourceDbString == "Men")
                {
                    SmartGymMenEntities sourceDb = new SmartGymMenEntities();
                    List<T_session_subscriber> sourceCustomers = sourceDb.T_session_subscriber.Where(x => !String.IsNullOrEmpty(x.sunmobil)).ToList();

                    List<T_session_subscriber> toBeInsertedPossibleCustomers = new List<T_session_subscriber>();
                    foreach (T_session_subscriber element in sourceCustomers)
                    {
                        if (US.checkPhoneNumberVaildaty(element.sunmobil))
                        {
                            if (db.possibleCustomers.Where(possibleCusElement => possibleCusElement.mobile.ToString() == element.sunmobil).Count() == 0)
                            {
                                toBeInsertedPossibleCustomers.Add(element);
                            }
                        }
                    }
                    errors.AddRange(InsertIntoPossibleCustomersFromMenDb(toBeInsertedPossibleCustomers, currentUser));
                }
                else if (sourceDbString == "Women")
                {
                    SmartGymMenEntities sourceDb = new SmartGymMenEntities();
                }
                else
                {
                    errors.Add("Failed to identify source to update from");
                    return errors;
                }


                return errors;

            }
        }
        public List<String> UpdateSalesCustomerFromdb(string user_name, string password, string sourceDbString)
        {
            using (var db = new SmartGymSalesEntities())
            {
                UtilsService US = new UtilsService();
                UsersService userService = new UsersService();
                UserRolesService userRolesService = new UserRolesService();


                User currentUser = userService.GetUserbyUser_name(user_name);
                List<String> errors = new List<string>();
                if (!userService.checkUserCred(user_name, password))
                {
                    errors.Add("please Login and try again");
                    return errors;
                }
                if (!userRolesService.isUserManger(user_name))
                {
                    errors.Add("You are not authorized");
                    return errors;
                }
                if (sourceDbString == "Men")
                {
                    SmartGymMenEntities sourceDb = new SmartGymMenEntities();
                    List<Customer> sourceCustomers = sourceDb.Customers.Where(x => !String.IsNullOrEmpty(x.Phone_mobile)).ToList();

                    List<Customer> toBeUpdatedCustomers = new List<Customer>();
                    List<Customer> toBeInsertedCustomers = new List<Customer>();
                    foreach (Customer element in sourceCustomers)
                    {
                        if (US.checkPhoneNumberVaildaty(element.Phone_mobile))
                        {
                            if (db.SalesCustomers.Where(salesCusElement => salesCusElement.men_forign_Key == element.Customer_ID).Count() > 1)
                            {
                                toBeUpdatedCustomers.Add(element);
                            }
                            else
                            {
                                toBeInsertedCustomers.Add(element);
                            }
                        }
                    }
                    updateCustomersFromMenDb(toBeUpdatedCustomers, currentUser);
                    List<String> insertResult = InsertIntoCustomersFromMenDb(toBeInsertedCustomers, currentUser);
                    errors.AddRange(insertResult);
                }
                else if (sourceDbString == "Women")
                {
                    SmartGymMenEntities sourceDb = new SmartGymMenEntities();
                }
                else
                {
                    errors.Add("Failed to identify source to update from");
                    return errors;
                }


                return errors;
            }

        }
        private void updateCustomersFromMenDb(List<Customer> sourceList, User currentUser)
        {
            using (var db = new SmartGymSalesEntities())
            {
                SmartGymMenEntities sourceDb = new SmartGymMenEntities();
                foreach (Customer srcCustomer in sourceList)
                {
                    SalesCustomer salesCustomer = db.SalesCustomers.Where(x => x.men_forign_Key == srcCustomer.Customer_ID).FirstOrDefault();

                    Membership srcCustomerMembership = sourceDb.Memberships.Where(x => x.Customer_ID == srcCustomer.Customer_ID).FirstOrDefault();
                    //isActive and SubscribtionDate
                    if (srcCustomerMembership == null)
                    {
                        salesCustomer.subscription_end_date = salesCustomer.subscription_start_date = null;
                        salesCustomer.is_active = false;
                    }
                    else
                    {

                        salesCustomer.subscription_start_date = srcCustomerMembership.S_Date;
                        salesCustomer.subscription_end_date = srcCustomerMembership.E_Date;
                        if (salesCustomer.subscription_start_date < DateTime.Now &&
                        DateTime.Now < salesCustomer.subscription_end_date)
                        {
                            salesCustomer.is_active = true;
                            salesCustomer.subscription_paid_money = srcCustomerMembership.moneypaied;

                        }
                        else
                        {
                            salesCustomer.is_active = false;
                        }
                    }
                    db.SalesCustomers.AddOrUpdate(salesCustomer);
                }
                db.SaveChanges();
            }
        }

        public SalesCustomer getSalesCustomers(int id)
        {
            if (CustomerExists(id))
            {
                SmartGymSalesEntities db = new SmartGymSalesEntities();
                return db.SalesCustomers.Where(x => x.id == id).FirstOrDefault();
            }
            return null;
        }
        public bool CustomerExists(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.SalesCustomers.Count(e => e.id == id) > 0;
        }
        private List<String> InsertIntoCustomersFromMenDb(List<Customer> sourceList, User currentUser)
        {
            using (var db = new SmartGymSalesEntities())
            {
                UtilsService US = new UtilsService();
                SmartGymMenEntities sourceDb = new SmartGymMenEntities();

                List<String> errors = new List<String>();
                int rowError = 0;
                foreach (Customer srcCustomer in sourceList)
                {

                    SalesCustomer newCustomer = new SalesCustomer();
                    bool customerError = false;
                    //name
                    if (!String.IsNullOrEmpty(srcCustomer.Name))
                    {
                        newCustomer.name = srcCustomer.Name;
                    }
                    else
                    {
                        customerError = true;
                    }

                    //mobile
                    if (String.IsNullOrEmpty(srcCustomer.Phone_mobile))
                    {
                        customerError = true;
                    }
                    else if (!US.checkPhoneNumberVaildaty(srcCustomer.Phone_mobile))
                    {
                        customerError = true;
                    }
                    else if (US.checkPhoneNumberRedundancy(srcCustomer.Phone_mobile))
                    {
                        customerError = true;
                    }
                    else
                    {
                        newCustomer.mobile = srcCustomer.Phone_mobile;
                    }
                    //email
                    if (!String.IsNullOrEmpty(srcCustomer.email) && !US.checkEmailVaildaty(srcCustomer.email))
                    {
                        customerError = true;
                    }
                    else if (!String.IsNullOrEmpty(srcCustomer.email) && US.checkEmailRedundancy(srcCustomer.email))
                    {
                        customerError = true;
                    }
                    else if (!String.IsNullOrEmpty(srcCustomer.email))
                    {
                        newCustomer.email = srcCustomer.email;
                    }


                    Membership srcCustomerMembership = sourceDb.Memberships.Where(x => x.Customer_ID == srcCustomer.Customer_ID).OrderByDescending(x => x.recorddate).FirstOrDefault();
                    //isActive and SubscribtionDate
                    if (srcCustomerMembership == null)
                    {
                        newCustomer.subscription_end_date = newCustomer.subscription_start_date = null;
                        newCustomer.is_active = false;
                    }
                    else
                    {

                        newCustomer.subscription_start_date = srcCustomerMembership.S_Date;
                        newCustomer.subscription_end_date = srcCustomerMembership.E_Date;
                        if (newCustomer.subscription_start_date < DateTime.Now &&
                        DateTime.Now < newCustomer.subscription_end_date)
                        {
                            newCustomer.is_active = true;
                            newCustomer.subscription_paid_money = srcCustomerMembership.moneypaied;
                        }
                        else
                        {
                            newCustomer.is_active = false;
                        }
                    }


                    newCustomer.added_By_id = currentUser.id;
                    newCustomer.addition_type_id = (int)enums.AddtionalLookupEnum.Sync;
                    newCustomer.is_called = false;
                    newCustomer.calles_count = 0;
                    newCustomer.men_forign_Key = srcCustomer.Customer_ID;
                    newCustomer.women_forign_key = null;
                    newCustomer.creation_date = DateTime.Now;
                    if (newCustomer.subscription_start_date < DateTime.Now &&
                        DateTime.Now < newCustomer.subscription_end_date)
                    {
                        newCustomer.is_active = true;
                    }
                    else
                    {
                        newCustomer.is_active = false;
                    }


                    if (!customerError)
                    {
                        db.SalesCustomers.Add(newCustomer);

                    }
                    else
                    {
                        rowError++;

                    }

                }
                if (rowError > 0)
                {
                    errors.Add("Couldn't Sync " + rowError + " customers due to wrong data");
                }
                db.SaveChanges();

                return errors;
            }
        }

        private List<String> InsertIntoPossibleCustomersFromMenDb(List<T_session_subscriber> sourceList, User currentUser)
        {
            var db = new SmartGymSalesEntities();
            List<String> errors = new List<String>();
            UtilsService US = new UtilsService();
            SmartGymMenEntities sourceDb = new SmartGymMenEntities();
            foreach (T_session_subscriber item in sourceList)
            {
                possibleCustomer possibleCustomerItem = new possibleCustomer();
                bool customerError = false;
                //name
                if (!String.IsNullOrEmpty(item.subname))
                {
                    possibleCustomerItem.name = item.subname;
                }
                else
                {
                    errors.Add("Name field is empty at id :" + item.id);
                    customerError = true;
                }

                //mobile
                if (String.IsNullOrEmpty(item.sunmobil))
                {
                    errors.Add("Mobile field is empty at id :" + item.id + " with name: " + item.subname);
                    customerError = true;
                }
                else if (!US.checkPhoneNumberVaildaty(item.sunmobil))
                {
                    errors.Add("Mobile field is not valid at id :" + item.id + " with name: " + item.subname);
                    customerError = true;
                }
                else if (US.checkPhoneNumberRedundancyforPossibleCustomers(item.sunmobil))
                {
                    errors.Add("Mobile field is found before at id :" + item.id + " with name: " + item.subname);
                    customerError = true;
                }
                else
                {
                    possibleCustomerItem.mobile = item.sunmobil;
                }
                //email
                if (!String.IsNullOrEmpty(item.subemail) && !US.checkEmailVaildaty(item.subemail))
                {
                    errors.Add("Email field is not valid at id :" + item.id + " with name: " + item.subname);
                    customerError = true;
                }
                else if (!String.IsNullOrEmpty(item.subemail) && US.checkEmailRedundancyforPossibleCustomers(item.subemail))
                {
                    errors.Add("Email field is found before at id :" + item.id + " with name: " + item.subname);
                    customerError = true;
                }
                else
                {
                    possibleCustomerItem.email = item.subemail;
                }
                possibleCustomerItem.knowledge_id = (int)KnowledgeLookupEnum.Sync;
                possibleCustomerItem.is_called = false;
                possibleCustomerItem.calles_count = 0;
                possibleCustomerItem.is_subscribed = false;
                possibleCustomerItem.is_hidden = false;
                possibleCustomerItem.addition_type_id = (int)AddtionalLookupEnum.Sync;
                possibleCustomerItem.added_By_id = currentUser.id;
                possibleCustomerItem.creaation_date = DateTime.Now;

                if (!customerError)
                {
                    db.possibleCustomers.Add(possibleCustomerItem);
                    db.SaveChanges();
                }
            }
            return errors;
        }
        public List<String> insertExcelToCustomers(HttpPostedFile postedFile, string user_name, string password)
        {
            using (var db = new SmartGymSalesEntities())
            {
                UtilsService US = new UtilsService();
                UsersService userService = new UsersService();
                UserRolesService userRolesService = new UserRolesService();


                User currentUser = userService.GetUserbyUser_name(user_name);
                List<String> errors = new List<string>();
                if (!userService.checkUserCred(user_name, password))
                {
                    errors.Add("please Login and try again");
                    return errors;
                }
                if (!userRolesService.isUserManger(user_name))
                {
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
                    SalesCustomer newCustomer = new SalesCustomer();
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
                            else if (US.checkPhoneNumberRedundancy(row[column].ToString()))
                            {
                                errors.Add("Mobile Phone is already found at row : " + rowIndex);
                                customerError = true;
                            }
                            else
                            {
                                newCustomer.mobile = row[column].ToString();
                            }
                        }
                        if (column.ColumnName == customerSheetHeadersEnum.Email.ToDescriptionString())
                        {
                            if (String.IsNullOrEmpty(row[column].ToString()))
                            {
                                errors.Add("Email is Empty at row : " + rowIndex);
                            }
                            else if (!String.IsNullOrEmpty(row[column].ToString()) && !US.checkEmailVaildaty(row[column].ToString()))
                            {
                                errors.Add("Email is not correct at row : " + rowIndex);
                                customerError = true;
                            }
                            else if (!String.IsNullOrEmpty(row[column].ToString()) && US.checkEmailRedundancy(row[column].ToString()))
                            {
                                errors.Add("Email is already found at row : " + rowIndex);
                                customerError = true;
                            }
                            else
                            {
                                newCustomer.email = row[column].ToString();
                            }
                        }
                        if (column.ColumnName == customerSheetHeadersEnum.discont_percentage.ToDescriptionString())
                        {
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
                        if (column.ColumnName == customerSheetHeadersEnum.StartDate.ToDescriptionString())
                        {
                            DateTime startDate = new DateTime();
                            bool sucess = DateTime.TryParse(row[column].ToString(), out startDate);
                            if (!sucess)
                            {
                                errors.Add("Couldn't convert start date at row : " + rowIndex);
                                customerError = true;
                            }
                            else
                            {
                                newCustomer.subscription_start_date = startDate;
                            }
                        }


                        if (column.ColumnName == customerSheetHeadersEnum.EndDate.ToDescriptionString())
                        {
                            DateTime endDate = new DateTime();
                            bool sucess = DateTime.TryParse(row[column].ToString(), out endDate);
                            if (!sucess)
                            {
                                errors.Add("Couldn't convert end date at row : " + rowIndex);
                                customerError = true;
                            }
                            else
                            {
                                if (newCustomer.subscription_start_date != null && newCustomer.subscription_start_date > endDate)
                                {
                                    errors.Add("End Date can't be earlier than start date at row : " + rowIndex);
                                    customerError = true;
                                }
                                else
                                {
                                    newCustomer.subscription_end_date = endDate;
                                }
                            }
                        }
                    }

                    #region fill cutomerData
                    newCustomer.added_By_id = currentUser.id;
                    newCustomer.addition_type_id = (int)enums.AddtionalLookupEnum.Sheet;
                    newCustomer.is_called = false;
                    newCustomer.calles_count = 0;
                    newCustomer.men_forign_Key = null;
                    newCustomer.women_forign_key = null;
                    newCustomer.creation_date = DateTime.Now;
                    if (newCustomer.subscription_start_date < DateTime.Now &&
                        DateTime.Now < newCustomer.subscription_end_date)
                    {
                        newCustomer.is_active = true;
                    }
                    else
                    {
                        newCustomer.is_active = false;
                    }

                    #endregion


                    if (!customerError)
                    {
                        db.SalesCustomers.Add(newCustomer);

                    }
                }
                if (errors.Count == 0)
                {
                    db.SaveChanges();
                }
                return errors;
            }
        }

        public List<SalesCustomer> getAllCustomers(string user_name, string password, string name, string mobile, string email, int? source, bool? isCalled, bool? isSubscriped)
        {

            var db = new SmartGymSalesEntities();
            UsersService userService = new UsersService();
            UserRolesService userRolesService = new UserRolesService();
            User currentUser = userService.GetUserbyUser_name(user_name);
            if (!userService.checkUserCred(user_name, password))
            {
                return null;
            }
            if (!userRolesService.isUserManger(user_name) && !userRolesService.isUserSales(user_name))
            {
                return null;
            }
            List<SalesCustomer> result = db.SalesCustomers.ToList();

            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(x => x.name.ToLower().Contains(name.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(mobile))
            {
                result = result.Where(x => x.mobile.ToLower().Contains(mobile.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(email))
            {
                result = result.Where(x => x.email.ToLower().Contains(email.ToLower())).ToList();
            }
            if (source.HasValue)
            {
                result = result.Where(x => x.addition_type_id == source.Value).ToList();
            }
            if (isCalled.HasValue)
            {
                result = result.Where(x => x.is_called == isCalled.Value).ToList();
            }
            if (isSubscriped.HasValue)
            {
                result = result.Where(x => x.is_active == isSubscriped.Value).ToList();
            }
            return result;

        }
    }
}