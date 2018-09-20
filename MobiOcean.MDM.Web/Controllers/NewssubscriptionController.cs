using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Text.RegularExpressions;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;

namespace MobiOcean.MDM.Web.Controller
{
    public class NewssubscriptionController : APIBase
    {


        SendMailBAL sendmail;
        [ActionName("NewsSub")]
        public string Post([FromBody]NewsSubBAL nsb)
        {
            try
            {
                if (chkvalidations(nsb))
                {
                    if (isValidEmail(nsb.EmailId))
                    {
                        if (nsb.IsSubscription == 0)
                        {

                            nsb.InsertDemoproduct();
                            sendmail = new SendMailBAL();
                            string subject = "Demo Request";
                            string msgbody = "";
                            msgbody = msgbody + "<table><tr colspan='2'><td>" + subject + "</td><tr>";
                            if (!string.IsNullOrEmpty(nsb.Name))
                                msgbody = msgbody + "<tr><td>Name</td><td>" + nsb.Name + "</td></tr>";
                            if (!string.IsNullOrEmpty(nsb.MobileNo))
                                msgbody = msgbody + "<tr><td>Mobile No</td><td>" + nsb.MobileNo + "</td></tr>";
                            msgbody = msgbody + "<tr><td>Email ID:</td><td>" + nsb.EmailId + "</td></tr></table>";
                            sendmail.SendEmail(Constant.salesEmail, subject, msgbody, 1, Constant.developerEmail);
                            sendmail.DemoReqMail(nsb.EmailId);
                            return "Thanks for your interest in MobiOcean solutions.";
                        }
                        else
                        {
                            string sub = "News Subscription Request";
                            nsb.NewsSub();
                            sendmail = new SendMailBAL();
                            string subject = "News Subscription Details";
                            string msgbody = "";
                            msgbody = msgbody + "<table><tr><td>Name</td><td>" + nsb.EmailId + "</td></tr><tr><td>" + sub + "</td><tr></table>";
                            sendmail.SendEmail(Constant.salesEmail, subject, msgbody, 1, Constant.developerEmail);
                            //sendmail.NewsSubscriptionMail(nsb.EmailId);
                            return "Thanks to subscribe!";
                        }
                    }
                    else
                    {
                        return "Invalid E-mail ID";
                    }
                }
                else
                {
                    return "Please fill E-mail ID";
                }
            }
            catch (Exception)
            {
                return "Something went wrong!";
            }
        }
        private bool isValidEmail(string EmailId)
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
        private bool chkvalidations(NewsSubBAL nsb)
        {
            if (nsb.EmailId == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
