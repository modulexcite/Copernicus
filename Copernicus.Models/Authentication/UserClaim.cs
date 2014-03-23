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
using Utilities.ORM.Parameters;

namespace Copernicus.Models.Authentication
{
    /// <summary>
    /// User claim
    /// </summary>
    public class UserClaim : ModelBase<UserClaim>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserClaim()
            : base()
        {
            Users = new List<User>();
        }

        /// <summary>
        /// Claim type
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// User associated with the claim
        /// </summary>
        public virtual List<User> Users { get; set; }

        /// <summary>
        /// Claim value
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Loads a specific claim
        /// </summary>
        /// <param name="Type">Claim type</param>
        /// <param name="Value">Claim value</param>
        /// <returns>User claim specified</returns>
        public static UserClaim Load(string Type, string Value)
        {
            return Any(new AndParameter(
                new StringEqualParameter(Type, "Type_", 128),
                new StringEqualParameter(Value, "Value_", 5000)
            ));
        }
    }
}