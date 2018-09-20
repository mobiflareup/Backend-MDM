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
    public partial class ProfileUserMapping : Base
    {
        DDLBAL ddl;
        ProfileUserMappingBAL profileusermapbal;
        ProfileBAL profilebal;
        VikramSearch srch;
        AnuSearch asrch;
        UserDeviceBAL usrdevice;
        DataTable dt;
        SendSMSBAL sms;
        int ClientId, RoleId, UserId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["Role"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                BindProfile(ddlProfile);
                BindUserName();
                BindGrid();
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
        protected void BindUserName()
        {
            try
            {
                ListItem ls = new ListItem("--Select--", "0");
                ddl = new DDLBAL();
                ddl.ClientId = ClientId;
                ddl.UserId = UserId;
                ddl.DeptId = DeptId;
                ddlUser.Items.Clear();
                ddlUser.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUser.DataSource = ddl.GetUserByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUser.DataSource = ddl.GetUserWithoutDeptHead();
                }
                else
                {
                    ddlUser.DataSource = ddl.GetUserByReporntgMngrWITHOUTmngr();
                }
                ddlUser.DataTextField = "UserName";
                ddlUser.DataValueField = "UserId";
                ddlUser.DataBind();
            }
            catch (Exception)
            {

            }

        }
        protected void BindGrid()
        {
            srch = new VikramSearch();
            if (RoleId == 1 || RoleId == 2)
            {
                grdProfile.DataSource = srch.srchUsrProfileMapping(ClientId, 0, Convert.ToInt32(ddlProfile.SelectedValue.ToString()), Convert.ToInt32(ddlUser.SelectedValue.ToString()));
            }
            else if (RoleId == 3)
            {
                grdProfile.DataSource = srch.srchUsrProfileMapping(ClientId, 0, Convert.ToInt32(ddlProfile.SelectedValue.ToString()), Convert.ToInt32(ddlUser.SelectedValue.ToString()), DeptId);
            }
            else
            {
                grdProfile.DataSource = srch.srchUsrProfileMapping(ClientId, UserId, Convert.ToInt32(ddlProfile.SelectedValue.ToString()), Convert.ToInt32(ddlUser.SelectedValue.ToString()));
            }
            //grdProfile.DataSource = srch.srchUsrProfileMapping(ClientId, Convert.ToInt32(ddlProfile.SelectedValue.ToString()), Convert.ToInt32(ddlUser.SelectedValue.ToString()));
            grdProfile.DataBind();
        }
        protected void grdProfile_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdProfile.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdProfile_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdProfile.Rows[e.RowIndex];
            profileusermapbal = new ProfileUserMappingBAL();
            profileusermapbal.ProfileUserId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
            profileusermapbal.LoggedBy = UserId;
            profileusermapbal.AppliedDateTime = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
            profileusermapbal.IsEnable = 0;
            profileusermapbal.ProfileId = Convert.ToInt32(((Label)gvr.FindControl("lblProfileId")).Text.Trim());
            profileusermapbal.ClientId = ClientId;
            profileusermapbal.UserId = Convert.ToInt32(((Label)gvr.FindControl("lblUserId")).Text.Trim());
            profileusermapbal.Status = 1;
            int res = Convert.ToInt32(profileusermapbal.DeleteProfileUserMapping());
            if (res > 0)
            {
                lblMsg.Text = "Successfully Deleted";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                SendUpdateMsg(profileusermapbal.UserId);
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Not Deleted";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void grdProfile_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdProfile.Rows[e.RowIndex];
            profileusermapbal = new ProfileUserMappingBAL();
            profileusermapbal.ProfileUserId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
            profileusermapbal.ProfileId = Convert.ToInt32(((DropDownList)gvr.FindControl("ddlProfileName")).SelectedValue.ToString().Trim());
            profileusermapbal.LoggedBy = UserId;
            profileusermapbal.AppliedDateTime = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
            profileusermapbal.IsEnable = Convert.ToInt32(((DropDownList)gvr.FindControl("ddlIsEnable")).SelectedValue.ToString().Trim());
            profileusermapbal.ClientId = ClientId;
            profileusermapbal.UserId = Convert.ToInt32(((Label)gvr.FindControl("lblUserId")).Text.Trim());
            profileusermapbal.Status = 0;
            int res = Convert.ToInt32(profileusermapbal.spProfileUserMapping());
            if (res > 0)
            {
                lblMsg.Text = "Successfully Updated";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                grdProfile.EditIndex = -1;
                SendUpdateMsg(profileusermapbal.UserId);
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Not Updated";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void grdProfile_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            grdProfile.EditIndex = -1;
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
        protected void addToTable_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssignProfileToUser.aspx");
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdProfile_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProfile.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void lnkbtnHistory_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtnHistory = sender as LinkButton;
            GridViewRow gvr = lnkbtnHistory.NamingContainer as GridViewRow;
            Label lbl = (Label)grdProfile.Rows[gvr.RowIndex].FindControl("lblId");
            asrch = new AnuSearch();
            Gdv.DataSource = asrch.GetProfileUserHistory(ClientId, Convert.ToInt32(lbl.Text));
            Gdv.DataBind();
            Popup(true);
            // mpe.Show();
        }
        protected string MyFormat(string CreationDate)
        {
            return Convert.ToDateTime(CreationDate).ToString("dd MMM yyyy HH:mm");
        }
        protected void SendUpdateMsg(int UsrId)
        {
            usrdevice = new UserDeviceBAL();
            dt = new DataTable();
            usrdevice.UserId = UsrId;
            usrdevice.ClientId = ClientId;
            dt = usrdevice.GetDevicewithMDMByUserId();
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
        void Popup(bool isDisplay)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //if (isDisplay)
            //{
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#myModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
            //}
            //else
            //{
            //   // sb.Append(@"<script type='text/javascript'>");

            //   // sb.Append(" $('#myModal').removeClass('show');");
            //   //// sb.Append(" $('#myModal').removeClass('fade');");
            //   // //sb.Append("$('#myModal').modal('hide');");
            //   // sb.Append(@"</script>");
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "openModal();", false);
            //}

        }
    }
}