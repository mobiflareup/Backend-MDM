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
    public partial class AttendanceReport1 :Base
    {
        int ClientId, UserId, RoleId, DeptId;
        DDLBAL ddlbal;
        AnuSearch srch;
        StringBuilder msg = new StringBuilder();
        public string Message = "";
        DataTable dt;
        SendMailBAL send;
        AttendanceBAL att;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            txtFrmDt.Attributes.Add("readonly", "readonly");
            txtToDt.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                lblMsg.Text = string.Empty;
                BindUser();
                txtFrmDt.Text = txtToDt.Text = GetCurrentDateTimeByUserId().ToString(Constant.DateFormat);
                BindGrid();
            }
        }
        protected void BindUser()
        {
            try
            {
                System.Web.UI.WebControls.ListItem ls = new System.Web.UI.WebControls.ListItem("--- Select ---", "0");
                ddlbal = new DDLBAL();
                ddlbal.ClientId = ClientId;
                ddlbal.UserId = UserId;
                ddlbal.DeptId = DeptId;
                ddlEmployee.Items.Clear();
                ddlEmployee.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlEmployee.DataSource = ddlbal.GetUserByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlEmployee.DataSource = ddlbal.GetUserWithoutDeptHead();
                }
                else
                {
                    ddlEmployee.Items.Clear();
                    ddlEmployee.DataSource = ddlbal.GetUserByUserId();
                }
                ddlEmployee.DataTextField = "UserName";
                ddlEmployee.DataValueField = "UserId";
                ddlEmployee.DataBind();
            }
            catch (Exception)
            {

            }
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            if (RoleId == 1 || RoleId == 2)
            {
                grdUser.DataSource = srch.GetAttendanceDetails(ClientId, 0, ddlEmployee.SelectedValue.ToString(), "", txtFrmDt.Text.Trim(), txtToDt.Text.Trim(), txtEmpId.Text.Trim());
            }
            else if (RoleId == 3)
            {
                grdUser.DataSource = srch.GetAttendanceDetails(ClientId, 0, ddlEmployee.SelectedValue.ToString(), "", txtFrmDt.Text.Trim(), txtToDt.Text.Trim(), txtEmpId.Text.Trim(), DeptId);
            }
            else
            {
                grdUser.DataSource = srch.GetAttendanceDetails(ClientId, UserId, ddlEmployee.SelectedValue.ToString(), "", txtFrmDt.Text.Trim(), txtToDt.Text.Trim(), txtEmpId.Text.Trim());
            }
            ViewState["dtUser"] = grdUser.DataSource;
            grdUser.DataBind();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlstatus = (DropDownList)e.Row.FindControl("ddlAttendanceStatus");
                Label lblstatus = (Label)e.Row.FindControl("lblAttendanceStatus");
                ddlstatus.SelectedValue = lblstatus.Text.Trim();
            }
        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void btnsavetopdf_Click(object sender, EventArgs e)
        {
            // grdUser.AllowPaging = false;

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

                PdfPCell cell0 = null, cell1 = null, cell2 = null, cell4 = null, cell5 = null;
                cell0 = PhraseCell(new Phrase("Attendance Report", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                cell0.Colspan = 9;
                cell0.BorderWidth = 0;
                table.AddCell(cell0);
                if (Convert.ToInt16(ddlEmployee.SelectedValue) == 0)
                {
                    cell1 = PhraseCell(new Phrase(" User : All ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                else
                {
                    cell1 = PhraseCell(new Phrase(" User : " + ddlEmployee.SelectedItem.ToString(), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                cell1.Colspan = 9;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);

                if (txtEmpId.Text != "")
                {
                    cell2 = PhraseCell(new Phrase(" Employee Id : " + txtEmpId.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell2.Colspan = 9;
                    cell2.BorderWidth = 0;
                    table.AddCell(cell2);
                }
                ////if (txtMobileNo.Text != "")
                ////{
                ////    cell3 = PhraseCell(new Phrase("  Mobile No : " + txtMobileNo.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                ////    cell3.Colspan = 9;
                ////    cell3.BorderWidth = 0;
                ////    table.AddCell(cell3);
                ////}          
                if (txtFrmDt.Text != "")
                {
                    cell4 = PhraseCell(new Phrase("  From Date : " + txtFrmDt.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell4.Colspan = 9;
                    cell4.BorderWidth = 0;
                    table.AddCell(cell4);
                }
                if (txtToDt.Text != "")
                {
                    cell5 = PhraseCell(new Phrase("  To Date : " + txtToDt.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell5.Colspan = 9;
                    cell5.BorderWidth = 0;
                    table.AddCell(cell5);
                }

                //cell7.Colspan = 8;
                //cell7.BorderWidth = 0;
                //table.AddCell(cell7);


                for (int x = 0; x < grdUser.Columns.Count; x++)
                {
                    if (x == 0 || x == 10 || x == 11)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(grdUser.HeaderRow.Cells[x].Text);

                    //Set Font and Font Color
                    iTextSharp.text.Font font = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);//new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
                    font.Color = new iTextSharp.text.BaseColor(255, 255, 255);
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(42, 54, 137);
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    table.AddCell(cell);
                }
                DataTable dtpdf = new DataTable();
                dtpdf = (DataTable)ViewState["dtUser"];
                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    for (int j = 0; j <= grdUser.Columns.Count; j++)
                    {
                        string cellText;
                        if (j > 8)
                        {
                            continue;
                        }
                        if (j == 3)
                        {
                            cellText = Server.HtmlDecode(string.IsNullOrEmpty(dtpdf.Rows[i][14].ToString()) ? dtpdf.Rows[i][3].ToString() : Convert.ToDateTime(dtpdf.Rows[i][14].ToString()).ToString(Constant.DateTimeFormat));
                        }
                        else if (j == 4)
                        {
                            cellText = Server.HtmlDecode(string.IsNullOrEmpty(dtpdf.Rows[i][15].ToString()) ? dtpdf.Rows[i][4].ToString() : Convert.ToDateTime(dtpdf.Rows[i][15].ToString()).ToString(Constant.DateTimeFormat));
                        }
                        else if (j == 7)
                        {
                            cellText = MyFormat(Server.HtmlDecode(dtpdf.Rows[i][3].ToString()), Server.HtmlDecode(dtpdf.Rows[i][4].ToString()), Server.HtmlDecode(dtpdf.Rows[i][14].ToString()), Server.HtmlDecode(dtpdf.Rows[i][15].ToString()));
                        }
                        else if (j == 5)
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "Location not found" ? dtpdf.Rows[i]["InLatitude"].ToString() + "," + dtpdf.Rows[i]["InLongitude"].ToString() : Convert.ToBoolean(dtpdf.Rows[i]["IsInLocationManuallyEntered"].ToString()) ? dtpdf.Rows[i][j].ToString() + " (Manually Entered)" : dtpdf.Rows[i][j].ToString());
                        }
                        else if (j == 6)
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "Location not found" ? dtpdf.Rows[i]["OutLatitude"].ToString() + "," + dtpdf.Rows[i]["OutLongitude"].ToString() : Convert.ToBoolean(dtpdf.Rows[i]["IsOutLocationManuallyEntered"].ToString()) ? dtpdf.Rows[i][j].ToString() + " (Manually Entered)" : dtpdf.Rows[i][j].ToString());
                        }
                        else
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        }
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        if (j == 8)
                        {
                            if (cellText == "0")
                            {
                                cellText = "Absent";
                            }
                            if (cellText == "1")
                            {
                                cellText = "Full Day";
                            }
                            if (cellText == "2")
                            {
                                cellText = "Half Day";
                            }
                        }
                        //Set Font and Font Color
                        iTextSharp.text.Font font;
                        if (cellText.Contains(" (Manually Entered)"))
                        {
                            font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.RED);// new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                            font.Color = new BaseColor(255, 0, 0);
                        }
                        else
                        {
                            font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);// new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                            font.Color = new BaseColor(0, 0, 0);
                        }
                        iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        if (i % 2 == 0)
                        {
                            //Set Row BackGround Color
                            cell.BackgroundColor = new iTextSharp.text.BaseColor(229, 229, 229);
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
                Response.AddHeader("Content-Disposition", "attachment; filename=AttendanceReport.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
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
                msg.Append("<b>The below table has the details for User Attendance.</b>");
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
                send.SendEmail(EmailId, "Attendance Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
                lblMsg.Text = "Mail sent successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception)
            {
                lblMsg.Text = "Could not send the mail.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            MessagePnl.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

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
                    send.SendEmail(txtMailTo.Text, "Attendance Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
                    lblMsg.Text = "Mail sent successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception)
                {
                    lblMsg.Text = "Could not send the mail.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblerrorMailTo.Text = "Please enter e-mail ID";
                lblerrorMailTo.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

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
        public void GrideTable()
        {
            DataTable dtpdf = new DataTable();
            dtpdf = (DataTable)ViewState["dtUser"];
            if (dtpdf.Rows.Count > 0)
            {
                string head = "", UserName = "", EmpId = "", FrmDt = "", ToDt = "";
                try
                {
                    head = "<b style=' font-size: 20px;> Attendance Report </b>";
                    if (Convert.ToInt16(ddlEmployee.SelectedValue) == 0)
                    {
                        UserName = "<br/>  User : All ";
                    }
                    else
                    {
                        UserName = "<br/> User : " + ddlEmployee.SelectedItem.ToString();
                    }
                    if (txtEmpId.Text != "")
                    {
                        EmpId = "<br/> Employee Id : " + txtEmpId.Text;
                    }
                    //if (txtMobileNo.Text != "")
                    //{
                    //    MobNo = "<br/>  Mobile No : " + txtMobileNo.Text;
                    //}

                    if (txtFrmDt.Text != "")
                    {
                        FrmDt = "<br/> From Date : " + txtFrmDt.Text;
                    }
                    if (txtToDt.Text != "")
                    {
                        ToDt = "<br/> To Date : " + txtToDt.Text;
                    }


                }
                catch (Exception)
                {

                }
                Message = ("<table width='100%' cellspacing='0' cellpadding='0' border=0 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9' align='center'>");
                Message = Message + ("<b style=' font-size: 20px;>" + head + "</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9' align='center'>");
                Message = Message + (UserName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9' align='center'>");
                Message = Message + (EmpId);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                //Message = Message + ("<tr>");
                //Message = Message + ("<td colspan='9'align='center'>");
                //Message = Message + (MobNo);
                //Message = Message + ("</td>");
                //Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9'align='center'>");
                Message = Message + (FrmDt);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9'align='center'>");
                Message = Message + (ToDt);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr  style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Employee Id</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>User Name</b>");
                Message = Message + ("</td>");

                //Message = Message + ("<td align='center'>");
                //Message = Message + ("<b>Mobile No</b>");
                //Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Attendance Date</b>");
                Message = Message + ("</td>");


                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Attendance In</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Attendance Out</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Location In</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Location Out</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Duration</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Attendance Status</b>");
                Message = Message + ("</td>");

                Message = Message + ("</tr>");

                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j <= grdUser.Columns.Count; j++)
                    {

                        //if (j == 2 && string.IsNullOrEmpty(dtpdf.Rows[i][j].ToString()))
                        //{
                        //    j = 3;
                        //}
                        string cellText;
                        if (j > 8)
                        {
                            continue;
                        }
                        Message = Message + ("<td align='center'>");
                        if (j == 3)
                        {
                            cellText = Server.HtmlDecode(string.IsNullOrEmpty(dtpdf.Rows[i][14].ToString()) ? dtpdf.Rows[i][3].ToString() : Convert.ToDateTime(dtpdf.Rows[i][14].ToString()).ToString(Constant.DateTimeFormat));
                        }
                        else if (j == 4)
                        {
                            cellText = Server.HtmlDecode(string.IsNullOrEmpty(dtpdf.Rows[i][15].ToString()) ? dtpdf.Rows[i][4].ToString() : Convert.ToDateTime(dtpdf.Rows[i][15].ToString()).ToString(Constant.DateTimeFormat));
                        }
                        else if (j == 5)
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "Location not found" ? dtpdf.Rows[i]["InLatitude"].ToString() + "," + dtpdf.Rows[i]["InLongitude"].ToString() : Convert.ToBoolean(dtpdf.Rows[i]["IsInLocationManuallyEntered"].ToString()) ? dtpdf.Rows[i][j].ToString() + " (Manually Entered)" : dtpdf.Rows[i][j].ToString());
                        }
                        else if (j == 6)
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "Location not found" ? dtpdf.Rows[i]["OutLatitude"].ToString() + "," + dtpdf.Rows[i]["OutLongitude"].ToString() : Convert.ToBoolean(dtpdf.Rows[i]["IsOutLocationManuallyEntered"].ToString()) ? dtpdf.Rows[i][j].ToString() + " (Manually Entered)" : dtpdf.Rows[i][j].ToString());
                        }
                        else if (j == 7)
                        {
                            cellText = MyFormat(Server.HtmlDecode(dtpdf.Rows[i][3].ToString()), Server.HtmlDecode(dtpdf.Rows[i][4].ToString()), Server.HtmlDecode(dtpdf.Rows[i][14].ToString()), Server.HtmlDecode(dtpdf.Rows[i][15].ToString()));
                        }
                        else
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        }


                        // cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        if (j == 8)
                        {
                            if (cellText == "0")
                            {
                                cellText = "Absent";
                            }
                            if (cellText == "1")
                            {
                                cellText = "Full Day";
                            }
                            if (cellText == "2")
                            {
                                cellText = "Half Day";
                            }
                        }
                        Message = Message + (cellText);
                        Message = Message + ("</td>");
                    }
                    Message = Message + ("</tr>");
                }
                Message = Message + ("</table>");
            }
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
            //DataTable dt = BindGrid();
        }
        protected void ddlAttendanceStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList ddlAttendancestatus = (DropDownList)row.FindControl("ddlAttendanceStatus");
            string Attendancestatus = ddlAttendancestatus.SelectedValue.ToString();
            string AttendanceId = grdUser.DataKeys[row.RowIndex].Value.ToString();
            att = new AttendanceBAL();
            att.UserId = UserId;
            att.AttendanceId = Convert.ToInt32(AttendanceId);
            att.UpdationDate = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm");
            att.AttendanceStatus = Attendancestatus;
            int res = att.UpdateAttendanceStatus();
            if (res > 0)
            {
                lblMsg.Text = "Updated successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            else
            {
                lblMsg.Text = "Not updated!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
        }
        protected string MyFormat(object InTime, object OutTime, object InDateTime, object OutDateTime)
        {
            try
            {
                if (!string.IsNullOrEmpty(InDateTime.ToString()) && !string.IsNullOrEmpty(OutDateTime.ToString()))
                {
                    InTime = InDateTime;
                    OutTime = OutDateTime;
                }
                if (InTime != null && OutTime != null)
                {
                    DateTime d2 = Convert.ToDateTime(InTime);
                    DateTime d1 = Convert.ToDateTime(OutTime);
                    TimeSpan ts = d1.Subtract(d2);
                    int res = 0;
                    res = ts.Days * 24 + ts.Hours;
                    return (res < 10 ? "0" + res.ToString() : res.ToString()) + ":" + (ts.Minutes < 10 ? "0" + ts.Minutes.ToString() : ts.Minutes.ToString());
                }
                else
                {
                    return "00:00";
                }
            }
            catch (Exception)
            {
                return "00:00";
            }
        }
    }
}
