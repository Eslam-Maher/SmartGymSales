using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymSales.BLL
{
    public class InputPossibleCustomer
    {
        public int id { get; set; }
        public string subname { get; set; }
        public string subemail { get; set; }
        public string sunmobil { get; set; }
        public string subidentity { get; set; }
        public Nullable<System.DateTime> sessiondate { get; set; }
        public Nullable<int> sessionactivity { get; set; }
        public Nullable<decimal> sessioncust { get; set; }
        public Nullable<int> userstuffid { get; set; }
        public Nullable<int> recordstatus { get; set; }
        public Nullable<System.DateTime> recorddate { get; set; }
        public Nullable<System.TimeSpan> recordtime { get; set; }
    }
}