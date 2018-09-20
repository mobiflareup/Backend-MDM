using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.RegularExpressions;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;

namespace MobiOcean.MDM.Web.Controller
{
    public class PartnerController : ApiController
    {
        SendMailBAL sendmail;
        [ActionName("InsertPartnerDetails")]
        public string Post([FromBody]FeedbackBAL Feedback)
        {
            try
            {
                if (chkvalidations(Feedback))
                {
                    if (isvalidEmail(Feedback.EmailId))
                    {
                        if (isvalidMobileNo(Feedback.MobileNo))
                        {
                            Feedback.InsertPartnerDetails();
                            string type = "";
                            sendmail = new SendMailBAL();
                            string Subject = "Partner Request";
                            string msgBody = "";
                            if (Feedback.Type_Id == 1)
                            {
                                type = "Partner";
                            }
                            else if (Feedback.Type_Id == 2)
                            {
                                type = "Affiliate";
                            }
                            else
                            {
                                type = "Contact Sales";
                            }
                            msgBody = msgBody + "<table><tr><td>Name</td><td>" + Feedback.Name + "</td></tr><tr><td>Company Name</td><td>" + Feedback.CompanyName + "</td></tr><tr><td>Email Id</td><td>" + Feedback.EmailId + "</td></tr><tr><td>Mobile No</td><td>" + Feedback.MobileNo + "</td></tr><tr><td>Type Of Industry</td><td>" + Feedback.Industry + "</td></tr><tr><td>Type</td><td>" + type + "</td></tr></table>";
                            sendmail.SendEmail(Constant.salesEmail, Subject, msgBody, 1, Constant.developerEmail);
                            sendmail.PartnerMail(Feedback.EmailId, Feedback.Name);
                            return "Your request submitted successfully. We will contact you shortly.";
                        }
                        else
                        {
                            return "Invalid Mobile No";
                        }
                    }
                    else
                    {
                        return "Invalid E-mail ID";
                    }
                }
                else
                {
                    return "Please fill mandatory fields!";
                }
            }
            catch (Exception)
            {
                return "Something went wrong!";
            }
        }
        public bool isvalidEmail(string EmailId)
        {
            string stremail = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                   @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                   @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(stremail);
            if (re.IsMatch(EmailId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isvalidMobileNo(string MobileNo)
        {
            string regexmobileno = @"^\d{10}$";
            Regex re = new Regex(regexmobileno);
            if (re.IsMatch(MobileNo))
                return (true);
            else
                return (false);
        }
        protected bool chkvalidations(FeedbackBAL Feedback)
        {
            if (Feedback.Name == "" || Feedback.EmailId == "" || Feedback.MobileNo == "" || Feedback.Industry == "" || Feedback.CompanyName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        [ActionName("InsertSubscriptionDetails")]
        public string Post([FromBody]PaymentResponse pay)
        {
            return pay.OfflinePaymentHandle();
        }


    }
}
