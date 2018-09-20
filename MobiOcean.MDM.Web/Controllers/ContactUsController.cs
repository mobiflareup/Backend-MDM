using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Text;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;

namespace MobiOcean.MDM.Web.Controller
{
    public class ContactUsController : APIBase
    {


        SendMailBAL sendmail;
        [ActionName("InsertIntoContactUs")]
        public string Post([FromBody]ContactBAL value)
        {
            try
            {

                string res = value.InsertContactUs().ToString();
                if (res == "1")
                {
                    sendmail = new SendMailBAL();
                    string Subject = "Contact Details";
                    string msgBody = "";
                    msgBody = msgBody + "<table><tr><td>Name</td><td>" + value.Name + "</td></tr><tr><td>Email Id</td><td>" + value.EmailId + "</td></tr><tr><td>Mobile No</td><td>" + value.MobileNo + "</td></tr><tr><td>Company Name</td><td>" + value.Company_Name + "</td></tr><tr><td>Type Of Industry</td><td>" + value.TypeOfIndustry + "</td></tr><tr><td>Country</td><td>" + value.Country + "</td></tr><tr><td>Remark</td><td>" + value.Remark + "</td></tr></table>";
                    try
                    {
                        sendmail.SendEmail(Constant.salesEmail, Subject, msgBody, 1);
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        sendmail.ContactSalesMail(value.EmailId, value.Name);
                    }
                    catch (Exception) { }
                    return "Your request submitted succesfully. Our executive will contact you shortly.";
                }
                else
                {
                    return "Sorry, Your request not submitted!";
                }

            }
            catch (Exception)
            {
                return "Something went wrong!";
            }
        }
    }
}
