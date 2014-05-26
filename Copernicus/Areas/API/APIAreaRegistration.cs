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
            Contract.Requires<ArgumentNullException>(context != null, "context");
            context.MapRoute(
                name: "API_Save",
                url: "API/v1/{ModelName}",
                defaults: new { controller = "API", action = "Save" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST", "PUT", "PATCH") }
            );

            context.MapRoute(
                name: "API_Save2",
                url: "API/v1/{ModelName}/{ID}",
                defaults: new { controller = "API", action = "Save" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST", "PUT", "PATCH") }
            );

            context.MapRoute(
                name: "API_Delete",
                url: "API/v1/{ModelName}/{ID}",
                defaults: new { controller = "API", action = "Delete" },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.MapRoute(
                name: "API_SaveProperty",
                url: "API/v1/{ModelName}/{ID}/{PropertyName}",
                defaults: new { controller = "API", action = "SaveProperty" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST", "PUT", "PATCH") }
            );

            context.MapRoute(
                name: "API_SaveProperty2",
                url: "API/v1/{ModelName}/{ID}/{PropertyName}/{PropertyID}",
                defaults: new { controller = "API", action = "SaveProperty" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST", "PUT", "PATCH") }
            );

            context.MapRoute(
                name: "API_DeleteProperty",
                url: "API/v1/{ModelName}/{ID}/{PropertyName}/{PropertyID}",
                defaults: new { controller = "API", action = "DeleteProperty" },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.MapRoute(
                name: "API_GetProperty",
                url: "API/v1/{ModelName}/{ID}/{PropertyName}",
                defaults: new { controller = "API", action = "GetProperty" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.MapRoute(
                name: "API_Any",
                url: "API/v1/{ModelName}/{ID}",
                defaults: new { controller = "API", action = "Any" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.MapRoute(
                name: "API_All",
                url: "API/v1/{ModelName}",
                defaults: new { controller = "API", action = "All" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.MapRoute(
                name: "API_Save_Ending",
                url: "API/v1/{ModelName}.{ending}",
                defaults: new { controller = "API", action = "Save" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST", "PUT", "PATCH") }
            );

            context.MapRoute(
                name: "API_Save2_Ending",
                url: "API/v1/{ModelName}/{ID}.{ending}",
                defaults: new { controller = "API", action = "Save" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST", "PUT", "PATCH") }
            );

            context.MapRoute(
                name: "API_Delete_Ending",
                url: "API/v1/{ModelName}/{ID}.{ending}",
                defaults: new { controller = "API", action = "Delete" },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.MapRoute(
                name: "API_SaveProperty_Ending",
                url: "API/v1/{ModelName}/{ID}/{PropertyName}.{ending}",
                defaults: new { controller = "API", action = "SaveProperty" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST", "PUT", "PATCH") }
            );

            context.MapRoute(
                name: "API_SaveProperty2_Ending",
                url: "API/v1/{ModelName}/{ID}/{PropertyName}/{PropertyID}.{ending}",
                defaults: new { controller = "API", action = "SaveProperty" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST", "PUT", "PATCH") }
            );

            context.MapRoute(
                name: "API_DeleteProperty_Ending",
                url: "API/v1/{ModelName}/{ID}/{PropertyName}/{PropertyID}.{ending}",
                defaults: new { controller = "API", action = "DeleteProperty" },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") }
            );

            context.MapRoute(
                name: "API_GetProperty_Ending",
                url: "API/v1/{ModelName}/{ID}/{PropertyName}.{ending}",
                defaults: new { controller = "API", action = "GetProperty" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.MapRoute(
                name: "API_Any_Ending",
                url: "API/v1/{ModelName}/{ID}.{ending}",
                defaults: new { controller = "API", action = "Any" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.MapRoute(
                name: "API_All_Ending",
                url: "API/v1/{ModelName}.{ending}",
                defaults: new { controller = "API", action = "All" },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            context.MapRoute(
                "API_default",
                "API/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}