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
    public partial class Subscribe : System.Web.UI.Page
    {
        int ClientId, UserId, RoleId, DeptId;
        
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["ClientId"] == null || Session["UserId"] == null || Session["Role"] == null)
            {
                Response.Redirect(Constant.MobiURL);
                //Response.Redirect("~/login.aspx");
            }
            else
            {
                ClientId = Convert.ToInt32(Session["ClientId"].ToString());
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                RoleId = Convert.ToInt32(Session["Role"].ToString());
                DeptId = Convert.ToInt32(Session["DeptId"].ToString());
                lblMsg.Text = string.Empty;
                if (!IsPostBack)
                {

                }
                ShowMsg();
            }
        }
        protected void ShowMsg()
        {
            ClientBAL cbal = new ClientBAL();
            dt = new DataTable();
            cbal.ClientId = ClientId;
            dt = cbal.GetClientByClientId();
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (dt.Rows[0]["IsFirstLogin"].ToString() == "1")
                    {
                        lblverify.Text = "Your Account Has Been Verified Successfully";
                        lblverify.ForeColor = System.Drawing.Color.Green;
                        lblMsg.Text = "Please Enter Your Licence Key To Manage Your Employee!";
                    }
                    else
                    {
                        lblverify.Text = "";
                        if (!string.IsNullOrEmpty(dt.Rows[0]["ExpiryDate"].ToString()) && Convert.ToDateTime(dt.Rows[0]["ExpiryDate"].ToString()) > DateTime.UtcNow.AddMinutes(Constant.addMinutes))
                        {
                            int count = (Convert.ToDateTime(dt.Rows[0]["ExpiryDate"].ToString()).Date - DateTime.UtcNow.AddMinutes(Constant.addMinutes).Date).Days;
                            lblMsg.Text = "Your license will be expired in " + count + " Days.";
                        }
                        else
                        {
                            lblMsg.Text = "Your license key has been expired. To continue our service please subscribe.";
                        }

                    }
                }
                catch (Exception)
                {
                    Response.Redirect(Constant.MobiURL);
                }
            }
            else
            {
                Response.Redirect(Constant.MobiURL);
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(freetrail.Text))
            //{
            //    MpSubscribe.Show();
            //    message.Text = "Enter the product key!";
            //    message.ForeColor = System.Drawing.Color.Red;
            //    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Enter the product key!');", true);
            //}
            //else
            //{
            //    Cbal = new ClientBAL();
            //    int res = Cbal.ChkProductKeybysub3(ClientId, freetrail.Text);
            //    if (res == 1)
            //    {
            //        mpsuccess.Show();
            //        lblSuccess.Text = "Product key verified!";
            //        lblSuccess.ForeColor = System.Drawing.Color.Green;
            //        //ClientScript.RegisterStartupScript(GetType(), "Alert", "<SCRIPT LANGUAGE='javascript'>Alert('Product key verified!');</script>");
            //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Product key verified!');window.location.assign(\"AdminDashboard.aspx\")", true);
            //        //Response.Redirect("~/AdminDashboard.aspx");

            //    }
            //    else if (res == 2)
            //    {
            //        freetrail.Text = string.Empty;
            //        mpsuccess.Show();
            //        lblSuccess.Text = "Your product key is already verified!";
            //        lblSuccess.ForeColor = System.Drawing.Color.Red;
            //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Your product key is already verified!');", true);
            //    }
            //    else
            //    {
            //        freetrail.Text = string.Empty;
            //        MpSubscribe.Show();
            //        message.Text = "Product key is not correct!";
            //        message.ForeColor = System.Drawing.Color.Red;
            //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "err_msg", "alert('Product key is not correct!');", true);
            //        // Page.ClientScript.RegisterStartupScript(GetType(), "Alert", "<SCRIPT LANGUAGE='javascript'>Alert('Product key is not correct!');</script>");
            //    }
            //}
        }
        protected void ok_Click(object sender, EventArgs e)
        {
            MpSubscribe.Hide();
        }
        protected void btnsucok_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDashboard.aspx");
        }
    }
}
