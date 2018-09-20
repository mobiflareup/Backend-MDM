using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;
using Microsoft.AspNet.FriendlyUrls.Resolvers;

namespace MobiOcean.MDM.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            
            //routes.Ignore("KycBrowserForward.aspx");
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;            
            routes.EnableFriendlyUrls(settings);//, new IFriendlyUrlResolver[] { new MyWebFormsFriendlyUrlResolver() }
        }
    }
}
