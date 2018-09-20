using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;

/// <summary>
/// Summary description for SensoreController
/// </summary>
/// 
namespace MobiOcean.MDM.Web.Controller
{
    public class SensoreController : APIBase
    {
        int ClientId = 0;
        DataTable dt, dt1;
        SensorModel SSBB;
        SensorBAL sensorBal;
        // GET api/<controller>
        [ActionName("getSensorDetails")]
        public List<SensorModel> Get(string AppiId)
        {
            List<SensorModel> Sensorlist = new List<SensorModel>();
            try
            {
                dt = new DataTable();
                dt1 = new DataTable();
                dt = getDeviceDtlByAppId(AppiId);
                if (dt.Rows.Count > 0)
                {
                    //DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    //userId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
                sensorBal = new SensorBAL();
                sensorBal.ClientId = ClientId;
                dt1 = sensorBal.GetSensoreDetails();
                foreach (DataRow row in dt1.Rows)
                {
                    SSBB = new SensorModel
                    {
                        SensorId = Convert.ToInt32(row["SensorId"]),
                        BranchId = Convert.ToInt32(row["BranchId"]),
                        ProfileId = Convert.ToInt32(row["ProfileId"]),
                        SensorName = row["SensorName"].ToString(),
                        Descripition = row["Descripition"].ToString(),
                        BSSID = row["BSSID"].ToString(),
                        SSID = row["SSID"].ToString(),
                        Password = row["Password"].ToString(),
                        SensorStatus = Convert.ToInt32(row["SensorStatus"]),
                        WifiStatus = Convert.ToInt32(row["WiFiStatus"])
                    };
                    Sensorlist.Add(SSBB);
                }
                return Sensorlist;
            }
            catch
            {
                return Sensorlist;
            }
            finally
            {

            }

        }
    }
}