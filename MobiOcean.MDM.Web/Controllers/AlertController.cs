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
    public class AlertController : APIBase
    {
        DataTable dt;
        int DeviceId = 0, ClientId = 0, UserId = 0;
        [ActionName("InsertAlertDetails")]
        public int Post([FromBody]AlertBAL alert)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(alert.AppId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
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
                    alert.UserId = UserId;
                    alert.ClientId = ClientId;
                    dt = alert.GetDeptHeadAndAdmin();
                    foreach (DataRow row in dt.Rows)
                    {
                        alert.ForUserId = Convert.ToInt32(row["UserId"].ToString());
                        alert.UserId = UserId;
                        alert.ClientId = ClientId;
                        alert.LogDateTime = getDateInSQlServerFormat(alert.LogDateTime);
                        alert.LoggedBy = UserId;
                        alert.InsertIntotblAlert();
                    }
                }

                catch (Exception)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            return 1;
        }

    }
}
