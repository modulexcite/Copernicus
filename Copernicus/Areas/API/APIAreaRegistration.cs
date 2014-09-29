using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;
using System.Web.Routing;

namespace Copernicus.Areas.API
{
    /// <summary>
    /// API Area registration
    /// </summary>
    public class APIAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets the name of the area to register.
        /// </summary>
        /// <returns>The name of the area to register.</returns>
        public override string AreaName
        {
            get
            {
                return "API";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">
        /// Encapsulates the information that is required in order to register the area.
        /// </param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            Utilities.IoC.Manager.Bootstrapper.Resolve<Ironman.Core.API.Manager.Manager>().RegisterRoutes(context.Routes, "API", "API");

            context.MapRoute(
                "API_default",
                "API/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}