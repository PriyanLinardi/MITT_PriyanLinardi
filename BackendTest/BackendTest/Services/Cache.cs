using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTest
{
    public class Service
    {
        public static string GetUserNameLogin()
        {
            if (HttpContext.Current?.Cache == null) return null;

            return Convert.ToString(HttpContext.Current.Cache["user"]);
        }

        public static void SetUserNameCache(string userName)
        {
            if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                HttpContext.Current.Cache["user"] = userName;
        }

        public static void EmptyUserNameCache()
        {
            if (HttpContext.Current != null && HttpContext.Current.Cache != null)
                HttpContext.Current.Cache["user"] = "";
        }
    }
}