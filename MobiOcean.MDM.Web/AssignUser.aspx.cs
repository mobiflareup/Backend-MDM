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
    public partial class AssignUser : Base
    {
        int ClientId, RoleId, UserId, DeptId;
        bool help = true;

        DataTable dt;
        UserDeviceBAL userDeviceBal;
        FileUploadBAL fa;
        AnuSearch anuSearch;
        SendSMSBAL sendSMSBal;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            //lblMsg.Text = string.Empty;

            if (!IsPostBack)
            {
                if (Request["Id"] == null)
                {
                    Response.Redirect("FileUpload.aspx");
                }
                BindAllUser();
                GetFileName();
            }
        }
        protected void GetFileName()
        {
            fa = new FileUploadBAL();
            int FileId = Convert.ToInt32(Request["Id"]);
            File.Text = fa.GetFileNameById(FileId);
        }
        protected void BindAllUser()
        {
            anuSearch = new AnuSearch();
            int FileId = Convert.ToInt32(Request["Id"]);
            dt = new DataTable();
            if (ViewState["dtUser"] == null)
            {
                dt = anuSearch.GetFileUploadUserSearchList(UserName.Text, UserCode.Text, ClientId, FileId, txtBranch.Text, txtDept.Text);
                if (dt != null)
                {
                    //grdUsr.DataBind();
                    ViewState["dtUser"] = dt;
                    ViewState["dtUserCount"] = dt.Rows.Count;
                }
            }
            else if (!(string.IsNullOrEmpty(UserName.Text) && string.IsNullOrEmpty(UserCode.Text) && string.IsNullOrEmpty(txtBranch.Text) && string.IsNullOrEmpty(txtDept.Text)))
            {
                if (help)
                {
                    dt = anuSearch.GetFileUploadUserSearchList(UserName.Text, UserCode.Text, ClientId, FileId, txtBranch.Text, txtDept.Text);
                    ViewState["dtUser"] = dt;
                }
                else
                    dt = (DataTable)ViewState["dtUser"];
            }
            else if (ViewState["dtUser"] != null && ((DataTable)ViewState["dtUser"]).Rows.Count != Convert.ToInt32(ViewState["dtUserCount"]))
            {
                if (help)
                {
                    dt = anuSearch.GetFileUploadUserSearchList(UserName.Text, UserCode.Text, ClientId, FileId, txtBranch.Text, txtDept.Text);
                    ViewState["dtUser"] = dt;
                }
                else
                    dt = (DataTable)ViewState["dtUser"];
            }
            else
            {
                dt = (DataTable)ViewState["dtUser"];
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                grdUsr.DataSource = dt;
                grdUsr.DataBind();
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
            DataTable dtFile = new DataTable();
            dtFile.Columns.AddRange(new DataColumn[9] { new DataColumn("FileId"), new DataColumn("UserId"), new DataColumn("ClientId"), new DataColumn("Permission"), new DataColumn("CreatedBy"), new DataColumn("CreatedDate"), new DataColumn("Status"), new DataColumn("UpdatedBy"), new DataColumn("UpdationDate") });
            bool result = true;
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
                        int permission = 0;
                        CheckBox chkRow = (row.Cells[5].FindControl("User") as CheckBox);
                        if (chkRow.Checked)
                        {
                            string UserId1 = (row.Cells[0].FindControl("lblId") as Label).Text;
                            string ClientIds = ClientId.ToString();
                            string FileId = Request["Id"].ToString();
                            CheckBox chkRow1 = (row.Cells[6].FindControl("Read") as CheckBox);
                            CheckBox chkRow2 = (row.Cells[6].FindControl("Write") as CheckBox);
                            CheckBox chkRow3 = (row.Cells[6].FindControl("Modify") as CheckBox);
                            if (chkRow1.Checked)
                            {
                                permission = permission + 1;

                            }
                            if (chkRow2.Checked)
                            {
                                permission = permission + 2;
                            }
                            if (chkRow3.Checked)
                            {
                                permission = permission + 4;
                            }
                            if (permission > 0)
                            {
                                string Createddate = DateTime.Now.ToString("yyyy-MM-dd");
                                string status = "0";
                                string updatedBy = null;
                                string updatedDate = null;

                                dtFile.Rows.Add(FileId, UserId1, ClientIds, permission, UserId, Createddate, status, updatedBy, updatedDate);
                            }
                            else
                            {
                                lblMsg.Text = "Please Select Any One Permission";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                result = false;
                                break;
                            }
                        }
                        else
                        {

                            string FileId = Request["Id"].ToString();
                            string UserId1 = (row.Cells[0].FindControl("lblId") as Label).Text;
                            string ClientIds = ClientId.ToString();
                            string Createddate = DateTime.Now.ToString("yyyy-MM-dd");
                            string status = "1";
                            string updatedBy = null;
                            string updatedDate = null;
                            dtFile.Rows.Add(FileId, UserId1, ClientIds, permission, UserId, Createddate, status, updatedBy, updatedDate);
                        }
                    }
                    //Do Your Commands Here
                }
            }
            //Getting Back to the First State
            grdUsr.SetPageIndex(b);
            if (result)
            {
                fa = new FileUploadBAL();
                userDeviceBal = new UserDeviceBAL();
                sendSMSBal = new SendSMSBAL();
                int res = fa.InsertAssignUserList(dtFile);
                if (res > 0)
                {
                    //fa.GetUserMoblieNo(dt);
                    foreach (DataRow row in dtFile.Rows)
                    {

                        if (row["Permission"].ToString() != "0")
                        {
                            userDeviceBal.UserId = Convert.ToInt32(row["UserId"]);
                            DataTable st = (DataTable)userDeviceBal.GetDevicewithMDMByUserId();
                            if (st.Rows.Count > 0)
                            {
                                foreach (DataRow rows in st.Rows)
                                {
                                    sendSMSBal.sendFinalSMS(rows["MobileNo1"].ToString(), "GBox set as SS 1", ClientId);// Secure Storage
                                }
                            }
                        }
                    }
                }
                Response.Redirect("FileUpload.aspx");

            }

        }


        protected void Search_Click(object sender, EventArgs e)
        {
            BindAllUser();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FileUpload.aspx");
        }

        protected void CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            help = false;
            CheckBox ChkBoxHeader = (CheckBox)grdUsr.HeaderRow.FindControl("CheckAll");
            dt = new DataTable();
            dt = (DataTable)ViewState["dtUser"];
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
                CheckBox ChkBoxRows1 = row.Cells[6].FindControl("Read") as CheckBox;
                CheckBox ChkBoxRows2 = row.Cells[6].FindControl("Write") as CheckBox;
                CheckBox ChkBoxRows3 = row.Cells[6].FindControl("Modify") as CheckBox;
                if (ChkBoxHeader.Checked == true)
                {
                    dt.Rows[index]["Permission"] = 7;
                    ChkBoxRows.Checked = true;
                    ChkBoxRows1.Checked = true;
                    ChkBoxRows2.Checked = true;
                    ChkBoxRows3.Checked = true;
                }
                else
                {
                    dt.Rows[index]["Permission"] = 0;
                    ChkBoxRows.Checked = false;
                    ChkBoxRows1.Checked = false;
                    ChkBoxRows2.Checked = false;
                    ChkBoxRows3.Checked = false;
                }
            }
            grdUsr.AllowPaging = true;
            ViewState["dtUser"] = dt;
        }

        protected void User_CheckedChanged(object sender, EventArgs e)
        {
            help = false;
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
            CheckBox ChkBoxRows1 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Read") as CheckBox;
            CheckBox ChkBoxRows2 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Write") as CheckBox;
            CheckBox ChkBoxRows3 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Modify") as CheckBox;
            if (cb.Checked == true)
            {
                dt.Rows[index]["Permission"] = 7;
                ChkBoxRows1.Checked = true;
                ChkBoxRows2.Checked = true;
                ChkBoxRows3.Checked = true;
            }
            else
            {
                dt.Rows[index]["Permission"] = 0;
                ChkBoxRows1.Checked = false;
                ChkBoxRows2.Checked = false;
                ChkBoxRows3.Checked = false;
                CheckBox ChkBoxHeader = (CheckBox)grdUsr.HeaderRow.FindControl("CheckAll");
                ChkBoxHeader.Checked = false;
            }
            ViewState["dtUser"] = dt;
        }



        protected void grdUsr_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Read_CheckedChanged(object sender, EventArgs e)
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
            CheckBox ChkBoxRows = (CheckBox)grdUsr.Rows[setIndex].FindControl("User") as CheckBox;
            CheckBox ChkBoxRows1 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Read") as CheckBox;
            CheckBox ChkBoxRows2 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Write") as CheckBox;
            CheckBox ChkBoxRows3 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Modify") as CheckBox;
            int permission = 0;
            if (ChkBoxRows1.Checked)
                permission += 1;
            if (ChkBoxRows2.Checked)
                permission += 2;
            if (ChkBoxRows3.Checked)
                permission += 4;
            dt.Rows[index][5] = permission;
            if (permission == 7)
                ChkBoxRows.Checked = true;
            else
                ChkBoxRows.Checked = false;
            dt.Rows[index]["Permission"] = permission;
            ViewState["dtUser"] = dt;
        }

        protected void Write_CheckedChanged(object sender, EventArgs e)
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
            CheckBox ChkBoxRows = (CheckBox)grdUsr.Rows[setIndex].FindControl("User") as CheckBox;
            CheckBox ChkBoxRows1 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Read") as CheckBox;
            CheckBox ChkBoxRows2 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Write") as CheckBox;
            CheckBox ChkBoxRows3 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Modify") as CheckBox;
            int permission = 0;
            if (ChkBoxRows1.Checked)
                permission += 1;
            if (ChkBoxRows2.Checked)
                permission += 2;
            if (ChkBoxRows3.Checked)
                permission += 4;
            dt.Rows[index][5] = permission;
            if (permission == 7)
                ChkBoxRows.Checked = true;
            else
                ChkBoxRows.Checked = false;
            dt.Rows[index]["Permission"] = permission;
            ViewState["dtUser"] = dt;
        }

        protected void Modify_CheckedChanged(object sender, EventArgs e)
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
            CheckBox ChkBoxRows = (CheckBox)grdUsr.Rows[setIndex].FindControl("User") as CheckBox;
            CheckBox ChkBoxRows1 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Read") as CheckBox;
            CheckBox ChkBoxRows2 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Write") as CheckBox;
            CheckBox ChkBoxRows3 = (CheckBox)grdUsr.Rows[setIndex].FindControl("Modify") as CheckBox;
            int permission = 0;
            if (ChkBoxRows1.Checked)
                permission += 1;
            if (ChkBoxRows2.Checked)
                permission += 2;
            if (ChkBoxRows3.Checked)
                permission += 4;
            if (permission == 7)
                ChkBoxRows.Checked = true;
            else
                ChkBoxRows.Checked = false;
            dt.Rows[index]["Permission"] = permission;
            ViewState["dtUser"] = dt;
        }
    }
}