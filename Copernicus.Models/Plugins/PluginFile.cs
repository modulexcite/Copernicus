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
using System.Web;
using Copernicus.Models.BaseClasses;
using Utilities.IO;
using Utilities.IO.Logging.Enums;

namespace Copernicus.Models.Plugins
{
    /// <summary>
    /// Files included with a plugin
    /// </summary>
    public class PluginFile : ModelBase<PluginFile>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginFile" /> class.
        /// </summary>
        public PluginFile()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a directory.
        /// </summary>
        /// <value><c>true</c> if this instance is a directory; otherwise, <c>false</c>.</value>
        public virtual bool IsDirectory { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public virtual int Order { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public virtual string Path { get; set; }

        /// <summary>
        /// Gets or sets the plugin.
        /// </summary>
        /// <value>The plugin.</value>
        public virtual Plugin Plugin { get; set; }

        /// <summary>
        /// Removes this instance.
        /// </summary>
        public void Remove()
        {
            Log.Get().LogMessage("Removing {0} {1}", MessageType.Info, IsDirectory ? "directory" : "file", Path);
            if (IsDirectory)
                new DirectoryInfo(Path).Delete();
            else
                new FileInfo(Path).Delete();
        }
    }
}