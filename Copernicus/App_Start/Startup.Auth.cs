using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Copernicus.Models.Authentication;
using Copernicus.Models.Authentication.Stores;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Copernicus
{
    /// <summary>
    /// OWIN Startup
    /// </summary>
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// <summary>
        /// Configures auth
        /// </summary>
        /// <param name="app">App builder</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            if (app == null)
                return;
            var Config = WebConfigurationManager.OpenWebConfiguration("/");
            var AuthSection = (AuthenticationSection)Config.GetSection("system.web/authentication");
            if (AuthSection.Mode == AuthenticationMode.Forms)
            {
                // Enable the application to use a cookie to store information for the signed in user
                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Account/Login"),
                    LogoutPath = new PathString("/Account/LogOff")
                });
                // Use a cookie to temporarily store information about a user logging in with a
                // third party login provider
                app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            }
            else if (AuthSection.Mode == AuthenticationMode.Windows)
            {
                app.Use(typeof(WindowsPrincipalHandler));
            }

            //app.UseMicrosoftAccountAuthentication(
            // clientId: "",
            // clientSecret: "");

            //app.UseTwitterAuthentication(
            // consumerKey: "",
            // consumerSecret: "");

            //app.UseFacebookAuthentication(
            // appId: "",
            // appSecret: "");

            //app.UseGoogleAuthentication();
        }
    }

    /// <summary>
    /// Windows auth for OWIN
    /// </summary>
    public class WindowsPrincipalHandler
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        public WindowsPrincipalHandler(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        private readonly Func<IDictionary<string, object>, Task> _next;

        /// <summary>
        /// Called when it's invoked
        /// </summary>
        /// <param name="env">Environment variables</param>
        /// <returns>The task object</returns>
        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);

            if (context.Request.User == null)
            {
                if (context.Request.Path != new PathString("/Account/Login"))
                {
                    context.Response.Redirect((context.Request.PathBase + new PathString("/Account/Login")).Value);
                }
            }

            var windowsPrincipal = context.Request.User as WindowsPrincipal;
            if (windowsPrincipal != null && windowsPrincipal.Identity.IsAuthenticated)
            {
                await _next(env);

                if (context.Response.StatusCode == 401)
                {
                    var nameClaim = windowsPrincipal.FindFirst(ClaimTypes.Name);

                    string name = nameClaim.Value;
                    var parts = name.Split(new[] { '\\' }, 2);
                    string shortName = parts.Length == 1 ? parts[0] : parts[parts.Length - 1];
                    var userManager = new UserManager<User, long>(new UserStore());
                    User user = userManager.FindByNameAsync(shortName).Result;

                    var claims = new List<Claim>(); claims.Add(new Claim(ClaimTypes.NameIdentifier, name));
                    claims.Add(new Claim(ClaimTypes.Name, shortName));
                    claims.Add(new Claim(ClaimTypes.AuthenticationMethod, "Windows"));
                    var identity = new ClaimsIdentity(claims, "WindowsAuthType");

                    context.Authentication.SignIn(identity);

                    context.Response.Redirect((context.Request.PathBase + context.Request.Path).Value);
                }

                return;
            }

            await _next(env);
        }
    }
}