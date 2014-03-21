using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Copernicus.Startup))]

namespace Copernicus
{
    /// <summary>
    /// OWIN Startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configuration for OWIN
        /// </summary>
        /// <param name="app">App builder</param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}