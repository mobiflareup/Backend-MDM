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
    public partial class AppLogsDetail : ReportBase
    {
        int ClientId, UserId, RoleId, DeptId;
        public string Message = "";        
        DDLBAL ddl;
        AnuSearch srch;
        DataTable dt;
        SendMailBAL send;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            txtFrmDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                txtFrmDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy");
                BindUserName();
                BindGrid();
            }
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            grdWebsiteLogs.PageIndex = 0;
            BindGrid();
        }
        protected void BindUserName()
        {
            try
            {
                System.Web.UI.WebControls.ListItem ls = new System.Web.UI.WebControls.ListItem("--- All ---", "0");
                ddl = new DDLBAL();
                ddl.ClientId = ClientId;
                ddl.UserId = UserId;
                ddl.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = ddl.GetUserDeviceByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = ddl.GetUsrDeviceByDeptHead();
                }
                else
                {
                    ddlUserName.DataSource = ddl.GetUserDeviceByUserId();
                }
                ddlUserName.DataTextField = "DeviceName";
                ddlUserName.DataValueField = "DeviceId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }

        }

        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdWebsiteLogs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdWebsiteLogs.PageIndex = e.NewPageIndex;
            BindGrid();
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
            //if (hours > 0)
            result = String.Format("{0}:{1}:{2}", h, m, s);
            //else if (minutes > 0)
            //    result = String.Format("{0}m {1}s ", minutes, seconds);
            //else
            //    result = String.Format("{0}s ", seconds);

            return result;
        }


        protected void BindGrid()
        {
            DataTable dtUser = new DataTable();
            string FrmDateTime, ToDateTime;
            try
            {
                #region------ Manage From/To Date Time and Duration -----------
                try
                {
                    FrmDateTime = txtFrmDate.Text;

                    if (FrmDateTime.Trim() != "")
                    {
                        FrmDateTime = txtFrmDate.Text.Trim() + " 00:00";
                    }
                }

                catch (Exception)
                {
                    FrmDateTime = txtFrmDate.Text.Trim();
                }
                try
                {
                    ToDateTime = txtToDate.Text;

                    if (ToDateTime.Trim() != "")
                    {
                        ToDateTime = txtToDate.Text.Trim() + " 23:59";
                    }

                }


                catch (Exception)
                {
                    ToDateTime = txtToDate.Text.Trim();
                }



                #endregion
                srch = new AnuSearch();
                if (RoleId == 1 || RoleId == 2)
                {
                    dtUser = srch.SrchAppLogsDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), SrchAppName.Text.Trim(), FrmDateTime, ToDateTime);
                }
                else if (RoleId == 3)
                {
                    dtUser = srch.SrchAppLogsDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), SrchAppName.Text.Trim(), FrmDateTime, ToDateTime, DeptId);
                }
                else
                {
                    dtUser = srch.SrchAppLogsDtls(ClientId, UserId, ddlUserName.SelectedValue.ToString(), SrchAppName.Text.Trim(), FrmDateTime, ToDateTime);
                }

                grdWebsiteLogs.DataSource = dtUser;
                ViewState["dtUser"] = dtUser;
                grdWebsiteLogs.DataBind();
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
            //GrideTable();
            //pdfCreationCommand(Message);            
            DataTable dtpdf = new DataTable();
            dtpdf = (DataTable)ViewState["dtUser"];
            Document document = new Document(PageSize.A2, 10f, 10f, 10f, 10f);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                PdfPTable table = new PdfPTable(8);
                //PdfPCell cell1 = null, cell2 = null, cell3 = null, cell4 = null, cell5 = null;

                PdfPCell pdfcell = PhraseCell(new Phrase(" App Log Report ", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                pdfcell.Colspan = 8;
                pdfcell.BorderWidth = 0;
                table.AddCell(pdfcell);
                if (Convert.ToInt16(ddlUserName.SelectedValue) == 0)
                {
                    pdfcell = PhraseCell(new Phrase(" Device/User Name : All", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                else
                {
                    pdfcell = PhraseCell(new Phrase(" Device/User Name : " + ddlUserName.SelectedItem.ToString(), FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                pdfcell.Colspan = 8;
                pdfcell.BorderWidth = 0;
                table.AddCell(pdfcell);
                if (SrchAppName.Text != "")
                {
                    pdfcell = PhraseCell(new Phrase("  App Name : " + SrchAppName.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    pdfcell.Colspan = 8;
                    pdfcell.BorderWidth = 0;
                    table.AddCell(pdfcell);
                }
                if (txtFrmDate.Text != "")
                {
                    pdfcell = PhraseCell(new Phrase("  From Date : " + txtFrmDate.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    pdfcell.Colspan = 8;
                    pdfcell.BorderWidth = 0;
                    table.AddCell(pdfcell);
                }
                if (txtToDate.Text != "")
                {
                    pdfcell = PhraseCell(new Phrase("  To Date : " + txtToDate.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    pdfcell.Colspan = 8;
                    pdfcell.BorderWidth = 0;
                    table.AddCell(pdfcell);
                }



                int k = 1;
                for (int x = 0; x < grdWebsiteLogs.Columns.Count; x++)
                {
                    string cellText = Server.HtmlDecode(grdWebsiteLogs.HeaderRow.Cells[x].Text);
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
                    for (int j = 0; j < grdWebsiteLogs.Columns.Count; j++)
                    {
                        if (j > 8)
                        {
                            continue;
                        }
                        string cellText;
                        if (j == 0)
                        {
                            cellText = (k).ToString();
                            k++;
                        }
                        else if (j == 6)
                        {
                            cellText = Server.HtmlDecode(TimeCalculate(dtpdf.Rows[i][j - 1].ToString()));
                        }
                        else
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j - 1].ToString());
                        }
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        //Set Font and Font Color
                        iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
                        font.Color = new BaseColor(0, 0, 0);
                        iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        if (i % 2 == 0)
                        {
                            //Set Row BackGround Color
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
                Response.AddHeader("Content-Disposition", "attachment; filename=AppLogsDetails.pdf");
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //string gridHTML = sw.ToString().Replace("\"", "'")
            //    .Replace(System.Environment.NewLine, "");
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload = new function(){");
            //sb.Append("var printWin = window.open('', '', 'left=0");
            //sb.Append(",top=0,width=1000,height=600,status=0');");
            //sb.Append("printWin.document.write(\"");

            GrideTable();
            printCommand(Message);
            //sb.Append(Message);
            //sb.Append("\");");
            //sb.Append("printWin.document.close();");
            //sb.Append("printWin.focus();");
            //sb.Append("printWin.print();");
            //sb.Append("printWin.close();};");
            //sb.Append("</script>");
            //ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        }

        public void GrideTable()
        {
            DataTable dtpdf = new DataTable();
            dtpdf = (DataTable)ViewState["dtUser"];
            if (dtpdf.Rows.Count > 0)
            {
                string UName = "", AppName = "", FDate = "", TDate = "";
                try
                {
                    if (Convert.ToInt32(ddlUserName.SelectedValue) == 0)
                    {
                        UName = "<b> Device/User Name </b>: All";
                    }
                    else
                    {
                        UName = " <br/>Device/User Name : " + ddlUserName.SelectedItem.Text;
                    }
                    if (SrchAppName.Text != "")
                    {
                        AppName = "<br/> App Name :" + SrchAppName.Text.Trim();
                    }
                    if (txtFrmDate.Text != "")
                    {
                        FDate = "<br/> From Date : " + txtFrmDate.Text.Trim();
                    }
                    if (txtToDate.Text != "")
                    {
                        TDate = " <br/>To Date: " + txtToDate.Text.Trim();
                    }

                }
                catch (Exception)
                {

                }
                Message = ("<table width='100%' cellspacing='0' cellpadding='0' border=0 style='background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + ("<b style=' font-size: 20px;'>Application Log</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8'>");
                Message = Message + ("<br/>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UName);
                Message = Message + ("</td>");

                Message = Message + ("</tr>");
                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (AppName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (FDate);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (TDate);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("</table>");

                Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Sr.No.</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>User Name</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Device Name</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>App Name</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Start Time</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>End Time</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Duration</b>");
                Message = Message + ("</td>");
                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>LogDateTime</b>");
                Message = Message + ("</td>");

                Message = Message + ("</tr>");

                int k = 1;
                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j < grdWebsiteLogs.Columns.Count; j++)
                    {
                        
                        if (j > 8)
                        {
                            continue;
                        }
                        string cellText;
                        if (j == 0)
                        {
                            cellText = (k).ToString();
                            k++;
                        }
                        else if (j == 6)
                        {
                            cellText = Server.HtmlDecode(TimeCalculate(dtpdf.Rows[i][j - 1].ToString()));
                        }
                        else
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j - 1].ToString());
                        }
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        Message = Message + ("<td align='center' >");
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
                StringBuilder msg = new StringBuilder();
                #region---- Make user Table ----------
                msg.Append("Dear Sir");
                msg.AppendLine(); msg.AppendLine();
                msg.Append("<b>The below table has the details for App Log.</b>");
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
                send.SendEmail(EmailId, "App Log Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
                lblMsg.Text = "Mail sent successfully";
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
                    send = new SendMailBAL();
                    send.SendEmail(txtMailTo.Text, "App Log Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
                    lblMsg.Text = "Mail sent Successfully";
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
                lblerrorMailTo.Text = "Please Enter E-Mail Id";
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
    }
}
