﻿using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using MobiOcean.MDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class GeoFenceMgmt : Base
    {
        int ClientId, UserId, RoleId, FeatureId, ProfileId, HdrId, AlowFromDay, AlowToDay, TotalDuration, DeptId;
        string AlowFromTime, AlowToTime, ProfileName;
        int CategoryId = 24;
        GroupBAL grpbal;
        ProfileBAL probal;
        VikramSearch srch;
        GingerboxSrch GSrch;
        DataTable dt, dt1, dtMsg;
        LocationBAL Loc;
        AnuSearch Search;

        protected String LabelProperty
        {
            get
            {
                return hidden.Value;
            }
            set
            {
                hidden.Value = value;
            }
        }
        protected String StartAddress
        {
            get { return hdnStartAddress.Value; }
            set { hdnStartAddress.Value = value; }
        }
        protected String StartLat
        {
            get
            {
                return hdnStartLat.Value;
            }
            set
            {
                hdnStartLat.Value = value;
            }
        }
        protected void btnCheckFeatureCancel_Click(object sender, EventArgs e)
        {
            MPCheckFeature.Hide();
        }
        protected String StartLong
        {
            get
            {
                return hdnStartLong.Value;
            }
            set
            {
                hdnStartLong.Value = value;
            }
        }
        protected String StartRouteAddress
        {
            get { return RouteStartAddr.Value; }
            set { RouteStartAddr.Value = value; }
        }
        protected String StartRouteLat
        {
            get
            {
                return RouteStartLat.Value;
            }
            set
            {
                RouteStartLat.Value = value;
            }
        }
        protected String StartRouteLong
        {
            get
            {
                return RouteStartLong.Value;
            }
            set
            {
                RouteStartLong.Value = value;
            }
        }
        protected String EndRouteAddress
        {
            get
            {
                return RouteEndAddr.Value;
            }
            set
            {
                RouteEndAddr.Value = value;
            }
        }
        protected String EndRouteLat
        {
            get
            {
                return RouteEndLat.Value;
            }
            set
            {
                RouteEndLat.Value = value;
            }
        }
        protected String EndRouteLong
        {
            get
            {
                return RouteEndLong.Value;
            }
            set
            {
                RouteEndLong.Value = value;
            }
        }
        protected String Radius
        {
            get
            {
                return hdnRadius.Value;
            }
            set
            {
                hdnRadius.Value = value;
            }
        }
        protected String LocationName
        {
            get
            {
                return SetLocationName.Value;
            }
            set
            {
                SetLocationName.Value = value;
            }
        }
        protected String Latitude
        {
            get
            {
                return SetLat.Value;
            }
            set
            {
                SetLat.Value = value;
            }
        }
        protected String Longitude
        {
            get
            {
                return SetLongi.Value;
            }
            set
            {
                SetLongi.Value = value;
            }
        }
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
                ViewState["UserIdList"] = "";
                //BindProfileDetail();
                BindGrid();
                txtProfileName.Text = ProfileName.ToString();
            }
            lblMsg.Text = string.Empty;
            lblMsgPop.Text = string.Empty;
            lblManFields.Text = string.Empty;
            lblFinalmsg.Text = string.Empty;
            Label1.Text = string.Empty;
            lblMultipleSlotSlctnMSGDayNumber.Text = string.Empty;
            lblMultipleSlotSlctnMSG.Text = string.Empty;
            msgpop.Text = string.Empty;
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
                    LinkButton lnkSchedule = (LinkButton)e.Row.FindControl("Schedule");
                    LinkButton lnkFrequency = (LinkButton)e.Row.FindControl("Frequency");
                    ImageButton imgbtnyes = (ImageButton)e.Row.FindControl("btnyes");
                    ImageButton imgbtnno = (ImageButton)e.Row.FindControl("btnNo");
                    CheckBox chk3 = (CheckBox)e.Row.FindControl("switchsize");
                    Label lblIsScheduleNeed = (Label)e.Row.FindControl("lblIsScheduleNeed");
                    Label FeatureId = (Label)e.Row.FindControl("lblId");


                    try
                    {
                        chk3.Checked = Convert.ToBoolean(grdDevice["IsEnable"]);
                    }
                    catch (Exception) { chk3.Checked = false; }
                    if (chk3.Checked)
                    {
                        imgbtnyes.Visible = true;
                        lnkSchedule.Enabled = true;
                        lnkFrequency.Enabled = true;
                        lnkSchedule.Attributes["style"] = " background-color:#2A368B !important;";
                        lnkFrequency.Attributes["style"] = " background-color:#2A368B !important;";
                    }
                    else
                    {
                        imgbtnno.Visible = true;
                        lnkSchedule.Enabled = false;
                        lnkFrequency.Enabled = false;
                        lnkSchedule.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                        lnkFrequency.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                    }
                    if (lblIsScheduleNeed.Text == "1")
                    {
                        lnkSchedule.Text = "Schedule";

                    }
                    else
                    {
                        lnkSchedule.Text = "&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;";


                    }
                    //if (FeatureId.Text == "38")
                    //{
                    //    lnkFrequency.Enabled = false;
                    //    lnkFrequency.Text = "&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;";
                    //}
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
                //GridView GridFeature = (GridView)(gvr.Parent.Parent);
                Label lbl = ((Label)gvr.FindControl("lblId"));
                Label lbl1 = ((Label)gvr.FindControl("lblProfileId"));
                lblFeatureN.Text = "Schedule " + ((Label)gvr.FindControl("lblFeatureName")).Text;
                lblFeatureId.Text = lbl.Text;
                lblProfilePopId.Text = lbl1.Text;
                lblHdrId.Text = ((Label)gvr.FindControl("lblProfileFeatureId")).Text;
                lblCategoryIdMp.Text = CategoryId.ToString();
                BindTimingGrid();
                lblAppHead.Visible = false;
                ddlAppGroupMain.Visible = false;
                gdv.Columns[2].Visible = false;
                lblManFields.Text = string.Empty;
                lblMultipleSlotSlctnMSG.Text = string.Empty;
                lblMultipleSlotSlctnMSGDayNumber.Text = string.Empty;
                txtTotal.Visible = false;
                gdv.Columns[7].Visible = false;
                mp.Show();
            }
            else if (e.CommandName == "Frequency")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                //GridView GridFeature = (GridView)(gvr.Parent.Parent);            
                Label lbl = ((Label)gvr.FindControl("lblId"));
                Label lbl1 = ((Label)gvr.FindControl("lblProfileId"));

                lblFeatureId1.Text = lbl.Text;
                lblFeatureId.Text = lbl.Text;
                lblProfilePopId1.Text = lbl1.Text;
                lblHdrId.Text = ((Label)gvr.FindControl("lblProfileFeatureId")).Text;
                if (lblFeatureId1.Text == "37")
                {
                    lblFeatureN1.Text = "Manage Frequency " + ((Label)gvr.FindControl("lblFeatureName")).Text;
                    lblHdrId1.Text = ((Label)gvr.FindControl("lblProfileFeatureId")).Text;
                    lblHdrId.Text = ((Label)gvr.FindControl("lblProfileFeatureId")).Text;
                    //lblCategoryIdMp.Text = 5.ToString();
                    BindPopUpGrid();
                    mpfreq.Show();
                }
                if (lblFeatureId1.Text == "38")
                {
                    lblFeatureN2.Text = "Manage " + ((Label)gvr.FindControl("lblFeatureName")).Text;
                    BindPopUpGeoFenceGrid();
                    mpgeo.Show();
                    radbtnlisst.SelectedIndex = 0;
                    btnSaveGeo.Visible = true;
                    grdGeo.Visible = true;

                }
                if (lblFeatureId1.Text == "60")
                {
                    lblFeatureN3.Text = "Manage " + ((Label)gvr.FindControl("lblFeatureName")).Text;
                    BindSetLocationPopup();
                    mpsetlocation.Show();
                }
            }
            else if (e.CommandName == "Yes")
            {
                GridViewRow gvr = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                //GridView GridFeature = (GridView)(gvr.Parent.Parent);
                ImageButton imgbtnyes = (ImageButton)gvr.FindControl("btnyes");
                ImageButton imgbtnno = (ImageButton)gvr.FindControl("btnNo");
                LinkButton lnkSchedule = (LinkButton)gvr.FindControl("Schedule");
                CheckBox chk4 = (CheckBox)gvr.FindControl("switchsize");
                Label lblIsScheduleNeed = (Label)gvr.FindControl("lblIsScheduleNeed");
                Label lblChanged = (Label)gvr.FindControl("lblChanged");
                //Label lblCId = (Label)gvr.FindControl("lblCId");
                LinkButton lnkFrequency = (LinkButton)gvr.FindControl("Frequency");
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
                        lnkSchedule.Text = "&nbsp;&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                    lnkSchedule.Enabled = false;
                    lnkSchedule.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                    lnkFrequency.Enabled = false;
                    lnkFrequency.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                    if (lblChanged.Text == "0")
                    {
                        lblChanged.Text = "1";
                    }
                    else
                    {
                        lblChanged.Text = "0";
                    }
                    // ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + 9 + "','one');</script>");

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
                CheckBox chk5 = (CheckBox)gvr.FindControl("switchsize");
                Label lblIsScheduleNeed = (Label)gvr.FindControl("lblIsScheduleNeed");
                Label lblChanged = (Label)gvr.FindControl("lblChanged");
                //Label lblCId = (Label)gvr.FindControl("lblCId");
                LinkButton lnkFrequency = (LinkButton)gvr.FindControl("Frequency");
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
                            lnkSchedule.Text = "Schedule";
                            lnkSchedule.Enabled = true;
                            lnkFrequency.Enabled = true;
                            lnkSchedule.Attributes["style"] = " background-color:#2A368B !important;";
                            lnkFrequency.Attributes["style"] = " background-color:#2A368B !important;";
                        }
                        else
                        {
                            lnkSchedule.Text = "&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;";
                            lnkSchedule.Enabled = false;
                            lnkSchedule.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                            lnkFrequency.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                            lnkFrequency.Enabled = false;

                    }
                    // ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + 9 + "','one');</script>");

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
        protected void GridFeature_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow rows in grdDevice.Rows)
            {
                if (rows.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        probal = new ProfileBAL();
                        probal.ClientId = ClientId;
                        probal.ProfileId = ProfileId;
                        probal.FeatureId = Convert.ToInt32(((Label)rows.FindControl("lblId")).Text.Trim());
                        probal.IsEnable = ((CheckBox)rows.FindControl("switchsize")).Checked ? 1 : 0;
                        probal.IU_ProfileFetaureON();
                    }
                    catch (Exception) { }
                }
            }
            probal = new ProfileBAL();
            probal.ProfileId = ProfileId;
            probal.LoggedBy = UserId.ToString();
            probal.MoveDatainOriginalTables();
            //Session["ProfileId"] = null;
            //SendUpdateMsg();
            MP1.Show();
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
                catch (Exception) { ProfileFeatureMappingId = GSrch.GetProfileFeatureMappingId(ProfileId, Convert.ToInt32(lblFeatureId.Text)); }
                try
                {
                    GroupId = int.Parse(ddlAppGroupMain.SelectedValue.ToString());
                }
                catch (Exception) { GroupId = 0; }

                gdv.DataSource = GSrch.FeatureTimingfromTemp(ProfileFeatureMappingId, GroupId);
                gdv.DataBind();
            }
            catch (Exception)
            {

            }
        }
        protected void BindGroupDDL(DropDownList ddl)
        {
            ListItem ls = new ListItem("---Select---", "0");
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
            ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + lblCategoryIdMp.Text + "','one');</script>");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + lblCategoryIdMp.Text + "','one');</script>");
            mp.Hide();
        }
        protected void btnClose1_Click1(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + lblCategoryIdMp.Text + "','one');</script>");
            mp.Hide();
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            mpcancel.Show();
        }
        protected void gdv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gdv.EditIndex = e.NewEditIndex;
            BindTimingGrid();
            mp.Show();

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
                                lblManFields.ForeColor = System.Drawing.Color.Red;                            //mp.Show();
                            }


                            gdv.EditIndex = -1;
                            BindTimingGrid();
                            ClearAllFields();
                            //mp.Show();
                        }
                        catch (Exception)
                        {
                            //  mp.Show();
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
                lblname.Text = "1";
                string lblDtlId = ((Label)gvr.FindControl("lblTimingId")).Text;
                FeatureId = (Convert.ToInt32(lblProfilePopId.Text));
                //lblalert.Text = "Confirm";
                //lblUser.Text = "Are you sure to delete?";
                lblalertTimingid.Text = ((Label)gvr.FindControl("lblTimingId")).Text;
                lblalertfeatureid.Text = lblProfilePopId.Text;
                mp.Show();
                mpdelete.Show();
                //    GdvStngsDeleting(lblDtlId, FeatureId, false);
                //    lblManFields.Text = "Deleted Successfully";
                //    lblManFields.ForeColor = System.Drawing.Color.Green;
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
                probal.ChangeTempTimingStatus();
                BindTimingGrid();
                mp.Show();
            }
            catch (Exception) { }
            finally
            {
                probal = null;

            }
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
                            lblMultipleSlotSlctnMSG.Text = "The settings of follwoing days will be override, because Total Duration is not allowed with multiple time slots :-<br>" +
                                                          "<b><i>" + listofMultipleTimeSlotsAndSlotswithDuration + "</i></b>";
                        }
                        else
                        {
                            lblMultipleSlotSlctnMSG.Text = "The settings of follwoing days will be override, because Total Duration is not allowed with multiple time slots :-<br>" +
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
                BindTimingGrid();
                mp.Show();
            }
            else
            {
                lblManFields.Text = "Schedule Added succesfully.";
                lblManFields.ForeColor = System.Drawing.Color.Green;
                BindTimingGrid();
                mp.Show();
                ClearAllFields();
            }


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
            txtFreq.Text = "";
            cball.Checked = false;
            cbWeekdays.Checked = false;
            cbWeekEnd.Checked = false;
            foreach (ListItem itm in chkDay.Items)
            {
                itm.Selected = false;
            }

            //SetEnabilityOfFromAndToTime(false);

            // txtTotal.Enabled = false;

        }
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if (gdv.Rows.Count > 0)
            //{
            UpdateLocationSchedule();
        }


        protected void UpdateLocationSchedule()
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
                }
                PrvsCntrlTypeId = CntrlTypeId;
                FnlStngCmd = myStngCmds;
            }

            if (FnlStngCmd.Trim() != "")
            {
                string MsgText = "NA";
                if (lblFeatureId.Text == "37")
                {
                    MsgText = sendLocReqFreqMsgByProfileId(ClientId, ProfileId);
                }
                if (MsgText != "NA")
                {
                    FnlStngCmd = MsgText + " " + FnlStngCmd;
                }
                else
                {
                    FnlStngCmd = "GBox set as " + FnlStngCmd;
                }

                return FnlStngCmd;
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
        protected void BindPopUpGrid()
        {
            try
            {
                Loc = new LocationBAL();
                Loc.ProfileId = ProfileId;
                grdLocReq.DataSource = Loc.GetLocFreqMgmt();
                grdLocReq.DataBind();
            }
            catch (Exception) { }
            finally
            {
                Loc = null;
            }
        }
        protected void btnAddFrequency_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            try
            {
                if (ChkFrmAndToTimeAreSetOk(ddlFromHrFreq, ddlFromMinFreq, ddlToHourFreq, ddlToMinFreq))
                {
                    if (!string.IsNullOrEmpty(txtFreq.Text) && Convert.ToInt32(txtFreq.Text) > 0)
                    {
                        Loc = new LocationBAL();
                        Loc.ClientId = ClientId;
                        Loc.UserId = UserId;
                        Loc.FrmTime = ddlFromHrFreq.SelectedItem.ToString() + ":" + ddlFromMinFreq.SelectedItem.ToString(); ;
                        Loc.ToTime = ddlToHourFreq.SelectedItem.ToString() + ":" + ddlToMinFreq.SelectedItem.ToString();
                        Loc.ProfileId = ProfileId;
                        Loc.LocReqFrequency = Convert.ToInt32(txtFreq.Text);
                        int result = Loc.IU_LocReqFreqMgmtByProfileId();
                        if (result == 1)
                        {
                            lblMsg.Text = "Saved successfully.";
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                            BindPopUpGrid();
                            ClearAllData();
                            mpfreq.Show();

                        }
                        else
                        {
                            lblMsg.Text = "Time intervals aready exists or overlapping the existing intervals.";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            mpfreq.Show();
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please enter time interval.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        mpfreq.Show();
                    }
                }

                else
                {
                    lblMsg.Text = "Please select From and To Time carefully.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    mpfreq.Show();
                }

            }
            catch (Exception)
            {
                lblMsg.Text = "Something went wrong.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                mpfreq.Show();
            }
            finally
            {
                Loc = null;
            }
        }
        protected void ClearAllData()
        {
            try
            {
                //ddlFHr.SelectedIndex = 0;
                // ddlFMin.SelectedIndex = 0;
                // ddlTHr.SelectedIndex = 0;
                //ddlTMin.SelectedIndex = 0;
                // txtLocation.Text = "";
                //  txtRadius.Text = "";
            }
            catch (Exception) { }
        }
        protected bool ChkFrmAndToTimeAreSetOk(DropDownList myddlFromHour, DropDownList myddlFromMin, DropDownList myddlToHour, DropDownList myddlToMin)
        {
            bool isOk = false;
            if (myddlFromHour.SelectedIndex > 0 || myddlFromMin.SelectedIndex > 0 || myddlToHour.SelectedIndex > 0 || myddlToMin.SelectedIndex > 0) //------ If one or more than on time fields are selected
            {
                isOk = false;
                if (myddlFromHour.SelectedIndex > 0 && myddlFromMin.SelectedIndex > 0 && myddlToHour.SelectedIndex > 0 && myddlToMin.SelectedIndex > 0) //------ if all time Fields are selected
                {
                    //--------- Now we chek that 'From Time' shouldn't be greater than or equal to 'To Time'

                    if (myddlFromHour.SelectedIndex < myddlToHour.SelectedIndex)
                    {
                        //---------------- When 'From Hour' is less than 'To hour'
                        isOk = true;
                    }
                    else if (myddlFromHour.SelectedIndex == myddlToHour.SelectedIndex)
                    {
                        //---------------- When 'From Hour' is equal to 'To hour'
                        if (myddlFromMin.SelectedIndex < myddlToMin.SelectedIndex)
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
                isOk = false;  //--------- When none of the Time field is selected -----------------
            }
            return isOk;
        }
        protected void grdLocReq_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdLocReq.EditIndex = e.NewEditIndex;
            BindPopUpGrid();
            mpfreq.Show();
        }
        protected void grdLocReq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblname.Text = "2";

            GridViewRow gvr = grdLocReq.Rows[e.RowIndex];
            try
            {
                lblalertlocreqid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                //    if (Loc.DeleteLocReqFreqMgmt() == 1)
                //    {
                //        lblMsg.Text = "Deleted successfully.";
                //        BindPopUpGrid();
                //        mpfreq.Show();
                //    }
                //    else
                //    {
                //        lblMsg.Text = "Not deleted successfully";
                //        mpfreq.Show();
                //    }
            }
            catch (Exception)
            {

            }
            finally
            {
                Loc = null;
            }
            mpfreq.Show();
            mpdelete.Show();
        }
        protected void grdLocReq_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdLocReq.Rows[e.RowIndex];
            try
            {
                DropDownList ddlEFromHourFreq = ((DropDownList)gvr.FindControl("ddlEFromHourFreq"));
                DropDownList ddlEFromMinFreq = ((DropDownList)gvr.FindControl("ddlEFromMinFreq"));
                DropDownList ddlEToHourFreq = ((DropDownList)gvr.FindControl("ddlEToHourFreq"));
                DropDownList ddlEToMinFreq = ((DropDownList)gvr.FindControl("ddlEToMinFreq"));

                if (ChkFrmAndToTimeAreSetOk(ddlEFromHourFreq, ddlEFromMinFreq, ddlEToHourFreq, ddlEToMinFreq))
                {
                    Loc = new LocationBAL();
                    Loc.ClientId = ClientId;
                    Loc.LocReqFreqId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                    Loc.LocReqFrequency = Convert.ToInt32(((TextBox)gvr.FindControl("txtELocReqFrequency")).Text.Trim());
                    Loc.ProfileId = ProfileId;
                    Loc.FrmTime = ddlEFromHourFreq.SelectedItem.ToString() + ":" + ddlEFromMinFreq.SelectedItem.ToString(); ;
                    Loc.ToTime = ddlEToHourFreq.SelectedItem.ToString() + ":" + ddlEToMinFreq.SelectedItem.ToString();
                    int result = Loc.IU_LocReqFreqMgmtByProfileId();
                    if (result == 1)
                    {
                        lblMsg.Text = "Updated successfully.";
                        lblMsg.ForeColor = System.Drawing.Color.Green;

                        grdLocReq.EditIndex = -1;
                        BindPopUpGrid();
                        mpfreq.Show();
                    }
                    else
                    {
                        lblMsg.Text = "Time intervals aready exists or overlapping the existing intervals.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        mpfreq.Show();
                    }
                }
                else
                {
                    lblMsg.Text = "Please select From and To Time carefully.";
                    mpfreq.Show();
                }
            }
            catch (Exception)
            {
                mpfreq.Show();
            }
            finally
            {
                Loc = null;
            }
        }
        protected void grdLocReq_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdLocReq.EditIndex = -1;
            BindPopUpGrid();
            mpfreq.Show();

        }
        protected void grdLocReq_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                    DropDownList ddlEFromHourFreq = (DropDownList)e.Row.FindControl("ddlEFromHourFreq");
                    DropDownList ddlEFromMinFreq = (DropDownList)e.Row.FindControl("ddlEFromMinFreq");
                    DropDownList ddlEToHourFreq = (DropDownList)e.Row.FindControl("ddlEToHourFreq");
                    DropDownList ddlEToMinFreq = (DropDownList)e.Row.FindControl("ddlEToMinFreq");

                    Label lblEStrtTimeFreq = (Label)e.Row.FindControl("lblEStrtTimeFreq");
                    Label lblEEndTimeFreq = (Label)e.Row.FindControl("lblEEndTimeFreq");

                    if (string.IsNullOrEmpty(lblEStrtTimeFreq.Text))
                    {
                        ddlEFromHourFreq.SelectedIndex = 0;
                        ddlEFromMinFreq.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlEFromHourFreq.SelectedValue = lblEStrtTimeFreq.Text.Substring(0, 2);
                        ddlEFromMinFreq.SelectedValue = lblEStrtTimeFreq.Text.Substring(3, 2);
                    }

                    if (string.IsNullOrEmpty(lblEEndTimeFreq.Text))
                    {
                        ddlEToHourFreq.SelectedIndex = 0;
                        ddlEToMinFreq.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlEToHourFreq.SelectedValue = lblEEndTimeFreq.Text.Substring(0, 2);
                        ddlEToMinFreq.SelectedValue = lblEEndTimeFreq.Text.Substring(3, 2);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            string Msg;
            probal = new ProfileBAL();
            dtMsg = new DataTable();
            try
            {
                probal.ProfileFeatureMappingId = int.Parse(lblHdrId1.Text.Trim());
            }
            catch (Exception) { probal.ProfileFeatureMappingId = 0; }
            dtMsg = probal.GetPhAndPrmsStngChangeForSMSAndEmail();
            Msg = makeStngSMSForPhSettings(dtMsg);
            if (Msg == "0")
            {
                Msg = null;
            }
            UpdateMsg(Msg, probal.ProfileFeatureMappingId);
        }
        protected string sendLocReqFreqMsgByProfileId(int ClientId, int ProfileId)
        {
            string msgTxt = "";
            try
            {
                Search = new AnuSearch();
                msgTxt = Search.getLocReqFreqMsgByProfileId(ClientId, ProfileId);
            }
            catch (Exception) { }
            return msgTxt;
        }
        protected void BindPopUpGeoFenceGrid()
        {
            Loc = new LocationBAL();
            Loc.ClientId = ClientId;
            Loc.ProfileId = ProfileId;
            grdGeo.DataSource = Loc.GetRouteNameByClientIdAndProfileId();
            grdGeo.DataBind();
        }
        protected void grdGeo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int c = 0;
            int count = grdGeo.Rows.Count;
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
                for (int idx = 0; idx < grdGeo.Rows.Count; idx++)
                {
                    if (((CheckBox)grdGeo.Rows[idx].FindControl("chkbox")).Checked)
                    {
                        c++;
                    }
                }
                CheckBox chkheader = (CheckBox)grdGeo.HeaderRow.FindControl("grdGeo_parent");
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
        protected void btnSaveGeo_Click(object sender, EventArgs e)
        {
            try
            {
                string RouteIdList = "";
                for (int idx = 0; idx < grdGeo.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdGeo.Rows[idx].FindControl("chkbox"))).Checked)
                    {
                        RouteIdList = RouteIdList + ((Label)grdGeo.Rows[idx].FindControl("lblId")).Text + "~$";
                    }
                    //else
                    //{
                    //    RouteIdList = RouteIdList + ((Label)grdGeo.Rows[idx].FindControl("lblId")).Text + "~$";
                    //}
                }
                if (RouteIdList != "")
                {
                    Loc = new LocationBAL();
                    Loc.ProfileId = ProfileId;
                    Loc.ClientId = ClientId;
                    Loc.UserId = UserId;
                    Loc.RouteIdList = RouteIdList;
                    int res = Loc.AssignRouteToProfile();
                    if (res > 0)
                    {
                        lblMsgPop.Text = "Changes Saved Successfully";
                        lblMsgPop.ForeColor = System.Drawing.Color.Green;
                        mpgeo.Show();
                    }
                    else
                    {
                        lblMsgPop.Text = "Not Saved";
                        lblMsgPop.ForeColor = System.Drawing.Color.Red;
                        mpgeo.Show();
                    }
                }

                else
                {
                    lblMsgPop.Text = "Route Name is not selected!";
                    lblMsgPop.ForeColor = System.Drawing.Color.Red;
                    mpgeo.Show();
                }
            }

            catch (Exception)
            {

            }
        }
        //protected string InputValue { get; set; }
        //protected System.Web.UI.HtmlControls.HtmlInputText txtPlaces;
        //protected string Value { get; set; }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            if (LabelProperty.Trim() == "")
            {
                lblMessage.Text = "Please draw a route on Map!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                string script = "window.onload = function() { initialize(); };";
                ClientScript.RegisterStartupScript(this.GetType(), "initialize", script, true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "initialize()", true);
            }
            else if (txtCode.Text.Trim() == "" || txtMobileNo.Text.Trim() == "")
            {
                lblMessage.Text = "Please fill mandatory field!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                string Latlong = LabelProperty.Trim().Substring(0, LabelProperty.Length - 1);
                dt = new DataTable();
                dt.Columns.Add("Latitude");
                dt.Columns.Add("Longitude");
                string[] latlngary = Latlong.Split(',');
                for (int i = 0; i < latlngary.Length; i = i + 2)
                {
                    dt.Rows.Add(latlngary[i].ToString(), latlngary[i + 1].ToString());
                }
                Loc = new LocationBAL();
                Loc.ClientId = ClientId;
                Loc.RouteName = txtMobileNo.Text.Trim();
                Loc.RouteCode = txtCode.Text;
                Loc.LoggedBy = UserId.ToString();
                Loc.RouteDesc = txtDesc.Text;
                Loc.RouteId = 0;
                Loc.ProfileId = ProfileId;
                Loc.LatLong = dt;
                int res = Convert.ToInt32(Loc.AddRouteToProfile());
                if (res > 0)
                {
                    lblFinalmsg.Text = "Saved Successfully";
                    lblFinalmsg.ForeColor = System.Drawing.Color.Green;

                    clearhiddenfieldandradio();
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "clearinputtext", "clearinputtext()", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "clearinputtext", "clearinputtext()", true);
                    //txtPlaces.Value = "test";
                    //Value = "ABC";
                    //this.InputValue = "";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('txtPlaces').value='';", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('txtPlaces1').value='';", true);
                }
                else
                {
                    lblMessage.Text = "Not Saved";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        protected void btn_Click(object sender, EventArgs e)
        {
            if (Add.Visible)
            {
                Add.Visible = false;
            }
            else
            {
                Add.Visible = true;
            }
        }
        protected void btnC_Click(object sender, EventArgs e)
        {
            clearhiddenfieldandradio();
        }
        protected void radbtnlisst_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblFinalmsg.Text = "";
            if (radbtnlisst.SelectedIndex == 0)
            {
                mpgeo.Show();
                lblMsgPop.Text = "";
                BindPopUpGeoFenceGrid();
                grdGeo.Visible = true;
                grdArea.Visible = false;
                Panel3.Visible = false;
                btnSaveGeo.Visible = true;
                btnSaveGeoFenceByRadius.Visible = false;
            }
            if (radbtnlisst.SelectedIndex == 1)
            {
                mpgeo.Show();
                btnSaveGeo.Visible = false;
                btnSaveGeoFenceByRadius.Visible = true;
                lblMsgPop.Text = "";
                gridradius();
                grdGeo.Visible = false;
                grdArea.Visible = true;
                Panel3.Visible = true;
            }
        }
        protected void gridradius()
        {

            try
            {
                Loc = new LocationBAL();
                Loc.ClientId = ClientId;
                Loc.ProfileId = ProfileId;
                grdArea.DataSource = Loc.GetAreaNameByClientIdAndProfileId();
                grdArea.DataBind();
            }
            catch (Exception) { }
            finally
            {
                Loc = null;
            }

        }
        protected void btnSaveGeoFenceByRadius_Click(object sender, EventArgs e)
        {
            string AreaIdList = "";
            for (int idx = 0; idx < grdArea.Rows.Count; idx++)
            {
                if (((CheckBox)(grdArea.Rows[idx].FindControl("chkboxArea"))).Checked)
                {
                    AreaIdList = AreaIdList + ((Label)(grdArea.Rows[idx].FindControl("lblAreaId"))).Text + ",";
                }
            }

            Loc = new LocationBAL();
            Loc.ProfileId = ProfileId;
            Loc.ClientId = ClientId;
            Loc.UserId = UserId;
            Loc.AreaIdList = AreaIdList;
            int res = Loc.AssignAreaToProfile();
            if (res > 0)
            {
                lblMsgPop.Text = "Changes Saved Successfully";
                lblMsgPop.ForeColor = System.Drawing.Color.Green;
                mpgeo.Show();
            }
            else
            {
                lblMsgPop.Text = "Not Saved";
                lblMsgPop.ForeColor = System.Drawing.Color.Red;
                mpgeo.Show();
            }

        }
        protected void grdArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdArea.PageIndex = e.NewPageIndex;
            mpgeo.Show();
            gridradius();
            grdGeo.Visible = false;
            grdArea.Visible = true;
            Panel3.Visible = true;

        }
        protected void grdArea_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int count = grdArea.Rows.Count;
            int c = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Status = ((Label)e.Row.FindControl("lblAreaStatus"));
                CheckBox chkbox = ((CheckBox)e.Row.FindControl("chkboxArea"));
                if (Status.Text == "0")
                {
                    chkbox.Checked = true;
                }
                else
                {
                    chkbox.Checked = false;
                }
                for (int idx = 0; idx < grdArea.Rows.Count; idx++)
                {
                    if (((CheckBox)grdArea.Rows[idx].FindControl("chkboxArea")).Checked)
                    {
                        c++;
                    }
                }
                CheckBox chkheader = (CheckBox)grdArea.HeaderRow.FindControl("grdArea_parent");
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
        protected void rbtnaddGeoFence_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rbtnaddGeoFence.SelectedIndex == 0)
            {
                panel.Visible = true;
                paneladdarea.Visible = false;
            }
            if (rbtnaddGeoFence.SelectedIndex == 1)
            {
                paneladdarea.Visible = true;
                panel.Visible = false;
            }
            lblMessage.Text = "";
            txtMobileNo.Text = "";
            txtCode.Text = "";
            txtDesc.Text = "";
            RouteEndAddr.Value = "";
            RouteStartAddr.Value = "";
            txtArea.Text = "";
            hdnStartAddress.Value = "";
            map.Visible = false;
            btnSaveArea.Visible = false;
            btnAssign.Visible = false;
        }
        protected void btnsavearea_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkValidation())
                {
                    getAreaData();
                    Loc = new LocationBAL();
                    Loc.ClientId = ClientId;
                    Loc.AreaName = txtArea.Text.Trim();
                    Loc.ProfileId = ProfileId;
                    Loc.Radius = (float)(Convert.ToDouble(Radius.Trim()));
                    Loc.UserId = UserId;

                    Loc.Latitude = StartLat.Trim();
                    Loc.Longitude = StartLong.Trim();

                    Loc.Location = StartAddress.Trim();
                    int res = Loc.IU_tblProfileArea();
                    if (res > 0)
                    {
                        lblFinalmsg.Text = "Saved Successfully";
                        lblFinalmsg.ForeColor = System.Drawing.Color.Green;
                        clearhiddenfieldandradio();
                    }
                    else
                    {
                        lblMessage2.Text = "Area Name already Exists";
                        lblMessage2.ForeColor = System.Drawing.Color.Red;
                        //BindGrid();
                    }

                }
                else
                {
                    lblMessage2.Text = "Please Fill Mandatory Fields";
                    lblMessage2.ForeColor = System.Drawing.Color.Red;
                    //BindGrid();
                }

            }
            catch (Exception)
            {

            }
        }
        protected bool ChkValidation()
        {
            if (txtArea.Text.Trim() == "" || StartAddress.Trim() == "")
            //txtLocation.Text==""||
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void btncncl_Click(object sender, EventArgs e)
        {
            clearhiddenfieldandradio();
            //paneladdarea.Visible = false;
            //map.Visible = false;
        }
        protected string getdata()
        {
            //location = new LocationBAL();
            //location.RouteId = RouteId;
            dt = new DataTable();
            dt.Columns.Add("Latiude");
            dt.Columns.Add("Longitude");
            try
            {
                dt.Rows.Add(StartRouteLat.Trim(), StartRouteLong.Trim());
                dt.Rows.Add(EndRouteLat.Trim(), EndRouteLong.Trim());
                GoogleAPI gapi = new GoogleAPI();
                string thrd = gapi.midPoint(double.Parse(StartRouteLat.Trim()), double.Parse(StartRouteLong.Trim()), double.Parse(EndRouteLat.Trim()), double.Parse(EndRouteLong.Trim()));
                dt.Rows.Add(thrd.Substring(0, thrd.IndexOf(',')), thrd.Substring(thrd.IndexOf(',') + 1));
                LabelProperty = StartRouteLat.Trim() + "," + StartRouteLong.Trim() + "," + (thrd.Substring(0, thrd.IndexOf(','))) + "," + (thrd.Substring(thrd.IndexOf(',') + 1)) + "," + EndRouteLat.Trim() + "," + EndRouteLong.Trim();

                //28.6524629, 77.1260591),
                //   new google.maps.LatLng(, ),
                // new google.maps.LatLng(, 77.1232247
                //dt = location.GetGeoFenceRouteDetail();
            }
            catch (Exception)
            {
                dt.Rows.Clear();
                dt.Rows.Add("28.6524629", "77.1260591");
                dt.Rows.Add("28.6527392", "77.1250851");
                dt.Rows.Add("28.6534888", "77.1232247");
                LabelProperty = "28.6524629" + "," + "77.1260591" + "," + "28.6527392" + "," + "77.1250851" + "," + "28.6534888" + "," + "77.1232247";
            }
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Clear();
                dt.Rows.Add("28.6524629", "77.1260591");
                dt.Rows.Add("28.6527392", "77.1250851");
                dt.Rows.Add("28.6534888", "77.1232247");
                LabelProperty = "28.6524629" + "," + "77.1260591" + "," + "28.6527392" + "," + "77.1250851" + "," + "28.6534888" + "," + "77.1232247";
            }
            return DataTableToJSONWithJavaScriptSerializer(dt);
            //return "0";

        }
        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {


            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }
        protected void Draw_Click(object sender, EventArgs e)
        {
            Loc = new LocationBAL();
            dt = new DataTable();
            dt1 = new DataTable();
            if (txtCode.Text.Trim() == "" || txtMobileNo.Text.Trim() == "" || Request.Form["startlocation"].ToString().Trim() == "" || Request.Form["deslocation"].ToString().Trim() == "")
            {
                lblMessage.Text = "Please fill mandatory field!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {

                Loc.ClientId = ClientId;
                Loc.RouteName = txtMobileNo.Text.Trim();
                Loc.RouteCode = txtCode.Text;
                dt = Loc.IsRoteCodeExists();
                dt1 = Loc.IsRoteNameExists();
                if (dt.Rows.Count > 0)
                {
                    lblMessage.Text = "Route Code Already Exists";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (dt1.Rows.Count > 0)
                    {
                        lblMessage.Text = "Route Name Already Exists";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblMessage.Text = "";
                        mapCall();
                    }
                }
            }

        }
        private void mapCall()
        {
            if (!string.IsNullOrEmpty(StartRouteLat.Trim()) && !string.IsNullOrEmpty(StartRouteLong.Trim()) && !string.IsNullOrEmpty(EndRouteLat.Trim()) && !string.IsNullOrEmpty(EndRouteLong.Trim()))
            {
                btnAssign.Visible = true;
                lblMessage.Text = "";
                //btnC.Visible = true;
                string script = "window.onload = function() { initialize(); };";
                ClientScript.RegisterStartupScript(this.GetType(), "initialize", script, true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "initialize()", true);
                map.Visible = true;
            }
            else
            {
                btnAssign.Visible = false;
                //btnC.Visible = false;
            }
        }
        protected void grdGeo_parent_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdGeo.HeaderRow.FindControl("grdGeo_parent");
            foreach (GridViewRow row in grdGeo.Rows)
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
            mpgeo.Show();
        }
        protected void grdArea_parent_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdArea.HeaderRow.FindControl("grdArea_parent");
            foreach (GridViewRow row in grdArea.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkboxArea");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
            mpgeo.Show();
        }
        protected void DrawArea_Click(object sender, EventArgs e)
        {
            if (txtArea.Text.Trim() != "" && Request.Form["AreaLocation"] != "" && txtRadius.Text != "")
            {
                hdnRadius.Value = txtRadius.Text.Trim();
                dt = new DataTable();
                Loc = new LocationBAL();
                Loc.ClientId = ClientId;
                Loc.AreaName = txtArea.Text.Trim();
                dt = Loc.IsAreaNameExists();
                if (dt.Rows.Count > 0)
                {
                    lblMessage2.Text = "Area Name Already Exists!!!";
                    lblMessage2.ForeColor = System.Drawing.Color.Red;
                    btnSaveArea.Visible = false;
                    //btnC.Visible = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(StartLat.Trim()) && !string.IsNullOrEmpty(StartLong.Trim()))
                    {

                        map.Visible = true;
                        btnSaveArea.Visible = true;
                        //btncncl.Visible = true;
                        lblMessage2.Text = "";
                        string script = "window.onload = function() { area(); };";
                        ClientScript.RegisterStartupScript(this.GetType(), "area", script, true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "area", "area()", true);
                    }
                    else
                    {
                        btnSaveArea.Visible = false;
                        //btncncl.Visible = false;
                    }
                }
            }
            else
            {
                lblMessage2.Text = "Please Fill All Field";
                lblMessage2.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected string getAreaData()
        {
            dt = new DataTable();
            dt.Columns.Add("Latiude");
            dt.Columns.Add("Longitude");
            dt.Columns.Add("Radius");
            try
            {
                dt.Rows.Add(StartLat.Trim(), StartLong.Trim(), Radius.Trim());
            }
            catch (Exception)
            {

            }
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }
        private void clearhiddenfieldandradio()
        {
            panel.Visible = false;
            paneladdarea.Visible = false;
            map.Visible = false;
            rbtnaddGeoFence.SelectedIndex = -1;
            hdnStartAddress.Value = String.Empty;
            hdnStartAddress.Value = String.Empty;
            RouteStartAddr.Value = String.Empty;
            RouteEndAddr.Value = String.Empty;
            hdnStartLat.Value = String.Empty;
            hdnStartLong.Value = String.Empty;
            RouteStartLat.Value = String.Empty;
            RouteStartLong.Value = String.Empty;
            RouteEndLat.Value = String.Empty;
            RouteEndLong.Value = String.Empty;
            hdnRadius.Value = String.Empty;
            txtMobileNo.Text = "";
            txtCode.Text = "";
            txtDesc.Text = "";
            RouteEndAddr.Value = "";
            RouteStartAddr.Value = "";
            txtArea.Text = "";
            btnAssign.Visible = false;
            btnSaveArea.Visible = false;
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
        protected void btnCloseGeo_Click(object sender, EventArgs e)
        {
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            string name = lblname.Text;
            if (name == "1")
            {
                string timingid = lblalertTimingid.Text;
                int fid = Convert.ToInt32(lblalertfeatureid.Text);
                probal = new ProfileBAL();
                GdvStngsDeleting(timingid, fid, false);
                lblManFields.Text = "Timing Details deleted successfully!";
                lblManFields.ForeColor = System.Drawing.Color.Green;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblManFields.ClientID + "').style.display='none'\",5000)</script>");
            }
            else
            {
                string locreqid = lblalertlocreqid.Text;
                Loc = new LocationBAL();
                Loc.LocReqFreqId = Convert.ToInt32(locreqid);
                if (Loc.DeleteLocReqFreqMgmt() == 1)
                {
                    lblMsg.Text = "Deleted successfully.";
                    BindPopUpGrid();
                    mpfreq.Show();
                }
                else
                {
                    lblMsg.Text = "Not deleted successfully";
                    mpfreq.Show();
                }
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }


        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalertTimingid.Text = "";
            lblalertfeatureid.Text = "";
        }
        protected void BindSetLocationPopup()
        {
            Loc = new LocationBAL();
            Loc.ProfileId = ProfileId;
            dt = Loc.GetSetLocation();
            ViewState["SetLocation"] = dt;
            grdsetlocation.DataSource = dt;
            grdsetlocation.DataBind();
        }
        protected void btnaddlocation_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["SetLocation"];
                if (Latitude.Trim() != "")
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = Latitude.Trim();
                    dr[1] = Longitude.Trim();
                    dr[2] = LocationName.Trim();
                    // dr[3] = 0;
                    dt.Rows.Add(dr);
                    msgpop.Text = "Successfully Added!";
                    msgpop.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    msgpop.Text = "Please enter the correct Location";
                    msgpop.ForeColor = System.Drawing.Color.Red;
                }
                Latitude = Longitude = LocationName = "";
                ViewState["SetLocation"] = dt;
                grdsetlocation.DataSource = dt;
                grdsetlocation.DataBind();
                mpsetlocation.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void grdsetlocation_parent_CheckedChanged(object sender, EventArgs e)
        {
            string idlist = ViewState["UserIdList"].ToString();
            CheckBox chkheader = (CheckBox)grdsetlocation.HeaderRow.FindControl("grdsetlocation_parent");
            foreach (GridViewRow row in grdsetlocation.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("chkboxrow");
                if (chkheader.Checked == true)
                {
                    chkrow.Checked = true;
                    //idlist = idlist + ((Label)row.FindControl("lblLocationName")).Text + ",";
                    //ViewState["UserIdList"] = idlist;
                }
                else
                {
                    chkrow.Checked = false;
                }
            }
            mpsetlocation.Show();
        }
        protected void chkboxrow_parent_CheckedChanged(object sender, EventArgs e)
        {
            string idlist = ViewState["UserIdList"].ToString();
            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.Parent.Parent;
            if (chk.Checked)
            {
                idlist = idlist + ((Label)gr.FindControl("lblLocationName")).Text + ",";
                ViewState["UserIdList"] = idlist;
            }
        }
        protected void btnapply_Click(object sender, EventArgs e)
        {
            string locationname = "", latitude = "", longitude = "";
            try
            {
                DataTable dtfinal = (DataTable)ViewState["FinalTable"];
                dtfinal = new DataTable();
                dtfinal.Columns.Add("Latitude");
                dtfinal.Columns.Add("Longitude");
                dtfinal.Columns.Add("Location");
                dtfinal.Columns.Add("IsInsert");

                for (int idx = 0; idx < grdsetlocation.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdsetlocation.Rows[idx].FindControl("chkboxrow"))).Checked)
                    {
                        locationname = ((Label)(grdsetlocation.Rows[idx].FindControl("lblLocationName"))).Text;
                        latitude = ((Label)(grdsetlocation.Rows[idx].FindControl("lblLatitude"))).Text;
                        longitude = ((Label)(grdsetlocation.Rows[idx].FindControl("lblLongitude"))).Text;
                        dtfinal.Rows.Add(latitude, longitude, locationname, 1);
                        ViewState["FinalTable"] = dtfinal;
                    }
                    else
                    {
                        locationname = ((Label)(grdsetlocation.Rows[idx].FindControl("lblLocationName"))).Text;
                        latitude = ((Label)(grdsetlocation.Rows[idx].FindControl("lblLatitude"))).Text;
                        longitude = ((Label)(grdsetlocation.Rows[idx].FindControl("lblLongitude"))).Text;
                        dtfinal.Rows.Add(latitude, longitude, locationname, 0);
                        ViewState["FinalTable"] = dtfinal;
                    }

                }
                if (ViewState["FinalTable"] != null)
                {
                    int res = 0, k = 0;
                    dtfinal = (DataTable)ViewState["FinalTable"];
                    foreach (DataRow obj in dtfinal.Rows)
                    {
                        string locname = obj["Location"].ToString();
                        string lat = obj["Latitude"].ToString();
                        string longi = obj["Longitude"].ToString();
                        int isinsert = Convert.ToInt32(obj["IsInsert"].ToString());
                        Loc = new LocationBAL();
                        Loc.ProfileId = ProfileId;
                        Loc.Latitude = lat;
                        Loc.Longitude = longi;
                        Loc.Location = locname;
                        Loc.IsInsert = isinsert;
                        res = Loc.InsertSetLocation();
                    }
                    if (res > 0)
                    {
                        k++;
                    }
                    if (k > 0)
                    {
                        msgpop.Text = "Changes Applied Successfully";
                        msgpop.ForeColor = System.Drawing.Color.Green;
                        mpsetlocation.Show();
                    }
                }

            }
            catch (Exception)
            {

            }
        }
        protected void grdsetlocation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int count = grdsetlocation.Rows.Count;
            int c = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Status = ((Label)e.Row.FindControl("lblisuploadlocation"));
                CheckBox chkbox = ((CheckBox)e.Row.FindControl("chkboxrow"));
                //CheckBox chkheader = (CheckBox)grdsetlocation.HeaderRow.FindControl("grdsetlocation_parent");
                if (Status.Text == "1")
                {
                    chkbox.Checked = true;
                    //chkheader.Checked = true;
                }
                else
                {
                    chkbox.Checked = false;
                }
                for (int idx = 0; idx < grdsetlocation.Rows.Count; idx++)
                {
                    if (((CheckBox)grdsetlocation.Rows[idx].FindControl("chkboxrow")).Checked)
                    {
                        c++;
                    }
                }
                CheckBox chkheader = (CheckBox)grdsetlocation.HeaderRow.FindControl("grdsetlocation_parent");
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
    }
}