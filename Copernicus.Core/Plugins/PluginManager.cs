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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Copernicus.Models.Plugins;
using NuGet;
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
            Contract.Requires<ArgumentNullException>(Repositories != null, "Repositories");
            PackageRepositories = Repositories.ForEach(x => PackageRepositoryFactory.Default.CreateRepository(x));
        }

        /// <summary>
        /// Gets the package repositories.
        /// </summary>
        /// <value>The package repositories.</value>
        protected IEnumerable<IPackageRepository> PackageRepositories { get; private set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            foreach (Plugin TempPlugin in Plugin.All())
            {
                foreach (IPackageRepository Repo in PackageRepositories)
                {
                    IPackage Package = Repo.FindPackage(TempPlugin.PluginID);
                    if (Package != null)
                    {
                        TempPlugin.OnlineVersion = Package.Version.ToString();
                        TempPlugin.Save();
                    }
                }
            }
        }

        /// <summary>
        /// Installs the plugin associated with the ID
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>Returns true if it is installed successfully, false otherwise</returns>
        public bool InstallPlugin(string ID)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(ID), "ID");
            string User = HttpContext.Current.Chain(x => x.User).Chain(x => x.Identity).Chain(x => x.Name, "");
            Log.Get().LogMessage("Plugin {0} is being installed by {1}", MessageType.Info, ID, User);
            Plugin TempPlugin = Plugin.Load(ID);
            if (TempPlugin != null)
                UninstallPlugin(ID);
            foreach (IPackageRepository Repo in PackageRepositories)
            {
                IPackage Package = Repo.FindPackage(ID);
                if (Package != null)
                {
                    new DirectoryInfo("~/App_Data/packages/" + Package.Id + "." + Package.Version.ToString() + "/lib").Create();
                    new DirectoryInfo("~/App_Data/packages/" + Package.Id + "." + Package.Version.ToString() + "/content").Create();
                    new DirectoryInfo("~/App_Data/packages/" + Package.Id + "." + Package.Version.ToString() + "/tools").Create();
                    PackageManager Manager = new PackageManager(Repo,
                        new DefaultPackagePathResolver(Repo.Source),
                        new PhysicalFileSystem(new DirectoryInfo("~/App_Data/packages").FullName));
                    Manager.InstallPackage(Package, false, true);
                    TempPlugin = new Plugin()
                    {
                        PluginID = ID,
                        Version = Package.Version.ToString(),
                        Author = Package.Authors.ToString(x => x),
                        Description = Package.Description,
                        LastUpdated = Package.Published.Value.DateTime,
                        Name = Package.Title,
                        OnlineVersion = Package.Version.ToString(),
                        Tags = Package.Tags,
                        Website = Package.ProjectUrl.ToString()
                    };
                    TempPlugin.Save();
                }
            }
            Log.Get().LogMessage("Plugin {0} has been installed by {1}", MessageType.Info, ID, User);
            return true;
        }

        /// <summary>
        /// Uninstalls the plugin associated with the ID
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>Returns true if it is uninstalled successfully, false otherwise</returns>
        public bool UninstallPlugin(string ID)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(ID), "ID");
            Plugin TempPlugin = Plugin.Load(ID);
            if (TempPlugin == null)
                return true;
            string User = HttpContext.Current.Chain(x => x.User).Chain(x => x.Identity).Chain(x => x.Name, "");
            Log.Get().LogMessage("Plugin {0} is being uninstalled by {1}", MessageType.Info, ID, User);
            TempPlugin.Delete();
            Log.Get().LogMessage("Plugin {0} has been uninstalled by {1}", MessageType.Info, ID, User);
            return true;
        }

        /// <summary>
        /// Updates the plugin associated with the ID
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>Returns true if it is updated successfully, false otherwise</returns>
        public bool UpdatePlugin(string ID)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(ID), "ID");
            Plugin TempPlugin = Plugin.Load(ID);
            if (TempPlugin == null)
                return true;
            string User = HttpContext.Current.Chain(x => x.User).Chain(x => x.Identity).Chain(x => x.Name, "");
            bool Result = true;
            Log.Get().LogMessage("Plugin {0} is being updated by {1}", MessageType.Info, ID, User);
            foreach (IPackageRepository Repo in PackageRepositories)
            {
                IPackage Package = Repo.FindPackage(ID);
                if (Package != null)
                {
                    TempPlugin = Plugin.Load(ID);
                    if (TempPlugin != null)
                    {
                        TempPlugin.OnlineVersion = Package.Version.ToString();
                        TempPlugin.Save();
                        if (TempPlugin.UpdateAvailable)
                            Result = UninstallPlugin(ID);
                        if (Result)
                            Result = InstallPlugin(ID);
                    }
                    break;
                }
            }
            return Result;
        }
    }
}