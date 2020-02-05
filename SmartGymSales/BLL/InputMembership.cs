using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymSales.BLL
{
    public class InputMembership
    {
        public int ID { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public Nullable<int> Category_ID { get; set; }
        public Nullable<int> offer_id { get; set; }
        public string Cardid { get; set; }
        public Nullable<decimal> Subscripcost { get; set; }
        public Nullable<decimal> moneypaied { get; set; }
        public Nullable<decimal> moneyremind { get; set; }
        public Nullable<decimal> deduct { get; set; }
        public Nullable<int> subscriptype { get; set; }
        public Nullable<int> subscrippariod { get; set; }
        public System.DateTime S_Date { get; set; }
        public System.DateTime E_Date { get; set; }
        public Nullable<int> userstuffid { get; set; }
        public Nullable<int> recordstatus { get; set; }
        public Nullable<System.DateTime> recorddate { get; set; }
        public Nullable<System.TimeSpan> recordtime { get; set; }
    }
}