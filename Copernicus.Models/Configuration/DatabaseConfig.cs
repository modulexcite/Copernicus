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

using Utilities.ORM.Interfaces;

namespace Copernicus.Models.Configuration
{
    /// <summary>
    /// Database config
    /// </summary>
    public class DatabaseConfig : IDatabase
    {
        /// <summary>
        /// Audit the database
        /// </summary>
        public bool Audit { get { return true; } }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get { return "Default"; } }

        /// <summary>
        /// Order
        /// </summary>
        public int Order { get { return 1; } }

        /// <summary>
        /// Readable
        /// </summary>
        public bool Readable { get { return true; } }

        /// <summary>
        /// Update
        /// </summary>
        public bool Update { get { return true; } }

        /// <summary>
        /// Writable
        /// </summary>
        public bool Writable { get { return true; } }
    }
}