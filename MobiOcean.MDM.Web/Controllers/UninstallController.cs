using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controller
{
    public class UninstallController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;
        GingerboxSrch andrdSrch;
        UserDeviceBAL usrdevicebal;
        // GET api/<controller>
        [ActionName("getUninstallationPass")]
        public string Get(string Id)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(Id);
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
                try
                {
                    andrdSrch = new GingerboxSrch();
                    return "GBox set as PP" + andrdSrch.getUninstallationPasswrdByAndroidAppId(Id);
                }
                finally
                {
                    andrdSrch = null;
                }
            }
            else
            {
                return "-1";
            }
        }

        // POST api/<controller>
        [ActionName("setAppInstallationStatus")]
        public string Post([FromBody]Uninstall value)
        {
            try
            {
                //if (value.AndroidAppId.Contains("G"))
                //{
                //    return "0";
                //}
                usrdevicebal = new UserDeviceBAL();
                usrdevicebal.APPId = value.AndroidAppId;
                usrdevicebal.IsAppUninstalled = Convert.ToInt32(value.isUninstalled);
                return usrdevicebal.setAndroidAppInstalledStatus() + "";
            }
            finally
            {
                usrdevicebal = null;
            }
        }


    }
}
