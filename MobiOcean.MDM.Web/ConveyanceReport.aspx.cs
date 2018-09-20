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
    public partial class ConveyanceReport : Base
    {
        int ClientId, UserId, RoleId, DeptId, Cuserid;
        decimal Conrate = 0;
        DDLBAL ddlbal;
        AnuSearch srch;
        StringBuilder msg = new StringBuilder();
        public string Message = "";
        DataTable dt;
        SendMailBAL send;

        Decimal TotalDis1;
        ConveyanceBAL conveyance;
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
                ViewState["UserIdList"] = "";
                BindUser();
                txtFrmDt.Text = txtToDt.Text = GetCurrentDateTimeByUserId().ToString(Constant.DateFormat);
                BindGrid();
            }
            btnApprove.Visible = false;
        }
        protected void BindUser()
        {
            try
            {
                System.Web.UI.WebControls.ListItem ls = new System.Web.UI.WebControls.ListItem("--- All ---", "0");
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
                grdUser.DataSource = srch.GetConveyanceDetails(ClientId, 0, ddlEmployee.SelectedValue.ToString(), txtMobileNo.Text.Trim(), txtFrmDt.Text.Trim(), txtToDt.Text.Trim(), txtEmpId.Text.Trim());
            }
            else if (RoleId == 3)
            {
                grdUser.DataSource = srch.GetConveyanceDetails(ClientId, 0, ddlEmployee.SelectedValue.ToString(), txtMobileNo.Text.Trim(), txtFrmDt.Text.Trim(), txtToDt.Text.Trim(), txtEmpId.Text.Trim(), DeptId);
            }
            else
            {
                grdUser.DataSource = srch.GetConveyanceDetails(ClientId, UserId, ddlEmployee.SelectedValue.ToString(), txtMobileNo.Text.Trim(), txtFrmDt.Text.Trim(), txtToDt.Text.Trim(), txtEmpId.Text.Trim());
            }
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            ViewState["dtUser"] = grdUser.DataSource;
            dt = (DataTable)ViewState["dtUser"];
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Cuserid = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[i]["UserId"].ToString()) ? 0 : dt.Rows[i]["UserId"]);
                TotalDis1 += Convert.ToDecimal(string.IsNullOrEmpty(dt.Rows[i]["Distance"].ToString()) ? 0 : dt.Rows[i]["Distance"]);
            }
            if (Cuserid > 0)
            {
                dt1 = srch.GetConveyaRt(Cuserid);
                if (dt1.Rows.Count > 0)
                    Conrate = Convert.ToDecimal(string.IsNullOrEmpty(dt1.Rows[0]["KM"].ToString()) ? 0 : dt1.Rows[0]["KM"]);
            }
            Session["conrate"] = Conrate.ToString("N2");
            Session["grandtotal"] = TotalDis1.ToString("N2");
            Session["totalConrate"] = (Conrate * TotalDis1).ToString("N2");
            grdUser.DataBind();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGrid();
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

                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(13);

                PdfPCell cell0 = null, cell1 = null, cell2 = null, cell3 = null, cell4 = null, cell5 = null, cell6 = null, cell7 = null;
                cell0 = PhraseCell(new Phrase("Conveyance Report", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                cell0.Colspan = 13;
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
                cell1.Colspan = 13;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);

                if (txtEmpId.Text != "")
                {
                    cell2 = PhraseCell(new Phrase(" Employee Id : " + txtEmpId.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell2.Colspan = 13;
                    cell2.BorderWidth = 0;
                    table.AddCell(cell2);
                }
                if (txtMobileNo.Text != "")
                {
                    cell3 = PhraseCell(new Phrase("  Mobile No : " + txtMobileNo.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell3.Colspan = 13;
                    cell3.BorderWidth = 0;
                    table.AddCell(cell3);
                }
                if (txtFrmDt.Text != "")
                {
                    cell4 = PhraseCell(new Phrase("  From Date : " + txtFrmDt.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell4.Colspan = 13;
                    cell4.BorderWidth = 0;
                    table.AddCell(cell4);
                }
                if (txtToDt.Text != "")
                {
                    cell5 = PhraseCell(new Phrase("  To Date : " + txtToDt.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell5.Colspan = 13;
                    cell5.BorderWidth = 0;
                    table.AddCell(cell5);
                }



                for (int x = 0; x < grdUser.Columns.Count; x++)
                {
                    if (x == 0 || x == 11 || x == 14 || x == 16 || x == 17 || x == 18)
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

                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    for (int j = 0; j < grdUser.Columns.Count; j++)
                    {
                        if (j > 12)
                        {
                            continue;
                        }
                        //if (j == 2 && string.IsNullOrEmpty(dtpdf.Rows[i][j].ToString()))
                        //{
                        //    j = 3;
                        //}
                        string cellText;

                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        if (j == 4)
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "Location not found" ? dtpdf.Rows[i]["FromLatitude"].ToString() + "," + dtpdf.Rows[i]["FromLongitude"].ToString() : dtpdf.Rows[i][j].ToString());
                        }
                        else if (j == 5)
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "Location not found" ? dtpdf.Rows[i]["ToLatitude"].ToString() + "," + dtpdf.Rows[i]["ToLongitude"].ToString() : dtpdf.Rows[i][j].ToString());
                        }
                        else if (j == 6)
                        {
                            cellText = Convert.ToDouble(cellText).ToString("F");
                        }
                        //Set Font and Font Color
                        iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);// new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                        font.Color = new iTextSharp.text.BaseColor(0,0,0);
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


                if (Session["grandtotal"] != null)
                {
                    cell6 = PhraseCell(new Phrase("Grand Total Distance : " + Session["grandtotal"].ToString(), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell6.Colspan = 13;
                    cell6.BorderWidth = 0;
                    table.AddCell(cell6);

                    if (Session["totalConrate"] != null)
                    {

                        cell7 = PhraseCell(new Phrase("Total Conveyance : " + Session["totalConrate"].ToString(), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                        cell7.Colspan = 13;
                        cell7.BorderWidth = 0;
                        table.AddCell(cell7);
                    }
                }

                document.Open();
                document.Add(table);
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=ConveyanceReport.pdf");
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
                msg.Append("<b>The below table has the details for Conveyance.</b>");
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
                send.SendEmail(EmailId, "Conveyance Report ", msgbody, ClientId);
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
                    send.SendEmail(txtMailTo.Text, "Conveyance Report", msgbody, ClientId);
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
            MessagePnl.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

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
                string head = "", UserName = "", EmpId = "", FrmDt = "", MobNo = "", ToDt = "";
                try
                {
                    head = "<b style=' font-size: 20px;> Conveyance Report </b>";
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
                    if (txtMobileNo.Text != "")
                    {
                        MobNo = "<br/>  Mobile No : " + txtMobileNo.Text;
                    }

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
                Message = Message + ("<td colspan='13' align='center'>");
                Message = Message + ("<b style=' font-size: 20px;>" + head + "</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='13' align='center'>");
                Message = Message + (UserName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='13' align='center'>");
                Message = Message + (EmpId);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='13'align='center'>");
                Message = Message + (MobNo);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='13'align='center'>");
                Message = Message + (FrmDt);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='13'align='center'>");
                Message = Message + (ToDt);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");
                Message = Message + ("</table>");

                Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr  style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Employee Id</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>User Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>From Date</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>To Date</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>From Location</b>");
                Message = Message + ("</td>");


                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>To Location</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Distance</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Remark</b>");
                Message = Message + ("</td>");


                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Vehicle Start Reading</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Start Time Remark</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Vehicle Stop Reading</b>");
                Message = Message + ("</td>");


                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Stop Time Remark</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Distance According To Vehicle Reading</b>");
                Message = Message + ("</td>");


                Message = Message + ("</tr>");

                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j < grdUser.Columns.Count; j++)
                    {
                        if (j > 12)
                        {
                            continue;
                        }
                        Message = Message + ("<td align='center'>");
                        //if (j == 2 && string.IsNullOrEmpty(dtpdf.Rows[i][j].ToString()))
                        //{
                        //    j = 3;
                        //}
                        string cellText;

                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        if (j == 6)
                        {
                            cellText = Convert.ToDouble(cellText).ToString("F");
                        }
                        Message = Message + (cellText);
                        Message = Message + ("</td>");
                    }
                    Message = Message + ("</tr>");
                }
                Message = Message + ("</table>");


                if (Session["grandtotal"] != null)
                {
                    Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=0 style=' background-color:#F1F1F1; font-size: 10px;'>");



                    Message = Message + ("<tr>");
                    Message = Message + ("<td colspan='13'align='center'>");
                    Message = Message + "Grand Total Distance : " + Session["grandtotal"].ToString();
                    Message = Message + ("</td>");
                    Message = Message + ("</tr>");

                    if (Session["totalConrate"] != null)
                    {
                        Message = Message + ("<tr>");
                        Message = Message + ("<td colspan='13'align='center'>");
                        Message = Message + "Total Conveyance : " + Session["totalConrate"].ToString();
                        Message = Message + ("</td>");
                        Message = Message + ("</tr>");

                    }
                    Message = Message + ("</table>");
                }
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
        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string filename = e.CommandArgument.ToString();
                if (string.IsNullOrEmpty(filename))
                {
                    //lblMsg.Text = "There is no file to download";
                }
                else
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "filename=" + Path.GetFileName(filename));
                    Response.TransmitFile(Server.MapPath("~/Files/Android_Files/") + filename);
                    Response.End();

                }
            }
            catch (Exception)
            {
                lblMsg.Text = "File not found!";
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

        }

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (ddlEmployee.SelectedIndex == 0)
            {
                grdUser.ShowFooter = false;
                //grdUser.FooterRow.Visible = false;
            }
            else
            {
                // FINALLY, SHOW THE RUNNING AND GRAND TOTAL ON EACH PAGE.
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    try
                    {
                        grdUser.ShowFooter = true;
                        //grdUser.FooterRow.Cells[1].ColumnSpan = 7;
                        //grdUser.FooterRow.Cells[0].BackColor = System.Drawing.Color.Red;

                        //grdUser.FooterRow.Cells.RemoveAt(0);
                        //for (int i = 2; i <= 8; i++)
                        //    grdUser.FooterRow.Cells.RemoveAt(i);
                        //Label lblPageTotal = (Label)e.Row.FindControl("lblpageDistanceG");
                        //lblPageTotal.Text = TotalDis.ToString();

                        //GRAND TOTAL.
                        Label lblGrandTotal = (Label)e.Row.FindControl("lblTotalDistanceG");
                        lblGrandTotal.Text = Session["grandtotal"].ToString();
                        //Label lblCon = (Label)e.Row.FindControl("lblConveyancerate");
                        //lblCon.Text = Conrate.ToString("N2");
                        Label lblConRt = (Label)e.Row.FindControl("lblConRt");
                        lblConRt.Text = Session["totalConrate"].ToString();
                        btnApprove.Visible = true;
                        //}
                    }
                    catch (Exception)
                    { }
                }
            }
        }

        protected void grdUser_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                try
                {
                    for (int i = 0; i < grdUser.Columns.Count - 2; i++)
                        e.Row.Cells.RemoveAt(2);
                    e.Row.Cells[1].ColumnSpan = 18;
                }
                catch (Exception)
                { }
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Decimal distancecalculate = 0;
            int i = 0;
            foreach (GridViewRow row in grdUser.Rows)
            {

                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkboxRows_Parents");
                //Label lblcatid = (Label)row.FindControl("lblCategoryId");
                if (ChkBoxRows.Checked)
                {
                    i++;
                    Label lblGrandTotal = (Label)row.FindControl("lblDistance");
                    distancecalculate += decimal.Parse(lblGrandTotal.Text);
                }

            }
            if (i != 0)
            {
                Decimal grandconrt = (Conrate * distancecalculate);
                conveyance = new ConveyanceBAL();
                string FromDate = "", ToDate = "";
                int ApprovedUser = 0, ConveyanceId = 0, ApprovedconveyanceId = 0;
                dt = (DataTable)ViewState["dtUser"];
                if (dt.Rows.Count > 0)
                {
                    ApprovedUser = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ToDate = Convert.ToDateTime(dt.Rows[0]["LogDateTime"].ToString()).Date.ToString(Constant.DateFormat);
                    FromDate = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["LogDateTime"].ToString()).Date.ToString(Constant.DateFormat);
                    conveyance.FromDate = FromDate;
                    conveyance.ToDate = ToDate;
                    conveyance.UserId = ApprovedUser;
                    conveyance.ApprovedBy = UserId;
                    conveyance.TotalDistance = distancecalculate.ToString();
                    conveyance.ConveyanceRate = Conrate.ToString();
                    conveyance.TotalAmount = double.Parse(grandconrt.ToString());
                    conveyance.ClientId = Convert.ToInt32(Session["ClientId"].ToString());
                    ApprovedconveyanceId = conveyance.InsertApprovedConveyanceDetails();

                    foreach (GridViewRow row in grdUser.Rows)
                    {
                        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkboxRows_Parents");
                        //Label lblcatid = (Label)row.FindControl("lblCategoryId");
                        if (ChkBoxRows.Checked)
                        {

                            Label lblConveyanceId = (Label)row.FindControl("lblId");
                            ConveyanceId = Convert.ToInt32(lblConveyanceId.Text);
                            conveyance.ConveyanceId = ConveyanceId;
                            conveyance.ApprovedBy = UserId;
                            conveyance.CreatedBy = UserId.ToString();
                            conveyance.ConveyanceHistoryId = ApprovedconveyanceId;
                            int res = conveyance.UpdateConveyance();
                            if (res > 0)
                            {
                                lblMsg.Text = "Approved successfully";
                                lblMsg.ForeColor = System.Drawing.Color.Green;
                                BindGrid();

                            }
                            else
                            {
                                lblMsg.Text = "Something went wrong!";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }

            }
            else
            {
                lblMsg.Text = "Please select record!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                btnApprove.Visible = true;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");


        }
        protected void lnkbtndownload_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(filePath);
                Response.End();
            }
            catch (Exception)
            {

            }

        }
        protected void btnhstry_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApprovedConveyanceReport.aspx");
        }
        protected void grdUser_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdUser.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdUser_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            grdUser.EditIndex = -1;
            BindGrid();
        }
        protected void grdUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string regexexpression = @"^[0-9]+(\.([0-9]{1,2})?)?$";
            GridViewRow gvr = grdUser.Rows[e.RowIndex];
            try
            {
                string km = ((TextBox)gvr.FindControl("txtDistance")).Text.Trim();
                if (Regex.IsMatch(km, regexexpression))
                {
                    conveyance = new ConveyanceBAL();
                    conveyance.ConveyanceId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                    conveyance.Distance = km;
                    int res = conveyance.UpdateDistanceInConveyance();
                    if (res > 0)
                    {
                        lblMsg.Text = "Updated successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        grdUser.EditIndex = -1;
                        BindGrid();
                    }
                }
                else
                {
                    lblMsg.Text = "Enter distance with 2 decimal precision";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    grdUser.EditIndex = -1;
                    BindGrid();
                }
            }
            finally
            {
                conveyance = null;
            }
        }
        protected void lnkbtnShowOnMap_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = sender as LinkButton;
            GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
            string conveyanceId = grdUser.DataKeys[row.RowIndex].Value.ToString();
            Response.Redirect("~/ConveyanceLocation.aspx?Id=" + conveyanceId);
        }

        protected void lnkbtnView_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkPopUp = sender as LinkButton;
                GridViewRow gvr = lnkPopUp.NamingContainer as GridViewRow;
                Label lbl = ((Label)grdUser.Rows[gvr.RowIndex].FindControl("lblId"));
                dt = new DataTable();
                srch = new AnuSearch();
                dt = srch.GetConveyanceDetailsByConveyanceId(Convert.ToInt32(lbl.Text));
                if (dt.Rows.Count > 0)
                {
                    lblEmpId.Text = string.IsNullOrEmpty(dt.Rows[0]["EmpCompanyId"].ToString()) ? "---" : dt.Rows[0]["EmpCompanyId"].ToString();
                    lblEmpName.Text = string.IsNullOrEmpty(dt.Rows[0]["UserName"].ToString()) ? "---" : dt.Rows[0]["UserName"].ToString();
                    lblFromDate.Text = string.IsNullOrEmpty(dt.Rows[0]["LogDateTime"].ToString()) ? "---" : dt.Rows[0]["LogDateTime"].ToString();
                    lblToDate.Text = string.IsNullOrEmpty(dt.Rows[0]["ToLogDateTime"].ToString()) ? "---" : dt.Rows[0]["ToLogDateTime"].ToString();
                    lblFromLoc.Text = string.IsNullOrEmpty(dt.Rows[0]["FromLocation"].ToString()) ? "---" : dt.Rows[0]["FromLocation"].ToString();
                    lblToLoc.Text = string.IsNullOrEmpty(dt.Rows[0]["ToLocation"].ToString()) ? "---" : dt.Rows[0]["ToLocation"].ToString();
                    lblStartReading.Text = string.IsNullOrEmpty(dt.Rows[0]["VehicleStartReading"].ToString()) ? "---" : dt.Rows[0]["VehicleStartReading"].ToString();
                    lblStartRemark.Text = string.IsNullOrEmpty(dt.Rows[0]["StartTimeRemark"].ToString()) ? "---" : dt.Rows[0]["StartTimeRemark"].ToString();
                    lblStopReading.Text = string.IsNullOrEmpty(dt.Rows[0]["VehicleStopReading"].ToString()) ? "---" : dt.Rows[0]["VehicleStopReading"].ToString();
                    lblStopRemark.Text = string.IsNullOrEmpty(dt.Rows[0]["StopTimeRemark"].ToString()) ? "---" : dt.Rows[0]["StopTimeRemark"].ToString();
                    lblReadingDistance.Text = string.IsNullOrEmpty(dt.Rows[0]["VehicleReadingDistance"].ToString()) ? "---" : string.Format(dt.Rows[0]["VehicleReadingDistance"].ToString(), "{0:0.00}");
                    lblPopUpDistance.Text = string.IsNullOrEmpty(dt.Rows[0]["Distance"].ToString()) ? "---" : Convert.ToDouble(dt.Rows[0]["Distance"]).ToString("F");
                    lblPopUpRemark.Text = string.IsNullOrEmpty(dt.Rows[0]["Remark"].ToString()) ? "---" : dt.Rows[0]["Remark"].ToString();
                }

                mpdtl.Show();
            }
            catch (Exception)
            {

            }
        }
    }
}