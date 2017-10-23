
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IdeoTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
      
        protected void Application_Start()
        {
            ModelContext context = new ModelContext();
                if (context.Nodes.Count() == 0)
                {
                    context.Nodes.Add(
                     new Node { Name = "System", NodeId = 0, ParentId=1, Kind="Root" }
                    );
                context.SaveChanges();
                }
          
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
