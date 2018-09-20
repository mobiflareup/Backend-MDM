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
    public class AndroidDocController : APIBase
    {
        DataTable dt, dt1;        
        LocationBAL locationBal;
        UserBAL userBal;
        ClientBAL client;

        int UserId = 0, ClientId = 0, DeviceId = 0;

        [ActionName("BtnPress_XMLFormat")]
        public string Post([FromBody]DocBAL value)
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

                    value.ClientId = ClientId;
                    value.UserId = UserId;
                    value.DeviceId = DeviceId;
                    value.dateTime = Convert.ToDateTime(value.dateTime).ToString("dd-MMM-yyyy HH:mm");

                    locationBal = new LocationBAL();
                    LocationModel locationModel = locationBal.GetLocation(value.Lat, value.Long, value.CellId.ToString(), value.locationAreaCode.ToString(), value.mobileCountryCode.ToString(), value.mobileNetworkCode.ToString(), ClientId, Constant.AndroidBtnPressDtls, UserId);
                    value.Lat = locationModel.latitude.ToString();
                    value.Long = locationModel.longitude.ToString();
                    value.Location = locationModel.location;
                    string res = value.BtnPress_XMLFormat();
                    if (value.functionalityId == 10)
                    {
                        sendMobileStatusNotification(value.Remarks, value.dateTime);
                    }
                    return res;//"0";
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
        private void sendMobileStatusNotification(string Remarks, string dateTime)
        {
            try
            {
                userBal = new UserBAL();
                dt = new DataTable();
                userBal.UserId = UserId;
                dt = userBal.GetUserDtlByUserId();

                //89910445110325989519$27-8-2016 17:05
                string text = userBal.ChkSimChange(Remarks, DeviceId);
                if (text != "0")
                {
                    string MsgText = dt.Rows[0]["UserName"].ToString() + " switched on the mobile at " + dateTime + " . " + text;
                    if (ChkIFFeatureIsEnableAccordingDate(UserId, dateTime, 12) > 0) // Chk Sim Change Notification Need or Not
                    {
                        IsAlertEnable(5, MsgText, UserId, ClientId);
                    }
                    MsgText = dt.Rows[0]["UserName"].ToString() + " (" + dt.Rows[0]["MobileNo"].ToString() + ") switched on the mobile at " + dateTime + " . " + text;

                    dt1 = new DataTable();
                    client = new ClientBAL();
                    client.ClientId = ClientId;
                    DataTable dtClient = client.GetClientByClientId();
                    if (dtClient != null && dtClient.Rows.Count > 0 && dtClient.Rows[0]["DeviceId"] != null && dtClient.Rows[0]["DeviceId"].ToString() == "1")
                    {
                        userBal = new UserBAL();
                        userBal.RoleId = 2;
                        userBal.ClientId = ClientId;
                        dt1 = userBal.GetUserByRoleId();
                        //SendSMSBAL sms = new SendSMSBAL();
                        //for (int j = 0; j < dt1.Rows.Count; j++)
                        //{
                        //    sms.sendMsgUsingSMS(MsgText, dt1.Rows[j]["MobileNo"].ToString(), ClientId);
                        //}
                    }

                }
            }
            catch (Exception)
            { }
        }

    }
}
