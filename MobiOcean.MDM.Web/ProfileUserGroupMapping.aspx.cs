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
    public partial class ProfileUserGroupMapping : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        ProfileBAL profilebal;
        AnuSearch srch;
        UserDeviceBAL usrdevice;
        DataTable dt;
        SendSMSBAL sms;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindGroupName();
                BindProfile(ddlProfileName);
                BindGrid();
            }
        }
        protected void BindGroupName()
        {
            ListItem li = new ListItem("All", "0");
            try
            {
                profilebal = new ProfileBAL();
                profilebal.ClientId = ClientId;
                ddlGroupName.Items.Clear();
                ddlGroupName.Items.Add(li);
                ddlGroupName.DataSource = profilebal.GetGroupName();
                ddlGroupName.DataTextField = "GrouppName";
                ddlGroupName.DataValueField = "GroupId";
                ddlGroupName.DataBind();
            }
            catch (Exception) { }
            finally
            {
                profilebal = null;
            }
        }
        protected void BindProfile(DropDownList ddlProfile)
        {

            ListItem ls = new ListItem("All", "0");
            try
            {
                profilebal = new ProfileBAL();
                profilebal.ClientId = ClientId;

                ddlProfile.Items.Clear();
                ddlProfile.Items.Add(ls);
                ddlProfile.DataSource = profilebal.GetProfileData();
                ddlProfile.DataTextField = "ProfileName";
                ddlProfile.DataValueField = "ProfileId";
                ddlProfile.DataBind();
            }
            catch (Exception) { }
            finally
            {
                ls = null;
                profilebal = null;
            }
        }
        protected void BindGrid()
        {
            string GroupId, ProfileId;
            GroupId = ddlGroupName.SelectedValue.ToString();
            ProfileId = ddlProfileName.SelectedValue.ToString();

            try
            {
                srch = new AnuSearch();
                grdProfile.DataSource = srch.SearchProfileUsrGrpDtls(ClientId, GroupId, ProfileId);
                grdProfile.DataBind();
            }
            catch (Exception)
            {
            }
            finally
            {
                srch = null;
            }
        }
        protected void addToTable_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssignGroupToUser.aspx");
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdProfile_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProfile.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdProfile_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                profilebal = new ProfileBAL();
                GridViewRow gvr = grdProfile.Rows[e.RowIndex];
                profilebal.ProfileGroupId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                profilebal.ProfileId = Convert.ToInt32(((DropDownList)gvr.FindControl("ddlProfileName")).SelectedValue.ToString());
                profilebal.IsEnable = Convert.ToInt32(((DropDownList)gvr.FindControl("ddlIsEnable")).SelectedValue.ToString().Trim());
                profilebal.AppliedDateTime = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
                profilebal.ClientId = ClientId;
                profilebal.GroupId = Convert.ToInt32(((Label)gvr.FindControl("lblGroupId")).Text.Trim());
                profilebal.LoggedBy = UserId.ToString();
                profilebal.Status = 0;
                string res = profilebal.spProfileUsrGrpMpng();
                if (int.Parse(res) > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdProfile.EditIndex = -1;
                    SendUpdateMsg(profilebal.GroupId);
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "This Profile is already assigned to the Group.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }

            }
            catch (Exception)
            {
            }
        }
        protected void grdProfile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                profilebal = new ProfileBAL();
                GridViewRow gvr = grdProfile.Rows[e.RowIndex];
                profilebal.ProfileGroupId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                profilebal.ProfileId = Convert.ToInt32(((Label)gvr.FindControl("lblProfileId")).Text.Trim());
                profilebal.IsEnable = 0;
                profilebal.AppliedDateTime = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
                profilebal.ClientId = ClientId;
                profilebal.GroupId = Convert.ToInt32(((Label)gvr.FindControl("lblGroupId")).Text.Trim());
                profilebal.LoggedBy = UserId.ToString();
                profilebal.Status = 1;
                string res = profilebal.spProfileUsrGrpMpng();
                if (profilebal.DeleteProfileUsrGrpDtls() > 0)
                {
                    lblMsg.Text = "Deleted Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    SendUpdateMsg(profilebal.GroupId);
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Not deleted";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
            }
        }
        protected void grdProfile_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdProfile.EditIndex = -1;
            BindGrid();
        }
        protected void grdProfile_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProfile.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void grdProfile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                try
                {
                    DropDownList ddlProfileName = (DropDownList)e.Row.FindControl("ddlProfileName");
                    DropDownList ddlIsEnable = (DropDownList)e.Row.FindControl("ddlIsEnable");
                    Label lblEIsEnable = (Label)e.Row.FindControl("lblEIsEnable");
                    if (ddlProfileName != null)
                    {
                        BindProfile(ddlProfileName);
                        Label lblProfileName = (Label)e.Row.FindControl("lblEProfileId");
                        if (lblProfileName != null)
                        {
                            ddlProfileName.SelectedValue = lblProfileName.Text.Trim().ToString();
                        }
                    }
                    ddlIsEnable.SelectedValue = lblEIsEnable.Text.Trim();
                }
                catch (Exception)
                {

                }
            }
        }
        protected void SendUpdateMsg(int GroupId)
        {
            usrdevice = new UserDeviceBAL();
            dt = new DataTable();
            usrdevice.GroupId = GroupId;
            usrdevice.ClientId = ClientId;
            dt = usrdevice.GetUserDevicebyGroupId();
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(row["MobileNo1"].ToString(), "GBox set as WP7", ClientId);
                }
                catch (Exception)
                { }
            }

        }
    }
}