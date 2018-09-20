using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controller
{
    public class DeviceInfoController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;        
        UserBAL userBAL;
        DeviceInfoBAL devinfobal;
        LoginBAL loginBal;
        UserDeviceBAL usrdevicebal;
        ClientBAL client;
        SendSMSBAL sms;
        

        [ActionName("CheckRemoteLockPin")]
        public string Get(string Id)
        {
            try
            {
                DataTable dtdevice = getDeviceDtlByAppId(Id);
                if (dtdevice.Rows.Count > 0)
                {
                    dt = new DataTable();
                    usrdevicebal = new UserDeviceBAL();
                    usrdevicebal.APPId = Id;
                    dt = usrdevicebal.CheckRemoteLockPin();
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0]["PIN"].ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        [ActionName("InsertBatteryInfo")]
        public string Post([FromBody]BatteryInfo batteryinfo)
        {
            if (!string.IsNullOrWhiteSpace(batteryinfo.LogDateTime))
            {
                try
                {
                    dt = new DataTable();
                    dt = getDeviceDtlByAppId(batteryinfo.AppId);
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
                try
                {
                    if (DeviceId > 0)
                    {
                        batteryinfo.DeviceId = DeviceId;
                        devinfobal = new DeviceInfoBAL();
                        devinfobal.batteryinfo = batteryinfo;
                        if (batteryinfo.BatteryStatus != "Charging")
                        {
                            client = new ClientBAL();
                            client.ClientId = ClientId;
                            DataTable dtClient = client.GetClientByClientId();
                            if (dtClient != null && dtClient.Rows.Count > 0 && Convert.ToDateTime(dtClient.Rows[0]["ExpiryDate"].ToString())>=GetCurrentDateTimeByUserId(UserId) && dtClient.Rows[0]["DeviceId"] != null && dtClient.Rows[0]["DeviceId"].ToString() == "1")
                            {
                                sendBatteryAlert(Convert.ToInt32(batteryinfo.BatteryPercent.Replace("%", "")), dt);
                            }
                        }
                        devinfobal.InsertBatteryInfo1();
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
                catch
                {
                    return "0";
                }
            }
            return "0";
        }
        [ActionName("InsertNetworkInfo")]
        public string Post([FromBody]NetworkInfo networkinfo)
        {
            if (!string.IsNullOrWhiteSpace(networkinfo.LogDateTime))
            {
                try
                {
                    dt = new DataTable();
                    dt = getDeviceDtlByAppId(networkinfo.AppId);
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
                try
                {
                    if (DeviceId > 0)
                    {

                        networkinfo.DeviceId = DeviceId;
                        devinfobal = new DeviceInfoBAL();
                        devinfobal.networkinfo = networkinfo;
                        return devinfobal.InsertNetworkInfo();
                    }
                    else
                    {
                        return "0";
                    }
                }
                catch
                {
                    return "0";
                }
            }
            return "0";
        }
        [ActionName("InsertInternetConnectivity")]
        public string Post([FromBody]InternetConnectivity internetconnectivity)
        {
            if (!string.IsNullOrWhiteSpace(internetconnectivity.LogDateTime))
            {
                try
                {
                    dt = new DataTable();
                    dt = getDeviceDtlByAppId(internetconnectivity.AppId);
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
                try
                {
                    if (DeviceId > 0)
                    {

                        internetconnectivity.DeviceId = DeviceId;
                        devinfobal = new DeviceInfoBAL();
                        devinfobal.internetconnectivity = internetconnectivity;
                        return devinfobal.InsertInternetConnectivity();
                    }
                    else
                    {
                        return "0";
                    }
                }
                catch
                {
                    return "0";
                }
            }
            return "0";
        }
        [ActionName("InsertMdMVersion")]
        public string Post([FromBody]MdMVersionInfo mdmversioninfo)
        {
            if (!string.IsNullOrWhiteSpace(mdmversioninfo.dateTime))
            {
                try
                {
                    dt = new DataTable();
                    dt = getDeviceDtlByAppId(mdmversioninfo.AppId);
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
                try
                {
                    if (DeviceId > 0)
                    {

                        mdmversioninfo.DeviceId = DeviceId;
                        devinfobal = new DeviceInfoBAL();
                        devinfobal.mdmversioninfo = mdmversioninfo;
                        return devinfobal.InsertMdMVersion();
                    }
                    else
                    {
                        return "0";
                    }
                }
                catch
                {
                    return "0";
                }
            }
            return "0";
        }
        [HttpPost]
        public string InsertMdMVersion1(MdMVersionInfo mdmversioninfo)
        {
            if (!string.IsNullOrWhiteSpace(mdmversioninfo.dateTime))
            {
                try
                {
                    dt = new DataTable();
                    dt = getDeviceDtlByAppId(mdmversioninfo.AppId);
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
                try
                {
                    if (DeviceId > 0)
                    {

                        mdmversioninfo.DeviceId = DeviceId;
                        devinfobal = new DeviceInfoBAL();
                        devinfobal.mdmversioninfo = mdmversioninfo;
                        return devinfobal.InsertMdMVersion1();
                    }
                    else
                    {
                        return "0";
                    }
                }
                catch
                {
                    return "0";
                }
            }
            return "0";
        }
        [ActionName("InsertDeviceInfo")]
        public string Post([FromBody]DeviceInfoHistory deviceinfo)
        {
            if (!string.IsNullOrWhiteSpace(deviceinfo.LogDateTime))
            {
                try
                {
                    dt = new DataTable();
                    dt = getDeviceDtlByAppId(deviceinfo.AppId);
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
                try
                {
                    if (DeviceId > 0)
                    {

                        deviceinfo.DeviceId = DeviceId;
                        devinfobal = new DeviceInfoBAL();
                        devinfobal.deviceinfo = deviceinfo;
                        return devinfobal.InsertDeviceInfo();
                    }
                    else
                    {
                        return "0";
                    }
                }
                catch
                {
                    return "0";
                }
            }
            return "0";
        }
        [ActionName("LoginInfo")]
        public string Post([FromBody]LoginBAL logbal)
        {
            string LoginKey;
            try
            {
                dt = new DataTable();
                dt = logbal.ValidateLogin();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["IsFirstLogin"].ToString() == "1")
                    {
                        return "-1";
                    }
                    else
                    {
                        LoginKey = GenToken(15, "");// GenPass("SaurabhAggarwal");
                        loginBal = new LoginBAL();
                        loginBal.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                        loginBal.ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                        loginBal.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"]);
                        loginBal.UserName = dt.Rows[0]["UserName"].ToString();
                        loginBal.DeptId = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["DeptId"].ToString()) ? "0" : dt.Rows[0]["DeptId"].ToString());
                        loginBal.LoginKey = LoginKey;
                        loginBal.currentDateTime = GetCurrentDateTimeByUserId(Convert.ToInt32(dt.Rows[0]["UserID"]));
                        if (loginBal.InsertLoginData1() > 0)
                        {
                            if (string.IsNullOrEmpty(logbal.Payment))
                            {
                                logbal.Payment = "No=&Time=&Ftr=";
                            }
                            return LoginKey + "&EmailId=" + logbal.EmailId + "&" + logbal.Payment + "&UserName=" + loginBal.UserName;
                        }
                        else
                        {
                            return "0";
                        }
                    }
                }
                else
                {
                    //functionlist = new LoginBAL();
                    return "0";
                }
                //return functionlist;
            }
            catch
            {
                return "0";
            }
        }
        [ActionName("InsertClientManager")]
        public string Post([FromBody]ClientBAL value)
        {
            if (isValidEmail(value.EmailId))
            {
                try
                {
                    if (value.ConfirmPassword == value.Password)
                    {
                        value.currentDateTime = GetCurrentDateTimeByCountry(1);
                        return (value.InsertClientManager()).ToString();
                    }
                    else
                    {
                        return "Password and Confirm Password Doesn't Match";

                    }
                }
                catch (Exception)
                {
                    return "Error, Registration Not Completetd";
                }
            }
            else
            {
                return "Please Enter A Valid EmailId";
            }
        }
        public bool isValidEmail(string EmailId)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(EmailId))
                return (true);
            else
                return (false);
        }      
        [ActionName("IUDeviceRouted")]
        public int Post([FromBody]DeviceInfoBAL value)
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
            try
            {
                if (DeviceId > 0)
                {
                    devinfobal = new DeviceInfoBAL();
                    devinfobal.ClientId = ClientId;
                    devinfobal.UserId = UserId;
                    devinfobal.DeviceId = DeviceId;
                    devinfobal.LogDateTime = value.LogDateTime;
                    devinfobal.IsDeviceRouted = value.IsDeviceRouted;
                    devinfobal.IU_IsDeviceRouted();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void sendBatteryAlert(int batterypercentage,DataTable dtDevice)
        {
            if (batterypercentage <= 25)
            {
                devinfobal = new DeviceInfoBAL();
                devinfobal.ClientId = ClientId;
                devinfobal.DeviceId = DeviceId;

                DateTime? date = devinfobal.LastSMSSentTime();
                bool help = false;
                if (batterypercentage <= 10 && (string.IsNullOrEmpty(date.ToString()) || DateTime.UtcNow.AddMinutes(Constant.addMinutes).Subtract((DateTime)date).Minutes > 10))
                {
                    help = true;
                }
                else if (string.IsNullOrEmpty(date.ToString()) || DateTime.UtcNow.AddMinutes(Constant.addMinutes).Subtract((DateTime)date).Minutes > 30)
                {
                    help = true;
                }
                if (help)
                {
                    sms = new SendSMSBAL();
                    userBAL = new UserBAL();
                    userBAL.ClientId = ClientId;
                    userBAL.RoleId = 2;
                    DataTable dt2 = userBAL.GetUserByRoleId();


                    userBAL.UserId = UserId;
                    DataTable dtUser = userBAL.GetUserDtlByUserId();

                    string text = dtUser.Rows[0]["UserName"] + "'s (" + dtDevice.Rows[0]["MobileNo1"] + ") battery is Discharging ( current is " + batterypercentage + "%). Sent at " + GetCurrentDateTimeByUserId(UserId);
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        if (j == 0)
                            sms.sendMsgUsingSMS(text, dt2.Rows[j]["MobileNo"].ToString(), ClientId,1);
                        else
                            break;
                    }
                    devinfobal.InsertOrUpdateBatterInfoSMS();
                }

            }
        }
        //[HttpGet]
        //public string ActualCal()
        //{
        //    client = new ClientBAL();
        //    int remaining = client.ActualCal(208, 387, 38);
        //    if (remaining == 20)
        //    {
        //        return "Your Message Balance is Low.";
        //    }
        //    else if (remaining == 10)
        //    {
        //        return "Your Message Balance is Low.";
        //    }
        //    else if(remaining == -10)
        //    {
        //        return "Your Message Balance is Low.";
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
    }
}
