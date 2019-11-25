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

        public List<UserRole> GetUserRoleByUserId(int id) {
            User user=userService.GetUserbyId(id);

            List<UserRole> userRoles = db.UserRoles.Where(e=>e.user_id== user.id).ToList();

            return userRoles;


        }
        public IQueryable<UserRole> GetUserRoles()
        {
            return db.UserRoles;
        }

        public bool insertUserRole(UserRole userRole) {
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

        public bool deleteUserRole(int id)
        {
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