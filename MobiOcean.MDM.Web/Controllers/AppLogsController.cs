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
    public class AppLogsController : APIBase
    {
        //Global Initialization of variables
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;


        [ActionName("AppLogs")] // Webservice Name
        public string Post([FromBody]AppLogsBAL value)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(value.AppId);
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
                value.ClientId = ClientId;
                value.DeviceId = DeviceId;
                value.UserId = UserId;
                value.StartTime = getDateInSQlServerFormat(value.StartTime);
                value.EndTime = getDateInSQlServerFormat(value.EndTime);
                value.LogDateTime = getDateInSQlServerFormat(value.LogDateTime);
                value.currentDateTime = GetCurrentDateTimeByUserId(UserId);
                return value.InsertAppLogs();
            }
            else
            {
                return "0";
            }
        }
    }
}
