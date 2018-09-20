using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class Feedback : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AnuSearch srch;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                txtFrmDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy");
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            grdFeedback.DataSource = srch.SrchFeedbackDetails(txtFrmDate.Text.Trim(), txtToDate.Text.Trim());
            grdFeedback.DataBind();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFeedback.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected string Format(string CreationDate)
        {
            return Convert.ToDateTime(CreationDate).ToString("dd-MMM-yyyy HH:mm");
        }
    }
}