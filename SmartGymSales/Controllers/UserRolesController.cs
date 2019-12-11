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
        private SmartGymSalesEntities db = new SmartGymSalesEntities();
        private UserRolesService userRolesService = new UserRolesService();

        // GET: api/UserRoles
        [HttpGet]
        [ActionName("getAllUserRoles")]
        public IQueryable<UserRole> GetUserRoles()
        {
            return userRolesService.GetUserRoles();
        }

        // GET: api/UserRoles/5
        [ResponseType(typeof(UserRole))]
        public IHttpActionResult GetUserRole(int id)
        {
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return NotFound();
            }

            return Ok(userRole);
        }

        // PUT: api/UserRoles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserRole(int id, UserRole userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRole.id)
            {
                return BadRequest();
            }

            db.Entry(userRole).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
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
            bool isDeleted = userRolesService.deleteUserRole(id, userName, pwd);
            return Ok(isDeleted);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserRoleExists(int id)
        {
            return db.UserRoles.Count(e => e.id == id) > 0;
        }
    }
}