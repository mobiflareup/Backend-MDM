using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MobiOcean.MDM.BAL.Model;
using System.Net;
using System.IO;
using MobiOcean.MDM.Infrastructure;
using System.Security.Cryptography;
using MobiOcean.MDM.DAL.DAL.PhStnSynDALTableAdapters;

namespace MobiOcean.MDM.BAL.BAL
{
    public class SendSMSBAL
    {

        SMS_API_PanelTableAdapter smsPnl;
        SendSMSDetailsTableAdapter smsDtl;
        GetActiveCmnGatewayByCountryByRajTableAdapter getActiveGateWay;

        DataTable dt;
        SendSMS sms;
        SendMail mail;
        UserDeviceBAL usrDevice;
        GCM gcm;
        GCMBAL gcmbal;
        ClientBAL client;

        string mailBody = "", mailSubject = "";
        #region---------- Save Message in Send SMS For GCM --------------
        public int SaveSMSSendInfo(string ContactNo, string text, int ClientId)
        {
            try
            {
                smsDtl = new SendSMSDetailsTableAdapter();
                return Convert.ToInt32(smsDtl.INsertSendSMS(ClientId, ContactNo, text, DateTime.UtcNow.AddMinutes(330)).ToString());
            }
            finally
            {
                smsDtl = null;
            }
        }
        #endregion

        #region------------- Save SMS for SMS_API_Panel ----------
        public void SaveSMSSendInfoForAPI_Panel(string ContactNo, string text, string smsId, int ClientId)
        {
            try
            {
                string dateTime = DateTime.UtcNow.AddMinutes(330).ToString("dd-MMM-yyyy HH:mm");
                smsPnl = new SMS_API_PanelTableAdapter();
                smsPnl.IU_SMSAPIPanel(0, ClientId, smsId, ContactNo, text, 0, dateTime, dateTime, 0).ToString();
            }
            catch (Exception) { }
            finally
            {
                smsPnl = null;
            }
        }
        #endregion

        public void sendFinalSMS(string ContactNo, string text, int ClientId)
        {
            if (ContactNo.Trim() != "")
            {
                text = text.Replace("Gbox set as", "GBox set as");
                try
                {
                    int SendMsgId = SaveSMSSendInfo(ContactNo, text, ClientId);
                }
                catch (Exception) { }
                SendMsgUsingGCM(text, ContactNo, ClientId);

            }
        }


        public void sendMsgUsingSMS(string text, string ContactNo, int ClientId, int subscriptionCheck = 0)
        {
            string extra = "";
            if (subscriptionCheck == 1)
            {
                client = new ClientBAL();
                int remaining = client.ActualCal(ClientId, 0, 38);//FeatureID=38
                //if (remaining > 0)
                //if (remaining == 20)
                //{
                //    extra = ". Your Message Balance is Low, Please purchase more.";
                //}
                //else if (remaining == 10)
                //{
                //    extra = ". Your Message Balance is Low, Please purchase more.";
                //}
                //else if (remaining == -10)
                //{
                //    extra = ". Your Message Balance is Low, Please purchase more.";
                //}
                //else if (remaining == -11)
                //{
                //    extra = "False";
                //}
            }
            if (extra != "False")
            {
                sms = new SendSMS();
                ContactNo = ContactNo.Trim();
                //text += extra;
                string res = FindGateway(ContactNo, ClientId);
                if (!string.IsNullOrEmpty(res))
                {
                    //if(text.Contains("GBox"))
                    //{
                    //    text = "GBox set as " + encrypt(text.Substring(12));
                    //}
                    res = sms.sendMsgUsingSMS(text, ContactNo, ClientId, res);
                }
                else
                {
                    string Emailid = FindEmailid(ContactNo, ClientId);
                    if (Emailid != "0")
                    {
                        sendMailOfSMS(Emailid, "Send Mail Of SMS", text, ClientId);
                        res = "Sent By Email";
                    }
                    else
                    {
                        res = "No Gateway found";
                    }
                }
                SaveSMSSendInfoForAPI_Panel(ContactNo, text, res, ClientId);
            }
        }
        private string FindGateway(string ContactNo, int clientId)
        {
            getActiveGateWay = new GetActiveCmnGatewayByCountryByRajTableAdapter();
            DataTable dt = getActiveGateWay.GetActiveCmnGatewayByCountryByRaj(ContactNo, clientId, "");
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["PhoneCode"].ToString().Replace("+", "") + "~" + dt.Rows[0]["Value"].ToString();
            }
            return "";
        }
        private string FindEmailid(string ContactNo, int ClientId)
        {
            getActiveGateWay = new GetActiveCmnGatewayByCountryByRajTableAdapter();
            return getActiveGateWay.GetEmailIdByClinetIdorContactByRaj(ContactNo, ClientId).ToString();
        }


