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
    public partial class FeatureSosMgmt : Base
    {
        int ClientId, UserId, RoleId, ProfileId, DeptId;

        int CategoryId = 7;

        ProfileBAL probal;
        VikramSearch srch;        
        DataTable dt;
        string ProfileName;
        ContactBAL contact;
        WipePhoneBAL WipeBal;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = "";
            try
            {
                ProfileId = Convert.ToInt32(Session["ProfileId"].ToString());
                ProfileName = Session["ProfileName"].ToString();
            }
            catch (Exception) { ProfileId = 0; }
            if (ProfileId == 0)
            {
                Response.Redirect("ProfileMaster.aspx");
            }
            if (!IsPostBack)
            {
                //BindProfileDetail();
                BindGrid();
                txtProfileName.Text = ProfileName.ToString();
            }
            lblMessage.Text = "";
        }
        protected void BindGrid()
        {
            srch = new VikramSearch();
            grdDevice.DataSource = srch.srchProfilefeaturebycategoryid(ProfileId, CategoryId);
            grdDevice.DataBind();
        }       
        protected void grdDevice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView grdDevice = (DataRowView)e.Row.DataItem;
                    // LinkButton lnkSchedule = (LinkButton)e.Row.FindControl("Schedule");                
                    ImageButton imgbtnyes = (ImageButton)e.Row.FindControl("btnyes");
                    ImageButton imgbtnno = (ImageButton)e.Row.FindControl("btnNo");
                    CheckBox chk3 = (CheckBox)e.Row.FindControl("switchsize");
                    // Label lblIsScheduleNeed = (Label)e.Row.FindControl("lblIsScheduleNeed");   
                    try
                    {
                        chk3.Checked = Convert.ToBoolean(grdDevice["IsEnable"]);
                    }
                    catch (Exception) { chk3.Checked = false; }
                    if (chk3.Checked)
                    {
                        imgbtnyes.Visible = true;
                        //lnkSchedule.Enabled = true;
                    }
                    else
                    {
                        imgbtnno.Visible = true;
                        //lnkSchedule.Enabled = false;
                    }
                }
            }
            catch (Exception)
            { }
        }
        protected void btnCheckFeatureCancel_Click(object sender, EventArgs e)
        {
            MPCheckFeature.Hide();
        }
        protected void grdDevice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Yes")
            {
                GridViewRow gvr = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                //GridView GridFeature = (GridView)(gvr.Parent.Parent);
                ImageButton imgbtnyes = (ImageButton)gvr.FindControl("btnyes");
                ImageButton imgbtnno = (ImageButton)gvr.FindControl("btnNo");
                //LinkButton lnkSchedule = (LinkButton)gvr.FindControl("Schedule");
                CheckBox chk4 = (CheckBox)gvr.FindControl("switchsize");
                //Label lblIsScheduleNeed = (Label)gvr.FindControl("lblIsScheduleNeed");
                Label lblChanged = (Label)gvr.FindControl("lblChanged");
                //Label lblCId = (Label)gvr.FindControl("lblCId");

                try
                {
                    chk4.Checked = false;
                    imgbtnyes.Visible = false;
                    imgbtnno.Visible = true;
                    //lnkSchedule.Enabled = false;
                    //if (lblIsScheduleNeed.Text == "1")
                    //{
                    //    lnkSchedule.Text = "Schedule";
                    //}
                    //else
                    //{
                    //    lnkSchedule.Text = "&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;";
                    //}
                    //lnkSchedule.Enabled = false;
                    if (lblChanged.Text == "0")
                    {
                        lblChanged.Text = "1";
                    }
                    else
                    {
                        lblChanged.Text = "0";
                    }


                }
                catch (Exception)
                {
                    chk4.Checked = false;
                    imgbtnyes.Visible = true;
                    imgbtnno.Visible = false;
                }
            }
            else
            {
                GridViewRow gvr = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                //GridView GridFeature = (GridView)(gvr.Parent.Parent);
                ImageButton imgbtnyes = (ImageButton)gvr.FindControl("btnyes");
                ImageButton imgbtnno = (ImageButton)gvr.FindControl("btnNo");
                //LinkButton lnkSchedule = (LinkButton)gvr.FindControl("Schedule");
                CheckBox chk5 = (CheckBox)gvr.FindControl("switchsize");
                // Label lblIsScheduleNeed = (Label)gvr.FindControl("lblIsScheduleNeed");
                Label lblChanged = (Label)gvr.FindControl("lblChanged");
                Label lbl = ((Label)gvr.FindControl("lblId"));
                probal = new ProfileBAL();
                probal.ClientId = ClientId;
                probal.ProfileId = ProfileId;
                probal.FeatureId = Convert.ToInt32(lbl.Text);

                if (probal.CheckEnableFeatures())
                {
                    try
                    {
                        chk5.Checked = true;
                        imgbtnyes.Visible = true;
                        imgbtnno.Visible = false;
                        //if (lblIsScheduleNeed.Text == "1")
                        //{
                        //    lnkSchedule.Text = "Schedule";
                        //    lnkSchedule.Enabled = true;

                    //}
                    //else
                    //{
                    //    lnkSchedule.Text = "&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;";
                    //    lnkSchedule.Enabled = false;

                    //}
                    //ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + 5 + "','one');</script>");

                }
                catch (Exception)
                {
                    chk5.Checked = false;
                    imgbtnyes.Visible = true;
                    imgbtnno.Visible = false;

                    }
                    if (lblChanged.Text == "0")
                    {
                        lblChanged.Text = "1";
                    }
                    else
                    {
                        lblChanged.Text = "0";
                    }
                }
                else
                {
                    MPCheckFeature.Show();
                }
            }

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow rows in grdDevice.Rows)
            {
                if (rows.RowType == DataControlRowType.DataRow)
                {
                    if (((Label)rows.FindControl("lblChanged")).Text.Trim() == "1")
                    {
                        try
                        {
                            probal = new ProfileBAL();
                            probal.ClientId = ClientId;
                            probal.ProfileId = ProfileId;
                            probal.FeatureId = Convert.ToInt32(((Label)rows.FindControl("lblId")).Text.Trim());
                            probal.IsEnable = ((CheckBox)rows.FindControl("switchsize")).Checked ? 1 : 0;
                            probal.LoggedBy = UserId.ToString();
                            probal.IU_ProfileFetaureON();
                            probal.MoveDatainOriginalTables();
                        }
                        catch (Exception) { }
                    }
                }
            }
            //Response.Redirect("Feature.aspx");
            MP1.Show();
        }
        protected void ok_Click(object sender, EventArgs e)
        {
            Response.Redirect("Feature.aspx");
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            mpcancel.Show();
        }


        protected void btnSaveForm_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() != "" && txtMobileNo.Text.Trim() != "")
            {
                try
                {
                    contact = new ContactBAL();
                    contact.ClientId = ClientId;
                    contact.UserId = UserId;
                    contact.ProfileId = ProfileId;
                    contact.ContactPersonName = txtName.Text.Trim();
                    contact.Designation = txtDesignation.Text.Trim();
                    contact.ContactNo = txtMobileNo.Text.Trim();
                    contact.EmailId = txtEmailId.Text.Trim();
                    int res = contact.InsertSosContactsByProfile();
                    if (res > 0)
                    {
                        lblMsg.Text = "Saved Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        reset();
                        BindContactDetail();
                    }
                    else
                    {
                        lblMsg.Text = "Contact No already assigned to this profile.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                lblMsg.Text = "Please Enter Name and Mobile No";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

        }
        protected void reset()
        {
            txtName.Text = "";
            txtDesignation.Text = "";
            txtMobileNo.Text = "";
            txtEmailId.Text = "";
        }
        protected void btnAppmgmt_Click(object sender, EventArgs e)
        {
            if (PanelAppMgmt.Visible)
            {
                PanelAppMgmt.Visible = false;
            }
            else
            {
                BindContactDetail();
                PanelAppMgmt.Visible = true;
            }
        }

        private void BindContactDetail()
        {
            try
            {
                WipeBal = new WipePhoneBAL();
                WipeBal.ProfileId = ProfileId;
                dt = WipeBal.GetProfileSosContacts();
                ViewState["Sos"] = dt;
                grdAppGrp.DataSource = dt;
                grdAppGrp.DataBind();
            }
            catch (Exception)
            {
                lblMsg.Text = "Something went wrong.";
            }
            finally
            {
                WipeBal = null;
            }
        }
        protected void grdAppGrp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAppGrp.EditIndex = -1;
            BindContactDetail();
        }
        protected void grdAppGrp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                contact = new ContactBAL();
                GridViewRow gvr = grdAppGrp.Rows[e.RowIndex];
                contact.Contact_Id = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());

                int res = contact.DeleteSosContact();
                if (res > 0)
                {
                    lblMsg.Text = "Deleted Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    // grdAppGrp.EditIndex = -1;
                    BindContactDetail();
                }
                else
                {
                    lblMsg.Text = "Not Deleted!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }

            }
            catch (Exception)
            {
            }
        }
        protected void grdAppGrp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAppGrp.EditIndex = e.NewEditIndex;
            BindContactDetail();
        }
        protected void grdAppGrp_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                contact = new ContactBAL();
                GridViewRow gvr = grdAppGrp.Rows[e.RowIndex];
                contact.Contact_Id = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                contact.Name = ((TextBox)gvr.FindControl("txtGrpCode")).Text.Trim();
                contact.MobileNo = ((TextBox)gvr.FindControl("txtMobNo")).Text.Trim();
                contact.EmailId = ((TextBox)gvr.FindControl("txtEmail")).Text.Trim();
                contact.Designation = ((TextBox)gvr.FindControl("txtGrpName")).Text.Trim();
                contact.ProfileId = Convert.ToInt32(((Label)gvr.FindControl("lblProfileID")).Text.Trim());

                int res = contact.IU_SosContacts();
                if (res > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdAppGrp.EditIndex = -1;
                    BindContactDetail();
                }
                else
                {
                    lblMsg.Text = "Already exists";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }

            }
            catch (Exception)
            {
            }

        }
        protected void CancelForm_Click(object sender, EventArgs e)
        {
            reset();
        }
        protected void grdAppGrp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int count = grdAppGrp.Rows.Count;
                int c = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label Status = ((Label)e.Row.FindControl("lblStatus"));
                    CheckBox chkbox = ((CheckBox)e.Row.FindControl("chkbox"));
                    if (Status.Text == "0")
                    {
                        chkbox.Checked = true;
                    }
                    else
                    {
                        chkbox.Checked = false;
                    }
                }

                for (int idx = 0; idx < grdAppGrp.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdAppGrp.Rows[idx].FindControl("chkbox"))).Checked)
                    {
                        c++;
                    }
                }
                CheckBox chkheader = (CheckBox)grdAppGrp.HeaderRow.FindControl("Sos_parent");
                if (c == count)
                {

                    chkheader.Checked = true;
                }
                else
                {
                    chkheader.Checked = false;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnApplyProfileChanges_Click(object sender, EventArgs e)
        {
            try
            {
                contact = new ContactBAL();
                string SosIdList = "";
                contact.UserId = UserId;
                for (int idx = 0; idx < grdAppGrp.Rows.Count; idx++)
                {
                    if (((CheckBox)grdAppGrp.Rows[idx].FindControl("chkbox")).Checked)
                    {
                        contact.Status = 0;
                        SosIdList = ((Label)grdAppGrp.Rows[idx].FindControl("lblId")).Text;
                        contact.SosIdList = SosIdList;
                        contact.AssignSosContactsToProfile();
                    }

                    else
                    {
                        contact.Status = 1;
                        SosIdList = ((Label)grdAppGrp.Rows[idx].FindControl("lblId")).Text;
                        contact.SosIdList = SosIdList;
                        contact.AssignSosContactsToProfile();
                    }

                }
                lblMessage.Text = "Changes Applied Successfully";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }

            catch (Exception)
            {
                lblMessage.Text = "Changes Not Saved";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            BindContactDetail();
        }
        protected void Sos_parent_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdAppGrp.HeaderRow.FindControl("Sos_parent");
            foreach (GridViewRow row in grdAppGrp.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkbox");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void btncancelok_Click(object sender, EventArgs e)
        {
            Response.Redirect("Feature.aspx");
        }
        protected void btncancelcan_Click(object sender, EventArgs e)
        {
            mpcancel.Hide();
        }
    }
}
