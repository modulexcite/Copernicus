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
using Utilities.DataTypes;
using Utilities.ORM.Parameters;

namespace Copernicus.Models.General
{
    /// <summary>
    /// General look up
    /// </summary>
    public class LookUp : ModelBase<LookUp>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LookUp()
            : base()
        {
        }

        /// <summary>
        /// Display name
        /// </summary>
        [Utilities.Validation.MinLength(1)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Type associated with this LookUp
        /// </summary>
        [Required]
        public virtual LookUpType Type { get; set; }

        /// <summary>
        /// Loads a specific LookUp based on the display name and look up type specified
        /// </summary>
        /// <param name="DisplayName">Display name specified</param>
        /// <param name="Type">Lookup type specified</param>
        /// <returns>LookUp associated with the display name</returns>
        public static LookUp Load(string DisplayName, string Type)
        {
            return All(new StringEqualParameter(DisplayName, "DisplayName_", 256))
                .FirstOrDefault(x => x.Chain(y => y.Type, new LookUpType()).DisplayName == Type);
        }

        /// <summary>
        /// String representation of the object
        /// </summary>
        /// <returns>Display name</returns>
        public override string ToString()
        {
            return DisplayName;
        }
    }
}