using RentalCarWeb.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RentalCarWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static SqlConnection conn;
        protected void Application_Start()
        {
            InitializeDb initializeDb = new InitializeDb();
            conn = initializeDb.getConnection();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
