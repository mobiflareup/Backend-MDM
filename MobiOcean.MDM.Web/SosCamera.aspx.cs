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
    public partial class SosCamera : Base
    {
        GingerboxSrch gsrch;

        DataTable dt;
        SendMailBAL send;
        StringBuilder msg = new StringBuilder();
        int ClientId, UserId, RoleId, DeptId;
        public string Message = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());

            txtFrmDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                txtFrmDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy");
                BindGrid();
            }
        }

        private void BindGrid()
        {

            gsrch = new GingerboxSrch();
            dt = new DataTable();
            string FrmDateTime = "", ToDateTime = "";
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
                dt = gsrch.GetSosCamera(ClientId, FrmDateTime, ToDateTime, txtUsername.Text, txtcontact.Text);
                sosCam.DataSource = dt;
                sosCam.DataBind();
                ViewState["dtUser"] = dt;
            }
            catch (Exception)
            {
            }
            finally
            {
                gsrch = null;
            }
        }
        protected void sosCam_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            sosCam.PageIndex = e.NewPageIndex;
            sosCam.EditIndex = -1;
            BindGrid();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {

            /* Verifies that the control is rendered */

        }
        protected void btnsavetopdf_Click(object sender, EventArgs e)
        {

            // grdUser.AllowPaging = false;
            int cellcount = 6;
            DataTable dtpdf = new DataTable();
            dtpdf = (DataTable)ViewState["dtUser"];
            Document document = new Document(PageSize.A2, 10f, 10f, 10f, 10f);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(cellcount);
                PdfPCell cell1 = null, cell2 = null, cell3 = null, cell4 = null, cell5 = null;


                cell1 = PhraseCell(new Phrase("SOS Camera Report", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);

                cell1.Colspan = cellcount;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);
                if (txtUsername.Text != "")
                {
                    cell2 = PhraseCell(new Phrase("  Person Name : " + txtUsername.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell2.Colspan = cellcount;
                    cell2.BorderWidth = 0;
                    table.AddCell(cell2);
                }
                if (txtUsername.Text != "")
                {
                    cell5 = PhraseCell(new Phrase("  Contact No : " + txtcontact.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell5.Colspan = cellcount;
                    cell5.BorderWidth = 0;
                    table.AddCell(cell5);
                }
                if (txtFrmDate.Text != "")
                {
                    cell3 = PhraseCell(new Phrase("  From Date : " + txtFrmDate.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell3.Colspan = cellcount;
                    cell3.BorderWidth = 0;
                    table.AddCell(cell3);
                }
                if (txtToDate.Text != "")
                {
                    cell4 = PhraseCell(new Phrase("  To Date : " + txtToDate.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell4.Colspan = 10;
                    cell4.BorderWidth = 0;
                    table.AddCell(cell4);
                }



                for (int x = 0; x < sosCam.Columns.Count; x++)
                {
                    if (x == 0 || x == 1)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(sosCam.HeaderRow.Cells[x].Text);
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
                    for (int j = 0; j < sosCam.Columns.Count; j++)
                    {
                        if (j >5)
                        {
                            continue;
                        }
                        string cellText;

                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
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
                Response.AddHeader("Content-Disposition", "attachment; filename=SosCamera.pdf");
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
                string UName = "", ContactNo = "", FDate = "", TDate = "";
                try
                {



                    if (txtUsername.Text != "")
                    {
                        UName = "<b> User Name : </b>" + txtUsername.Text.Trim();
                    }
                    if (txtcontact.Text != "")
                    {
                        ContactNo = "<b> Contact No : </b>" + txtcontact.Text.Trim();
                    }
                    if (txtFrmDate.Text != "")
                    {
                        FDate = "<b> From Date : </b> " + txtFrmDate.Text.Trim();
                    }
                    if (txtToDate.Text != "")
                    {
                        TDate = " <b>To Date : </b> " + txtToDate.Text.Trim();
                    }
                }
                catch (Exception)
                {

                }

                Message = ("<table width='100%' cellspacing='0' cellpadding='0' border=0 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + ("<b style=' font-size: 20px;'>Sos Camera Report</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (ContactNo);
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

                Message = Message + ("</table>");

                Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Person Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Contact No</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Latitude</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Longitude</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Location</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Log Date</b>");
                Message = Message + ("</td>");

                Message = Message + ("</tr>");


                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j < 6; j++)
                    {
                        Message = Message + ("<td align='center'>");

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

                msg.Append("Dear Sir");
                msg.AppendLine(); msg.AppendLine();
                msg.Append("<b>Please find below The Sos Camera Report</b>");
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
                lblerrorMailTo.Text = "Please select any of the One";
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
                send.SendEmail(EmailId, "Sos Camera Report at " + GetCurrentDateTimeByUserId(), Msgbody, ClientId);
                lblMsg.Text = "Mail Send Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception)
            {
                lblMsg.Text = "Mail Not Send Successfully";
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
                    send.SendEmail(txtMailTo.Text, "Sos Camera Report at " + GetCurrentDateTimeByUserId(), Msgbody, ClientId);
                    lblMsg.Text = "Mail Send Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception)
                {
                    lblMsg.Text = "Mail Not Send Successfully";
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