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
using Copernicus.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Utilities.DataTypes;
using Utilities.IO;
using Utilities.ORM;
using Utilities.ORM.Parameters;
using Utilities.Web;

namespace Copernicus.Models.BaseClasses
{
    /// <summary>
    /// Model base class
    /// </summary>
    /// <typeparam name="ClassType">Class type</typeparam>
    public abstract class ModelBase<ClassType> : ModelBase<ClassType, long>
        where ClassType : ModelBase<ClassType>, new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected ModelBase()
            : base()
        {
        }

        /// <summary>
        /// Loads the item based on the ID
        /// </summary>
        /// <param name="ID">ID of the item to load</param>
        /// <returns>The specified item</returns>
        public static ClassType Load(long ID)
        {
            return Load(ID, "ID_");
        }
    }

    /// <summary>
    /// Model base class
    /// </summary>
    /// <typeparam name="ClassType">Class type</typeparam>
    /// <typeparam name="IDType">ID type</typeparam>
    public abstract class ModelBase<ClassType, IDType> : ObjectBaseClass<ClassType, IDType>, IModel
        where ClassType : ModelBase<ClassType, IDType>, new()
        where IDType : IComparable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected ModelBase()
            : base()
        {
        }

        /// <summary>
        /// Active
        /// </summary>
        [ScaffoldColumn(true)]
        public override bool Active { get; set; }

        /// <summary>
        /// Creator
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual User Creator { get; set; }

        /// <summary>
        /// Date created
        /// </summary>
        [ScaffoldColumn(false)]
        public override DateTime DateCreated { get; set; }

        /// <summary>
        /// Date modified
        /// </summary>
        [ScaffoldColumn(false)]
        public override DateTime DateModified { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [ScaffoldColumn(true)]
        [HiddenInput(DisplayValue = false)]
        public override IDType ID { get; set; }

        /// <summary>
        /// Modifier
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual User Modifier { get; set; }

        /// <summary>
        /// Loads the item based on the ID
        /// </summary>
        /// <param name="ID">ID of the item to load</param>
        /// <param name="IDName">specify column name to match id</param>
        /// <returns>The specified item</returns>
        public static ClassType Load(IDType ID, string IDName)
        {
            return Any(new EqualParameter<IDType>(ID, IDName));
        }

        /// <summary>
        /// Sets up the object for saving purposes
        /// </summary>
        public override void SetupObject()
        {
            base.SetupObject();
            Modifier = User.LoadCurrentUser();
            if (Creator == null && Modifier != null)
                Creator = Modifier;
            if (Creator != null && Modifier == null)
                Modifier = Creator;
        }
    }
}