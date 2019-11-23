using SmartGymSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymSales.Services
{
    public class RolesService
    {
        private SmartGymSalesEntities db = new SmartGymSalesEntities();

        public bool RoleExists(int id)
        {
            return db.Roles.Count(e => e.id == id) > 0;
        }


        public IQueryable<Role> GetRoles()
        {
            return db.Roles;
        }
    }
}