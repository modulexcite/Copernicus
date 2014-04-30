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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Copernicus.Models.BaseClasses;
using Copernicus.Models.Data;
using Copernicus.Models.General;

namespace Copernicus.Models.Data
{
    /// <summary>
    /// Class to help generate properties that the user specifies
    /// </summary>
    public class Property : ModelBase<Property>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Property" /> class.
        /// </summary>
        public Property()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        public virtual LookUp DataType { get; set; }

        /// <summary>
        /// Gets or sets the foreign model.
        /// </summary>
        /// <value>The foreign model.</value>
        public virtual Model ForeignModel { get; set; }

        /// <summary>
        /// Gets or sets the maximum size.
        /// </summary>
        /// <value>The maximum size.</value>
        public virtual int MaxSize { get; set; }

        /// <summary>
        /// Gets or sets the minimum size.
        /// </summary>
        /// <value>The minimum size.</value>
        public virtual int MinSize { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent model.
        /// </summary>
        /// <value>The parent model.</value>
        public virtual Model ParentModel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Property" /> is required.
        /// </summary>
        /// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        public virtual bool Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Property" /> is unique.
        /// </summary>
        /// <value><c>true</c> if unique; otherwise, <c>false</c>.</value>
        public virtual bool Unique { get; set; }
    }
}