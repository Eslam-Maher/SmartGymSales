using SmartGymSales.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SmartGymSales.Services
{
    public static class UtilsEnumService
    {
        public static string ToDescriptionString(this customerSheetHeadersEnum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string ToDescriptionString(this ParentModalEnum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }


    }
}