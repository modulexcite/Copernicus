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

using Copernicus.Models.BaseClasses;

namespace Copernicus.Models.General.Mappings
{
    /// <summary>
    /// LookUpType API mapping
    /// </summary>
    public class LookUpTypeAPIMapping : APIMappingBase<LookUpType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookUpTypeAPIMapping"/> class.
        /// </summary>
        public LookUpTypeAPIMapping()
            : base(1)
        {
            Reference(x => x.DisplayName);
            Reference(x => x.Description);
            MapList(x => x.LookUps);
        }
    }

    /// <summary>
    /// Lookup type mapping
    /// </summary>
    public class LookUpTypeMapping : ModelMappingBase<LookUpType>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LookUpTypeMapping()
            : base()
        {
            Reference(x => x.Description).SetMaxLength(500).SetDefaultValue(() => "");
            Reference(x => x.DisplayName).SetMaxLength(50).SetDefaultValue(() => "");
            ManyToOne(x => x.LookUps).SetCascade();
        }
    }
}