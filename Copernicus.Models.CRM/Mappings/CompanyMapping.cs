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
    /// Company API mapping
    /// </summary>
    public class CompanyAPIMapping : APIMappingBase<Company>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyAPIMapping"/> class.
        /// </summary>
        public CompanyAPIMapping()
            : base(1)
        {
            MapList(x => x.Addresses);
            MapList(x => x.Employees);
            MapList(x => x.Images);
            Reference(x => x.Name);
            MapList(x => x.Notes);
            Map(x => x.Status);
            Map(x => x.Type);
            MapList(x => x.Subsidiaries);
            Map(x => x.ParentCompany);
        }
    }

    /// <summary>
    /// Company mapping
    /// </summary>
    public class CompanyMapping : ModelMappingBase<Company>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyMapping()
            : base()
        {
            ManyToOne(x => x.Addresses).SetCascade();
            ManyToMany(x => x.Employees);
            ManyToOne(x => x.Images).SetCascade();
            Reference(x => x.Name).SetMaxLength(64).SetNotNull();
            ManyToOne(x => x.Notes).SetCascade();
            Map(x => x.Status);
            Map(x => x.Type);
            ManyToOne(x => x.Subsidiaries);
            ManyToOne(x => x.ParentCompany);
        }
    }
}