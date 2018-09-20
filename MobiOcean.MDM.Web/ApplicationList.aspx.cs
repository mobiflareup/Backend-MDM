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
    public partial class ApplicationList : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        DDLBAL ddl;
        GroupBAL grpbal;
        AnuSearch srch;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                //BindGroupDDL(ddlSrchGroup);
                BindUserName();
                BindGrid();
            }
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void BindUserName()
        {
            try
            {
                ListItem ls = new ListItem("--All--", "0");
                ddl = new DDLBAL();
                ddl.ClientId = ClientId;
                ddl.UserId = UserId;
                ddl.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = ddl.GetUserDeviceByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = ddl.GetUserDeviceWithoutDeptHead();
                }
                else
                {
                    ddlUserName.DataSource = ddl.GetUserDeviceByUserId();
                }
                ddlUserName.DataTextField = "DeviceName";
                ddlUserName.DataValueField = "DeviceId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }

        }
        protected void grdAppMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAppMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void BindGroupDDL(DropDownList ddl)
        {
            ListItem ls = new ListItem("--All--", "0");
            try
            {
                grpbal = new GroupBAL();
                ddl.Items.Clear();
                ddl.Items.Add(ls);
                ddl.DataSource = grpbal.GetAppGrpNameForDDL();
                ddl.DataTextField = "AppGroupName";
                ddl.DataValueField = "AppGroupId";
                ddl.DataBind();
            }
            catch (Exception) { }
            finally
            {
                ls = null;
                grpbal = null;
            }
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            if (RoleId == 1 || RoleId == 2)
            {
                grdAppMaster.DataSource = srch.srchAppList(ClientId, 0, "0", txtSrchApplication.Text.Trim(), Convert.ToInt32(ddlUserName.SelectedValue.ToString()));
            }
            else if (RoleId == 3)
            {
                grdAppMaster.DataSource = srch.srchAppList(ClientId, 0, "0", txtSrchApplication.Text.Trim(), Convert.ToInt32(ddlUserName.SelectedValue.ToString()), DeptId);
            }
            else
            {
                grdAppMaster.DataSource = srch.srchAppList(ClientId, UserId, "0", txtSrchApplication.Text.Trim(), Convert.ToInt32(ddlUserName.SelectedValue.ToString()));
            }
            grdAppMaster.DataBind();
        }
        protected void grdAppMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAppMaster.EditIndex = -1;
            BindGrid();
        }
        protected void grdAppMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAppMaster.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdAppMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdAppMaster.Rows[e.RowIndex];
            try
            {
                grpbal = new GroupBAL();
                // dt = new DataTable();
                grpbal.ClientId = ClientId;
                grpbal.AppId = ((Label)gvr.FindControl("lblId")).Text.Trim();
                grpbal.GroupName = ((DropDownList)gvr.FindControl("EddlGroupName")).SelectedItem.Text.Trim();
                grpbal.ChatGroupId = Convert.ToInt32(((DropDownList)gvr.FindControl("EddlGroupName")).SelectedValue.ToString());
                grpbal.LoggedBy = UserId.ToString();
                int res = grpbal.UpdateappGroupByUser();
                //if (int.Parse(res) > 0)
                if (res > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                    grdAppMaster.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Details already exists";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            finally
            {
                grpbal = null;
            }
        }
        protected void grdAppMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ListItem ls = new ListItem("Select", "0");
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList EddlGroupName = (DropDownList)e.Row.FindControl("EddlGroupName");
                    Label ElblGroupName = (Label)e.Row.FindControl("ElblGroupName");
                    //  BindGroupDDL(EddlGroupName);

                    grpbal = new GroupBAL();
                    EddlGroupName.Items.Clear();
                    EddlGroupName.Items.Add(ls);
                    EddlGroupName.DataSource = grpbal.GetAppGrpNameForDDL();
                    EddlGroupName.DataTextField = "AppGroupName";
                    EddlGroupName.DataValueField = "AppGroupId";
                    EddlGroupName.DataBind();
                    EddlGroupName.SelectedValue = ElblGroupName.Text;
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "Something Went Wrong!";
            }
        }
    }
}