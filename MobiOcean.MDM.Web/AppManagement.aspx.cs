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
    public partial class AppManagement : Base
    {
        int ClientId, UserId, RoleId, FeatureId, ProfileId, HdrId, AlowFromDay, AlowToDay, TotalDuration, DeptId;
        string AlowFromTime, AlowToTime, ProfileName;
        int CategoryId = 8;
        UserDeviceBAL usrdevice;
        GroupBAL grpbal;
        ProfileBAL probal;
        VikramSearch srch;
        GingerboxSrch GSrch;
        DataTable dt, dtMsg;
        SendSMSBAL sms;
        AnuSearch asrch;
        AppBAL app;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblSave.Text = "";
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
                txtProfileName.Text = ProfileName.ToString();
                BindGrid();                
                
            }
            lblMsg.Text = string.Empty;
            lblManFields.Text = string.Empty;
            Label1.Text = string.Empty;
            lblMultipleSlotSlctnMSGDayNumber.Text = string.Empty;
            lblMultipleSlotSlctnMSG.Text = string.Empty;
        }
        protected void BindGrid()
        {
            srch = new VikramSearch();
            grdDevice.DataSource = srch.srchProfilefeaturebycategoryid(ProfileId, CategoryId);
            grdDevice.DataBind();
        }
       
        protected void grdDevice_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                    CheckBox chk = (CheckBox)e.Row.FindControl("CBNotification");
                    CheckBox chk1 = (CheckBox)e.Row.FindControl("CBLog");
                    CheckBox chk2 = (CheckBox)e.Row.FindControl("CBAutoSync");
                    ImageButton imgbtnyes = (ImageButton)e.Row.FindControl("btnyes");
                    ImageButton imgbtnno = (ImageButton)e.Row.FindControl("btnNo");
                    CheckBox chk3 = (CheckBox)e.Row.FindControl("switchsize");
                    Label lblIsScheduleNeed = (Label)e.Row.FindControl("lblIsScheduleNeed");
                    try
                    {
                        chk.Checked = Convert.ToBoolean(grdDevice["NotificationOn"]);
                    }
                    catch (Exception) { }
                    try
                    {
                        chk1.Checked = Convert.ToBoolean(grdDevice["LogOn"]);
                    }
                    catch (Exception) { }
                    try
                    {
                        chk2.Checked = Convert.ToBoolean(grdDevice["AutoSyncOn"]);
                    }
                    catch (Exception) { }

                    try
                    {
                        chk3.Checked = Convert.ToBoolean(grdDevice["IsEnable"]);
                    }
                    catch (Exception) { chk3.Checked = false; }
                    if (chk3.Checked)
                    {
                        imgbtnyes.Visible = true;
                        lnkSchedule.Enabled = true;
                        lnkSchedule.Attributes["style"] = " background-color:#2A368B !important;";

                    }
                    else
                    {
                        imgbtnno.Visible = true;
                        lnkSchedule.Enabled = false;
                        lnkSchedule.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                    }
                    if (lblIsScheduleNeed.Text == "1")
                    {
                        lnkSchedule.Text = "Schedule";

                    }
                    else
                    {
                        lnkSchedule.Text = "&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;";


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
                //GridView GridFeature = (GridView)(gvr.Parent.Parent);
                Label lbl = ((Label)gvr.FindControl("lblId"));
                Label lbl1 = ((Label)gvr.FindControl("lblProfileId"));
                lblFeatureN.Text = "Schedule " + ((Label)gvr.FindControl("lblFeatureName")).Text;
                lblFeatureId.Text = lbl.Text;
                lblProfilePopId.Text = lbl1.Text;
                lblHdrId.Text = ((Label)gvr.FindControl("lblProfileFeatureId")).Text;
                lblCategoryIdMp.Text = CategoryId.ToString();
                lblMultipleSlotSlctnMSG.Text = string.Empty;
                lblManFields.Text = string.Empty;
                Label1.Text = string.Empty;
                lblMultipleSlotSlctnMSGDayNumber.Text = string.Empty;
                BindTimingGrid();

                if (lbl.Text == "30")
                {
                    BindGroupDDL(ddlAppGroupMain);
                    lblAppHead.Visible = true;
                    ddlAppGroupMain.Visible = true;
                    gdv.Columns[2].Visible = true;
                }
                else
                {

                    ddlAppGroupMain.Items.Clear();
                    ddlAppGroupMain.Items.Add(new ListItem("--- Select ---", "0"));
                    lblAppHead.Visible = false;
                    ddlAppGroupMain.Visible = false;
                    gdv.Columns[2].Visible = false;

                }
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
                mp.Show();
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
                    if (lblChanged.Text == "0")
                    {
                        lblChanged.Text = "1";
                    }
                    else
                    {
                        lblChanged.Text = "0";
                    }
                    ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + 8 + "','one');</script>");

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
                            lnkSchedule.Attributes["style"] = " background-color:#2A368B !important;";
                        }
                        else
                        {
                            lnkSchedule.Text = "&nbsp;&nbsp;&nbsp;N/A&nbsp;&nbsp;&nbsp;&nbsp;";
                            lnkSchedule.Enabled = false;
                            lnkSchedule.Attributes["style"] = " background-color:#6077C8 !important;color:White;";
                        }
                        ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + 8 + "','one');</script>");

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
            //        Response.Redirect("Feature.aspx");
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
            ListItem ls = new ListItem("--- Select ---", "0");
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
                        if (((Label)(gdv.Rows[idx].FindControl("lblDayNumber"))).Text.Trim() == AlowFromDay.ToString() && ((Label)(gdv.Rows[idx].FindControl("lblAppGroupId"))).Text.Trim() == lblAppGroupId.Text)
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

                            FeatureId = (Convert.ToInt32(lblFeatureId.Text));

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

                            try
                            {
                                probal.GroupId = Convert.ToInt32(lblAppGroupId.Text.Trim());
                            }
                            catch (Exception)
                            {
                                probal.GroupId = 0;
                            }
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
                //mp.Show();
            }
            else
            {
                lblManFields.Text = "Schedule added Succesfully";
                lblManFields.ForeColor = System.Drawing.Color.Green;
            }
            BindTimingGrid();
            mp.Show();
            ClearAllFields();
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
                    if (ddlAppGroupMain.SelectedValue == ((Label)(gdv.Rows[idx].FindControl("lblAppGroupId"))).Text.Trim())
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
            string Msg;
            probal = new ProfileBAL();
            dtMsg = new DataTable();
            try
            {
                probal.ProfileFeatureMappingId = int.Parse(lblHdrId.Text.Trim());
            }
            catch (Exception) { probal.ProfileFeatureMappingId = 0; }
            if (lblFeatureId.Text == "30")
            {
                dtMsg = probal.GetPhAndPrmsStngChangeForSMSAndEmailForApp();
                Msg = makeStngSMSForPhSettingsApp(dtMsg);
            }
            else
            {
                dtMsg = probal.GetPhAndPrmsStngChangeForSMSAndEmail();
                Msg = makeStngSMSForPhSettings(dtMsg);
            }

            if (Msg == "0")
            {
                Msg = null;
            }
            UpdateMsg(Msg, probal.ProfileFeatureMappingId);
            ClearAllFields();
            //ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + lblCategoryIdMp.Text + "','one');</script>");
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
        protected int UpdateMsg(string Msg, int ProfileFeatureId)
        {
            probal = new ProfileBAL();
            probal.ProfileFeatureMappingId = ProfileFeatureId;
            probal.Message = Msg;
            return probal.UpdateMsg();
        }
        protected void btnAppmgmt_Click(object sender, EventArgs e)
        {
            if (PanelAppMgmt.Visible)
            {
                PanelAppMgmt.Visible = false;
            }
            else
            {
                BindGroupDDL();
                BindAppMgmtGrid();
                PanelAppMgmt.Visible = true;
            }
        }
        protected void BindAppMgmtGrid()
        {
            app = new AppBAL();
            grdAppGrp.DataSource = app.GetAppGrpData();
            grdAppGrp.DataBind();
            if (RoleId == 1)
            {
                grdAppGrp.Columns[4].Visible = true;
                grdAppGrp.Columns[3].Visible = false;
            }
            else
            {
                grdAppGrp.Columns[3].Visible = true;
                grdAppGrp.Columns[4].Visible = false;
            }
        }
        protected void grdAppGrp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAppGrp.EditIndex = e.NewEditIndex;
            BindAppMgmtGrid();
        }
        protected void grdAppGrp_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                app = new AppBAL();
                GridViewRow gvr = grdAppGrp.Rows[e.RowIndex];
                app.AppGroupId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                app.AppGroupCode = ((TextBox)gvr.FindControl("txtGrpCode")).Text.Trim();
                app.AppGroupName = ((TextBox)gvr.FindControl("txtGrpName")).Text.Trim();

                string res = app.spAppGrpDtls();
                if (int.Parse(res) > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdAppGrp.EditIndex = -1;
                    BindAppMgmtGrid();
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
        protected void grdAppGrp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAppGrp.EditIndex = -1;
            BindAppMgmtGrid();
        }
        protected void grdAppGrp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            app = new AppBAL();
            GridViewRow gvr = grdAppGrp.Rows[e.RowIndex];
            try
            {
                app.AppGroupId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                if (app.DeleteAppGrpDtls() > 0)
                {
                    lblMsg.Text = "Deleted Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindAppMgmtGrid();
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
        protected void grdAppGrp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAppGrp.PageIndex = e.NewPageIndex;
            BindAppMgmtGrid();
        }
        protected void lnkbtnManageApp_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtnManageApp = sender as LinkButton;
                GridViewRow gvr = lnkbtnManageApp.NamingContainer as GridViewRow;
                Label lbl = ((Label)grdAppGrp.Rows[gvr.RowIndex].FindControl("lblId"));
                Label lbl1 = ((Label)grdAppGrp.Rows[gvr.RowIndex].FindControl("lblGrpName"));
                lblGrpId.Text = lbl.Text;
                lblGroupName.Text = lbl1.Text;
                BindRemoveSelectedGrid(Convert.ToInt32(lbl.Text));
                BindAddSelectedGrid(Convert.ToInt32(lbl.Text));
                lblPopMsg.Text = string.Empty;
                txtSelectedSearch.Text = txtRemoveSearch.Text = string.Empty;
                mpe.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void BindAddSelectedGrid(int GroupId)
        {
            asrch = new AnuSearch();            
            grdaddselected.DataSource = asrch.GetAppNameByProfileIdNotAssigned(GroupId, ClientId, ProfileId, txtSelectedSearch.Text);            
            grdaddselected.DataBind();
        }
        protected void BindRemoveSelectedGrid(int GroupId)
        {
            asrch = new AnuSearch();           
            grdremoveselected.DataSource = asrch.GetAppNameByProfileIdAssigned(GroupId, ClientId, ProfileId, txtRemoveSearch.Text);           
            grdremoveselected.DataBind();
        }
        protected void btnaddselected_Click(object sender, EventArgs e)
        {
            try
            {
                string ApplicationIdList = "";
                for (int idx = 0; idx < grdaddselected.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdaddselected.Rows[idx].FindControl("AchkRow_Parents"))).Checked)
                    {
                        ApplicationIdList = ApplicationIdList + ((Label)grdaddselected.Rows[idx].FindControl("AlblAppName")).Text + "~$";
                    }
                }
                if (ApplicationIdList != "")
                {
                    app = new AppBAL();
                    app.AppGroupId = Convert.ToInt32(lblGrpId.Text);
                    app.AppGroupName = lblGroupName.Text;
                    app.ApplicationIdList = ApplicationIdList;
                    app.LoggedBy = UserId.ToString();
                    //app.ClientId = ClientId;
                    app.ProfileId = ProfileId;
                    if (app.AssignProfileGrpName() == 1)
                    {
                        lblPopMsg.Text = "Group assigned successfully to selected App.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Green;
                        BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        mpe.Show();
                    }
                    else
                    {
                        lblPopMsg.Text = "Group not assigned to selected App.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Red;
                        mpe.Show();
                    }


                }
                else
                {
                    lblPopMsg.Text = "Please select App";
                    lblPopMsg.ForeColor = System.Drawing.Color.Red;
                    mpe.Show();
                }

            }
            catch (Exception)
            {
            }
        }
        protected void btnremoveselected_Click(object sender, EventArgs e)
        {
            try
            {
                string ApplicationIdList = "";
                for (int idx = 0; idx < grdremoveselected.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdremoveselected.Rows[idx].FindControl("RachkRow_Parents"))).Checked)
                    {
                        ApplicationIdList = ApplicationIdList + ((Label)grdremoveselected.Rows[idx].FindControl("RlblAppName")).Text + "~$";
                    }
                }
                if (ApplicationIdList != "")
                {
                    app = new AppBAL();
                    app.AppGroupId = 0;
                    app.ApplicationIdList = ApplicationIdList;
                    app.LoggedBy = UserId.ToString();
                    app.ProfileId = ProfileId;
                    //if (RoleId != 1)
                    //{
                    //app.ClientId = ClientId;
                    //}
                    //else
                    //{
                    //    app.ClientId = 0;
                    //}
                    if (app.UnAssignProfileGrpName() == 1)
                    {
                        lblPopMsg.Text = "Group unassigned successfully to selected App.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Green;
                        BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        mpe.Show();
                    }
                    else
                    {
                        lblPopMsg.Text = "Group not unassigned to selected App.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Red;
                        mpe.Show();
                    }


                }
                else
                {
                    lblPopMsg.Text = "Please select App";
                    lblPopMsg.ForeColor = System.Drawing.Color.Red;
                    mpe.Show();
                }


            }
            catch (Exception)
            {
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            PanelAppMgmt.Visible = false;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {

                grpbal = new GroupBAL();
                if (ChkValidation())
                {
                    grpbal.GroupName = ddlGroupName.SelectedItem.Text.Trim();
                    grpbal.GroupId = Convert.ToInt32(ddlGroupName.SelectedValue.ToString());
                    grpbal.ClientId = ClientId;
                    grpbal.AppCode = txtApplicationCode.Text.Trim();
                    grpbal.AppName = txtApplicationName.Text.Trim();
                    grpbal.LoggedBy = UserId.ToString();
                    grpbal.ProfileId = ProfileId;
                    int result = grpbal.IU_ProfileAppMaster();
                    //if (int.Parse(result) > 0)
                    if (result > 0)
                    {
                        Reset();
                        lblSave.Text = "Application Added successfully";
                        lblSave.ForeColor = System.Drawing.Color.Green;
                        // Response.Redirect("AppMaster.aspx");
                    }
                    else
                    {
                        lblSave.Text = "Application already exists!";
                        lblSave.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblSave.Text = "Fill all mandatory fields!";
                    lblSave.ForeColor = System.Drawing.Color.Red;
                }
            }

            catch (Exception) { }
            finally
            {
                grpbal = null;
            }
        }
        protected void BindGroupDDL()
        {
            ListItem ls = new ListItem("Select", "0");
            try
            {
                grpbal = new GroupBAL();
                //grpbal.GroupId = GroupId;
                ddlGroupName.Items.Clear();
                ddlGroupName.Items.Add(ls);
                ddlGroupName.DataSource = grpbal.GetAppGrpNameForDDL();
                ddlGroupName.DataTextField = "AppGroupName";
                ddlGroupName.DataValueField = "AppGroupId";
                ddlGroupName.DataBind();
            }
            catch (Exception) { }
            finally
            {
                ls = null;
                grpbal = null;
            }
        }
        protected bool ChkValidation()
        {
            if (txtApplicationCode.Text.Trim() == "" || txtApplicationName.Text.Trim() == "" || ddlGroupName.SelectedIndex <= 0)
                return false;
            else
                return true;
        }
        protected void Reset()
        {
            ddlGroupName.SelectedIndex = 0;
            txtApplicationName.Text = string.Empty;
            txtApplicationCode.Text = string.Empty;
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
        protected void btnSelectedSearch_Click(object sender, EventArgs e)
        {
            BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text.Trim()));
            mpe.Show();
        }
        protected void btnremoveSearch_Click(object sender, EventArgs e)
        {
            BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text.Trim()));
            mpe.Show();
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
        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkheader = (CheckBox)grdaddselected.HeaderRow.FindControl("chkAddHeader");
                foreach (GridViewRow row in grdaddselected.Rows)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("AchkRow_Parents");
                    if (chkheader.Checked == true)
                    {
                        chkrow.Checked = true;
                    }
                    else
                    {
                        chkrow.Checked = false;
                    }
                }
                mpe.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void chkRemoveHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkbox = (CheckBox)grdremoveselected.HeaderRow.FindControl("chkRemoveHeader");
                foreach (GridViewRow row in grdremoveselected.Rows)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("RachkRow_Parents");
                    if (chkbox.Checked == true)
                    {
                        chkrow.Checked = true;
                    }
                    else
                    {
                        chkrow.Checked = false;
                    }
                }
                mpe.Show();
            }
            catch (Exception)
            {

            }
        }
    }
}
