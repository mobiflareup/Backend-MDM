using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.RegularExpressions;
using MobiOcean.MDM.BAL.BAL;

namespace MobiOcean.MDM.Web.Controller
{

    public class FeedbackController : ApiController
    {
        [ActionName("PostFeedback")]
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
                            Feedback.IU_Feedback();
                            return "Submitted Successfully.";
                        }
                        else
                        {
                            return "Invalid MobileNo";
                        }
                    }
                    else
                    {
                        return "Invalid EmailId";
                    }
                }
                else
                {
                    return "Please fill mandatory fields";
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
            if (Feedback.Name == "" || Feedback.EmailId == "" || Feedback.MobileNo == "" || Feedback.Feedback == "" || Feedback.CompanyName == "")
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
