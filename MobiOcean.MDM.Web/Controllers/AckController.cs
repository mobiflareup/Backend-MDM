using MobiOcean.MDM.BAL;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using MobiOcean.MDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controller
{
    public class GetAckController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        AckBAL ack;
        DataTable dt;
        [ActionName("GetAck")]
        public string Post([FromBody]AckBAL ack)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(ack.AppId);
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
                    return ack.GetAck();
                }
                catch
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }

        [ActionName("CheckUpdates")]
        public List<Result> Get(string Id)
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
                ack = new AckBAL();
                ack.ClientId = ClientId;
                ack.UserDeviceId = DeviceId;
                ack.UserId = UserId;
                ack.AppId = Id;
                ack.currentDateTime = GetCurrentDateTimeByUserId(UserId);
                return ack.CheckUpdates();
            }
            else
            {
                List<Result> lst = new List<Result>();
                return lst;
            }

        }


        [ActionName("MapMyIndia")]
        public string Gets(string area, string Id)
        {
            ack = new AckBAL();
            ack.ClientId = Convert.ToInt32(Id);
            dt = new DataTable();
            dt = ack.GetURLforMap(Constant.AutoSuggest);
            if (dt != null && dt.Rows.Count>0)
            {
               string Api = dt.Rows[0]["API"].ToString().Replace("<API_KEY>", dt.Rows[0]["Key"].ToString());
                RestClient restClient = new RestClient(Api, HttpVerb.GET, "");//"http://apis.mapmyindia.com/advancedmaps/v1/kqq6h7jrnlzzox3co5mqvief7lnhfsob/autosuggest?q="
                return restClient.MakeRequest(area);
            }
            else
            {
                return "{responseCode : 0}";
            }
        }
        [ActionName("ApiCount")]
        public int getApiCount(string ClientId, string Type, string IsUsed, string UserId)
        {
            AckBAL ack = new AckBAL();
            var res = ack.insertApiCount(Convert.ToInt64(ClientId),Convert.ToInt16(Type), Constant.GetMapIsUsed(IsUsed), Convert.ToInt32(UserId));
            return res;
        }
        //[ActionName("MapMyIndiaRoute")]
        //public string Get(string start, string destination, string via, string id)
        //{
        //    ack = new AckBAL();
        //    ack.ClientId = Convert.ToInt32(id);
        //    dt = new DataTable();
        //    dt = ack.GetURLforMap(Constant.AutoSuggest);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        string Api = dt.Rows[0]["API"].ToString().Replace("<API_KEY>", dt.Rows[0]["Key"].ToString()).Replace("<start>", start).Replace("<destination>", destination).Replace("<via>", via);
        //        RestClient restClient = new RestClient(Api, HttpVerb.GET, "");//"http://apis.mapmyindia.com/advancedmaps/v1/kqq6h7jrnlzzox3co5mqvief7lnhfsob/autosuggest?q="
        //        return restClient.MakeRequest();
        //    }
        //    else
        //    {
        //        return "{responseCode : 0}";
        //    }
        //    // url = url.Replace("<rest_lic_key>", "kqq6h7jrnlzzox3co5mqvief7lnhfsob");
        //    //string Api = "http://apis.mapmyindia.com/advancedmaps/v1/kqq6h7jrnlzzox3co5mqvief7lnhfsob/route?start=" + start + "&destination=" + destination + "&viapoints=" + via + "&rtype=0&vtype=1&avoids=1&with_advices=1&alternatives=false";

        //    //RestClient restClient = new RestClient(Api, HttpVerb.POST, "");
        //   // var data = restClient.MakeRequest();

        //   // return data;
        //}

    }
}
