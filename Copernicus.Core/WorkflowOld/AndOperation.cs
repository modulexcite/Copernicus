﻿/*
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
using Utilities.DataTypes;

namespace Copernicus.Core.Workflow
{
    /// <summary>
    /// And operation
    /// </summary>
    public class AndOperation : OperationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AndOperation" /> class.
        /// </summary>
        public AndOperation()
            : base()
        {
        }

        /// <summary>
        /// Executes the operation on the specified value.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns>The result of the operation</returns>
        public override dynamic Execute(dynamic Value)
        {
            return Value;
        }

        /// <summary>
        /// Starts the operation
        /// </summary>
        /// <param name="Value">The value passed in</param>
        /// <returns>A task that will return true if the operation succeeded, false otherwise.</returns>
        public override async Task<bool> Start(dynamic Value)
        {
            return await Task.Run<bool>(() =>
            {
                if (SuccessOperations.Count == 0)
                    return true;
                if (SuccessOperations.ForEachParallel(x => x.Start(new Dynamo(Value)).Result).All(x => x))
                    return true;
                FailureOperations.ForEachParallel(x => x.Start(new Dynamo(Value)));
                return false;
            });
        }
    }
}