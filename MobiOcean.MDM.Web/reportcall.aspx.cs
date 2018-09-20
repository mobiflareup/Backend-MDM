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
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class reportcall1 : Base
    {
        DataTable dt;
        int ClientId, UserId, RoleId, DeptId;
        public string Message = "";
        StringBuilder msg = new StringBuilder();
        AnuSearch srch;
        DDLBAL ddlbal;
        SendMailBAL send;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            txtFrmDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                BindUsrName();
                txtFrmDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy");
                BindGrid();

            }
            Chart1.Style.Add("width", "100%");
        }
        protected void BindGrid()
        {
            //txtFrmDate.Text = txtFrmDate.Text;
            //txtToDate.Text = txtToDate.Text;
            DataTable dtUser = new DataTable();
            string StartTime, EndTime;
            try
            {
                StartTime = ddlFromHour.SelectedItem.Text + ":" + ddlFromMin.SelectedItem.Text;
                EndTime = ddlToHour.SelectedItem.Text + ":" + ddlToMin.SelectedItem.Text;
                srch = new AnuSearch();
                if (RoleId == 1 || RoleId == 2)
                {
                    dtUser = srch.SrchReportCallDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), txtSrchNo.Text.Trim(), ddlDirection.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), StartTime, EndTime);
                }
                else if (RoleId == 3)
                {
                    dtUser = srch.SrchReportCallDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), txtSrchNo.Text.Trim(), ddlDirection.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), StartTime, EndTime, DeptId);
                }
                else
                {
                    dtUser = srch.SrchReportCallDtls(ClientId, UserId, ddlUserName.SelectedValue.ToString(), txtSrchNo.Text.Trim(), ddlDirection.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), StartTime, EndTime);
                }
                grdmsgh.DataSource = dtUser;
                ViewState["dtUser"] = dtUser;
                grdmsgh.DataBind();
                DataTable dt = GetData();
                LoadChartData(dt);
            }


            catch (Exception)
            {
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {

            /* Verifies that the control is rendered */

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

                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(8);

                PdfPCell cell0 = null, cell1 = null, cell2 = null, cell3 = null, cell4 = null, cell5 = null, cell6 = null, cell7 = null;
                cell0 = PhraseCell(new Phrase(" Call Log", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                cell0.Colspan = 8;
                cell0.BorderWidth = 0;
                table.AddCell(cell0);
                if (Convert.ToInt16(ddlUserName.SelectedValue) == 0)
                {
                    cell1 = PhraseCell(new Phrase(" Device/User Name : All ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                else
                {
                    cell1 = PhraseCell(new Phrase(" Device/User Name : " + ddlUserName.SelectedItem.ToString(), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                cell1.Colspan = 8;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);

                if (txtSrchNo.Text != "")
                {
                    cell2 = PhraseCell(new Phrase(" From/To : " + txtSrchNo.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell2.Colspan = 8;
                    cell2.BorderWidth = 0;
                    table.AddCell(cell2);
                }
                if (txtFrmDate.Text != "")
                {
                    cell3 = PhraseCell(new Phrase("  From Date : " + txtFrmDate.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell3.Colspan = 8;
                    cell3.BorderWidth = 0;
                    table.AddCell(cell3);
                }
                if (ddlFromHour.SelectedItem.ToString() != "HH" && ddlFromMin.SelectedItem.ToString() != "MM")
                {
                    cell5 = PhraseCell(new Phrase(" From Time : " + ddlFromHour.SelectedItem.ToString() + " : " + ddlFromMin.SelectedItem.ToString(), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell5.Colspan = 8;
                    cell5.BorderWidth = 0;
                    table.AddCell(cell5);
                }
                if (txtToDate.Text != "")
                {
                    cell4 = PhraseCell(new Phrase("  To Date : " + txtToDate.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell4.Colspan = 8;
                    cell4.BorderWidth = 0;
                    table.AddCell(cell4);
                }
                if (ddlToHour.SelectedItem.ToString() != "HH" && ddlToMin.SelectedItem.ToString() != "MM")
                {
                    cell6 = PhraseCell(new Phrase(" To Time : " + ddlToHour.SelectedItem.ToString() + " : " + ddlToMin.SelectedItem.ToString(), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell6.Colspan = 8;
                    cell6.BorderWidth = 0;
                    table.AddCell(cell6);
                }
                if (Convert.ToInt16(ddlDirection.SelectedValue) == 100)
                {
                    cell7 = PhraseCell(new Phrase(" Direction : All", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                if (Convert.ToInt16(ddlDirection.SelectedValue) == 1)
                {
                    cell7 = PhraseCell(new Phrase(" Direction : Incoming", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                if (Convert.ToInt16(ddlDirection.SelectedValue) == 0)
                {
                    cell7 = PhraseCell(new Phrase(" Direction : Outgoing", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                cell7.Colspan = 8;
                cell7.BorderWidth = 0;
                table.AddCell(cell7);


                for (int x = 0; x < grdmsgh.Columns.Count; x++)
                {
                    if (x == 0)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(grdmsgh.HeaderRow.Cells[x].Text);

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
                    for (int j = 0; j < grdmsgh.Columns.Count; j++)
                    {
                        if (j > 8 || j == 3)
                        {
                            continue;
                        }
                        if (j == 2 && string.IsNullOrEmpty(dtpdf.Rows[i][j].ToString()))
                        {
                            j = 3;
                        }
                        string cellText;

                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        if (j == 8)
                        {
                            if (cellText == "1")
                            {
                                cellText = "Incoming";
                            }
                            if (cellText == "0")
                            {
                                cellText = "Outgoing";
                            }
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
                document.Open();
                document.Add(table);
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=CallReport.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
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
            DataTable dt = GetData();
            LoadChartData(dt);
        }
        public void GrideTable()
        {
            DataTable dtpdf = new DataTable();
            dtpdf = (DataTable)ViewState["dtUser"];
            if (dtpdf.Rows.Count > 0)
            {
                string head = "", DevName = "", FrTo = "", FrmDt = "", FrTM = "", ToDt = "", ToTM = "", Direction = "";
                try
                {
                    head = "<b style=' font-size: 20px;> Message Log </b>";
                    if (Convert.ToInt16(ddlUserName.SelectedValue) == 0)
                    {
                        DevName = "<br/>  Device/User Name : All ";
                    }
                    else
                    {
                        DevName = "<br/> Device/User Name : " + ddlUserName.SelectedItem.ToString();
                    }
                    if (txtSrchNo.Text != "")
                    {
                        FrTo = "<br/> From/To : " + txtSrchNo.Text;
                    }
                    if (txtFrmDate.Text != "")
                    {
                        FrmDt = "<br/>  From Date : " + txtFrmDate.Text;
                    }
                    if (ddlFromHour.SelectedItem.ToString() != "HH" && ddlFromMin.SelectedItem.ToString() != "MM")
                    {
                        FrTM = "<br/> From Time : " + ddlFromHour.SelectedItem.ToString() + " : " + ddlFromMin.SelectedItem.ToString();
                    }
                    if (txtToDate.Text != "")
                    {
                        ToDt = "<br/> To Date : " + txtToDate.Text;
                    }
                    if (ddlToHour.SelectedItem.ToString() != "HH" && ddlToMin.SelectedItem.ToString() != "MM")
                    {
                        ToTM = "<br/> To Time : " + ddlToHour.SelectedItem.ToString() + " : " + ddlToMin.SelectedItem.ToString();
                    }
                    if (Convert.ToInt16(ddlDirection.SelectedValue) == 100)
                    {
                        Direction = "<br/> Direction : All";
                    }
                    if (Convert.ToInt16(ddlDirection.SelectedValue) == 1)
                    {
                        Direction = "<br/> Direction : Incoming";
                    }
                    if (Convert.ToInt16(ddlDirection.SelectedValue) == 0)
                    {
                        Direction = "<br/> Direction : Outgoing";
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
                Message = Message + (DevName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9' align='center'>");
                Message = Message + (FrTo);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9'align='center'>");
                Message = Message + (FrmDt);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9'align='center'>");
                Message = Message + (FrTM);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9'align='center'>");
                Message = Message + (ToDt);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9'align='center'>");
                Message = Message + (ToTM);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9'align='center'>");
                Message = Message + (Direction);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");
                Message = Message + ("</table >");

                Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr  style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>User Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Device Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Call From/To</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Start DateTime</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>End DateTime</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Duration (HH:MM:SS)</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Location</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Direction</b>");
                Message = Message + ("</td>");

                Message = Message + ("</tr>");

                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j < grdmsgh.Columns.Count; j++)
                    {
                        if (j > 8 || j == 3)
                        {
                            continue;
                        }
                        Message = Message + ("<td align='center'>");
                        if (j == 2 && string.IsNullOrEmpty(dtpdf.Rows[i][j].ToString()))
                        {
                            j = 3;
                        }
                        string cellText;

                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        if (j == 8)
                        {
                            if (cellText == "1")
                            {
                                cellText = "Incoming";
                            }
                            if (cellText == "0")
                            {
                                cellText = "Outgoing";
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
                msg.Append("<b>The below table has the details for Call Log.</b>");
                msg.AppendLine(); msg.AppendLine();

                GrideTable();
                msg.Append(Message);

                msg.AppendLine();
                msg.AppendLine();
                msg.Append("Have a nice day :)");
                msg.AppendLine(); msg.AppendLine(); msg.AppendLine();

                msg.Append("Regards");
                msg.AppendLine();
                msg.Append("Mobiocean Team");


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
                send.SendEmail(EmailId, "Call Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
                lblMsg.Text = "MailMail sent successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception)
            {
                lblMsg.Text = "Could not send the mail.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
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
                    send.SendEmail(txtMailTo.Text, "Call Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
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
        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderWidth = 1;
            cell.BorderColor = iTextSharp.text.BaseColor.BLACK;
            cell.VerticalAlignment = align;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 5f;
            return cell;
        }
        private void LoadChartData(DataTable initialDataSource)
        {
            for (int i = 2; i < initialDataSource.Columns.Count; i++)
            {
                Series series = new Series();

                foreach (DataRow dr in initialDataSource.Rows)
                {
                    //if (ddlDirection.SelectedValue == "100")
                    //{
                    int y = Convert.ToInt32(string.IsNullOrEmpty(dr[i].ToString()) ? "0" : dr[i].ToString());
                    series.Points.AddXY(string.IsNullOrEmpty(dr["date1"].ToString()) ? Convert.ToDateTime(dr["date2"]).ToString("dd MMM yyyy") : Convert.ToDateTime(dr["date1"]).ToString("dd MMM yyyy"), y);
                    //pnlfull.Visible = true;
                    //pnlInComing.Visible = false;
                    //pnlOutgoing.Visible = false;
                    //    }
                    //    else
                    //    if(ddlDirection.SelectedValue == "1")
                    //    {
                    //         int y = Convert.ToInt32(string.IsNullOrEmpty(dr[i].ToString()) ? "0" : dr[i].ToString());
                    //        series.Points.AddXY(Convert.ToDateTime(dr["date1"]).ToString("dd MMM yyyy"), y);
                    //        pnlfull.Visible=false;
                    //pnlInComing.Visible=true;
                    //pnlOutgoing.Visible=false;
                    //    }
                    //    else
                    //    if(ddlDirection.SelectedValue == "0")
                    //    {
                    //         int y = Convert.ToInt32(string.IsNullOrEmpty(dr[i].ToString()) ? "0" : dr[i].ToString());
                    //        series.Points.AddXY(Convert.ToDateTime(dr["date2"]).ToString("dd MMM yyyy"), y);
                    //        pnlfull.Visible=false;
                    //pnlInComing.Visible=false;
                    //pnlOutgoing.Visible=true;;
                    //    }
                    //    else
                    //    {
                    //         int y = Convert.ToInt32(string.IsNullOrEmpty(dr[i].ToString()) ? "0" : dr[i].ToString());
                    //        series.Points.AddXY(string.IsNullOrEmpty(dr["date1"].ToString()) ? Convert.ToDateTime(dr["date2"]).ToString("dd MMM yyyy") : Convert.ToDateTime(dr["date1"]).ToString("dd MMM yyyy"), y);
                    //     pnlfull.Visible=true;
                    //pnlInComing.Visible=false;
                    //pnlOutgoing.Visible=false;;
                    //    }

                    //}
                }
                Chart1.Series.Add(series);
            }
        }
        protected DataTable GetData()
        {
            DataTable dt1 = new DataTable();

            srch = new AnuSearch();

            string StartTime, EndTime;
            try
            {
                StartTime = ddlFromHour.SelectedItem.Text + ":" + ddlFromMin.SelectedItem.Text;
                EndTime = ddlToHour.SelectedItem.Text + ":" + ddlToMin.SelectedItem.Text;
                srch = new AnuSearch();
                if (RoleId == 1 || RoleId == 2)
                {
                    dt1 = srch.SrchReportCallDtlsForChart(ClientId, 0, ddlUserName.SelectedValue.ToString(), txtSrchNo.Text.Trim(), ddlDirection.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), StartTime, EndTime);
                }
                else if (RoleId == 3)
                {
                    dt1 = srch.SrchReportCallDtlsForChart(ClientId, 0, ddlUserName.SelectedValue.ToString(), txtSrchNo.Text.Trim(), ddlDirection.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), StartTime, EndTime, DeptId);
                }
                else
                {
                    dt1 = srch.SrchReportCallDtlsForChart(ClientId, UserId, ddlUserName.SelectedValue.ToString(), txtSrchNo.Text.Trim(), ddlDirection.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), StartTime, EndTime);
                }
            }
            catch (Exception)
            {
            }
            return dt1;
        }
        protected void BindUsrName()
        {
            try
            {
                ddlbal = new DDLBAL();
                ddlbal.UserId = UserId;
                ddlbal.ClientId = ClientId;
                ddlbal.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(new System.Web.UI.WebControls.ListItem("--- All ---", "0"));
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = ddlbal.GetUserDeviceByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = ddlbal.GetUsrDeviceByDeptHead();
                }
                else
                {
                    ddlUserName.DataSource = ddlbal.GetUserDeviceByUserId();
                }
                ddlUserName.DataTextField = "DeviceName";
                ddlUserName.DataValueField = "DeviceId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                ddlbal = null;
            }
        }
        protected void grdmsgh_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdmsgh.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            grdmsgh.PageIndex = 0;
            BindGrid();

        }
        protected void grdmsgh_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected string MyFormat(string from, string to, string IsIncoming)
        {
            if (IsIncoming == "1")
            {
                return from;
            }
            else
            {
                return to;
            }
        }
        protected string TimeCalculate(object TimeInSec)
        {
            string result = "", h, m, s;
            int totalSeconds = 0;
            totalSeconds = Convert.ToInt32(TimeInSec.ToString());
            //int totalSeconds = 7565;

            int hours = totalSeconds / 3600;
            if (hours < 10)
            {
                h = ("0" + hours);
            }
            else
                h = hours.ToString();
            int minutes = (totalSeconds % 3600) / 60;
            if (minutes < 10)
            {
                m = ("0" + minutes);
            }
            else
                m = minutes.ToString();
            int seconds = (totalSeconds % 60);
            if (seconds < 10)
            {
                s = ("0" + seconds);
            }
            else
                s = seconds.ToString();
            result = String.Format("{0}:{1}:{2}", h, m, s);
            return result;
        }
    }
}
