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
    public class SupportController : APIBase
    {


        // POST api/<controller>
        [ActionName("Insert")]
        public string Post([FromBody]SupportBAL value)
        {
            if ((!string.IsNullOrEmpty(value.EmailId)) && isValidEmail(value.EmailId) && (!string.IsNullOrEmpty(value.MobileNo)))
            {
                try
                {
                    int res = value.Insert_Support();
                    if (res > 0)
                        return "Your request submitted successfully. Our executive will contact you shortly.";
                    else
                        return "Request not submitted. Pl write to us on " + Constant.supportEmail + "!";
                }
                catch (Exception)
                {
                    return "Something went wrong!";
                }
            }
            else
            {
                return "Please enter valid E-mail ID/ Mobile No.";
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
    }
    public class AppSupportController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;
        UserBAL userBal;
        [ActionName("Insert")]
        public string Post([FromBody]SupportBAL value)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(value.appId);
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
                dt = new DataTable();
                userBal = new UserBAL();
                value.UserId = UserId;
                userBal.UserId = UserId;
                dt = userBal.GetUserDtlByUserId();
                value.UserName = dt.Rows[0]["UserName"].ToString();
                value.MobileNo = dt.Rows[0]["MobileNo"].ToString();
                value.EmailId = dt.Rows[0]["EmailId"].ToString();
                value.ClientId = ClientId;
                try
                {
                    int res = Convert.ToInt32(value.Insert_Support());
                    if (res > 0)
                        return "Your request submitted successfully. Our executive will contact you shortly.";
                    else
                        return "Request not submitted. Pl write to us on " + Constant.supportEmail + "!";                  
                }
                catch (Exception)
                {
                    return "Something went wrong!";
                }
            }
            else
            {
                return "Please enter valid E-mail ID/ Mobile No.";
            }
        }
    }
}
