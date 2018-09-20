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
    public class ChatGroupController : APIBase
    {
        int DeviceId;

        [ActionName("InsertGroupDtl")]
        public string Post([FromBody]GroupBAL value)
        {
            string result;
            try
            {
                DataTable dt = getDeviceDtlByAppId(value.AppId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(value.GetDeviceByAppId());
                    if (DeviceId > 0)
                    {
                        result = value.InsertIntoGrp();
                        return result;
                    }
                    else
                    {

                        return "0";
                    }
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
        }


    }
}

