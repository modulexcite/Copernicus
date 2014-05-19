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
        /// Installs the plugins selected
        /// </summary>
        /// <param name="Form">The form.</param>
        /// <returns>The view</returns>
        [HttpPost]
        public ActionResult InstallPlugin(FormCollection Form)
        {
            Contract.Requires<ArgumentNullException>(Form != null, "Form");
            PluginManager Manager = Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>();
            foreach (string Key in Form)
            {
                Manager.InstallPlugin(Key);
            }
            Manager.RestartSystem();
            return View("PluginsAvailable", Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>().PluginsAvailable);
        }

        /// <summary>
        /// Lists the plugins that are installed
        /// </summary>
        /// <param name="Form">The form.</param>
        /// <returns>The view</returns>
        [HttpGet]
        public ActionResult Plugins()
        {
            return View(Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>().PluginsInstalled);
        }

        /// <summary>
        /// Displays available plugins.
        /// </summary>
        /// <returns>The view</returns>
        [HttpGet]
        public ActionResult PluginsAvailable()
        {
            return View(Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>().PluginsAvailable);
        }

        /// <summary>
        /// Uninstalls the plugins selected
        /// </summary>
        /// <param name="Form">The form.</param>
        /// <returns>The view</returns>
        [HttpPost]
        public ActionResult UninstallPlugin(FormCollection Form)
        {
            Contract.Requires<ArgumentNullException>(Form != null, "Form");
            PluginManager Manager = Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>();
            foreach (string Key in Form)
            {
                Manager.UninstallPlugin(Key);
            }
            Manager.RestartSystem();
            return View(Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>().PluginsAvailable);
        }

        /// <summary>
        /// Updates the plugins selected
        /// </summary>
        /// <param name="Form">The form.</param>
        /// <returns>The view</returns>
        [HttpPost]
        public ActionResult UpdatePlugin(FormCollection Form)
        {
            Contract.Requires<ArgumentNullException>(Form != null, "Form");
            PluginManager Manager = Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>();
            foreach (string Key in Form)
            {
                Manager.UpdatePlugin(Key);
            }
            Manager.RestartSystem();
            return View(Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>().PluginsAvailable);
        }
    }
}