using Microsoft.AspNet.FriendlyUrls.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobiOcean.MDM.Web
{
    public class MyWebFormsFriendlyUrlResolver : WebFormsFriendlyUrlResolver
    {
        public MyWebFormsFriendlyUrlResolver() { }

        public override string ConvertToFriendlyUrl(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (path.ToLower().Contains("kycbrowserforward"))
                { // Here the filter code
                    return path;
                }
            }
            return base.ConvertToFriendlyUrl(path);
        }
    }
}