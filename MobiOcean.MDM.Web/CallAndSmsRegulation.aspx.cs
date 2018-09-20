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
    public partial class CallAndSmsRegulation : Base
    {
        int ClientId, UserId, RoleId, FeatureId, ProfileId, HdrId, AlowFromDay, AlowToDay, TotalDuration, DeptId;
        string AlowFromTime, AlowToTime, ProfileName;
        int CategoryId = 6;
        ProfileBAL probal;
        VikramSearch srch;
        GingerboxSrch GSrch;
        DataTable  dtMsg, dt1;
        AlertBAL alert;
        AllowPhNoBAL allowphno;
        PermisesBAL perm;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
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
                dt1 = new DataTable();
                perm = new PermisesBAL();
                dt1 = perm.GetCountries();
                ViewState["GetCountry"] = dt1;
                BindCountryddl();
                txtProfileName.Text = ProfileName.ToString();
            }
            lblNotMsg.Text = string.Empty;
            lblManFields.Text = string.Empty;
            Label1.Text = string.Empty;
            lblMultipleSlotSlctnMSGDayNumber.Text = string.Empty;
            lblMultipleSlotSlctnMSG.Text = string.Empty;
            lblCAllSms.Text = "";
            SmsLabl.Text = "";
            alertlbl.Text = "";
        }
        protected void BindCountryddl()
        {
            #region--------- Get School List --------
            try
            {
                ListItem li = new ListItem("Select", "0");
                ddlCountry.Items.Clear();
                ddlCountry.Items.Add(li);
                ddlCountry.DataSource = (DataTable)ViewState["GetCountry"];
                ddlCountry.DataTextField = "Country";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ListItem li1 = new ListItem("Select", "0");
                ddlCallMCountry.Items.Clear();
                ddlCallMCountry.Items.Add(li1);
                ddlCallMCountry.DataSource = (DataTable)ViewState["GetCountry"];
                ddlCallMCountry.DataTextField = "Country";
                ddlCallMCountry.DataValueField = "CountryId";
                ddlCallMCountry.DataBind();
            }
            catch (Exception)
            {
            }

            #endregion
        }
        protected void BindGrid()
        {
            srch = new VikramSearch();
            grdDevice.DataSource = srch.srchProfilefeaturebycategoryid(ProfileId, CategoryId);
            grdDevice.DataBind();
        }
        protected void btnCheckFeatureCancel_Click(object sender, EventArgs e)
        {
            MPCheckFeature.Hide();
        }
        protected void grdDevice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView grdDevice = (DataRowView)e.Row.DataItem;
                    LinkButton lnkSchedule = (LinkButton)e.Row.FindControl("Schedule");
                    ImageButton imgbtnyes = (ImageButton)e.Row.FindControl("btnyes");
                    ImageButton imgbtnno = (ImageButton)e.Row.FindControl("btnNo");
                    CheckBox chk3 = (CheckBox)e.Row.FindControl("switchsize");
                    Label lblIsScheduleNeed = (Label)e.Row.FindControl("lblIsScheduleNeed");
                    Label lblIsManageNeed = (Label)e.Row.FindControl("lblIsManageNeed");
                    Label FeatureId = (Label)e.Row.FindControl("lblId");
                    TextBox txtfreq = (TextBox)e.Row.FindControl("txtfreq");
                    Label lblDays = (Label)e.Row.FindControl("lblDays");
                    LinkButton lnkManage = (LinkButton)e.Row.FindControl("Manage");
                    try
                    {
                        chk3.Checked = Convert.ToBoolean(grdDevice["IsEnable"]);
                    }
                    catch (Exception) { chk3.Checked = false; }
                    if (chk3.Checked)
                    {
                        imgbtnyes.Visible = true;
                        lnkSchedule.Enabled = true;
                        lnkManage.Enabled = true;
                        lnkSchedule.Attributes["style"] = " background-color:#2A368B !important;";
                        lnkManage.Attributes["style"] = " background-color:#2A368B !important;";
                    }
                    else
                    {
                        imgbtnno.Visible = true;
                        lnkSchedule.Enabled = false;
                        lnkManage.Enabled = false;
                        lnkSchedule.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                        lnkManage.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                    }
                    if (lblIsScheduleNeed.Text == "1")
                    {
                        lnkSchedule.Text = "Schedule";
                    }
                    else
                    {
                        lnkSchedule.Text = "&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;";
                        lnkSchedule.Enabled = false;
                        lnkSchedule.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                    }
                    if (lblIsManageNeed.Text == "1")
                    {
                        lnkManage.Text = "Manage";
                        if (FeatureId.Text == "17" || FeatureId.Text == "18")
                        {

                            txtfreq.Visible = true;
                            lblDays.Visible = true;
                            lnkManage.Visible = false;
                            lnkManage.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                        }
                    }
                    else
                    {
                        lnkManage.Text = "&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;";
                        lnkManage.Enabled = false;
                        lnkManage.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        protected void grdDevice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Schedule")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lbl = ((Label)gvr.FindControl("lblId"));
                //Label lbl1 = ((Label)gvr.FindControl("lblProfileId"));
                lblFeatureN.Text = "Schedule " + ((Label)gvr.FindControl("lblFeatureName")).Text;
                lblFeatureId.Text = lbl.Text;
                lblProfilePopId.Text = ProfileId.ToString();
                lblHdrId.Text = ((Label)gvr.FindControl("lblProfileFeatureId")).Text;
                lblCategoryIdMp.Text = CategoryId.ToString();
                BindTimingGrid();
                lblAppHead.Visible = false;
                ddlAppGroupMain.Visible = false;
                gdv.Columns[2].Visible = false;
                if (((Label)gvr.FindControl("lblDurationReq")).Text == "0")
                {
                    txtTotal.Visible = false;
                    gdv.Columns[7].Visible = false;
                }
                else
                {
                    txtTotal.Visible = true;
                    gdv.Columns[7].Visible = true;
                }
                //if (lbl.Text == "43" || lbl.Text == "44")
                //{
                //    txtTotal.Visible = true;
                //    gdv.Columns[7].Visible = true;
                //}
                //else
                //{
                //    txtTotal.Visible = false;
                //    gdv.Columns[7].Visible = false;
                //}
                mp.Show();
            }
            else if (e.CommandName == "Manage")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label featureid = ((Label)gvr.FindControl("lblId"));
                Label profileid = ((Label)gvr.FindControl("lblProfileId"));
                lblHdrId.Text = ((Label)gvr.FindControl("lblProfileFeatureId")).Text;
                lblFeatureId.Text = featureid.Text;
                lblNotMsg.Text = string.Empty;
                if (featureid.Text == "24")
                {
                    Multiview1.ActiveViewIndex = 0;
                    lblKey.Text = "Keyword Management";
                    BindKeywordGrid();
                    mpemanage.Show();
                }
                if (featureid.Text == "52")
                {
                    Multiview1.ActiveViewIndex = 1;
                    lblKey.Text = "Alert Management";
                    BindAlertGrid();
                    mpemanage.Show();
                }
                if (featureid.Text == "19")
                {
                    Multiview1.ActiveViewIndex = 2;
                    lblKey.Text = "Call Management";
                    rbtnCall.SelectedValue = ((TextBox)gvr.FindControl("txtfreq")).Text.Trim() == "1" ? "1" : "0";
                    BindCallGrid();
                    mpemanage.Show();
                }
                if (featureid.Text == "22")
                {
                    Multiview1.ActiveViewIndex = 3;
                    lblKey.Text = "Sms Management";
                    rbtnsms.SelectedValue = ((TextBox)gvr.FindControl("txtfreq")).Text.Trim() == "1" ? "1" : "0";
                    BindSmsGrid();
                    mpemanage.Show();
                }
            }
            else if (e.CommandName == "Yes")
            {
                GridViewRow gvr = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                //GridView GridFeature = (GridView)(gvr.Parent.Parent);
                ImageButton imgbtnyes = (ImageButton)gvr.FindControl("btnyes");
                ImageButton imgbtnno = (ImageButton)gvr.FindControl("btnNo");
                LinkButton lnkSchedule = (LinkButton)gvr.FindControl("Schedule");
                LinkButton lnkManage = (LinkButton)gvr.FindControl("Manage");
                CheckBox chk4 = (CheckBox)gvr.FindControl("switchsize");
                Label lblIsScheduleNeed = (Label)gvr.FindControl("lblIsScheduleNeed");
                Label lblChanged = (Label)gvr.FindControl("lblChanged");
                Label lblIsManageNeed = (Label)gvr.FindControl("lblIsManageNeed");
                //Label lblCId = (Label)gvr.FindControl("lblCId");

                try
                {
                    chk4.Checked = false;
                    imgbtnyes.Visible = false;
                    imgbtnno.Visible = true;
                    //lnkSchedule.Enabled = false;
                    if (lblIsScheduleNeed.Text == "1")
                    {
                        lnkSchedule.Text = "Schedule";
                    }
                    else
                    {
                        lnkSchedule.Text = "&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                    lnkSchedule.Enabled = false;
                    lnkSchedule.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                    lnkManage.Enabled = false;
                    lnkManage.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
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
                LinkButton lnkSchedule = (LinkButton)gvr.FindControl("Schedule");
                LinkButton lnkManage = (LinkButton)gvr.FindControl("Manage");
                CheckBox chk5 = (CheckBox)gvr.FindControl("switchsize");
                Label lblIsScheduleNeed = (Label)gvr.FindControl("lblIsScheduleNeed");
                Label lblChanged = (Label)gvr.FindControl("lblChanged");
                Label lblIsManageNeed = (Label)gvr.FindControl("lblIsManageNeed");
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
                        if (lblIsScheduleNeed.Text == "1")
                        {
                            lnkSchedule.Enabled = true;
                            lnkSchedule.Attributes["style"] = " background-color:#2A368B !important;";
                        }
                        else
                        {
                            lnkSchedule.Enabled = false;
                            lnkSchedule.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                        }
                        if (lblIsManageNeed.Text == "1")
                        {
                            lnkManage.Enabled = true;
                            lnkManage.Attributes["style"] = " background-color:#2A368B !important;";
                        }
                        else
                        {
                            lnkManage.Enabled = false;
                            lnkManage.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                        }
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
                    try
                    {
                        lblerrmsg.Text = string.Empty;
                        probal = new ProfileBAL();
                        if (((CheckBox)rows.FindControl("switchsize")).Checked && (Convert.ToInt32(((Label)rows.FindControl("lblId")).Text.Trim()) == 17 || Convert.ToInt32(((Label)rows.FindControl("lblId")).Text.Trim()) == 18))
                        {
                            if (string.IsNullOrEmpty(((TextBox)rows.FindControl("txtfreq")).Text.Trim()))
                            {
                                if (Convert.ToInt32(((Label)rows.FindControl("lblId")).Text.Trim()) == 18)
                                {
                                    lblerrmsg.Text = "Please enter frequency for Sync Calender feature.";
                                }
                                else
                                    lblerrmsg.Text = "Please enter frequency for Sync Address Book feature.";
                                break;
                            }
                            else
                            {
                                try
                                {
                                    probal.AutoSyncOn = Convert.ToInt32(((TextBox)rows.FindControl("txtfreq")).Text.Trim());
                                }
                                catch (Exception)
                                {
                                    lblerrmsg.Text = "Please enter valid frequency.";
                                    break;
                                }
                            }

                        }
                        else if (Convert.ToInt32(((Label)rows.FindControl("lblId")).Text.Trim()) == 19)
                        {
                            probal.AutoSyncOn = Convert.ToInt32(rbtnCall.SelectedValue.ToString());
                        }
                        else if (Convert.ToInt32(((Label)rows.FindControl("lblId")).Text.Trim()) == 22)
                        {
                            probal.AutoSyncOn = Convert.ToInt32(rbtnsms.SelectedValue.ToString());
                        }
                        else
                        {
                            probal.AutoSyncOn = 0;
                        }

                        probal.ClientId = ClientId;
                        probal.ProfileId = ProfileId;
                        probal.FeatureId = Convert.ToInt32(((Label)rows.FindControl("lblId")).Text.Trim());
                        probal.IsEnable = ((CheckBox)rows.FindControl("switchsize")).Checked ? 1 : 0;
                        probal.IU_ProfileFetaureON();
                    }
                    catch (Exception) { }
                }
            }
            if (lblerrmsg.Text == "")
            {
                probal = new ProfileBAL();
                probal.ProfileId = ProfileId;
                probal.LoggedBy = UserId.ToString();
                probal.MoveDatainOriginalTables();
                //Session["ProfileId"] = null;
                //SendUpdateMsg();
                MP1.Show();
                //            Response.Redirect("Feature.aspx");
            }
        }
        protected void ok_Click(object sender, EventArgs e)
        {
            Response.Redirect("Feature.aspx");
        }
        private void BindTimingGrid()
        {
            try
            {
                int ProfileFeatureMappingId, GroupId;
                GSrch = new GingerboxSrch();
                try
                {
                    ProfileFeatureMappingId = int.Parse(lblHdrId.Text);
                }
                catch (Exception)
                {

                    ProfileFeatureMappingId = GSrch.GetProfileFeatureMappingId(ProfileId, Convert.ToInt32(lblFeatureId.Text));
                }
                GroupId = 0;
                gdv.DataSource = GSrch.FeatureTimingfromTemp(ProfileFeatureMappingId, GroupId);
                gdv.DataBind();
                gdv.EditIndex = -1;
            }
            catch (Exception)
            {

            }
        }
        protected void BindGroupDDL(DropDownList ddl)
        {
            //ListItem ls = new ListItem("---Select---", "0");
            //try
            //{
            //    grpbal = new GroupBAL();
            //    ddl.Items.Clear();
            //    ddl.Items.Add(ls);
            //    ddl.DataSource = grpbal.GetAppGrpNameForDDL();
            //    ddl.DataTextField = "AppGroupName";
            //    ddl.DataValueField = "AppGroupId";
            //    ddl.DataBind();
            //}
            //catch (Exception  { }
            //finally
            //{
            //    ls = null;
            //    grpbal = null;
            //}
        }
        protected void gdv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddlEFromHour = (DropDownList)e.Row.FindControl("ddlEFromHour");
                DropDownList ddlEFromMin = (DropDownList)e.Row.FindControl("ddlEFromMin");
                DropDownList ddlEToHour = (DropDownList)e.Row.FindControl("ddlEToHour");
                DropDownList ddlEToMin = (DropDownList)e.Row.FindControl("ddlEToMin");

                Label lblEInCallAlowFromTime = (Label)e.Row.FindControl("lblEInCallAlowFromTime");
                Label lblEInCallAlowToTime = (Label)e.Row.FindControl("lblEInCallAlowToTime");

                if (string.IsNullOrEmpty(lblEInCallAlowFromTime.Text))
                {
                    ddlEFromHour.SelectedIndex = 0;
                    ddlEFromMin.SelectedIndex = 0;
                }
                else
                {
                    ddlEFromHour.SelectedValue = lblEInCallAlowFromTime.Text.Substring(0, 2);
                    ddlEFromMin.SelectedValue = lblEInCallAlowFromTime.Text.Substring(3, 2);
                }
                if (string.IsNullOrEmpty(lblEInCallAlowToTime.Text))
                {
                    ddlEToHour.SelectedIndex = 0;
                    ddlEToMin.SelectedIndex = 0;
                }
                else
                {
                    ddlEToHour.SelectedValue = lblEInCallAlowToTime.Text.Substring(0, 2);
                    ddlEToMin.SelectedValue = lblEInCallAlowToTime.Text.Substring(3, 2);
                }
            }
        }
        protected void btnClose1_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + lblCategoryIdMp.Text + "','one');</script>");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            // ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + lblCategoryIdMp.Text + "','one');</script>");
            mp.Hide();
        }
        protected void btnClose1_Click1(object sender, EventArgs e)
        {
            // ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + lblCategoryIdMp.Text + "','one');</script>");
            mp.Hide();
        }
        protected void gdv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gdv.EditIndex = e.NewEditIndex;
            BindTimingGrid();
            mp.Show();

        }
        protected void gdv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = gdv.Rows[e.RowIndex];
            DropDownList ddlUFromHr = (DropDownList)gvr.FindControl("ddlEFromHour");
            DropDownList ddlUFromMin = (DropDownList)gvr.FindControl("ddlEFromMin");
            DropDownList ddlUToHour = (DropDownList)gvr.FindControl("ddlEToHour");
            DropDownList ddlUToMin = (DropDownList)gvr.FindControl("ddlEToMin");
            TextBox txtUTotal = ((TextBox)gvr.FindControl("txtETotal"));
            Label lblUDtlID = ((Label)gvr.FindControl("lblTimingId"));
            Label lblUDay = ((Label)gvr.FindControl("lblInCallAlowFromDay"));
            Label lblAppGroupId = ((Label)gvr.FindControl("lblAppGroupId"));

            int DtlId = 0, countSameDay = 0;
            try
            {
                if (ChkUpdateFieldsOk(ddlUFromHr, ddlUFromMin, ddlUToHour, ddlUToMin, txtUTotal))
                {
                    #region------ GetDayNumber From Day ------------
                    switch (lblUDay.Text.Trim().ToLower())
                    {
                        case "monday":
                            {
                                AlowFromDay = AlowToDay = 1;
                                break;
                            }
                        case "tuesday":
                            {
                                AlowFromDay = AlowToDay = 2;
                                break;
                            }
                        case "wednesday":
                            {
                                AlowFromDay = AlowToDay = 3;
                                break;
                            }
                        case "thursday":
                            {
                                AlowFromDay = AlowToDay = 4;
                                break;
                            }
                        case "friday":
                            {
                                AlowFromDay = AlowToDay = 5;
                                break;
                            }
                        case "saturday":
                            {
                                AlowFromDay = AlowToDay = 6;
                                break;
                            }
                        case "sunday":
                            {
                                AlowFromDay = AlowToDay = 7;
                                break;
                            }
                    }
                    #endregion
                    for (int idx = 0; idx < gdv.Rows.Count; idx++)
                    {
                        if (((Label)(gdv.Rows[idx].FindControl("lblDayNumber"))).Text.Trim() == AlowFromDay.ToString())
                            countSameDay++;

                    }
                    if (txtUTotal.Text.Trim() == "" || countSameDay == 1)
                    {
                        try
                        {
                            probal = new ProfileBAL();
                            probal.IsDayControlled = 1;

                            HdrId = Convert.ToInt32(lblHdrId.Text);
                            DtlId = Convert.ToInt32(lblUDtlID.Text);

                            FeatureId = (Convert.ToInt32(lblProfilePopId.Text));

                            if (ddlUFromHr.SelectedIndex > 0)
                            {
                                AlowFromTime = ddlUFromHr.SelectedItem.ToString() + ":" + ddlUFromMin.SelectedItem.ToString(); ;
                                AlowToTime = ddlUToHour.SelectedItem.ToString() + ":" + ddlUToMin.SelectedItem.ToString();
                                probal.IsTimeControlled = 1;
                            }
                            else
                            {
                                AlowFromTime = AlowToTime = "--:--";
                            }
                            try
                            {
                                TotalDuration = Convert.ToInt32(txtUTotal.Text);
                                probal.IsDurationControlled = 1;
                            }
                            catch (Exception) { }

                            probal.FeatureId = FeatureId;

                            probal.ProfileId = ProfileId;
                            probal.ClientId = ClientId;
                            probal.IsEnable = 1;
                            probal.LoggedBy = UserId.ToString();





                            probal.AlowFromDay = AlowFromDay;
                            probal.AlowFromTime = AlowFromTime;
                            probal.AlowToDay = AlowToDay;
                            probal.AlowToTime = AlowToTime;
                            probal.TotalDuration = TotalDuration;


                            //------------ Insert the Phone Settings ------------------                       
                            probal.ProfileFeatureMappingId = HdrId;
                            probal.ProfileFeatureTimingId = DtlId;

                            if (probal.IU_FeatureTimingDtl() > 0)
                            {
                                lblManFields.Text = "Updated successfully.";
                                lblManFields.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                lblManFields.Text = "Time intervals are overlapping the existing interval.";
                                lblManFields.ForeColor = System.Drawing.Color.Red;
                                //mp.Show();
                            }


                            gdv.EditIndex = -1;
                            BindTimingGrid();
                            ClearAllFields();
                            //mp.Show();
                        }
                        catch (Exception)
                        {
                            //mp.Show();
                        }
                    }
                    else
                    {
                        gdv.EditIndex = -1;
                        BindTimingGrid();
                        lblManFields.Text = "The settings not saved, because Total Duration is not allowed with multiple time slots.";
                        lblManFields.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    //mp.Show();
                }
            }
            catch (Exception)
            { }
            finally
            { }
            mp.Show();
        }
        protected void gdv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gdv.EditIndex = -1;
            BindTimingGrid();
            mp.Show();


        }
        protected void gdv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                gdv.EditIndex = -1;
                GridViewRow gvr = gdv.Rows[e.RowIndex];
                lblalertTimingid.Text = ((Label)gvr.FindControl("lblTimingId")).Text;
                lblalertfeatureid.Text = lblProfilePopId.Text;
                mp.Show();
                mpdelete.Show();
            }
            catch (Exception)
            {
                lblManFields.Text = "Deleted Not Successfully";
                lblManFields.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void GdvStngsDeleting(string lblDtlId, int CntrlTypeId, bool isForPreview)
        {
            try
            {
                probal = new ProfileBAL();
                probal.ProfileFeatureTimingId = Convert.ToInt32(lblDtlId);
                int res = probal.ChangeTempTimingStatus();
                if (res > 0)
                {
                    lblManFields.Text = "Deleted successfully.";
                    lblManFields.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblManFields.Text = "Deleted Not successfully.";
                    lblManFields.ForeColor = System.Drawing.Color.Red;
                }
                BindTimingGrid();
                mp.Show();
            }
            catch (Exception) { }
            finally
            {
                probal = null;

            }
        }
        protected bool ChkUpdateFieldsOk(DropDownList ddlUFromHr, DropDownList ddlUFromMin, DropDownList ddlUToHour, DropDownList ddlUToMin, TextBox txtUTotal)
        {
            bool isOk = false;

            string msg = "", suMsg = "";
            lblManFields.Text = "";



            if (msg == "")
            {
                suMsg = "";
                if (txtUTotal.Text.Trim() == "" && ddlUFromHr.SelectedIndex < 1 && ddlUFromMin.SelectedIndex < 1 && ddlUToHour.SelectedIndex < 1 && ddlUToMin.SelectedIndex < 1)
                {
                    suMsg = "<li>Either Select the From-To time or duration or both.</li>";
                    msg = msg + suMsg;
                }
                else
                {
                    if (ddlUFromHr.SelectedIndex > 0 || ddlUFromMin.SelectedIndex > 0 || ddlUToHour.SelectedIndex > 0 || ddlUToMin.SelectedIndex > 0)
                    {
                        suMsg = "";
                        if (!ChkFrmAndToTimeAreSetOkUpdate(ddlUFromHr, ddlUFromMin, ddlUToHour, ddlUToMin))
                        {
                            suMsg = "<li>Please Select From-To time pair carefully.</li>";
                            msg = msg + suMsg;
                        }
                    }
                }

                if (txtUTotal.Text.Trim() != "")
                {
                    if (ddlUFromHr.SelectedIndex > 0)
                    {
                        TimeSpan FrmTime = new TimeSpan(Convert.ToInt32(ddlUFromHr.SelectedValue.ToString()), Convert.ToInt32(ddlUFromMin.SelectedValue.ToString()), 0);
                        TimeSpan ToTime = new TimeSpan(Convert.ToInt32(ddlUToHour.SelectedValue.ToString()), Convert.ToInt32(ddlUToMin.SelectedValue.ToString()), 0);

                        int timedifference = Convert.ToInt32((ToTime - FrmTime).TotalMinutes);

                        if (Convert.ToInt32(txtUTotal.Text.Trim()) > timedifference && msg.IndexOf("Please Select From-To time pair") == -1)
                        {
                            msg = msg + "<li>Maximum possible Total Duration for selected time interval is " + timedifference + " minutes.</li>";
                        }
                    }
                    else if (Convert.ToInt32(txtUTotal.Text) > 1440)
                    {
                        isOk = false;
                        msg = msg + "<li>Total Duration should be less than or equal to 1440.</li>";
                    }
                }
            }

            if (msg.Trim() == "")
            {
                isOk = true;
            }
            else
            {
                isOk = false;
                lblManFields.Text = "<ol>" + msg + "</ol>";
                lblManFields.ForeColor = System.Drawing.Color.Red;
            }

            return isOk;
        }
        protected bool ChkFrmAndToTimeAreSetOkUpdate(DropDownList ddlUFromHr, DropDownList ddlUFromMin, DropDownList ddlUToHour, DropDownList ddlUToMin)
        {
            bool isOk = false;
            if (ddlUFromHr.SelectedIndex > 0 || ddlUFromMin.SelectedIndex > 0 || ddlUToHour.SelectedIndex > 0 || ddlUToMin.SelectedIndex > 0) //------ If one or more than on time fields are selected
            {
                isOk = false;
                if (ddlUFromHr.SelectedIndex > 0 && ddlUFromMin.SelectedIndex > 0 && ddlUToHour.SelectedIndex > 0 && ddlUToMin.SelectedIndex > 0) //------ if all time Fields are selected
                {
                    //--------- Now we chek that 'From Time' shouldn't be greater than or equal to 'To Time'

                    if (ddlUFromHr.SelectedIndex < ddlUToHour.SelectedIndex)
                    {
                        //---------------- When 'From Hour' is less than 'To hour'
                        isOk = true;
                    }
                    else if (ddlUFromHr.SelectedIndex == ddlUToHour.SelectedIndex)
                    {
                        //---------------- When 'From Hour' is equal to 'To hour'
                        if (ddlUFromMin.SelectedIndex < ddlUToMin.SelectedIndex)
                        {
                            isOk = true;
                        }
                    }
                    else
                    {
                        isOk = false; //------------------ When Some Time Fields are selected and some are not selected
                    }
                }
            }
            else
            {
                isOk = true;  //--------- When none of the Time field is selected -----------------
            }

            return isOk;
        }
        protected void ddlAppGroupMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTimingGrid();
            mp.Show();
        }
        protected void btnPhSave_Click(object sender, EventArgs e)
        {
            string listofMultipleTimeSlotsAndSlotswithDuration = "", MsgForAppgroup = "";
            try
            {
                if (chkAllMndatoryFieldsareOk())
                {
                    //  makeForSingleTimeSlots();  //---------- Only call this when u want disable multiple time slots
                    if (lblFeatureId.Text == "30")
                    {
                        if (ddlAppGroupMain.SelectedIndex > 0)
                        {
                            listofMultipleTimeSlotsAndSlotswithDuration = chkIsMultipleSlotsOccurForApp();
                        }
                        else
                        {
                            MsgForAppgroup = "Please select Application group.";
                        }
                    }
                    else
                    {
                        listofMultipleTimeSlotsAndSlotswithDuration = chkIsMultipleSlotsOccur();
                    }

                    if (txtTotal.Text.Trim() == "")
                    {
                        //  listofMultipleTimeSlotsAndSlotswithDuration = listofMultipleTimeSlotsAndSlotswithDuration.Substring(listofMultipleTimeSlotsAndSlotswithDuration.IndexOf(","));
                    }


                    if (listofMultipleTimeSlotsAndSlotswithDuration.Length == 0 && MsgForAppgroup == "") //-------- because ',' is occur by default between two lists
                    {
                        saveAction();
                    }
                    else
                    {
                        //------- Multiple slots problem  


                        lblMultipleSlotSlctnMSGDayNumber.Text = listofMultipleTimeSlotsAndSlotswithDuration;

                        #region----------- Replace day number by day name ---------------
                        listofMultipleTimeSlotsAndSlotswithDuration = listofMultipleTimeSlotsAndSlotswithDuration.Replace("1", "Monday");
                        listofMultipleTimeSlotsAndSlotswithDuration = listofMultipleTimeSlotsAndSlotswithDuration.Replace("2", "Tuesday");
                        listofMultipleTimeSlotsAndSlotswithDuration = listofMultipleTimeSlotsAndSlotswithDuration.Replace("3", "Wednesday");
                        listofMultipleTimeSlotsAndSlotswithDuration = listofMultipleTimeSlotsAndSlotswithDuration.Replace("4", "Thursday");
                        listofMultipleTimeSlotsAndSlotswithDuration = listofMultipleTimeSlotsAndSlotswithDuration.Replace("5", "Friday");
                        listofMultipleTimeSlotsAndSlotswithDuration = listofMultipleTimeSlotsAndSlotswithDuration.Replace("6", "Saturday");
                        listofMultipleTimeSlotsAndSlotswithDuration = listofMultipleTimeSlotsAndSlotswithDuration.Replace("7", "Sunday");
                        #endregion


                        if (listofMultipleTimeSlotsAndSlotswithDuration.Length == 0 && MsgForAppgroup != "")
                        {
                            lblMultipleSlotSlctnMSG.Text = MsgForAppgroup;
                        }
                        else if (listofMultipleTimeSlotsAndSlotswithDuration.Length != 0 && MsgForAppgroup == "")
                        {
                            lblMultipleSlotSlctnMSG.Text = "The settings of follwoing days not saved, because Total Duration is not allowed with multiple time slots :-<br>" +
                                                          "<b><i>" + listofMultipleTimeSlotsAndSlotswithDuration + "</i></b>";
                        }
                        else
                        {
                            lblMultipleSlotSlctnMSG.Text = "The settings of follwoing days not saved, because Total Duration is not allowed with multiple time slots :-<br>" +
                                                          "<b><i>" + listofMultipleTimeSlotsAndSlotswithDuration + "</i></b><br/>" + MsgForAppgroup + "";
                        }
                        //mpeMltpleSlotSlction.Show();
                        mp.Show();
                    }


                }
                else
                {
                    //------------- Show Mandatory Filds -----------------------
                    mp.Show();
                }
            }
            catch (Exception) { }
        }
        protected void saveAction()
        {
            probal = new ProfileBAL();
            try
            {
                TotalDuration = Convert.ToInt32(txtTotal.Text);
            }
            catch (Exception) { }

            probal.FeatureId = int.Parse(lblFeatureId.Text);

            try
            {
                HdrId = Convert.ToInt32(lblHdrId.Text);
            }
            catch (Exception) { }


            lblManFields.Text = "";

            foreach (ListItem itm in chkDay.Items)
            {
                if (itm.Selected && itm.Value.ToString().Trim() != "0")
                {
                    #region --------------------- Select Whether Time Span and duration are Controlled or not --------------------
                    if (ddlFromHour.SelectedIndex < 1)
                    {
                        probal.IsTimeControlled = 0;
                    }
                    else
                    {
                        probal.IsTimeControlled = 1;
                    }

                    if (txtTotal.Text.Trim() == "")
                    {
                        probal.IsDurationControlled = 0;
                    }
                    else
                    {
                        probal.IsDurationControlled = 1;
                    }
                    #endregion

                    if (ddlFromHour.SelectedIndex > 0)
                    {
                        AlowFromTime = ddlFromHour.SelectedItem.ToString() + ":" + ddlFromMin.SelectedItem.ToString(); ;
                        AlowToTime = ddlToHour.SelectedItem.ToString() + ":" + ddlToMin.SelectedItem.ToString();
                    }
                    else
                    {
                        AlowFromTime = AlowToTime = "--:--";
                    }

                    #region---------- When day controlled ---------------
                    probal.IsDayControlled = 1;
                    probal.ProfileId = ProfileId;
                    probal.ClientId = ClientId;
                    probal.IsChanged = 1;
                    probal.LoggedBy = UserId.ToString();

                    AlowFromDay = AlowToDay = Convert.ToInt32(itm.Value.ToString());

                    probal.AlowFromDay = AlowFromDay;
                    probal.AlowFromTime = AlowFromTime;
                    probal.AlowToDay = AlowToDay;
                    probal.AlowToTime = AlowToTime;
                    probal.TotalDuration = TotalDuration;

                    //if (CntrlTypeId <= 4)
                    //{
                    //------------ Insert the Phone Settings ------------------
                    probal.FeatureId = int.Parse(lblFeatureId.Text);

                    if (HdrId <= 0)
                    {
                        HdrId = probal.IU_ProfileFeatureMapping();
                        lblHdrId.Text = HdrId.ToString();
                    }
                    probal.ProfileFeatureMappingId = HdrId;
                    if (lblFeatureId.Text == "30")
                    {
                        probal.GroupId = Convert.ToInt32((ddlAppGroupMain.SelectedValue.ToString()));
                    }
                    int result = probal.IU_FeatureTimingDtl();

                    if (result <= 0)
                    {
                        lblManFields.Text = lblManFields.Text + "<br>"
                                         + itm.Text.ToString() + " " + AlowFromTime + " " + AlowToTime + " " + TotalDuration;
                        lblManFields.ForeColor = System.Drawing.Color.Red;
                    }
                    //}
                    //else
                    //{
                    //    probal.PrmisnHdrId = HdrId;

                    //    if (HdrId <= 0)
                    //    {
                    //        HdrId = probal.IU_PermissionHdr();
                    //        probal.PrmisnHdrId = HdrId;
                    //    }

                    //    int result = probal.IU_PermissionDtl();

                    //    if (result <= 0)
                    //    {
                    //        lblManFields.Text = lblManFields.Text + "<br>"
                    //                          + itm.Text.ToString() + " " + AlowFromTime + " " + AlowToTime + " " + TotalDuration;
                    //    }

                    //}
                    #endregion



                }
            }

            if (lblManFields.Text.Trim() != "")
            {
                lblManFields.Text = "Following Time intervals are not valid, because they are overlapping the existing intervals. <br>" + lblManFields.Text;
                lblManFields.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                lblManFields.Text = "Schedule added Succesfully";
                lblManFields.ForeColor = System.Drawing.Color.Green;
            }

            BindTimingGrid();
            mp.Show();
            ClearAllFields();

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

        }
        protected bool chkAllMndatoryFieldsareOk()
        {
            bool isOk = true;
            string msg = "", suMsg = "";
            lblManFields.Text = "";

            if (msg.Trim() == "")
            {
                suMsg = "<li>Please select day.</li>";
                foreach (ListItem itm in chkDay.Items)
                {
                    if (itm.Selected == true)
                    {
                        suMsg = "";
                    }
                }

                //if (suMsg == "")
                //{
                //    chkDay.Enabled = true;
                //    ddlFromHour.Enabled = true;
                //    ddlFromMin.Enabled = true;
                //    ddlToHour.Enabled = true;
                //    ddlToMin.Enabled = true;
                //    txtTotal.Enabled = true;
                //}
                //else
                //{
                //    ddlFromHour.Enabled = false;
                //    ddlFromMin.Enabled = false;
                //    ddlToHour.Enabled = false;
                //    ddlToMin.Enabled = false;
                //    txtTotal.Enabled = false;
                //}

                msg = msg + suMsg;
            }

            if (msg.Trim() == "")
            {
                suMsg = "";
                if (txtTotal.Text.Trim() == "" && ddlFromHour.SelectedIndex < 1 && ddlFromMin.SelectedIndex < 1 && ddlToHour.SelectedIndex < 1 && ddlToMin.SelectedIndex < 1)
                {
                    suMsg = "<li>Either Select the From-To time or duration or both.</li>";
                    msg = msg + suMsg;
                }
                else
                {
                    if (ddlFromHour.SelectedIndex > 0 || ddlFromMin.SelectedIndex > 0 || ddlToHour.SelectedIndex > 0 || ddlToMin.SelectedIndex > 0)
                    {
                        suMsg = "";
                        if (!ChkFrmAndToTimeAreSetOk())
                        {
                            suMsg = "<li>Please Select From-To time pair carefully.</li>";
                            msg = msg + suMsg;
                        }
                    }
                }

                if (txtTotal.Text.Trim() != "")
                {
                    if (ddlFromHour.SelectedIndex > 0)
                    {
                        TimeSpan FrmTime = new TimeSpan(Convert.ToInt32(ddlFromHour.SelectedValue.ToString()), Convert.ToInt32(ddlFromMin.SelectedValue.ToString()), 0);
                        TimeSpan ToTime = new TimeSpan(Convert.ToInt32(ddlToHour.SelectedValue.ToString()), Convert.ToInt32(ddlToMin.SelectedValue.ToString()), 0);

                        int timedifference = Convert.ToInt32((ToTime - FrmTime).TotalMinutes);

                        if (Convert.ToInt32(txtTotal.Text.Trim()) > timedifference && msg.IndexOf("Please Select From-To time pair carefully") == -1)
                        {
                            msg = msg + "<li>Maximum possible Total Duration for selected time interval is " + timedifference + " minutes.</li>";
                        }
                    }
                    else if (Convert.ToInt32(txtTotal.Text) > 1440)
                    {
                        msg = msg + "<li>Total Duration should be less than or equal to 1440.</li>";
                    }
                }
            }


            if (msg.Trim() == "")
            {
                isOk = true;
            }
            else
            {
                isOk = false;
                lblManFields.Text = "<ol type=\"1\">" + msg + "</ol>";
                lblManFields.ForeColor = System.Drawing.Color.Red;
            }

            return isOk;
        }
        protected bool ChkFrmAndToTimeAreSetOk()
        {
            bool isOk = false;
            if (ddlFromHour.SelectedIndex > 0 || ddlFromMin.SelectedIndex > 0 || ddlToHour.SelectedIndex > 0 || ddlToMin.SelectedIndex > 0) //------ If one or more than on time fields are selected
            {
                isOk = false;
                if (ddlFromHour.SelectedIndex > 0 && ddlFromMin.SelectedIndex > 0 && ddlToHour.SelectedIndex > 0 && ddlToMin.SelectedIndex > 0) //------ if all time Fields are selected
                {
                    //--------- Now we chek that 'From Time' shouldn't be greater than or equal to 'To Time'

                    if (ddlFromHour.SelectedIndex < ddlToHour.SelectedIndex)
                    {
                        //---------------- When 'From Hour' is less than 'To hour'
                        isOk = true;
                    }
                    else if (ddlFromHour.SelectedIndex == ddlToHour.SelectedIndex)
                    {
                        //---------------- When 'From Hour' is equal to 'To hour'
                        if (ddlFromMin.SelectedIndex < ddlToMin.SelectedIndex)
                        {
                            isOk = true;
                        }
                    }
                    else
                    {
                        isOk = false; //------------------ When Some Time Fields are selected and some are not selected
                    }
                }
            }
            else
            {
                isOk = true;  //--------- When none of the Time field is selected -----------------
            }
            return isOk;
        }
        protected string chkIsMultipleSlotsOccur()
        {


            string dayListForClrStngDueToDuration = "";



            int countforDay1 = 0;
            int countforDay2 = 0;
            int countforDay3 = 0;
            int countforDay4 = 0;
            int countforDay5 = 0;
            int countforDay6 = 0;
            int countforDay7 = 0;


            int StngOnlyFrmToTime_countforDay1 = 0;
            int StngOnlyFrmToTime_countforDay2 = 0;
            int StngOnlyFrmToTime_countforDay3 = 0;
            int StngOnlyFrmToTime_countforDay4 = 0;
            int StngOnlyFrmToTime_countforDay5 = 0;
            int StngOnlyFrmToTime_countforDay6 = 0;
            int StngOnlyFrmToTime_countforDay7 = 0;


            int StngHavngBothTimeAndDuration_countforDay1 = 0;
            int StngHavngBothTimeAndDuration_countforDay2 = 0;
            int StngHavngBothTimeAndDuration_countforDay3 = 0;
            int StngHavngBothTimeAndDuration_countforDay4 = 0;
            int StngHavngBothTimeAndDuration_countforDay5 = 0;
            int StngHavngBothTimeAndDuration_countforDay6 = 0;
            int StngHavngBothTimeAndDuration_countforDay7 = 0;



            int StngOnlyDuration_countforDay1 = 0;
            int StngOnlyDuration_countforDay2 = 0;
            int StngOnlyDuration_countforDay3 = 0;
            int StngOnlyDuration_countforDay4 = 0;
            int StngOnlyDuration_countforDay5 = 0;
            int StngOnlyDuration_countforDay6 = 0;
            int StngOnlyDuration_countforDay7 = 0;

            #region------------- Count number of slots day wise ---------------------
            try
            {
                for (int idx = 0; idx < gdv.Rows.Count; idx++)
                {
                    #region------------------- Slots counts / day ---------------
                    switch (((Label)(gdv.Rows[idx].FindControl("lblDayNumber"))).Text.Trim())
                    {
                        case "1":
                            {
                                countforDay1 = countforDay1 + 1;
                                break;
                            }
                        case "2":
                            {
                                countforDay2 = countforDay2 + 1;
                                break;
                            }
                        case "3":
                            {
                                countforDay3 = countforDay3 + 1;
                                break;
                            }
                        case "4":
                            {
                                countforDay4 = countforDay4 + 1;
                                break;
                            }
                        case "5":
                            {
                                countforDay5 = countforDay5 + 1;
                                break;
                            }
                        case "6":
                            {
                                countforDay6 = countforDay6 + 1;
                                break;
                            }
                        case "7":
                            {
                                countforDay7 = countforDay7 + 1;
                                break;
                            }
                    }
                    #endregion

                    if (((Label)(gdv.Rows[idx].FindControl("lblInCallTotalDuration"))).Text.Trim() != "--" && ((Label)(gdv.Rows[idx].FindControl("lblInCallAlowFromTime"))).Text.Trim() != "--:--")
                    {
                        #region------------------- Slots counts having both From To time and Duration / day ---------------
                        switch (((Label)(gdv.Rows[idx].FindControl("lblDayNumber"))).Text.Trim())
                        {
                            case "1":
                                {
                                    StngHavngBothTimeAndDuration_countforDay1 = StngHavngBothTimeAndDuration_countforDay1 + 1;
                                    break;
                                }
                            case "2":
                                {
                                    StngHavngBothTimeAndDuration_countforDay2 = StngHavngBothTimeAndDuration_countforDay2 + 1;
                                    break;
                                }
                            case "3":
                                {
                                    StngHavngBothTimeAndDuration_countforDay3 = StngHavngBothTimeAndDuration_countforDay3 + 1;
                                    break;
                                }
                            case "4":
                                {
                                    StngHavngBothTimeAndDuration_countforDay4 = StngHavngBothTimeAndDuration_countforDay4 + 1;
                                    break;
                                }
                            case "5":
                                {
                                    StngHavngBothTimeAndDuration_countforDay5 = StngHavngBothTimeAndDuration_countforDay5 + 1;
                                    break;
                                }
                            case "6":
                                {
                                    StngHavngBothTimeAndDuration_countforDay6 = StngHavngBothTimeAndDuration_countforDay6 + 1;
                                    break;
                                }
                            case "7":
                                {
                                    StngHavngBothTimeAndDuration_countforDay7 = StngHavngBothTimeAndDuration_countforDay7 + 1;
                                    break;
                                }
                        }
                        #endregion
                    }

                    if (((Label)(gdv.Rows[idx].FindControl("lblInCallTotalDuration"))).Text.Trim() != "--" && ((Label)(gdv.Rows[idx].FindControl("lblInCallAlowFromTime"))).Text.Trim() == "--:--")
                    {
                        #region------------------- Slots counts with only durations / day ---------------
                        switch (((Label)(gdv.Rows[idx].FindControl("lblDayNumber"))).Text.Trim())
                        {
                            case "1":
                                {
                                    StngOnlyDuration_countforDay1 = StngOnlyDuration_countforDay1 + 1;
                                    break;
                                }
                            case "2":
                                {
                                    StngOnlyDuration_countforDay2 = StngOnlyDuration_countforDay2 + 1;
                                    break;
                                }
                            case "3":
                                {
                                    StngOnlyDuration_countforDay3 = StngOnlyDuration_countforDay3 + 1;
                                    break;
                                }
                            case "4":
                                {
                                    StngOnlyDuration_countforDay4 = StngOnlyDuration_countforDay4 + 1;
                                    break;
                                }
                            case "5":
                                {
                                    StngOnlyDuration_countforDay5 = StngOnlyDuration_countforDay5 + 1;
                                    break;
                                }
                            case "6":
                                {
                                    StngOnlyDuration_countforDay6 = StngOnlyDuration_countforDay6 + 1;
                                    break;
                                }
                            case "7":
                                {
                                    StngOnlyDuration_countforDay7 = StngOnlyDuration_countforDay7 + 1;
                                    break;
                                }
                        }
                        #endregion
                    }

                    if (((Label)(gdv.Rows[idx].FindControl("lblInCallTotalDuration"))).Text.Trim() == "--" && ((Label)(gdv.Rows[idx].FindControl("lblInCallAlowFromTime"))).Text.Trim() != "--:--")
                    {
                        #region------------------- Slots counts with only From To Time / day ---------------
                        switch (((Label)(gdv.Rows[idx].FindControl("lblDayNumber"))).Text.Trim())
                        {
                            case "1":
                                {
                                    StngOnlyFrmToTime_countforDay1 = StngOnlyFrmToTime_countforDay1 + 1;
                                    break;
                                }
                            case "2":
                                {
                                    StngOnlyFrmToTime_countforDay2 = StngOnlyFrmToTime_countforDay2 + 1;
                                    break;
                                }
                            case "3":
                                {
                                    StngOnlyFrmToTime_countforDay3 = StngOnlyFrmToTime_countforDay3 + 1;
                                    break;
                                }
                            case "4":
                                {
                                    StngOnlyFrmToTime_countforDay4 = StngOnlyFrmToTime_countforDay4 + 1;
                                    break;
                                }
                            case "5":
                                {
                                    StngOnlyFrmToTime_countforDay5 = StngOnlyFrmToTime_countforDay5 + 1;
                                    break;
                                }
                            case "6":
                                {
                                    StngOnlyFrmToTime_countforDay6 = StngOnlyFrmToTime_countforDay6 + 1;
                                    break;
                                }
                            case "7":
                                {
                                    StngOnlyFrmToTime_countforDay7 = StngOnlyFrmToTime_countforDay7 + 1;
                                    break;
                                }
                        }
                        #endregion
                    }

                }
            }
            catch (Exception) { }
            #endregion

            #region--------- Chk For only those days which are currently selected --------------
            for (int idx = 0; idx < chkDay.Items.Count; idx++)
            {
                if (chkDay.Items[idx].Selected == false)
                {
                    if (idx == 0)
                    {
                        countforDay1 = -1;
                    }
                    else if (idx == 1)
                    {
                        countforDay2 = -1;
                    }
                    else if (idx == 2)
                    {
                        countforDay3 = -1;
                    }
                    else if (idx == 3)
                    {
                        countforDay4 = -1;
                    }
                    else if (idx == 4)
                    {
                        countforDay5 = -1;
                    }
                    else if (idx == 5)
                    {
                        countforDay6 = -1;
                    }
                    else if (idx == 6)
                    {
                        countforDay7 = -1;
                    }
                }
            }
            #endregion



            #region-----Find the list of days which have duration fields in their previous settings and we should clear out them before we are going to insert/update new entries for this --------------

            /*
            if ((countforDay1 > 0 || countforDay2 > 0 || countforDay3 > 0 || countforDay4 > 0 || countforDay5 > 0 || countforDay6 > 0 || countforDay7 > 0))
            {
                if (countforDay1 > 0 && StngHavngBothTimeAndDuration_countforDay1 > 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "1";
                }
                else if (countforDay1 > 0)
                {
                    dayListWhichSomeStngbutWithoutDuration = dayListWhichSomeStngbutWithoutDuration + "1";
                }

                if (countforDay2 > 0 && StngHavngBothTimeAndDuration_countforDay2 > 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "2";
                }
                else if (countforDay2 > 0)
                {
                    dayListWhichSomeStngbutWithoutDuration = dayListWhichSomeStngbutWithoutDuration + "2";
                }

                if (countforDay3 > 0 && StngHavngBothTimeAndDuration_countforDay3 > 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "3";
                }
                else if (countforDay3 > 0)
                {
                    dayListWhichSomeStngbutWithoutDuration = dayListWhichSomeStngbutWithoutDuration + "3";
                }

                if (countforDay4 > 0 && StngHavngBothTimeAndDuration_countforDay4 > 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "4";
                }
                else if (countforDay4 > 0)
                {
                    dayListWhichSomeStngbutWithoutDuration = dayListWhichSomeStngbutWithoutDuration + "4";
                }

                if (countforDay5 > 0 && StngHavngBothTimeAndDuration_countforDay5 > 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "5";
                }
                else if (countforDay5 > 0)
                {
                    dayListWhichSomeStngbutWithoutDuration = dayListWhichSomeStngbutWithoutDuration + "5";
                }

                if (countforDay6 > 0 && StngHavngBothTimeAndDuration_countforDay6 > 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "6";
                }
                else if (countforDay6 > 0)
                {
                    dayListWhichSomeStngbutWithoutDuration = dayListWhichSomeStngbutWithoutDuration + "6";
                }

                if (countforDay7 > 0 && StngHavngBothTimeAndDuration_countforDay7 > 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "7";
                }
                else if (countforDay7 > 0)
                {
                    dayListWhichSomeStngbutWithoutDuration = dayListWhichSomeStngbutWithoutDuration + "7";
                }
            }
             * 
             * */
            #endregion


            #region---------- Now we have check those days whoes settings are override due to duration ------------

            dayListForClrStngDueToDuration = "";

            #region----------------------- When Input = Only Duration ----------------------
            if (ddlFromHour.SelectedIndex <= 0 && txtTotal.Text.Trim() != "")
            {
                if (countforDay1 > 0 && StngOnlyDuration_countforDay1 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "1";
                }
                if (countforDay2 > 0 && StngOnlyDuration_countforDay2 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "2";
                }
                if (countforDay3 > 0 && StngOnlyDuration_countforDay3 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "3";
                }
                if (countforDay4 > 0 && StngOnlyDuration_countforDay4 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "4";
                }
                if (countforDay5 > 0 && StngOnlyDuration_countforDay5 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "5";
                }
                if (countforDay6 > 0 && StngOnlyDuration_countforDay6 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "6";
                }
                if (countforDay7 > 0 && StngOnlyDuration_countforDay7 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "7";
                }
            }
            #endregion

            #region-------- When Input = only From-To Time --------------
            if (ddlFromHour.SelectedIndex > 0 && txtTotal.Text.Trim() == "")
            {
                if (countforDay1 > 0 && StngOnlyFrmToTime_countforDay1 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "1";
                }
                if (countforDay2 > 0 && StngOnlyFrmToTime_countforDay2 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "2";
                }
                if (countforDay3 > 0 && StngOnlyFrmToTime_countforDay3 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "3";
                }
                if (countforDay4 > 0 && StngOnlyFrmToTime_countforDay4 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "4";
                }
                if (countforDay5 > 0 && StngOnlyFrmToTime_countforDay5 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "5";
                }
                if (countforDay6 > 0 && StngOnlyFrmToTime_countforDay6 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "6";
                }
                if (countforDay7 > 0 && StngOnlyFrmToTime_countforDay7 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "7";
                }
            }
            #endregion

            #region----------- When Input = Both From-To Time & Duration ---------
            if (ddlFromHour.SelectedIndex > 0 && txtTotal.Text.Trim() != "")
            {
                if (countforDay1 > 0 && StngHavngBothTimeAndDuration_countforDay1 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "1";
                }
                if (countforDay2 > 0 && StngHavngBothTimeAndDuration_countforDay2 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "2";
                }
                if (countforDay3 > 0 && StngHavngBothTimeAndDuration_countforDay3 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "3";
                }
                if (countforDay4 > 0 && StngHavngBothTimeAndDuration_countforDay4 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "4";
                }
                if (countforDay5 > 0 && StngHavngBothTimeAndDuration_countforDay5 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "5";
                }
                if (countforDay6 > 0 && StngHavngBothTimeAndDuration_countforDay6 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "6";
                }
                if (countforDay7 > 0 && StngHavngBothTimeAndDuration_countforDay7 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "7";
                }
            }
            #endregion

            #endregion

            dayListForClrStngDueToDuration = String.Join<char>(", ", dayListForClrStngDueToDuration);

            return dayListForClrStngDueToDuration;

        }
        protected string chkIsMultipleSlotsOccurForApp()
        {


            string dayListForClrStngDueToDuration = "";



            int countforDay1 = 0;
            int countforDay2 = 0;
            int countforDay3 = 0;
            int countforDay4 = 0;
            int countforDay5 = 0;
            int countforDay6 = 0;
            int countforDay7 = 0;


            int StngOnlyFrmToTime_countforDay1 = 0;
            int StngOnlyFrmToTime_countforDay2 = 0;
            int StngOnlyFrmToTime_countforDay3 = 0;
            int StngOnlyFrmToTime_countforDay4 = 0;
            int StngOnlyFrmToTime_countforDay5 = 0;
            int StngOnlyFrmToTime_countforDay6 = 0;
            int StngOnlyFrmToTime_countforDay7 = 0;


            int StngHavngBothTimeAndDuration_countforDay1 = 0;
            int StngHavngBothTimeAndDuration_countforDay2 = 0;
            int StngHavngBothTimeAndDuration_countforDay3 = 0;
            int StngHavngBothTimeAndDuration_countforDay4 = 0;
            int StngHavngBothTimeAndDuration_countforDay5 = 0;
            int StngHavngBothTimeAndDuration_countforDay6 = 0;
            int StngHavngBothTimeAndDuration_countforDay7 = 0;



            int StngOnlyDuration_countforDay1 = 0;
            int StngOnlyDuration_countforDay2 = 0;
            int StngOnlyDuration_countforDay3 = 0;
            int StngOnlyDuration_countforDay4 = 0;
            int StngOnlyDuration_countforDay5 = 0;
            int StngOnlyDuration_countforDay6 = 0;
            int StngOnlyDuration_countforDay7 = 0;

            #region------------- Count number of slots day wise ---------------------
            try
            {
                for (int idx = 0; idx < gdv.Rows.Count; idx++)
                {
                    if (ddlAppGroupMain.SelectedValue == ((Label)(gdv.Rows[idx].FindControl("lblInCallTotalDuration"))).Text.Trim())
                    {
                        #region------------------- Slots counts / day ---------------
                        switch (((Label)(gdv.Rows[idx].FindControl("lblDayNumber"))).Text.Trim())
                        {
                            case "1":
                                {
                                    countforDay1 = countforDay1 + 1;
                                    break;
                                }
                            case "2":
                                {
                                    countforDay2 = countforDay2 + 1;
                                    break;
                                }
                            case "3":
                                {
                                    countforDay3 = countforDay3 + 1;
                                    break;
                                }
                            case "4":
                                {
                                    countforDay4 = countforDay4 + 1;
                                    break;
                                }
                            case "5":
                                {
                                    countforDay5 = countforDay5 + 1;
                                    break;
                                }
                            case "6":
                                {
                                    countforDay6 = countforDay6 + 1;
                                    break;
                                }
                            case "7":
                                {
                                    countforDay7 = countforDay7 + 1;
                                    break;
                                }
                        }
                        #endregion

                        if (((Label)(gdv.Rows[idx].FindControl("lblInCallTotalDuration"))).Text.Trim() != "--")
                        {
                            #region------------------- Slots counts having both From To time and Duration / day ---------------
                            switch (((Label)(gdv.Rows[idx].FindControl("lblDayNumber"))).Text.Trim())
                            {
                                case "1":
                                    {
                                        StngHavngBothTimeAndDuration_countforDay1 = StngHavngBothTimeAndDuration_countforDay1 + 1;
                                        break;
                                    }
                                case "2":
                                    {
                                        StngHavngBothTimeAndDuration_countforDay2 = StngHavngBothTimeAndDuration_countforDay2 + 1;
                                        break;
                                    }
                                case "3":
                                    {
                                        StngHavngBothTimeAndDuration_countforDay3 = StngHavngBothTimeAndDuration_countforDay3 + 1;
                                        break;
                                    }
                                case "4":
                                    {
                                        StngHavngBothTimeAndDuration_countforDay4 = StngHavngBothTimeAndDuration_countforDay4 + 1;
                                        break;
                                    }
                                case "5":
                                    {
                                        StngHavngBothTimeAndDuration_countforDay5 = StngHavngBothTimeAndDuration_countforDay5 + 1;
                                        break;
                                    }
                                case "6":
                                    {
                                        StngHavngBothTimeAndDuration_countforDay6 = StngHavngBothTimeAndDuration_countforDay6 + 1;
                                        break;
                                    }
                                case "7":
                                    {
                                        StngHavngBothTimeAndDuration_countforDay7 = StngHavngBothTimeAndDuration_countforDay7 + 1;
                                        break;
                                    }
                            }
                            #endregion
                        }

                        if (((Label)(gdv.Rows[idx].FindControl("lblInCallTotalDuration"))).Text.Trim() != "--" && ((Label)(gdv.Rows[idx].FindControl("lblInCallAlowFromTime"))).Text.Trim() == "--:--")
                        {
                            #region------------------- Slots counts with only durations / day ---------------
                            switch (((Label)(gdv.Rows[idx].FindControl("lblDayNumber"))).Text.Trim())
                            {
                                case "1":
                                    {
                                        StngOnlyDuration_countforDay1 = StngOnlyDuration_countforDay1 + 1;
                                        break;
                                    }
                                case "2":
                                    {
                                        StngOnlyDuration_countforDay2 = StngOnlyDuration_countforDay2 + 1;
                                        break;
                                    }
                                case "3":
                                    {
                                        StngOnlyDuration_countforDay3 = StngOnlyDuration_countforDay3 + 1;
                                        break;
                                    }
                                case "4":
                                    {
                                        StngOnlyDuration_countforDay4 = StngOnlyDuration_countforDay4 + 1;
                                        break;
                                    }
                                case "5":
                                    {
                                        StngOnlyDuration_countforDay5 = StngOnlyDuration_countforDay5 + 1;
                                        break;
                                    }
                                case "6":
                                    {
                                        StngOnlyDuration_countforDay6 = StngOnlyDuration_countforDay6 + 1;
                                        break;
                                    }
                                case "7":
                                    {
                                        StngOnlyDuration_countforDay7 = StngOnlyDuration_countforDay7 + 1;
                                        break;
                                    }
                            }
                            #endregion
                        }

                        if (((Label)(gdv.Rows[idx].FindControl("lblInCallTotalDuration"))).Text.Trim() == "--" && ((Label)(gdv.Rows[idx].FindControl("lblInCallAlowFromTime"))).Text.Trim() != "--:--")
                        {
                            #region------------------- Slots counts with only From To Time / day ---------------
                            switch (((Label)(gdv.Rows[idx].FindControl("lblDayNumber"))).Text.Trim())
                            {
                                case "1":
                                    {
                                        StngOnlyFrmToTime_countforDay1 = StngOnlyFrmToTime_countforDay1 + 1;
                                        break;
                                    }
                                case "2":
                                    {
                                        StngOnlyFrmToTime_countforDay2 = StngOnlyFrmToTime_countforDay2 + 1;
                                        break;
                                    }
                                case "3":
                                    {
                                        StngOnlyFrmToTime_countforDay3 = StngOnlyFrmToTime_countforDay3 + 1;
                                        break;
                                    }
                                case "4":
                                    {
                                        StngOnlyFrmToTime_countforDay4 = StngOnlyFrmToTime_countforDay4 + 1;
                                        break;
                                    }
                                case "5":
                                    {
                                        StngOnlyFrmToTime_countforDay5 = StngOnlyFrmToTime_countforDay5 + 1;
                                        break;
                                    }
                                case "6":
                                    {
                                        StngOnlyFrmToTime_countforDay6 = StngOnlyFrmToTime_countforDay6 + 1;
                                        break;
                                    }
                                case "7":
                                    {
                                        StngOnlyFrmToTime_countforDay7 = StngOnlyFrmToTime_countforDay7 + 1;
                                        break;
                                    }
                            }
                            #endregion
                        }

                    }
                }
            }
            catch (Exception) { }
            #endregion

            #region--------- Chkl For only those days which are currently selected --------------
            for (int idx = 0; idx < chkDay.Items.Count; idx++)
            {
                if (chkDay.Items[idx].Selected == false)
                {
                    if (idx == 0)
                    {
                        countforDay1 = -1;
                    }
                    else if (idx == 1)
                    {
                        countforDay2 = -1;
                    }
                    else if (idx == 2)
                    {
                        countforDay3 = -1;
                    }
                    else if (idx == 3)
                    {
                        countforDay4 = -1;
                    }
                    else if (idx == 4)
                    {
                        countforDay5 = -1;
                    }
                    else if (idx == 5)
                    {
                        countforDay6 = -1;
                    }
                    else if (idx == 6)
                    {
                        countforDay7 = -1;
                    }
                }
            }
            #endregion
            #region---------- Now we have check those days whoes settings are override due to duration ------------

            dayListForClrStngDueToDuration = "";

            #region----------------------- When Input = Only Duration ----------------------
            if (ddlFromHour.SelectedIndex <= 0 && txtTotal.Text.Trim() != "")
            {
                if (countforDay1 > 0 && StngOnlyDuration_countforDay1 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "1";
                }
                if (countforDay2 > 0 && StngOnlyDuration_countforDay2 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "2";
                }
                if (countforDay3 > 0 && StngOnlyDuration_countforDay3 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "3";
                }
                if (countforDay4 > 0 && StngOnlyDuration_countforDay4 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "4";
                }
                if (countforDay5 > 0 && StngOnlyDuration_countforDay5 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "5";
                }
                if (countforDay6 > 0 && StngOnlyDuration_countforDay6 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "6";
                }
                if (countforDay7 > 0 && StngOnlyDuration_countforDay7 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "7";
                }
            }
            #endregion

            #region-------- When Input = only From-To Time --------------
            if (ddlFromHour.SelectedIndex > 0 && txtTotal.Text.Trim() == "")
            {
                if (countforDay1 > 0 && StngOnlyFrmToTime_countforDay1 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "1";
                }
                if (countforDay2 > 0 && StngOnlyFrmToTime_countforDay2 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "2";
                }
                if (countforDay3 > 0 && StngOnlyFrmToTime_countforDay3 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "3";
                }
                if (countforDay4 > 0 && StngOnlyFrmToTime_countforDay4 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "4";
                }
                if (countforDay5 > 0 && StngOnlyFrmToTime_countforDay5 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "5";
                }
                if (countforDay6 > 0 && StngOnlyFrmToTime_countforDay6 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "6";
                }
                if (countforDay7 > 0 && StngOnlyFrmToTime_countforDay7 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "7";
                }
            }
            #endregion

            #region----------- When Input = Both From-To Time & Duration ---------
            if (ddlFromHour.SelectedIndex > 0 && txtTotal.Text.Trim() != "")
            {
                if (countforDay1 > 0 && StngHavngBothTimeAndDuration_countforDay1 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "1";
                }
                if (countforDay2 > 0 && StngHavngBothTimeAndDuration_countforDay2 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "2";
                }
                if (countforDay3 > 0 && StngHavngBothTimeAndDuration_countforDay3 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "3";
                }
                if (countforDay4 > 0 && StngHavngBothTimeAndDuration_countforDay4 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "4";
                }
                if (countforDay5 > 0 && StngHavngBothTimeAndDuration_countforDay5 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "5";
                }
                if (countforDay6 > 0 && StngHavngBothTimeAndDuration_countforDay6 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "6";
                }
                if (countforDay7 > 0 && StngHavngBothTimeAndDuration_countforDay7 <= 0)
                {
                    dayListForClrStngDueToDuration = dayListForClrStngDueToDuration + "7";
                }
            }
            #endregion

            #endregion

            dayListForClrStngDueToDuration = String.Join<char>(", ", dayListForClrStngDueToDuration);

            return dayListForClrStngDueToDuration;

        }
        protected void ClearAllFields()
        {
            ddlFromMin.SelectedIndex = 0;
            ddlFromHour.SelectedIndex = 0;
            ddlToHour.SelectedIndex = 0;
            ddlToMin.SelectedIndex = 0;
            txtTotal.Text = "";

            txtAddKeywordCode.Text = "";
            txtAddKeywordName.Text = "";
            txtAddDescription.Text = "";
            txtAddMobileNo.Text = "";
            txtCallName.Text = "";
            txtAddNo.Text = "";
            txtsmsname.Text = "";
            txtMobileNo.Text = "";
            foreach (ListItem itm in chkDay.Items)
            {
                itm.Selected = false;
            }
            cball.Checked = false;
            cbWeekdays.Checked = false;
            cbWeekEnd.Checked = false;
            chkIncoming.Checked = false;
            chkOutgoing.Checked = false;
            chkSms.Checked = false;
            //SetEnabilityOfFromAndToTime(false);

            // txtTotal.Enabled = false;

        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Msg;
            probal = new ProfileBAL();
            dtMsg = new DataTable();
            try
            {
                probal.ProfileFeatureMappingId = int.Parse(lblHdrId.Text.Trim());
            }
            catch (Exception) { probal.ProfileFeatureMappingId = 0; }
            dtMsg = probal.GetPhAndPrmsStngChangeForSMSAndEmail();
            Msg = makeStngSMSForPhSettings(dtMsg);


            if (Msg == "0")
            {
                Msg = null;
            }
            UpdateMsg(Msg, probal.ProfileFeatureMappingId);
            ClearAllFields();
            // ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + lblCategoryIdMp.Text + "','one');</script>");
            //}
            //else
            //{
            //    mp.Hide();
            //}
        }
        protected string makeStngSMSForPhSettingsApp(DataTable myStngDT)
        {
            string myStngCmds = "";
            string FnlStngCmd = "";

            int PrvsCntrlTypeId = 0;
            int CntrlTypeId = 0;// int.Parse(lblFeatureId.Text);
            string myCntrolTypeName = "";
            string AlowFromDay = "";
            string Stngs = "";


            for (int idx = 0; idx < myStngDT.Rows.Count; idx++)
            {
                AlowFromDay = myStngDT.Rows[idx]["AlowFromDay"].ToString();
                Stngs = myStngDT.Rows[idx]["Stngs"].ToString();
                CntrlTypeId = Convert.ToInt32(myStngDT.Rows[idx]["GroupId"].ToString());
                Stngs = Stngs.Replace(":", "");
                if (idx != 0 && myStngCmds != "" && PrvsCntrlTypeId == CntrlTypeId)
                {
                    if (PrvsCntrlTypeId == CntrlTypeId)
                    {
                        myStngCmds = myStngCmds + " D" + AlowFromDay + Stngs;
                    }
                }
                else
                {
                    myCntrolTypeName = "CA1";
                    myStngCmds = myStngCmds + " " + myCntrolTypeName + " " + CntrlTypeId + " D" + AlowFromDay + Stngs;
                }
                PrvsCntrlTypeId = CntrlTypeId;


                //if (myStngCmds.Length > 145)
                //{
                //    FnlStngCmd = "GBox set as " + FnlStngCmd;

                //    sendChangesToPhbySMS(StuMob, FnlStngCmd, College_Id);
                //    SendStngChnageMail(FnlStngCmd, FnlStngCmd);

                //    myStngCmds = "";
                //    FnlStngCmd = "";

                //    if (idx != 0)
                //    {
                //        idx = idx - 1; //------------ Reverse the loop 1 step only
                //    }
                //}
                //else
                //{
                FnlStngCmd = myStngCmds;
                //}

            }

            if (FnlStngCmd.Trim() != "")
            {
                FnlStngCmd = "GBox set as" + FnlStngCmd;
                return FnlStngCmd;
                //sendChangesToPhbySMS(StuMob, FnlStngCmd, College_Id);
                //SendStngChnageMail(FnlStngCmd, FnlStngCmd);
            }
            else
            {
                return "0";
            }
        }
        protected string makeStngSMSForPhSettings(DataTable myStngDT)
        {
            string myStngCmds = "";
            string FnlStngCmd = "";

            int PrvsCntrlTypeId = 0;
            int CntrlTypeId = 0;// int.Parse(lblFeatureId.Text);
            string myCntrolTypeName = "";
            string AlowFromDay = "";
            string Stngs = "";

            for (int idx = 0; idx < myStngDT.Rows.Count; idx++)
            {
                AlowFromDay = myStngDT.Rows[idx]["AlowFromDay"].ToString();
                Stngs = myStngDT.Rows[idx]["Stngs"].ToString();

                Stngs = Stngs.Replace(":", "");
                if (idx != 0 && myStngCmds != "" && PrvsCntrlTypeId == CntrlTypeId)
                {
                    if (PrvsCntrlTypeId == CntrlTypeId)
                    {
                        myStngCmds = myStngCmds + " D" + AlowFromDay + Stngs;
                    }
                }
                else
                {
                    myCntrolTypeName = "GM1";
                    myStngCmds = myStngCmds + " " + myCntrolTypeName + " " + lblFeatureId.Text + " D" + AlowFromDay + Stngs;
                    //myStngCmds = myStngCmds + " " + myCntrolTypeName + " " + lblFeatureId.Text + " D" + AlowFromDay + Stngs;
                }
                PrvsCntrlTypeId = CntrlTypeId;
                FnlStngCmd = myStngCmds;
            }

            if (FnlStngCmd.Trim() != "")
            {
                FnlStngCmd = "GBox set as" + FnlStngCmd;
                return FnlStngCmd;
                //sendChangesToPhbySMS(StuMob, FnlStngCmd, College_Id);
                //SendStngChnageMail(FnlStngCmd, FnlStngCmd);
            }
            else
            {
                return "0";
            }
        }
        protected int UpdateMsg(string Msg, int ProfileFeatureId)
        {
            probal = new ProfileBAL();
            probal.ProfileFeatureMappingId = ProfileFeatureId;
            probal.Message = Msg;
            return probal.UpdateMsg();
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            mpcancel.Show();
        }
        protected void BindKeywordGrid()
        {
            alert = new AlertBAL();
            alert.ProfileId = ProfileId;
            grdKey.DataSource = alert.GetKeyWordForProfile();
            grdKey.DataBind();
        }
        protected void btnclosemanage_Click1(object sender, EventArgs e)
        {
            Response.Redirect("CallAndSmsRegulation.aspx");
        }
        protected void grdKey_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKey.PageIndex = e.NewPageIndex;
            BindKeywordGrid();
        }
        protected void btnAddKeywordForWeb_Click(object sender, EventArgs e)
        {
            if (!PanelKeyword.Visible)
            {
                PanelKeyword.Visible = true;
                KeyWordPnl.Visible = false;
                btnAddKeywordForWeb.Text = "View List";
                ClearAllFields();
            }
            else
            {
                PanelKeyword.Visible = false;
                KeyWordPnl.Visible = true;
                btnAddKeywordForWeb.Text = "Add Keyword";
            }
            mpemanage.Show();
        }
        protected void CancelKey_Click(object sender, EventArgs e)
        {
            PanelKeyword.Visible = false;
            mpemanage.Show();
        }
        protected void btnAlertMgmt_Click(object sender, EventArgs e)
        {
            lblNotMsg.Text = string.Empty;
            if (PanelAlert.Visible)
            {
                PanelAlert.Visible = false;
                alertPnl.Visible = true;
                btnAlertMgmt.Text = "Add Mobile No";
                ClearAllFields();
                mpemanage.Show();
            }
            else
            {
                PanelAlert.Visible = true;
                alertPnl.Visible = false;
                btnAlertMgmt.Text = "View List";
                mpemanage.Show();
            }
        }
        protected void BindAlertGrid()
        {
            alert = new AlertBAL();
            alert.ProfileId = ProfileId;
            grdAlert.DataSource = alert.GetAlertNoByProfileId();
            grdAlert.DataBind();
        }
        protected void BindCallGrid()
        {
            allowphno = new AllowPhNoBAL();
            allowphno.ProfileId = ProfileId;
            allowphno.IsWhiteList = Convert.ToInt32(rbtnCall.SelectedValue.ToString());
            grdNo.DataSource = allowphno.GetProfileAllowedPhNo();
            grdNo.DataBind();
        }
        protected void BindSmsGrid()
        {
            allowphno = new AllowPhNoBAL();
            allowphno.ProfileId = ProfileId;
            allowphno.IsWhiteList = Convert.ToInt32(rbtnsms.SelectedValue.ToString());
            gridsms.DataSource = allowphno.GetProfileAllowedPhNo();
            gridsms.DataBind();
        }
        protected void btnaddalert_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtAddMobileNo.Text.Trim()))
            {
                if (txtAddMobileNo.Text.Length > 2 && txtAddMobileNo.Text.Length < 16 && Convert.ToInt32(ddlCountry.SelectedItem.Value) > 0)
                {
                    alert = new AlertBAL();
                    alert.ClientId = ClientId;
                    alert.ProfileId = ProfileId;
                    alert.MobileNo = txtAddMobileNo.Text.Trim();
                    alert.CountryId = ddlCountry.SelectedItem.Value;
                    alert.UserId = UserId;
                    if (Convert.ToInt32(alert.InsertProfileAlertNoRaj()) == 1)
                    {
                        lblNotMsg.Text = "Mobile No added succesfully.";
                        txtAddMobileNo.Text = string.Empty;
                        lblNotMsg.ForeColor = System.Drawing.Color.Green;
                        BindAlertGrid();
                    }
                    else
                    {
                        lblNotMsg.Text = "Already exists.";
                        txtAddMobileNo.Text = string.Empty;
                        lblNotMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    alertlbl.Text = "Only allowed 3-15 Digit Mobile No ";
                    alertlbl.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                alertlbl.Text = "Please enter mobile no.";
                alertlbl.ForeColor = System.Drawing.Color.Red;
            }
            mpemanage.Show();
        }
        protected void grdAlert_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int c = 0;
                int count = grdAlert.Rows.Count;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // DataRowView grdAlert = (DataRowView)e.Row.DataItem;
                    CheckBox cb = (CheckBox)e.Row.FindControl("chkalertNo");
                    Label lblStatus = (Label)e.Row.FindControl("alertmobStatus");
                    if (lblStatus.Text == "0")
                    {
                        cb.Checked = true;
                    }
                    else
                        cb.Checked = false;
                    for (int idx = 0; idx < grdAlert.Rows.Count; idx++)
                    {
                        if (((CheckBox)grdAlert.Rows[idx].FindControl("chkalertNo")).Checked)
                        {
                            c++;
                        }
                    }

                    CheckBox chkheader = (CheckBox)grdAlert.HeaderRow.FindControl("chkalertHeader");
                    if (count == c)
                    {
                        chkheader.Checked = true;
                    }
                    else
                    {
                        chkheader.Checked = false;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        protected void btnApplyChangesForAlert_Click(object sender, EventArgs e)
        {
            try
            {
                PanelAlert.Visible = false;
                string WebsiteUrlList = "";
                for (int idx = 0; idx < grdAlert.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdAlert.Rows[idx].FindControl("chkalertNo"))).Checked)
                    {
                        WebsiteUrlList = WebsiteUrlList + ((Label)grdAlert.Rows[idx].FindControl("lblalertId")).Text + "~$";
                    }
                }
                if (WebsiteUrlList != "")
                {
                    alert = new AlertBAL();
                    alert.ProfileId = ProfileId;
                    alert.UserId = UserId;
                    alert.AlertIdList = WebsiteUrlList;
                    if (Convert.ToInt32(alert.AssignAlertNoToProfile()) == 1)
                    {
                        lblNotMsg.Text = "Changes applied successfully.";
                        lblNotMsg.ForeColor = System.Drawing.Color.Green;
                        BindAlertGrid();
                    }
                    else
                    {
                        lblNotMsg.Text = "Something went wrong!";
                        lblNotMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblNotMsg.Text = "No Mobile No. selected!";
                    lblNotMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception)
            {

            }
            mpemanage.Show();
        }
        protected void btnApplyChangesForKeyword_Click(object sender, EventArgs e)
        {
            try
            {
                string WebsiteUrlList = "";
                for (int idx = 0; idx < grdKey.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdKey.Rows[idx].FindControl("chkKeyWordNo"))).Checked)
                    {
                        WebsiteUrlList = WebsiteUrlList + ((Label)grdKey.Rows[idx].FindControl("lblKeywordId")).Text + "~$";
                    }
                }
                if (WebsiteUrlList != "")
                {
                    alert = new AlertBAL();
                    alert.ProfileId = ProfileId;
                    alert.UserId = UserId;
                    alert.AlertIdList = WebsiteUrlList;
                    if (Convert.ToInt32(alert.AssignKeyWordToProfile()) == 1)
                    {
                        lblNotMsg.Text = "Changes applied successfully.";
                        lblNotMsg.ForeColor = System.Drawing.Color.Green;
                        BindKeywordGrid();
                    }
                    else
                    {
                        lblNotMsg.Text = "Something went wrong!";
                        lblNotMsg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else
                {
                    lblNotMsg.Text = "No keyword selected!";
                    lblNotMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
            mpemanage.Show();
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddKeywordCode.Text.Trim()) && !string.IsNullOrEmpty(txtAddKeywordName.Text.Trim()) && !string.IsNullOrEmpty(txtAddDescription.Text.Trim()))
            {
                alert = new AlertBAL();
                alert.ClientId = ClientId;
                alert.ProfileId = ProfileId;
                alert.KeywordCode = txtAddKeywordCode.Text.Trim();
                alert.KeywordName = txtAddKeywordName.Text.Trim();
                alert.KeywordDesc = txtAddDescription.Text.Trim();
                alert.UserId = UserId;
                if (Convert.ToInt32(alert.InsertProfileKeyWord()) == 1)
                {
                    lblNotMsg.Text = "Keyword added succesfully.";
                    txtAddKeywordCode.Text = txtAddKeywordName.Text = txtAddDescription.Text = string.Empty;
                    lblNotMsg.ForeColor = System.Drawing.Color.Green;
                    BindKeywordGrid();
                }
                else
                {
                    lblNotMsg.Text = "Keyword Already exists.";
                    txtAddMobileNo.Text = string.Empty;
                    lblNotMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblNotMsg.Text = "Please enter all fields.";
                lblNotMsg.ForeColor = System.Drawing.Color.Red;
            }
            mpemanage.Show();
        }
        protected void grdKey_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int count = grdKey.Rows.Count;
                int c = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView grdAlert = (DataRowView)e.Row.DataItem;
                    CheckBox cb = (CheckBox)e.Row.FindControl("chkKeyWordNo");
                    Label lblStatus = (Label)e.Row.FindControl("lblKWStatus");
                    if (lblStatus.Text == "0")
                    {
                        cb.Checked = true;
                    }
                    else
                        cb.Checked = false;
                    for (int idx = 0; idx < grdKey.Rows.Count; idx++)
                    {
                        if (((CheckBox)grdKey.Rows[idx].FindControl("chkKeyWordNo")).Checked)
                        {
                            c++;
                        }
                    }

                    CheckBox chkheader = (CheckBox)grdKey.HeaderRow.FindControl("chkKeywordHeader");
                    if (count == c)
                    {
                        chkheader.Checked = true;
                    }
                    else
                    {
                        chkheader.Checked = false;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        protected void btnaddCall_Click(object sender, EventArgs e)
        {
            if (panelCall.Visible)
            {
                panelCall.Visible = false;
                CallPnlTbl.Visible = true;
                btnaddCall.Text = "Add Mobile No";
                ClearAllFields();
            }
            else
            {
                panelCall.Visible = true;
                CallPnlTbl.Visible = false;
                btnaddCall.Text = "View List";
            }
            mpemanage.Show();
        }
        protected void btnCallManagement_Click(object sender, EventArgs e)
        {
            try
            {
                string IncomingCall = "";
                string OutgomingCall = "";
                for (int idx = 0; idx < grdNo.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdNo.Rows[idx].FindControl("chkCallNo"))).Checked)
                    {
                        IncomingCall = IncomingCall + ((Label)grdNo.Rows[idx].FindControl("lblProfileAllowedNoId")).Text + "~$";
                    }
                    if (((CheckBox)(grdNo.Rows[idx].FindControl("chkoutgoingCallNo"))).Checked)
                    {
                        OutgomingCall = OutgomingCall + ((Label)grdNo.Rows[idx].FindControl("lblProfileAllowedNoId")).Text + "~$";
                    }
                }
                if (IncomingCall != "" || OutgomingCall != "")
                {
                    alert = new AlertBAL();
                    alert.ProfileId = ProfileId;
                    alert.UserId = UserId;
                    alert.IncomingList = IncomingCall;
                    alert.OutgoingList = OutgomingCall;
                    alert.featureId = Convert.ToInt32(lblFeatureId.Text.ToString().Trim());
                    alert.IswhiteList = Convert.ToInt32(rbtnCall.SelectedValue.ToString());
                    alert.ClientId = ClientId;
                    string res = alert.AssignAllowedNoToProfile();
                    if (Convert.ToInt32(res) == 1)
                    {
                        lblNotMsg.Text = "Changes applied successfully.";
                        lblNotMsg.ForeColor = System.Drawing.Color.Green;
                        BindCallGrid();
                        foreach (GridViewRow row in grdDevice.Rows)
                        {
                            if (((Label)row.FindControl("lblId")).Text.Trim() == "19")
                            {
                                ((TextBox)row.FindControl("txtfreq")).Text = rbtnCall.SelectedValue.ToString();
                                break;
                            }
                        }

                    }
                    else
                    {
                        lblNotMsg.Text = "Something went wrong!";
                        lblNotMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblNotMsg.Text = "No Mobile No. selected!";
                    lblNotMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception)
            {

            }
            mpemanage.Show();
        }
        protected void btnAddNo_Click(object sender, EventArgs e)
        {
            AllowPhNoBAL phno;
            if (txtCallName.Text.Trim() != "" && txtAddNo.Text.Trim() != "")
            {
                if (chkIncoming.Checked || chkOutgoing.Checked || chkSms.Checked)
                {
                    if (txtAddNo.Text.Length > 2 && txtAddNo.Text.Length < 16 && Convert.ToInt32(ddlCallMCountry.SelectedItem.Value) > 0)
                    {
                        phno = new AllowPhNoBAL();
                        phno.ProfileId = ProfileId;
                        phno.ClientId = ClientId;
                        phno.UserId = UserId;
                        phno.Name = txtCallName.Text.Trim();
                        phno.MobileNo = txtAddNo.Text.Trim();
                        phno.CountryId = ddlCallMCountry.SelectedItem.Value;
                        phno.IsWhiteList = Convert.ToInt32(rbtnCall.SelectedValue.ToString());
                        if (chkIncoming.Checked == true)
                        {
                            phno.IsIncoming = 1;
                        }
                        else
                        {
                            phno.IsIncoming = 0;
                        }
                        if (chkOutgoing.Checked == true)
                        {
                            phno.IsOutgoing = 1;
                        }
                        else
                        {
                            phno.IsOutgoing = 0;
                        }
                        phno.IsForSms = 0;
                        int res = phno.InsertProfileAllowedNoRaj();
                        if (res > 0)
                        {
                            lblNotMsg.Text = "Inserted Successfully";
                            lblNotMsg.ForeColor = System.Drawing.Color.Green;
                            BindCallGrid();
                            txtCallName.Text = txtAddNo.Text = string.Empty;
                            chkIncoming.Checked = chkOutgoing.Checked = false;

                        }
                        else
                        {
                            lblNotMsg.Text = "Already Exists!!!";
                            lblNotMsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblCAllSms.Text = "Only allowed 3-15 Digit Mobile No ";
                        lblCAllSms.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblCAllSms.Text = "Select checkboxes for Incoming call or outgoing call!";
                    lblCAllSms.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblCAllSms.Text = "Enter name and mobileno!";
                lblCAllSms.ForeColor = System.Drawing.Color.Red;
            }
            mpemanage.Show();
        }
        protected void rbtnCall_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCallGrid();
            mpemanage.Show();
        }
        protected void grdNo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int count = grdNo.Rows.Count;
                int c = 0, a = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView grdAlert = (DataRowView)e.Row.DataItem;
                    CheckBox chkCallNo = (CheckBox)e.Row.FindControl("chkCallNo");
                    CheckBox chkoutgoingCallNo = (CheckBox)e.Row.FindControl("chkoutgoingCallNo");
                    Label lblIsIncoming = (Label)e.Row.FindControl("lblIsIncoming");
                    Label lblIsOutgoing = (Label)e.Row.FindControl("lblIsOutgoing");
                    if (lblIsIncoming.Text == "1")
                    {
                        chkCallNo.Checked = true;
                    }
                    else
                        chkCallNo.Checked = false;
                    if (lblIsOutgoing.Text == "1")
                    {
                        chkoutgoingCallNo.Checked = true;
                    }
                    else
                        chkoutgoingCallNo.Checked = false;
                    for (int idx = 0; idx < grdNo.Rows.Count; idx++)
                    {
                        if (((CheckBox)(grdNo.Rows[idx].FindControl("chkCallNo"))).Checked)
                        {
                            c++;
                        }
                    }
                    CheckBox chkheader = (CheckBox)grdNo.HeaderRow.FindControl("chkIncomingHeader");
                    if (c == count)
                    {

                        chkheader.Checked = true;
                    }
                    else
                    {
                        chkheader.Checked = false;
                    }
                    for (int idx = 0; idx < grdNo.Rows.Count; idx++)
                    {
                        if (((CheckBox)(grdNo.Rows[idx].FindControl("chkoutgoingCallNo"))).Checked)
                        {
                            a++;
                        }
                    }
                    CheckBox chkheaderoutgoing = (CheckBox)grdNo.HeaderRow.FindControl("chkOutgoingHeader");
                    if (a == count)
                    {

                        chkheaderoutgoing.Checked = true;
                    }
                    else
                    {
                        chkheaderoutgoing.Checked = false;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        protected void rbtnsms_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSmsGrid();
            mpemanage.Show();
        }
        protected void btnAddSMS_Click(object sender, EventArgs e)
        {
            if (panelSms.Visible)
            {
                panelSms.Visible = false;
                PnlSMS.Visible = true;
                btnAddSMS.Text = "Add Mobile No";
                ClearAllFields();
            }
            else
            {
                panelSms.Visible = true;
                PnlSMS.Visible = false;
                btnAddSMS.Text = "View List";
            }
            mpemanage.Show();
        }
        protected void btnManageSms_Click(object sender, EventArgs e)
        {
            try
            {
                string IncomingCall = "";
                for (int idx = 0; idx < gridsms.Rows.Count; idx++)
                {
                    if (((CheckBox)(gridsms.Rows[idx].FindControl("chkCallNosms"))).Checked)
                    {
                        IncomingCall = IncomingCall + ((Label)gridsms.Rows[idx].FindControl("lblProfileAllowedNoIdsms")).Text + "~$";
                    }
                }
                if (IncomingCall != "")
                {
                    alert = new AlertBAL();
                    alert.ProfileId = ProfileId;
                    alert.UserId = UserId;
                    alert.IncomingList = IncomingCall;
                    alert.featureId = Convert.ToInt32(lblFeatureId.Text.ToString().Trim());
                    alert.IswhiteList = Convert.ToInt32(rbtnsms.SelectedValue.ToString());
                    alert.ClientId = ClientId;
                    string res = alert.AssignAllowedNoToProfileSMS();
                    if (Convert.ToInt32(res) == 1)
                    {
                        lblNotMsg.Text = "Changes applied successfully.";
                        lblNotMsg.ForeColor = System.Drawing.Color.Green;
                        BindSmsGrid();
                        foreach (GridViewRow row in grdDevice.Rows)
                        {
                            if (((Label)row.FindControl("lblId")).Text.Trim() == "22")
                            {
                                ((TextBox)row.FindControl("txtfreq")).Text = rbtnsms.SelectedValue.ToString();
                                break;
                            }
                        }
                    }
                    else
                    {
                        lblNotMsg.Text = "Something went wrong!";
                        lblNotMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblNotMsg.Text = "No Mobile No. selected!";
                    lblNotMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception)
            {

            }
            mpemanage.Show();
        }
        protected void btnaddSMS12_Click(object sender, EventArgs e)
        {
            AllowPhNoBAL phno;
            if (txtsmsname.Text.Trim() != "" && txtMobileNo.Text.Trim() != "")
            {
                if (txtMobileNo.Text.Length > 2 && txtMobileNo.Text.Length < 16)
                {
                    phno = new AllowPhNoBAL();
                    phno.ProfileId = ProfileId;
                    phno.ClientId = ClientId;
                    phno.UserId = UserId;
                    phno.Name = txtsmsname.Text.Trim();
                    phno.MobileNo = txtMobileNo.Text.Trim();
                    phno.IsWhiteList = Convert.ToInt32(rbtnsms.SelectedValue.ToString());
                    phno.IsIncoming = 0;
                    phno.IsOutgoing = 0;
                    phno.IsForSms = 1;
                    int res = phno.InsertProfileAllowedNo();
                    if (res > 0)
                    {
                        lblNotMsg.Text = "Inserted Successfully";
                        lblNotMsg.ForeColor = System.Drawing.Color.Green;
                        BindSmsGrid();
                        txtsmsname.Text = txtMobileNo.Text = string.Empty;

                    }
                    else
                    {
                        SmsLabl.Text = "Already Exists!!!";
                        SmsLabl.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    SmsLabl.Text = "Only allowed 3-15 Digit Mobile No ";
                    SmsLabl.ForeColor = System.Drawing.Color.Red;
                }
            }

            else
            {
                SmsLabl.Text = "Enter name and mobileno!";
                SmsLabl.ForeColor = System.Drawing.Color.Red;
            }
            mpemanage.Show();
        }
        protected void gridsms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int c = 0;
                int count = gridsms.Rows.Count;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView grdAlert = (DataRowView)e.Row.DataItem;
                    CheckBox chkCallNo = (CheckBox)e.Row.FindControl("chkCallNosms");
                    Label lblIsSMS = (Label)e.Row.FindControl("lblIsSMS");
                    if (lblIsSMS.Text == "1")
                    {
                        chkCallNo.Checked = true;
                    }
                    else
                        chkCallNo.Checked = false;
                    for (int idx = 0; idx < gridsms.Rows.Count; idx++)
                    {
                        if (((CheckBox)gridsms.Rows[idx].FindControl("chkCallNosms")).Checked)
                        {
                            c++;
                        }
                    }
                    CheckBox chkheader = (CheckBox)gridsms.HeaderRow.FindControl("chkSmsHeader");
                    if (count == c)
                    {
                        chkheader.Checked = true;
                    }
                    else
                    {
                        chkheader.Checked = false;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        protected void cball_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem li in chkDay.Items)
            {
                li.Selected = cball.Checked ? true : false;
            }
            cbWeekdays.Checked = false;
            cbWeekEnd.Checked = false;
            mp.Show();
        }
        protected void cbWeekdays_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem li in chkDay.Items)
            {
                li.Selected = false;
                if (li.Value != "6" && li.Value != "7")
                    li.Selected = cbWeekdays.Checked ? true : false;
            }
            cball.Checked = false;
            cbWeekEnd.Checked = false;
            mp.Show();
        }
        protected void cbWeekEnd_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem li in chkDay.Items)
            {
                li.Selected = false;
                if (li.Value == "6" || li.Value == "7")
                    li.Selected = cbWeekEnd.Checked ? true : false;
            }
            cball.Checked = false;
            cbWeekdays.Checked = false;
            mp.Show();
        }
        protected void btncancelok_Click(object sender, EventArgs e)
        {
            Response.Redirect("Feature.aspx");
        }
        protected void btncancelcan_Click(object sender, EventArgs e)
        {
            mpcancel.Hide();
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            string timingid = lblalertTimingid.Text;
            int fid = Convert.ToInt32(lblalertfeatureid.Text);
            probal = new ProfileBAL();
            GdvStngsDeleting(timingid, fid, false);
            lblManFields.Text = "Timing Details deleted successfully!";
            lblManFields.ForeColor = System.Drawing.Color.Green;
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblManFields.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalertTimingid.Text = "";
            lblalertfeatureid.Text = "";
        }
        protected void chkIncomingHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkheader = (CheckBox)grdNo.HeaderRow.FindControl("chkIncomingHeader");
                foreach (GridViewRow row in grdNo.Rows)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("chkCallNo");
                    if (chkheader.Checked == true)
                    {
                        chkrow.Checked = true;
                    }
                    else
                    {
                        chkrow.Checked = false;
                    }
                }
                mpemanage.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void chkOutgoingHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkheader = (CheckBox)grdNo.HeaderRow.FindControl("chkOutgoingHeader");
                foreach (GridViewRow row in grdNo.Rows)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("chkoutgoingCallNo");
                    if (chkheader.Checked == true)
                    {
                        chkrow.Checked = true;
                    }
                    else
                    {
                        chkrow.Checked = false;
                    }
                }
                mpemanage.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void chkSmsHeader_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkheader = (CheckBox)gridsms.HeaderRow.FindControl("chkSmsHeader");
                foreach (GridViewRow row in gridsms.Rows)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("chkCallNosms");
                    if (chkheader.Checked == true)
                    {
                        chkrow.Checked = true;
                    }
                    else
                    {
                        chkrow.Checked = false;
                    }
                }
                mpemanage.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void chkKeywordHeader_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkheader = (CheckBox)grdKey.HeaderRow.FindControl("chkKeywordHeader");
                foreach (GridViewRow row in grdKey.Rows)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("chkKeyWordNo");
                    if (chkheader.Checked == true)
                    {
                        chkrow.Checked = true;
                    }
                    else
                    {
                        chkrow.Checked = false;
                    }
                }
                mpemanage.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void chkalertHeader_OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkheader = (CheckBox)grdAlert.HeaderRow.FindControl("chkalertHeader");
                foreach (GridViewRow row in grdAlert.Rows)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("chkalertNo");
                    if (chkheader.Checked == true)
                    {
                        chkrow.Checked = true;
                    }
                    else
                    {
                        chkrow.Checked = false;
                    }
                }
                mpemanage.Show();
            }
            catch (Exception)
            {

            }
        }
    }
}
