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
using Utilities.ORM.Parameters;
using Utilities.Validation;

namespace Copernicus.Models.General
{
    /// <summary>
    /// Look Up Type
    /// </summary>
    public class LookUpType : ModelBase<LookUpType>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LookUpType()
            : base()
        {
            LookUps = new List<LookUp>();
        }

        /// <summary>
        /// Description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Display name
        /// </summary>
        [MinLength(1)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// LookUps associated with this type
        /// </summary>
        public virtual List<LookUp> LookUps { get; set; }

        /// <summary>
        /// Loads a specific LookUpType based on the display name specified
        /// </summary>
        /// <param name="DisplayName">Display name specified</param>
        /// <returns>LookUpType associated with the display name</returns>
        public static LookUpType Load(string DisplayName)
        {
            return Any(new StringEqualParameter(DisplayName, "DisplayName_", 256));
        }

        /// <summary>
        /// String representation of the object
        /// </summary>
        /// <returns>Display Name</returns>
        public override string ToString()
        {
            return DisplayName;
        }
    }
}