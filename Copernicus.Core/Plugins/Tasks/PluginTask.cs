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

using Ironman.Core.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copernicus.Core.Plugins.Tasks
{
    /// <summary>
    /// Plugin task
    /// </summary>
    public class PluginTask : ITask
    {
        /// <summary>
        /// Name of the task
        /// </summary>
        public string Name { get { return "Plugin Update"; } }

        /// <summary>
        /// Time that the task should be run
        /// </summary>
        public Ironman.Core.Tasks.Enums.RunTime TimeToRun { get { return Ironman.Core.Tasks.Enums.RunTime.PostStart; } }

        /// <summary>
        /// Runs the task
        /// </summary>
        public void Run()
        {
            Utilities.IoC.Manager.Bootstrapper.Resolve<PluginManager>().Initialize();
        }
    }
}