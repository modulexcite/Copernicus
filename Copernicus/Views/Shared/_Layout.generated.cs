﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Copernicus.Views.Shared
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using Copernicus;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/_Layout.cshtml")]
    public partial class Layout : System.Web.Mvc.WebViewPage<dynamic>
    {
        public Layout()
        {
        }
        public override void Execute()
        {
WriteLiteral("<!DOCTYPE html>\r\n<html");

WriteLiteral(" lang=\"en\"");

WriteLiteral(">\r\n    <head>\r\n        <meta");

WriteLiteral(" charset=\"utf-8\"");

WriteLiteral(" />\r\n        <title>");

            
            #line 5 "..\..\Views\Shared\_Layout.cshtml"
          Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral(" - My ASP.NET MVC Application</title>\r\n        <link");

WriteAttribute("href", Tuple.Create(" href=\"", 162), Tuple.Create("\"", 182)
, Tuple.Create(Tuple.Create("", 169), Tuple.Create<System.Object, System.Int32>(Href("~/favicon.ico")
, 169), false)
);

WriteLiteral(" rel=\"shortcut icon\"");

WriteLiteral(" type=\"image/x-icon\"");

WriteLiteral(" />\r\n        <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width\"");

WriteLiteral(" />\r\n");

WriteLiteral("        ");

            
            #line 8 "..\..\Views\Shared\_Layout.cshtml"
   Write(Styles.Render("~/Content/Theme/bundle/css"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </head>\r\n    <body>\r\n        <header");

WriteLiteral(" id=\"header\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" id=\"top-header\"");

WriteLiteral(">\r\n                <nav>\r\n                    <ul");

WriteLiteral(" id=\"menu\"");

WriteLiteral(">\r\n                        <li");

WriteLiteral(" class=\"left\"");

WriteLiteral("><a");

WriteLiteral(" href=\"/\"");

WriteLiteral(" id=\"Home\"");

WriteLiteral(" title=\"Invoices\"");

WriteLiteral("><img");

WriteLiteral(" src=\"/Content/Images/Invoice2.png\"");

WriteLiteral(" alt=\"Invoices\"");

WriteLiteral(" title=\"Invoices\"");

WriteLiteral(" /></a></li>\r\n");

WriteLiteral("                        ");

            
            #line 16 "..\..\Views\Shared\_Layout.cshtml"
                   Write(Html.Partial("_LoginPartial"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                        <li");

WriteLiteral(" class=\"left\"");

WriteLiteral("><a");

WriteLiteral(" href=\"/CRM\"");

WriteLiteral(" title=\"User Info\"");

WriteLiteral("><img");

WriteLiteral(" src=\"/Content/Images/User2.png\"");

WriteLiteral(" alt=\"User Info\"");

WriteLiteral(" title=\"User Info\"");

WriteLiteral(" /></a></li>\r\n                        <li");

WriteLiteral(" class=\"left\"");

WriteLiteral("><a");

WriteLiteral(" href=\"/CRM\"");

WriteLiteral(" title=\"Business Info\"");

WriteLiteral("><img");

WriteLiteral(" src=\"/Content/Images/Business2.png\"");

WriteLiteral(" alt=\"Business Info\"");

WriteLiteral(" title=\"Business Info\"");

WriteLiteral(" /></a></li>\r\n                        <li");

WriteLiteral(" class=\"left\"");

WriteLiteral("><a");

WriteLiteral(" href=\"/Project\"");

WriteLiteral(" title=\"Projects\"");

WriteLiteral("><img");

WriteLiteral(" src=\"/Content/Images/Project2.png\"");

WriteLiteral(" alt=\"Projects\"");

WriteLiteral(" title=\"Projects\"");

WriteLiteral(" /></a></li>\r\n                        <li");

WriteLiteral(" class=\"left\"");

WriteLiteral("><a");

WriteLiteral(" href=\"/CRM\"");

WriteLiteral(" title=\"Activities\"");

WriteLiteral("><img");

WriteLiteral(" src=\"/Content/Images/Activity2.png\"");

WriteLiteral(" alt=\"Activities\"");

WriteLiteral(" title=\"Activities\"");

WriteLiteral(" /></a></li>\r\n                        <li");

WriteLiteral(" class=\"left\"");

WriteLiteral("><a");

WriteLiteral(" href=\"/CRM\"");

WriteLiteral(" title=\"Messages\"");

WriteLiteral("><img");

WriteLiteral(" src=\"/Content/Images/Message2.png\"");

WriteLiteral(" alt=\"Messages\"");

WriteLiteral(" title=\"Messages\"");

WriteLiteral(" /></a></li>\r\n                    </ul>\r\n                </nav>\r\n            </di" +
"v>\r\n            <div");

WriteLiteral(" id=\"search\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"inputWrapper\"");

WriteLiteral(">\r\n                    <a");

WriteLiteral(" href=\"/\"");

WriteLiteral(" class=\"image right searchToggle toggle\"");

WriteLiteral(" data-bind=\"event: { click: setRecent }\"");

WriteLiteral(" title=\"Recent Searches\"");

WriteLiteral("><img");

WriteLiteral(" src=\"/Content/Images/asc.gif\"");

WriteLiteral(" title=\"Recent Searches\"");

WriteLiteral(" alt=\"Recent Searches\"");

WriteLiteral(" /></a>\r\n                    <label");

WriteLiteral(" for=\"q\"");

WriteLiteral("><img");

WriteLiteral(" src=\"/Content/Images/Search.png\"");

WriteLiteral(" width=\"16px\"");

WriteLiteral(" height=\"16px\"");

WriteLiteral(" title=\"Search Icon\"");

WriteLiteral(" alt=\"Search Icon\"");

WriteLiteral("></label>\r\n                    <input");

WriteLiteral(" type=\"text\"");

WriteLiteral(" placeholder=\"Search Concierge\"");

WriteLiteral(" name=\"q\"");

WriteLiteral(" id=\"q\"");

WriteLiteral(" data-bind=\"event: { blur: addRecent, keyup: doSearch }\"");

WriteLiteral(" />\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"search\"");

WriteLiteral(" data-bind=\"visible: showRecent\"");

WriteLiteral(">\r\n                    <h4>Recent Searches</h4>\r\n                    <div");

WriteLiteral(" class=\"recentSearches\"");

WriteLiteral(" data-bind=\"foreach: recent\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" data-bind=\"text: text, attr: { href: href, title: text }\"");

WriteLiteral("></a>\r\n\t\t            </div>\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"searchResults\"");

WriteLiteral(" data-bind=\"visible: showResults\"");

WriteLiteral(">\r\n                    <h4>Results</h4>\r\n                    <div");

WriteLiteral(" class=\"results\"");

WriteLiteral(" data-bind=\"foreach: results\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" data-bind=\"text: Text, attr: { href: Value, title: text }\"");

WriteLiteral("></a>\r\n\t\t            </div>\r\n                </div>\r\n            </div>\r\n        " +
"</header>\r\n        <div");

WriteLiteral(" id=\"body\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 46 "..\..\Views\Shared\_Layout.cshtml"
       Write(RenderSection("featured", required: false));

            
            #line default
            #line hidden
WriteLiteral("\r\n            <section");

WriteLiteral(" class=\"content-wrapper main-content clear-fix\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 48 "..\..\Views\Shared\_Layout.cshtml"
           Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral("\r\n            </section>\r\n        </div>\r\n        <footer>\r\n            <div");

WriteLiteral(" class=\"three columns\"");

WriteLiteral(" id=\"notifications\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"content\"");

WriteLiteral(">\r\n                    <hgroup");

WriteLiteral(" class=\"title\"");

WriteLiteral(" data-bind=\"event: { click: setToggle }\"");

WriteLiteral(">\r\n                        <h3><img");

WriteLiteral(" src=\"/Content/Images/Notification.png\"");

WriteLiteral(" class=\"right\"");

WriteLiteral(" title=\"Notifications\"");

WriteLiteral(" alt=\"Notifications\"");

WriteLiteral(" />Notifications</h3>\r\n                    </hgroup>\r\n                    <ol");

WriteLiteral(" class=\"data\"");

WriteLiteral(" data-bind=\"visible: toggle, foreach: notifications\"");

WriteLiteral(">\r\n                        <li");

WriteLiteral(" data-bind=\"html: Text\"");

WriteLiteral(">\r\n                        </li>\r\n                    </ol>\r\n                </di" +
"v>\r\n            </div>\r\n            <div");

WriteLiteral(" class=\"three columns\"");

WriteLiteral(" id=\"onlineUsers\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"content\"");

WriteLiteral(">\r\n                    <hgroup");

WriteLiteral(" class=\"title\"");

WriteLiteral(" data-bind=\"event: { click: setToggle }\"");

WriteLiteral(">\r\n                        <h3><img");

WriteLiteral(" src=\"/Content/Images/User.png\"");

WriteLiteral(" class=\"right\"");

WriteLiteral(" title=\"Online Users\"");

WriteLiteral(" alt=\"Online Users\"");

WriteLiteral(" />Online Users</h3>\r\n                    </hgroup>\r\n                    <ol");

WriteLiteral(" class=\"data\"");

WriteLiteral(" data-bind=\"visible: toggle, foreach: users\"");

WriteLiteral(">\r\n                        <li");

WriteLiteral(" data-bind=\"html: Name\"");

WriteLiteral(">\r\n                        </li>\r\n                    </ol>\r\n                </di" +
"v>\r\n            </div>\r\n            &nbsp;\r\n        </footer>\r\n\r\n        <script" +
"");

WriteAttribute("src", Tuple.Create(" src=\"", 4413), Tuple.Create("\"", 4455)
, Tuple.Create(Tuple.Create("", 4419), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/JQuery/jquery-2.1.1.min.js")
, 4419), false)
);

WriteLiteral("></script>\r\n        <script");

WriteAttribute("src", Tuple.Create(" src=\"", 4483), Tuple.Create("\"", 4527)
, Tuple.Create(Tuple.Create("", 4489), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/KnockoutJS/knockout-3.2.0.js")
, 4489), false)
);

WriteLiteral("></script>\r\n        <script");

WriteAttribute("src", Tuple.Create(" src=\"", 4555), Tuple.Create("\"", 4583)
, Tuple.Create(Tuple.Create("", 4561), Tuple.Create<System.Object, System.Int32>(Href("~/Scripts/Main/Main.js")
, 4561), false)
);

WriteLiteral("></script>\r\n");

WriteLiteral("        ");

            
            #line 80 "..\..\Views\Shared\_Layout.cshtml"
   Write(RenderSection("scripts", required: false));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </body>\r\n</html>\r\n");

        }
    }
}
#pragma warning restore 1591
