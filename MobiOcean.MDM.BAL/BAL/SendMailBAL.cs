using HtmlAgilityPack;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiOcean.MDM.BAL.BAL
{
    public class SendMailBAL
    {
        SendMail sendmail;
        string subject = "", mailBody = "";      
       
        public void SupportMail(string EmailId, string UserName, string MsgBody)
        {

            try
            {
                subject = "MobiOcean Support Query";
                mailBody = "<b>Dear " + UserName + ",</b><br><br>";
                mailBody = mailBody + "Thanks for sending your support request to us. We will be happy to assist you.<br>";
                mailBody = mailBody + "We have received your details. Our representative will contact you soon.<br><br>";
                mailBody = mailBody + " " + MsgBody + "<br><br>";
                mailBody = mailBody + "Kindly let us know your convenient time to get in touch with you.<br><br>";
                mailBody = mailBody + "For any further enquiry, please contact us at " + Constant.salesEmail + " or call " + Constant.TollFree + " <br><br>";
                mailBody = mailBody + "<b>Best Regards,</b>";
                mailBody = mailBody + "<br>MobiOcean Team";
                SendEmail(EmailId, subject, mailBody, 1);
            }
            catch (Exception)
            { }
        }
        public void UserRegister(string UserName, int IsFirstLogin, string EmailId, string LoginKey, string ProductKey, string ExpiryDate, int ClientId, int RoleId)
        {
            try
            {
                subject = "MobiOcean Account Activation Notification";
                mailBody = "Dear Admin(" + UserName + "), <br/><br/>";
                if (RoleId == 2 && IsFirstLogin == 1)
                {
                    mailBody = mailBody + "Greetings from " + Constant.CompanyShortName + "!<br/><br/> Thank you for registering with MobiOcean's Mobility-Managed-Regulated-Secured solutions.<br/><br/> Please click here to activate your account : <a id=\"addToTable\" href=\"" + Constant.URL + "Web/Verifymail.aspx?email=" + EmailId + "&verificationCode=" + LoginKey + "\">Click here to Validate</a> <br/> <br/> <br/>Your account activation link is valid for 3 days. Please activate your account before its expiry. <br/><br/>Your free trial will expire on " + ExpiryDate + ".<br/> <br/>For subscription click here " + Constant.MobiURL + "/cloud-managed . <br/><br/>For any query contact us at " + Constant.enquiryEmail + " or call " + Constant.TollFree + ". <br/><br/><br/>This is an auto-generated e-mail, please do not reply to this message!<br/> <br/>   Best Regards, <br/> MobiOcean Team";//Your free trial product key is: " + ProductKey + " This key will expire on " + ExpiryDate + ".
                }
                else
                {
                    mailBody = mailBody + "Greetings from " + Constant.CompanyShortName + "!<br/><br/> Thank you for registering with MobiOcean's Mobility-Managed-Regulated-Secured solutions.<br/><br/> Please click here to activate your account : <a id=\"addToTable\" href=\"" + Constant.URL + "Web/Verifymail.aspx?email=" + EmailId + "&verificationCode=" + LoginKey + "\">Click here to Validate</a> <br/> <br/> <br/>Your account activation link is valid for 3 days. Please activate your account before its expiry. <br/><br/>For any query contact us at " + Constant.enquiryEmail + " or call " + Constant.TollFree + " . <br/><br/><br/>This is an auto-generated e-mail, please do not reply to this message!<br/> <br/>   Best Regards, <br/> MobiOcean Team";

                }
                SendEmail(EmailId, subject, mailBody, ClientId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            { }
        }
        public void ExpiryReminder(string emailId, int remainingExpiryDay, int clientId)
        {
            subject = "";
            mailBody = "";
            try
            {
                subject = "Notice: MobiOcean Expiry Reminder";
                mailBody = "Dear Admin, <br/><br/>";
                if (remainingExpiryDay != 0)
                {
                    mailBody = mailBody + "Greetings from " + Constant.CompanyShortName + "!<br/><br/> Your MobiOcean subscription is expiring in " + remainingExpiryDay + " Days. To continue our services, Please  <a href=\"" + Constant.MobiURL + "/cloud-managed\" target=\"_blank\">Subscribe</a> <br/><br/><br/>For any query contact us at " + Constant.enquiryEmail + " or call " + Constant.TollFree + " . <br/><br/><br/>This is an auto-generated e-mail, please do not reply to this message!<br/> <br/>   Best Regards, <br/> MobiOcean Team";
                }
                else
                {
                    mailBody = mailBody + "Greetings from " + Constant.CompanyShortName + "!<br/><br/> Your MobiOcean subscription is expiring today. To continue our service, Please  <a href=\"" + Constant.MobiURL + "/cloud-managed\" target=\"_blank\">Subscribe</a> <br/><br/><br/>For any query contact us at " + Constant.enquiryEmail + " or call " + Constant.TollFree + " . <br/><br/><br/>This is an auto-generated e-mail, please do not reply to this message!<br/> <br/>   Best Regards, <br/> MobiOcean Team";
                }
                SendEmail(emailId, subject, mailBody, clientId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            { }
        }
        public void ForgotPassword(string EmailId, string Password, string UserName, int ClientId)
        {
            try
            {
                subject = "MobiOcean Password Recovery Email";
                mailBody = "<b>Dear " + UserName + " ,</b><br><br>";
                mailBody = mailBody + "As per your request, we have resetted your password. Click the below link to reset your password: <br>";
                mailBody = mailBody + "</br></br>";
                mailBody = mailBody + " <a href=\"" + Constant.MobiURL + "otpverification.php?Id=" + Password + "&Email=" + EmailId + "\" target=\"_blank\">Click here to reset your password</a> <br><br>";
                //  mailBody = mailBody + "<b>Url :</b> <a href='www.Nimboli.in'>www.Nimboli.in</a><br><br>";
                mailBody = mailBody + "<b>Thanks and Regards</b>";
                mailBody = mailBody + "<br>MobiOcean Team";
                SendEmail(EmailId, subject, mailBody, ClientId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {               
            }
        }
        public void PartnerMail(string EmailId, string UserName)
        {
            try
            {
                subject = "MobiOcean Partner Enquiry";
                mailBody = "<b>Dear " + UserName + ",</b><br><br>";
                mailBody = mailBody + "Thanks for your interest in MobiOcean solutions. We shall contact you shortly.<br>";
                // mailBody = mailBody + "We have received your details. Our representative will contact you soon to complete your requirement.<br><br>";
                mailBody = mailBody + "For any further enquiry, please contact us at " + Constant.salesEmail + " or call " + Constant.TollFree + " <br><br>";
                mailBody = mailBody + "Looking  forward to working with you.<br><br>";
                mailBody = mailBody + "<b>Best Regards,</b>";
                mailBody = mailBody + "<br>MobiOcean Team";
                SendEmail(EmailId, subject, mailBody, 1);
            }
            catch (Exception)
            {

            }

        }
        public void SendEmail(string EmailTo, string subject, string msgBody, int College_Id, string cc = "", string bcc = "")
        {
            sendmail = new SendMail();
            sendmail.SendEmail(EmailTo, subject, msgBody, College_Id, Constant.infoEmail, cc, bcc);
        }
        public void DemoReqMail(string EmailId)
        {
            try
            {
                subject = "MobiOcean Demo Request";
                mailBody = "<b>Dear Sir/Madam,</b><br><br>";
                mailBody = mailBody + "Thanks for your interest in MobiOcean solutions.  We hope it is a potential fit for your company.<br>";
                mailBody = mailBody + "We have received your details. Our representative will contact you soon.<br><br>";
                mailBody = mailBody + "For any further enquiry, please contact us at " + Constant.salesEmail + " or call " + Constant.TollFree + " <br><br>";
                mailBody = mailBody + "Looking  forward to assist you.<br><br>";
                mailBody = mailBody + "<b>Best Regards,</b>";
                mailBody = mailBody + "<br>MobiOcean Team";
                SendEmail(EmailId, subject, mailBody, 1);
            }
            catch (Exception)
            {

            }
        }
        public void NewMobiOrderMail(string EmailTo, string msgBody, string UserName, byte[] invoice)
        {
            try
            {
                subject = "MobiOcean Order Confirmation";
                mailBody = "<b>Dear " + UserName + ",</b><br><br>";
                mailBody = mailBody + "Thanks to subscribe MobiOcean solutions! <br>";

                mailBody = mailBody + "<b>Your Order Detail</b>" + msgBody + "<br><br>";

                mailBody = mailBody + "For any further enquiry, please contact us at " + Constant.salesEmail + " or call " + Constant.TollFree + " <br><br>";
                mailBody = mailBody + "Looking  forward to working with you.<br><br>";
                mailBody = mailBody + "<b>Best Regards,</b>";
                mailBody = mailBody + "<br>MobiOcean Team";

                sendmail = new SendMail();
                sendmail.SendEmailWithAttachment(EmailTo, subject, mailBody, 208, invoice, Constant.infoEmail, "", Constant.salesEmail + "," + Constant.accountEmail);
            }
            catch (Exception)
            {

            }
        }
        public void UserUpgradation(string UserName, string Password, string EmailId, string ByUser, string LoginKey, int ClientId)
        {
            try
            {
                subject = "MobiOcean Account Upgradation Notification";
                mailBody = "Dear " + UserName + ", <br/><br/>";
                mailBody = mailBody + "Greeting from " + Constant.CompanyShortName + "!<br/><br/> Your role for MobiOcean account has upgraded by " + ByUser + ".<br/><br/> To start, please take a few seconds to verify your email address by click on the following link : <a id=\"addToTable\" href=\"" + Constant.URL + "Web/Verifymail.aspx?email=" + EmailId + "&verificationCode=" + LoginKey + "\">Click here to Validate</a> <br/> <br/> <br/>Your account activation link is valid for 3 days. Please activate your account before its expiry. <br/><br/>Your password is : " + Password + " <br/><br/><br/>For any query contact us at " + Constant.enquiryEmail + " or call " + Constant.TollFree + " . <br/><br/><br/>This is an auto-generated e-mail, please do not reply to this message!<br/> <br/>   Best Regards, <br/> MobiOcean Team";

                SendEmail(EmailId, subject, mailBody, ClientId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            { }
        }
        public void ContactSalesMail(string EmailId, string UserName)
        {
            try
            {
                subject = "MobiOcean Product Enquiry";
                mailBody = "<b>Dear " + UserName + ",</b><br><br>";
                mailBody = mailBody + "Thanks for your interest in MobiOcean solutions.  We hope it is a potential fit for your company.<br>";
                mailBody = mailBody + "We have received your details. Our representative will contact you soon.<br><br>";
                mailBody = mailBody + "For any further enquiry, please contact us at " + Constant.salesEmail + " or call " + Constant.TollFree + " <br><br>";
                // mailBody = mailBody + "Looking  forward to working with you.<br><br>";
                mailBody = mailBody + "<b>Best Regards,</b>";
                mailBody = mailBody + "<br>MobiOcean Team";
                SendEmail(EmailId, subject, mailBody, 1);
            }
            catch (Exception)
            {

            }
        }
    }
}
