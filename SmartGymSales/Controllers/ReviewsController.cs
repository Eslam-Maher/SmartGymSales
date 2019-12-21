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
    public class ReviewsController : ApiController
    {

        // GET: api/Reviews
        public IQueryable<review> Getreviews()
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.reviews;
        }

        // GET: api/Reviews/5
        [ResponseType(typeof(review))]
        public IHttpActionResult Getreview(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            review review = db.reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }


        // POST: api/Reviews
        [HttpPost]
        [ActionName("insertReview")]
        [ResponseType(typeof(review))]
        public IHttpActionResult Postreview(review review)
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
            SmartGymSalesEntities db = new SmartGymSalesEntities();

            ReviewService reviewService = new ReviewService();

           return Ok(reviewService.insertReview(userName, pwd, review));

        }

        // DELETE: api/Reviews/5
        [ResponseType(typeof(review))]
        public IHttpActionResult Deletereview(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            review review = db.reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            db.reviews.Remove(review);
            db.SaveChanges();

            return Ok(review);
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

        private bool reviewExists(int id)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.reviews.Count(e => e.id == id) > 0;
        }
    }
}