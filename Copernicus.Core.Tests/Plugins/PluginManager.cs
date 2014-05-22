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
using Copernicus.Models.Plugins;
using Utilities.IO;
using Xunit;

namespace Copernicus.Core.Tests.Plugins
{
    public class PluginManager : IDisposable
    {
        public PluginManager()
        {
            var Bootstrapper = Utilities.IoC.Manager.Bootstrapper;
            new DirectoryInfo("~/App_Data").Delete();
        }

        [Fact]
        public void Creation()
        {
            Copernicus.Core.Plugins.PluginManager Manager = null;
            Assert.DoesNotThrow(() => Manager = new Core.Plugins.PluginManager(new string[] { "http://localhost:8797/api/v2" }));
        }

        public void Dispose()
        {
            new DirectoryInfo("~/App_Data").Delete();
        }

        [Fact]
        public void GetPluginsAvailable()
        {
            Copernicus.Core.Plugins.PluginManager Manager = new Core.Plugins.PluginManager(new string[] { "http://localhost:8797/api/v2" });
            IEnumerable<Copernicus.Models.Plugins.Plugin> Plugins = Manager.PluginsAvailable;
            Assert.True(Plugins.Any(x => x.Name == "xUnit.net"));
            Assert.True(Plugins.Count() > 0);
        }

        [Fact]
        public void InstallPlugin()
        {
            Copernicus.Core.Plugins.PluginManager Manager = new Core.Plugins.PluginManager(new string[] { "http://localhost:8797/api/v2" });
            Assert.DoesNotThrow(() => Manager.InstallPlugin("xunit"));
            Plugin TempPlugin = PluginList.Load().Get("xunit");
            Assert.Equal(6, new DirectoryInfo("~/App_Data/xunit/").EnumerateFiles().Count());
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.xml").Exists);
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.runner.utility.dll").Exists);
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.runner.tdnet.dll").Exists);
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.runner.msbuild.dll").Exists);
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.dll.tdnet").Exists);
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.dll").Exists);
            Assert.NotNull(TempPlugin);
            Assert.Equal("JamesNewkirk,BradWilson", TempPlugin.Author.Replace(" ", ""));
            Assert.Equal("xUnit.net is a developer testing framework, built to support Test Driven Development, with a design goal of extreme simplicity and alignment with framework features.", TempPlugin.Description);
            Assert.Equal(6, TempPlugin.Files.Count);
            Assert.Equal("xUnit.net", TempPlugin.Name);
            Assert.Equal("1.9.2", TempPlugin.OnlineVersion);
            Assert.Equal("xunit", TempPlugin.PluginID);
            Assert.Equal(0, TempPlugin.Priority);
            Assert.Equal(null, TempPlugin.Tags);
            Assert.Equal(null, TempPlugin.Type);
            Assert.Equal(false, TempPlugin.UpdateAvailable);
            Assert.Equal("1.9.2", TempPlugin.Version);
        }

