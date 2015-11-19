using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BeerFinder
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            String DatabasePath = Server.MapPath(@"~\App_Data\BeerDatabase.mdf");
            Session["Database"] = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='" + DatabasePath + "'; Integrated Security=true; Max Pool Size=1024; Pooling=true;";
        }
    }
}
