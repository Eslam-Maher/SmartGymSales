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
    public class CommissionsController : ApiController
    {

        // GET: api/Commissions
        [HttpGet]
        [ActionName("getAllCommissions")]
        public List<commission> Getcommissions()
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
            CommissionService commService = new CommissionService();
            return commService.getAllCommissions(userName,pwd);
        }

        // POST: api/possibleCustomers
        [HttpPost]
        [ActionName("insertCommission")]
        public IHttpActionResult Postcommission(commission commission)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
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
            CommissionService commService = new CommissionService();
            return Ok(commService.insertCommission(userName, pwd,commission));
        }


        [HttpPost]
        [ActionName("calcCommission")]
        public IHttpActionResult calcCommission(HttpRequestMessage request, [FromBody]InputCommission data)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
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
            CommissionService commService = new CommissionService();
            return Ok(commService.calcCommission(userName, pwd, data.dateFrom.AddDays(1), data.dateTo.AddDays(1), data.user));
        }


        // DELETE: api/Commissions/5
        [HttpDelete]
        [ActionName("deleteCommission")]
        public IHttpActionResult Deletecommission(int id)
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
            CommissionService commService = new CommissionService();
            return Ok(commService.deleteCommission(userName, pwd, id));
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

        private bool commissionExists(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.commissions.Count(e => e.id == id) > 0;
        }
    }
}