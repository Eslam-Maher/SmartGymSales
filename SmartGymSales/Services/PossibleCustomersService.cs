using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartGymSales.Models;

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

        public bool possibleCustomerExists(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.possibleCustomers.Count(e => e.id == id) > 0;
        }
    }
}