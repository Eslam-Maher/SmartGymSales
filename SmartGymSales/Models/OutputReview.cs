using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymSales.Models
{
    public class OutputReview
    {
        public int id { get; set; }
        public int training { get; set; }
        public int reciption { get; set; }
        public int general { get; set; }
        public string comment { get; set; }
        public System.DateTime creation_date { get; set; }
        public int parent_id { get; set; }
        public string parent_type { get; set; }
        public string parent_name { get; set; }

    }
}