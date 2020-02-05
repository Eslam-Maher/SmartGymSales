using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartGymSales.Models;
using SmartGymSales.enums;
using System.Data.Entity.Migrations;
using SmartGymSales.Models.SmartGymMen;

namespace SmartGymSales.Services
{
    public class PossibleCustomersService
    {
        public List<possibleCustomer> getAllCustomerService(string user_name, string password)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
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
            return  db.possibleCustomers.Where(x=>x.is_hidden==false).ToList();
        }
        public void UpdatePossibleCustomersCalledByUser(User user) {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            List<possibleCustomer> PCList= db.possibleCustomers.Where(x => x.is_called_by ==user.id).ToList();
            foreach (possibleCustomer element in PCList) {
                element.is_called_by = null;
                db.possibleCustomers.AddOrUpdate(element);
            }
            db.SaveChanges();
        }

        public bool possibleCustomerExists(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.possibleCustomers.Count(e => e.id == id) > 0;
        }
        public possibleCustomer getPossibleCustomerById(int id) {
            if (possibleCustomerExists(id)) {
                SmartGymSalesEntities db = new SmartGymSalesEntities();

                return db.possibleCustomers.Where(x => x.id == id).FirstOrDefault();
            }
            return null;
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

                    List<Object> toBeInsertedPossibleCustomers = new List<Object>();
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
                    Models.SmartGymWomen.SmatGymWomenEntities sourceDb = new Models.SmartGymWomen.SmatGymWomenEntities();
                    List<Models.SmartGymWomen.T_session_subscriber> sourceCustomers = sourceDb.T_session_subscriber.Where(x => !String.IsNullOrEmpty(x.sunmobil)).ToList();

                    List<Object> toBeInsertedPossibleCustomers = new List<Object>();
                    foreach (Models.SmartGymWomen.T_session_subscriber element in sourceCustomers)
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
                else
                {
                    errors.Add("Failed to identify source to update from");
                    return errors;
                }


                return errors;

            }
        }


        private List<String> InsertIntoPossibleCustomersFromMenDb(List<Object> sourceList, User currentUser)
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
        public List<String> insertPossibleCustomer(string userName, string pwd, possibleCustomer possibleCustomer)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            List<String> errors = new List<string>();
            UsersService userService = new UsersService();
            UserRolesService userRolesService = new UserRolesService();

            User currentUser = userService.GetUserbyUser_name(userName);
            if (!userService.checkUserCred(userName, pwd))
            {
                errors.Add("please Login and try again");
                return errors;
            }
            if (!userRolesService.isUserSales(userName))
            {
                errors.Add("You are not authorized");
                return errors;
            }


            UtilsService US = new UtilsService();
            bool customerError = false;
            //name
            if (String.IsNullOrEmpty(possibleCustomer.name))
            {
                errors.Add("Name field is empty");
                customerError = true;
            }

            //mobile
            if (String.IsNullOrEmpty(possibleCustomer.mobile))
            {
                errors.Add("Mobile field is empty");
                customerError = true;
            }
            else if (!US.checkPhoneNumberVaildaty(possibleCustomer.mobile))
            {
                errors.Add("Mobile field is not valid");
                customerError = true;
            }
            else if (US.checkPhoneNumberRedundancyforPossibleCustomers(possibleCustomer.mobile))
            {
                errors.Add("Mobile field is found before");
                customerError = true;
            }
            //email
            if (!String.IsNullOrEmpty(possibleCustomer.email) && !US.checkEmailVaildaty(possibleCustomer.email))
            {
                errors.Add("Email field is not valid");
                customerError = true;
            }
            else if (!String.IsNullOrEmpty(possibleCustomer.email) && US.checkEmailRedundancyforPossibleCustomers(possibleCustomer.email))
            {
                errors.Add("Email field is found before");
                customerError = true;
            }
            
            
            possibleCustomer.is_called = false;
            possibleCustomer.calles_count = 0;
            possibleCustomer.is_subscribed = false;
            possibleCustomer.is_hidden = false;
            possibleCustomer.addition_type_id = (int)AddtionalLookupEnum.Manual;
            possibleCustomer.added_By_id = currentUser.id;
            possibleCustomer.is_called_by = currentUser.id;
            possibleCustomer.creaation_date = DateTime.Now;

            if (!customerError)
            {
                db.possibleCustomers.Add(possibleCustomer);
                db.SaveChanges();
            }
            return errors;
        }
    }
}