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
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Copernicus.Core.API.BaseClasses
{
    /// <summary>
    /// API controller base class
    /// </summary>
    public abstract class APIControllerBaseClass : Ironman.Core.API.BaseClasses.APIControllerBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="APIControllerBaseClass" /> class.
        /// </summary>
        protected APIControllerBaseClass()
            : base()
        {
        }

        /// <summary>
        /// Gets all items of the specified type
        /// GET: {APIRoot}/{ModelName}/
        /// </summary>
        /// <param name="ModelName">Model name</param>
        /// <returns>The resulting list of items</returns>
        public override ActionResult All(string ModelName)
        {
            return base.All(ModelName);
        }

        /// <summary>
        /// Gets the item of the specified type, with the specified ID
        /// GET: {APIRoot}/{ModelName}/{ID}
        /// </summary>
        /// <param name="ModelName">Model name</param>
        /// <param name="ID">ID of the object to get</param>
        /// <returns>The resulting item</returns>
        public override ActionResult Any(string ModelName, string ID)
        {
            return base.Any(ModelName, ID);
        }

        /// <summary>
        /// Deletes the specified object
        /// </summary>
        /// <param name="ModelName">Model name</param>
        /// <param name="ID">ID of the object to delete</param>
        /// <returns>The result</returns>
        public override ActionResult Delete(string ModelName, string ID)
        {
            return base.Delete(ModelName, ID);
        }

        /// <summary>
        /// Deletes the property item specified
        /// </summary>
        /// <param name="ModelName">Model name</param>
        /// <param name="ID">ID of the object to get</param>
        /// <param name="PropertyName">Property name</param>
        /// <param name="PropertyID">Property ID</param>
        /// <returns>The result</returns>
        public override ActionResult DeleteProperty(string ModelName, string ID, string PropertyName, string PropertyID)
        {
            return base.DeleteProperty(ModelName, ID, PropertyName, PropertyID);
        }

        /// <summary>
        /// Saves the specified object
        /// </summary>
        /// <param name="ModelName">Model name</param>
        /// <param name="Model">Model to save</param>
        /// <returns>The result</returns>
        public override ActionResult Save(string ModelName, IEnumerable<System.Dynamic.ExpandoObject> Model)
        {
            return base.Save(ModelName, Model);
        }

        /// <summary>
        /// Saves the specified object as a property
        /// </summary>
        /// <param name="ModelName">Model name</param>
        /// <param name="ID">Model ID</param>
        /// <param name="PropertyName">Property name</param>
        /// <param name="Model">Model to save</param>
        /// <returns>The result</returns>
        public override ActionResult SaveProperty(string ModelName, string ID, string PropertyName, IEnumerable<System.Dynamic.ExpandoObject> Model)
        {
            return base.SaveProperty(ModelName, ID, PropertyName, Model);
        }
    }
}