using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Copernicus
{
    /// <summary>
    /// HTTP Application class
    /// </summary>
    public class MvcApplication : Ironman.Core.BaseClasses.HttpApplicationBase
    {
        /// <summary>
        /// Application start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}