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

namespace SmartGymSales.Controllers
{
    public class knowledgeLookupsController : ApiController
    {

        // GET: api/knowledgeLookups
        [HttpGet]
        [ActionName("GetknowledgeLookups")]
        public IQueryable<knowledgeLookup> GetknowledgeLookups()
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.knowledgeLookups;
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

        private bool knowledgeLookupExists(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.knowledgeLookups.Count(e => e.id == id) > 0;
        }
    }
}