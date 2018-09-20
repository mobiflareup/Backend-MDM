using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WebsiteUrl
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.Model
{
    public class WebsiteUrlBase
    {
        public int IsWhiteList { get; set; }
        public List<WebsiteUrlChild> urls { get; set; }
    }
    public class WebsiteUrlChild
    {
        public string Url { get; set; }        
    }
}