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
    public class UsersController : ApiController
    {
        private SmartGymSalesEntities db = new SmartGymSalesEntities();
        private UsersService UsersService = new UsersService();

        // GET: api/Users
        [HttpGet]
        [ActionName("getUsers")]
        public List<User> GetUsers()
        {
            return UsersService.getAllUsers();
        }

        [HttpPost]
        [ActionName("login")]
        public User checkCredentials(User user ) {
            bool isValid = false;
            if ( string.IsNullOrEmpty(user.user_name) || string.IsNullOrEmpty(user.password)) {
               return null;
            }
            isValid= db.Users.Where(e => e.user_name == user.user_name && e.password == user.password).Any();
            User foundUser = db.Users.Where(e => e.user_name == user.user_name && e.password == user.password).FirstOrDefault();
            return isValid? foundUser : null;
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        [HttpPost]
        [ActionName("insertUsers")]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isInserted = UsersService.insertUsers(user);

            return Ok(isInserted);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        [HttpDelete]
        [ActionName("deleteUser")]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                bool isDeleted =UsersService.deleteUser(id);
                return Ok(isDeleted);

            }
            catch {
                return Ok(false);  
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.id == id) > 0;
        }
    }
}