﻿using System;
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
    public class UsersController : ApiController
    {

        // GET: api/Users
        [HttpGet]
        [ActionName("getUsers")]
        public List<User> GetUsers()
        {
            UsersService UsersService = new UsersService();
            return UsersService.getAllUsers();
        }

        [HttpGet]
        [ActionName("getSalesUsers")]
        public List<User> getSalesUsers()
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
            UsersService UsersService = new UsersService();
            return UsersService.getAllSalesUsers(userName, pwd);
        }

        [HttpPost]
        [ActionName("login")]
        public User checkCredentials(User user ) {
            UsersService UsersService = new UsersService();
            return UsersService.checkCredentials(user);
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
            UsersService UsersService = new UsersService();
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
                UsersService UsersService = new UsersService();
                bool isDeleted =UsersService.deleteUser(id);
                return Ok(isDeleted);

            }
            catch {
                return Ok(false);  
            }
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