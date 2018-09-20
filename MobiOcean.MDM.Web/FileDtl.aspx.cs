using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class FileDtl : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AnuSearch srch;
        DDLBAL ddlbal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            txtFrmDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                BindUsrName();
                txtFrmDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy");
                BindGrid();
            }
        }
        protected void BindUsrName()
        {
            try
            {
                ddlbal = new DDLBAL();
                ddlbal.UserId = UserId;
                ddlbal.ClientId = ClientId;
                ddlbal.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(new ListItem("--- All ---", "0"));
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = ddlbal.GetUserDeviceByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = ddlbal.GetUsrDeviceByDeptHead();
                }
                else
                {
                    ddlUserName.DataSource = ddlbal.GetUserDeviceByUserId();
                }
                ddlUserName.DataTextField = "DeviceName";
                ddlUserName.DataValueField = "DeviceId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                ddlbal = null;
            }
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            if (RoleId == 1 || RoleId == 2)
            {
                grdFileDtl.DataSource = srch.SrchFileDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), 1);
            }
            else if (RoleId == 3)
            {
                grdFileDtl.DataSource = srch.SrchFileDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), 1, DeptId);
            }
            else
            {
                grdFileDtl.DataSource = srch.SrchFileDtls(ClientId, UserId, ddlUserName.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), 1);
            }
            grdFileDtl.DataBind();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void lnkbtnDownload_Click(object sender, EventArgs e)
        {
            string ContentType = "";
            try
            {
                LinkButton lnkBtn = sender as LinkButton;
                GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
                Label lblFilePath = (Label)row.FindControl("lblFilePath");
                Label lblFileType = (Label)row.FindControl("lblFileType");

                string filePath = "/Files/Android_Files" + lblFilePath.Text;
                ContentType = lblFileType.Text.ToString();
                if (ContentType == "Video")
                {
                    Response.ContentType = "video/mp4";
                }
                else
                {
                    Response.ContentType = "audio/mp4";
                }
                filePath = "~/" + filePath.Substring(1);
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + lblFilePath.Text + "\"");
                string s = Server.MapPath(filePath);
                Response.TransmitFile(s);
                Response.End();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void grdFileDtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFileDtl.PageIndex = e.NewPageIndex;
            grdFileDtl.EditIndex = -1;
            BindGrid();
        }
    }
}