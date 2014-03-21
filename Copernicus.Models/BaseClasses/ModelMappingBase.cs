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
using Copernicus.Models.Configuration;
using Utilities.ORM.BaseClasses;

namespace Copernicus.Models.BaseClasses
{
    /// <summary>
    /// Model mapping base class
    /// </summary>
    /// <typeparam name="ClassType">Model class type</typeparam>
    public abstract class ModelMappingBase<ClassType> : ModelMappingBase<ClassType, long>
        where ClassType : ModelBase<ClassType>, new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected ModelMappingBase()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="TableName">Table name</param>
        /// <param name="Suffix">Suffix</param>
        /// <param name="Prefix">Prefix</param>
        /// <param name="Order">Order to load in</param>
        protected ModelMappingBase(string TableName = "", string Suffix = "_", string Prefix = "", int Order = 10)
            : base(TableName, Suffix, Prefix, Order)
        {
        }
    }

    /// <summary>
    /// Model mapping base class
    /// </summary>
    /// <typeparam name="ClassType">Model class type</typeparam>
    /// <typeparam name="IDType">ID type</typeparam>
    public abstract class ModelMappingBase<ClassType, IDType> : MappingBaseClass<ClassType, DatabaseConfig>
        where ClassType : ModelBase<ClassType, IDType>, new()
        where IDType : IComparable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected ModelMappingBase()
            : this("", "_", "", 10)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="TableName">Table name</param>
        /// <param name="Suffix">Suffix</param>
        /// <param name="Prefix">Prefix</param>
        /// <param name="Order">Order to load in</param>
        protected ModelMappingBase(string TableName = "", string Suffix = "_", string Prefix = "", int Order = 10)
            : base(TableName, Suffix, Prefix, Order)
        {
            SetupBaseProperties();
        }

        /// <summary>
        /// Sets up the default properties
        /// </summary>
        protected virtual void SetupBaseProperties()
        {
            ID(x => x.ID).SetAutoIncrement();
            Reference(x => x.Active).SetDefaultValue(() => true);
            Reference(x => x.DateCreated).SetDefaultValue(() => new DateTime(1900, 1, 1));
            Reference(x => x.DateModified).SetDefaultValue(() => new DateTime(1900, 1, 1));
            Map(x => x.Creator);
            Map(x => x.Modifier);
        }
    }
}