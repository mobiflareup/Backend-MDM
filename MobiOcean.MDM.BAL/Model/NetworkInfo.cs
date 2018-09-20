using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NetworkInfo
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class NetworkInfo
    {
        public int DeviceId { get; set; }
        public string NetworkStrength { get; set; }        
        public string LogDateTime { get; set; }       
        public string NetworkTypeInfo { get; set; }        
        public string NetworkStatus { get; set; }       
        public string Roaming { get; set; }        
        public string IsConnectedToProvisioningNetwork { get; set; }
        public string AppId { get; set; }
    }
}