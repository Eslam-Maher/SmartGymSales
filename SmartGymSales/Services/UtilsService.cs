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

        public bool checkPhoneNumberVaildaty(String number)
        {
            Regex rx = new Regex(@"(01)[0-9]{9}",RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            // Find matches.
            MatchCollection matches = rx.Matches(number);
            return matches.Count > 0;
        }
        public bool checkPhoneNumberRedundancy(String number)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.SalesCustomers.Where(x => x.mobile == number).Any();
        }

        public bool checkPhoneNumberRedundancyforPossibleCustomers(String number)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.possibleCustomers.Where(x => x.mobile.ToString() == number).Any();
        }
        public bool checkEmailRedundancyforPossibleCustomers(string email)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.possibleCustomers.Where(x => x.email.ToLower() == email.ToLower()).Any();
        }

        public bool checkEmailRedundancy(string email)
        {
            SmartGymSalesEntities db = new SmartGymSalesEntities();
            return db.SalesCustomers.Where(x=>x.email.ToLower()==email.ToLower()).Any();
        }

        internal bool checkEmailVaildaty(string email)
        {
            return new EmailAddressAttribute().IsValid(email); ;
        }
    }
}