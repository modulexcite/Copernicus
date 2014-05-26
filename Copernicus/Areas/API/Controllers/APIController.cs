using Copernicus.Core.API.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Copernicus.Areas.API.Controllers
{
    /// <summary>
    /// API Controller
    /// </summary>
    public class APIController : APIControllerBaseClass
    {
        /// <summary>
        /// Version number for this API endpoint
        /// </summary>
        protected override int Version
        {
            get { return 1; }
        }
    }
}