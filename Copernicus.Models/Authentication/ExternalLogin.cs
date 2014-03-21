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
    /// External login (OpenID, etc.)
    /// </summary>
    public class ExternalLogin : ModelBase<ExternalLogin>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExternalLogin()
            : base()
        {
        }

        /// <summary>
        /// Login provider
        /// </summary>
        [Required]
        [MinLength(1)]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// Provider key
        /// </summary>
        [Required]
        [MinLength(1)]
        public virtual string ProviderKey { get; set; }

        /// <summary>
        /// User associated with the login
        /// </summary>
        [Required]
        public virtual User User { get; set; }

        /// <summary>
        /// Loads a specific external login based on the info specified
        /// </summary>
        /// <param name="LoginProvider">Login provider</param>
        /// <param name="ProviderKey">Provider key</param>
        /// <returns>External login specified</returns>
        public static ExternalLogin Load(string LoginProvider, string ProviderKey)
        {
            return Any(new AndParameter(
                        new StringEqualParameter(LoginProvider, "LoginProvider_", 256),
                        new StringEqualParameter(ProviderKey, "ProviderKey_", 256)
                      ));
        }
    }
}