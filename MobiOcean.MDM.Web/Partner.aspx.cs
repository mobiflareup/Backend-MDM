using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class Partner : Base
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
            grdPartner.DataSource = srch.SrchPartnerDetails(txtName.Text.Trim(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim());
            grdPartner.DataBind();
        }
        protected void grdPartner_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPartner.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected string Format(string CreationDate)
        {
            return Convert.ToDateTime(CreationDate).ToString("dd-MMM-yyyy HH:mm");
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}