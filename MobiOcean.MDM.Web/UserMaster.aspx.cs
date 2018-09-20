using iTextSharp.text;
using iTextSharp.text.pdf;
using MobiOcean.MDM.BAL.BAL;
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
    public partial class UserMaster : Base
    {
        usrBAL user;
        DeptBAL dept;
        ProfileBAL profileBal;
        GingerboxSrch srch;
        SendMailBAL send;
        DataTable dtUser;
        int ClientId, RoleId, UserId, DeptId;
        public string Message = "";
        private UserDeviceBAL usrdevice;
        private DataTable dt;
        private SendSMSBAL sms;
        ProfileUserMappingBAL profileusermapbal;
        int BranchId, DepartmentId, ProfileId;
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
                if (RoleId == 3)
                {
                    grdUsr.Columns[12].Visible = false;
                }

                grdUsr.Columns[6].Visible = false;
                grdUsr.Columns[7].Visible = false;
                grdUsr.Columns[8].Visible = false;
                grdUsr.Columns[9].Visible = false;
            }
        }


        protected void btnchange_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            BindGrid();
            grdUsr.Columns[6].Visible = false;
            grdUsr.Columns[7].Visible = false;
            grdUsr.Columns[8].Visible = false;
            grdUsr.Columns[9].Visible = false;
            UserImg.ImageUrl = "image/user2-hover.png";
            BranchImg.ImageUrl = "image/branch2.png";
            DepartImg.ImageUrl = "image/department.png";
            ProfileImg.ImageUrl = "image/profile2.png";
        }
        protected void btnchangeBranch_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            UserImg.ImageUrl = "image/user2.png";
            BranchImg.ImageUrl = "image/branch2-hover.png";
            DepartImg.ImageUrl = "image/department.png";
            ProfileImg.ImageUrl = "image/profile2.png";
            BindBranchDdl();
            try
            {
                //dt = new DataTable();
                //support = new SupportBAL();
                //support.ClientId = ClientId;
                //support.BranchId = Convert.ToInt32(dtBranch.SelectedValue.ToString());
                BranchId = Convert.ToInt32(dtBranch.SelectedValue.ToString());
                int count = BindGrid();
                //dt = support.GetDataBranchChangingCount();
                lblbranchCount.Text = "No of User : " + count;// dt.Rows[0]["DbCount"].ToString();
            }
            catch (Exception)
            {
                lblbranchCount.Text = "No of User : 0";// +dt.Rows[0]["DbCount"].ToString();
            }

            grdUsr.Columns[6].Visible = true;
            grdUsr.Columns[7].Visible = false;
            grdUsr.Columns[8].Visible = false;
            grdUsr.Columns[9].Visible = false;

        }
        protected void btnchangeDepartment_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            UserImg.ImageUrl = "image/user2.png";
            BranchImg.ImageUrl = "image/branch2.png";
            DepartImg.ImageUrl = "image/department-hover.png";
            ProfileImg.ImageUrl = "image/profile2.png";
            BindDepartDDL();
            try
            {
                //dt = new DataTable();
                //support = new SupportBAL();
                //support.ClientId = ClientId;
                //support.DepartmentId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
                DepartmentId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
                int count = BindGrid();
                //dt = support.GetCountByChangingDepId();
                lbldepcount.Text = "No of User : " + count;// dt.Rows[0]["CountDP"].ToString();
            }
            catch (Exception)
            {
                lbldepcount.Text = "No of User : 0";// +dt.Rows[0]["DbCount"].ToString();
            }
            grdUsr.Columns[6].Visible = false;
            grdUsr.Columns[7].Visible = true;
            grdUsr.Columns[8].Visible = false;
            grdUsr.Columns[9].Visible = false;

        }
        protected void btnchangeProfile_Click(object sender, EventArgs e)
        {
            UserImg.ImageUrl = "image/user2.png";
            BranchImg.ImageUrl = "image/branch2.png";
            DepartImg.ImageUrl = "image/department.png";
            ProfileImg.ImageUrl = "image/profile2-hover.png";
            MultiView1.ActiveViewIndex = 3;
            BindProfileDDL();
            try
            {
                //dt = new DataTable();
                //support = new SupportBAL();
                //support.ClientId = ClientId;
                //support.ProfileId = Convert.ToInt32(dtProfile.SelectedValue.ToString());
                ProfileId = Convert.ToInt32(dtProfile.SelectedValue.ToString());
                int count = BindGrid();
                //dt = support.GetDataByProfileCount();
                lblProfileCount.Text = "No of User : " + count;// dt.Rows[0]["CountDP"].ToString();
            }
            catch (Exception)
            {
                lblProfileCount.Text = "No of User : 0";// +dt.Rows[0]["DbCount"].ToString();
            }
            grdUsr.Columns[6].Visible = false;
            grdUsr.Columns[7].Visible = false;
            grdUsr.Columns[8].Visible = true;
            grdUsr.Columns[9].Visible = true;

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

            try
            {
                dept = new DeptBAL();
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
                profileBal.ClientId = ClientId;
                dtProfile.Items.Clear();
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
                txtSrchUserCode.Text = string.Empty;
                txtSrchMobileNo.Text = string.Empty;
                txtSrchEmailId.Text = string.Empty;
                BranchId = Convert.ToInt32(dtBranch.SelectedValue.ToString());
            }
            if (MultiView1.ActiveViewIndex == 2)
            {
                txtSrchUserName.Text = string.Empty;
                txtSrchUserCode.Text = string.Empty;
                txtSrchMobileNo.Text = string.Empty;
                txtSrchEmailId.Text = string.Empty;
                DepartmentId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
            }
            if (MultiView1.ActiveViewIndex == 3)
            {
                txtSrchUserName.Text = string.Empty;
                txtSrchUserCode.Text = string.Empty;
                txtSrchMobileNo.Text = string.Empty;
                txtSrchEmailId.Text = string.Empty;
                ProfileId = Convert.ToInt32(dtProfile.SelectedValue.ToString());
            }
            if (RoleId == 1 || RoleId == 2)
            {
                dtUser = srch.GetUser1(ClientId, txtSrchUserCode.Text, txtSrchUserName.Text, txtSrchMobileNo.Text, txtSrchEmailId.Text, 0, BranchId, DepartmentId, ProfileId);
            }
            else if (RoleId == 3)
            {
                dtUser = srch.GetUser1(ClientId, txtSrchUserCode.Text, txtSrchUserName.Text, txtSrchMobileNo.Text, txtSrchEmailId.Text, 0, BranchId, DepartmentId, ProfileId, DeptId);
            }
            else
            {
                dtUser = srch.GetUser1(ClientId, txtSrchUserCode.Text, txtSrchUserName.Text, txtSrchMobileNo.Text, txtSrchEmailId.Text, UserId, 0, BranchId, DepartmentId, ProfileId);
            }
            grdUsr.DataSource = dtUser;
            ViewState["dtUser"] = dtUser;
            grdUsr.DataBind();
            return dtUser.Rows.Count;
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dt = new DataTable();
            //support = new SupportBAL();
            //support.ClientId = ClientId;
            //support.BranchId = Convert.ToInt32(dtBranch.SelectedValue.ToString());
            BranchId = Convert.ToInt32(dtBranch.SelectedValue.ToString());
            int count = BindGrid();
            //dt = support.GetDataBranchChangingCount();
            lblbranchCount.Text = "No of User : " + count;// dt.Rows[0]["DbCount"].ToString();
        }
        protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dt = new DataTable();
            //support = new SupportBAL();
            try
            {
                //support.ClientId = ClientId;
                //support.DepartmentId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
                DepartmentId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
                int count = BindGrid();
                //dt = support.GetCountByChangingDepId();
                lbldepcount.Text = "No of User : " + count;// dt.Rows[0]["CountDP"].ToString();
            }
            catch
            {
                lbldepcount.Text = "No of User : 0";
            }
        }
        protected void ddlProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dt = new DataTable();
            try
            {
                //support = new SupportBAL();
                //support.ClientId = ClientId;
                //support.ProfileId = Convert.ToInt32(dtProfile.SelectedValue.ToString());
                ProfileId = Convert.ToInt32(dtProfile.SelectedValue.ToString());
                int count = BindGrid();
                //dt = support.GetDataByProfileCount();
                lblProfileCount.Text = "No of User : " + count;// dt.Rows[0]["CountDP"].ToString();
            }
            catch
            {
                lblProfileCount.Text = "No of User : 0";
            }
        }
        protected void grdUsr_RowEditing(object sender, GridViewEditEventArgs e)
        {

            grdUsr.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void grdUsr_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdUsr.Rows[e.RowIndex];
            try
            {
                user = new usrBAL();
                user.UserId = Convert.ToInt32(((Label)gvr.FindControl("lblUId")).Text.Trim());
                user.UserCode = ((TextBox)gvr.FindControl("txtUserCode")).Text.Trim();
                user.UserName = ((TextBox)gvr.FindControl("txtUserName")).Text.Trim();
                user.MobileNo = ((TextBox)gvr.FindControl("txtMobileNo")).Text.Trim();
                user.EmailId = ((TextBox)gvr.FindControl("txtEmailId")).Text.Trim();
                string res = "0";// user.Insertuser();
                if (int.Parse(res) > 0)
                {
                    lblmsg.Text = "Updated Successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    grdUsr.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblmsg.Text = "UserCode or EmailId Already exists";
                    lblmsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
            }
            finally
            {
                user = null;
            }
        }

        protected void grdUsr_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdUsr.EditIndex = -1;
            BindGrid();
        }

        protected void grdUsr_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdUsr.Rows[e.RowIndex];
            try
            {
                lblkeyid.Text = ((Label)gvr.FindControl("lblUId")).Text.Trim();
                lblroleid.Text = ((Label)gvr.FindControl("lblRole")).Text.Trim();
                mpdelete.Show();
            }
            catch (Exception)
            {
            }
        }

        protected void SendExpiryMessage(int UsrId)
        {
            usrdevice = new UserDeviceBAL();
            dt = new DataTable();
            usrdevice.UserId = UsrId;
            usrdevice.ClientId = ClientId;
            dt = usrdevice.GetDevicewithMDMByUserId();
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(row["MobileNo1"].ToString(), "GBox set as EC 01 01 2016", ClientId);
                }
                catch (Exception)
                { }
            }

        }

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
            //txtSrchUserName.Text = string.Empty;
            txtSrchUserCode.Text = string.Empty;
            txtSrchMobileNo.Text = string.Empty;
            txtSrchEmailId.Text = string.Empty;
        }
        protected void btnSrch1_Click(object sender, EventArgs e)
        {
            BindGrid();
            txtSrchUserName.Text = string.Empty;
            //txtSrchUserCode.Text = string.Empty;
            txtSrchMobileNo.Text = string.Empty;
            txtSrchEmailId.Text = string.Empty;
        }
        protected void btnSrch2_Click(object sender, EventArgs e)
        {
            BindGrid();
            txtSrchUserName.Text = string.Empty;
            txtSrchUserCode.Text = string.Empty;
            //txtSrchMobileNo.Text = string.Empty;
            txtSrchEmailId.Text = string.Empty;
        }
        protected void btnSrch3_Click(object sender, EventArgs e)
        {
            BindGrid();
            txtSrchUserName.Text = string.Empty;
            txtSrchUserCode.Text = string.Empty;
            txtSrchMobileNo.Text = string.Empty;
            //txtSrchEmailId.Text = string.Empty;
        }

        protected void grdUsr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUsr.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = sender as LinkButton;
            GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
            string StuId = grdUsr.DataKeys[row.RowIndex].Value.ToString();
            string url = "~/EditUser.aspx?Id=" + StuId;
            Response.Redirect(url);
        }

        protected void Dashboard_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = sender as LinkButton;
            GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
            string StuId = grdUsr.DataKeys[row.RowIndex].Value.ToString();
            Session["AdminId"] = Session["UserId"];
            Session["AdminName"] = Session["UserName"];
            Session["UserId"] = StuId;
            Session["helpRoleId"] = Session["Role"];
            //Session["RoleId"] = Session["Role"];
            Session["Role"] = "4";
            Session["UserName"] = ((Label)row.FindControl("lblUserName")).Text.Trim();
            Response.Redirect("UserDashBoard.aspx");
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
                string UName = "", UCode = "", UMob = "", UEmail = "", UBranch = "", UDept = "", UProfile = "";
                try
                {
                    if (MultiView1.ActiveViewIndex == 0)
                    {
                        if (txtSrchUserName.Text != "")
                        {
                            UName = " User Name : " + txtSrchUserName.Text.Trim();
                        }
                        if (txtSrchUserCode.Text != "")
                        {
                            UCode = " User Code : " + txtSrchUserCode.Text.Trim();
                        }
                        if (txtSrchMobileNo.Text != "")
                        {
                            UMob = " Mobile No : " + txtSrchMobileNo.Text.Trim();
                        }
                        if (txtSrchEmailId.Text != "")
                        {
                            UEmail = " Email Id : " + txtSrchEmailId.Text.Trim();
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

                }
                catch (Exception)
                {

                }
                Message = ("<table width='100%' cellspacing='0' cellpadding='0' border=0 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + ("<b style=' font-size: 20px;'>User Report</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UCode);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UMob);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UEmail);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");


                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UBranch);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UDept);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UProfile);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");
                Message = Message + ("</table>");

                Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");
                Message = Message + ("<tr style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>User Code</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>User Name</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Mobile No</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Email Id</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Role</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Branch name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Department Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Profile Name</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Profile Status</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");
                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    //if (grdUser.Rows[i].RowType == DataControlRowType.DataRow)
                    //{
                    for (int j = 0; j < 9; j++)
                    {
                        Message = Message + ("<td align='center' >");
                        if (j > 8)
                        {
                            continue;
                        }
                        string cellText;
                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (j == 8)
                        {
                            if (cellText == "1")
                            {
                                cellText = "Active";
                            }
                            if (cellText == "0")
                            {
                                cellText = "Inactive";
                            }
                        }
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        Message = Message + (cellText);
                        Message = Message + ("</td>");

                    }
                    Message = Message + ("</tr>");
                }

                Message = Message + ("</table>");
            }
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
                //Phrase phrase = null;
                //PdfPCell cell = null;
                //PdfPTable table = null;
                //Color color = null;
                //table = new PdfPTable(3);
                //table.TotalWidth = 500f;
                //table.LockedWidth = true;
                //BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\ARIALUNI.TTF", BaseFont.IDENTITY_H, true);

                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(9);
                PdfPCell cell0 = null, cell1 = null, cell2 = null, cell3 = null, cell4 = null, cell5 = null, cell6 = null, cell7 = null;

                if (MultiView1.ActiveViewIndex == 0)
                {
                    cell0 = PhraseCell(new Phrase(" User Report ", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell0.Colspan = 9;
                    cell0.BorderWidth = 0;
                    table.AddCell(cell0);
                    if (txtSrchUserName.Text != "")
                    {
                        cell1 = PhraseCell(new Phrase(" User Name : " + txtSrchUserName.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                        cell1.Colspan = 9;
                        cell1.BorderWidth = 0;
                        table.AddCell(cell1);
                    }
                    if (txtSrchUserCode.Text != "")
                    {
                        cell2 = PhraseCell(new Phrase("  User Code : " + txtSrchUserCode.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                        cell2.Colspan = 9;
                        cell2.BorderWidth = 0;
                        table.AddCell(cell2);
                    }
                    if (txtSrchMobileNo.Text != "")
                    {
                        cell3 = PhraseCell(new Phrase("  Mobile No : " + txtSrchMobileNo.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                        cell3.Colspan = 9;
                        cell3.BorderWidth = 0;
                        table.AddCell(cell3);
                    }
                    if (txtSrchEmailId.Text != "")
                    {
                        cell4 = PhraseCell(new Phrase("  Email Id : " + txtSrchEmailId.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                        cell4.Colspan = 9;
                        cell4.BorderWidth = 0;
                        table.AddCell(cell4);
                    }
                }
                if (MultiView1.ActiveViewIndex == 1)
                {
                    cell5 = PhraseCell(new Phrase(dtBranch.SelectedItem.ToString() + " Branch Report ", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell5.Colspan = 9;
                    cell5.BorderWidth = 0;
                    table.AddCell(cell5);
                }
                if (MultiView1.ActiveViewIndex == 2)
                {
                    cell6 = PhraseCell(new Phrase(dtDepartment.SelectedItem.ToString() + " Department Report ", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell6.Colspan = 9;
                    cell6.BorderWidth = 0;
                    table.AddCell(cell6);
                }
                if (MultiView1.ActiveViewIndex == 3)
                {
                    cell7 = PhraseCell(new Phrase(dtProfile.SelectedItem.ToString() + " Profile Report ", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell7.Colspan = 9;
                    cell7.BorderWidth = 0;
                    table.AddCell(cell7);
                }


                for (int x = 0; x < grdUsr.Columns.Count; x++)
                {
                    if (x == 0 || x == 10 || x == 12 || x == 11)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(grdUsr.HeaderRow.Cells[x].Text);

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

                    //if (grdUser.Rows[i].RowType == DataControlRowType.DataRow)
                    //{
                    for (int j = 0; j < grdUsr.Columns.Count; j++)
                    {
                        if (j > 8)
                        {
                            continue;
                        }
                        string cellText;
                        //if (j == 0)
                        //{
                        //    cellText = (k).ToString();
                        //    k++;
                        //}
                        //else
                        //{            
                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (j == 8)
                        {
                            if (cellText == "1")
                            {
                                cellText = "Enabled";
                            }
                            if (cellText == "0")
                            {
                                cellText = "Disabled";
                            }

                        }
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        //}
                        //Set Font and Font Color
                        iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);//new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                        font.Color = new BaseColor(0, 0, 0);
                        iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));

                        if (i % 2 == 0)
                        {
                            //Set Row BackGround Color
                            cell.BackgroundColor = new BaseColor(229, 229, 229);

                        }
                        table.AddCell(cell);
                        //}
                    }
                }
                document.Open();
                document.Add(table);
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=UserReport.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
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
                msg.Append("<b>The below table has the details for User.</b>");
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
                lblerrorMailTo.Text = "Please choose whom you want to send e-mail.";
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
                send.SendEmail(EmailId, "User Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
                lblmsg.Text = "Mail sent successfully";
                lblmsg.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception)
            {
                lblmsg.Text = "Could not send the mail.";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            MessagePnl.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
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
                    send.SendEmail(txtMailTo.Text, "User Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
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
                lblerrorMailTo.Text = "Please enter e-mail ID";
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

        protected void lbtnactivate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkstatus = sender as LinkButton;
                GridViewRow gvr = lnkstatus.NamingContainer as GridViewRow;
                Label lblProfileId = (Label)grdUsr.Rows[gvr.RowIndex].FindControl("lblProfileId");
                Label lblIsEnable = (Label)grdUsr.Rows[gvr.RowIndex].FindControl("lblIsEnable");
                Label lblUId = (Label)grdUsr.Rows[gvr.RowIndex].FindControl("lblUId");

                if (!string.IsNullOrEmpty(lblProfileId.Text.Trim()) && !string.IsNullOrEmpty(lblIsEnable.Text.Trim()))
                {
                    profileusermapbal = new ProfileUserMappingBAL();
                    profileusermapbal.ProfileUserId = 0;
                    profileusermapbal.ClientId = ClientId;
                    profileusermapbal.UserId = Convert.ToInt32(lblUId.Text.Trim());
                    profileusermapbal.ProfileId = Convert.ToInt32(lblProfileId.Text.Trim()); ;
                    profileusermapbal.LoggedBy = UserId;
                    profileusermapbal.AppliedDateTime = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
                    profileusermapbal.IsEnable = lblIsEnable.Text == "0" ? 1 : 0;
                    profileusermapbal.Status = 0;
                    profileusermapbal.spProfileUserMapping();
                    SendUpdateMsg(lblUId.Text.Trim());
                    lblmsg.Text = "Profile status updated successfully.";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    BindGrid();
                }
                else
                {
                    lblmsg.Text = "Profile is not applied on the user.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void SendUpdateMsg(string UserIdList)
        {
            UserDeviceBAL usrdevice = new UserDeviceBAL();
            dt = new DataTable();
            usrdevice.UserId = Convert.ToInt32(UserIdList);
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
        protected void grdUsr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DataTable dtuser = (DataTable)ViewState["dtUser"];
                if (dtUser.Rows.Count > 0)
                {
                    int isPassVisible = Convert.ToInt32(dtUser.Rows[0]["IsPasswordVisible"].ToString());
                    if (isPassVisible >= RoleId)
                        e.Row.Cells[10].Text = "DashBoard / Password";
                    else
                        e.Row.Cells[10].Text = "DashBoard";
                }
                else
                {
                    e.Row.Cells[10].Text = "DashBoard";
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtnAttachments = (LinkButton)e.Row.FindControl("lbtnactivate");
                if (lnkbtnAttachments.Text == "Active")
                {
                    lnkbtnAttachments.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lnkbtnAttachments.ForeColor = System.Drawing.Color.Red;
                }

            }
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            user = new usrBAL();
            user.UserId = Convert.ToInt32(lblkeyid.Text);
            if (UserId != Convert.ToInt32(lblkeyid.Text))
            {
                if (user.DeleteUserDtls() == 1)
                {

                    lblmsg.Text = "Deleted successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    SendExpiryMessage(user.UserId);
                    BindGrid();
                }
                else
                {
                    lblmsg.Text = "Could not delete";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(lblkeyid.Text.Trim()) && lblroleid.Text.Contains("Admin"))
                {
                    lblmsg.Text = "You can't delete the account because after that you will not be able to use the product!";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblmsg.Text = "You can't delete yourself!";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblkeyid.Text = "";
            lblroleid.Text = "";
        }
    }
}
