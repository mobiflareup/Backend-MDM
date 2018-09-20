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
    public partial class ApplicationMarketList : Base
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
                dt = srch.SrchApplicationMarketList(txtSrchAppName.Text, txtSrchPackage.Text, txtSrchDeveloper.Text, ClientId);
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
            apbal.AppMarketId = Convert.ToInt32(lblfinalAppMarketId.Text);

            if (apbal.DeleteAppMarke() == 1)
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
            //lblfinalAppMarketId.Text = "Test";// ((Label)gvr.FindControl("lblAppMarketId")).Text.Trim();
            //mpdelete.Show();
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdapplst.Rows[gvr.RowIndex].FindControl("lblAppMarketId"));
            //Label lblname = ((Label)grdUser.Rows[gvr.RowIndex].FindControl("lblUserName"));
            //lblGrpId.Text = lblid.Text;
            lblfinalAppMarketId.Text = lblid.Text;
            mpdelete.Show();
        }

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdapplst.Rows[gvr.RowIndex].FindControl("lblAppMarketId"));
            Response.Redirect("ApplicationUpload.aspx?Id=" + lblid.Text, false);
        }

        protected void lnkbtnDvMap_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdapplst.Rows[gvr.RowIndex].FindControl("lblAppMarketId"));
            lblAppMarketIdMapping.Text = lblid.Text;
            BindDeviceTypeDropdown();
            mpManage.Show();
        }
        private void BindDeviceTypeDropdown()
        {
            apbal = new AppBAL();
            try
            {
                ListItem li1 = new ListItem("All Device", "0");
                dtDeviceType.Items.Clear();
                dtDeviceType.Items.Add(li1);
                dtDeviceType.DataSource = apbal.GetDeviceType();
                dtDeviceType.DataTextField = "DeviceType";
                dtDeviceType.DataValueField = "DeviceTypeId";
                dtDeviceType.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                apbal = null;
            }
        }

        protected void btnDeviceMapping_Click(object sender, EventArgs e)
        {
            apbal = new AppBAL();
            apbal.AppMarketId = Convert.ToInt32(lblAppMarketIdMapping.Text);
            apbal.ApplyDeviceType = Convert.ToInt32(dtDeviceType.SelectedValue.ToString());
            apbal.AppMarketDeviceMapping();
            if (apbal.InsertOTAPackage() > 0)
            {
                lblMsg.Text = "Successfully saved";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            else
            {
                lblMsg.Text = "Something went wrong";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void lblAssignDevice_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdapplst.Rows[gvr.RowIndex].FindControl("lblAppMarketId"));
            Response.Redirect("AssignAppMarket.aspx?Id=" + lblid.Text, false);
        }

        protected void lnkbtnInfo_Click(object sender, EventArgs e)
        {

        }

        protected void addApk_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationUpload.aspx");
        }

        protected void lblInstall_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdapplst.Rows[gvr.RowIndex].FindControl("lblAppMarketId"));
            Response.Redirect("AppTerminal.aspx?Id=" + lblid.Text + "&PageId=1&PageClickId=1", false);
        }

        protected void lblUgrade_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdapplst.Rows[gvr.RowIndex].FindControl("lblAppMarketId"));
            Response.Redirect("AppTerminal.aspx?Id=" + lblid.Text + "&PageId=1&PageClickId=2", false);
        }

        protected void lblUnInstall_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdapplst.Rows[gvr.RowIndex].FindControl("lblAppMarketId"));
            Response.Redirect("AppTerminal.aspx?Id=" + lblid.Text + "&PageId=1&PageClickId=3", false);
        }
    }
}