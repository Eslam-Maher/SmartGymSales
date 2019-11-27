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
using System.Web;
using OfficeOpenXml;
using SmartGymSales.Services;

namespace SmartGymSales.Controllers
{
    public class CustomersController : ApiController
    {
        private SmartGymSalesEntities db = new SmartGymSalesEntities();

        // GET: api/Customers
        public IQueryable<customer> Getcustomers()
        {
            return db.customers;
        }


        //[ResponseType(typeof(UserRole))]
        [HttpPost]
        [ActionName("updateCustomersFromSheet")]
        public void UploadFile(HttpRequestMessage request)
        {
            HttpContext context = HttpContext.Current;
            HttpPostedFile postedFile = context.Request.Files["file"];
            CustomerService cs = new CustomerService();
            List<String> errors = cs.insertExcelToCustomers(postedFile);
        }

        // GET: api/Customers/5
        [ResponseType(typeof(customer))]
        public IHttpActionResult Getcustomer(int id)
        {
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcustomer(int id, customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.id)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customerExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(customer))]
        public IHttpActionResult Postcustomer(customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.id }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(customer))]
        public IHttpActionResult Deletecustomer(int id)
        {
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool customerExists(int id)
        {
            return db.customers.Count(e => e.id == id) > 0;
        }
    }
}