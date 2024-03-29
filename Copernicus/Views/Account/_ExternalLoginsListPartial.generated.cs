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

namespace Copernicus.Views.Account
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
    
    #line 1 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
    using Microsoft.Owin.Security;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Account/_ExternalLoginsListPartial.cshtml")]
    public partial class ExternalLoginsListPartial : System.Web.Mvc.WebViewPage<dynamic>
    {
        public ExternalLoginsListPartial()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<h4>Use another service to log in.</h4>\r\n<hr />\r\n");

            
            #line 5 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
  
    var loginProviders = Context.GetOwinContext().Authentication.GetExternalAuthenticationTypes();
    if (loginProviders.Count() == 0)
    {

            
            #line default
            #line hidden
WriteLiteral("        <div>\r\n            <p>There are no external authentication services confi" +
"gured. See <a");

WriteLiteral(" href=\"http://go.microsoft.com/fwlink/?LinkId=313242\"");

WriteLiteral(">this article</a>\r\n            for details on setting up this ASP.NET application" +
" to support logging in via external services.</p>\r\n        </div>\r\n");

            
            #line 13 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
    }
    else
    {
        string action = Model.Action;
        string returnUrl = Model.ReturnUrl;
        using (Html.BeginForm(action, "Account", new { ReturnUrl = returnUrl }))
        {
            
            
            #line default
            #line hidden
            
            #line 20 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
       Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 20 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
                                    

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" id=\"socialLoginList\"");

WriteLiteral(">\r\n                <p>\r\n");

            
            #line 23 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
                
            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
                 foreach (AuthenticationDescription p in loginProviders)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <button");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteAttribute("id", Tuple.Create(" id=\"", 985), Tuple.Create("\"", 1011)
            
            #line 25 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
, Tuple.Create(Tuple.Create("", 990), Tuple.Create<System.Object, System.Int32>(p.AuthenticationType
            
            #line default
            #line hidden
, 990), false)
);

WriteLiteral(" name=\"provider\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1028), Tuple.Create("\"", 1057)
            
            #line 25 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
                                    , Tuple.Create(Tuple.Create("", 1036), Tuple.Create<System.Object, System.Int32>(p.AuthenticationType
            
            #line default
            #line hidden
, 1036), false)
);

WriteAttribute("title", Tuple.Create(" title=\"", 1058), Tuple.Create("\"", 1102)
, Tuple.Create(Tuple.Create("", 1066), Tuple.Create("Log", 1066), true)
, Tuple.Create(Tuple.Create(" ", 1069), Tuple.Create("in", 1070), true)
, Tuple.Create(Tuple.Create(" ", 1072), Tuple.Create("using", 1073), true)
, Tuple.Create(Tuple.Create(" ", 1078), Tuple.Create("your", 1079), true)
            
            #line 25 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
                                                                                   , Tuple.Create(Tuple.Create(" ", 1083), Tuple.Create<System.Object, System.Int32>(p.Caption
            
            #line default
            #line hidden
, 1084), false)
, Tuple.Create(Tuple.Create(" ", 1094), Tuple.Create("account", 1095), true)
);

WriteLiteral(">");

            
            #line 25 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
                                                                                                                                                                                   Write(p.AuthenticationType);

            
            #line default
            #line hidden
WriteLiteral("</button>\r\n");

            
            #line 26 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("                </p>\r\n            </div>\r\n");

            
            #line 29 "..\..\Views\Account\_ExternalLoginsListPartial.cshtml"
        }
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
