using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
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
    public partial class AppTerminalMGM : Base
    {
        int ClientId, UserId, RoleId;
        bool help = true;
        DataTable dt;
        GingerboxSrch srch;
        AppBAL appbal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            if (!IsPostBack)
            {
                BindGrid();

            }
        }
        public void BindGrid()
        {
            srch = new GingerboxSrch();
            dt = new DataTable();
            if (ViewState["dtUser"] == null)
            {
                dt = srch.SrchUserDeviceTerminalList(ClientId, txtUserName.Text, txtDeviceName.Text, txtBranchName.Text, txtDept.Text);
                if (dt != null)
                {
                    ViewState["dtUser"] = dt;
                    ViewState["dtUserCount"] = dt.Rows.Count;
                }
            }
            else
            if (!(string.IsNullOrEmpty(txtUserName.Text) && string.IsNullOrEmpty(txtDeviceName.Text) && string.IsNullOrEmpty(txtBranchName.Text) && string.IsNullOrEmpty(txtDept.Text)))
            {
                if (help)
                {
                    dt = srch.SrchUserDeviceTerminalList(ClientId, txtUserName.Text, txtDeviceName.Text, txtBranchName.Text, txtDept.Text);
                    ViewState["dtUser"] = dt;
                }
                else
                    dt = (DataTable)ViewState["dtUser"];
            }
            else
                if (ViewState["dtUser"] != null && ((DataTable)ViewState["dtUser"]).Rows.Count != Convert.ToInt32(ViewState["dtUserCount"]))
            {
                if (help)
                {
                    dt = srch.SrchUserDeviceTerminalList(ClientId, txtUserName.Text, txtDeviceName.Text, txtBranchName.Text, txtDept.Text);
                    ViewState["dtUser"] = dt;
                }
                else
                    dt = (DataTable)ViewState["dtUser"];
            }
            else
                dt = (DataTable)ViewState["dtUser"];
            if (dt != null && dt.Rows.Count > 0)
            {
                grdUsr.DataSource = dt;
                grdUsr.DataBind();
            }           
        }



        protected void Search_Click(object sender, EventArgs e)
        {
            BindGrid();
        }


        protected void grdUsr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUsr.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void grdUsr_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            help = false;
            CheckBox ChkBoxHeader = (CheckBox)grdUsr.HeaderRow.FindControl("CheckAll");
            dt = new DataTable();
            dt = (DataTable)ViewState["dtUser"];
            if (dt != null && dt.Rows.Count > 0)
            {
                //int pages = grdUsr.PageCount;
                //int b = grdUsr.PageIndex;
                //for (int i = 0; i < grdUsr.PageCount; i++)
                //{
                //Set Page Index
                //grdUsr.SetPageIndex(i);
                grdUsr.AllowPaging = false;
                foreach (GridViewRow row in grdUsr.Rows)
                {
                    int pageIndex = grdUsr.PageIndex;
                    int index = row.RowIndex;
                    if (pageIndex > 0)
                    {
                        index = row.RowIndex + (pageIndex * 10);
                    }
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("User");
                    if (ChkBoxHeader.Checked == true)
                    {
                        dt.Rows[index]["Status"] = 0;
                        ChkBoxRows.Checked = true;
                    }
                    else
                    {
                        dt.Rows[index]["Status"] = 1;
                        ChkBoxRows.Checked = false;
                    }
                    ViewState["dtUser"] = dt;
                }
                grdUsr.AllowPaging = true;
                //}
                //grdUsr.SetPageIndex(b);

            }
        }

        protected void User_CheckedChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt = (DataTable)ViewState["dtUser"];
            int setIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            int index = setIndex;
            int pageIndex = grdUsr.PageIndex;
            if (pageIndex > 0)
            {
                index = index + (pageIndex * 10);
            }
            CheckBox cb = (CheckBox)grdUsr.Rows[setIndex].FindControl("User");
            if (cb.Checked == true)
            {
                dt.Rows[index]["Status"] = 0;
            }
            else
            {
                dt.Rows[index]["Status"] = 1;
                CheckBox ChkBoxHeader = (CheckBox)grdUsr.HeaderRow.FindControl("CheckAll");
                ChkBoxHeader.Checked = false;
            }
            ViewState["dtUser"] = dt;
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("AppMarketId"), new DataColumn("AppURL") });
                dt.Rows.Add(lblfinalOSId.Text, Constant.URL + "PublicApk/OS/" + lblfinalOSPath.Text);
                InsertList(dt, 0);
                lblMsg.Text = "Successfully Completed";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {
                lblMsg.Text = "Something went wrong";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void No_Click(object sender, EventArgs e)
        {
        }
        protected void btnOsUpgrade_Click(object sender, EventArgs e)
        {
            srch = new GingerboxSrch();
            DataTable dt = new DataTable();
            dt = srch.SrchOSList1(ClientId);
            lblfinalOSPath.Text = dt.Rows[0]["Os_Path"].ToString();
            lblfinalOSId.Text = dt.Rows[0]["OsId"].ToString();
            lblosUpgradetext.Text = "Are you sure you Going to Upgrade Os Package (" + dt.Rows[0]["AppPackage"].ToString() + ")?";
            mpdelete.Show();
        }

        protected void btnInstallNewApp_Click(object sender, EventArgs e)
        {
            lblClickId.Text = "1";
            BindAppList();
            mp.Show();
        }

        protected void btnUpgradeAPP_Click(object sender, EventArgs e)
        {
            lblClickId.Text = "2";
            BindAppList();
            mp.Show();
        }
        protected void btnUnIstallApp_Click(object sender, EventArgs e)
        {
            lblClickId.Text = "3";
            BindAppList();
            mp.Show();
        }
        private void InsertList(DataTable apdt, int btn)
        {

            for (int h = 0; h < apdt.Rows.Count; h++)
            {
                DataTable dt1 = new DataTable();
                DataTable dt = new DataTable();
                dt1.Columns.AddRange(new DataColumn[2] { new DataColumn("MobileNo"), new DataColumn("AppPath") });
                dt.Columns.AddRange(new DataColumn[15] { new DataColumn("AppMarketAssignId"), new DataColumn("ClientId"), new DataColumn("UserId"), new DataColumn("DeviceId"), new DataColumn("AppMarketId"), new DataColumn("Status"), new DataColumn("CreatedBy"), new DataColumn("CreationDate"), new DataColumn("UpdatedBy"), new DataColumn("UpdationDate"), new DataColumn("IsAppMarket"), new DataColumn("OsUpgrade"), new DataColumn("AppInstall"), new DataColumn("AppUpdate"), new DataColumn("AppUnInstall") });
                int b = grdUsr.PageIndex;
                //Loop through All Pages
                for (int i = 0; i < grdUsr.PageCount; i++)
                {
                    //Set Page Index
                    grdUsr.SetPageIndex(i);
                    //After Setting Page Index Loop through its Rows
                    foreach (GridViewRow row in grdUsr.Rows)
                    {
                        int a = grdUsr.Rows.Count;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkRow = (CheckBox)(row.FindControl("User"));
                            string UserId1 = (row.Cells[0].FindControl("lblUserId") as Label).Text;
                            string DeviceId1 = (row.Cells[0].FindControl("lblDeviceId") as Label).Text;
                            string status = "0";
                            if (chkRow.Checked)
                            {
                                status = "1";
                                dt1.Rows.Add((row.Cells[0].FindControl("lblMobileNo") as Label).Text, apdt.Rows[h]["AppURL"].ToString());
                            }
                            else
                                status = "0";
                            string datetime = DateTime.UtcNow.AddMinutes(330).ToString("yyyy-MM-dd hh:mm");

                            dt.Rows.Add(0, ClientId, UserId1, DeviceId1, Convert.ToInt32(apdt.Rows[h]["AppMarketId"]), 0, UserId, datetime, UserId, datetime, (btn == 0) ? 3 : 4, (btn == 0) ? status : "0", (btn == 1) ? status : "0", (btn == 2) ? status : "0", (btn == 3) ? status : "0");
                        }
                    }
                    //Do Your Commands Here
                }

                appbal = new AppBAL();
                appbal.AssignDeviceAppMarket(dt, btn);
                SendSMS(dt1, btn);
            }
        }
        public void SendSMS(DataTable dt, int btn)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string message = "GBox set as AU " + btn + " " + dt.Rows[i]["AppPath"].ToString();
                    SendSMSBAL sms = new SendSMSBAL();
                    sms.sendFinalSMS(dt.Rows[i]["MobileNo"].ToString(), message, ClientId);
                }
            }
            catch { }
        }
        protected void AchkHeader_Parents_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdaddselected.HeaderRow.FindControl("AchkHeader_Parents");
            foreach (GridViewRow row in grdaddselected.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("AchkRow_Parents");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
            mp.Show();
        }
        protected void BindAppList()
        {
            srch = new GingerboxSrch();
            grdaddselected.DataSource = srch.GetAppListByClientId(ClientId);
            grdaddselected.DataBind();
        }

        protected void btnAppSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[2] { new DataColumn("AppMarketId"), new DataColumn("AppURL") });
                foreach (GridViewRow row in grdaddselected.Rows)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("AchkRow_Parents") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string url = lblClickId.Text == "3" ? ((Label)row.FindControl("AlblAppPackage")).Text : Constant.URL + "PublicApk/APK/" + (row.Cells[0].FindControl("AlbAppUrl") as Label).Text;
                        dt.Rows.Add((row.Cells[0].FindControl("AlblAppId") as Label).Text, url);
                    }
                }
                InsertList(dt, Convert.ToInt32(lblClickId.Text));
                lblMsg.Text = "Successfully Completed";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                mp.Hide();
            }
            catch
            {
                lblMsg.Text = "Something went wrong";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}