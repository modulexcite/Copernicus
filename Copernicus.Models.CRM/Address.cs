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
using Copernicus.Models.General;

namespace Copernicus.Models.CRM
{
    /// <summary>
    /// Address information
    /// </summary>
    public class Address : ModelBase<Address>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Address" /> class.
        /// </summary>
        public Address()
            : base()
        {
        }

        /// <summary>
        /// Address type
        /// </summary>
        [Required]
        public LookUp AddressType { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [MaxLength(64)]
        public string City { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [Required]
        public Country Country { get; set; }

        /// <summary>
        /// Postal code
        /// </summary>
        [MaxLength(32)]
        public string PostalCode { get; set; }

        /// <summary>
        /// State/Province
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// Street Address 1
        /// </summary>
        [MaxLength(64)]
        public string StreetAddress1 { get; set; }

        /// <summary>
        /// Street address 2
        /// </summary>
        [MaxLength(64)]
        public string StreetAddress2 { get; set; }

        /// <summary>
        /// Street address 3
        /// </summary>
        [MaxLength(64)]
        public string StreetAddress3 { get; set; }
    }
}