        [Fact]
        public void InstallPluginWithRequireds()
        {
            Copernicus.Core.Plugins.PluginManager Manager = new Core.Plugins.PluginManager(new string[] { "http://localhost:8797/api/v2" });
            Assert.DoesNotThrow(() => Manager.InstallPlugin("Copernicus.Models.CRM"));
            Plugin TempPlugin = PluginList.Load().Get("Copernicus.Models.CRM");
            Assert.Equal(1, new DirectoryInfo("~/App_Data/Copernicus.Models.CRM/").EnumerateFiles().Count());
            Assert.True(new FileInfo("~/App_Data/Copernicus.Models.CRM/Copernicus.Models.CRM.dll").Exists);
            Assert.Equal(1, new DirectoryInfo("~/App_Data/Copernicus.Models.Content/").EnumerateFiles().Count());
            Assert.True(new FileInfo("~/App_Data/Copernicus.Models.Content/Copernicus.Models.Content.dll").Exists);
            Assert.NotNull(TempPlugin);
            Assert.Equal("JamesCraig", TempPlugin.Author.Replace(" ", ""));
            Assert.Equal("Contains the models used by various plugins that need crm.", TempPlugin.Description);
            Assert.Equal(1, TempPlugin.Files.Count);
            Assert.Equal("Copernicus Models for CRM", TempPlugin.Name);
            Assert.Equal("1.0.0", TempPlugin.OnlineVersion);
            Assert.Equal("Copernicus.Models.CRM", TempPlugin.PluginID);
            Assert.Equal(0, TempPlugin.Priority);
            Assert.Equal("models crm", TempPlugin.Tags);
            Assert.Equal(null, TempPlugin.Type);
            Assert.Equal(false, TempPlugin.UpdateAvailable);
            Assert.Equal("1.0.0", TempPlugin.Version);
            TempPlugin = PluginList.Load().Get("Copernicus.Models.Content");
            Assert.NotNull(TempPlugin);
            Assert.Equal("JamesCraig", TempPlugin.Author.Replace(" ", ""));
            Assert.Equal("Contains the models used by various plugins that need content.", TempPlugin.Description);
            Assert.Equal(1, TempPlugin.Files.Count);
            Assert.Equal("Copernicus Models for Content", TempPlugin.Name);
            Assert.Equal("1.0.0", TempPlugin.OnlineVersion);
            Assert.Equal("Copernicus.Models.Content", TempPlugin.PluginID);
            Assert.Equal(0, TempPlugin.Priority);
            Assert.Equal("models content", TempPlugin.Tags);
            Assert.Equal(null, TempPlugin.Type);
            Assert.Equal(false, TempPlugin.UpdateAvailable);
            Assert.Equal("1.0.0", TempPlugin.Version);
        }

        [Fact]
        public void UninstallPlugin()
        {
            Copernicus.Core.Plugins.PluginManager Manager = new Core.Plugins.PluginManager(new string[] { "http://localhost:8797/api/v2" });
            Assert.DoesNotThrow(() => Manager.InstallPlugin("xunit"));
            Assert.DoesNotThrow(() => Manager.UninstallPlugin("xunit"));
            Assert.Equal(0, new DirectoryInfo("~/App_Data/xunit/").EnumerateFiles().Count());
            Assert.Null(PluginList.Load().Get("xunit"));
            Assert.Equal(0, PluginList.Load().Plugins.Count);
        }

        [Fact]
        public void UpdatePlugin()
        {
            Copernicus.Core.Plugins.PluginManager Manager = new Core.Plugins.PluginManager(new string[] { "http://localhost:8797/api/v2" });
            Assert.DoesNotThrow(() => Manager.InstallPlugin("xunit"));
            Plugin TempPlugin = PluginList.Load().Get("xunit");
            TempPlugin.OnlineVersion = "2.0.0";
            Assert.DoesNotThrow(() => Manager.UpdatePlugin("xunit"));
            TempPlugin = PluginList.Load().Get("xunit");
            Assert.Equal(1, PluginList.Load().Plugins.Count);
            Assert.Equal(6, new DirectoryInfo("~/App_Data/xunit/").EnumerateFiles().Count());
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.xml").Exists);
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.runner.utility.dll").Exists);
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.runner.tdnet.dll").Exists);
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.runner.msbuild.dll").Exists);
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.dll.tdnet").Exists);
            Assert.True(new FileInfo("~/App_Data/xunit/xunit.dll").Exists);
            Assert.NotNull(TempPlugin);
            Assert.Equal("JamesNewkirk,BradWilson", TempPlugin.Author.Replace(" ", ""));
            Assert.Equal("xUnit.net is a developer testing framework, built to support Test Driven Development, with a design goal of extreme simplicity and alignment with framework features.", TempPlugin.Description);
            Assert.Equal(6, TempPlugin.Files.Count);
            Assert.Equal("xUnit.net", TempPlugin.Name);
            Assert.Equal("1.9.2", TempPlugin.OnlineVersion);
            Assert.Equal("xunit", TempPlugin.PluginID);
            Assert.Equal(0, TempPlugin.Priority);
            Assert.Equal(null, TempPlugin.Tags);
            Assert.Equal(null, TempPlugin.Type);
            Assert.Equal(false, TempPlugin.UpdateAvailable);
            Assert.Equal("1.9.2", TempPlugin.Version);
        }
    }
}