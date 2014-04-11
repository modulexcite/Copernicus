using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Copernicus.Core.Tests.Providers
{
    public class WindowsPrincipalHandler
    {
        [Fact]
        public void Creation()
        {
            var Context = new Dictionary<string, object>();
            Assert.DoesNotThrow(() => { Copernicus.Core.Providers.WindowsPrincipalHandler Handlers = new Core.Providers.WindowsPrincipalHandler(x => Task.Run(() => { })); });
        }

        [Fact]
        public void Invoke()
        {
            var Context = new Dictionary<string, object>();
            Copernicus.Core.Providers.WindowsPrincipalHandler Handlers = new Core.Providers.WindowsPrincipalHandler(x => Task.Run(() => { }));
            Assert.DoesNotThrow(() => Handlers.Invoke(Context));
        }
    }
}