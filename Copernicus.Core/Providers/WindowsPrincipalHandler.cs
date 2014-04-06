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

using Copernicus.Models.Authentication;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Copernicus.Core.Providers
{
    /// <summary>
    /// Windows auth for OWIN
    /// </summary>
    public class WindowsPrincipalHandler
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Next">The current call</param>
        public WindowsPrincipalHandler(Func<IDictionary<string, object>, Task> Next)
        {
            this.Next = Next;
        }

        /// <summary>
        /// The current call
        /// </summary>
        private readonly Func<IDictionary<string, object>, Task> Next;

        /// <summary>
        /// Called when it's invoked
        /// </summary>
        /// <param name="env">Environment variables</param>
        /// <returns>The task object</returns>
        public async Task Invoke(IDictionary<string, object> env)
        {
            Contract.Requires<ArgumentNullException>(env != null, "env");
            var Context = new OwinContext(env);
            if (Context.Request.User == null && Context.Request.Path != new PathString("/Account/Login"))
            {
                Context.Response.Redirect((Context.Request.PathBase + new PathString("/Account/Login")).Value);
            }

            var Principal = Context.Request.User as WindowsPrincipal;
            if (Principal != null && Principal.Identity.IsAuthenticated)
            {
                if (User.LoadCurrentUser() == null)
                {
                    User TempUser = GetUser(Principal);
                    SetupDefaultClaims(Principal, TempUser);
                    TempUser.Save();
                    var Identity = new ClaimsIdentity(TempUser.Claims.Select(x => new Claim(x.Type, x.Value)), "WindowsAuthType");
                    Context.Authentication.SignIn(Identity);
                    Context.Response.Redirect((Context.Request.PathBase + Context.Request.Path).Value);
                }

                await Next(env);

                if (Context.Response.StatusCode == 401)
                {
                    User TempUser = GetUser(Principal);
                    var Identity = new ClaimsIdentity(TempUser.Claims.Select(x => new Claim(x.Type, x.Value)), "WindowsAuthType");
                    Context.Authentication.SignIn(Identity);
                    Context.Response.Redirect((Context.Request.PathBase + Context.Request.Path).Value);
                }
                return;
            }

            await Next(env);
        }

        private User GetUser(WindowsPrincipal windowsPrincipal)
        {
            var nameClaim = windowsPrincipal.FindFirst(ClaimTypes.Name);
            string name = nameClaim.Value;
            string[] parts = name.Split(new[] { '\\' }, 2);
            string shortName = parts.Length == 1 ? parts[0] : parts[parts.Length - 1];
            var userManager = new UserManager<User, long>(new UserStore());
            User user = userManager.FindByNameAsync(shortName).Result;
            if (user == null)
            {
                var Result = userManager.CreateAsync(new User() { UserName = shortName }, Guid.NewGuid().ToString()).Result;
                user = userManager.FindByNameAsync(shortName).Result;
            }
            return user;
        }

        private void SetupDefaultClaims(WindowsPrincipal windowsPrincipal, User TempUser)
        {
            TempUser.Claims.Add(new UserClaim()
            {
                Value = windowsPrincipal.FindFirst(ClaimTypes.Name).Value,
                Type = ClaimTypes.NameIdentifier
            });
            TempUser.Claims.Add(new UserClaim()
            {
                Value = TempUser.UserName,
                Type = ClaimTypes.Name
            });
            TempUser.Claims.Add(UserClaim.Load(ClaimTypes.AuthenticationMethod, "Windows").Check(new UserClaim() { Value = "Windows", Type = ClaimTypes.AuthenticationMethod }));
        }
    }
}