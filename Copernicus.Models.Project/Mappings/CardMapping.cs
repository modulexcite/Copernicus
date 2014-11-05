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

namespace Copernicus.Models.Project.Mappings
{
    /// <summary>
    /// Card API mapping
    /// </summary>
    public class CardAPIMapping : APIMappingBase<Card>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardAPIMapping" /> class.
        /// </summary>
        public CardAPIMapping()
            : base(1)
        {
            Map(x => x.Creator);
            Map(x => x.Modifier);
            Reference(x => x.Description);
            MapList(x => x.CheckLists);
            MapList(x => x.Documents);
            Reference(x => x.DueDate);
            MapList(x => x.Labels);
            MapList(x => x.Members);
            Reference(x => x.Name);
            MapList(x => x.Notes);
        }
    }

    /// <summary>
    /// Card mapping
    /// </summary>
    public class CardMapping : ModelMappingBase<Card>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardMapping" /> class.
        /// </summary>
        public CardMapping()
            : base()
        {
            Reference(x => x.Description).SetMaxLength(5012);
            ManyToOne(x => x.CheckLists).SetCascade();
            ManyToOne(x => x.Documents).SetCascade();
            Reference(x => x.DueDate);
            ManyToMany(x => x.Labels);
            ManyToMany(x => x.Members);
            Reference(x => x.Name).SetMaxLength(128).SetNotNull();
            ManyToOne(x => x.Notes).SetCascade();
        }
    }
}