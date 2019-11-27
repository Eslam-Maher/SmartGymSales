using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SmartGymSales.enums
{
    public enum customerSheetHeadersEnum
    {
        [Description("Name")]
            Name,
        [Description("Mobile")]
        Mobile,
        [Description("Email")]
        Email,
        [Description("Subscription Start Date")]
         StartDate,
        [Description("Subscription End Date")]
        EndDate,


    }
}