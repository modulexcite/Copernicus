using Copernicus.Core.Plugins;
using Copernicus.Models.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Copernicus.Controllers
{
    /// <summary>
    /// Admin controller
    /// </summary>
    public class AdminController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// GET: /Admin/
        /// </summary>
        /// <returns>The view</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays available plugins.
        /// </summary>
        /// <returns>The view</returns>
        [HttpGet]
        public ActionResult Plugins()
        {
            return View(Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>().PluginsAvailable);
        }

        /// <summary>
        /// Displays available plugins.
        /// </summary>
        /// <param name="Form">The form.</param>
        /// <returns>The view</returns>
        [HttpPost]
        public ActionResult Plugins(FormCollection Form)
        {
            Contract.Requires<ArgumentNullException>(Form != null, "Form");
            PluginManager Manager = Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>();
            foreach (string Key in Form)
            {
                Manager.InstallPlugin(Key);
            }
            Manager.RestartSystem();
            return View(Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>().PluginsAvailable);
        }
    }
}