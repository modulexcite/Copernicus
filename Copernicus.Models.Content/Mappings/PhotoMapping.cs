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

namespace Copernicus.Models.Content.Mappings
{
    /// <summary>
    /// Photo API mapping
    /// </summary>
    public class PhotoAPIMapping : APIMappingBase<Photo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoAPIMapping"/> class.
        /// </summary>
        public PhotoAPIMapping()
            : base(1)
        {
            Map(x => x.Creator);
            Map(x => x.Modifier);
            Reference(x => x.Name);
        }
    }

    /// <summary>
    /// Photo mapping
    /// </summary>
    public class PhotoMapping : ModelMappingBase<Photo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoMapping" /> class.
        /// </summary>
        public PhotoMapping()
            : base()
        {
            Reference(x => x.Location).SetMaxLength(128).SetNotNull();
            Reference(x => x.Name).SetMaxLength(128).SetNotNull();
        }
    }
}