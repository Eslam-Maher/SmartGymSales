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
using System.Net.Http.Headers;

namespace SmartGymSales.Controllers
{
    public class UserRolesController : ApiController
    {

        // GET: api/UserRoles
        [HttpGet]
        [ActionName("getAllUserRoles")]
        public IQueryable<UserRole> GetUserRoles()
        {
            UserRolesService userRolesService = new UserRolesService();
            return userRolesService.GetUserRoles();
        }

   
        // POST: api/UserRoles
        [ResponseType(typeof(UserRole))]
        [HttpPost]
        [ActionName("insertUserRole")]
        public IHttpActionResult PostUserRole(UserRole userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            HttpRequestHeaders headers = this.Request.Headers;
            string userName = string.Empty;
            string pwd = string.Empty;
            if (headers.Contains("userName"))
            {
                userName = headers.GetValues("userName").First();
            }
            if (headers.Contains("Password"))
            {
                pwd = headers.GetValues("Password").First();
            }
            UserRolesService userRolesService = new UserRolesService();
            bool isInserted = userRolesService.insertUserRole(userRole, userName,pwd);

            return Ok(isInserted);
        }

        // DELETE: api/UserRoles/5
        [ResponseType(typeof(UserRole))]
        [HttpDelete]
        [ActionName("deleteUserRole")]
        public IHttpActionResult DeleteUserRole(int id)
        {
            HttpRequestHeaders headers = this.Request.Headers;
            string userName = string.Empty;
            string pwd = string.Empty;
            if (headers.Contains("userName"))
            {
                userName = headers.GetValues("userName").First();
            }
            if (headers.Contains("Password"))
            {
                pwd = headers.GetValues("Password").First();
            }
            UserRolesService userRolesService = new UserRolesService();
            bool isDeleted = userRolesService.deleteUserRole(id, userName, pwd);
            return Ok(isDeleted);
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