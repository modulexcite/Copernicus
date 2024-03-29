﻿/*
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copernicus.Models.Authentication.Mappings
{
    /// <summary>
    /// User API mapping
    /// </summary>
    public class UserAPIMapping : APIMappingBase<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAPIMapping" /> class.
        /// </summary>
        public UserAPIMapping()
            : base(1)
        {
            Reference(x => x.UserName);
            Reference(x => x.Email);
            this.SetCanDelete(x => false);
            this.SetCanSave(x => false);
        }
    }

    /// <summary>
    /// User mapping
    /// </summary>
    public class UserMapping : ModelMappingBase<User>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserMapping()
            : base()
        {
            ManyToMany(x => x.Claims).SetCascade();
            Reference(x => x.Email).SetMaxLength(256);
            Reference(x => x.EmailConfirmed);
            ManyToOne(x => x.ExternalLogins).SetCascade();
            Reference(x => x.PasswordHash).SetMaxLength(5000);
            Reference(x => x.PhoneConfirmed);
            Reference(x => x.PhoneNumber).SetMaxLength(40);
            Reference(x => x.SecurityStamp).SetMaxLength(5000);
            Reference(x => x.TwoFactorEnabled);
            Reference(x => x.UserName).SetMaxLength(256);
        }
    }
}