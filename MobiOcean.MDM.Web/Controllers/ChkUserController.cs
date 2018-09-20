using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.BAL;

namespace MobiOcean.MDM.Web.Controller
{
    public class ChkUserController : APIBase
    {
        UserBAL userBAL;
        SendSMSBAL sms;

        [ActionName("Registration")]
        public string Post([FromBody]UserBAL usr)
        {
            try
            {
                if (usr.ClientCode != null && usr.ClientCode.ToLower().Equals("genus"))
                {
                    GenusAuth genusauth = new GenusAuth();
                    genusauth.UserCode = usr.EmpCompanyId;
                    genusauth.MobileNo = usr.MobileNo;
                    return genusauth.Registration();
                }
                string appId = usr.ChkUser();
                if (appId == "1" || appId == "2")
                    GenerateAndSendOTP(usr.MobileNo);
                return appId;
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [ActionName("OTP")]
        public string Post([FromBody]OTPClass usr)
        {
            try
            {
                if (usr.ClientCode != null && usr.ClientCode.ToLower().Equals("genus"))
                {
                    GenusAuth genusauth = new GenusAuth();
                    genusauth.otp = usr.OTP;
                    genusauth.MobileNo = usr.MobileNo;
                    return genusauth.ChkOTP();
                }
                return usr.ChkIsStuResgistered();
            }
            catch (Exception)
            {
                return "0";
            }
        }
        [HttpPost]
        public OTPResponse VerifyOTP([FromBody]OTPClass usr)
        {
            OTPResponse otpResponse = new OTPResponse();
            try
            {
                if (usr.ClientCode != null && usr.ClientCode.ToLower().Equals("genus"))
                {
                    GenusAuth genusauth = new GenusAuth();
                    genusauth.otp = usr.OTP;
                    genusauth.MobileNo = usr.MobileNo;
                    //return genusauth.ChkOTP();
                    otpResponse.appID = "0";
                    otpResponse.EngnrID = "0";
                    return otpResponse;
                }
                return usr.OTPVerification();
            }
            catch (Exception)
            {
                otpResponse.appID = "0";
                otpResponse.EngnrID = "0";
                return otpResponse;
            }
        }
        private void GenerateAndSendOTP(string mobileNo)
        {
            string OTP = GenToken(Constant.OTPLength, "012345635786128617789");
            userBAL = new UserBAL();
            string clienTId = userBAL.UpdateOTP(OTP, mobileNo).ToString();
            sendOTPSMS(mobileNo, OTP.ToString(), clienTId);
        }
        private void sendOTPSMS(string MobileNo, string OTP, string ClientId)
        {
            sms = new SendSMSBAL();
            sms.sendMsgUsingSMS("OTP for MObiocean Registration is " + OTP + " . Please use this OTP to complete the registration.", MobileNo, Convert.ToInt32(ClientId));
            //sms.sendMsgUsingSMS("GBox Set as WP7", "8939027654", 208);
        }
    }
}
