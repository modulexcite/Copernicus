﻿/*
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
using Copernicus.Core.Plugins.Interfaces;
using Copernicus.Models.Plugins;
using Utilities.DataTypes.Patterns.BaseClasses;
using Utilities.IO;
using Utilities.IO.Logging.Enums;

namespace Copernicus.Core.Plugins.BaseClasses
{
    /// <summary>
    /// Plugin base class
    /// </summary>
    public abstract class PluginBase : SafeDisposableBaseClass, IPlugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginBase" /> class.
        /// </summary>
        protected PluginBase()
            : base()
        {
            Log.Get().LogMessage("Initializing plugin: {0}", MessageType.Debug, Name);
            PluginData = PluginList.Load().Get(Name);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the plugin data.
        /// </summary>
        /// <value>The plugin data.</value>
        public Models.Plugins.Plugin PluginData { get; private set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {
            PluginData.Initialize();
        }

        /// <summary>
        /// Function to override in order to dispose objects
        /// </summary>
        /// <param name="Managed">
        /// If true, managed and unmanaged objects should be disposed. Otherwise unmanaged objects only.
        /// </param>
        protected override void Dispose(bool Managed)
        {
            Log.Get().LogMessage("Uninitializing plugin: {0}", MessageType.Debug, Name);
        }
    }
}