using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymSales.Services
{
    public class UtilsService
    {
        public bool checkPhoneNumberVaildaty(String number)
        {
            return false;
        }
        public bool checkPhoneNumberRedundancy(String number)
        {
            return false;
        }

        internal bool checkEmailRedundancy(string v)
        {
            throw new NotImplementedException();
        }

        internal bool checkEmailVaildaty(string v)
        {
            throw new NotImplementedException();
        }
    }
}