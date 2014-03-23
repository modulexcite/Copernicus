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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities.IO;
using Xunit;

namespace Copernicus.Models.Tests.Authentication.Stores
{
    /// <summary>
    /// User store
    /// </summary>
    public class UserStore : IDisposable
    {
        public UserStore()
        {
            var BootLoader = Utilities.IoC.Manager.Bootstrapper;
            new Utilities.ORM.Manager.ORMManager(BootLoader);
            Store = new Models.Authentication.Stores.UserStore();
        }

        private Copernicus.Models.Authentication.Stores.UserStore Store { get; set; }

        [Fact]
        public void AddClaimAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Assert.DoesNotThrow(() => Store.AddClaimAsync(TempUser, new Claim(ClaimTypes.AuthenticationMethod, "Windows")).Wait());
            Assert.Equal(1, TempUser.Claims.Count);
            Assert.Equal("Windows", TempUser.Claims[0].Value);
            Assert.Equal(ClaimTypes.AuthenticationMethod, TempUser.Claims[0].Type);
        }

        [Fact]
        public void AddLoginAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Assert.DoesNotThrow(() => Store.AddLoginAsync(TempUser, new Microsoft.AspNet.Identity.UserLoginInfo("Something", "A Key")).Wait());
            Assert.Equal(1, TempUser.ExternalLogins.Count);
            Assert.Equal("Something", TempUser.ExternalLogins[0].LoginProvider);
            Assert.Equal("A Key", TempUser.ExternalLogins[0].ProviderKey);
        }

        [Fact]
        public void CreateAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            Assert.DoesNotThrow(() => Store.CreateAsync(TempUser).Wait());
        }

        public void Dispose()
        {
            if (Store != null)
            {
                Store.Dispose();
                Store = null;
            }
            try
            {
                Utilities.ORM.QueryProvider.Batch("Default")
                            .AddCommand(null, null, CommandType.Text, "ALTER DATABASE CopernicusTest SET OFFLINE WITH ROLLBACK IMMEDIATE")
                            .AddCommand(null, null, CommandType.Text, "ALTER DATABASE CopernicusTest SET ONLINE")
                            .AddCommand(null, null, CommandType.Text, "DROP DATABASE CopernicusTest")
                            .Execute();
            }
            catch { }
        }
    }
}