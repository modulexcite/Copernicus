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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Copernicus.Models.Authentication;
using Utilities.DataTypes.Patterns.BaseClasses;
using Utilities.IO;
using Xunit;

namespace Copernicus.Models.Tests.Authentication.Stores
{
    /// <summary>
    /// User store
    /// </summary>
    public class UserStore : SafeDisposableBaseClass
    {
        public UserStore()
        {
            var BootLoader = Utilities.IoC.Manager.Bootstrapper;
            new Utilities.ORM.Manager.ORMManager(BootLoader.Resolve<Utilities.ORM.Manager.Mapper.Manager>(),
                BootLoader.Resolve<Utilities.ORM.Manager.QueryProvider.Manager>(),
                BootLoader.Resolve<Utilities.ORM.Manager.Schema.Manager>(),
                BootLoader.Resolve<Utilities.ORM.Manager.SourceProvider.Manager>(),
                BootLoader.ResolveAll<Utilities.ORM.Interfaces.IDatabase>());
            Store = new Models.Authentication.Stores.UserStore();
        }

        private Copernicus.Models.Authentication.Stores.UserStore Store { get; set; }

        [Fact]
        public void AddClaimAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.AddClaimAsync(TempUser, new Claim(ClaimTypes.AuthenticationMethod, "Windows")).Wait();
            Assert.Equal(1, TempUser.Claims.Count);
            Assert.Equal("Windows", TempUser.Claims[0].Value);
            Assert.Equal(ClaimTypes.AuthenticationMethod, TempUser.Claims[0].Type);
            Assert.Throws<ArgumentNullException>(() => Store.AddClaimAsync(null, new Claim(ClaimTypes.AuthenticationMethod, "Windows")).Wait());
            Assert.Throws<ArgumentNullException>(() => Store.AddClaimAsync(TempUser, null).Wait());
        }

