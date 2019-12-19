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
using System.Runtime.Remoting.Messaging;
using System.IO;
using System.Net.Http.Headers;

namespace SmartGymSales.Controllers
{
    public class CustomersController : ApiController
    {

        // GET: api/Customers
        [HttpGet]
        [ActionName("getAllCustomers")]
        public List<SalesCustomer> Getcustomers()
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
            CustomerService cs = new CustomerService();
            List< SalesCustomer> tmep = cs.getAllCustomers(userName, pwd);
            return tmep;
        }


        //[ResponseType(typeof(UserRole))]
        [HttpPost]
        [ActionName("updateCustomersFromSheet")]
        public IHttpActionResult UploadFile(HttpRequestMessage request)
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
            HttpContext context = HttpContext.Current;
            HttpPostedFile postedFile = context.Request.Files["file"];
            CustomerService cs = new CustomerService();
            List<String> errors = cs.insertExcelToCustomers(postedFile, userName, pwd);  
            return Ok(errors) ;
        }


        [HttpGet]
        [ActionName("downloadCustomersFile")]
        public HttpResponseMessage downloadCustomerExcelTemplate()
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

            var response = new HttpResponseMessage();
            UsersService userService = new UsersService();
            UserRolesService userRolesService = new UserRolesService();

            if (!userService.checkUserCred(userName, pwd))
            {
                return response;
            }
            if (!userRolesService.isUserManger(userName))
            {
                return response;
            }
            //Create the file in Web App Physical Folder
            string fileName = "Customers Sheet.xlsx";
            string filePath = HttpContext.Current.Server.MapPath(String.Format("~/Excels/{0}", fileName));
            //Check whether File exists.
            if (!File.Exists(filePath))
            {
                //Throw 404 (Not Found) exception if File not found.
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", fileName);
                throw new HttpResponseException(response);
            }



            byte[] bytes = File.ReadAllBytes(filePath);

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the Response Content Length.
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
            return response;

        }

        [HttpPost]
        [ActionName("updateCustomersFromDb")]
        public IHttpActionResult updateSalesCustomerTable(HttpRequestMessage request, [FromBody]String dbType)
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
            CustomerService cs = new CustomerService();
            List<String> errors = cs.UpdateSalesCustomerFromdb(userName, pwd, dbType);
            return Ok(errors);
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
            CustomerService cs = new CustomerService();
            List<String> errors = cs.UpdatePossibleCustomerFromdb(userName, pwd, dbType);
            return Ok(errors);

        }
        // GET: api/Customers/5
        //[ResponseType(typeof(SalesCustomer))]
        //public IHttpActionResult Getcustomer(int id)
        //{
        //    SalesCustomer customer = db.SalesCustomers.Find(id);
        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(customer);
        //}

        //// PUT: api/Customers/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putcustomer(int id, SalesCustomer customer)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != customer.id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(customer).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!customerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

      

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