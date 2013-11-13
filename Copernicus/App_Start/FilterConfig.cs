using System.Web;
using System.Web.Mvc;

namespace Copernicus
{
    /// <summary>
    /// Filter config
    /// </summary>
    public static class FilterConfig
    {
        /// <summary>
        /// Registers the global filter
        /// </summary>
        /// <param name="filters">Filter collection</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            if (filters == null)
                return;
            filters.Add(new HandleErrorAttribute());
        }
    }
}