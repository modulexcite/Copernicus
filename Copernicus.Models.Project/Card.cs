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

using Copernicus.Models.Authentication;
using Copernicus.Models.BaseClasses;
using Copernicus.Models.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Validation;

namespace Copernicus.Models.Project
{
    /// <summary>
    /// Project list class
    /// </summary>
    public class Card : ModelBase<Card>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Card" /> class.
        /// </summary>
        public Card()
            : base()
        {
            Members = new List<User>();
            Labels = new List<Label>();
            CheckLists = new List<CheckList>();
            Documents = new List<Document>();
            Notes = new List<Note>();
        }

        /// <summary>
        /// Gets or sets the check lists.
        /// </summary>
        /// <value>
        /// The check lists.
        /// </value>
        public virtual List<CheckList> CheckLists { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [System.ComponentModel.DataAnnotations.MaxLength(5012)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the documents.
        /// </summary>
        /// <value>
        /// The documents.
        /// </value>
        public virtual List<Document> Documents { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [Utilities.Validation.Between("1/1/2000", "1/1/2100")]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        public virtual List<Label> Labels { get; set; }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        /// <value>
        /// The members.
        /// </value>
        public virtual List<User> Members { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [System.ComponentModel.DataAnnotations.MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public virtual List<Note> Notes { get; set; }
    }
}