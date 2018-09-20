using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class NewsSubscription : Base
    {
        int ClientId, UserId, RoleId;
        AnuSearch srch;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            if (RoleId == 1)
            {
                BindGrid();
            }
            else if (RoleId == 2)
            {
                Response.Redirect("AdminDashboard.aspx");
            }
            else
            {
                Response.Redirect("AdminDashboard.aspx");
            }
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy");
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            grdNewssub.DataSource = srch.GetSubscriptionDetails(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
            grdNewssub.DataBind();
        }
        protected void grdNewssub_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdNewssub.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected string Format(string CreationDate)
        {
            return Convert.ToDateTime(CreationDate).ToString("dd-MMM-yyyy HH:mm");
        }
    }
}