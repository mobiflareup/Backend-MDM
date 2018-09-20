using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MobiOcean.MDM.BAL.Model;
using System.Text.RegularExpressions;
using MobiOcean.MDM.BAL.BAL;

namespace MobiOcean.MDM.Web.MobiOcean
{
    public partial class MobiSupport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            SupportBAL value = new SupportBAL();
            value.UserName = Request.Form["supportname"];
            value.CompanyName = Request.Form["supportcompany"];
            value.MobileNo = Request.Form["supportmobile"];
            value.EmailId = Request.Form["supportmail"];
            value.DefectName = Request.Form["supportproblem"];
            value.DefectDesc = Request.Form["discription"];
            string FilePath = "";
            if (errorlog.HasFile)
            {
                string FileName = Path.GetFileName(errorlog.PostedFile.FileName);
                //string Extension = Path.GetExtension(errorlog.PostedFile.FileName);
                FilePath = Server.MapPath(Constant.FolderPath + FileName);
                errorlog.SaveAs(FilePath);
                value.DocPath = FilePath;
                if ((!string.IsNullOrEmpty(value.EmailId)) && isValidEmail(value.EmailId) && (!string.IsNullOrEmpty(value.MobileNo)))
                {
                    try
                    {
                        int res = value.Insert_Support();
                        if (res > 0)
                        {
                            Response.Write("<script>alert('Your request submitted successfully. Our executive will contact you shortly.')</script>");//return "Your request submitted successfully. Our executive will contact you shortly.";
                            Response.Redirect("ThankYou.aspx");
                        }
                        else
                            Response.Write("<script>alert('Request not submitted. Pl write to us on " + Constant.supportEmail + "!')</script>");// return "Request not submitted. Pl write to us on " + Constant.supportEmail + "!";
                    }
                    catch (Exception)
                    {
                        Response.Write("<script>alert('Internal Server Error')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please enter valid E-mail ID/ Mobile No.')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('File Not Exists')</script>");
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
}