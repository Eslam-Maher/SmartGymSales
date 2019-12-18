using SmartGymSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymSales.Services
{
    public class RolesService
    {

        public bool RoleExists(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            return db.Roles.Count(e => e.id == id) > 0;
        }


        public IQueryable<Role> GetRoles()
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            return db.Roles;
        }
    }
}