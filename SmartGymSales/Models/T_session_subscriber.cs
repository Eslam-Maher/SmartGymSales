//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartGymSales.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_session_subscriber
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