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
    /// Note API mapping
    /// </summary>
    public class NoteAPIMapping : APIMappingBase<Note>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteAPIMapping"/> class.
        /// </summary>
        public NoteAPIMapping()
            : base(1)
        {
            Map(x => x.Creator);
            Map(x => x.Modifier);
            Reference(x => x.Content);
        }
    }

    /// <summary>
    /// Note mapping
    /// </summary>
    public class NoteMapping : ModelMappingBase<Note>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteMapping" /> class.
        /// </summary>
        public NoteMapping()
            : base()
        {
            Reference(x => x.Content).SetMaxLength(512).SetNotNull();
        }
    }
}