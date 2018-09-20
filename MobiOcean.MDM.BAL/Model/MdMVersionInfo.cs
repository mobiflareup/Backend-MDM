using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MdMVersion
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class MdMVersionInfo
    {
        public int DeviceId { get; set; }
        public string ClientVersionMDM { get; set; }
        public string AppId { get; set; }
        public string dateTime { get; set; }
        public int AppTypeId { get; set; }
        public int InstalledStatus { get; set; }
    }
}
