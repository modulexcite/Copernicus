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

namespace Copernicus.Models.Content
{
    /// <summary>
    /// Photo class
    /// </summary>
    public class Photo : ModelBase<Photo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Photo" /> class.
        /// </summary>
        public Photo()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Photo" /> class.
        /// </summary>
        /// <param name="Location">The location.</param>
        /// <param name="Name">The name.</param>
        public Photo(string Location, string Name)
            : this()
        {
            this.Location = Location;
            this.Name = Name;
        }

        /// <summary>
        /// Location
        /// </summary>
        /// <value>The location.</value>
        [Required]
        [System.ComponentModel.DataAnnotations.MaxLength(128)]
        public string Location { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [System.ComponentModel.DataAnnotations.MaxLength(128)]
        public string Name { get; set; }
    }
}