using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BatteryInfo
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.Model
{
    public class BatteryInfo
    {
       
        public int DeviceId { get; set; }        
        public string Battery_Info { get; set; }
        public string Voltage { get; set; }
        public string LogDateTime { get; set; }
        public string Temperature { get; set; }
        public string BatteryPercent { get; set; }
        public string BatteryStatus { get; set; }
        public string BatteryHealth { get; set; }
        public string AppId { get; set; }
    }
}