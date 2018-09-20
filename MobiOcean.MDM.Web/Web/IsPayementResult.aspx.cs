using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web.Web
{
    public partial class IsPayementResult : System.Web.UI.Page
    {
        int ClientId, UserId, RoleId, DeptId;
        DataTable dt;
        UserBAL user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["ClientId"] == null || Session["UserId"] == null || Session["Role"] == null)
            {
                // Response.Redirect("~/MyError.aspx?MyMsg=" + "Base class Session end " + Session.SessionID + "  Hi  " + Session.ToString());
                Response.Redirect(Constant.MobiURL);
                // Response.Redirect("~/login.aspx");
            }
            else
            {
                ClientId = Convert.ToInt32(Session["ClientId"].ToString());
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                RoleId = Convert.ToInt32(Session["Role"].ToString());
                DeptId = Convert.ToInt32(Session["DeptId"].ToString());
                user = new UserBAL();
                dt = new DataTable();
                user.UserId = UserId;
                dt = user.GetUserDtlByUserId();
                if (!IsPostBack)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lblUserName.Text = dt.Rows[0]["UserName"].ToString();
                    }
                    try
                    {
                        if (Session["result"].ToString() == "Success")
                        {
                            pnlsuccess.Visible = true;
                            pnlfailure.Visible = false;
                            //System.Threading.Thread.SpinWait(5000);
                            Session["result"] = null;
                            Session["CloudPrice"] = null;
                            //Response.Redirect("admindashboard.aspx");
                        }
                        else
                        {
                            pnlsuccess.Visible = false;
                            pnlfailure.Visible = true;
                            //System.Threading.Thread.SpinWait(5000);
                            Session["result"] = null;
                            //Response.Redirect("Paymentraj.aspx");
                        }
                    }
                    catch
                    {
                        Response.Redirect(Constant.MobiURL + "cloud-managed");
                    }
                }
            }
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("" + Constant.MobiURL + "/login.php");
        }

        protected void btnsuccess_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Subscribe.aspx");
        }

        protected void btnfailure_Click(object sender, EventArgs e)
        {
            Response.Redirect(Constant.MobiURL + "cloud-managed");
        }
    }
}