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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AntiTheftFile : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        public string Message = "";
        DDLBAL ddlbal;
        AnuSearch srch;
        SendMailBAL send;
        UserBAL user;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindUsrName();
                txtFrmdate.Text = txtTodate.Text = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy");
                BindGrid();
            }
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
                ddlUserName.Items.Add(new System.Web.UI.WebControls.ListItem("All", "0"));
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

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            grdFileDtl.PageIndex = 0;
            BindGrid();
        }
        protected void lnkbtnDownload_Click(object sender, EventArgs e)
        {
            string ContentType = "";
            try
            {
                LinkButton lnkBtn = sender as LinkButton;
                GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
                Label lblFilePath = (Label)row.FindControl("lblFilePath");
                Label lblFileType = (Label)row.FindControl("lblFileType");

                string filePath = "/Files/Android_Files" + lblFilePath.Text;
                ContentType = lblFileType.Text.ToString();
                if (ContentType == "Video")
                {
                    Response.ContentType = "video/mp4";
                }
                else
                {
                    Response.ContentType = "audio/mp4";
                }
                filePath = "~/" + filePath.Substring(1);
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
                string s = Server.MapPath(filePath);
                Response.TransmitFile(s);
                Response.End();
            }
            catch (Exception) { }
        }
        protected void grdFileDtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFileDtl.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void BindGrid()
        {
            DataTable dtUser = new DataTable();
            srch = new AnuSearch();
            if (RoleId == 1 || RoleId == 2)
            {
                dtUser = srch.SrchFileDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), txtFrmdate.Text.Trim(), txtTodate.Text.Trim(), 0);
            }
            else if (RoleId == 3)
            {
                dtUser = srch.SrchFileDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), txtFrmdate.Text.Trim(), txtTodate.Text.Trim(), 0, DeptId);
            }
            else
            {
                dtUser = srch.SrchFileDtls(ClientId, UserId, ddlUserName.SelectedValue.ToString(), txtFrmdate.Text.Trim(), txtTodate.Text.Trim(), 0);
            }

            grdFileDtl.DataSource = dtUser;
            ViewState["dtUser"] = dtUser;
            grdFileDtl.DataBind();
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

                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(6);
                PdfPCell cell1 = null, cell2 = null, cell3 = null, cell4 = null;

                cell2 = PhraseCell(new Phrase(" Anti Theft File Report ", FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                cell2.Colspan = 8;
                cell2.BorderWidth = 0;
                table.AddCell(cell2);
                if (Convert.ToInt16(ddlUserName.SelectedValue) == 0)
                {
                    cell1 = PhraseCell(new Phrase(" Device/User Name : All", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                else
                {
                    cell1 = PhraseCell(new Phrase(" Device/User Name : " + ddlUserName.SelectedItem.ToString(), FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                cell1.Colspan = 8;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);
                if (txtFrmdate.Text != "")
                {
                    cell3 = PhraseCell(new Phrase("  From Date : " + txtFrmdate.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell3.Colspan = 8;
                    cell3.BorderWidth = 0;
                    table.AddCell(cell3);
                }
                if (txtTodate.Text != "")
                {
                    cell4 = PhraseCell(new Phrase("  To Date : " + txtTodate.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell4.Colspan = 8;
                    cell4.BorderWidth = 0;
                    table.AddCell(cell4);
                }




                for (int x = 0; x < grdFileDtl.Columns.Count; x++)
                {
                    if (x == 4 || x == 8)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(grdFileDtl.HeaderRow.Cells[x].Text);
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
                    for (int j = 0; j < grdFileDtl.Columns.Count; j++)
                    {
                        if (j > 5)
                        {
                            continue;
                        }
                        string cellText;

                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        if (cellText == "1")
                        {
                            cellText = "Audio";
                        }
                        if (cellText == "0")
                        {
                            cellText = "Video";
                        }
                        //Set Font and Font Color
                        iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);//new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
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
                Response.AddHeader("Content-Disposition", "attachment; filename=AntiTheftFileReport.pdf");
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

                string UName = "", FDate = "", TDate = "";
                try
                {
                    if (Convert.ToInt32(ddlUserName.SelectedValue) == 0)
                    {
                        UName = "<b> Device/User Name </b>: All";
                    }
                    else
                    {
                        UName = "<b> User Name :</b> " + ddlUserName.SelectedItem.Text;
                    }
                    if (txtFrmdate.Text != "")
                    {
                        FDate = "<b> From Date : </b>" + txtFrmdate.Text.Trim();
                    }
                    if (txtTodate.Text != "")
                    {
                        TDate = "<b> To Date : </b>" + txtTodate.Text.Trim();
                    }


                }
                catch (Exception)
                {

                }
                Message = ("<table width='100%' cellspacing='0' cellpadding='0' border=0 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='6'align='center'>");
                Message = Message + ("<b style=' font-size: 20px;'>AntiTheft File Report</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");
                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='6' align='center'>");
                Message = Message + (UName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");
                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='6' align='center'>");
                Message = Message + (FDate);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='6' align='center'>");
                Message = Message + (TDate);
                Message = Message + ("</td>");
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
                Message = Message + ("<b>File Name");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>LogDateTime");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Location");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>File Type");
                Message = Message + ("</td>");

                Message = Message + ("</tr>");

                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j < 6; j++)
                    {
                        Message = Message + ("<td align='center'>");
                        if (j > 5)
                        {
                            continue;
                        }
                        string cellText;

                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        if (cellText == "1")
                        {
                            cellText = "Audio";
                        }
                        if (cellText == "0")
                        {
                            cellText = "Video";
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


            #region---- Make user Table ----------
            StringBuilder msg = new StringBuilder();

            msg.Append("Dear Sir/Madam");
            msg.AppendLine(); msg.AppendLine();
            msg.Append("<b>The below table has the details for antiTheft file.</b>");
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

            try
            {
                string EmailId = "";
                user = new UserBAL();
                user.UserId = UserId;
                dt = new DataTable();
                dt = user.GetUserDtlByUserId();
                if (dt.Rows.Count > 0)
                {
                    EmailId = dt.Rows[0]["EmailId"].ToString();
                }
                send = new SendMailBAL();
                send.SendEmail(EmailId, Constant.developerEmail, msg.ToString(), ClientId);
                string message = "Mail sent successfully";
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
            }
            catch (Exception)
            {
                string message = "Could not send the mail.";
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
            }
        }
    }
}