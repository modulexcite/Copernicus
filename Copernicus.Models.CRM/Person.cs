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
using Utilities.ORM.Parameters;

namespace Copernicus.Models.CRM
{
    /// <summary>
    /// Person profile
    /// </summary>
    public class Person : ModelBase<Person>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person" /> class.
        /// </summary>
        public Person()
            : base()
        {
            this.Schools = new List<Education>();
            this.Addresses = new List<Address>();
            this.Companies = new List<WorkExperience>();
            this.Connections = new List<Relationship>();
            this.ContactInfo = new List<ContactInfo>();
            this.Dates = new List<ImportantDates>();
            this.Images = new List<Photo>();
            this.Schools = new List<Education>();
            this.Skills = new List<Skill>();
        }

        /// <summary>
        /// Addresses
        /// </summary>
        /// <value>The addresses.</value>
        [Utilities.Validation.Cascade]
        public virtual List<Address> Addresses { get; set; }

        /// <summary>
        /// Companies
        /// </summary>
        /// <value>The companies.</value>
        [Utilities.Validation.Cascade]
        public virtual List<WorkExperience> Companies { get; set; }

        /// <summary>
        /// Connections
        /// </summary>
        /// <value>The connections.</value>
        [Utilities.Validation.Cascade]
        public virtual List<Relationship> Connections { get; set; }

        /// <summary>
        /// Contact info
        /// </summary>
        /// <value>The contact information.</value>
        [Utilities.Validation.Cascade]
        public virtual List<ContactInfo> ContactInfo { get; set; }

        /// <summary>
        /// Dates
        /// </summary>
        /// <value>The dates.</value>
        [Utilities.Validation.Cascade]
        public virtual List<ImportantDates> Dates { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        /// <value>The gender.</value>
        [MaxLength(1)]
        public string Gender { get; set; }

        /// <summary>
        /// Images
        /// </summary>
        /// <value>The images.</value>
        [Utilities.Validation.Cascade]
        public virtual List<Photo> Images { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required]
        [MaxLength(32)]
        public string LastName { get; set; }

        /// <summary>
        /// Middle name
        /// </summary>
        /// <value>The name of the middle.</value>
        [MaxLength(32)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Nickname
        /// </summary>
        /// <value>The nickname.</value>
        [MaxLength(32)]
        public string Nickname { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        /// <value>The notes.</value>
        [Utilities.Validation.Cascade]
        public virtual List<Note> Notes { get; set; }

        /// <summary>
        /// Prefix
        /// </summary>
        /// <value>The prefix.</value>
        [MaxLength(16)]
        public string Prefix { get; set; }

        /// <summary>
        /// Schools
        /// </summary>
        /// <value>The schools.</value>
        [Utilities.Validation.Cascade]
        public virtual List<Education> Schools { get; set; }

        /// <summary>
        /// Skills
        /// </summary>
        /// <value>The skills.</value>
        public virtual List<Skill> Skills { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        /// <value>The status.</value>
        public virtual LookUp Status { get; set; }

        /// <summary>
        /// Suffix
        /// </summary>
        /// <value>The suffix.</value>
        [MaxLength(16)]
        public string Suffix { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        /// <value>The title.</value>
        [MaxLength(48)]
        public string Title { get; set; }

        /// <summary>
        /// Searches for a person based on the partial name/title specified
        /// </summary>
        /// <param name="Name">Partial name to search on</param>
        /// <returns>List of people that match the search term</returns>
        public static IEnumerable<Person> Search(string Name)
        {
            return All(new OrParameter(
                            new LikeParameter("%" + Name + "%", "Title_", 256),
                            new OrParameter(
                                new LikeParameter(Name + "%", "Nickname_", 256),
                                new OrParameter(
                                    new LikeParameter(Name + "%", "FirstName_", 256),
                                    new LikeParameter(Name + "%", "LastName_", 256)
                                    )
                                )
                            )
                        );
        }
    }
}