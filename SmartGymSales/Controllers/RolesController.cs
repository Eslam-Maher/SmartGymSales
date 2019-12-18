using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SmartGymSales.Models;
using SmartGymSales.Services;

namespace SmartGymSales.Controllers
{
    public class RolesController : ApiController
    {

        // GET: api/Roles
        [HttpGet]
        [ActionName("getAllRoles")]
        public IQueryable<Role> GetRoles()
        {
            RolesService roleService = new RolesService();
          return  roleService.GetRoles();
        }

        protected override void Dispose(bool disposing)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}