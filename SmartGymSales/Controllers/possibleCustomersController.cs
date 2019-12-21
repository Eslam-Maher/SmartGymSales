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
    public class possibleCustomersController : ApiController
    {
        [HttpGet]
        [ActionName("GetPossibleCustomers")]
        // GET: api/possibleCustomers
        public List<possibleCustomer> GetpossibleCustomers()
        {
            PossibleCustomersService PS_Service = new PossibleCustomersService();
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
            return PS_Service.getAllCustomerService(userName, pwd).ToList();
        }

        // GET: api/possibleCustomers/5
        [ResponseType(typeof(possibleCustomer))]
        public IHttpActionResult GetpossibleCustomer(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            possibleCustomer possibleCustomer = db.possibleCustomers.Find(id);
            if (possibleCustomer == null)
            {
                return NotFound();
            }

            return Ok(possibleCustomer);
        }

        // PUT: api/possibleCustomers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutpossibleCustomer(int id, possibleCustomer possibleCustomer)
        {
            PossibleCustomersService PS_Service = new PossibleCustomersService();

            SmartGymSalesEntities db = new SmartGymSalesEntities();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != possibleCustomer.id)
            {
                return BadRequest();
            }

            db.Entry(possibleCustomer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PS_Service.possibleCustomerExists(id))
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

        // POST: api/possibleCustomers
        [HttpPost]
        [ActionName("InsertPossibleCustomer")]
        [ResponseType(typeof(possibleCustomer))]
        public IHttpActionResult PostpossibleCustomer(possibleCustomer possibleCustomer)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            PossibleCustomersService PS_Service = new PossibleCustomersService();
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

            return Ok(PS_Service.insertPossibleCustomer(userName, pwd, possibleCustomer));

        }

        // DELETE: api/possibleCustomers/5
        [ResponseType(typeof(possibleCustomer))]
        public IHttpActionResult DeletepossibleCustomer(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            possibleCustomer possibleCustomer = db.possibleCustomers.Find(id);
            if (possibleCustomer == null)
            {
                return NotFound();
            }

            db.possibleCustomers.Remove(possibleCustomer);
            db.SaveChanges();

            return Ok(possibleCustomer);
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