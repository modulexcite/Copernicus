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
using System.Web.Configuration;
using Copernicus.Core.Providers;
using Ironman.Core.Tasks.Enums;
using Ironman.Core.Tasks.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Utilities.IoC.Interfaces;

namespace Copernicus.Core.Bootstrapper
{
    /// <summary>
    /// Provider module
    /// </summary>
    public class ProviderModule : ITask
    {
        /// <summary>
        /// Name of the task
        /// </summary>
        public string Name { get { return "OWIN Setup"; } }

        /// <summary>
        /// Time that the task should be run
        /// </summary>
        public RunTime TimeToRun
        {
            get { return RunTime.PostStart; }
        }

        /// <summary>
        /// Runs the task
        /// </summary>
        public void Run()
        {
            IAppBuilder Builder = Utilities.IoC.Manager.Bootstrapper.Resolve<IAppBuilder>();
            var Config = WebConfigurationManager.OpenWebConfiguration("/");
            var AuthSection = (AuthenticationSection)Config.GetSection("system.web/authentication");
            if (AuthSection.Mode == AuthenticationMode.Forms)
            {
                Builder.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Account/Login"),
                    LogoutPath = new PathString("/Account/LogOff")
                });
                Builder.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            }
            else if (AuthSection.Mode == AuthenticationMode.Windows)
            {
                Builder.Use(typeof(WindowsPrincipalHandler));
            }
        }
    }
}