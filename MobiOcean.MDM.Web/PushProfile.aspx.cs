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
    public partial class PushProfile : Base
    {
        int ClientId, RoleId, UserId, DeptId, ProfileId;
        string ProfileName;
        DeptBAL Dept;
        GingerboxSrch GSrch;
        DataTable dt;
        ProfileBAL profilebal;
        SensorBAL sensor;
        ProfileUserMappingBAL profileusermapbal;
        SendSMSBAL sms;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            try
            {
                ProfileId = Convert.ToInt32(Session["ProfileId"].ToString());
                ProfileName = Session["ProfileName"].ToString();

            }
            catch (Exception)
            {
                ProfileId = 0;
            }
            if (ProfileId == 0)
            {
                Response.Redirect("profilemaster.aspx");
            }
            if (!IsPostBack)
            {
                txtProfileName.Text = ProfileName.ToString();
                //BindProfileDetail();
                BindBranchName();
                BindDeptName();
                BindDeptBranch();
            }

        }
        protected void BindBranchName()
        {
            try
            {
                Dept = new DeptBAL();
                ListItem li = new ListItem("--- Select ---", "0");
                Dept.ClientId = ClientId;
                ddlBranchName.Items.Clear();
                ddlBranchName.Items.Add(li);
                ddlBranchName.DataSource = Dept.GetBranchName();
                ddlBranchName.DataTextField = "BranchName";
                ddlBranchName.DataValueField = "BranchId";
                ddlBranchName.DataBind();
            }
            catch (Exception)
            {

            }
        }
        protected void BindDeptName()
        {
            try
            {
                Dept = new DeptBAL();
                ListItem li = new ListItem("--- Select ---", "0");
                Dept.ClientId = ClientId;
                ddlDepartmentName.Items.Clear();
                ddlDepartmentName.Items.Add(li);
                ddlDepartmentName.DataSource = Dept.GetDptNameDDL();
                ddlDepartmentName.DataTextField = "DeptName";
                ddlDepartmentName.DataValueField = "DeptId";
                ddlDepartmentName.DataBind();
            }
            catch (Exception)
            {

            }
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkValidations())
                {
                    profilebal = new ProfileBAL();
                    profilebal.ProfileGroupId = 0;
                    profilebal.ClientId = ClientId;
                    profilebal.IsEnable = 1;
                    profilebal.BranchId = Convert.ToInt32(ddlBranchName.SelectedValue.ToString());
                    profilebal.DeptId = Convert.ToInt32(ddlDepartmentName.SelectedValue.ToString());
                    profilebal.ProfileId = ProfileId;
                    profilebal.AppliedDateTime = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
                    profilebal.LoggedBy = UserId.ToString();
                    profilebal.Status = 0;
                    string res = profilebal.spProfileBranchDeptMapping();
                    if (int.Parse(res) > 0)
                    {
                        lblMsg.Text = "Profile applied successfully!";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        ddlBranchName.SelectedIndex = 0;
                        ddlDepartmentName.SelectedIndex = 0;
                        BindDeptBranch();
                        SendUpdateMsgByBranch(profilebal.BranchId, profilebal.DeptId);
                        //Response.Redirect("ProfileUserGroupMapping.aspx");
                    }
                    else
                    {
                        lblMsg.Text = "This Profile is already assigned to the selected Branch and Department.";
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
        protected bool ChkValidations()
        {
            if (ddlBranchName.SelectedIndex <= 0 || ddlDepartmentName.SelectedIndex <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void BindGrid()
        {
            GSrch = new GingerboxSrch();
            if (RoleId == 1 || RoleId == 2)
            {
                grdUser.DataSource = GSrch.SrchUser(ClientId, "", "", "", "", 0, 0, 0);
            }
            else if (RoleId == 3)
            {
                grdUser.DataSource = GSrch.SrchUser(ClientId, "", "", "", "", 0, DeptId, 0);
            }
            else
            {
                grdUser.DataSource = GSrch.SrchUser(ClientId, "", "", "", "", UserId, 0, 0);
            }
            grdUser.DataBind();
        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string UserIdList = ViewState["UserIdList"].ToString();
            for (int idx = 0; idx < grdUser.Rows.Count; idx++)
            {
                if (((CheckBox)(grdUser.Rows[idx].FindControl("AchkRow_Parents"))).Checked)
                {
                    UserIdList = UserIdList + ((Label)grdUser.Rows[idx].FindControl("lblUserId")).Text + ",";
                }
            }
            if (UserIdList != "")
                ViewState["UserIdList"] = UserIdList;
            grdUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void Assign_Click(object sender, EventArgs e)
        {
            try
            {
                string UserIdList = "";// ViewState["UserIdList"].ToString();
                int Usercount = 0;
                for (int idx = 0; idx < grdUser.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdUser.Rows[idx].FindControl("AchkRow_Parents"))).Checked)
                    {
                        Usercount++;
                    }
                }
                profilebal = new ProfileBAL();
                profilebal.ClientId = ClientId;
                profilebal.ProfileId = ProfileId;
                profilebal.UserCount = Usercount;

                if (profilebal.CheckEnableFeaturesFromUSER())
                {
                    for (int idx = 0; idx < grdUser.Rows.Count; idx++)
                    {
                        if (((Label)(grdUser.Rows[idx].FindControl("lblIsChanged"))).Text.ToString().Trim() == "1")
                        {
                            UserIdList = UserIdList + ((Label)grdUser.Rows[idx].FindControl("lblUserId")).Text + ",";
                            profileusermapbal = new ProfileUserMappingBAL();
                            profileusermapbal.ProfileUserId = 0;
                            profileusermapbal.ClientId = ClientId;
                            profileusermapbal.UserId = Convert.ToInt32(((Label)grdUser.Rows[idx].FindControl("lblUserId")).Text);
                            profileusermapbal.ProfileId = ProfileId;
                            profileusermapbal.LoggedBy = UserId;
                            profileusermapbal.AppliedDateTime = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
                            profileusermapbal.IsEnable = ((CheckBox)(grdUser.Rows[idx].FindControl("AchkRow_Parents"))).Checked ? 1 : 0;
                            profileusermapbal.Status = 0;
                            profileusermapbal.spProfileUserMapping();
                        }
                    }
                    if (UserIdList != "" && UserIdList.Length > 0)
                    {
                        //UserIdList = UserIdList.Substring(0, UserIdList.Length - 1);
                        //string[] Ids = UserIdList.Split(',');
                        //foreach (string Id in Ids)
                        //{

                        //}
                        SendUpdateMsg(UserIdList);
                        lblMsg.Text = "Successfully Assigned";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Back();
                    }
                    else
                    {
                        lblMsg.Text = "You didn't change in previous list.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "You don't have license to enable some feature in this profile. Please purchase more license.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "Not Assigned!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Back();
        }
        private void Back()
        {
            Session["ProfileId"] = null;
            Response.Redirect("profilemaster.aspx");
        }
        protected void btnchangeSensor_Click(object sender, EventArgs e)
        {
            BindSensorName();
            BindProfileName(ddlProfileName);
            BindSensorGrid();
            firsttab.Attributes["class"] = "sdmtm";
            Secondtab.Attributes["class"] = "sdmtm";
            Thirdtab.Attributes["class"] = "active sdmtm";
            Fourthtab.Attributes["class"] = "sdmtm";
            MultiView1.ActiveViewIndex = 2;
        }
        protected void btnchangeBranch_Click(object sender, EventArgs e)
        {
            BindBranchName();
            BindDeptName();
            BindDeptBranch();
            firsttab.Attributes["class"] = "active sdmtm";
            Secondtab.Attributes["class"] = "sdmtm";
            Thirdtab.Attributes["class"] = "sdmtm";
            Fourthtab.Attributes["class"] = "sdmtm";
            MultiView1.ActiveViewIndex = 0;

        }
        protected void btnchangeUser_Click(object sender, EventArgs e)
        {
            BindGrid();
            ViewState["UserIdList"] = "";
            firsttab.Attributes["class"] = "sdmtm";
            Secondtab.Attributes["class"] = "active sdmtm";
            Thirdtab.Attributes["class"] = "sdmtm";
            Fourthtab.Attributes["class"] = "sdmtm";
            MultiView1.ActiveViewIndex = 1;

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Back();
        }
        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int count = grdUser.Rows.Count;
                int c = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView grdUser1 = (DataRowView)e.Row.DataItem;
                    CheckBox cb = (CheckBox)e.Row.FindControl("AchkRow_Parents");
                    Label lblStatus = (Label)e.Row.FindControl("lblProfileId");
                    Label lblIsEnable = (Label)e.Row.FindControl("lblIsEnable");

                    if (lblStatus.Text == ProfileId.ToString() && lblIsEnable.Text.Trim() == "1")
                    {
                        cb.Checked = true;
                    }
                    else
                        cb.Checked = false;
                }
                for (int idx = 0; idx < grdUser.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdUser.Rows[idx].FindControl("AchkRow_Parents"))).Checked)
                    {
                        c++;
                    }
                }
                CheckBox chkheader = (CheckBox)grdUser.HeaderRow.FindControl("ChkUserHeader");
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
        protected void BindSensorName()
        {
            try
            {
                profilebal = new ProfileBAL();
                profilebal.ClientId = ClientId;
                ListItem li = new ListItem("--- Select ---", "0");
                ddlSensorName.Items.Clear();
                ddlSensorName.Items.Add(li);
                ddlSensorName.DataSource = profilebal.GetSensorNameByClientId();
                ddlSensorName.DataTextField = "SensorName";
                ddlSensorName.DataValueField = "SensorId";
                ddlSensorName.DataBind();
            }
            catch (Exception)
            {

            }
        }
        protected void BindProfileName(DropDownList ddl)
        {
            try
            {
                profilebal = new ProfileBAL();
                profilebal.ClientId = ClientId;
                ListItem li = new ListItem("--- Select ---", "0");
                ddl.Items.Clear();
                ddl.Items.Add(li);
                ddl.DataSource = profilebal.GetProfileData();
                ddl.DataTextField = "ProfileName";
                ddl.DataValueField = "ProfileId";
                ddl.DataBind();
            }
            catch (Exception)
            {

            }
        }
        protected void BindSensorGrid()
        {
            try
            {
                profilebal = new ProfileBAL();
                profilebal.ClientId = ClientId;
                profilebal.ProfileId = ProfileId;
                grdsensor.DataSource = profilebal.GetWifiSensorDetails();
                grdsensor.DataBind();
            }
            catch (Exception)
            {

            }
        }
        protected void btnSaveSensor_Click(object sender, EventArgs e)
        {
            try
            {
                profilebal = new ProfileBAL();
                profilebal.ClientId = ClientId;
                profilebal.UserId = UserId;
                profilebal.SensorId = Convert.ToInt32(ddlSensorName.SelectedValue.ToString());
                profilebal.ProfileId = Convert.ToInt32(ddlProfileName.SelectedValue.ToString());
                int res = profilebal.InsertWifiSensor();
                if (res > 0)
                {
                    lblMsg.Text = "Saved Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindSensorGrid();
                }
                else
                {
                    lblMsg.Text = "Already Exists!!!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnCancelSensor_Click(object sender, EventArgs e)
        {
            ddlSensorName.SelectedIndex = 0;
            ddlProfileName.SelectedIndex = 0;
        }
        protected void grdsensor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdsensor.EditIndex = e.NewEditIndex;
            BindSensorGrid();
        }
        protected void grdsensor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow gvr = grdsensor.Rows[e.RowIndex];
                profilebal = new ProfileBAL();
                lblalertid.Text = ((Label)gvr.FindControl("lblId")).Text;
                mpdelete.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void grdsensor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow gvr = grdsensor.Rows[e.RowIndex];
                profilebal = new ProfileBAL();
                profilebal.ClientId = ClientId;
                profilebal.UserId = UserId;
                profilebal.WifiSensorId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text);
                profilebal.SensorId = Convert.ToInt32(((Label)gvr.FindControl("lblSensorId")).Text);
                profilebal.ProfileId = Convert.ToInt32(((DropDownList)gvr.FindControl("ddlEProfileName")).SelectedValue.ToString());
                int res = profilebal.InsertWifiSensor();
                if (res > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdsensor.EditIndex = -1;
                    BindSensorGrid();
                }
                else
                {
                    lblMsg.Text = "Not Updated!!!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void grdsensor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdsensor.EditIndex = -1;
            BindSensorGrid();
        }
        protected void grdsensor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int count = grdsensor.Rows.Count;
            int c = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label IsRead = ((Label)e.Row.FindControl("lblIsRead"));
                CheckBox chkbox = ((CheckBox)e.Row.FindControl("chksensor"));
                if (IsRead.Text == "0")
                {
                    chkbox.Checked = false;
                }
                else
                {
                    chkbox.Checked = true;
                }
                for (int idx = 0; idx < grdsensor.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdsensor.Rows[idx].FindControl("chksensor"))).Checked)
                    {
                        c++;
                    }
                }
                CheckBox chkheader = (CheckBox)grdsensor.HeaderRow.FindControl("chkboxSensorHeader");
                if (c == count)
                {

                    chkheader.Checked = true;
                }
                else
                {
                    chkheader.Checked = false;
                }
            }

        }
        protected void AchkRow_Parents_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gvr = ((GridViewRow)((CheckBox)sender).NamingContainer);
            Label lbl = ((Label)gvr.FindControl("lblIsChanged"));
            if (lbl.Text == "0")
            {
                lbl.Text = "1";
            }
            else
            {
                lbl.Text = "0";
            }
        }
        protected void SendUpdateMsg(string UserIdList)
        {
            UserIdList = UserIdList.Substring(0, UserIdList.Length - 1);
            string[] Ids = UserIdList.Split(',');
            foreach (string Id in Ids)
            {
                UserDeviceBAL usrdevice = new UserDeviceBAL();
                dt = new DataTable();
                usrdevice.UserId = Convert.ToInt32(Id);
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
        }
        protected void SendUpdateMsgByBranch(int BranchId, int DeptId)
        {
            UserDeviceBAL usrdevice = new UserDeviceBAL();
            dt = new DataTable();
            usrdevice.branchId = BranchId;
            usrdevice.deptId = DeptId;
            dt = usrdevice.GetUserDeviceByDeptAndBranch();
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
        protected void Yes_Click(object sender, EventArgs e)
        {
            profilebal = new ProfileBAL();
            profilebal.WifiSensorId = Convert.ToInt32(lblalertid.Text);
            int res = profilebal.DeleteWifiSensorDetails();
            if (res > 0)
            {
                lblMsg.Text = "Deleted Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                grdsensor.EditIndex = -1;
                BindSensorGrid();
            }
            else
            {
                lblMsg.Text = "Not deleted!!!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalertid.Text = "";
        }
        protected void btnApplyChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string SensorId = "", sensoridc = "";
                for (int idx = 0; idx < grdsensor.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdsensor.Rows[idx].FindControl("chksensor"))).Checked)
                    {
                        SensorId = SensorId + ((Label)grdsensor.Rows[idx].FindControl("lblId")).Text + ",";

                    }
                    else
                    {
                        sensoridc = sensoridc + ((Label)grdsensor.Rows[idx].FindControl("lblId")).Text + ",";
                    }
                }
                if (SensorId != "")
                {
                    sensor = new SensorBAL();
                    SensorId = SensorId.TrimEnd(',');
                    string[] senide = SensorId.Split(',');
                    foreach (string n in senide)
                    {
                        sensor.Isenable = 1;
                        sensor.UserId = UserId;
                        sensor.ClientId = ClientId;
                        sensor.WifiSensorId = Convert.ToInt32(n);
                        int res = sensor.AssignSensorToProfile();
                        if (res > 0)
                        {
                            lblMsg.Text = "Changes Applied Successfully";
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                            BindSensorGrid();
                        }
                        else
                        {
                            lblMsg.Text = "Changes Not Saved";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                if (sensoridc != "")
                {
                    sensor = new SensorBAL();
                    sensoridc = sensoridc.TrimEnd(',');
                    string[] senid = sensoridc.Split(',');
                    foreach (string n in senid)
                    {
                        sensor.Isenable = 0;
                        sensor.UserId = UserId;
                        sensor.ClientId = ClientId;
                        sensor.WifiSensorId = Convert.ToInt32(n);
                        int res = sensor.AssignSensorToProfile();
                        if (res > 0)
                        {
                            lblMsg.Text = "Changes Applied Successfully";
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                            BindSensorGrid();
                        }
                        else
                        {
                            lblMsg.Text = "Changes Not Saved";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        protected void BindDeptBranch()
        {
            sensor = new SensorBAL();
            sensor.ClientId = ClientId;
            grdbranchdept.DataSource = sensor.GetBranchDeptData();
            grdbranchdept.DataBind();
        }
        protected void grdbranchdept_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int count = grdbranchdept.Rows.Count;
            int c = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label IsRead = ((Label)e.Row.FindControl("lblEnable"));
                CheckBox chkbox = ((CheckBox)e.Row.FindControl("chk"));
                if (IsRead.Text == "0")
                {
                    chkbox.Checked = false;
                }
                else
                {
                    chkbox.Checked = true;
                }
                for (int idx = 0; idx < grdbranchdept.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdbranchdept.Rows[idx].FindControl("chk"))).Checked)
                    {
                        c++;
                    }
                }
                CheckBox chkheader = (CheckBox)grdbranchdept.HeaderRow.FindControl("ChkBranchHeader");
                if (c == count)
                {

                    chkheader.Checked = true;
                }
                else
                {
                    chkheader.Checked = false;
                }
            }
        }
        protected void ApplyChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string ProfileBranchdeptId = "";
                for (int idx = 0; idx < grdbranchdept.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdbranchdept.Rows[idx].FindControl("chk"))).Checked)
                    {
                        ProfileBranchdeptId = ProfileBranchdeptId + ((Label)grdbranchdept.Rows[idx].FindControl("lblBDId")).Text + ",";
                    }
                }
                profilebal = new ProfileBAL();
                profilebal.ClientId = ClientId;
                profilebal.ProfileId = ProfileId;
                profilebal.ProfileBranchDeptIds = ProfileBranchdeptId.TrimEnd(',');

                if (profilebal.CheckEnableFeaturesFromBrnandDpt())
                {
                    for (int idx = 0; idx < grdbranchdept.Rows.Count; idx++)
                    {
                        sensor = new SensorBAL();
                        sensor.ProfileBranchDeptId = Convert.ToInt32(((Label)grdbranchdept.Rows[idx].FindControl("lblBDId")).Text);
                        sensor.UserId = UserId;
                        sensor.ClientId = ClientId;
                        if (((CheckBox)(grdbranchdept.Rows[idx].FindControl("chk"))).Checked)
                        {
                            sensor.Checked = 1;
                        }
                        else
                        {
                            sensor.Checked = 0;
                        }
                        int res = sensor.AssignBranchDeptToProfile();
                    }

                    lblMsg.Text = "Changes Applied Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindDeptBranch();
                }
                else
                {
                    lblMsg.Text = "You don't have license to enable some feature in this profile. Please purchase more license.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
        }
        //protected void ApplyChanges_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string ProfileBranchdeptId = "", ProfileBranchdeptIdc = "";
        //        for (int idx = 0; idx < grdbranchdept.Rows.Count; idx++)
        //        {
        //            if (((CheckBox)(grdbranchdept.Rows[idx].FindControl("chk"))).Checked)
        //            {
        //                ProfileBranchdeptId = ProfileBranchdeptId + ((Label)grdbranchdept.Rows[idx].FindControl("lblBDId")).Text + ",";
        //            }
        //            else
        //            {
        //                ProfileBranchdeptIdc = ProfileBranchdeptIdc + ((Label)grdbranchdept.Rows[idx].FindControl("lblBDId")).Text + ",";
        //            }
        //        }
        //        if (ProfileBranchdeptId != "")
        //        {
        //            sensor = new SensorBAL();
        //            ProfileBranchdeptId = ProfileBranchdeptId.TrimEnd(',');
        //            string[] senide = ProfileBranchdeptId.Split(',');
        //            foreach (string n in senide)
        //            {
        //                sensor.Checked = 1;
        //                sensor.UserId = UserId;
        //                sensor.ClientId = ClientId;
        //                sensor.ProfileBranchDeptId = Convert.ToInt32(n);
        //                int res = sensor.AssignBranchDeptToProfile();
        //                if (res > 0)
        //                {
        //                    lblMsg.Text = "Changes Applied Successfully";
        //                    lblMsg.ForeColor = System.Drawing.Color.Green;
        //                    BindDeptBranch();
        //                }
        //                else
        //                {
        //                    lblMsg.Text = "Changes Not Saved";
        //                    lblMsg.ForeColor = System.Drawing.Color.Red;
        //                }
        //            }
        //        }
        //        if (ProfileBranchdeptIdc != "")
        //        {
        //            sensor = new SensorBAL();
        //            ProfileBranchdeptIdc = ProfileBranchdeptIdc.TrimEnd(',');
        //            string[] senide1 = ProfileBranchdeptIdc.Split(',');
        //            foreach (string n in senide1)
        //            {
        //                sensor.Checked = 0;
        //                sensor.UserId = UserId;
        //                sensor.ClientId = ClientId;
        //                sensor.ProfileBranchDeptId = Convert.ToInt32(n);
        //                int res = sensor.AssignBranchDeptToProfile();
        //                if (res > 0)
        //                {
        //                    lblMsg.Text = "Changes Applied Successfully";
        //                    lblMsg.ForeColor = System.Drawing.Color.Green;
        //                    BindDeptBranch();
        //                }
        //                else
        //                {
        //                    lblMsg.Text = "Changes Not Saved";
        //                    lblMsg.ForeColor = System.Drawing.Color.Red;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfileMaster.aspx");
        }
        protected void btnBackToProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfileMaster.aspx");
        }
        protected void BindGridDevice()
        {
            GSrch = new GingerboxSrch();
            if (RoleId == 1 || RoleId == 2)
            {
                grdDevice.DataSource = GSrch.GetDeviceList(ClientId, 0, 0, 0);
            }
            else if (RoleId == 3)
            {
                grdDevice.DataSource = GSrch.GetDeviceList(ClientId, 0, DeptId, 0);
            }
            else
            {
                grdDevice.DataSource = GSrch.GetDeviceList(ClientId, UserId, 0, 0);
            }
            grdDevice.DataBind();
        }
        protected void btnchangeDevice_Click(object sender, EventArgs e)
        {
            BindGridDevice();
            firsttab.Attributes["class"] = "sdmtm";
            Secondtab.Attributes["class"] = "sdmtm";
            Thirdtab.Attributes["class"] = "sdmtm";
            Fourthtab.Attributes["class"] = "active sdmtm";
            MultiView1.ActiveViewIndex = 3;
        }
        protected void chkheader_Click(object sender, EventArgs e)
        {
            CheckBox chkheader = (CheckBox)grdDevice.HeaderRow.FindControl("chkheader");
            foreach (GridViewRow row in grdDevice.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("chkRow");
                if (chkheader.Checked == true)
                {
                    chkrow.Checked = true;
                }
                else
                {
                    chkrow.Checked = false;
                }
            }
        }
        protected void btnAssignDevice_Click(object sender, EventArgs e)
        {
            try
            {
                int result = 0;
                string idlist = "", idlist1 = "";
                for (int idx = 0; idx < grdDevice.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdDevice.Rows[idx].FindControl("chkRow"))).Checked)
                    {
                        idlist = idlist + ((Label)(grdDevice.Rows[idx].FindControl("lblDId"))).Text + ",";
                    }
                    else
                    {
                        idlist1 = idlist1 + ((Label)(grdDevice.Rows[idx].FindControl("lblDId"))).Text + ",";
                    }
                }
                profileusermapbal = new ProfileUserMappingBAL();
                profileusermapbal.ClientId = ClientId;
                profileusermapbal.AppliedDateTime = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
                profileusermapbal.ProfileId = ProfileId;
                profileusermapbal.LoggedBy = UserId;
                profileusermapbal.Status = 0;
                if (idlist != "")
                {
                    idlist = idlist.Trim(',');
                    string[] id = idlist.Split(',');
                    foreach (var deviceid in id)
                    {
                        profileusermapbal.DeviceId = Convert.ToInt32(deviceid);
                        profileusermapbal.IsEnable = 1;
                        result = profileusermapbal.spProfileDeviceMapping();
                    }
                }
                if (idlist1 != "")
                {
                    idlist1 = idlist1.Trim(',');
                    string[] id1 = idlist1.Split(',');
                    foreach (var deviceid1 in id1)
                    {
                        profileusermapbal.DeviceId = Convert.ToInt32(deviceid1);
                        profileusermapbal.IsEnable = 0;
                        result = profileusermapbal.spProfileDeviceMapping();
                    }
                }
                if (result > 0)
                {
                    lblMsg.Text = "Changes Applied Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindGridDevice();
                }
                else
                {
                    lblMsg.Text = "Changes Not Saved";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void grdDevice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int count = grdDevice.Rows.Count;
                int c = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox cb = (CheckBox)e.Row.FindControl("chkRow");
                    Label lblIsEnable = (Label)e.Row.FindControl("lblDeviceEnable");

                    if (lblIsEnable.Text.Trim() == "1")
                    {
                        cb.Checked = true;
                    }
                    else
                        cb.Checked = false;

                }
                for (int idx = 0; idx < grdDevice.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdDevice.Rows[idx].FindControl("chkRow"))).Checked)
                    {
                        c++;
                    }
                }
                CheckBox chkheader = (CheckBox)grdDevice.HeaderRow.FindControl("chkheader");
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
        protected void ChkUserHeader_Click(object sender, EventArgs e)
        {
            CheckBox chkheader = (CheckBox)grdUser.HeaderRow.FindControl("ChkUserHeader");
            foreach (GridViewRow row in grdUser.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("AchkRow_Parents");
                Label lbl = (Label)row.FindControl("lblIsChanged");
                if (chkheader.Checked == true)
                {
                    chkrow.Checked = true;
                    lbl.Text = "1";
                }
                else
                {
                    chkrow.Checked = false;
                }
            }
        }
        protected void ChkBranchHeader_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkheader = (CheckBox)grdbranchdept.HeaderRow.FindControl("ChkBranchHeader");
            foreach (GridViewRow row in grdbranchdept.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("chk");
                if (chkheader.Checked == true)
                {
                    chkrow.Checked = true;
                }
                else
                {
                    chkrow.Checked = false;
                }
            }
        }
        protected void chkboxSensorHeader_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkheader = (CheckBox)grdsensor.HeaderRow.FindControl("chkboxSensorHeader");
            foreach (GridViewRow row in grdsensor.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("chksensor");
                if (chkheader.Checked == true)
                {
                    chkrow.Checked = true;
                }
                else
                {
                    chkrow.Checked = false;
                }
            }
        }
    }
}
