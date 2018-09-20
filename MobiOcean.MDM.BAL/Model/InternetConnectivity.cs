using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InternetConnectivity
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class InternetConnectivity
    {
        public int DeviceId { get; set; }
        public string ConnectedToWiFi { get; set; }
        public string LogDateTime { get; set; }
        public string ConnectedToMobile { get; set; }
        public string ConnectivityName { get; set; }
        public string ConnectivityType { get; set; }
        public string AppId { get; set; }
        
    }
}