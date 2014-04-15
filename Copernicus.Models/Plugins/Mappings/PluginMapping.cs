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

using Copernicus.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copernicus.Models.Plugins.Mappings
{
    /// <summary>
    /// Plugin mapping
    /// </summary>
    public class PluginMapping : ModelMappingBase<Plugin>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PluginMapping()
            : base()
        {
            Reference(x => x.Author).SetMaxLength(80);
            Reference(x => x.Description).SetMaxLength(1000);
            Reference(x => x.LastUpdated);
            Reference(x => x.Name).SetMaxLength(100);
            Reference(x => x.OnlineVersion).SetMaxLength(10);
            Reference(x => x.Priority);
            Reference(x => x.Tags).SetMaxLength(100);
            Map(x => x.Type);
            Reference(x => x.Version).SetMaxLength(10);
            Reference(x => x.Website).SetMaxLength(100);
        }
    }
}