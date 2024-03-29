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

namespace Copernicus.Models.CRM
{
    /// <summary>
    /// Education API mapping
    /// </summary>
    public class EducationAPIMapping : APIMappingBase<Education>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EducationAPIMapping"/> class.
        /// </summary>
        public EducationAPIMapping()
            : base(1)
        {
            Reference(x => x.Degree);
            Reference(x => x.End);
            Map(x => x.School);
            Reference(x => x.Start);
            Map(x => x.Person);
        }
    }

    /// <summary>
    /// Education mapping
    /// </summary>
    public class EducationMapping : ModelMappingBase<Education>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EducationMapping()
            : base()
        {
            Reference(x => x.Degree).SetMaxLength(64);
            Reference(x => x.End).SetDefaultValue(() => new DateTime(1900, 1, 1));
            ManyToOne(x => x.School);
            Reference(x => x.Start).SetDefaultValue(() => new DateTime(1900, 1, 1));
            ManyToOne(x => x.Person);
        }
    }
}