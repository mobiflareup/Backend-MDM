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
    public partial class BranchReport1 : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        public string Message = "";
        StringBuilder msg = new StringBuilder();
        AnuSearch srch;
        SendMailBAL send;
        DataTable dtUser, dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            dtUser = new DataTable();
            srch = new AnuSearch();
            dtUser = srch.GetBranchReportDtls(ClientId, txtUser.Text.Trim());
            grdUser.DataSource = dtUser;
            ViewState["dtUser"] = dtUser;
            grdUser.DataBind();
        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            grdUser.PageIndex = 0;
            BindGrid();
        }
        protected string MyFormat(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                return Convert.ToDateTime(date).ToString("dd-MMM-yyyy HH:mm");
            }
            return date;
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

                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(5);
                PdfPCell cell1 = null, cell2 = null;

                cell1 = PhraseCell(new Phrase(" Branch Report ", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                cell1.Colspan = 8;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);

                if (txtUser.Text != "")
                {
                    cell2 = PhraseCell(new Phrase("  Branch Name : " + txtUser.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell2.Colspan = 8;
                    cell2.BorderWidth = 0;
                    table.AddCell(cell2);
                }



                for (int x = 0; x < grdUser.Columns.Count; x++)
                {
                    if (x == 0)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(grdUser.HeaderRow.Cells[x].Text);
                    //Set Font and Font Color
                    iTextSharp.text.Font font = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK); //new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
                    font.Color = new BaseColor(255, 255, 255);
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                    cell.BackgroundColor = new BaseColor(42, 54, 137);
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    table.AddCell(cell);
                }

                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    for (int j = 0; j < grdUser.Columns.Count; j++)
                    {
                        if (j > 4)
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
                        if (j == 2)
                        {
                            cellText = Convert.ToDateTime(cellText).ToString("dd-MMM-yyyy HH:mm");
                        }
                        if (j == 4 && cellText != "")
                        {
                            cellText = Convert.ToDateTime(cellText).ToString("dd-MMM-yyyy HH:mm");
                        }
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        //}
                        //Set Font and Font Color
                        iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK); //new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
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
                Response.AddHeader("Content-Disposition", "attachment; filename=BranchReport.pdf");
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
                string UserName = "";
                try
                {

                    if (txtUser.Text != "")
                    {
                        UserName = "<b> Branch Name : </b>" + txtUser.Text.Trim();
                    }


                }
                catch (Exception)
                {

                }
                Message = "<table width='100%' cellspacing='0' cellpadding='0' border=0 style=' background-color:#F1F1F1; font-size: 10px;'>";
                Message = Message + "<tr>";
                Message = Message + "<td colspan='8' align='center'>";
                Message = Message + "<b style=' font-size: 20px;'>Branch Report</b>";
                Message = Message + "</td>";
                Message = Message + "</tr>";
                Message = Message + "<tr>";
                Message = Message + "<td colspan='8' align='center'>";
                Message = Message + UserName;
                Message = Message + "</td>";
                Message = Message + "</tr>";
                Message = Message + "</table>";

                Message = Message + "<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>";

                Message = Message + "<tr style='background-color:#2A368B; color:White; font-size: 12px;'>";

                Message = Message + "<td align='center'>";
                Message = Message + "<b>Branch Name";
                Message = Message + "</td>";

                Message = Message + "<td align='center'>";
                Message = Message + "<b>Created By";
                Message = Message + "</td>";

                Message = Message + "<td align='center'>";
                Message = Message + "<b>Creation Date";
                Message = Message + "</td>";

                Message = Message + "<td align='center'>";
                Message = Message + "<b>Updated By";
                Message = Message + "</td>";

                Message = Message + "<td align='center'>";
                Message = Message + "<b>Updation Date";
                Message = Message + "</td>";


                Message = Message + "</tr>";

                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + "<tr>";
                    for (int j = 0; j < grdUser.Columns.Count; j++)
                    {
                        
                        if (j > 4)
                        {
                            continue;
                        }
                        Message = Message + "<td align='center' >";
                        string cellText;
                        //if (j == 0)
                        //{
                        //    cellText = (k).ToString();
                        //    k++;
                        //}
                        //else
                        //{
                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (j == 2)
                        {
                            cellText = Convert.ToDateTime(cellText).ToString("dd-MMM-yyyy HH:mm");
                        }
                        if (j == 4 && cellText != "")
                        {
                            cellText = Convert.ToDateTime(cellText).ToString("dd-MMM-yyyy HH:mm");
                        }
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        Message = Message + cellText;
                        Message = Message + "</td>";

                    }
                    Message = Message + "</tr>";
                }

                Message = Message + "</table>";
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
                #region---- Make Branch Table ----------
                msg.Append("Dear Sir/Madam");
                msg.AppendLine(); msg.AppendLine();
                msg.Append("<b>The below table has the details for Branch.</b>");
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
                send.SendEmail(EmailId, "Branch Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
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
                    send.SendEmail(txtMailTo.Text, "Branch Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
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
    }
}