        #region---SendMsgUsingGCM---
        public void SendMsgUsingGCM(string Message, string Stumobile, int ClientId)
        {
            string AppId;
            string SenderId = "";
            string result = "";
            string GCMID = "";
            int IsAndroid = 1;
            string AndAppId = "";
            try
            {
                gcm = new GCM();
                #region ---------- Get Student GCM RegistrationID by Stu Mob Number -------------
                try
                {
                    usrDevice = new UserDeviceBAL();
                    dt = new DataTable();

                    usrDevice.MobileNo1 = Stumobile;
                    dt = usrDevice.GetDeviceByMobileNo();
                    if (dt.Rows.Count > 0)
                    {
                        GCMID = dt.Rows[0]["GCMId"].ToString();
                        AndAppId = dt.Rows[0]["AppId"].ToString();
                        SenderId = dt.Rows[0]["SenderId"].ToString();
                        IsAndroid = string.IsNullOrEmpty(dt.Rows[0]["IsAndroid"].ToString()) ? 1 : Convert.ToInt32(dt.Rows[0]["IsAndroid"].ToString());
                    }
                    else
                    {
                        GCMID = "User not authenticated";
                    }
                }
                catch (Exception) { GCMID = "User not authenticated"; }

                #endregion
                if (GCMID.Trim() != "0" && !string.IsNullOrEmpty(GCMID.Trim()) && GCMID.Trim() != "User not authenticated")
                {
                    if (SenderId == "157564954376")
                    {
                        AppId = "AIzaSyCmCQ-iYcAOpRBHVhQTH-vMPFlJbltTGxQ"; //Gboxcontrol@gmail.com //For Common App
                    }
                    else if (SenderId == "854479189925")
                    {
                        AppId = "AIzaSyCZuaVB5zMbQYX_WtXfRJ8LKQ1Ne8QV5jY";
                    }
                    else
                    {
                        SenderId = "715110779681";
                        AppId = "AIzaSyD8Gom4UMgv4E_BOHeq0rcwAR4vJOsjSzg";
                    }
                    if (IsAndroid == 1)
                    {
                        result = gcm.AndroidPush(GCMID, SenderId, AppId, Message);
                    }
                    else
                    {
                        string postData = "{\"to\":\"" + GCMID + "\",\"data\":{\"body\":\"" + Message + "\"},\"content_available\":true}";
                        result = gcm.AndroidPushForIOS(GCMID, SenderId, AppId, postData);
                    }

                    #region-------- If Registration ID is Invalid, then send SMS to student to Update his/her RegId --------------
                    if (result.ToUpper().Trim().IndexOf("registration_id=") != -1)
                    {
                        UpdateGCMId(AndAppId, result);
                    }
                    #endregion
                    #region------ Send text in mail --------------------
                    mailSubject = "GCM notification send to Mob: " + Stumobile + "";
                    mailBody = "Send To Mob: " + Stumobile +
                               "<br> Registration ID: " + GCMID +
                             "<br><br>" + Message +
                             "<br><br>" +
                             "Response Given By GCM server is: " + result;

                    sendMailOfSMS("gboxcontrol@gmail.com", mailSubject, mailBody, ClientId);
                    #endregion
                }
                //else
                //{
                //    //sendMsgUsingSMS(Message, Stumobile, ClientId);
                //    //#region------ Send text in mail --------------------
                //    //mailSubject = "GCM notification send to Mob Through SMS: " + Stumobile + "";
                //    //mailBody = "Send To Mob: " + Stumobile +
                //    //           "<br> Registration ID: " + GCMID +
                //    //         "<br><br>" + Message +
                //    //         "<br><br>";// +
                //    //                    // "Response Given By GCM server is: " + result;

                //    //sendMailOfSMS("gboxcontrol@gmail.com", mailSubject, mailBody, ClientId);
                //    //#endregion
                //}

            }
            catch (Exception)
            {
                #region------ Send text in mail --------------------
                mailSubject = "GCM Exception while notification send to Mob: " + Stumobile + "";
                mailBody = "Send To Mob: " + Stumobile +
                           "<br> Registration ID: " + GCMID +
                         "<br><br>" + Message +
                         "<br><br>" +
                         "Response Given By GCM server is: " + result;

                sendMailOfSMS("gboxcontrol@gmail.com", mailSubject, mailBody, ClientId);
                #endregion
            }
            finally
            {
                gcm = null;
            }
        }
        protected void UpdateGCMId(string AndroidAppId, string Result)
        {
            try
            {
                Result = Result.Substring(Result.IndexOf("registration_id="));
                Result = Result.Substring(Result.IndexOf("registration_id=") + 1);
                gcmbal = new GCMBAL();
                gcmbal.GCMId = Result;
                gcmbal.AppId = AndroidAppId;
                gcmbal.UpdateGCMIDByAppId();//    
            }
            catch (Exception) { }
        }
        public void sendMailOfSMS(string to, string subject, string body, int ClientId)
        {
            try
            {
                mail = new SendMail();
                mail.SendEmail(to, subject, body, ClientId, Constant.infoEmail, "", Constant.developerEmail);
            }
            catch (Exception) { }
            finally
            {
                mail = null;
            }
        }
        #endregion


        #region--------- Encrption ------------------       
        public string encrypt(string encryptionString)
        {
            byte[] clearTextBytes = Encoding.UTF8.GetBytes(encryptionString);

            SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();

            MemoryStream ms = new MemoryStream();
            byte[] rgbIV = Encoding.ASCII.GetBytes("AAAAAAAAAAAAAAAA");
            byte[] key = Encoding.ASCII.GetBytes("0123456789abcdef");
            CryptoStream cs = new CryptoStream(ms, rijn.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write);

            cs.Write(clearTextBytes, 0, clearTextBytes.Length);

            cs.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        #endregion
    }
}
