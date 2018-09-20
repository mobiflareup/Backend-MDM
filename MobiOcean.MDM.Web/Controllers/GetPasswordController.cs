using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Text;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;

namespace MobiOcean.MDM.Web.Controller
{
    public class GetPasswordController : APIBase
    {
        usrBAL usr;
        ChangePasswordBAL cpwd;
        DataTable dt;
        SendMailBAL mail;
        SendSMSBAL sms;
        [ActionName("GetPassword")]
        public string Post([FromBody]usrBAL value)
        {
            try
            {
                usr = new usrBAL();
                mail = new SendMailBAL();
                sms = new SendSMSBAL();
                dt = new DataTable();
                usr.EmailId = value.EmailId;
                dt = usr.GetPassword();
                if (dt.Rows.Count > 0 && dt.Columns.Count > 2)
                {
                    string Token, OTP, EmailId, UserName, MobileNo;
                    int ClientId;
                    Token = GenToken(30,"");
                    OTP = GenToken(6, "012345615789715789");

                    EmailId = dt.Rows[0]["EmailId"].ToString();
                    UserName = dt.Rows[0]["UserName"].ToString();
                    MobileNo = dt.Rows[0]["MobileNo"].ToString();
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    usr = new usrBAL();
                    usr.EmailId = EmailId;
                    usr.Token = Token;
                    usr.OTP = Convert.ToInt32(OTP);
                    usr.currentDateTime = GetCurrentDateTimeByCountry(1);
                    if (usr.InsertIntoForgetPwdKey() > 0)
                    {
                        mail.ForgotPassword(EmailId, Token, UserName, ClientId);
                        sendOTPSMS(MobileNo, OTP, ClientId);
                        return "Reset password link has been sent to your e-mail ID.";
                    }
                    else
                    {
                        return "Please check your e-mail, You have alredy submitted the request to change password.";
                    }                                 
                }
                else
                {

                    return "Please enter the Correct Email ID";

                }
            }
            catch (Exception)
            {
                return "Something went wrong. Please contact our support team.";
            }
            finally
            {
                usr = null;
                mail = null;
                sms = null;
            }
        }       
       
        private void sendOTPSMS(string MobileNo, string OTP, int ClientId)
        {
            sms = new SendSMSBAL();
            sms.sendMsgUsingSMS("The OTP is: " + OTP + " ,To reset password on " + Constant.MobiURL + " .", MobileNo, ClientId);
        }
        [ActionName("ChkOTP")]
        public string Post([FromBody]ForgetPassword value)
        {
            try
            {
                dt = new DataTable();
                dt = value.CheckOTPusingTokenAndEmailId();
                if (dt.Rows.Count > 0)
                {
                    return "Id=" + dt.Rows[0]["Token"].ToString() + "&Email=" + dt.Rows[0]["EmailId"].ToString();
                }
                else
                {
                    return "Invalid credentials.";
                }
            }
            catch (Exception)
            {
                return "Something went wrong";
            }
        }
        [ActionName("UpdatePwd")]
        public string Post([FromBody]UpdatePwd value)
        {
            int res;
            try
            {
                if (!string.IsNullOrEmpty(value.Password))
                {
                    if (value.Password == value.CnfrmPassword)
                    {
                        res = value.UpdateUserPassword();
                        if (res > 0)
                        {
                            return "1";
                        }
                        else
                        {
                            return "Something went wrong.";
                        }
                    }
                    else
                    {
                        return "Password and confirm password doesn't match!";
                    }
                }
                else
                {
                    return "Please enter password!";
                }
            }
            catch (Exception)
            {
                return "Something went wrong";
            }
        }
        [ActionName("ChangePassword")]
        public string Post([FromBody]ChangePasswordBAL value)
        {

            try
            {
                cpwd = new ChangePasswordBAL();
                cpwd.EmailId = value.EmailId;
                int userid = value.GetUserIdByEmailId();
                if (userid != 0)
                {
                    cpwd.Password = value.Password;
                    cpwd.NewPassword = value.NewPassword;
                    value.ChangePassword();
                    //if(res>0)
                    //{
                    return "Password changed successfully";
                    //}
                    //else
                    //{
                    //    return "Old Password does not match";
                    //}
                }
                else
                {
                    return "Something went wrong";
                }
            }
            catch (Exception)
            {
                return "Something went wrong";
            }
        }
    }
}
