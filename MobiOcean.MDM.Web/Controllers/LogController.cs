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
    public class LogController : APIBase
    {
        KeywordBAL keyword;
        AlertBAL alert;
        UserBAL usr;
        LocationBAL locationBal;
        SendSMSBAL sms;
        DataTable dt, dt1, dt3, dt5;


        [ActionName("CallLogs")]
        public string Post([FromBody]CallSmsLog value)
        {
            int UserId = 0, ClientId = 0, DeviceId = 0;
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
                value.DeviceId = DeviceId;
                value.UserId = UserId;
                value.ClientId = ClientId;
                value.startDateTime = getDateInSQlServerFormat(value.startDateTime);

                locationBal = new LocationBAL();
                LocationModel locationModel = locationBal.GetLocation(value.Lat, value.Long, value.cellId.ToString(), value.locationAreaCode.ToString(), value.mobileCountryCode.ToString(), value.mobileNetworkCode.ToString(), ClientId, Constant.tblCallLog, UserId);
                value.Lat = locationModel.latitude.ToString();
                value.Long = locationModel.longitude.ToString();
                value.location = locationModel.location;
                //bileNo = value.MobileNo;
                try
                {
                    alert = new AlertBAL();
                    alert.ClientId = ClientId;
                    alert.UserId = UserId;
                    if (value.IsIncoming == 1)
                        alert.MobileNo = value.From;
                    else
                        alert.MobileNo = value.To;
                    int res = alert.GetMobileNoIfEnable();
                    if (res > 0)
                    {

                        // Insert Alert
                        string text = "";
                        usr = new UserBAL();
                        usr.UserId = UserId;
                        usr.ClientId = ClientId;
                        dt3 = new DataTable();
                        dt5 = new DataTable();
                        dt5 = GetAdminDetail(ClientId);
                        dt3 = usr.GetUserDtlByUserId();
                        if (value.IsIncoming == 1)
                            text = "A call received by your employee, " + dt3.Rows[0]["UserName"].ToString() + " from this no (" + value.From + ") at " + value.startDateTime + ".";//Dear " + row["UserName"].ToString() + ",
                        else
                            text = "A call dialled by your employee, " + dt3.Rows[0]["UserName"].ToString() + " to this no (" + value.To + ") at " + value.startDateTime + ".";
                        if (dt5.Rows.Count > 0 && dt3.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt5.Rows)
                            {
                                string text1 = "Dear " + row["UserName"].ToString() + " " + text;                                
                                sms = new SendSMSBAL();
                                sms.sendMsgUsingSMS(text1, row["MobileNo"].ToString(), ClientId,1);
                            }
                        }
                        IsAlertEnable(1, text, UserId, ClientId);
                    }

                }
                catch (Exception)
                {
                }
                return value.insertCallLogs() + "";
            }
            else
            {
                return -2 + "";
            }
        }
        [ActionName("SMSLogs")]
        public string Post([FromBody]SmsCallLog value)
        {
            int res = 0;
            string MsgText = "", FoundKeyWord = "";
            int UserId = 0, ClientId = 0, DeviceId = 0;
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


                value.DeviceId = DeviceId;
                value.UserId = UserId;
                value.ClientId = ClientId;
                value.startDateTime = getDateInSQlServerFormat(value.startDateTime);
                MsgText = value.MsgText;

                locationBal = new LocationBAL();
                LocationModel locationModel = locationBal.GetLocation(value.Lat, value.Long, value.cellId.ToString(), value.locationAreaCode.ToString(), value.mobileCountryCode.ToString(), value.mobileNetworkCode.ToString(), ClientId, Constant.tblSMSLogs, UserId);
                value.Lat = locationModel.latitude.ToString();
                value.Long = locationModel.longitude.ToString();
                value.location = locationModel.location;

                try
                {
                    alert = new AlertBAL();
                    alert.ClientId = ClientId;
                    alert.UserId = UserId;
                    if (value.IsIncoming == 1)
                        alert.MobileNo = value.From;
                    else
                        alert.MobileNo = value.To;
                    res = alert.GetMobileNoIfEnable();

                }
                catch (Exception)
                {
                    res = 0;
                }
                try
                {
                    dt1 = new DataTable();
                    keyword = new KeywordBAL();
                    keyword.ClientId = ClientId;
                    keyword.UserId = UserId;
                    dt1 = keyword.GetKeywordListIfEnable();
                    foreach (DataRow rows in dt1.Rows)
                    {
                        if (MsgText.Contains(rows["KeywordName"].ToString()))
                        {
                            FoundKeyWord = FoundKeyWord + "," + rows["KeywordName"].ToString();
                        }
                    }
                }
                catch (Exception) { }
                finally
                {
                    dt1 = null;
                    keyword = null;
                }



                try
                {
                    if (res > 0 || FoundKeyWord.Length > 0)
                    {
                        string text = "";
                        usr = new UserBAL();
                        usr.UserId = UserId;
                        usr.ClientId = ClientId;
                        dt3 = new DataTable();
                        dt5 = new DataTable();
                        dt5 = GetAdminDetail(ClientId);
                        dt3 = usr.GetUserDtlByUserId();
                        if (res > 0 && FoundKeyWord.Length > 0)
                        {
                            FoundKeyWord = FoundKeyWord.Substring(1);
                            if (value.IsIncoming == 1)
                                text = " A SMS received by your employee, " + dt3.Rows[0]["UserName"].ToString() + " from this no (" + value.From + ") at " + value.startDateTime + ". And these words(" + FoundKeyWord + ") found in the SMS";
                            else
                                text = "A SMS sent by your employee, " + dt3.Rows[0]["UserName"].ToString() + " to this no (" + value.To + ") at " + value.startDateTime + ". And these words(" + FoundKeyWord + ") found in the SMS";
                        }
                        else if (res > 0)
                        {
                            if (value.IsIncoming == 1)
                                text = " A SMS received by your employee, " + dt3.Rows[0]["UserName"].ToString() + " from this no (" + value.From + ") at " + value.startDateTime + ".";
                            else
                                text = " A SMS sent by your employee, " + dt3.Rows[0]["UserName"].ToString() + " to this no (" + value.To + ") at " + value.startDateTime + ".";
                        }
                        else
                        {
                            FoundKeyWord = FoundKeyWord.Substring(1);
                            text = "These words(" + FoundKeyWord + ") found in the SMS of your employee " + dt3.Rows[0]["UserName"].ToString() + ".";
                        }
                        foreach (DataRow row in dt5.Rows)
                        {
                            string text1 = "Dear " + row["UserName"].ToString() + " " + text;                            
                            sms = new SendSMSBAL();
                            sms.sendMsgUsingSMS(text1, row["MobileNo"].ToString(), ClientId,1);
                        }
                        IsAlertEnable(2, text, UserId, ClientId);
                    }

                }
                catch (Exception) { }
                return value.insertSMSLogs() + "";
            }
            else
            {
                return -2 + "";
            }
        }
        [ActionName("BulkSMS")]
        public string Post([FromBody]BulkSMS value)
        {

            int UserId = 0, ClientId = 0, DeviceId = 0;
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
                value.DeviceId = DeviceId;
                value.UserId = UserId;
                value.LogDateTime = getDateInSQlServerFormat(value.LogDateTime);
                return value.insertSMSLogs() + "";
            }
            else
            {
                return -2 + "";
            }
        }

    }
}
