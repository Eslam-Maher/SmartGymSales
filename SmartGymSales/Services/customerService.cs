using AutoMapper;
using OfficeOpenXml;
using SmartGymSales.BLL;
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
using SmartGymWomenRetrival.Models;
using SmartGymWomenRetrival.Services;

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
                    List<InputCustomer> sourceCustomers = new List<InputCustomer>();
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<Models.SmartGymMen.Customer, InputCustomer>();
                    });
                    IMapper iMapper = config.CreateMapper();
                    iMapper.Map(sourceDb.Customers.Where(x => !String.IsNullOrEmpty(x.Phone_mobile)).ToList(), sourceCustomers);

                    List <InputCustomer> toBeUpdatedCustomers = new List<InputCustomer>();
                    List<InputCustomer> toBeInsertedCustomers = new List<InputCustomer>();
                    foreach (InputCustomer element in sourceCustomers)
                    {
                        if (US.checkPhoneNumberVaildaty(element.Phone_mobile))
                        {
                            if (db.SalesCustomers.Where(salesCusElement => salesCusElement.men_forign_Key == element.Customer_ID).Count() >= 1)
                            {
                                toBeUpdatedCustomers.Add(element);
                            }
                            else
                            {
                                toBeInsertedCustomers.Add(element);
                            }
                        }
                    }
                    updateCustomersFromMenDb(toBeUpdatedCustomers, currentUser, sourceDbString);
                    List<String> insertResult = InsertIntoCustomersFromMenDb(toBeInsertedCustomers, currentUser, sourceDbString);
                    errors.AddRange(insertResult);

                    return errors;
                }
                else if (sourceDbString == "Women")
                {
                    var config = new MapperConfiguration(cfg => {
                        cfg.CreateMap<SmartGymWomenRetrival.Models.Customer, InputCustomer>();
                    });
                    IMapper iMapper = config.CreateMapper();

                    //List<Models.SmartGymWomen.Customer> sourceCustomers = sourceDb.Customers.Where(x => !String.IsNullOrEmpty(x.Phone_mobile)).ToList();
                    List<InputCustomer> sourceCustomers = new List<InputCustomer>();
                    DbService WomenDb= new DbService();
                    iMapper.Map(WomenDb.GetAllWomenCustomers().Where(x => !String.IsNullOrEmpty(x.Phone_mobile)).ToList(), sourceCustomers);



                    List<InputCustomer> toBeUpdatedCustomers = new List<InputCustomer>();
                    List<InputCustomer> toBeInsertedCustomers = new List<InputCustomer>();
                    foreach (InputCustomer element in sourceCustomers)
                    {
                        if (US.checkPhoneNumberVaildaty(element.Phone_mobile))
                        {
                            if (db.SalesCustomers.Where(salesCusElement => salesCusElement.women_forign_key == element.Customer_ID).Count() >= 1)
                            {
                                toBeUpdatedCustomers.Add(element);
                            }
                            else
                            {
                                toBeInsertedCustomers.Add(element);
                            }
                        }
                    }
                    updateCustomersFromMenDb(toBeUpdatedCustomers, currentUser, sourceDbString);
                    List<String> insertResult = InsertIntoCustomersFromMenDb(toBeInsertedCustomers, currentUser, sourceDbString);
                    errors.AddRange(insertResult);
                    return errors;


                }
                else
                {
                    errors.Add("Failed to identify source to update from");
                    return errors;
                }
               
            }

        }
        private void updateCustomersFromMenDb(List<InputCustomer> sourceList, User currentUser, string sourceDbString)
        {
            using (var db = new SmartGymSalesEntities())
            {
                foreach (InputCustomer srcCustomer in sourceList)
                {
                    SalesCustomer salesCustomer;
                    InputMembership srcCustomerMembership=new InputMembership();
                    if (sourceDbString == "Men") {
                        SmartGymMenEntities sourceDb = new SmartGymMenEntities();
                        Models.SmartGymMen.Membership CustomerMembership = sourceDb.Memberships.Where(x => x.Customer_ID == srcCustomer.Customer_ID).FirstOrDefault();
                        var config = new MapperConfiguration(cfg => {
                            cfg.CreateMap<Models.SmartGymMen.Membership, InputMembership>();
                        });
                        IMapper iMapper = config.CreateMapper();
                        iMapper.Map(CustomerMembership, srcCustomerMembership);
                        
                        salesCustomer = db.SalesCustomers.Where(x => x.men_forign_Key == srcCustomer.Customer_ID).FirstOrDefault();
                    }
                    else if (sourceDbString == "Women") {
                        salesCustomer = db.SalesCustomers.Where(x => x.women_forign_key == srcCustomer.Customer_ID).FirstOrDefault();
                        DbService WomenDb = new DbService();
                        SmartGymWomenRetrival.Models.Membership CustomerMembership = WomenDb.GetAllWomenMembership().Where(x => x.Customer_ID == srcCustomer.Customer_ID).FirstOrDefault();
                        var config = new MapperConfiguration(cfg => {
                            cfg.CreateMap<SmartGymWomenRetrival.Models.Membership, InputMembership>();
                        });
                        IMapper iMapper = config.CreateMapper();
                        iMapper.Map(CustomerMembership, srcCustomerMembership);
                    }
                    
                    else
                    {
                        return ;
                    }

                    //isActive and SubscribtionDate
                    if (srcCustomerMembership == null | srcCustomerMembership.S_Date == DateTime.MinValue || srcCustomerMembership.S_Date == DateTime.MaxValue
                        || srcCustomerMembership.E_Date == DateTime.MinValue || srcCustomerMembership.E_Date == DateTime.MaxValue)
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
       
        private List<String> InsertIntoCustomersFromMenDb(List<InputCustomer> sourceList, User currentUser,String sourceDbString)
        {
            using (var db = new SmartGymSalesEntities())
            {
                UtilsService US = new UtilsService();
                SmartGymMenEntities sourceDb = new SmartGymMenEntities();

                List<String> errors = new List<String>();
                int rowError = 0;
                foreach (InputCustomer srcCustomer in sourceList)
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

                    InputMembership srcCustomerMembership = new InputMembership();
                    if (sourceDbString == "Men")
                    {


                        Models.SmartGymMen.Membership CustomerMembership = sourceDb.Memberships.Where(x => x.Customer_ID == srcCustomer.Customer_ID).OrderByDescending(x => x.recorddate).FirstOrDefault();
                        var config = new MapperConfiguration(cfg => {
                            cfg.CreateMap<Models.SmartGymMen.Membership, InputMembership>();
                        });
                        IMapper iMapper = config.CreateMapper();
                        iMapper.Map(CustomerMembership, srcCustomerMembership);
                        newCustomer.men_forign_Key = srcCustomer.Customer_ID;
                    }
                    else if (sourceDbString == "Women")
                    {
                        DbService WomenDb = new DbService();
                        SmartGymWomenRetrival.Models.Membership CustomerMembership = WomenDb.GetAllWomenMembership().Where(x => x.Customer_ID == srcCustomer.Customer_ID).OrderByDescending(x => x.recorddate).FirstOrDefault();
                        var config = new MapperConfiguration(cfg => {
                            cfg.CreateMap<SmartGymWomenRetrival.Models.Membership, InputMembership>();
                        });
                        IMapper iMapper = config.CreateMapper();
                        iMapper.Map(CustomerMembership, srcCustomerMembership);
                        newCustomer.women_forign_key = srcCustomer.Customer_ID;

                    }
                    //isActive and SubscribtionDate
                    if (srcCustomerMembership == null || srcCustomerMembership.S_Date==DateTime.MinValue || srcCustomerMembership.S_Date == DateTime.MaxValue
                        || srcCustomerMembership.E_Date == DateTime.MinValue || srcCustomerMembership.E_Date == DateTime.MaxValue)
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
                    
                    newCustomer.creation_date = DateTime.Now;
              


                    if (!customerError)
                    {
                        db.SalesCustomers.Add(newCustomer);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            rowError++;
                            Console.Out.WriteLine(e.InnerException);
                        }

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
           

                return errors;
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