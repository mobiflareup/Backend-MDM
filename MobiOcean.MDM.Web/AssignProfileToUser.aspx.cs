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
    public partial class AssignProfileToUser : Base
    {
        SendSMSBAL sms;
        DDLBAL ddl;
        ProfileBAL profilebal;
        ProfileUserMappingBAL profileusermapbal;
        UserDeviceBAL usrdevice;
        DataTable dt;
        int ClientId, RoleId, UserId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                BindUserName();
                BindProfile();
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
        protected void BindProfile()
        {

            ListItem ls = new ListItem("Select", "0");
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
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            if (ChkValidation())
            {
                profileusermapbal = new ProfileUserMappingBAL();
                profileusermapbal.ProfileUserId = 0;
                profileusermapbal.ClientId = ClientId;
                profileusermapbal.UserId = Convert.ToInt32(ddlUser.SelectedValue.ToString());
                profileusermapbal.ProfileId = Convert.ToInt32(ddlProfile.SelectedValue.ToString());
                profileusermapbal.LoggedBy = UserId;
                profileusermapbal.AppliedDateTime = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
                profileusermapbal.IsEnable = 1;
                profileusermapbal.Status = 0;
                int res = Convert.ToInt32(profileusermapbal.spProfileUserMapping());
                if (res > 0)
                {
                    lblMsg.Text = "Successfully Assigned";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    SendUpdateMsg(profileusermapbal.UserId);
                    Response.Redirect("ProfileUserMapping.aspx");
                }
                else
                {
                    lblMsg.Text = "Profile is already assigned to the User";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMsg.Text = "Please select all field";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfileUserMapping.aspx");
        }
        protected bool ChkValidation()
        {
            if (ddlProfile.SelectedIndex > 0 && ddlUser.SelectedIndex > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}