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
    public partial class TAVisitDtl : Base
    {
        GingerboxSrch gsrch;
        int ClientId, UserId, RoleId, DeptId, MasterID;
        DataTable dt;
        SendMailBAL send;
        StringBuilder msg = new StringBuilder();
        string Message = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            MasterID = Convert.ToInt32((Request.QueryString["Id"]));
            try
            {
                MasterID = Convert.ToInt32((Request.QueryString["Id"]));
            }
            catch (Exception)
            {
                MasterID = 0;
            }
            if (!IsPostBack)
            {
                if (MasterID == 0)
                    Response.Redirect("TAMaster.aspx");
                else
                    BindGrid();
            }

        }

        private void BindGrid()
        {
            gsrch = new GingerboxSrch();
            dt = new DataTable();
            //dt = (DataTable)Session["dtTA"];
            dt = gsrch.GetVistDetailsbyMasterId(MasterID);
            ViewState["dtUser"] = dt;
            if (dt.Rows.Count > 0)
            {
                tavisit.DataSource = dt;
                tavisit.DataBind();
                lblUserName.Text = dt.Rows[0]["UserName"].ToString();
                lblLogDt.Text = dt.Rows[0]["Logdate"].ToString();
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
                Response.ContentType = "image";// ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                //Response.WriteFile("~/Files/Android_Files/" + filePath);
                Response.TransmitFile(Server.MapPath("~/Files/Android_Files/" + filePath));
                Response.End();
            }
            catch (Exception)
            { }
        }

        protected void tavisit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tavisit.PageIndex = e.NewPageIndex;
            tavisit.EditIndex = -1;
            BindGrid();
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
            int columnCount = 9;
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(columnCount);
                PdfPCell cell1 = null, cell2 = null, cell3 = null;


                cell1 = PhraseCell(new Phrase("Visit Details", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);

                cell1.Colspan = columnCount;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);

                if (lblUserName.Text != "")
                {
                    cell2 = PhraseCell(new Phrase("  User Name : " + lblUserName.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell2.Colspan = columnCount;
                    cell2.BorderWidth = 0;
                    table.AddCell(cell2);
                }
                if (lblLogDt.Text != "")
                {
                    cell3 = PhraseCell(new Phrase("  Log Date : " + lblLogDt.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell3.Colspan = columnCount;
                    cell3.BorderWidth = 0;
                    table.AddCell(cell3);
                }


                for (int x = 0; x < tavisit.Columns.Count; x++)
                {
                    if (x == 0 ||x==6|| x > 10)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(tavisit.HeaderRow.Cells[x].Text);
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
                    for (int j = 0; j < tavisit.Columns.Count; j++)
                    {
                        if (j == 1  || j==6||j > 10)
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
                        //}
                        if (j == 5)
                        {
                            if (dtpdf.Rows[i][j].ToString() == "1")
                            {
                                cellText = "Yes";
                            }
                            else
                            {
                                cellText = "No";
                            }
                        }
                        //if (j == 6)
                        //{
                        //    if (string.IsNullOrEmpty(dtpdf.Rows[i][j].ToString()))
                        //    {
                        //        cellText = "None";
                        //    }
                        //    else
                        //    {
                        //        cellText = "Download";
                        //    }
                        //}
                        if (cellText == "")
                        {
                            cellText = "--";
                        }
                        //Set Font and Font Color
                        iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK); // new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
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
                Response.AddHeader("Content-Disposition", "attachment; filename=TAVisit.pdf");
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
                string UName = "", LDate = "";
                try
                {



                    if (lblUserName.Text != "")
                    {
                        UName = "<b> User Name : </b>" + lblUserName.Text.Trim();
                    }
                    if (lblLogDt.Text != "")
                    {
                        LDate = "<b> Log Date : </b> " + lblLogDt.Text.Trim();
                    }

                }
                catch (Exception)
                {

                }

                Message = ("<table width='100%' cellspacing='0' cellpadding='0' border=0 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9' align='center'>");
                Message = Message + ("<b style=' font-size: 20px;'>Visit Details</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9' align='center'>");
                Message = Message + (UName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");


                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='9' align='center'>");
                Message = Message + (LDate);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("</table>");

                Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Customer Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Auto Customer Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>From Date</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Visit Date</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>To Date</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Visited</b>");
                Message = Message + ("</td>");

                //Message = Message + ("<td align='center'>");
                //Message = Message + ("<b>Proof</b>");
                //Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Remark</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Total Distance</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Claimed Travel Amt</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Mode of Travel</b>");
                Message = Message + ("</td>");

                Message = Message + ("</tr>");


                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j < 11; j++)
                        {
                        if (j == 1 || j == 6)
                        {
                            continue;
                        }

                        Message = Message + ("<td align='center'>");
                        //if (j > 5)
                        //{
                        //    continue;
                        //}
                        string cellText;
                        //if (j == 0)
                        //{
                        //    cellText = (k).ToString();
                        //    k++;
                        //}
                        //else
                        //{
                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        //}
                        if (j == 5)
                        {
                            if (dtpdf.Rows[i][j].ToString() == "1")
                            {
                                cellText = "Yes";
                            }
                            else
                            {
                                cellText = "No";
                            }
                        }
                        if (j == 6)
                        {
                            if (string.IsNullOrEmpty(dtpdf.Rows[i][j].ToString()))
                            {
                                cellText = "None";
                            }
                            else
                            {
                                cellText = "Download";
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
                #region---- Make Sos Table ----------
                StringBuilder msg = new StringBuilder();

                msg.Append("Dear Sir/Madam");
                msg.AppendLine(); msg.AppendLine();
                msg.Append("<b>The below table has the details for Visits.</b>");
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

        private void MailsendYou(string Msgbody)
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
                send.SendEmail(EmailId, "Visit Report at " + GetCurrentDateTimeByUserId(), Msgbody, ClientId);
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
        private void MailsendOther(string Msgbody)
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
                    send.SendEmail(txtMailTo.Text, "Visit Report at " + GetCurrentDateTimeByUserId(), Msgbody, ClientId);
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
        protected void btnViewLocation_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            gsrch = new GingerboxSrch();
            int visitid = 0;
            Button lnkPopUp = sender as Button;
            GridViewRow gvr = lnkPopUp.NamingContainer as GridViewRow;
            Label lbl = ((Label)tavisit.Rows[gvr.RowIndex].FindControl("lblId"));
            visitid = Convert.ToInt32(lbl.Text);
            dt = gsrch.GetLocationDtlbyVisitId(visitid);
            if (dt.Rows.Count > 0)
            {
                string url = "TALocation.aspx?Id=" + visitid + "&MId=" + MasterID;
                Response.Redirect(url);
            }
            else
            {
                lblMsg.Text = "No record found!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
