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
    }
}