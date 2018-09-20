using iTextSharp.text;
using iTextSharp.text.pdf;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using MobiOcean.MDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class SosReport : Base
    {
        StringBuilder msg = new StringBuilder();
        protected int ClientId, UserId, RoleId, DeptId;
        public string Message = "";
        UserDeviceBAL usrdevice;
        DataTable dt;
        SendSMSBAL sms;
        DDLBAL dropbal;
        AnuSearch srch;
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
                BindUser();
                txtFrmDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString(Constant.DateFormat);
                BindGrid();
            }
        }
        protected void BindUser()
        {
            try
            {
                System.Web.UI.WebControls.ListItem ls = new System.Web.UI.WebControls.ListItem("--- Select User ---", "0");
                dropbal = new DDLBAL();
                dropbal.ClientId = ClientId;
                dropbal.UserId = UserId;
                dropbal.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = dropbal.GetUserByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = dropbal.GetUserWithoutDeptHead();
                }
                else
                {
                    ddlUserName.DataSource = dropbal.GetUserByUserId();
                }
                ddlUserName.DataTextField = "UserName";
                ddlUserName.DataValueField = "UserId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }

        }
        protected void SendUpdateMsg()
        {
            usrdevice = new UserDeviceBAL();
            dt = new DataTable();
            usrdevice.ClientId = ClientId;
            dt = usrdevice.GetDeviceWithMDM();
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

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void lnkPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkPopUp = sender as LinkButton;
                GridViewRow gvr = lnkPopUp.NamingContainer as GridViewRow;
                Label lbl = ((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblId"));
                //lbllblProfileId.Text = lbl.Text;
                //  sbal = new SOSBAL();
                //  sbal.SosId = Convert.ToInt32(lbl.Text);
                //  if (RoleId == 1 || RoleId == 2)
                //  {
                //      Gdv.DataSource = sbal.GetSosSentDetail();
                // }
                // else
                // {
                //     Gdv.DataSource = sbal.GetSosSentDetail();
                // }
                // Gdv.DataBind();
                //mp.Show();
                // Popup(true);

                Label lblUserId = ((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblUserId"));
                Label lblDeviceId = ((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblDeviceId"));
                Session["CurrentUserId"] = lblUserId.Text;
                Session["CurrentDeviceId"] = lblDeviceId.Text;
                Response.Redirect("FindNearestEmployee.aspx");
            }
            catch (Exception)
            {

            }
        }

        void Popup(bool isDisplay)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#myModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);


        }
        protected void grdclient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdclient.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void BindGrid()
        {
            DataTable dtUser = new DataTable();
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
                srch = new AnuSearch();
                if (RoleId == 1 || RoleId == 2)
                {
                    dtUser = srch.SrchUserSoSLocationDtls(ClientId, ddlUserName.SelectedValue.ToString(), txtDeviceName.Text.Trim(), FrmDateTime, ToDateTime, 0);
                }
                else if (RoleId == 3)
                {
                    dtUser = srch.SrchUserSoSLocationDtls(ClientId, ddlUserName.SelectedValue.ToString(), txtDeviceName.Text.Trim(), FrmDateTime, ToDateTime, 0, DeptId);
                }
                else
                {
                    dtUser = srch.SrchUserSoSLocationDtls(ClientId, ddlUserName.SelectedValue.ToString(), txtDeviceName.Text.Trim(), FrmDateTime, ToDateTime, UserId);
                }
                grdclient.DataSource = dtUser;
                ViewState["dtUser"] = dtUser;
                grdclient.DataBind();
            }
            catch (Exception)
            {
            }
            finally
            {
                srch = null;
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

                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(6);
                PdfPCell cell1 = null, cell2 = null, cell3 = null, cell4 = null;


                if (Convert.ToInt16(ddlUserName.SelectedValue) == 0)
                {
                    cell1 = PhraseCell(new Phrase("SOS Report", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                else
                {
                    cell1 = PhraseCell(new Phrase(ddlUserName.SelectedItem.ToString() + " SoS Report", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                cell1.Colspan = 8;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);
                if (txtDeviceName.Text != "")
                {
                    cell2 = PhraseCell(new Phrase("  Device : " + txtDeviceName.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell2.Colspan = 8;
                    cell2.BorderWidth = 0;
                    table.AddCell(cell2);
                }
                if (txtFrmDate.Text != "")
                {
                    cell3 = PhraseCell(new Phrase("  From Date : " + txtFrmDate.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell3.Colspan = 8;
                    cell3.BorderWidth = 0;
                    table.AddCell(cell3);
                }
                if (txtToDate.Text != "")
                {
                    cell4 = PhraseCell(new Phrase("  To Date : " + txtToDate.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell4.Colspan = 8;
                    cell4.BorderWidth = 0;
                    table.AddCell(cell4);
                }



                int k = 1;
                for (int x = 0; x < grdclient.Columns.Count; x++)
                {
                    if (x == 4 || x == 0 || x == 5 || x == 6 || x == 10)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(grdclient.HeaderRow.Cells[x].Text);
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
                    for (int j = 0; j < grdclient.Columns.Count; j++)
                    {
                        if (j > 5)
                        {
                            continue;
                        }
                        string cellText;
                        if (j == 0)
                        {
                            cellText = (k).ToString();
                            k++;
                        }
                        else
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j - 1].ToString());
                        }
                        if (j == 3)
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j - 1].ToString() == "Location not found" ? dtpdf.Rows[i]["Latitude"].ToString() + "," + dtpdf.Rows[i]["Longitude"].ToString() : dtpdf.Rows[i][j - 1].ToString());
                        }
                        if (cellText == "")
                        {
                            cellText = "---";
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
                Response.AddHeader("Content-Disposition", "attachment; filename=SosReport.pdf");
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
                string UName = "", DeviceName = "", FDate = "", TDate = "";
                try
                {
                    if (Convert.ToInt32(ddlUserName.SelectedValue) == 0)
                    {
                        UName = "<b> User </b>: All";
                    }
                    else
                    {
                        UName = " <b>User Name : </b>" + ddlUserName.SelectedItem.Text;
                    }
                    if (txtDeviceName.Text != "")
                    {
                        DeviceName = "<b> Device : </b>" + txtDeviceName.Text.Trim();
                    }
                    if (txtFrmDate.Text != "")
                    {
                        FDate = "<b> From Date :</b> " + txtFrmDate.Text.Trim();
                    }
                    if (txtToDate.Text != "")
                    {
                        TDate = " <b>To Date:</b> " + txtToDate.Text.Trim();
                    }

                }
                catch (Exception)
                {

                }

                Message = ("<table width='100%' cellspacing='0' cellpadding='0' border=0 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + ("<b style=' font-size: 20px;'>Sos Report</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");
                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");
                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (DeviceName);
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
                Message = Message + ("<b>Sr.No.");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>User Name");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Device Name");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Location");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Date");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Time");
                Message = Message + ("</td>");

                Message = Message + ("</tr>");

                int k = 1;
                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j < 6; j++)
                    {
                        Message = Message + ("<td align='center'>");
                        if (j > 7)
                        {
                            continue;
                        }
                        string cellText;
                        if (j == 0)
                        {
                            cellText = (k).ToString();
                            k++;
                        }
                        else
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j - 1].ToString());
                        }
                        if (j == 3)
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j - 1].ToString() == "Location not found" ? dtpdf.Rows[i]["Latitude"].ToString() + "," + dtpdf.Rows[i]["Longitude"].ToString() : dtpdf.Rows[i][j - 1].ToString());
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
                msg.Append("<b>The below table has the details for SOS.</b>");
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
                send.SendEmail(EmailId, "SOS Report at " + GetCurrentDateTimeByUserId(), Msgbody, ClientId);
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
                    send = new SendMailBAL();
                    send.SendEmail(txtMailTo.Text, "SOS Report at " + GetCurrentDateTimeByUserId(), Msgbody, ClientId);
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
        protected void lnkCamera_Click(object sender, EventArgs e)
        {
            LinkButton lnkCamera = sender as LinkButton;
            GridViewRow gvr = lnkCamera.NamingContainer as GridViewRow;
            var data = new SOSBAL
            {
                UserId = Convert.ToInt32(((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblUserId")).Text.Trim()),
                personName = ((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblUserName")).Text.Trim(),
                contactNo = ((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblMobile")).Text.Trim(),
                Latitude = Convert.ToDouble(((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblLat")).Text.Trim()),
                Longitude = Convert.ToDouble(((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblLongi")).Text.Trim()),
                location = ((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblLocation")).Text.Trim(),
                logdatetime = ((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblLogDate")).Text.Trim() + " " + ((Label)grdclient.Rows[gvr.RowIndex].FindControl("lblLogTime")).Text.Trim(),

            };
            JavaScriptSerializer javascriptSerializer = new JavaScriptSerializer();
            string postData = javascriptSerializer.Serialize(data);
            RestClient client = new RestClient(Constant.URL + "api/Sos/InsertSosCameraDetails", HttpVerb.POST, postData);
            client.MakeRequest();
        }
    }
}
