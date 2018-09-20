using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class SubscribePeanut : Base
    {
        int ClientId, UserId, RoleId, DeptId;

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
            }
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