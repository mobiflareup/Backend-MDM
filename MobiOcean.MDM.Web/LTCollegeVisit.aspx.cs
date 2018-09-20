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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class LTCollegeVisit : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        string lblaccordingToCustomer;
        DDLBAL ddlbal;
        CustomerBAL cust;
        AnuSearch srch;
        StringBuilder msg = new StringBuilder();
        public string Message = "";
        DataTable dt;
        SendMailBAL send;
        LTCollegeVisitBAL lt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            txtFrmDt.Attributes.Add("readonly", "readonly");
            txtToDt.Attributes.Add("readonly", "readonly");
            lblaccordingToCustomer = ClientId == 399 ? "College" : "Customer";


            if (!IsPostBack)
            {
                lblMsg.Text = string.Empty;
                BindUser();
                BindCustomer();
                txtFrmDt.Text = txtToDt.Text = GetCurrentDateTimeByUserId().ToString(Constant.DateFormat);
                BindGrid();
                UniqueUserCount();
            }
        }
        protected bool CheckLatLong(string Lat, string Lng)
        {
            if (!string.IsNullOrEmpty(Lat) && !string.IsNullOrEmpty(Lng))
            {
                try
                {
                    double lat = Convert.ToDouble(Lat);
                    double lng = Convert.ToDouble(Lng);
                    if (lat > 0 || lng > 0)
                    {
                        return false;
                    }
                    return true;
                }
                catch
                {
                    return true;
                }
            }
            return true;
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
        protected void BindCustomer()
        {
            try
            {
                System.Web.UI.WebControls.ListItem ls = new System.Web.UI.WebControls.ListItem("--- All ---", "0");
                cust = new CustomerBAL();
                cust.ClientId = ClientId;
                ddlCollegeName.Items.Clear();
                ddlCollegeName.Items.Add(ls);
                ddlCollegeName.DataSource = cust.CustomerDetails();
                ddlCollegeName.DataTextField = "Name";
                ddlCollegeName.DataValueField = "CustomerId";
                ddlCollegeName.DataBind();
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
                grdUser.DataSource = srch.GetLTCollegeVisitDetails(ClientId, 0, ddlEmployee.SelectedValue.ToString(), "", ddlCollegeName.SelectedValue.ToString(), txtFrmDt.Text.Trim(), txtToDt.Text.Trim(), txtEmpId.Text.Trim());
            }
            else if (RoleId == 3)
            {
                grdUser.DataSource = srch.GetLTCollegeVisitDetails(ClientId, 0, ddlEmployee.SelectedValue.ToString(), "", ddlCollegeName.SelectedValue.ToString(), txtFrmDt.Text.Trim(), txtToDt.Text.Trim(), txtEmpId.Text.Trim(), DeptId);
            }
            else
            {
                grdUser.DataSource = srch.GetLTCollegeVisitDetails(ClientId, UserId, ddlEmployee.SelectedValue.ToString(), "", ddlCollegeName.SelectedValue.ToString(), txtFrmDt.Text.Trim(), txtToDt.Text.Trim(), txtEmpId.Text.Trim());
            }
            ViewState["dtUser"] = grdUser.DataSource;
            grdUser.DataBind();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            UniqueUserCount();
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

            Document document = new Document(PageSize.A2, 10f, 10f, 10f, 10f);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                int cellrows = (ClientId == 399) ? 11 : 9;
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(cellrows);

                PdfPCell cell0 = null, cell2 = null;

                ///Files_Android/KSWD/AAP-2/Images/Office/1446618880175.jpg
                if (ClientId == 399)
                {
                    string imageURL = Server.MapPath(".") + "/images/Untitled-2.png";// "/Image/Close.jpg";
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
                    //Resize image depend upon your need
                    jpg.ScaleToFit(940f, 140f);
                    //Give space before image
                    jpg.SpacingBefore = 5f;
                    //Give some space after the image
                    jpg.SpacingAfter = 1f;
                    jpg.Alignment = Element.ALIGN_CENTER;
                    PdfPCell imageCell = new PdfPCell(jpg);
                    imageCell.Border = 0;
                    imageCell.Colspan = cellrows;
                    imageCell.BorderWidth = 0;
                    table.AddCell(imageCell);
                }
                else
                {
                    cell0 = PhraseCell(new Phrase(lblaccordingToCustomer + " Visit Report", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell0.Colspan = cellrows;
                    cell0.BorderWidth = 0;
                    table.AddCell(cell0);
                }
                string serachdata = "";
                if (Convert.ToInt16(ddlEmployee.SelectedValue) == 0)
                {
                    serachdata = "  User : All  ";
                    //cell1 = PhraseCell(new Phrase(" User : All ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                else
                {
                    serachdata = "   User : " + ddlEmployee.SelectedItem.ToString() + "   ";
                    //cell1 = PhraseCell(new Phrase(" User : " + ddlEmployee.SelectedItem.ToString(), FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                }
                //cell1.Colspan = cellrows;
                //cell1.BorderWidth = 0;
                //table.AddCell(cell1);

                if (txtEmpId.Text != "")
                {
                    serachdata += "    Employee Id : " + txtEmpId.Text + "   ";
                    //cell2 = PhraseCell(new Phrase(" Employee Id : " + txtEmpId.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    //cell2.Colspan = cellrows;
                    //cell2.BorderWidth = 0;
                    //table.AddCell(cell2);
                }
                if (ddlCollegeName.SelectedValue != "0")
                {
                    serachdata += "        " + lblaccordingToCustomer + " Name : " + ddlCollegeName.SelectedItem.Text + "   ";
                    //cell3 = PhraseCell(new Phrase(lblaccordingToCustomer+" Name : " + ddlCollegeName.SelectedItem.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    //cell3.Colspan = 11;
                    // cell3.BorderWidth = 0;
                    //table.AddCell(cell3);
                }
                if (txtFrmDt.Text != "")
                {
                    serachdata += "    From Date : " + txtFrmDt.Text + "   ";
                    //cell4 = PhraseCell(new Phrase("  From Date : " + txtFrmDt.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    //cell4.Colspan = cellrows;
                    //cell4.BorderWidth = 0;
                    //table.AddCell(cell4);
                }
                if (txtToDt.Text != "")
                {
                    serachdata += "    To Date : " + txtToDt.Text + "   ";
                    //cell5 = PhraseCell(new Phrase("  To Date : " + txtToDt.Text, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    //cell5.Colspan = cellrows;
                    //cell5.BorderWidth = 0;
                    //table.AddCell(cell5);
                }

                //cell7.Colspan = 8;
                //cell7.BorderWidth = 0;
                //table.AddCell(cell7);
                cell2 = PhraseCell(new Phrase(serachdata, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                cell2.Colspan = cellrows;
                cell2.BorderWidth = 0;
                table.AddCell(cell2);

                for (int x = 0; x < grdUser.Columns.Count; x++)
                {
                    if (x == 0 || x == 1 || x == 2 || x == 10)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(grdUser.HeaderRow.Cells[x].Text);

                    //Set Font and Font Color
                    iTextSharp.text.Font font = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
                    font.Color = new BaseColor(255, 255, 255);
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                    cell.BackgroundColor = new BaseColor(42, 54, 137);
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    table.AddCell(cell);
                }
                if (ClientId == 399)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        string cellText = i == 0 ? "Remark Available?" : "Image Available?";
                        //Set Font and Font Color
                        iTextSharp.text.Font font = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
                        font.Color = new BaseColor(255, 255, 255);
                        iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                        cell.BackgroundColor = new BaseColor(42, 54, 137);
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                        table.AddCell(cell);
                    }
                }
                DataTable dtpdf = new DataTable();
                dtpdf = (DataTable)ViewState["dtUser"];
                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    for (int j = 0; j <= grdUser.Columns.Count; j++)
                    {
                        string cellText;
                        if (ClientId == 399 ? j > 10 : j > 8)
                        {
                            continue;
                        }
                        if (ClientId == 399 && (j == 9 || j == 10))
                        {
                            srch = new AnuSearch();
                            dt = new DataTable();
                            dt = srch.CheckRemarkANdImageByVisitId(dtpdf.Rows[i]["LtCollegeVisitId"].ToString());
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                cellText = (j == 9 ? !string.IsNullOrWhiteSpace(dt.Rows[0]["Remark"].ToString()) ? "Yes" : "No" : !string.IsNullOrWhiteSpace(dt.Rows[0]["ImagePath"].ToString()) ? "Yes" : "No");
                            }
                            else
                            {
                                cellText = "No";
                            }
                        }
                        else if (j == 5)
                        {
                            if (dtpdf.Rows[i]["InVerification"].ToString() != "1")
                            {
                                cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "Location not found" ? dtpdf.Rows[i]["InLatitude"].ToString() + "," + dtpdf.Rows[i]["InLongitude"].ToString() : Convert.ToBoolean(dtpdf.Rows[i]["IsInLocationManuallyEntered"].ToString()) ? dtpdf.Rows[i][j].ToString() + " (Manually Entered)" : dtpdf.Rows[i][j].ToString());
                            }
                            else
                            {
                                cellText = dtpdf.Rows[i]["Name"].ToString();
                            }
                        }
                        else if (j == 6)
                        {
                            if (dtpdf.Rows[i]["OutVerification"].ToString() != "1")
                            {
                                cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "Location not found" ? dtpdf.Rows[i]["OutLatitude"].ToString() + "," + dtpdf.Rows[i]["OutLongitude"].ToString() : Convert.ToBoolean(dtpdf.Rows[i]["IsOutLocationManuallyEntered"].ToString()) ? dtpdf.Rows[i][j].ToString() + " (Manually Entered)" : dtpdf.Rows[i][j].ToString());
                            }
                            else
                            {
                                cellText = dtpdf.Rows[i]["Name"].ToString();
                            }
                        }
                        else if (j == 7 || j == 8)
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "1" ? "Yes" : (string.IsNullOrEmpty(dtpdf.Rows[i]["cusomerlatitude"].ToString()) && string.IsNullOrEmpty(dtpdf.Rows[i]["customerlongitude"].ToString())) ? "Customer location not available" : "No");
                        }
                        else
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        }
                        if (cellText == "")
                        {
                            cellText = "---";
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

                Response.AddHeader("Content-Disposition", "attachment; filename=" + lblaccordingToCustomer + "VisitReport.pdf");
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
                msg.Append("<b>The below table has the details for " + lblaccordingToCustomer + " Visits</b>");
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
                send.SendEmail(EmailId, "Customer Visit Report at " + GetCurrentDateTimeByUserId(), msgbody, ClientId);
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
                string head = "", UserName = "", EmpId = "", FrmDt = "", ToDt = "", CollegeName = "";
                try
                {
                    head = "<b style=' font-size: 20px;> " + lblaccordingToCustomer + " Visit Report </b>";
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
                    if (ddlCollegeName.SelectedValue != "0")
                    {
                        CollegeName = "<br/>  " + lblaccordingToCustomer + " Name : " + ddlCollegeName.SelectedItem.Text;
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
                Message = Message + ("<td colspan='11' align='center'>");
                Message = Message + ("<b style=' font-size: 20px;>" + head + "</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='11' align='center'>");
                Message = Message + (UserName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='11' align='center'>");
                Message = Message + (EmpId);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='11'align='center'>");
                Message = Message + (CollegeName);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='11'align='center'>");
                Message = Message + (FrmDt);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='11'align='center'>");
                Message = Message + (ToDt);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr  style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>" + lblaccordingToCustomer + " Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Employee Id</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Employee Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>In LogDateTime</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Out LogDateTime</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Location In</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Location Out</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>In Verification</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Out Verification</b>");
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
                        if (j == 5)
                        {
                            if (dtpdf.Rows[i]["InVerification"].ToString() != "1")
                            {
                                cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "Location not found" ? dtpdf.Rows[i]["InLatitude"].ToString() + "," + dtpdf.Rows[i]["InLongitude"].ToString() : Convert.ToBoolean(dtpdf.Rows[i]["IsInLocationManuallyEntered"].ToString()) ? dtpdf.Rows[i][j].ToString() + " (Manually Entered)" : dtpdf.Rows[i][j].ToString());
                            }
                            else
                            {
                                cellText = dtpdf.Rows[i]["Name"].ToString();
                            }
                        }
                        else if (j == 6)
                        {
                            if (dtpdf.Rows[i]["OutVerification"].ToString() != "1")
                            {
                                cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "Location not found" ? dtpdf.Rows[i]["OutLatitude"].ToString() + "," + dtpdf.Rows[i]["OutLongitude"].ToString() : Convert.ToBoolean(dtpdf.Rows[i]["IsOutLocationManuallyEntered"].ToString()) ? dtpdf.Rows[i][j].ToString() + " (Manually Entered)" : dtpdf.Rows[i][j].ToString());
                            }
                            else
                            {
                                cellText = dtpdf.Rows[i]["Name"].ToString();
                            }
                        }
                        else if (j == 7 || j == 8)
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString() == "1" ? "Yes" : (string.IsNullOrEmpty(dtpdf.Rows[i]["cusomerlatitude"].ToString()) && string.IsNullOrEmpty(dtpdf.Rows[i]["customerlongitude"].ToString())) ? "Customer location not available" : "No");
                        }
                        else
                        {
                            cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
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
        protected void Gdv_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    Response.TransmitFile(Server.MapPath("~/Files/Android_Files") + filename);
                    Response.End();

                }
            }
            catch (Exception)
            {
                lblMsg.Text = "File not found!";
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

        }
        protected void lnkbtnView_Click(object sender, EventArgs e)
        {
            try
            {
                lt = new LTCollegeVisitBAL();
                LinkButton lnkbtn = sender as LinkButton;
                GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
                Label lbl = (Label)grdUser.Rows[gvr.RowIndex].FindControl("lblId");
                Label cname = (Label)grdUser.Rows[gvr.RowIndex].FindControl("lblcname");
                lt.LTCollegeVisitId = Convert.ToInt32(lbl.Text);
                Gdv.DataSource = lt.GetLTCollegeVisitRemarkDetailsByLTCollegeVisitId();
                Gdv.DataBind();
                mpattach.Show();
            }
            catch (Exception)
            {

            }
        }
        public void UniqueUserCount()
        {
            srch = new AnuSearch();
            DataTable dt = srch.GetUniqueLTCollegeVisitDetailsToday(ClientId, txtFrmDt.Text.Trim(), txtToDt.Text.Trim());
            UserCount.Text = dt.Rows[0]["Unique_User_Count"].ToString();
            VisitCount.Text = dt.Rows[0]["Total_Visit_Count"].ToString();
            CollegeCount.Text = dt.Rows[0]["Unique_College_Count"].ToString();
        }
        protected void CreatePdf_Click(object sender, EventArgs e)
        {
            DataTable dtpdf = new DataTable();
            srch = new AnuSearch();
            dtpdf= srch.GetLTCollegeVisitDetailsToday(ClientId, DateTime.Now.ToString());
            Document document = new Document(PageSize.A2, 10f, 10f, 10f, 10f);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(2);
                PdfPCell cell1 = null;

                cell1 = PhraseCell(new Phrase(" Today Visit Report ", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                cell1.Colspan = 8;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);

                for (int x = 0; x < dtpdf.Columns.Count; x++)
                {
                    string cellText = Server.HtmlDecode(dtpdf.Columns[x].ToString());
                    iTextSharp.text.Font font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);//new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);
                    font.Color = new BaseColor(255, 255, 255);
                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                    cell.BackgroundColor = new BaseColor(42, 54, 137);
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    table.AddCell(cell);
                }

                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    for (int j = 0; j < dtpdf.Columns.Count; j++)
                    {
                        string cellText;
                        cellText = Server.HtmlDecode(dtpdf.Rows[i][j].ToString());
                        if (cellText == "")
                        {
                            cellText = "---";
                        }
                        iTextSharp.text.Font font = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);//new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                        font.Color = new BaseColor(0, 0, 0);
                        iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        if (i % 2 == 0)
                        {
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
                Response.AddHeader("Content-Disposition", "attachment; filename=VisitReport.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }
        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = Convert.ToInt32(Session["ClientId"].ToString()) == 399 ? "College Name" : "Customer Name";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lt = new LTCollegeVisitBAL();
                Label inVerification = (Label)e.Row.FindControl("lblInVerification");
                Label inLocation = (Label)e.Row.FindControl("lblLocationIn");
                Label inLocation1 = (Label)e.Row.FindControl("lblLocationIn1");
                Label outVerification = (Label)e.Row.FindControl("lblOutVerification");
                Label outLocation = (Label)e.Row.FindControl("lblLocationOut");
                Label outLocation1 = (Label)e.Row.FindControl("lblLocationOut1");
                Label name = (Label)e.Row.FindControl("lblcname");
                Label lbl = (Label)e.Row.FindControl("lblId");
                Label isInLocationManual = (Label)e.Row.FindControl("lblIsInLocationManual");
                Label isOutLocationManual = (Label)e.Row.FindControl("lblIsOutLocationManual");
                LinkButton lblLnkbtn = (LinkButton)e.Row.FindControl("lnkbtnView");
                HtmlControl inloc = (HtmlControl)e.Row.FindControl("inloc");
                HtmlControl outloc = (HtmlControl)e.Row.FindControl("outloc");
                lt.LTCollegeVisitId = Convert.ToInt32(lbl.Text);
                dt = lt.GetLTCollegeVisitRemarkDetailsByLTCollegeVisitId();
                if (dt.Rows.Count == 0)
                {
                    lblLnkbtn.Enabled = false;
                    lblLnkbtn.Text = "N/A";
                }
                if (!string.IsNullOrEmpty(inVerification.Text))
                {
                    if (inVerification.Text == "Yes")
                    {
                        inLocation.Text = name.Text;
                        inloc.Visible = false;
                    }
                    else
                    {
                        inLocation1.Visible = true;
                        inloc.Visible = true;
                    }
                }
                if (!string.IsNullOrEmpty(outVerification.Text))
                {
                    if (outVerification.Text == "Yes")
                    {
                        outLocation.Text = name.Text;
                        outloc.Visible = false;
                    }
                    else
                    {
                        outLocation1.Visible = true;
                        outloc.Visible = true;
                    }
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {

            try
            {
                string excelMsg = "";
                string FileName = "CustomerVisitReport";
                if (ddlEmployee.SelectedIndex == 0)
                {
                    excelMsg = excelMsg + "All Users complete Report<br/>";
                }
                else
                {
                    excelMsg = excelMsg + ddlEmployee.SelectedItem.Text.ToString() + " " + " Report<br/>";
                }
                if (ddlCollegeName.SelectedIndex == 0)
                {
                    excelMsg = excelMsg + "Customer Name : " + " All <br/>";
                }
                else
                {
                    excelMsg = excelMsg + "Customer Name : " + ddlCollegeName.SelectedItem.Text.ToString() + " " + "<br/>";
                }
                excelMsg = excelMsg + " By Employee Id : " + txtEmpId.Text.Trim() + "<br/>";
                excelMsg = excelMsg + txtFrmDt.Text.Trim() + " To " + txtToDt.Text.Trim() + "<br/>";



                DataTable dt = new DataTable();
                DataTable dtvisit = new DataTable();
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                DataGrid dgGrid = new DataGrid();
                DataTable mydt = new DataTable();
                BindGrid();
                mydt = (DataTable)ViewState["dtUser"];

                dt.Columns.Add("Name");
                dt.Columns.Add("Employee Id");
                dt.Columns.Add("Employee Name");
                dt.Columns.Add("In LogDateTime");
                dt.Columns.Add("Out LogDateTime");
                dt.Columns.Add("Location In");
                dt.Columns.Add("Location Out");
                dt.Columns.Add("In Verification");
                dt.Columns.Add("Out Verification");
                if (ClientId == 399)
                {
                    dt.Columns.Add("Remark Available");
                    dt.Columns.Add("Image Available");
                }
                if (mydt.Rows.Count > 0)
                {
                    foreach (DataRow row in mydt.Rows)
                    {
                        string inverify = "", outverify = "", inlocation = "", outlocation = "", remark = "", image = "";
                        inverify = row["InVerification"].ToString() == "1" ? "Yes" : (string.IsNullOrEmpty(row["cusomerlatitude"].ToString()) && string.IsNullOrEmpty(row["customerlongitude"].ToString())) ? "Customer location not available" : "No";
                        outverify = row["OutVerification"].ToString() == "1" ? "Yes" : (string.IsNullOrEmpty(row["cusomerlatitude"].ToString()) && string.IsNullOrEmpty(row["customerlongitude"].ToString())) ? "Customer location not available" : "No";
                        if (ClientId == 399)
                        {
                            srch = new AnuSearch();
                            dtvisit = srch.CheckRemarkANdImageByVisitId(row["LtCollegeVisitId"].ToString());
                            if (dtvisit != null && dtvisit.Rows.Count > 0)
                            {
                                remark = !string.IsNullOrWhiteSpace(dtvisit.Rows[0]["Remark"].ToString()) ? "Yes" : "No";
                                image = !string.IsNullOrWhiteSpace(dtvisit.Rows[0]["ImagePath"].ToString()) ? "Yes" : "No";
                            }
                            else
                            {
                                remark = "No";
                                image = "No";
                            }
                        }
                        if (inverify == "Yes")
                        {
                            inlocation = row["Name"].ToString();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(row["InLocation"].ToString()) || row["InLocation"].ToString() == Constant.LocationNotFound)
                            {
                                if (!string.IsNullOrEmpty(row["InLatitude"].ToString()) && !string.IsNullOrEmpty(row["InLongitude"].ToString()))
                                {
                                    inlocation = row["InLatitude"].ToString() + "," + row["InLongitude"].ToString();
                                }
                                else
                                {
                                    inlocation = "-----";
                                }
                            }
                            else
                            {
                                if (Convert.ToBoolean(row["IsInLocationManuallyEntered"].ToString()))
                                {
                                    inlocation = row["InLocation"].ToString() + " (Manually Entered)";
                                }
                                else
                                {
                                    inlocation = row["InLocation"].ToString();
                                }
                            }
                        }
                        if (outverify == "Yes")
                        {
                            outlocation = row["Name"].ToString();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(row["OutLocation"].ToString()) || row["OutLocation"].ToString() == Constant.LocationNotFound)
                            {
                                if (!string.IsNullOrEmpty(row["OutLatitude"].ToString()) && !string.IsNullOrEmpty(row["OutLongitude"].ToString()))
                                {
                                    outlocation = row["OutLatitude"].ToString() + "," + row["OutLongitude"].ToString();
                                }
                                else
                                {
                                    outlocation = "------";
                                }
                            }
                            else
                            {
                                if (Convert.ToBoolean(row["IsOutLocationManuallyEntered"].ToString()))
                                {
                                    outlocation = row["OutLocation"].ToString() + " (Manually Entered)";
                                }
                                else
                                {
                                    outlocation = row["OutLocation"].ToString();
                                }
                            }
                        }
                        if (ClientId == 399)
                        {
                            dt.Rows.Add(row["Name"], row["empcompanyid"], row["username"], row["InLogDateTime"], row["OutLogDateTime"], inlocation, outlocation, inverify, outverify, remark, image);
                        }
                        else
                        {
                            dt.Rows.Add(row["Name"], row["empcompanyid"], row["username"], row["InLogDateTime"], row["OutLogDateTime"], inlocation, outlocation, inverify, outverify);
                        }
                    }
                }
                dgGrid.DataSource = dt;
                dgGrid.HeaderStyle.Font.Bold = true;
                dgGrid.DataBind();
                dgGrid.RenderControl(htw);
                //Response.Write("<HTML><HEAD>");
                //Response.Write("<style> BODY { background-color:lightyellow; } TD { background-color:lightgrey; } </style>"); 
                string attachLogAndTbl = @"<center><table >
                                <tr style='height:180px;vertical-align:top;'>                                
                                <td colspan='3'><h2><b>@excelMsg</b></h2></td> </tr>
                               
                              </table></center>
                                ";
                //attachLogAndTbl = attachLogAndTbl.Replace("@CollegeName", colName);            
                attachLogAndTbl = attachLogAndTbl + "<br>" + sw.ToString();

                attachLogAndTbl = attachLogAndTbl.Replace("@excelMsg", excelMsg);

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", FileName + ".xls"));
                Response.ContentType = "application/ms-excel";
                Response.Write(attachLogAndTbl);
                //Response.Write(sw.ToString());
                //this.EnableViewState = false;
                sw.Close();
                Response.End();
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                //lblTblhd.Text = ex.Message;
            }

        }
        protected void btnExportToExcel_ClickTodayVisit(object sender, EventArgs e)
        {

            try
            {
                
                string FileName = "VisitReport";
                srch = new AnuSearch();   
                DataTable dt = new DataTable();
                DataTable dtvisit = new DataTable();
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                DataGrid dgGrid = new DataGrid();
                DataTable mydt = new DataTable();
                mydt = srch.GetLTCollegeVisitDetailsToday(ClientId, DateTime.Now.ToString());
                dt.Columns.Add("Name");
                dt.Columns.Add("Visit_Count");
                if (mydt.Rows.Count > 0)
                {
                    foreach (DataRow row in mydt.Rows)
                    {
                        dt.Rows.Add(row["Name"], row["Visit_Count"]);
                    }
                }
                dgGrid.DataSource = dt;
                dgGrid.HeaderStyle.Font.Bold = true;
                dgGrid.DataBind();
                dgGrid.RenderControl(htw);
                string attachLogAndTbl = @"<center><table >
                                <tr>                                
                               <td colspan='2'><h2><b>Today Visit Report</b></h2></td> </tr>

                             </table></center>
                               ";           
                attachLogAndTbl = attachLogAndTbl + "<br>" + sw.ToString();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", FileName + ".xls"));
                Response.ContentType = "application/ms-excel";
                Response.Write(attachLogAndTbl);
                 sw.Close();
                Response.End();
                
            }
            catch (Exception)
            {
                //lblTblhd.Text = ex.Message;
            }

        }
    }
}
