/*
Copyright (c) 2014 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using Copernicus.Models.Plugins;
using NuGet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilities.DataTypes;
using Utilities.IO;
using Utilities.IO.Logging.Enums;

namespace Copernicus.Core.Plugins
{
    /// <summary>
    /// Plugin manager
    /// </summary>
    public class PluginManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginManager" /> class.
        /// </summary>
        /// <param name="Repositories">The repositories.</param>
        public PluginManager(IEnumerable<string> Repositories)
        {
            PackageRepositories = Repositories.ForEach(x => PackageRepositoryFactory.Default.CreateRepository(x));
        }

        /// <summary>
        /// Gets the package repositories.
        /// </summary>
        /// <value>The package repositories.</value>
        protected IEnumerable<IPackageRepository> PackageRepositories { get; private set; }

        /// <summary>
        /// Installs the plugin associated with the ID
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>Returns true if it is installed successfully, false otherwise</returns>
        public bool InstallPlugin(string ID)
        {
            Plugin TempPlugin = Plugin.Load(ID);
            if (TempPlugin != null)
                UninstallPlugin(ID);
        }

        /// <summary>
        /// Uninstalls the plugin associated with the ID
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>Returns true if it is uninstalled successfully, false otherwise</returns>
        public bool UninstallPlugin(string ID)
        {
            try
            {
                foreach (IPackageRepository Repo in PackageRepositories)
                {
                    IPackage Package = Repo.FindPackage(ID);
                    if (Package != null)
                    {
                        PackageManager Manager = new PackageManager(Repo,
                            new DefaultPackagePathResolver(Repo.Source),
                            new PhysicalFileSystem(new FileInfo("~/App_Data/packages").FullName));
                        Manager.UninstallPackage(Package, true, true);
                    }
                }
                Plugin TempPlugin = Plugin.Load(ID);
                if (TempPlugin != null)
                    TempPlugin.Delete();
                Log.Get().LogMessage("Plugin {0} has been uninstalled by {1}", MessageType.Info, HttpContext.Current.User.Identity.Name);
                return true;
            }
            catch (Exception e)
            {
                Log.Get().LogMessage("Plugin {0} was not uninstalled successfully: {1}", MessageType.Error, ID, e.ToString());
            }
            return false;
        }
    }
}