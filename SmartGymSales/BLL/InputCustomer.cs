using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymSales.BLL
{
    public class InputCustomer
    {
        public int Customer_ID { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string Gendar { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Phone_mobile { get; set; }
        public string phone_home { get; set; }
        public string id_type { get; set; }
        public string id_number { get; set; }
        public string cardid { get; set; }
        public Nullable<decimal> hieght { get; set; }
        public Nullable<decimal> wieght { get; set; }
        public string notes { get; set; }
        public string pic_path { get; set; }
        public Nullable<int> block { get; set; }
        public Nullable<int> userstuffid { get; set; }
        public Nullable<int> recordstatus { get; set; }
        public Nullable<System.DateTime> recorddate { get; set; }
        public Nullable<System.TimeSpan> recordtime { get; set; }
    }
}