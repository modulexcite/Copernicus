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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copernicus.Models.CRM
{
    /// <summary>
    /// Person API mapping
    /// </summary>
    public class PersonAPIMapping : APIMappingBase<Person>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonAPIMapping"/> class.
        /// </summary>
        public PersonAPIMapping()
            : base(1)
        {
            MapList(x => x.Addresses);
            MapList(x => x.Companies);
            MapList(x => x.Connections);
            MapList(x => x.ContactInfo);
            MapList(x => x.Dates);
            Reference(x => x.FirstName);
            Reference(x => x.Gender);
            MapList(x => x.Images);
            Reference(x => x.LastName);
            Reference(x => x.MiddleName);
            Reference(x => x.Nickname);
            MapList(x => x.Notes);
            Reference(x => x.Prefix);
            MapList(x => x.Schools);
            MapList(x => x.Skills);
            Map(x => x.Status);
            Reference(x => x.Suffix);
            Reference(x => x.Title);
        }
    }

    /// <summary>
    /// Person mapping
    /// </summary>
    public class PersonMapping : ModelMappingBase<Person>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PersonMapping()
            : base()
        {
            ManyToOne(x => x.Addresses).SetCascade();
            ManyToMany(x => x.Companies).SetCascade();
            ManyToOne(x => x.Connections).SetCascade();
            ManyToOne(x => x.ContactInfo).SetCascade();
            ManyToOne(x => x.Dates).SetCascade();
            Reference(x => x.FirstName).SetMaxLength(32).SetNotNull().SetIndex();
            Reference(x => x.Gender).SetMaxLength(1);
            ManyToOne(x => x.Images).SetCascade();
            Reference(x => x.LastName).SetMaxLength(32).SetNotNull().SetIndex();
            Reference(x => x.MiddleName).SetMaxLength(32);
            Reference(x => x.Nickname).SetMaxLength(32).SetIndex();
            ManyToOne(x => x.Notes).SetCascade();
            Reference(x => x.Prefix).SetMaxLength(16);
            ManyToOne(x => x.Schools).SetCascade();
            ManyToMany(x => x.Skills);
            Map(x => x.Status);
            Reference(x => x.Suffix).SetMaxLength(16);
            Reference(x => x.Title).SetMaxLength(48).SetIndex();
        }
    }
}