using MobiOcean.MDM.BAL.BAL;
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
    public partial class ApplicationManageOTG : Base
    {
        int ClientId, UserId, RoleId;
        GingerboxSrch srch;
        DataTable dt;
        AppBAL apbal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        public void BindGrid()
        {

            dt = new DataTable();

            try
            {
                srch = new GingerboxSrch();
                dt = srch.SrchOSList(txtSrchAppPackage.Text, txtSrchAppVersion.Text, ClientId);
                grdapplst.DataSource = dt;
                grdapplst.DataBind();
            }
            catch (Exception)
            {
            }
        }
        protected void grdapplst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdapplst.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void grdapplst_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            apbal = new AppBAL();
            apbal.OsId = Convert.ToInt32(lblfinalAppMarketId.Text);

            if (apbal.DeleteOTAApp() == 1)
            {

                lblMsg.Text = "Deleted successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Could not delete";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            BindGrid();

            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblfinalAppMarketId.Text = "";
        }

        protected void grdapplst_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdapplst.Rows[gvr.RowIndex].FindControl("lblOsId"));
            lblfinalAppMarketId.Text = lblid.Text;
            mpdelete.Show();
        }

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdapplst.Rows[gvr.RowIndex].FindControl("lblOsId"));
            Response.Redirect("AddOTAPackage.aspx?Id=" + lblid.Text, false);
        }

        protected void lnkbtnUpgrade_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdapplst.Rows[gvr.RowIndex].FindControl("lblOsId"));
            Response.Redirect("AppTerminal.aspx?Id=" + lblid.Text + "&PageId=2", false);
        }

        protected void addos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddOTAPackage.aspx", false);
        }
    }
}