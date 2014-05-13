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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Copernicus.Models.BaseClasses;
using Utilities.Validation;

namespace Copernicus.Models.CRM
{
    /// <summary>
    /// Education class
    /// </summary>
    public class Education : ModelBase<Education>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Education" /> class.
        /// </summary>
        public Education()
            : base()
        {
            this.Start = new DateTime(1900, 1, 1);
            this.End = new DateTime(1900, 1, 1);
        }

        /// <summary>
        /// Gets or sets the degree.
        /// </summary>
        /// <value>The degree.</value>
        [System.ComponentModel.DataAnnotations.MaxLength(64)]
        public virtual string Degree { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>The end.</value>
        [Between("1/1/1900", "1/1/2100")]
        public virtual DateTime End { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        /// <value>The person.</value>
        [Required]
        public virtual Person Person { get; set; }

        /// <summary>
        /// Gets or sets the school.
        /// </summary>
        /// <value>The school.</value>
        [Required]
        public virtual School School { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>The start.</value>
        [Between("1/1/1900", "1/1/2100")]
        public virtual DateTime Start { get; set; }
    }
}