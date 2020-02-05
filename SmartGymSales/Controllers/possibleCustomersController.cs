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

        // POST: api/possibleCustomers
        [HttpPost]
        [ActionName("InsertPossibleCustomer")]
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


        [HttpPost]
        [ActionName("updatePossibleCustomersFromDb")]
        public IHttpActionResult updatePossibleCustomerTable(HttpRequestMessage request, [FromBody] String dbType)
        {
            HttpRequestHeaders headers = request.Headers;
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
            PossibleCustomersService pcs = new PossibleCustomersService();
            List<String> errors = pcs.UpdatePossibleCustomerFromdb(userName, pwd, dbType);
            return Ok(errors);

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