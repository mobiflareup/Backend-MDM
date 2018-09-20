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
    public class getLocReqFreqController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;
        GroupBAL grp;

        // GET api/<controller>
        [ActionName("getLocReqFreq")]
        public string Get(string Id, int IsLoc = 0)
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
                    grp = new GroupBAL();
                    grp.UserId = UserId;
                    if (IsLoc == 0)
                    {
                        return grp.GetFreqMsgForApp();
                    }
                    else
                    {
                        return grp.GetGeoFenceFreqMsgForApp();
                    }
                }
                finally
                {
                    grp = null;
                }
            }
            else
            {
                return "NA";
            }
        }


    }
}