        [Fact]
        public void AddLoginAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.AddLoginAsync(TempUser, new Microsoft.AspNet.Identity.UserLoginInfo("Something", "A Key")).Wait();
            Assert.Equal(1, TempUser.ExternalLogins.Count);
            Assert.Equal("Something", TempUser.ExternalLogins[0].LoginProvider);
            Assert.Equal("A Key", TempUser.ExternalLogins[0].ProviderKey);
            Assert.Throws<ArgumentNullException>(() => Store.AddLoginAsync(TempUser, null).Wait());
            Assert.Throws<ArgumentNullException>(() => Store.AddLoginAsync(null, new Microsoft.AspNet.Identity.UserLoginInfo("Something", "A Key")).Wait());
        }

        [Fact]
        public void CreateAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            Store.CreateAsync(TempUser).Wait();
            Assert.Equal(1, User.All().Count());
            Assert.Throws<ArgumentNullException>(() => Store.CreateAsync(null).Wait());
        }

        [Fact]
        public void DeleteAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.DeleteAsync(TempUser).Wait();
            Assert.Equal(0, User.All().Count());
            Assert.Throws<ArgumentNullException>(() => Store.DeleteAsync(null).Wait());
        }

        [Fact]
        public void FindAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            TempUser.ExternalLogins.Add(new ExternalLogin() { LoginProvider = "TestProvider", ProviderKey = "TestKey", User = TempUser });
            TempUser.Save();
            User TempUser2 = null;
            { TempUser2 = Store.FindAsync(new Microsoft.AspNet.Identity.UserLoginInfo("TestProvider", "TestKey")).Result; };
            Assert.Equal("TestUser", TempUser2.UserName);
            Assert.Equal(TempUser.PasswordHash, TempUser2.PasswordHash);
            Assert.Equal(TempUser.ExternalLogins[0].ID, TempUser2.ExternalLogins[0].ID);
            Assert.Throws<ArgumentNullException>(() => { TempUser2 = Store.FindAsync(null).Result; });
        }

        [Fact]
        public void FindByEmailAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            User TempUser2 = null;
            TempUser2 = Store.FindByEmailAsync("something@somewhere.com").Result;
            Assert.Equal(TempUser.ID, TempUser2.ID);
            Assert.Equal(TempUser.UserName, TempUser2.UserName);
            Assert.Throws<ArgumentNullException>(() => TempUser2 = Store.FindByEmailAsync(null).Result);
        }

        [Fact]
        public void FindByIdAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            User TempUser2 = null;
            TempUser2 = Store.FindByIdAsync(TempUser.ID).Result;
            Assert.Equal(TempUser.ID, TempUser2.ID);
            Assert.Equal(TempUser.UserName, TempUser2.UserName);
        }

        [Fact]
        public void FindByNameAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            User TempUser2 = null;
            TempUser2 = Store.FindByNameAsync(TempUser.UserName).Result;
            Assert.Equal(TempUser.ID, TempUser2.ID);
            Assert.Equal(TempUser.UserName, TempUser2.UserName);
            Assert.Throws<ArgumentNullException>(() => TempUser2 = Store.FindByNameAsync(null).Result);
        }

        [Fact]
        public void GetClaimsAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Claims.Add(new UserClaim() { Type = "Something", Value = "SomethingAlso" });
            TempUser.Save();
            TempUser = User.Load(TempUser.ID);
            IList<Claim> Claims = null;
            Claims = Store.GetClaimsAsync(TempUser).Result;
            Assert.Equal(1, Claims.Count);
            Assert.Equal("Something", Claims[0].Type);
            Assert.Equal("SomethingAlso", Claims[0].Value);
            Assert.Throws<ArgumentNullException>(() => Claims = Store.GetClaimsAsync(null).Result);
        }

        [Fact]
        public void GetEmailAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            string Email = "";
            Email = Store.GetEmailAsync(TempUser).Result;
            Assert.Equal("something@somewhere.com", Email);
            Assert.Throws<ArgumentNullException>(() => Email = Store.GetEmailAsync(null).Result);
        }

        [Fact]
        public void GetEmailConfirmedAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            bool EmailConfirmed = false;
            EmailConfirmed = Store.GetEmailConfirmedAsync(TempUser).Result;
            Assert.False(EmailConfirmed);
            Assert.Throws<ArgumentNullException>(() => EmailConfirmed = Store.GetEmailConfirmedAsync(null).Result);
        }

        [Fact]
        public void GetLoginsAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.ExternalLogins.Add(new ExternalLogin() { LoginProvider = "APlace", ProviderKey = "ProviderKey" });
            TempUser.Save();
            IList<Microsoft.AspNet.Identity.UserLoginInfo> Logins = null;
            Logins = Store.GetLoginsAsync(TempUser).Result;
            Assert.Equal(1, Logins.Count);
            Assert.Equal("APlace", Logins[0].LoginProvider);
            Assert.Equal("ProviderKey", Logins[0].ProviderKey);
            Assert.Throws<ArgumentNullException>(() => Logins = Store.GetLoginsAsync(null).Result);
        }

        [Fact]
        public void GetPasswordHashAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com" };
            TempUser.Save();
            string Password = "";
            Password = Store.GetPasswordHashAsync(TempUser).Result;
            Assert.Equal(TempUser.PasswordHash, Password);
            Assert.Throws<ArgumentNullException>(() => Password = Store.GetPasswordHashAsync(null).Result);
        }

        [Fact]
        public void GetPhoneNumberAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555" };
            TempUser.Save();
            string PhoneNumber = "";
            PhoneNumber = Store.GetPhoneNumberAsync(TempUser).Result;
            Assert.Equal(TempUser.PhoneNumber, PhoneNumber);
            Assert.Throws<ArgumentNullException>(() => PhoneNumber = Store.GetPhoneNumberAsync(null).Result);
        }

        [Fact]
        public void GetPhoneNumberConfirmedAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555" };
            TempUser.Save();
            bool PhoneNumberConfirmed = false;
            PhoneNumberConfirmed = Store.GetPhoneNumberConfirmedAsync(TempUser).Result;
            Assert.Equal(TempUser.PhoneConfirmed, PhoneNumberConfirmed);
            Assert.Throws<ArgumentNullException>(() => PhoneNumberConfirmed = Store.GetPhoneNumberConfirmedAsync(null).Result);
        }

        [Fact]
        public void GetSecurityStampAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            string SecurityStamp = "";
            SecurityStamp = Store.GetSecurityStampAsync(TempUser).Result;
            Assert.Equal(TempUser.SecurityStamp, SecurityStamp);
            Assert.Throws<ArgumentNullException>(() => SecurityStamp = Store.GetSecurityStampAsync(null).Result);
        }

        [Fact]
        public void GetTwoFactorEnabledAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            bool TwoFactorEnabled = false;
            TwoFactorEnabled = Store.GetTwoFactorEnabledAsync(TempUser).Result;
            Assert.Equal(TempUser.TwoFactorEnabled, TwoFactorEnabled);
            Assert.Throws<ArgumentNullException>(() => TwoFactorEnabled = Store.GetTwoFactorEnabledAsync(null).Result);
        }

        [Fact]
        public void HasPasswordAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Assert.True(Store.HasPasswordAsync(TempUser).Result);
            Assert.Throws<ArgumentNullException>(() => Store.HasPasswordAsync(null).Result);
        }

        [Fact]
        public void RemoveClaimAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.AddClaimAsync(TempUser, new Claim(ClaimTypes.AuthenticationMethod, "Windows")).Wait();
            Assert.Equal(1, TempUser.Claims.Count);
            Assert.Equal("Windows", TempUser.Claims[0].Value);
            Assert.Equal(ClaimTypes.AuthenticationMethod, TempUser.Claims[0].Type);
            Store.RemoveClaimAsync(TempUser, new Claim(ClaimTypes.AuthenticationMethod, "Windows")).Wait();
            Assert.Equal(0, TempUser.Claims.Count);
            Assert.Throws<ArgumentNullException>(() => Store.RemoveClaimAsync(TempUser, null).Wait());
            Assert.Throws<ArgumentNullException>(() => Store.RemoveClaimAsync(null, new Claim(ClaimTypes.AuthenticationMethod, "Windows")).Wait());
            Assert.Throws<ArgumentNullException>(() => Store.RemoveClaimAsync(null, null).Wait());
        }

        [Fact]
        public void RemoveLoginAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.AddLoginAsync(TempUser, new Microsoft.AspNet.Identity.UserLoginInfo("Something", "A Key")).Wait();
            Assert.Equal(1, TempUser.ExternalLogins.Count);
            Assert.Equal("Something", TempUser.ExternalLogins[0].LoginProvider);
            Assert.Equal("A Key", TempUser.ExternalLogins[0].ProviderKey);
            Store.RemoveLoginAsync(TempUser, new Microsoft.AspNet.Identity.UserLoginInfo("Something", "A Key")).Wait();
            Assert.Equal(0, TempUser.ExternalLogins.Count);
            Assert.Throws<ArgumentNullException>(() => Store.RemoveLoginAsync(null, new Microsoft.AspNet.Identity.UserLoginInfo("Something", "A Key")).Wait());
            Assert.Throws<ArgumentNullException>(() => Store.RemoveLoginAsync(TempUser, null).Wait());
            Assert.Throws<ArgumentNullException>(() => Store.RemoveLoginAsync(null, null).Wait());
        }

        [Fact]
        public void SetEmailAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.SetEmailAsync(TempUser, "something2@somewhere.com").Wait();
            Assert.Equal("something2@somewhere.com", TempUser.Email);
            Assert.Throws<ArgumentNullException>(() => Store.SetEmailAsync(null, "something2@somewhere.com").Wait());
            Assert.Throws<ArgumentNullException>(() => Store.SetEmailAsync(TempUser, null).Wait());
        }

        [Fact]
        public void SetEmailConfirmedAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.SetEmailConfirmedAsync(TempUser, true).Wait();
            Assert.True(TempUser.EmailConfirmed);
            Assert.Throws<ArgumentNullException>(() => Store.SetEmailConfirmedAsync(null, true).Wait());
        }

        [Fact]
        public void SetPasswordHashAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.SetPasswordHashAsync(TempUser, "ASDFGHIJKL").Wait();
            Assert.Equal("ASDFGHIJKL", TempUser.PasswordHash);
            Assert.Throws<ArgumentNullException>(() => Store.SetPasswordHashAsync(null, "ASDFGHIJKL").Wait());
            Assert.Throws<ArgumentNullException>(() => Store.SetPasswordHashAsync(TempUser, null).Wait());
        }

        [Fact]
        public void SetPhoneNumberAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.SetPhoneNumberAsync(TempUser, "(555)555-1111").Wait();
            Assert.Equal("(555)555-1111", TempUser.PhoneNumber);
            Assert.Throws<ArgumentNullException>(() => Store.SetPhoneNumberAsync(null, "(555)555-1111").Wait());
            Assert.Throws<ArgumentNullException>(() => Store.SetPhoneNumberAsync(TempUser, null).Wait());
        }

        [Fact]
        public void SetPhoneNumberConfirmedAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.SetPhoneNumberConfirmedAsync(TempUser, true).Wait();
            Assert.True(TempUser.PhoneConfirmed);
            Assert.Throws<ArgumentNullException>(() => Store.SetPhoneNumberConfirmedAsync(null, true).Wait());
        }

        [Fact]
        public void SetSecurityStampAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.SetSecurityStampAsync(TempUser, "ASDFGHIJKL").Wait();
            Assert.Equal("ASDFGHIJKL", TempUser.SecurityStamp);
            Assert.Throws<ArgumentNullException>(() => Store.SetSecurityStampAsync(null, "ASDFGHIJKL").Wait());
            Assert.Throws<ArgumentNullException>(() => Store.SetSecurityStampAsync(TempUser, null).Wait());
        }

        [Fact]
        public void SetTwoFactorEnabledAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            Store.SetTwoFactorEnabledAsync(TempUser, true).Wait();
            Assert.True(TempUser.TwoFactorEnabled);
            Assert.Throws<ArgumentNullException>(() => Store.SetTwoFactorEnabledAsync(null, true).Wait());
        }

        [Fact]
        public void UpdateAsync()
        {
            User TempUser = new User() { UserName = "TestUser", PasswordHash = Guid.NewGuid().ToString().Hash(), Email = "something@somewhere.com", PhoneNumber = "(555)555-5555", SecurityStamp = Guid.NewGuid().ToString().Hash() };
            TempUser.Save();
            TempUser.Email = "MyNewEmail@somewhere.com";
            Store.UpdateAsync(TempUser).Wait();
            TempUser = User.Load(TempUser.ID);
            Assert.Equal("MyNewEmail@somewhere.com", TempUser.Email);
            Assert.Equal("TestUser", TempUser.UserName);
            Assert.Throws<ArgumentNullException>(() => Store.UpdateAsync(null).Wait());
        }

        protected override void Dispose(bool Managed)
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