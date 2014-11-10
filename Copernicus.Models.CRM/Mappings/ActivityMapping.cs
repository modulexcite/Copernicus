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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copernicus.Models.CRM
{
    /// <summary>
    /// Activity API mapping
    /// </summary>
    public class ActivityAPIMapping : APIMappingBase<Activity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityAPIMapping"/> class.
        /// </summary>
        public ActivityAPIMapping()
            : base(1)
        {
            Map(x => x.ActivityType);
            Reference(x => x.Description);
            Reference(x => x.EndDate);
            Map(x => x.Location);
            MapList(x => x.Participants);
            Reference(x => x.StartDate);
            Reference(x => x.Title);
        }
    }

    /// <summary>
    /// Activity mapping
    /// </summary>
    public class ActivityMapping : ModelMappingBase<Activity>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ActivityMapping()
            : base()
        {
            Map(x => x.ActivityType);
            Reference(x => x.Description).SetMaxLength(5012);
            Reference(x => x.EndDate).SetDefaultValue(() => new DateTime(2100, 1, 1));
            Map(x => x.Location);
            ManyToMany(x => x.Participants);
            Reference(x => x.StartDate).SetDefaultValue(() => new DateTime(2100, 1, 1));
            Reference(x => x.Title).SetMaxLength(100).SetNotNull();
        }
    }
}