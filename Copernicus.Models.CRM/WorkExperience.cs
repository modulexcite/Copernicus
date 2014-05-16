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
    /// Work experience class
    /// </summary>
    public class WorkExperience : ModelBase<WorkExperience>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkExperience" /> class.
        /// </summary>
        public WorkExperience()
            : base()
        {
        }

        /// <summary>
        /// Company
        /// </summary>
        /// <value>The company.</value>
        [Required]
        public virtual Company Company { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <value>The description.</value>
        [System.ComponentModel.DataAnnotations.MaxLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        /// <value>The end.</value>
        [Between("1/1/1900", "1/1/2100")]
        public DateTime End { get; set; }

        /// <summary>
        /// Start date
        /// </summary>
        /// <value>The start.</value>
        [Between("1/1/1900", "1/1/2100")]
        public DateTime Start { get; set; }
    }
}