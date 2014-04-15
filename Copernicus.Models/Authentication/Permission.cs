/*
CopyPermission (c) 2014 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the Permissions
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyPermission notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYPermission HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using Copernicus.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.ORM.Parameters;

namespace Copernicus.Models.Authentication
{
    /// <summary>
    /// Permissions given to a specific claim
    /// </summary>
    public class Permission : ModelBase<Permission>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Permission" /> class.
        /// </summary>
        public Permission()
            : base()
        {
            Claims = new List<UserClaim>();
        }

        /// <summary>
        /// Gets or sets the claims associated with the permission
        /// </summary>
        /// <value>The claims.</value>
        public virtual List<UserClaim> Claims { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public virtual PermissionType Type { get; set; }

        /// <summary>
        /// Loads the specified display name.
        /// </summary>
        /// <param name="DisplayName">The display name.</param>
        /// <returns>The permission specified</returns>
        public static Permission Load(string DisplayName)
        {
            return Any(new StringEqualParameter(DisplayName, "DisplayName_", 100));
        }

        /// <summary>
        /// Determines whether the specified user has permission.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns><c>True</c> if they do, <c>false</c> otherwise</returns>
        public bool HasPermission(User User)
        {
            return Type == PermissionType.Any ?
                Claims.Any(x => User.Claims.Contains(x)) :
                Claims.All(x => User.Claims.Contains(x));
        }
    }
}