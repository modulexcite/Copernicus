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

using Copernicus.Core.Workflow.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataTypes;
using Utilities.DataTypes.Patterns.BaseClasses;
using Utilities.IO;

namespace Copernicus.Core.Workflow
{
    /// <summary>
    /// Workflow manager
    /// </summary>
    public class Manager : SafeDisposableBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Manager" /> class.
        /// </summary>
        public Manager()
        {
            System.IO.FileInfo WorkflowFile = new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Workflows.obj");
            byte[] Data = null;
            if (WorkflowFile.Exists)
            {
                using (FileStream WorkflowStream = WorkflowFile.OpenRead())
                {
                    byte[] Buffer = new byte[1024];
                    using (MemoryStream Temp = new MemoryStream())
                    {
                        while (true)
                        {
                            int Count = WorkflowStream.Read(Buffer, 0, Buffer.Length);
                            if (Count <= 0)
                                break;
                            Temp.Write(Buffer, 0, Count);
                        }
                        Data = Temp.ToArray();
                    }
                }
            }
            this.Workflows = WorkflowFile.Exists ? Deserialize<Dictionary<string, IWorkflow>>(Data) : new Dictionary<string, IWorkflow>();
        }

        /// <summary> 
        /// Gets or sets the workflows. 
        /// </summary> 
        /// <value>The workflows.</value>
        private Dictionary<string, IWorkflow> Workflows { get; set; }

        /// <summary> 
        /// Creates the workflow. 
        /// </summary> 
        /// <param name="Name">The name.</param>
        /// <returns>The workflow that is created</returns> 
        public IWorkflow<T> CreateWorkflow<T>(string Name)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(Name), "Name");
            if (Workflows.Keys.Contains(Name))
                return (IWorkflow<T>)Workflows[Name];
            IWorkflow<T> ReturnValue = new Workflow<T>(Name);
            Workflows.Add(new KeyValuePair<string, IWorkflow>(Name, ReturnValue));
            return ReturnValue;
        }

        ///<summary>
        /// Removes the workflow. 
        /// </summary>
        /// <param name="Workflow">The workflow.</param>
        /// <returns>True if it is removed, false otherwise</returns>
        public bool RemoveWorkflow(IWorkflow Workflow)
        {
            return Workflows.Remove(Workflow.Name);
        }

        /// <summary>
        /// Function to override in order to dispose objects
        /// </summary>
        /// <param name="Managed">If true, managed and unmanaged objects should be disposed. Otherwise unmanaged objects only.</param>
        protected override void Dispose(bool Managed)
        {
            new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/").Create();
            System.IO.FileInfo WorkflowFile = new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Workflows.obj");
            byte[] Data = Serialize<Dictionary<string, IWorkflow>>(Workflows);
            using (FileStream WorkflowStream = WorkflowFile.OpenWrite())
            {
                WorkflowStream.Write(Data, 0, Data.Length);
            }
        }

        /// <summary> 
        /// Deserializes the data 
        /// </summary> 
        /// <typeparam name="T">Object type</typeparam> 
        /// <param name="Data">Data to deserialize</param> 
        /// <returns>The deserialized data</returns> 
        private T Deserialize<T>(byte[] Data)
        {
            Type ObjectType = typeof(T);
            if (Data == null || ObjectType == null || Data.Length == 0)
                return default(T);
            using (MemoryStream Stream = new MemoryStream(Data))
            {
                BinaryFormatter Formatter = new BinaryFormatter(); return (T)Formatter.Deserialize(Stream);
            }
        }

        /// <summary>
        /// Serializes the object
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="Data">Data to serialize</param>
        /// <returns>The serialized data</returns>
        private byte[] Serialize<T>(T Data)
        {
            Type ObjectType = Data.GetType();
            if (Data == null || ObjectType == null)
                return null;
            using (MemoryStream Stream = new MemoryStream())
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                Formatter.Serialize(Stream, Data);
                return Stream.ToArray();
            }
        }
    }
}