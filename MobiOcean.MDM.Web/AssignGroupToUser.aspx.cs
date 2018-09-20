using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AssignGroupToUser : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        ProfileBAL profilebal;
        UserDeviceBAL usrdevice;
        DataTable dt;
        SendSMSBAL sms;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindGroupName();
                BindProfile(ddlProfileName);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkValidations())
                {
                    profilebal = new ProfileBAL();
                    profilebal.ProfileGroupId = 0;
                    profilebal.ClientId = ClientId;
                    profilebal.IsEnable = 1;
                    profilebal.GroupId = Convert.ToInt32(ddlGroupName.SelectedValue.ToString());
                    profilebal.ProfileId = Convert.ToInt32(ddlProfileName.SelectedValue.ToString());
                    profilebal.AppliedDateTime = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
                    profilebal.LoggedBy = UserId.ToString();
                    profilebal.Status = 0;
                    string res = profilebal.spProfileUsrGrpMpng();
                    if (int.Parse(res) > 0)
                    {
                        lblMsg.Text = "Group Details Saved Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        SendUpdateMsg(profilebal.GroupId);
                        Response.Redirect("ProfileUserGroupMapping.aspx");

                    }
                    else
                    {
                        lblMsg.Text = "This Profile is already assigned to the Group.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;

                    }
                }
                else
                {
                    lblMsg.Text = "Please Fill Mandatory Fields";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfileUserGroupMapping.aspx");
        }
        protected void BindGroupName()
        {
            ListItem li = new ListItem("--Select--", "0");
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

            ListItem ls = new ListItem("--Select--", "0");
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
        protected bool ChkValidations()
        {
            if (ddlGroupName.SelectedIndex <= 0 || ddlProfileName.SelectedIndex <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}