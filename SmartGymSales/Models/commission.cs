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
    
    public partial class commission
    {
        public int id { get; set; }
        public double new_customer_percentatge { get; set; }
        public double old_customer_percentatge { get; set; }
        public Nullable<int> target { get; set; }
        public Nullable<bool> is_hidden { get; set; }
    }
}
