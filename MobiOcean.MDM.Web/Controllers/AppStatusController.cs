using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controller
{
    public class AppStatusController : APIBase
    {
        DataTable dt;
        RptBAL rpt;
        int UserId = 0, ClientId = 0, DeviceId = 0;

        [ActionName("ChkVersion")]
        public string Get(string AppId, string Version, int AppTypeId=1 )
        {
            try
            {
                return ChkforupdatedApk(AppId, Version, AppTypeId) + "";
            }
            catch (Exception)
            {
                return "-1";
            }
        }
        protected string ChkforupdatedApk(string appId, string InstalledVersion, int AppTypeId)
        {
            try
            {
                try
                {
                    dt = new DataTable();
                    dt = getDeviceDtlByAppId(appId);
                    if (dt.Rows.Count > 0)
                    {
                        DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                        UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    }
                }
                catch (Exception)
                {
                    DeviceId = 0;
                }
                if (DeviceId > 0)
                {
                    rpt = new RptBAL();
                    rpt.InstalledVersion = InstalledVersion;
                    rpt.ApptypeId = AppTypeId;
                    return rpt.ChkforUpdate();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
            //return "1";

        }
    }
}
