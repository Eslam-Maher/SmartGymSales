using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartGymSales.Models
{
    public class OutputCommission
    {
       public double inputIncome { get; set; }
        public int totalRequiredIncome { get; set; }
        public int totalCountCustomersCalled { get; set; }
        public int totalCountCustomerSubscriped { get; set; }
        public double commission { get; set; }
    }
}