using Copernicus.Models.Configuration;
using System;
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