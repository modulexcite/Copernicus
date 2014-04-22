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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Copernicus.Models.BaseClasses;
using Copernicus.Models.General;
using Utilities.DataTypes;
using Utilities.IO;
using Utilities.IO.FileSystem.Interfaces;
using Utilities.ORM.Parameters;

namespace Copernicus.Models.Plugins
{
    /// <summary>
    /// Plugin model
    /// </summary>
    public class Plugin : ModelBase<Plugin>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Plugin()
            : base()
        {
            Files = new List<PluginFile>();
        }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public virtual List<PluginFile> Files { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>The last updated.</value>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the online version.
        /// </summary>
        /// <value>The online version.</value>
        public string OnlineVersion { get; set; }

        /// <summary>
        /// Gets or sets the plugin identifier.
        /// </summary>
        /// <value>The plugin identifier.</value>
        public string PluginID { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public virtual LookUp Type { get; set; }

        /// <summary>
        /// Gets a value indicating whether an update is available.
        /// </summary>
        /// <value><c>true</c> if [update available]; otherwise, <c>false</c>.</value>
        public bool UpdateAvailable { get { return int.Parse(Version.Keep(StringFilter.Numeric)) < int.Parse(OnlineVersion.Keep(StringFilter.Numeric)); } }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>The website.</value>
        public string Website { get; set; }

        /// <summary>
        /// Loads the specified plugin based on the identifier.
        /// </summary>
        /// <param name="PluginID">The plugin identifier.</param>
        /// <returns>The plugin specified</returns>
        public static Plugin Load(string PluginID)
        {
            return Any(new StringEqualParameter(PluginID, "PluginID_", 100));
        }

        /// <summary>
        /// Deletes this instance and removes the files associated with it.
        /// </summary>
        public override void Delete()
        {
            Files.OrderByDescending(x => x.Order).ForEach(x => x.Remove());
            base.Delete();
        }

        /// <summary>
        /// Saves this instance and copies the files associated with it.
        /// </summary>
        public override void Save()
        {
            if (Files.Count == 0)
            {
                IDirectory LibDirectory = new DirectoryInfo(string.Format("~/App_Data/packages/{0}.{1}/lib", PluginID, Version)).EnumerateDirectories().OrderByDescending(x => x.Name).First();
                IDirectory ContentDirectory = new DirectoryInfo(string.Format("~/App_Data/packages/{0}.{1}/content", PluginID, Version));
                int y = 0;
                List<PluginFile> TempFiles = new List<PluginFile>();
                TempFiles.Add(LibDirectory.EnumerateFiles("*", System.IO.SearchOption.AllDirectories).ForEach(x => new PluginFile()
                {
                    IsDirectory = false,
                    Order = y++,
                    Path = "~/bin" + x.FullName.Replace(LibDirectory.FullName, "").Replace("\\", "/"),
                    Plugin = this
                }));
                TempFiles.Add(ContentDirectory.EnumerateFiles("*", System.IO.SearchOption.AllDirectories).ForEach(x => new PluginFile()
                {
                    IsDirectory = false,
                    Order = y++,
                    Path = "~" + x.FullName.Replace(ContentDirectory.FullName, "").Replace("\\", "/"),
                    Plugin = this
                }));
                Files = TempFiles;
                new DirectoryInfo("~/bin/").Create();
                LibDirectory.CopyTo(new DirectoryInfo("~/bin/"));
                ContentDirectory.CopyTo(new DirectoryInfo("~/"));
                new DirectoryInfo(string.Format("~/App_Data/packages/{0}/", PluginID)).Delete();
                new DirectoryInfo(string.Format("~/App_Data/packages/{0}.{1}/", PluginID, Version)).Delete();
            }
            base.Save();
        }
    }
}