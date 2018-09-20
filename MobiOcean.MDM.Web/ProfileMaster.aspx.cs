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
    public partial class ProfileMaster : Base
    {
        ProfileBAL profilebal;
        GingerboxSrch Srch;
        VikramSearch searching;
        int ClientId, UserId, RoleId, DeptId;
        UserDeviceBAL usrdevice;
        ProfileBAL profile;
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
                BindGrid();
                reset();
            }
            lblmsg.Text = string.Empty;
        }
        protected void BindGrid()
        {
            try
            {
                Srch = new GingerboxSrch();
                if (RoleId <= 2)
                {
                    grdclient.DataSource = Srch.GetProfile(ClientId, txtPcode.Text.Trim(), txtPname.Text.Trim(), 0);
                    grdclient.DataBind();
                }
                else if (RoleId == 3)
                {
                    grdclient.DataSource = Srch.GetProfile(ClientId, txtPcode.Text.Trim(), txtPname.Text.Trim(), UserId);
                    grdclient.DataBind();
                }
                else
                {
                    grdclient.DataSource = null;
                    grdclient.DataBind();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                profilebal = null;
            }
        }
        protected void grdclient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdclient.PageIndex = e.NewPageIndex;
            grdclient.EditIndex = -1;
            BindGrid();
        }
        protected void grdclient_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdclient.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdclient_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            GridViewRow gvr = grdclient.Rows[e.RowIndex];
            try
            {
                profilebal = new ProfileBAL();
                profilebal.ProfileId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                profilebal.ClientId = ClientId;
                profilebal.ProfileCode = ((TextBox)gvr.FindControl("txtProfileCode")).Text.Trim();
                profilebal.ProfileName = ((TextBox)gvr.FindControl("txtProfileName")).Text.Trim();
                profilebal.ProfilePurpose = ((TextBox)gvr.FindControl("txtProfilePurpose")).Text.Trim();

                string res = profilebal.InsertProfileData();
                if (int.Parse(res) > 0)
                {
                    lblmsg.Text = "Updated Successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    grdclient.EditIndex = -1;
                    //SendUpdateMsg();
                    BindGrid();
                }
                else
                {
                    lblmsg.Text = "Already exists";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    grdclient.EditIndex = -1;
                    BindGrid();

                }
            }
            finally
            {
                profilebal = null;
            }
        }
        protected void grdclient_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            grdclient.EditIndex = -1;
            BindGrid();
        }
        protected void grdclient_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string profilename = "";
            profilebal = new ProfileBAL();
            GridViewRow gvr = grdclient.Rows[e.RowIndex];
            try
            {
                grdclient.EditIndex = -1;
                profilebal.ProfileId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                profilebal.UserId = UserId;
                profilename = ((Label)gvr.FindControl("lblProfileName")).Text.Trim();
                lblUser.Text = "Are you sure to delete " + profilename + " details?";
                lblalertprofileid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                mpdelete.Show();
                //    profilebal.DeleteProfile();
                //    lblmsg.Text = "Profile deleted successfully!";
                //    lblmsg.ForeColor = System.Drawing.Color.Green;
                //    SendUpdateMsg();
                //    BindGrid();
            }
            catch (Exception)
            {
            }
            finally
            {
                profilebal = null;
            }

        }
        protected void SendUpdateMsg()
        {
            usrdevice = new UserDeviceBAL();
            dt = new DataTable();
            usrdevice.ClientId = ClientId;
            dt = usrdevice.GetDeviceWithMDM();
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
        protected void addToTable_Click1(object sender, EventArgs e)
        {
            Response.Redirect("AddNewProfile.aspx");
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void lnkPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkPopUp = sender as LinkButton;
                GridViewRow gvr = lnkPopUp.NamingContainer as GridViewRow;
                Label lbl = ((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblId"));
                //lbllblProfileId.Text = lbl.Text;
                searching = new VikramSearch();
                if (RoleId == 1 || RoleId == 2)
                {
                    Gdv.DataSource = searching.srchUsrProfileMapping(ClientId, 0, Convert.ToInt32(lbl.Text), 0, 0);
                }
                else if (RoleId == 3)
                {
                    Gdv.DataSource = searching.srchUsrProfileMapping(ClientId, 0, Convert.ToInt32(lbl.Text), 0, DeptId);
                }
                else
                {
                    Gdv.DataSource = searching.srchUsrProfileMapping(ClientId, UserId, Convert.ToInt32(lbl.Text), 0, 0);
                }
                Gdv.DataBind();
                //mp.Show();
                Popup(true);
            }
            catch (Exception)
            {

            }
        }
        protected void lbEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = sender as LinkButton;
            GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
            string ProfileId = grdclient.DataKeys[row.RowIndex].Value.ToString();
            string ProfileName = ((Label)row.FindControl("lblProfileName")).Text.Trim();
            Session["ProfileName"] = ProfileName.ToString();
            Session["ProfileId"] = ProfileId.ToString();
            Response.Redirect("Feature.aspx");
        }
        protected void lnkbtnReport_Click(Object sender, EventArgs e)
        {
            LinkButton lnkbtnReport = sender as LinkButton;
            GridViewRow row = lnkbtnReport.NamingContainer as GridViewRow;
            string ProfileId = grdclient.DataKeys[row.RowIndex].Value.ToString();
            string ProfileName = ((Label)row.FindControl("lblProfileName")).Text.Trim();
            Session["ProfileName"] = ProfileName.ToString();
            Session["ProfileId"] = ProfileId.ToString();
            Response.Redirect("ProfileReport.aspx");
        }
        protected void lbPushprofile_Click(object sender, EventArgs e)
        {
            LinkButton lbPushprofile = sender as LinkButton;
            GridViewRow row = lbPushprofile.NamingContainer as GridViewRow;
            string ProfileId = grdclient.DataKeys[row.RowIndex].Value.ToString();
            string ProfileName = ((Label)row.FindControl("lblProfileName")).Text.Trim();
            Session["ProfileName"] = ProfileName.ToString();
            Session["ProfileId"] = ProfileId.ToString();
            Response.Redirect("PushProfile.aspx");
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                profile = new ProfileBAL();
                profile.ClientId = ClientId;
                profile.ProfileCode = txtAddProfileCode.Text.Trim();
                profile.ProfileName = txtAddProfileName.Text.Trim();
                profile.ProfilePurpose = txtAddProfilePurpose.Text.Trim();
                profile.UserId = UserId;
                string res = profile.InsertProfileData();
                if (int.Parse(res) > 0)
                {
                    lblmsg.Text = "Profile Details saved successfully.";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    reset();
                    BindGrid();
                }
                else
                {
                    lblmsg.Text = "Profile Details already exists.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    //reset();
                }

            }
            catch (Exception)
            {
            }
        }
        public void reset()
        {
            txtAddProfileCode.Text = "";
            txtAddProfileName.Text = "";
            txtAddProfilePurpose.Text = "";
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            string profileid = lblalertprofileid.Text;
            profilebal = new ProfileBAL();
            profilebal.ProfileId = Convert.ToInt32(profileid);
            profilebal.UserId = UserId;
            profilebal.DeleteProfile();
            lblmsg.Text = "Profile deleted successfully!";
            lblmsg.ForeColor = System.Drawing.Color.Green;
            SendUpdateMsg();
            BindGrid();
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalertprofileid.Text = "";
        }
    }
}
