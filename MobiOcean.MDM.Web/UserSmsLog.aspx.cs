using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class UserSmsLog : Base
    {
        int ClientId, UserId, RoleId, DeptId;

        AnuSearch srch;



        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            txtFrmDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {

                txtFrmDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy");
                BindGrid();

            }
        }
        protected void BindGrid()
        {
            DataTable dtUser = new DataTable();

            try
            {

                srch = new AnuSearch();

                dtUser = srch.BackupSmsReport(UserId, txtSrchNo.Text.Trim(), ddlDirection.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim());

                grdmsgh.DataSource = dtUser;
                grdmsgh.DataBind();
            }
            catch (Exception)
            {
            }
        }

        protected void grdmsgh_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdmsgh.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            grdmsgh.PageIndex = 0;
            BindGrid();

        }
        protected void grdmsgh_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected string MyFormat(string from, string to, string IsIncoming)
        {
            if (IsIncoming == "1")
            {
                return from;
            }
            else
            {
                return to;
            }
        }
    }
}