using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;

namespace MobiOcean.MDM.Web.Controller
{
    public class WifiSensorController : APIBase
    {
        SensorBAL profile, profilesensorlst, sensor;
        int clientId = 0, DeviceId = 0, UserId = 0;
        DataTable dt, dt1;
        [ActionName("GetWifiSensorDtls")]
        public List<SensorBAL> Get(string appId)
        {
            List<SensorBAL> lst = new List<SensorBAL>();
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(appId);
                if (dt.Rows.Count > 0)
                {
                    clientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
                else
                {
                    clientId = 0;
                }
            }
            catch (Exception)
            {
                clientId = 0;
            }
            if (clientId > 0)
            {
                profile = new SensorBAL();
                profile.ClientId = clientId;
                dt1 = profile.GetWifiSensorDetails();
                foreach (DataRow row in dt1.Rows)
                {
                    profilesensorlst = new SensorBAL
                    {
                        SensorId = Convert.ToInt32(row["SensorId"].ToString()),
                        ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                        ProfileName = row["ProfileName"].ToString(),
                        ProfileCode = row["ProfileCode"].ToString(),
                        ProfilePurpose = row["ProfilePurpose"].ToString(),
                        SensorName = row["SensorName"].ToString(),
                        Description = row["Descripition"].ToString(),
                        BSSID = row["BSSID"].ToString(),
                        SSID = row["SSID"].ToString(),
                        Password = row["Password"].ToString()
                    };
                    lst.Add(profilesensorlst);
                }
            }
            return lst;
        }
        [ActionName("InsertSensorDetails")]
        public int Get(string appId, int sensorId, int isSensor)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(appId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    clientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                DeviceId = 0;
            }
            try
            {
                if (DeviceId > 0)
                {
                    sensor = new SensorBAL();
                    sensor.ClientId = clientId;
                    sensor.DeviceId = DeviceId;
                    sensor.UserId = UserId;
                    sensor.LogDateTime = getDateInSQlServerFormat(GetCurrentDateTimeByUserId(UserId).ToString());
                    sensor.SensorId = sensorId;
                    sensor.IsSensor = isSensor;
                    sensor.AppId = appId;
                    return sensor.InsertIntoSensorEnable();
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
