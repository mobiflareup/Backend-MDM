using iTextSharp.text;
using iTextSharp.text.pdf;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class UserDeviceModel : Base
    { 
        GingerboxSrch srch;
        ProfileBAL profileBal;
        UserBAL userBal;
        DeptBAL dept;
        //SupportBAL support;
        SendMailBAL send;
        RoleBAL role;
        DataTable dtUser;
        int ClientId, RoleId, UserId, DeptId;
        public string Message = "";        
        DataTable dt;
        SendSMSBAL sms;
        int BranchId, DepartmentId, ProfileId, Ownerid;
        string OsVersion = "";
        StringBuilder msg = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblmsg.Text = string.Empty;

            if (!IsPostBack)
            {

                BindGrid();
            }
        }
        protected void btnchange_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            BindGrid();
            UserImg.ImageUrl = "image/user2-hover.png";
            BranchImg.ImageUrl = "image/branch2.png";
            DepartImg.ImageUrl = "image/department.png";
            ProfileImg.ImageUrl = "image/profile2.png";
            DevOwnerImg.ImageUrl = "image/Device-Ownership.png";
            DeviceInfImg.ImageUrl = "image/Device-info.png";
        }
        protected void btnchangeBranch_Click(object sender, EventArgs e)
        {
            BindBranchDdl();
            try
            {
                BranchId = Convert.ToInt32(dtBranch.SelectedValue.ToString());
                int count = BindGrid();                
                lblbranchCount.Text = "No of devices : " + count;
            }
            catch
            {
                lblbranchCount.Text = "No of devices : 0";
            }
            UserImg.ImageUrl = "image/user2.png";
            BranchImg.ImageUrl = "image/branch2-hover.png";
            DepartImg.ImageUrl = "image/department.png";
            ProfileImg.ImageUrl = "image/profile2.png";
            DevOwnerImg.ImageUrl = "image/Device-Ownership.png";
            DeviceInfImg.ImageUrl = "image/Device-info.png";
            MultiView1.ActiveViewIndex = 1;

        }
        protected void btnchangeDepartment_Click(object sender, EventArgs e)
        {
            BindDepartDDL();
            try
            {
                
                DepartmentId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
                int count = BindGrid();                
                lbldepcount.Text = "No of devices : " + count;
            }
            catch
            {
                lbldepcount.Text = "No of devices : 0";
            }
            UserImg.ImageUrl = "image/user2.png";
            BranchImg.ImageUrl = "image/branch2.png";
            DepartImg.ImageUrl = "image/department-hover.png";
            ProfileImg.ImageUrl = "image/profile2.png";
            DevOwnerImg.ImageUrl = "image/Device-Ownership.png";
            DeviceInfImg.ImageUrl = "image/Device-info.png";
            MultiView1.ActiveViewIndex = 2;
        }
        protected void btnchangeProfile_Click(object sender, EventArgs e)
        {
            BindProfileDDL();            
            try
            {
                
                ProfileId = Convert.ToInt32(dtProfile.SelectedValue.ToString());
                int count = BindGrid();                
                lblProfileCount.Text = "No of devices : " + count;
            }
            catch
            {
                lblProfileCount.Text = "No of devices : 0";
            }
            UserImg.ImageUrl = "image/user2.png";
            BranchImg.ImageUrl = "image/branch2.png";
            DepartImg.ImageUrl = "image/department.png";
            ProfileImg.ImageUrl = "image/profile2-hover.png";
            DevOwnerImg.ImageUrl = "image/Device-Ownership.png";
            DeviceInfImg.ImageUrl = "image/Device-info.png";
            MultiView1.ActiveViewIndex = 3;
        }
        protected void btnDeviceOwnership_Click(object sender, EventArgs e)
        {
            BindUserOwnerShip(drpOwner);
            try
            {
                
                Ownerid = Convert.ToInt32(drpOwner.SelectedValue.ToString());
                int count=BindGrid();                
                lblDevOWnerShip.Text = "No of user : " + count;
            }
            catch (Exception)
            {
                lblDevOWnerShip.Text = "No of user : 0";
            }

            UserImg.ImageUrl = "image/user2.png";
            BranchImg.ImageUrl = "image/branch2.png";
            DepartImg.ImageUrl = "image/department.png";
            ProfileImg.ImageUrl = "image/profile2.png";
            DevOwnerImg.ImageUrl = "image/Device-Ownership-hover.png";
            DeviceInfImg.ImageUrl = "image/Device-info.png";
            MultiView1.ActiveViewIndex = 4;
        }
        protected void btnDeviceInfo_Click(object sender, EventArgs e)
        {
            BindDeviceInfo();
            try
            {
               
                OsVersion = drpDeviInfo.SelectedValue.ToString();
                int count = BindGrid();                
                lblDevInf.Text = "No of user : " + count;
            }
            catch (Exception)
            {
                lblDevInf.Text = "No of user : 0";
            }
            UserImg.ImageUrl = "image/user2.png";
            BranchImg.ImageUrl = "image/branch2.png";
            DepartImg.ImageUrl = "image/department.png";
            ProfileImg.ImageUrl = "image/profile2.png";
            DevOwnerImg.ImageUrl = "image/Device-Ownership.png";
            DeviceInfImg.ImageUrl = "image/Device-info-hover.png";
            MultiView1.ActiveViewIndex = 5;
        }
        protected void BindUserOwnerShip(DropDownList ddl)
        {
            System.Web.UI.WebControls.ListItem ls = new System.Web.UI.WebControls.ListItem("----Select---", "0");
            try
            {
                role = new RoleBAL();
                ddl.Items.Clear();                
                ddl.DataSource = role.GetOwnerShipName();
                ddl.DataTextField = "OwnerName";
                ddl.DataValueField = "OwnerId";
                ddl.DataBind();
            }
            catch (Exception) { }
            finally
            {
                role = null;
                ls = null;
            }

        }
        private void BindDeviceInfo()
        {
            //DropDown Branch
            try
            {
                userBal = new UserBAL();                
                drpDeviInfo.Items.Clear();
                userBal.ClientId = ClientId;
                drpDeviInfo.DataSource = userBal.GetOsVersion();
                drpDeviInfo.DataTextField = "OsVersion";
                drpDeviInfo.DataValueField = "OsVersion";
                drpDeviInfo.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                userBal = null;

            }
        }
        private void BindBranchDdl()
        {
            //DropDown Branch
            try
            {
                dept = new DeptBAL();                
                dtBranch.Items.Clear();               
                dept.ClientId = ClientId;
                dtBranch.DataSource = dept.GetBranchName();
                dtBranch.DataTextField = "BranchName";
                dtBranch.DataValueField = "BranchId";
                dtBranch.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                dept = null;

            }
        }
        private void BindDepartDDL()
        {
            dept = new DeptBAL();
            //DropDown Department
            try
            {
                
                dtDepartment.Items.Clear();
                dept.ClientId = ClientId;
                dtDepartment.DataSource = dept.GetDptNameDDL();
                dtDepartment.DataTextField = "DeptName";
                dtDepartment.DataValueField = "DeptId";
                dtDepartment.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                dept = null;

            }
        }
        private void BindProfileDDL()
        {
            try
            {
                profileBal = new ProfileBAL();                
                dtProfile.Items.Clear();                
                profileBal.ClientId = ClientId;
                dtProfile.DataSource = profileBal.GetProfileData();
                dtProfile.DataTextField = "ProfileName";
                dtProfile.DataValueField = "ProfileId";
                dtProfile.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                profileBal = null;

            }
        }
        protected void addToTable_Click1(object sender, EventArgs e)
        {
            Response.Redirect("AddUser.aspx");
        }
        protected int BindGrid()
        {
            srch = new GingerboxSrch();
            dtUser = new DataTable();
            if (MultiView1.ActiveViewIndex == 0)
            {

            }
            if (MultiView1.ActiveViewIndex == 1)
            {
                txtSrchUserName.Text = string.Empty;
                txtSrchDeviceNAme.Text = string.Empty;
                txtSrchMobileNo.Text = string.Empty;
                ddlappstus.SelectedIndex = 0;
                BranchId = Convert.ToInt32(dtBranch.SelectedValue.ToString());
            }
            if (MultiView1.ActiveViewIndex == 2)
            {
                txtSrchUserName.Text = string.Empty;
                txtSrchDeviceNAme.Text = string.Empty;
                txtSrchMobileNo.Text = string.Empty;
                ddlappstus.SelectedIndex = 0;
                DepartmentId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
            }
            if (MultiView1.ActiveViewIndex == 3)
            {
                txtSrchUserName.Text = string.Empty;
                txtSrchDeviceNAme.Text = string.Empty;
                txtSrchMobileNo.Text = string.Empty;
                ddlappstus.SelectedIndex = 0;
                ProfileId = Convert.ToInt32(dtProfile.SelectedValue.ToString());
            }
            if (MultiView1.ActiveViewIndex == 4)
            {
                txtSrchUserName.Text = string.Empty;
                txtSrchDeviceNAme.Text = string.Empty;
                txtSrchMobileNo.Text = string.Empty;
                ddlappstus.SelectedIndex = 0;
                Ownerid = Convert.ToInt32(drpOwner.SelectedValue.ToString());
                
            }
            if (MultiView1.ActiveViewIndex == 5)
            {
                txtSrchUserName.Text = string.Empty;
                txtSrchDeviceNAme.Text = string.Empty;
                txtSrchMobileNo.Text = string.Empty;
                ddlappstus.SelectedIndex = 0;
                OsVersion = drpDeviInfo.SelectedValue.ToString();
            }
            if (RoleId == 1 || RoleId == 2)
            {
                dtUser = srch.Userdetails(ClientId, txtSrchUserName.Text, txtSrchDeviceNAme.Text, txtSrchMobileNo.Text, Convert.ToInt32(ddlappstus.SelectedValue), 0, BranchId, DepartmentId, ProfileId, Ownerid, OsVersion);
            }
            else if (RoleId == 3)
            {
                dtUser = srch.Userdetails(ClientId, txtSrchUserName.Text, txtSrchDeviceNAme.Text, txtSrchMobileNo.Text, Convert.ToInt32(ddlappstus.SelectedValue), 0, BranchId, DepartmentId, ProfileId, Ownerid, OsVersion, DeptId);
            }
            else
            {
                dtUser = srch.Userdetails(ClientId, txtSrchUserName.Text, txtSrchDeviceNAme.Text, txtSrchMobileNo.Text, Convert.ToInt32(ddlappstus.SelectedValue), UserId, BranchId, DepartmentId, ProfileId, Ownerid, OsVersion);
            }
            GridUser.DataSource = dtUser;
            ViewState["dtUser"] = dtUser;
            GridUser.DataBind();
            return dtUser.Rows.Count;
        }
        protected void GridUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void drpOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //dt = new DataTable();
                //support = new SupportBAL();
                //support.ClientId = ClientId;
                //support.ownerid = Convert.ToInt32(drpOwner.SelectedValue.ToString());
                Ownerid = Convert.ToInt32(drpOwner.SelectedValue.ToString());
                int count = BindGrid();
                //dt = support.GetUserDeviceCOuunt();
                lblDevOWnerShip.Text = "No of user : " + count;// dt.Rows[0]["CountDP"].ToString();
            }
            catch (Exception)
            {
                lblDevOWnerShip.Text = "No of user : 0";// +dt.Rows[0]["DbCount"].ToString();
            }
        }
        protected void drpDeviInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //dt = new DataTable();
                //support = new SupportBAL();
                //support.ClientId = ClientId;
                //support.OsVersion = drpDeviInfo.SelectedValue.ToString();
                OsVersion = drpDeviInfo.SelectedValue.ToString();
                int count = BindGrid();
                //dt = support.GetDevInfCount();
                lblDevInf.Text = "No of user : " + count;// dt.Rows[0]["CountDP"].ToString();
            }
            catch (Exception)
            {
                lblDevInf.Text = "No of User : 0";// +dt.Rows[0]["DbCount"].ToString();
            }
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSrchUserName.Text = string.Empty;
            txtSrchDeviceNAme.Text = string.Empty;
            txtSrchMobileNo.Text = string.Empty;
            ddlappstus.SelectedIndex = 0;
            //dt = new DataTable();
            //support = new SupportBAL();
            //support.ClientId = ClientId;
            //support.BranchId = Convert.ToInt32(dtBranch.SelectedValue.ToString());
            BranchId = Convert.ToInt32(dtBranch.SelectedValue.ToString());
            int count = BindGrid();
            //dt = support.GetDataUDeviceByBranchCount();
            lblbranchCount.Text = "No of devices : " + count;// dt.Rows[0]["DbCount"].ToString();
        }
        protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtSrchUserName.Text = string.Empty;
            txtSrchDeviceNAme.Text = string.Empty;
            txtSrchMobileNo.Text = string.Empty;
            ddlappstus.SelectedIndex = 0;
            //dt = new DataTable();
            //support = new SupportBAL();
            //support.ClientId = ClientId;
            //support.DepartmentId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
            DepartmentId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
            int Count = BindGrid();            
            //dt = support.GetDataUDeviceByDepartCount();
            lbldepcount.Text = "No of devices : " + Count;// dt.Rows[0]["DbCount"].ToString();
            
        }
        protected void ddlProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSrchUserName.Text = string.Empty;
            txtSrchDeviceNAme.Text = string.Empty;
            txtSrchMobileNo.Text = string.Empty;
            ddlappstus.SelectedIndex = 0;
            //dt = new DataTable();
            //support = new SupportBAL();
            //support.ClientId = ClientId;
            //support.ProfileId = Convert.ToInt32(dtProfile.SelectedValue.ToString());
            ProfileId = Convert.ToInt32(dtProfile.SelectedValue.ToString());
            int Count =BindGrid();
            // dt = support.GetDataUDeviceByProfileCount();
            lblProfileCount.Text = "No of devices : " + Count;// dt.Rows[0]["DbCount"].ToString();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
            txtSrchDeviceNAme.Text = string.Empty;
            txtSrchMobileNo.Text = string.Empty;
            ddlappstus.SelectedIndex = 0;

        }
        protected void btnSrch1_Click(object sender, ImageClickEventArgs e)
        {

            txtSrchUserName.Text = string.Empty;
            txtSrchMobileNo.Text = string.Empty;
            ddlappstus.SelectedIndex = 0;
            BindGrid();
        }
        protected void btnSrch2_Click(object sender, ImageClickEventArgs e)
        {

            txtSrchUserName.Text = string.Empty;
            txtSrchDeviceNAme.Text = string.Empty;
            ddlappstus.SelectedIndex = 0;
            BindGrid();
        }
        protected void searchdropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtSrchUserName.Text = string.Empty;
            txtSrchDeviceNAme.Text = string.Empty;
            txtSrchMobileNo.Text = string.Empty;
            BindGrid();
        }
        protected void GridUser_OnDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblAppStatus = (Label)e.Row.FindControl("lblAppStatus");
                Label type = (Label)e.Row.FindControl("lblAPPInstallationstatus");
                LinkButton btnpush = (LinkButton)e.Row.FindControl("btnpush");
                //Button btnpush = (Button)e.Row.FindControl("btnpush");
                if (lblAppStatus.Text == "1")
                {
                    btnpush.Visible = true;// = "UnInstalled";
                    type.Visible = false;
                }
                if (lblAppStatus.Text == "0")
                {
                    btnpush.Visible = false;// = "Installed";
                    type.Visible = true;
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

            /* Verifies that the control is rendered */

        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");

            GrideTable();
            sb.Append(Message);

            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        }
        public void GrideTable()
        {
            DataTable dtpdf = new DataTable();
            dtpdf = (DataTable)ViewState["dtUser"];
            if (dtpdf.Rows.Count > 0)
            {
                string UHead = "", UName = "", UCode = "", UMob = "", UEmail = "", UBranch = "", UDept = "", UProfile = "";
                try
                {
                    UHead = "User Device Report";
                    if (MultiView1.ActiveViewIndex == 0)
                    {

                        if (txtSrchUserName.Text != "")
                        {
                            UName = " User Name : " + txtSrchUserName.Text.Trim();
                        }
                        if (txtSrchDeviceNAme.Text != "")
                        {
                            UCode = " Device Name : " + txtSrchDeviceNAme.Text.Trim();
                        }
                        if (txtSrchMobileNo.Text != "")
                        {
                            UMob = " Mobile No : " + txtSrchMobileNo.Text.Trim();
                        }
                        if (Convert.ToInt16(ddlappstus.SelectedValue) > 0)
                        {
                            UEmail = " App Status : " + ddlappstus.SelectedItem.ToString();
                        }
                    }
                    if (MultiView1.ActiveViewIndex == 1)
                    {
                        if (Convert.ToInt16(dtBranch.SelectedValue) == 0)
                        {
                            UBranch = "Branch Report";
                        }
                        else
                        {
                            UBranch = dtBranch.SelectedItem.ToString() + " Branch Report ";
                        }
                    }
                    if (MultiView1.ActiveViewIndex == 2)
                    {
                        if (Convert.ToInt16(dtDepartment.SelectedValue) == 0)
                        {
                            UDept = "Department Report";
                        }
                        else
                        {
                            UDept = dtDepartment.SelectedItem.ToString() + " Department Report ";
                        }
                    }
                    if (MultiView1.ActiveViewIndex == 3)
                    {
                        if (Convert.ToInt16(dtProfile.SelectedValue) == 0)
                        {
                            UProfile = "Profile Report";
                        }
                        else
                        {
                            UProfile = dtProfile.SelectedItem.ToString() + " Report ";
                        }
                    }
                    if (MultiView1.ActiveViewIndex == 4)
                    {
                        if (Convert.ToInt16(drpOwner.SelectedValue) == 0)
                        {
                            UProfile = "Device Ownership Report";
                        }
                        else
                        {
                            UProfile = drpOwner.SelectedItem.ToString() + " Device Report ";
                        }
                    }
                    if (MultiView1.ActiveViewIndex == 5)
                    {
                        if (Convert.ToInt16(drpDeviInfo.SelectedValue) == 0)
                        {
                            UProfile = "OS Version Report";
                        }
                        else
                        {
                            UProfile = "Device Report which have " + drpDeviInfo.SelectedItem.ToString() + " version ";
                        }
                    }

                }
                catch (Exception)
                {

                }
                int columnCount = 10;
                Message = ("<table width='100%' cellspacing='0' cellpadding='0' border=0 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr>");
                Message = Message + ("<td align='center' colspan='" + 10 + "'>");
                Message = Message + ("<b style=' font-size: 20px;'>" + UHead);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td align='center' colspan='" + 10 + "'>");
                Message = Message + (UName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td align='center' colspan='" + 10 + "'>");
                Message = Message + (UCode);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td align='center' colspan='" + 10 + "'>");
                Message = Message + (UMob);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td align='center' colspan='" + 10 + "'>");
                Message = Message + (UEmail);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td align='center' colspan='" + 10 + "'>");
                Message = Message + (UBranch);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td align='center' colspan='" + 10 + "'>");
                Message = Message + (UDept);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td align='center' colspan='" + 10 + "'>");
                Message = Message + (UProfile);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");
                Message = Message + ("</tr>");

                Message = Message + ("</table>");

                Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");


                Message = Message + ("<tr style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>User Name");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Device Name");
                Message = Message + ("</td>");



                Message = Message + ("<td align='center'>");
                Message = Message + ("<b> Mobile No");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Device Model");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>OS Version");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Branch Name");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Department Name");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Profile Name");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Profile Status");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>APP Status");
                Message = Message + ("</td>");

                Message = Message + ("</tr>");
                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j < GridUser.Columns.Count; j++)
                    {
                      //  Message = Message + ("<td align='center' >");
                        if (j >= columnCount)
                        {
                            continue;
                        }
                        Message = Message + ("<td align='center' >");
                        string cellText;
                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (j == columnCount - 2)
                        {
                            if (cellText == "1")
                            {
                                cellText = "Enabled";
                            }
                            else
                            {
                                cellText = "Disabled";
                            }
                        }
                        if (j == columnCount - 1)
                        {
                            if (cellText == "0")
                            {
                                cellText = "Installed";
                            }
                            else
                            {
                                cellText = "UnInstalled";
                            }
                        }
                        if (cellText == "")
                        {
                            cellText = "--";
                        }
                        Message = Message + (cellText);
                        Message = Message + ("</td>");
                    }
                    Message = Message + ("</tr>");
                }
                Message = Message + ("</table>");
            }
        }
        protected void btnSendtomail_Click(object sender, EventArgs e)
        {

            MessagePnl.Visible = true;
            TxtMailClear();

        }
        protected void Send_Click(object sender, EventArgs e)
        {
            MessagePnl.Visible = true;
            if (RbtnYou.Checked || RbtnOther.Checked)
            {
                #region---- Make user Table ----------


                msg.Append("Dear Sir/Madam");
                msg.AppendLine(); msg.AppendLine();
                msg.Append("<b>The below table has the details for User Device.</b>");
                msg.AppendLine(); msg.AppendLine();

                GrideTable();
                msg.Append(Message);

                msg.AppendLine();
                msg.AppendLine();
                msg.Append("Have a nice day :)");
                msg.AppendLine(); msg.AppendLine(); msg.AppendLine();

                msg.Append("Regards");
                msg.AppendLine();
                msg.Append("MobiOcean Team");

                #endregion
                if (RbtnYou.Checked)
                {
                    MailsendYou(msg.ToString());
                }

                if (RbtnOther.Checked)
                {
                    MailsendOther(msg.ToString());
                }

            }
            else
            {
                lblerrorMailTo.Text = "Please select any of the One";
                lblerrorMailTo.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void MailsendYou(string msgbody)
        {
            try
            {
                string EmailId = "";
                UserBAL usr = new UserBAL();
                usr.UserId = UserId;
                dt = new DataTable();
                dt = usr.GetUserDtlByUserId();
                if (dt.Rows.Count > 0)
                {
                    EmailId = dt.Rows[0]["EmailId"].ToString();
                }

                send = new SendMailBAL();
                send.SendEmail(EmailId, "User Device Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
                lblmsg.Text = "Mail sent successfully";
                lblmsg.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception)
            {
                lblmsg.Text = "Could not send the mail.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            MessagePnl.Visible = false;
        }
        private void MailsendOther(string msgbody)
        {
            string emailpattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            if (txtMailTo.Text != "" && Regex.IsMatch(txtMailTo.Text.Trim(), emailpattern))
            {
                try
                {
                    UserBAL usr = new UserBAL();
                    usr.UserId = UserId;
                    dt = new DataTable();
                    send = new SendMailBAL();
                    send.SendEmail(txtMailTo.Text, "User Device Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
                    lblmsg.Text = "Mail sent successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception)
                {
                    lblmsg.Text = "Could not send the mail.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblerrorMailTo.Text = "Please Enter e-Mail ID";
                lblerrorMailTo.ForeColor = System.Drawing.Color.Red;
            }
            MessagePnl.Visible = false;
        }
        protected void Group1_CheckedChanged(Object sender, EventArgs e)
        {

            MessagePnl.Visible = true;
            if (RbtnYou.Checked)
            {
                txtMailTo.Enabled = false;
                RequiredFieldValidator3.ValidationGroup = "m";
            }

            if (RbtnOther.Checked)
            {
                txtMailTo.Enabled = true;
                RequiredFieldValidator3.ValidationGroup = "mailsend";
            }
        }
        protected void CancelMail_Click(object sender, EventArgs e)
        {
            MessagePnl.Visible = false;
            TxtMailClear();
        }
        private void TxtMailClear()
        {
            txtMailTo.Text = "";
            txtMailTo.Enabled = false;
            RbtnYou.Checked = false;
            RbtnOther.Checked = false;
            lblerrorMailTo.Text = "";
        }

        protected void lkbtnAppStatus_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblUserId = ((Label)GridUser.Rows[gvr.RowIndex].FindControl("lblUserId"));
            Label lblDeviceId = ((Label)GridUser.Rows[gvr.RowIndex].FindControl("lblDeviceId"));
            Label lblUserName = ((Label)GridUser.Rows[gvr.RowIndex].FindControl("lblUserName"));
            Label lblDeviceName = ((Label)GridUser.Rows[gvr.RowIndex].FindControl("lblDeviceName"));

            lblPUserName.Text = lblUserName.Text;
            lblPDeviceName.Text = lblDeviceName.Text;

            srch = new GingerboxSrch();
            dt = new DataTable();
            grdappInstalledStatus.DataSource = srch.GetMDMAppList(lblUserId.Text, lblDeviceId.Text,Convert.ToInt32(Session["ClientID"].ToString()));
            grdappInstalledStatus.DataBind();

            mp.Show();
        }

        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderWidth = 1;
            cell.BorderColor = BaseColor.BLACK;
            cell.VerticalAlignment = align;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 5f;
            return cell;
        }
        protected void btnsavetopdf_Click(object sender, EventArgs e)
        {
            // grdUser.AllowPaging = false;
            DataTable dtpdf = new DataTable();
            dtpdf = (DataTable)ViewState["dtUser"];
            Document document = new Document(PageSize.A2, 10f, 10f, 10f, 10f);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                int ColumnCount = 10;
                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(ColumnCount);
                PdfPCell cell0 = null, cell1 = null, cell2 = null, cell3 = null, cell4 = null, cell5 = null, cell6 = null, cell7 = null;

                if (MultiView1.ActiveViewIndex == 0)
                {
                    cell0 = PhraseCell(new Phrase(" User Device Report ", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell0.Colspan = ColumnCount;
                    cell0.BorderWidth = 0;
                    table.AddCell(cell0);
                    if (txtSrchUserName.Text != "")
                    {
                        cell1 = PhraseCell(new Phrase(" User Name : " + txtSrchUserName.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                        cell1.Colspan = ColumnCount;
                        cell1.BorderWidth = 0;
                        table.AddCell(cell1);
                    }
                    if (txtSrchDeviceNAme.Text != "")
                    {
                        cell2 = PhraseCell(new Phrase("  Device Name : " + txtSrchDeviceNAme.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                        cell2.Colspan = ColumnCount;
                        cell2.BorderWidth = 0;
                        table.AddCell(cell2);
                    }
                    if (txtSrchMobileNo.Text != "")
                    {
                        cell3 = PhraseCell(new Phrase("  Mobile No : " + txtSrchMobileNo.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                        cell3.Colspan = ColumnCount;
                        cell3.BorderWidth = 0;
                        table.AddCell(cell3);
                    }
                    if (Convert.ToInt16(ddlappstus.SelectedValue) > 0)
                    {
                        cell4 = PhraseCell(new Phrase("  App Status : " + ddlappstus.SelectedItem.ToString(), FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                        cell4.Colspan = ColumnCount;
                        cell4.BorderWidth = 0;
                        table.AddCell(cell4);
                    }
                }
                if (MultiView1.ActiveViewIndex == 1)
                {
                    cell5 = PhraseCell(new Phrase(dtBranch.SelectedItem != null ? dtBranch.SelectedItem.ToString() : "" + "Device Report By Branch", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell5.Colspan = ColumnCount;
                    cell5.BorderWidth = 0;
                    table.AddCell(cell5);
                }
                if (MultiView1.ActiveViewIndex == 2)
                {
                    cell6 = PhraseCell(new Phrase(dtDepartment.SelectedItem != null ? dtDepartment.SelectedItem.ToString() : "" + "Device Report By Department", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell6.Colspan = ColumnCount;
                    cell6.BorderWidth = 0;
                    table.AddCell(cell6);
                }
                if (MultiView1.ActiveViewIndex == 3)
                {
                    cell7 = PhraseCell(new Phrase(dtProfile.SelectedItem != null ? dtProfile.SelectedItem.ToString() : "" + " Device Report By Profile", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell7.Colspan = ColumnCount;
                    cell7.BorderWidth = 0;
                    table.AddCell(cell7);
                }
                if (MultiView1.ActiveViewIndex == 4)
                {
                    cell7 = PhraseCell(new Phrase(dtProfile.SelectedItem != null ? dtProfile.SelectedItem.ToString() : "" + " Device Report By Device Ownership", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell7.Colspan = ColumnCount;
                    cell7.BorderWidth = 0;
                    table.AddCell(cell7);
                }
                if (MultiView1.ActiveViewIndex == 5)
                {
                    cell7 = PhraseCell(new Phrase(dtProfile.SelectedItem != null ? dtProfile.SelectedItem.ToString() : "" + " Device Report By OS Version ", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell7.Colspan = ColumnCount;
                    cell7.BorderWidth = 0;
                    table.AddCell(cell7);
                }


                for (int x = 0; x < GridUser.Columns.Count; x++)
                {
                    if (x >= 10)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(GridUser.HeaderRow.Cells[x].Text);
                    //Set Font and Font Color
                    iTextSharp.text.Font font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);//new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
                    font.Color = new BaseColor(255, 255, 255);
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                    cell.BackgroundColor = new BaseColor(42, 54, 137);
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    for (int j = 0; j < GridUser.Columns.Count; j++)
                    {
                        if (j >= 10)
                        {
                            continue;
                        }
                        string cellText;

                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());

                        if (j == ColumnCount - 2)
                        {
                            if (cellText == "1")
                            {
                                cellText = "Enabled";
                            }
                            else
                            {
                                cellText = "Disabled";
                            }
                        }
                        if (j == ColumnCount - 1)
                        {
                            if (cellText == "0")
                            {
                                cellText = "Installed";
                            }
                            else
                            {
                                cellText = "UnInstalled";
                            }
                        }
                        if (cellText == "")
                        {
                            cellText = "--";
                        }

                        iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);//new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                        font.Color = new BaseColor(0, 0, 0);
                        iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                        if (i % 2 == 0)
                        {
                            cell.BackgroundColor = new BaseColor(229, 229, 229);
                        }
                        table.AddCell(cell);
                    }
                }
                document.Open();
                document.Add(table);
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=UserDeviceReport.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }

        protected void btnpush_Click(object sender, EventArgs e)
        {
            //Button lnkstatus = sender as Button;
            LinkButton lnkstatus = sender as LinkButton;
            GridViewRow gvr = lnkstatus.NamingContainer as GridViewRow;
            Label lblDeviceId = (Label)GridUser.Rows[gvr.RowIndex].FindControl("lblDeviceId");
            Label lblUserId = (Label)GridUser.Rows[gvr.RowIndex].FindControl("lblUserId");
            Label lblMobileNo = (Label)GridUser.Rows[gvr.RowIndex].FindControl("lblMobileNo");
            Label lblUserName = (Label)GridUser.Rows[gvr.RowIndex].FindControl("lblUserName");
            SendMsgToDevice(Convert.ToInt32(lblDeviceId.Text), Convert.ToInt32(lblUserId.Text), lblMobileNo.Text, lblUserName.Text);
        }
        private void SendMsgToDevice(int DeviceId, int UserId, string MObileNO, string UserName)
        {
            sms = new SendSMSBAL();
            string ClientCode, UserCode;
            DataTable dtdevice = new DataTable();
            UserDeviceBAL udbl = new UserDeviceBAL();
            udbl.UserId = UserId;
            dtdevice = udbl.GetUserCodeClientCodeByUserId();
            if (dtdevice.Rows.Count > 0)
            {
                ClientCode = dtdevice.Rows[0]["ClientCode"].ToString();
                UserCode = dtdevice.Rows[0]["UserCode"].ToString();
                string downloadlink = ClientId == 399 ? Constant.LTAppDownloadUrl : Constant.CommonAppUrl;//: Constant.AppDownloadUrl;
                sms.sendMsgUsingSMS("Dear " + UserName + ", You have been registered on MobiOcean by " + Session["UserName"] + ". Please use the below URL to download the MobiOcean APP: " + downloadlink + " . After downloading, please use the below info to activate the APP. Mobile No:" + MObileNO + " ", MObileNO, ClientId);//Client Code:" + ClientCode + "   User Code: " + UserCode + " 
            }
        }
        protected void btnview_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lbldeviceId = (Label)GridUser.Rows[gvr.RowIndex].FindControl("lblDeviceId");
            Response.Redirect("DeviceInfo.aspx?Id=" + lbldeviceId.Text);
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtn = sender as LinkButton;
                GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
                Label lbldeviceId = (Label)GridUser.Rows[gvr.RowIndex].FindControl("lblMobileNo");
                sms = new SendSMSBAL();
                sms.sendFinalSMS(lbldeviceId.Text, "GBox set as SO", ClientId);
                lblmsg.Text = "Sign Out command sent successfully!";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception)
            {
                lblmsg.Text = "Something went wrong. Pl contact our support team!";
            }
        }
    }
}
