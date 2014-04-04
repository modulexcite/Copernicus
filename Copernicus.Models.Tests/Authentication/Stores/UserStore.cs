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
            Assert.Equal(1, User.All().Count());
        }

        [Fact]
        public void DeleteAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Assert.DoesNotThrow(() => Store.DeleteAsync(TempUser).Wait());
            Assert.Equal(0, User.All().Count());
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

        [Fact]
        public void FindAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            TempUser.ExternalLogins.Add(new ExternalLogin() { LoginProvider = "TestProvider", ProviderKey = "TestKey", User = TempUser });
            TempUser.Save();
            User TempUser2 = Store.FindAsync(new Microsoft.AspNet.Identity.UserLoginInfo("TestProvider", "TestKey")).Result;
            Assert.Equal("TestUser", TempUser2.UserName);
            Assert.Equal(TempUser.PasswordHash, TempUser2.PasswordHash);
            Assert.Equal(TempUser.ExternalLogins[0].ID, TempUser2.ExternalLogins[0].ID);
        }

        [Fact]
        public void FindByEmailAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            User TempUser2 = Store.FindByEmailAsync("something@somewhere.com").Result;
            Assert.Equal(TempUser.ID, TempUser2.ID);
            Assert.Equal(TempUser.UserName, TempUser2.UserName);
        }

        [Fact]
        public void FindByIdAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            User TempUser2 = Store.FindByIdAsync(TempUser.ID).Result;
            Assert.Equal(TempUser.ID, TempUser2.ID);
            Assert.Equal(TempUser.UserName, TempUser2.UserName);
        }

        [Fact]
        public void FindByNameAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            User TempUser2 = Store.FindByNameAsync(TempUser.UserName).Result;
            Assert.Equal(TempUser.ID, TempUser2.ID);
            Assert.Equal(TempUser.UserName, TempUser2.UserName);
        }

        [Fact]
        public void GetClaimsAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Claims.Add(new UserClaim() { Type = "Something", Value = "SomethingAlso" });
            TempUser.Save();
            TempUser = User.Load(TempUser.ID);
            IList<Claim> Claims = Store.GetClaimsAsync(TempUser).Result;
            Assert.Equal(1, Claims.Count);
            Assert.Equal("Something", Claims[0].Type);
            Assert.Equal("SomethingAlso", Claims[0].Value);
        }

        [Fact]
        public void GetEmailAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            string Email = Store.GetEmailAsync(TempUser).Result;
            Assert.Equal("something@somewhere.com", Email);
        }

        [Fact]
        public void GetEmailConfirmedAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            bool EmailConfirmed = Store.GetEmailConfirmedAsync(TempUser).Result;
            Assert.False(EmailConfirmed);
        }

        [Fact]
        public void GetLoginsAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.ExternalLogins.Add(new ExternalLogin() { LoginProvider = "APlace", ProviderKey = "ProviderKey" });
            TempUser.Save();
            IList<Microsoft.AspNet.Identity.UserLoginInfo> Logins = Store.GetLoginsAsync(TempUser).Result;
            Assert.Equal(1, Logins.Count);
            Assert.Equal("APlace", Logins[0].LoginProvider);
            Assert.Equal("ProviderKey", Logins[0].ProviderKey);
        }

        [Fact]
        public void GetPasswordHashAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            string Password = Store.GetPasswordHashAsync(TempUser).Result;
            Assert.Equal(TempUser.PasswordHash, Password);
        }
    }
}