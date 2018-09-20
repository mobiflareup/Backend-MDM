using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web.Web
{
    public partial class VerifyMail : LoginBase
    {
        LoginBAL logbal;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            string email = Request.QueryString["email"];
            string verificationCode = Request.QueryString["verificationCode"];
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(verificationCode))
            {
                lblMsg.Text = "Go to your mail and Try again!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Login(email, verificationCode);
            }
        }
        protected void Login(string email, string verificationCode)
        {
            dt = new DataTable();
            logbal = new LoginBAL();
            try
            {
                logbal.EmailId = email;
                logbal.LoginKey = verificationCode;
                dt = logbal.ChkFirstLoginValidation();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    lblMsg.Text = "The link has been Expired. Please contact our support team.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (dt.Rows[0]["UserFirstLogin"].ToString() == "1")
                    {
                        Session["isLoginByOther"] = Constant.MobiURL;
                        UpdateFirstLogin(email);
                        MaintainSession(dt);
                    }
                    else
                    {
                        lblMsg.Text = "Already You have activated your account. Please click on login!";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
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
        private void UpdateFirstLogin(string email)
        {
            logbal = new LoginBAL();
            logbal.EmailId = email;
            logbal.UpdateFirstLogin();
        }
    }
}