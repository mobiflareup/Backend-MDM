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
    public class GcmController : APIBase
    {
        DataTable dt;
        GCMBAL gcm;
        SendSMSBAL sms;
        int UserId = 0, ClientId = 0, DeviceId = 0;



        // POST api/<controller>
        [ActionName("GCMSender")]
        public string Post([FromBody]GCMBAL value)
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
                try
                {
                    value.DeviceId = DeviceId;
                    return value.UpdateGCMSenderId();
                }
                catch (Exception)
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        [ActionName("PendingSmS")]
        public string Post(string AppId)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
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

                    gcm = new GCMBAL();
                    gcm.DeviceId = DeviceId;
                    return gcm.GiveStngSMS();
                }
                catch (Exception)
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        [ActionName("GCMAck")]
        public string Post(string SendMsgIdList, int temp = 0)
        {
            gcm = new GCMBAL();
            gcm.SendMsgIdList = SendMsgIdList;
            return gcm.SetMsgStatusOnAckwldgment();
        }
        [ActionName("SetGCM")]
        public void Get()
        {
            gcm = new GCMBAL();
            dt = new DataTable();
            dt = gcm.SetGCM();// = SendMsgIdList;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        sms = new SendSMSBAL();
                        sms.sendMsgUsingSMS(row["Message"].ToString(), row["MobileNo"].ToString(), Convert.ToInt32(row["ClientId"].ToString()),1);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        //[HttpGet]
        //public void TestGCM()
        //{

        //    try
        //    {
        //        sms = new SendSMSBAL();
        //        sms.SendMsgUsingGCM("Gbox set as WP7", "7845461739", 208);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    //}
        //    //}
        //}
    }
}
