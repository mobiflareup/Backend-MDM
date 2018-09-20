using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for sensoreBAL
/// </summary>
namespace MobiOcean.MDM.BAL.Model
{
    public class SensorModel
    {
        public int SensorId { get; set; }
        public int BranchId { get; set; }
        public int ProfileId { get; set; }
        public int SensorStatus { get; set; }
        public int WifiStatus { get; set; }
        public string SensorName { get; set; }
        public string Descripition { get; set; }
        public string BSSID { get; set; }
        public string SSID { get; set; }
        public string Password { get; set; }
       

       
    }
}
