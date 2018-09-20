using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddNewProfile : Base
    {
        ProfileBAL profile;
        int ClientId, UserId, RoleId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblpopmsg.Text = string.Empty;
            if (!IsPostBack)
            {
                reset();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                profile = new ProfileBAL();
                profile.ClientId = ClientId;
                profile.ProfileCode = txtProfilecode.Text.Trim();
                profile.ProfileName = txtProfileName.Text.Trim();
                profile.ProfilePurpose = txtProfilePurpose.Text.Trim();
                string res = profile.InsertProfileData();
                if (int.Parse(res) > 0)
                {
                    lblpopmsg.Text = "Profile Details saved successfully.";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("ProfileMaster.aspx");
                }
                else
                {
                    lblpopmsg.Text = "Profile Details already exists.";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                    //reset();
                }

            }
            catch (Exception)
            {
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfileMaster.aspx");
        }
        public void reset()
        {
            txtProfilecode.Text = "";
            txtProfileName.Text = "";
            txtProfilePurpose.Text = "";
        }
    }
}