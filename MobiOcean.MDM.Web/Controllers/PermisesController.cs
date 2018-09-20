using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;

namespace MobiOcean.MDM.Web.Controller
{
    public class PermisesController : APIBase
    {
        PermisesBAL permise;
        string dtlst;
        SendMailBAL sendmail;

        [ActionName("Country")]
        public string Get()
        {
            permise = new PermisesBAL();
            return permise.GetCountry();
        }

        [ActionName("InsertOnPermiseDetails")]
        public int Post([FromBody]PermisesBAL value)
        {
            return InsertOnPermise(value.name, value.mobileno, value.emailid, value.companyname, value.industry, value.noofemployees, value.period, value.categorylist);
        }
        private int InsertOnPermise(string name, string mobileno, string emailid, string companyname, string industry, string noofemployees, string period, List<categorylist> categorylist)
        {
            string catname = "";
            try
            {
                permise = new PermisesBAL();
                permise.name = name;
                permise.mobileno = mobileno;
                permise.emailid = emailid;
                permise.companyname = companyname;
                permise.industry = industry;
                permise.noofemployees = noofemployees;
                permise.period = period;
                foreach (var obj in categorylist)
                {
                    catname = obj.CategoryName + "," + catname;
                    //catname = catname + ", " + obj.CategoryName;
                }
                dtlst = catname.TrimEnd(',');
                permise.dtlist = dtlst;
                int res = permise.InsertOnPermises();
                if (res > 0)
                {
                    sendmail = new SendMailBAL();
                    string subject = "On-Premises Request";
                    string msgbody = "";
                    msgbody = msgbody + "<table><tr><td>Name</td><td>" + permise.name + "</td></tr><tr><td>Email Id</td><td>" + permise.emailid + "</td></tr><tr><td>Mobile No</td><td>" + permise.mobileno + "</td></tr><tr><td>Company Name</td><td>" + permise.companyname + "</td></tr><tr><td>Type Of Industry</td><td>" + permise.industry + "</td></tr><tr><td>No Of Employees</td><td>" + permise.noofemployees + "</td></tr><tr><td>Period</td><td>" + permise.period + "</td></tr><tr><td>Category</td><td>" + dtlst + "</td></tr></table>";
                    sendmail.SendEmail(Constant.salesEmail, subject, msgbody, 1, Constant.developerEmail);
                    sendmail.ContactSalesMail(permise.emailid, permise.name);
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

    }
}
