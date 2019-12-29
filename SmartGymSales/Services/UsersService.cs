using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartGymSales.Models;


namespace SmartGymSales.Services
{
    public class UsersService
    {


        public List<User> getAllUsers()
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            return db.Users.ToList();

        }
        public User GetUserbyId(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            return db.Users.Find(id);
        }

        public bool deleteUser(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            UserRolesService userRoleService = new UserRolesService();
            try
            {
                User user = db.Users.Find(id);
                if (user == null || userRoleService.GetUserRoleByUserId(id).Count>0)
                {
                    return false;
                }

                PossibleCustomersService pcService = new PossibleCustomersService();
                CustomerService scService = new CustomerService();
                pcService.UpdatePossibleCustomersCalledByUser(user);
                scService.UpdateCustomersCalledByUser(user);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public bool insertUsers(User user) {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            if (db.Users.Where(e => e.user_name == user.user_name || e.user_name.ToLower()== user.user_name.ToLower()).Any()) {
                return false;
            }
            if (String.IsNullOrEmpty(user.name) || String.IsNullOrEmpty(user.user_name) || String.IsNullOrEmpty(user.password)) {

                return false;
            }
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        public bool UserExists(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            return db.Users.Count(e => e.id == id) > 0;
        }

        public User GetUserbyUser_name(string user_name)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            return db.Users.Where(x=> x.user_name.ToLower()== user_name.ToLower()).FirstOrDefault();
        }
        public bool checkUserCred(string user_name, string password) {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            return db.Users.Where(x => x.user_name.ToLower() == user_name.ToLower()
            &&x.password==password).Any();

        }

        public User checkCredentials(User user)
        {
            bool isValid = false;
            if (string.IsNullOrEmpty(user.user_name) || string.IsNullOrEmpty(user.password))
            {
                return null;
            }
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            isValid = db.Users.Where(e => e.user_name == user.user_name && e.password == user.password).Any();
            User foundUser = db.Users.Where(e => e.user_name == user.user_name && e.password == user.password).FirstOrDefault();
            return isValid ? foundUser : null;
        }
    }



}