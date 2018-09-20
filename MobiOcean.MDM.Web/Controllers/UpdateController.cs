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
    public class UpdateController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        AckBAL ack;
        DataTable dt;
        [ActionName("SetAck")]
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
                    DateTime AckDateTime = Convert.ToDateTime(ack.AckDateTime);
                    ack.AckDateTime = AckDateTime.ToString("dd-MMM-yyyy HH:mm");
                    return ack.SetAck();
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
                return ack.CheckUpdatesBySyncId();
            }
            else
            {
                List<Result> lst = new List<Result>();
                return lst;
            }

        }
    }
}
