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

using Copernicus.Models.BaseClasses;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilities.ORM.Parameters;

namespace Copernicus.Models.Authentication
{
    /// <summary>
    /// User class
    /// </summary>
    public class User : ModelBase<User>, IUser<long>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public User()
            : base()
        {
            ExternalLogins = new List<ExternalLogin>();
            Claims = new List<UserClaim>();
        }

        /// <summary>
        /// User claims
        /// </summary>
        public virtual List<UserClaim> Claims { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Has the email been confirmed?
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// External logins associated with the user
        /// </summary>
        public virtual List<ExternalLogin> ExternalLogins { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public virtual long Id { get { return this.ID; } }

        /// <summary>
        /// Password hash
        /// </summary>
        [Required]
        [Utilities.Validation.MinLength(1)]
        public virtual string PasswordHash { get; set; }

        /// <summary>
        /// Phone number confirmed
        /// </summary>
        public virtual bool PhoneConfirmed { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Security stamp
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// Is two factor authentication enabled?
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [Utilities.Validation.MinLength(1)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// Loads a specific user based on the username specified
        /// </summary>
        /// <param name="UserName">Username specified</param>
        /// <returns>User associated with the user name</returns>
        public static User Load(string UserName)
        {
            return Any(new StringEqualParameter(UserName, "UserName_", 256));
        }

        /// <summary>
        /// Loads a specific user based on the email specified
        /// </summary>
        /// <param name="Email">Email address</param>
        /// <returns>User associated with the email address</returns>
        public static User LoadByEmail(string Email)
        {
            return Any(new StringEqualParameter(Email, "Email_", 256));
        }

        /// <summary>
        /// Loads the current user
        /// </summary>
        /// <returns>The current user</returns>
        public static User LoadCurrentUser()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity != null)
            {
                string[] Names = HttpContext.Current.User.Identity.Name.Split('\\');
                return Load(Names[Names.Length - 1]);
            }
            return null;
        }

        /// <summary>
        /// Loads users based on the beginning of the user name
        /// </summary>
        /// <param name="UserName">User name beginning</param>
        /// <returns>A list of users</returns>
        public static IEnumerable<User> LoadSimilar(string UserName)
        {
            return All(new LikeParameter(UserName + "%", "UserName_", 256));
        }

        /// <summary>
        /// Converts the object to a string
        /// </summary>
        /// <returns>The object as a string</returns>
        public override string ToString()
        {
            return UserName;
        }
    }
}