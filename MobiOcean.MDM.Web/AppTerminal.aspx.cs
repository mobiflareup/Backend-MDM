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
    public partial class AppTerminal : Base
    {
        int ClientId, RoleId, UserId, DeptId, PageId, MarketId, PageClickId;
        bool help = true;
        DataTable dt;
        GingerboxSrch srch;
        AppBAL appbal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            PageId = Convert.ToInt32(Request["PageId"]);
            PageClickId = Convert.ToInt32(Request["PageClickId"]);
            MarketId = Convert.ToInt32(Request["Id"]);

            if (!IsPostBack)
            {
                PageActivity();
                BindAllUser();
            }
        }
        protected void BindAllUser()
        {
            srch = new GingerboxSrch();
            dt = new DataTable();
            if (ViewState["dtUser"] == null)
            {
                dt = srch.SrchUserDeviceList(ClientId, txtUserName.Text, txtDeviceName.Text, txtBranchName.Text, txtDept.Text, PageId, PageClickId, MarketId.ToString());
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
                    dt = srch.SrchUserDeviceList(ClientId, txtUserName.Text, txtDeviceName.Text, txtBranchName.Text, txtDept.Text, PageId, PageClickId, MarketId.ToString());
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
                    dt = srch.SrchUserDeviceList(ClientId, txtUserName.Text, txtDeviceName.Text, txtBranchName.Text, txtDept.Text, PageId, PageClickId, MarketId.ToString());
                    ViewState["dtUser"] = dt;
                }
                else
                    dt = (DataTable)ViewState["dtUser"];
            }
            else
                dt = (DataTable)ViewState["dtUser"];
            if (dt != null && dt.Rows.Count > 0)
            {
                txtHeading.Text = "Package : " + dt.Rows[0]["AppPackage"];
                grdUsr.DataSource = dt;
                grdUsr.DataBind();
            }

        }

        private void PageActivity()
        {
            if (PageId == 1)
            {
                btnOsUpgrade.Visible = false;
                if (PageClickId == 1)
                {
                    btnUpgradeAPP.Visible = false;
                    btnUnIstallApp.Visible = false;
                }
                else if (PageClickId == 2)
                {
                    btnInstallNewApp.Visible = false;
                    btnUnIstallApp.Visible = false;
                }
                else
                {
                    btnInstallNewApp.Visible = false;
                    btnUpgradeAPP.Visible = false;
                }
            }
            else if (PageId == 2)
            {
                btnInstallNewApp.Visible = false;
                btnUpgradeAPP.Visible = false;
                btnUnIstallApp.Visible = false;
            }
            else
            {
                btnOsUpgrade.Visible = false;
                btnInstallNewApp.Visible = false;
                btnUpgradeAPP.Visible = false;
                btnUnIstallApp.Visible = false;
            }
        }

        protected void grdUsr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdUsr.PageIndex = e.NewPageIndex;
            grdUsr.EditIndex = -1;
            BindAllUser();
        }

        protected void AssignUserList_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[3] { new DataColumn("MobileNo"), new DataColumn("Package"), new DataColumn("Version") });
            dt.Columns.AddRange(new DataColumn[10] { new DataColumn("AppMarketAssignId"), new DataColumn("ClientId"), new DataColumn("UserId"), new DataColumn("DeviceId"), new DataColumn("AppMarketId"), new DataColumn("Status"), new DataColumn("CreatedBy"), new DataColumn("CreationDate"), new DataColumn("UpdatedBy"), new DataColumn("UpdationDate") });
            //Loop through All Pages
            grdUsr.AllowPaging = false;
            //After Setting Page Index Loop through its Rows
            foreach (GridViewRow row in grdUsr.Rows)
            {
                int a = grdUsr.Rows.Count;
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[5].FindControl("User") as CheckBox);
                    string UserId1 = (row.Cells[0].FindControl("lblUserId") as Label).Text;
                    string ClientId1 = ClientId.ToString();
                    string DeviceId1 = (row.Cells[0].FindControl("lblDeviceId") as Label).Text;
                    string AppMarket = Request["Id"].ToString();
                    string AppMarketAssignId = (row.Cells[0].FindControl("lblAppMarketAssignId") as Label).Text;
                    string status = "0";
                    if (chkRow.Checked)
                    {
                        status = "0";
                        dt1.Rows.Add((row.Cells[0].FindControl("lblMobileNo") as Label).Text, (row.Cells[0].FindControl("lblPackageName") as Label).Text, (row.Cells[0].FindControl("lblVersion") as Label).Text);
                    }
                    else
                        status = "1";
                    string datetime = DateTime.UtcNow.AddMinutes(330).ToString("yyyy-MM-dd hh:mm");
                    dt.Rows.Add(AppMarketAssignId, ClientId1, UserId1, DeviceId1, AppMarket, status, UserId, datetime, UserId, datetime);
                }
                //Do Your Commands Here
            }

            appbal = new AppBAL();
            appbal.AssignDeviceAppMarket(dt);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Text = "GBox set as AU 1 " + dt1.Rows[i]["Package"].ToString();
                SendSMS(Text, dt1.Rows[i]["MobileNo"].ToString(), ClientId);
            }
            Response.Redirect("ApplicationMarketList.aspx");
        }

        public void SendSMS(string message, string Mobilno, int ClinetId)
        {
            try
            {
                SendSMSBAL sms = new SendSMSBAL();
                sms.sendFinalSMS(Mobilno, message, ClientId);
            }
            catch { }
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            BindAllUser();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            if (PageId == 1)
                Response.Redirect("ApplicationMarketList.aspx");
            if (PageId == 2)
                Response.Redirect("ApplicationManageOTG.aspx");
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
                    CheckBox ChkBoxRows = row.Cells[5].FindControl("User") as CheckBox;
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
                }
                grdUsr.AllowPaging = true;
                //}
                //grdUsr.SetPageIndex(b);
                ViewState["dtUser"] = dt;
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
        protected void grdUsr_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnOsUpgrade_Click(object sender, EventArgs e)
        {
            DataTable dt1 = InsertList();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                try
                {
                    string Text = "GBox set as AU 0 " + Constant.URL + "PublicApk/OS/" + dt1.Rows[i]["Path"].ToString();
                    SendSMS(Text, dt1.Rows[i]["MobileNo"].ToString(), ClientId);
                }
                catch { }
            }
            GoBack();
        }

        protected void btnInstallNewApp_Click(object sender, EventArgs e)
        {
            DataTable dt1 = InsertList();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Text = "GBox set as AU 1 " + Constant.URL + "PublicApk/APK/" + dt1.Rows[i]["Path"].ToString();
                SendSMS(Text, dt1.Rows[i]["MobileNo"].ToString(), ClientId);
            }
            GoBack();
        }

        protected void btnUpgradeAPP_Click(object sender, EventArgs e)
        {
            DataTable dt1 = InsertList();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Text = "GBox set as AU 2 " + Constant.URL + "PublicApk/APK/" + dt1.Rows[i]["Path"].ToString();
                SendSMS(Text, dt1.Rows[i]["MobileNo"].ToString(), ClientId);
            }
            GoBack();
        }

        protected void btnUnIstallApp_Click(object sender, EventArgs e)
        {
            DataTable dt1 = InsertList();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string Text = "GBox set as AU 3 " + dt1.Rows[i]["Package"].ToString();
                SendSMS(Text, dt1.Rows[i]["MobileNo"].ToString(), ClientId);
            }
            GoBack();
        }

        private DataTable InsertList()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt1.Columns.AddRange(new DataColumn[3] { new DataColumn("Path"), new DataColumn("MobileNo"), new DataColumn("Package") });
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
                        CheckBox chkRow = (row.Cells[5].FindControl("User") as CheckBox);
                        string UserId1 = (row.Cells[0].FindControl("lblUserId") as Label).Text;
                        string ClientId1 = ClientId.ToString();
                        string DeviceId1 = (row.Cells[0].FindControl("lblDeviceId") as Label).Text;
                        string AppMarket = Request["Id"].ToString();
                        string AppMarketAssignId = (row.Cells[0].FindControl("lblAppMarketAssignId") as Label).Text;
                        string AppInstall = (row.Cells[0].FindControl("lblInstall") as Label).Text;
                        string AppUpdate = (row.Cells[0].FindControl("lblUpdate") as Label).Text;
                        string AppUnInstall = (row.Cells[0].FindControl("lblUnInstall") as Label).Text;
                        string OsUpgrade = (row.Cells[0].FindControl("lblOsUpgrade") as Label).Text;
                        string status = "0";
                        if (chkRow.Checked)
                        {
                            status = "1";
                            dt1.Rows.Add((row.Cells[0].FindControl("lblPath") as Label).Text, (row.Cells[0].FindControl("lblMobileNo") as Label).Text, (row.Cells[0].FindControl("lblPackage") as Label).Text);
                        }
                        else
                            status = "0";
                        string datetime = DateTime.UtcNow.AddMinutes(330).ToString("yyyy-MM-dd hh:mm");

                        dt.Rows.Add(AppMarketAssignId, ClientId1, UserId1, DeviceId1, AppMarket, 0, UserId, datetime, UserId, datetime, (PageId == 1) ? 1 : 0, (PageId == 2) ? status : OsUpgrade, (PageClickId == 1) ? status : AppInstall, (PageClickId == 2) ? status : AppUpdate, (PageClickId == 3) ? status : AppUnInstall);
                    }
                }
                //Do Your Commands Here
            }

            appbal = new AppBAL();
            appbal.AssignDeviceAppMarket(dt);
            return dt1;
        }
    }
}