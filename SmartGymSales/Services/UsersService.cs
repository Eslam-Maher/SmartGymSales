using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartGymSales.Models;


namespace SmartGymSales.Services
{
    public class UsersService
    {
        private SmartGymSalesEntities db = new SmartGymSalesEntities();


        public List<User> getAllUsers()
        {
            return db.Users.ToList();

        }
        public User GetUserbyId(int id)
        {
            return db.Users.Find(id);
        }

        public bool deleteUser(int id)
        {
             UserRolesService userRoleService = new UserRolesService();
            try
            {
                User user = db.Users.Find(id);
                if (user == null || userRoleService.GetUserRoleByUserId(id).Count>0)
                {
                    return false;
                }
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
            return db.Users.Count(e => e.id == id) > 0;
        }

        public User GetUserbyUser_name(string user_name)
        {
          return  db.Users.Where(x=> x.user_name.ToLower()== user_name.ToLower()).FirstOrDefault();
        }
        public bool checkUserCred(string user_name, string password) {
            return db.Users.Where(x => x.user_name.ToLower() == user_name.ToLower()
            &&x.password==password).Any();

        }
    }



}