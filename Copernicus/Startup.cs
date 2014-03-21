using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Copernicus.Startup))]

namespace Copernicus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}