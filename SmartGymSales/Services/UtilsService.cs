using SmartGymSales.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SmartGymSales.Services
{
    public class UtilsService
    {
        private SmartGymSalesEntities db = new SmartGymSalesEntities();

        public bool checkPhoneNumberVaildaty(String number)
        {
            Regex rx = new Regex(@"(01)[0-9]{9}",RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            // Find matches.
            MatchCollection matches = rx.Matches(number);
            return matches.Count > 0;
        }
        public bool checkPhoneNumberRedundancy(String number)
        {
             return db.customers.Where(x => x.mobile == number).Any();
        }

        internal bool checkEmailRedundancy(string email)
        {
            return db.customers.Where(x=>x.email.ToLower()==email.ToLower()).Any();
        }

        internal bool checkEmailVaildaty(string email)
        {
            return new EmailAddressAttribute().IsValid(email); ;
        }
    }
}