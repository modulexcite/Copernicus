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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Utilities.DataTypes;
using Utilities.DataTypes.Patterns.BaseClasses;

namespace Copernicus.Models.Authentication.Stores
{
    /// <summary>
    /// User store
    /// </summary>
    public class UserStore : SafeDisposableBaseClass, IUserStore<User, long>, IUserLoginStore<User, long>, IUserPasswordStore<User, long>, IUserSecurityStampStore<User, long>, IUserTwoFactorStore<User, long>, IUserClaimStore<User, long>, IUserEmailStore<User, long>, IUserPhoneNumberStore<User, long>, IUserRoleStore<User, long>
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
        public virtual Task AddClaimAsync(User user, Claim claim)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (claim == null) throw new ArgumentNullException("claim");
            return Task.Factory.StartNew(() =>
            {
                UserClaim Claim = new UserClaim()
                {
                    Issuer = claim.Issuer,
                    OriginalIssuer = claim.OriginalIssuer,
                    Type = claim.Type,
                    ValueType = claim.ValueType,
                    Value = claim.Value
                };
                Claim.Users.Add(user);
                Claim.Save();
            });
        }

        /// <summary>
        /// Adds external login info
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="login">Login info (from external provider)</param>
        /// <returns>Task</returns>
        public virtual Task AddLoginAsync(User user, UserLoginInfo login)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (login == null) throw new ArgumentNullException("login");
            return Task.Factory.StartNew(() =>
            {
                ExternalLogin Login = new ExternalLogin();
                Login.User = user;
                Login.ProviderKey = login.ProviderKey;
                Login.LoginProvider = login.LoginProvider;
                Login.Save();
            });
        }

        /// <summary>
        /// Adds a user to a role
        /// </summary>
        /// <param name="user">User to add</param>
        /// <param name="roleName">Role name</param>
        /// <returns>Task</returns>
        public virtual Task AddToRoleAsync(User user, string roleName)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(roleName)) throw new ArgumentNullException("roleName");
            return Task.Factory.StartNew(() =>
            {
                Role TempRole = Role.Load(roleName).Check(() =>
                {
                    Role NewRole = new Role() { Name = roleName };
                    NewRole.Save();
                    return NewRole;
                });
                user.Roles.Add(TempRole);
                user.Save();
            });
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="user">User to create</param>
        /// <returns>Task</returns>
        public virtual Task CreateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
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
            if (user == null) throw new ArgumentNullException("user");
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
            if (login == null) throw new ArgumentNullException("login");
            return Task.Factory.StartNew(() =>
            {
                return ExternalLogin.Load(login.LoginProvider, login.ProviderKey).User;
            });
        }

        /// <summary>
        /// Finds a user based on their email address
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>The user associated with the email address</returns>
        public virtual Task<User> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("email");
            return Task.Factory.StartNew(() =>
            {
                return User.LoadByEmail(email);
            });
        }

        /// <summary>
        /// Finds a user by their ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>The user specified</returns>
        public virtual Task<User> FindByIdAsync(long userId)
        {
            return Task.Factory.StartNew(() =>
            {
                return User.Load(userId);
            });
        }

        /// <summary>
        /// Finds a user by the name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>User associated with the name</returns>
        public virtual Task<User> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("userName");
            return Task.Factory.StartNew(() =>
            {
                return User.Load(userName);
            });
        }

        /// <summary>
        /// Gets claims for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>The claims associated with the user</returns>
        public virtual Task<IList<Claim>> GetClaimsAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.Factory.StartNew(() =>
            {
                return (IList<Claim>)user.Claims.Select(x => new Claim(x.Type, x.Value, x.ValueType, x.Issuer, x.OriginalIssuer)).ToList();
            });
        }

        /// <summary>
        /// Gets the email address for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>The email address for the user</returns>
        public virtual Task<string> GetEmailAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// Gets whether or not the email address has been confirmed
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>True if it is confirmed, false otherwise</returns>
        public virtual Task<bool> GetEmailConfirmedAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>
        /// Gets login information for the user specified
        /// </summary>
        /// <param name="user">User to get login info for</param>
        /// <returns>List of login providers associated with the user</returns>
        public virtual Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
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
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>
        /// Gets the phone number for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>The phone number for the user</returns>
        public virtual Task<string> GetPhoneNumberAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.PhoneNumber);
        }

        /// <summary>
        /// Gets whether the phone number has been confirmed
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>True if it is confirmed, false otherwise</returns>
        public virtual Task<bool> GetPhoneNumberConfirmedAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.PhoneConfirmed);
        }

        /// <summary>
        /// Gets the roles for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>The roles associated with the user</returns>
        public virtual Task<IList<string>> GetRolesAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult((IList<string>)user.Roles.Select(x => x.Name));
        }

        /// <summary>
        /// Gets the security stamp for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>The security stamp</returns>
        public virtual Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.SecurityStamp);
        }

        /// <summary>
        /// Gets whether two factor authentication is enabled
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>True if it is enabled, false otherwise</returns>
        public virtual Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.TwoFactorEnabled);
        }

        /// <summary>
        /// Determines if the user has a password
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns>True if it does, false otherwise</returns>
        public virtual Task<bool> HasPasswordAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        /// <summary>
        /// Determines if the user is in the role
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="roleName">Role name</param>
        /// <returns>True if they are, false otherwise</returns>
        public virtual Task<bool> IsInRoleAsync(User user, string roleName)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(roleName)) throw new ArgumentNullException("roleName");
            return Task.FromResult(user.Roles.Any(x => string.Equals(x.Name, roleName, StringComparison.InvariantCultureIgnoreCase)));
        }

        /// <summary>
        /// Removes a claim from the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="claim">Claim to remove</param>
        /// <returns>Task</returns>
        public virtual Task RemoveClaimAsync(User user, Claim claim)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (claim == null) throw new ArgumentNullException("claim");
            return Task.Factory.StartNew(() =>
            {
                UserClaim TempClaim = user.Claims.FirstOrDefault(x => string.Equals(x.Issuer, claim.Issuer, StringComparison.InvariantCultureIgnoreCase)
                    && string.Equals(x.OriginalIssuer, claim.OriginalIssuer, StringComparison.InvariantCultureIgnoreCase)
                    && string.Equals(x.Type, claim.Type, StringComparison.InvariantCultureIgnoreCase)
                    && string.Equals(x.Value, claim.Value, StringComparison.InvariantCultureIgnoreCase)
                    && string.Equals(x.ValueType, claim.ValueType, StringComparison.InvariantCultureIgnoreCase));
                if (TempClaim != null)
                    TempClaim.Delete();
            });
        }

        /// <summary>
        /// Removes a user from a role
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="roleName">Role name</param>
        /// <returns>Task</returns>
        public virtual Task RemoveFromRoleAsync(User user, string roleName)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(roleName)) throw new ArgumentNullException("roleName");
            return Task.Factory.StartNew(() =>
            {
                user.Roles.Remove(Role.Load(roleName));
                user.Save();
            });
        }

        /// <summary>
        /// Removes login info from a user
        /// </summary>
        /// <param name="user">user object</param>
        /// <param name="login">login to remove from the user</param>
        /// <returns>Task</returns>
        public virtual Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (login == null) throw new ArgumentNullException("login");
            return Task.Factory.StartNew(() =>
            {
                ExternalLogin.Load(login.LoginProvider, login.ProviderKey).Delete();
            });
        }

        /// <summary>
        /// Sets the email address
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="email">Email address</param>
        /// <returns>Task</returns>
        public virtual Task SetEmailAsync(User user, string email)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("email");
            user.Email = email;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Sets email confirmed for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="confirmed">Confirmed</param>
        /// <returns>Task</returns>
        public virtual Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Sets the password hash
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="passwordHash">Hashed password</param>
        /// <returns>Task</returns>
        public virtual Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(passwordHash)) throw new ArgumentNullException("passwordHash");
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Sets the phone number for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>Task</returns>
        public virtual Task SetPhoneNumberAsync(User user, string phoneNumber)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException("phoneNumber");
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Sets the phone number confirmed for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="confirmed">Confirmed</param>
        /// <returns>Task</returns>
        public virtual Task SetPhoneNumberConfirmedAsync(User user, bool confirmed)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.PhoneConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Sets the security stamp for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="stamp">Security stamp</param>
        /// <returns>Task</returns>
        public virtual Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(stamp)) throw new ArgumentNullException("stamp");
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Sets two factor enabled for the user
        /// </summary>
        /// <param name="user">User object</param>
        /// <param name="enabled">Enabled</param>
        /// <returns>Task</returns>
        public virtual Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>Task</returns>
        public virtual Task UpdateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.Factory.StartNew(() =>
            {
                user.Save();
            });
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