using RazorGenerator.Mvc;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Copernicus.App_Start.RazorGeneratorMvcStart), "Start")]

namespace Copernicus.App_Start
{
    /// <summary>
    /// Razor generator class
    /// </summary>
    public static class RazorGeneratorMvcStart
    {
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public static void Start()
        {
            var engine = new PrecompiledMvcEngine(typeof(RazorGeneratorMvcStart).Assembly)
            {
                UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
            };

            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
    }
}