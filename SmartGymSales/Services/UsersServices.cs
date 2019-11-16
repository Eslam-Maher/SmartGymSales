using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartGymSales.Models;


namespace SmartGymSales.Services
{
    public class UsersServices
    {
        private SmartGymSalesEntities db = new SmartGymSalesEntities();

        public List<User> getAllUsers() {
            return db.Users.ToList();

        }
    }
}