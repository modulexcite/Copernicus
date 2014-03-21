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

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataTypes.Patterns.BaseClasses;

namespace Copernicus.Models.Authentication.Stores
{
    /// <summary>
    /// User store
    /// </summary>
    public class UserStore : SafeDisposableBaseClass, IUserStore<User, long>, IUserLoginStore<User, long>, IUserPasswordStore<User, long>, IUserSecurityStampStore<User, long>, IUserTwoFactorStore<User, long>, IUserClaimStore<User, long>, IUserEmailStore<User, long>, IUserPhoneNumberStore<User, long>, IUserRoleStore<User, long>, IUserTokenProvider<User, long>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserStore()
        {
        }

        /// <summary>
        /// Adds a claim to a user
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="claim">Claim information</param>
        /// <returns></returns>
        public Task AddClaimAsync(User user, Claim claim)
        {
            claim.
        }

        /// <summary>
        /// Adds external login info
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="login">Login info (from external provider)</param>
        /// <returns>Task</returns>
        public virtual Task AddLoginAsync(User user, UserLoginInfo login)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            Contract.Requires<ArgumentNullException>(login != null, "login");
            return Task.Factory.StartNew(() =>
            {
                ExternalLogin Login = new ExternalLogin();
                Login.User = user;
                Login.ProviderKey = login.ProviderKey;
                Login.LoginProvider = login.LoginProvider;
                Login.Save();
            });
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="user">User to create</param>
        /// <returns>Task</returns>
        public virtual Task CreateAsync(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            return Task.Factory.StartNew(() =>
            {
                user.Save();
            });
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="user">user to delete</param>
        /// <returns>Task</returns>
        public virtual Task DeleteAsync(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            return Task.Factory.StartNew(() =>
            {
                user.Delete();
            });
        }

        /// <summary>
        /// Finds a user based on external login info
        /// </summary>
        /// <param name="login">Login info</param>
        /// <returns>The user specified</returns>
        public virtual Task<User> FindAsync(UserLoginInfo login)
        {
            Contract.Requires<ArgumentNullException>(login != null, "login");
            return Task.Factory.StartNew(() =>
            {
                return ExternalLogin.Load(login.LoginProvider, login.ProviderKey).User;
            });
        }

        public Task<User> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds a user by the ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>User associated with the ID</returns>
        public virtual Task<User> FindByIdAsync(string userId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(userId), "userId");
            long ID = 0;
            if (!long.TryParse(userId, out ID))
                throw new ArgumentException("userId is not the correct type");

            return Task.Factory.StartNew(() =>
            {
                return User.Load(userId);
            });
        }

        public Task<User> FindByIdAsync(long userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds a user by the name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>User associated with the name</returns>
        public virtual Task<User> FindByNameAsync(string userName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(userName), "userName");
            return Task.Factory.StartNew(() =>
            {
                return User.Load(userName);
            });
        }

        public Task<string> GenerateAsync(string purpose, UserManager<User, long> manager, User user)
        {
            throw new NotImplementedException();
        }

        public Task<IList<System.Security.Claims.Claim>> GetClaimsAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets login information for the user specified
        /// </summary>
        /// <param name="user">User to get login info for</param>
        /// <returns>List of login providers associated with the user</returns>
        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            return Task.Factory.StartNew<IList<UserLoginInfo>>(() =>
            {
                return user.ExternalLogins.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList();
            });
        }

        /// <summary>
        /// Gets the password hash
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>The password hash</returns>
        public virtual Task<string> GetPasswordHashAsync(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetPhoneNumberAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the security stamp for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>The security stamp</returns>
        public virtual Task<string> GetSecurityStampAsync(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            return Task.FromResult(user.SecurityStamp);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if the user has a password
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>True if it does, false otherwise</returns>
        public virtual Task<bool> HasPasswordAsync(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsValidProviderForUserAsync(UserManager<User, long> manager, User user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(User user, System.Security.Claims.Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes login info from a user
        /// </summary>
        /// <param name="user">user object</param>
        /// <param name="login">login to remove from the user</param>
        /// <returns>Task</returns>
        public virtual Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            Contract.Requires<ArgumentNullException>(login != null, "login");
            return Task.Factory.StartNew(() =>
            {
                ExternalLogin.Load(login.LoginProvider, login.ProviderKey).Delete();
            });
        }

        public Task SetEmailAsync(User user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the password hash
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="passwordHash">Hashed password</param>
        /// <returns>Task</returns>
        public virtual Task SetPasswordHashAsync(User user, string passwordHash)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the security stamp for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="stamp">Security stamp</param>
        /// <returns>Task</returns>
        public virtual Task SetSecurityStampAsync(User user, string stamp)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>Task</returns>
        public virtual Task UpdateAsync(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null, "user");
            return Task.Factory.StartNew(() =>
            {
                user.Save();
            });
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<User, long> manager, User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Disposes of the object (nothing to dispose of though)
        /// </summary>
        /// <param name="Managed">Managed or not</param>
        protected override void Dispose(bool Managed)
        {
        }
    }
}