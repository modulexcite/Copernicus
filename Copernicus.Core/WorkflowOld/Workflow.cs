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
using Utilities.DataTypes;

namespace Copernicus.Core.Workflow
{
    /// <summary>
    /// Workflow holder
    /// </summary>
    [Serializable]
    public class Workflow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Workflow" /> class.
        /// </summary>
        /// <param name="Name">The name.</param>
        public Workflow(string Name)
            : this()
        {
            this.Name = Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Workflow" /> class.
        /// </summary>
        public Workflow()
        {
            this.Operations = new List<IOperation>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the operations.
        /// </summary>
        /// <value>The operations.</value>
        public List<IOperation> Operations { get; set; }

        /// <summary>
        /// Adds an operation.
        /// </summary>
        /// <typeparam name="T">Operation type</typeparam>
        /// <returns>The operation that was added</returns>
        public IOperation AddOperation<T>()
            where T : IOperation, new()
        {
            return Operations.AddAndReturn(new T());
        }

        /// <summary>
        /// Adds the operation.
        /// </summary>
        /// <param name="Operation">The operation.</param>
        /// <returns>The operation that was added</returns>
        public IOperation AddOperation(IOperation Operation)
        {
            return Operations.AddAndReturn(Operation);
        }

        /// <summary>
        /// Adds the operation.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="Operation">The operation.</param>
        /// <returns>The operation that was added</returns>
        public IOperation AddOperation(string Name, Func<dynamic, dynamic> Operation)
        {
            return Operations.AddAndReturn(new GenericOperation() { Name = Name, InternalOperation = Operation });
        }

        /// <summary>
        /// Starts the specified value.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        public dynamic Start(dynamic Value)
        {
            return Operations.ForEachParallel(x => x.Start(Value).Result).All(x => x);
        }
    }
}