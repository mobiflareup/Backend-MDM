using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controller
{
    public class SubscribeController : APIBase
    {
        FeatureBAL featureayBAL;
        SubscribeBAL sub;       
        [ActionName("CalculatePrice")]
        public string Post([FromBody]Calculation cal)
        {
            featureayBAL = new FeatureBAL();
            return featureayBAL.calculateprice(cal.categoryids, cal.duration, cal.noofsolution);
        }
        [ActionName("ChkExpiry")]
        public string Get(string appId,int appTypeId=0)
        {
            int UserId = 0, ClientId = 0, DeviceId = 0;
            try
            {
                DataTable dt = new DataTable();
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
                ClientId = 0;
            }
            if (ClientId > 0)
            {
                try
                {
                    sub = new SubscribeBAL();
                    sub.ClientId = ClientId;
                    DataTable res = sub.GetSubscriptionByClientId();
                    if (res.Rows.Count > 0)
                    {
                        return "GBox set as EC " + Convert.ToDateTime(res.Rows[0]["ExpiryDateTime"]).ToString("dd MM yyyy");
                    }
                    else
                    {
                        return "-1";
                    }
                }
                catch
                {
                    return "-1";
                }
            }
            else
            {
                //if (appId.Contains("G"))
                //{
                //    return "GBox set as EC 31 12 2017";
                //}
                return "-1";
            }


        }
        [HttpPost]
        public string ChkExpiryByClient(int clientId)
        {
            if (clientId > 0)
            {
                try
                {
                    sub = new SubscribeBAL();
                    sub.ClientId = clientId;
                    DataTable res = sub.GetSubscriptionByClientId();
                    if (res.Rows.Count > 0)
                    {
                        return "GBox set as EC " + Convert.ToDateTime(res.Rows[0]["ExpiryDateTime"]).ToString("dd MM yyyy");
                    }
                    else
                    {
                        return "-1";
                    }
                }
                catch
                {
                    return "-1";
                }
            }
            else
            {
                return "-1";
            }


        }

    }
    public class SubscribeCheckController : APIBase
    {
        [ActionName("SubcribeInsert")]
        public int Post([FromBody]SubscribeBAL sub)
        {
            int res = sub.SubcribtionCheckandInsert();
            if (res > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
