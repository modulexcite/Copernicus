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
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Copernicus.Models.BaseClasses;
using Utilities.DataTypes;
using Utilities.DataTypes.CodeGen;
using Utilities.DataTypes.CodeGen.BaseClasses;
using Utilities.IO;

namespace Copernicus.Models.Data
{
    /// <summary>
    /// Source class
    /// </summary>
    public class Source : ModelBase<Source>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Source" /> class.
        /// </summary>
        public Source()
            : base()
        {
            Models = new List<Model>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Source" /> is audited.
        /// </summary>
        /// <value><c>true</c> if audit; otherwise, <c>false</c>.</value>
        public bool Audit { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the models (table mappings)
        /// </summary>
        /// <value>The models.</value>
        public virtual List<Model> Models { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Source" /> is readable.
        /// </summary>
        /// <value><c>true</c> if readable; otherwise, <c>false</c>.</value>
        public bool Readable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Source" />'s schema is updatable.
        /// </summary>
        /// <value><c>true</c> if update; otherwise, <c>false</c>.</value>
        public bool Update { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Source" /> is writable.
        /// </summary>
        /// <value><c>true</c> if writable; otherwise, <c>false</c>.</value>
        public bool Writable { get; set; }

        /// <summary>
        /// Encrypts this instance.
        /// </summary>
        /// <returns>This</returns>
        public Source Decrypt()
        {
            string Password = ConfigurationManager.AppSettings["Copernicus:EncryptionPassword"];
            byte[] Salt = ConfigurationManager.AppSettings["Copernicus:EncryptionSalt"].ToByteArray();
            string InitialVector = ConfigurationManager.AppSettings["Copernicus:EncryptionInitialVector"];
            using (DeriveBytes TempPassword = new System.Security.Cryptography.Rfc2898DeriveBytes(Password, Salt))
            {
                ConnectionString = ConnectionString.Decrypt(TempPassword, InitialVector: InitialVector);
            }
            return this;
        }

        /// <summary>
        /// Encrypts this instance.
        /// </summary>
        /// <returns>This</returns>
        public Source Encrypt()
        {
            string Password = ConfigurationManager.AppSettings["Copernicus:EncryptionPassword"];
            byte[] Salt = ConfigurationManager.AppSettings["Copernicus:EncryptionSalt"].ToByteArray();
            string InitialVector = ConfigurationManager.AppSettings["Copernicus:EncryptionInitialVector"];
            using (DeriveBytes TempPassword = new System.Security.Cryptography.Rfc2898DeriveBytes(Password, Salt))
            {
                ConnectionString = ConnectionString.Encrypt(TempPassword, InitialVector: InitialVector);
            }
            return this;
        }

        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns>This</returns>
        public Source Generate(Compiler Compiler)
        {
            Models.ForEach(x => x.Generate(Compiler));
            StringBuilder Builder = new StringBuilder();
            Builder.AppendLineFormat("namespace Copernicus.Generated.Configuration.{0}", Guid.NewGuid().ToString("N"))
                   .AppendLine("{")
                   .AppendLineFormat("public class {0} : IDatabase", Name)
                   .AppendLine("{")
                   .AppendLineFormat("public bool Audit {{ get {{ return {0}; }} }}", Audit)
                   .AppendLineFormat("public string Name {{ get {{ return \"{0}\"; }} }}", Name)
                   .AppendLineFormat("public int Order {{ get {{ return {0}; }} }}", Order)
                   .AppendLineFormat("public bool Readable {{ get {{ return {0}; }} }}", Readable)
                   .AppendLineFormat("public bool Update {{ get {{ return {0}; }} }}", Update)
                   .AppendLineFormat("public bool Writable {{ get {{ return {0}; }} }}", Writable)
                   .AppendLine("}")
                   .AppendLine("}");
            Compiler.CreateClass(Name,
                                Builder.ToString(),
                                new string[] { "Utilities.ORM.Interfaces" },
                                typeof(Utilities.ORM.Interfaces.IDatabase).Assembly);
            return this;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}