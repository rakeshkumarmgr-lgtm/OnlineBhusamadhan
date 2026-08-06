using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Bhusamadhan.DB
{
    public static class DBConHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["conns"].ConnectionString;
        }
    }
}