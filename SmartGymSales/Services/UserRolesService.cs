using SmartGymSales.enums;
using SmartGymSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymSales.Services
{
    public class UserRolesService
    {
        private SmartGymSalesEntities db = new SmartGymSalesEntities();
        private UsersService userService = new UsersService();

        private bool UserRoleExists(int id)
        {
            return db.UserRoles.Count(e => e.id == id) > 0;
        }
        public bool isUserAdmin(string User_name) {
            User user = userService.GetUserbyUser_name(User_name);
            return user.UserRoles.Where(x => x.role_id == (int)rolesEnum.admin).Any();

        }
        public bool isUserManger(string User_name)
        {
            User user = userService.GetUserbyUser_name(User_name);
            return user.UserRoles.Where(x => x.role_id == (int)rolesEnum.manger).Any();

        }
        public bool isUserSales(string User_name)
        {
            User user = userService.GetUserbyUser_name(User_name);
            return user.UserRoles.Where(x => x.role_id == (int)rolesEnum.sales).Any();

        }
        public List<UserRole> GetUserRoleByUserId(int id) {
            User user=userService.GetUserbyId(id);

            List<UserRole> userRoles = db.UserRoles.Where(e=>e.user_id== user.id).ToList();

            return userRoles;


        }
        public IQueryable<UserRole> GetUserRoles()
        {
            return db.UserRoles;
        }

        public bool insertUserRole(UserRole userRole,string userName, string password) {

            UsersService userService = new UsersService();
            UserRolesService userRolesService = new UserRolesService();

            if (!userService.checkUserCred(userName, password))
            {
                return false;
            }
            if (!userRolesService.isUserAdmin(userName))
            {
                return false;
            }

            if (db.UserRoles.Count(e => e.user_id == userRole.user_id && e.role_id == userRole.role_id) > 0)
            {
                return false;
            }
            else {
                db.UserRoles.Add(userRole);
                db.SaveChanges();
                return true;
            }
        }

        public bool deleteUserRole(int id, string userName, string password)
        {
            UsersService userService = new UsersService();
            UserRolesService userRolesService = new UserRolesService();

            if (!userService.checkUserCred(userName, password))
            {
                return false;
            }
            if (!userRolesService.isUserAdmin(userName))
            {
                return false;
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null) {
                return false;
            }
            List<UserRole> AdminUserRoles = db.UserRoles.Where(e => e.role_id == (int)rolesEnum.admin).ToList();
            if (AdminUserRoles.Contains(userRole) && AdminUserRoles.Count == 1)
            {
                return false;
            }
            else {
                db.UserRoles.Remove(userRole);
                db.SaveChanges();
                return true;
            }
        }
    }
}