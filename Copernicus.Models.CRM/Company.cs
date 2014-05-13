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
using Copernicus.Models.Content;
using Copernicus.Models.General;
using Utilities.Validation;

namespace Copernicus.Models.CRM
{
    /// <summary>
    /// Company info
    /// </summary>
    public class Company : ModelBase<Company>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Company" /> class.
        /// </summary>
        public Company()
            : base()
        {
            this.Addresses = new List<Address>();
            this.Employees = new List<WorkExperience>();
            this.Images = new List<Photo>();
            this.Notes = new List<Note>();
            this.Subsidiaries = new List<Company>();
        }

        /// <summary>
        /// Gets or sets the addresses.
        /// </summary>
        /// <value>The addresses.</value>
        [Cascade]
        public virtual List<Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>The employees.</value>
        public virtual List<WorkExperience> Employees { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>The images.</value>
        [Cascade]
        public virtual List<Photo> Images { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [Cascade]
        public virtual List<Note> Notes { get; set; }

        /// <summary>
        /// Gets or sets the parent company.
        /// </summary>
        /// <value>The parent company.</value>
        public virtual Company ParentCompany { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [Required]
        public virtual LookUp Status { get; set; }

        /// <summary>
        /// Gets or sets the subsidiaries.
        /// </summary>
        /// <value>The subsidiaries.</value>
        [Cascade]
        public virtual List<Company> Subsidiaries { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [Required]
        public virtual LookUp Type { get; set; }
    }
}