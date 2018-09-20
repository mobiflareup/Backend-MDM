using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AfterLogin : LoginBase
    {
        LoginBAL logbal;
        DataTable dt;
        string EmailId = "", No = "", Time = "", Ftr = "", Id = "", IsPayment = "", Payment = "", UserName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Id = Request.QueryString["Id"];
            EmailId = Request.QueryString["EmailId"];
            No = string.IsNullOrEmpty(Request.QueryString["No"].ToString()) ? "" : Request.QueryString["No"].ToString();

            Time = string.IsNullOrEmpty(Request.QueryString["Time"].ToString()) ? "" : Request.QueryString["Time"].ToString();

            Ftr = string.IsNullOrEmpty(Request.QueryString["Ftr"].ToString()) ? "" : Request.QueryString["Ftr"].ToString();

            UserName = string.IsNullOrEmpty(Request.QueryString["UserName"].ToString()) ? "" : Request.QueryString["UserName"].ToString();

            if (string.IsNullOrEmpty(Id))
            {
                Response.Redirect(Constant.MobiURL);
            }
            else
            {
                Login();
                //Login(Id);
            }
        }
        //protected void Login(string UserId)
        //{
        protected void Login()
        {
            dt = new DataTable();
            logbal = new LoginBAL();
            try
            {
                logbal.LoginKey = Id;
                logbal.EmailId = EmailId;
                IsPayment = No + Time + Ftr;
                Payment = "No=" + No + "&Time=" + Time + "&Ftr=" + Ftr + "&UserName=" + UserName;
                dt = logbal.ChkValidation();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    Response.Redirect(Constant.MobiURL);
                }
                else
                {
                    if (dt.Rows[0]["UserFirstLogin"].ToString() == "1")
                    {
                        Response.Write("You are not verified your E-mail Id. Please check your Email.");
                    }
                    else
                    {
                        Session["isLoginByOther"] = Constant.MobiURL;
                        MaintainSession(dt, IsPayment, Payment);
                    }

                }
            }
            catch (Exception)
            {
            }
            finally
            {
                dt = null;
            }
        }
      

    }
}
