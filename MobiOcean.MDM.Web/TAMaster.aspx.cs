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
    public partial class TAMaster : Base
    {
        CustomerBAL customer;
        GingerboxSrch gsrch;
        usrBAL user;
        DataTable dt;
        SendMailBAL send;
        StringBuilder msg = new StringBuilder();
        int ClientId, UserId, RoleId, DeptId;
        string Message = "";
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
                txtFrmDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString(Constant.DateFormat);
                if (Session["tamastersearch"] != null)
                {
                    string[] a = Session["tamastersearch"].ToString().Split(',');
                    for (int i = 0; i < a.Count(); i++)
                    {
                        if (i == 0 && !string.IsNullOrEmpty(a[0]))
                        {
                            txtUsername.Text = a[0];
                        }
                        if (i == 1 && !string.IsNullOrEmpty(a[1]))
                        {
                            txtFrmDate.Text = a[1];
                        }
                        if (i == 2 && !string.IsNullOrEmpty(a[2]))
                        {
                            txtToDate.Text = a[2];
                        }
                        if (i == 3 && !string.IsNullOrEmpty(a[3]))
                        {
                            ddlApproved.Text = a[3];
                        }
                        if (i == 4 && !string.IsNullOrEmpty(a[4]))
                        {
                            ddlPaid.Text = a[4];
                        }
                    }
                    Session["tamastersearch"] = null;
                }
                if (RoleId == 1)
                {
                    BindClientDDL();
                }
                else
                {
                    divclientddl.Visible = false;
                }
                Session["dtTA"] = null;
                BindGrid();
            }
        }
        private void BindClientDDL()
        {
            try
            {
                user = new usrBAL();
                System.Web.UI.WebControls.ListItem li3 = new System.Web.UI.WebControls.ListItem("All", "0");
                dtClientId.Items.Clear();
                dtClientId.Items.Add(li3);
                user.ClientId = ClientId;
                dtClientId.DataSource = user.GetClientName();
                dtClientId.DataTextField = "ClientName";
                dtClientId.DataValueField = "ClientId";
                dtClientId.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                user = null;

            }
        }
        protected void dtClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
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
                if (RoleId == 1)
                {
                    dt = gsrch.GetTAMaster(Convert.ToInt32(dtClientId.SelectedValue.ToString()), 0, FrmDateTime, ToDateTime, txtUsername.Text, ddlApproved.SelectedValue.ToString(), ddlPaid.SelectedValue.ToString());
                }
                else if (RoleId == 2)
                {
                    dt = gsrch.GetTAMaster(ClientId, 0, FrmDateTime, ToDateTime, txtUsername.Text, ddlApproved.SelectedValue.ToString(), ddlPaid.SelectedValue.ToString());
                }
                else if (RoleId == 3)
                {
                    dt = gsrch.GetTAMaster(ClientId, 0, FrmDateTime, ToDateTime, txtUsername.Text, ddlApproved.SelectedValue.ToString(), ddlPaid.SelectedValue.ToString(), DeptId);
                }
                else
                {
                    dt = gsrch.GetTAMaster(ClientId, UserId, FrmDateTime, ToDateTime, txtUsername.Text, ddlApproved.SelectedValue.ToString(), ddlPaid.SelectedValue.ToString());
                }
                tamaster.DataSource = dt;
                tamaster.DataBind();
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
        protected void tamaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tamaster.PageIndex = e.NewPageIndex;
            tamaster.EditIndex = -1;
            BindGrid();
        }

        protected void lnkDetails_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            gsrch = new GingerboxSrch();
            int masterid = 0;
            LinkButton lnkPopUp = sender as LinkButton;
            GridViewRow gvr = lnkPopUp.NamingContainer as GridViewRow;
            Label lbl = ((Label)tamaster.Rows[gvr.RowIndex].FindControl("lblId"));
            masterid = Convert.ToInt32(lbl.Text);
            dt = gsrch.GetVistDetailsbyMasterId(masterid);
            if (dt.Rows.Count > 0)
            {
                //Session["dtTA"] = dt;
                string url = "TAVisitDtl.aspx?Id=" + masterid;
                //string url = "TAVisitDtl.aspx";
                Session["tamastersearch"] = txtUsername.Text + "," + txtFrmDate.Text + "," + txtToDate.Text + "," + ddlApproved.SelectedValue.ToString() + "," + ddlPaid.SelectedValue.ToString();
                Response.Redirect(url);
            }
            else
            {
                lblMsg.Text = "No visit record found!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void LinkOthers_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            gsrch = new GingerboxSrch();
            LinkButton lnkPopUp = sender as LinkButton;
            GridViewRow gvr = lnkPopUp.NamingContainer as GridViewRow;
            Label lbl = ((Label)tamaster.Rows[gvr.RowIndex].FindControl("lblId"));
            dt = gsrch.GetExtraDetailsbyMasterId(Convert.ToInt32(lbl.Text));
            if (dt.Rows.Count > 0)
            {
                //Session["dtTA"] = dt;
                string url = "TAOthersDtl.aspx?Id=" + (lbl.Text);
                //string url = "TAOthersDtl.aspx" ;
                Session["tamastersearch"] = txtUsername.Text + "," + txtFrmDate.Text + "," + txtToDate.Text + "," + ddlApproved.SelectedValue.ToString() + "," + ddlPaid.SelectedValue.ToString();
                Response.Redirect(url);
            }
            else
            {
                lblMsg.Text = "No extra expense detail found!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void chkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)tamaster.HeaderRow.FindControl("chkboxHeader_Parents");
            foreach (GridViewRow row in tamaster.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkboxRows_Parents");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void btnapproval_Click(object sender, EventArgs e)
        {
            try
            {
                Button lnkPopUp = sender as Button;
                GridViewRow gvr = lnkPopUp.NamingContainer as GridViewRow;
                Label lbl = ((Label)tamaster.Rows[gvr.RowIndex].FindControl("lblId"));
                gsrch = new GingerboxSrch();
                dt = new DataTable();
                dt = gsrch.GetTAMasterDetailsByMasterId(ClientId, Convert.ToInt32(lbl.Text));
                txtapprovalamt.Text = dt.Rows[0]["ClaimedAmt"].ToString();
                empid.Text = dt.Rows[0]["EmpCompanyId"].ToString();
                usna.Text = dt.Rows[0]["UserName"].ToString();
                logd.Text = dt.Rows[0]["LogDate"].ToString();
                claimamout.Text = dt.Rows[0]["ClaimedAmt"].ToString();
                lbltamasteid.Text = dt.Rows[0]["MasterId"].ToString();
                //Popup(true);
                mpe.Show();
            }
            catch (Exception)
            {

            }
        }


        protected void Approved_Click(object sender, EventArgs e)
        {
            customer = new CustomerBAL();
            customer.MasterID = int.Parse(lbltamasteid.Text);
            customer.UserId = UserId;

            customer.ApprovedAmmount = double.Parse(txtapprovalamt.Text == "" ? "0" : txtapprovalamt.Text);
            customer.ApproverRemark = txtappremark.Text;
            int res = customer.UpdateApprovedstatus();
            if (res == 1)
            {
                lblMsg.Text = "Approved successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                //Popup(false);
                mpe.Hide();
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Something went wrong!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                //Popup(false);
                mpe.Hide();
            }
        }
        protected void Approvedandpay_Click(object sender, EventArgs e)
        {
            customer = new CustomerBAL();
            customer.MasterID = int.Parse(lbltamasteid.Text);
            customer.UserId = UserId;
            customer.ApprovedAmmount = double.Parse(txtapprovalamt.Text == "" ? "0" : txtapprovalamt.Text);
     //       customer.ApprovedAmmount = double.Parse(txtapprovalamt.Text);
            customer.ApproverRemark = txtappremark.Text;
            int res = customer.UpdateApprovedandPaystatus();
            if (res == 1)
            {
                lblMsg.Text = "Approved and paid successfully!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                //Popup(false);
                mpe.Hide();
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Something went wrong!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                //Popup(false);
                mpe.Hide();
            }
        }
        //void Popup(bool isDisplay)
        //{
        //    if (isDisplay)
        //    {
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        sb.Append(@"<script type='text/javascript'>");
        //        sb.Append("$('#myModal').modal('show');");
        //        sb.Append(@"</script>");
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
        //    }
        //    else
        //    {

        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalScript", "$('#myModal').modal('close');", true);
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        sb.Append(@"<script type='text/javascript'>");
        //        sb.Append("$('#myModal').modal('close');");
        //        sb.Append(@"</script>");
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), true);

        //    }
        //}

        protected void btnpaid_Click(object sender, EventArgs e)
        {
            try
            {
                Button lnkPopUp = sender as Button;
                GridViewRow gvr = lnkPopUp.NamingContainer as GridViewRow;
                Label lbl = ((Label)tamaster.Rows[gvr.RowIndex].FindControl("lblId"));
                Label lblamount = ((Label)tamaster.Rows[gvr.RowIndex].FindControl("lblApAmt"));
                Label lblremark = ((Label)tamaster.Rows[gvr.RowIndex].FindControl("lblappR"));
                customer = new CustomerBAL();
                customer.MasterID = Convert.ToInt32(lbl.Text);
                customer.UserId = UserId;
                customer.ApprovedAmmount = double.Parse(lblamount.Text);
                customer.ApproverRemark = lblremark.Text;
                int res = customer.UpdateApprovedandPaystatus();
                if (res == 1)
                {
                    lblMsg.Text = "Paid successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    //Popup(false);
                    mpe.Hide();
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Something went wrong!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    mpe.Hide();
                    //Popup(false);
                }
            }
            catch (Exception)
            {

            }

        }


        protected void btnclose_Click(object sender, EventArgs e)
        {
            mpe.Hide();
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
            DataTable dtpdf = new DataTable();
            dtpdf = (DataTable)ViewState["dtUser"];
            Document document = new Document(PageSize.A2, 10f, 10f, 10f, 10f);
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(10);
                PdfPCell cell1 = null, cell2 = null, cell3 = null, cell4 = null, cell5 = null, cell6 = null;


                cell1 = PhraseCell(new Phrase("Travel Allowance Master Report", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);

                cell1.Colspan = 10;
                cell1.BorderWidth = 0;
                table.AddCell(cell1);
                if (txtUsername.Text != "")
                {
                    cell2 = PhraseCell(new Phrase("  User Name : " + txtUsername.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell2.Colspan = 10;
                    cell2.BorderWidth = 0;
                    table.AddCell(cell2);
                }
                if (txtFrmDate.Text != "")
                {
                    cell3 = PhraseCell(new Phrase("  From Date : " + txtFrmDate.Text, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell3.Colspan = 10;
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
                if (Convert.ToInt16(ddlApproved.SelectedValue) == 0 || Convert.ToInt16(ddlApproved.SelectedValue) == 1)
                {
                    cell5 = PhraseCell(new Phrase("  Approved : " + ddlApproved.SelectedItem.ToString(), FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell5.Colspan = 10;
                    cell5.BorderWidth = 0;
                    table.AddCell(cell5);
                }

                if (Convert.ToInt16(ddlPaid.SelectedValue) == 0 || Convert.ToInt16(ddlPaid.SelectedValue) == 1)
                {
                    cell6 = PhraseCell(new Phrase("  Paid : " + ddlPaid.SelectedItem.ToString(), FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                    cell6.Colspan = 10;
                    cell6.BorderWidth = 0;
                    table.AddCell(cell6);
                }


                for (int x = 0; x < tamaster.Columns.Count; x++)
                {
                    if (x == 0 || x == 1 || x > 11)
                    {
                        continue;
                    }
                    string cellText = Server.HtmlDecode(tamaster.HeaderRow.Cells[x].Text);
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
                    for (int j = 0; j < tamaster.Columns.Count; j++)
                    {
                        if (j > 9)
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
                        if (j == 6 || j == 9)
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
                Response.AddHeader("Content-Disposition", "attachment; filename=TAMaster.pdf");
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
                string UName = "", IsPaid = "", IsApproved = "", FDate = "", TDate = "";
                try
                {



                    if (txtUsername.Text != "")
                    {
                        UName = "<b> User Name : </b>" + txtUsername.Text.Trim();
                    }
                    if (txtFrmDate.Text != "")
                    {
                        FDate = "<b> From Date : </b> " + txtFrmDate.Text.Trim();
                    }
                    if (txtToDate.Text != "")
                    {
                        TDate = " <b>To Date : </b> " + txtToDate.Text.Trim();
                    }
                    if (Convert.ToInt16(ddlApproved.SelectedValue) == 0 || Convert.ToInt16(ddlApproved.SelectedValue) == 1)
                    {
                        IsApproved = " <b>Approved : </b> " + ddlApproved.SelectedItem.ToString();
                    }
                    if (Convert.ToInt16(ddlPaid.SelectedValue) == 0 || Convert.ToInt16(ddlPaid.SelectedValue) == 1)
                    {
                        IsPaid = " <b>Paid : </b> " + ddlPaid.SelectedItem.ToString();
                    }
                }
                catch (Exception)
                {

                }

                Message = ("<table width='100%' cellspacing='0' cellpadding='0' border=0 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + ("<b style=' font-size: 20px;'>TA Master Report</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (UName);
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

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (IsApproved);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("<tr>");
                Message = Message + ("<td colspan='8' align='center'>");
                Message = Message + (IsPaid);
                Message = Message + ("</td>");
                Message = Message + ("</tr>");

                Message = Message + ("</table>");

                Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Employee Id</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>User Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Log Date</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Total Distance</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Claimed Amount</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Approved Amount</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Approval</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Approved By</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Approver Remark</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Paid</b>");
                Message = Message + ("</td>");

                Message = Message + ("</tr>");


                for (int i = 0; i < dtpdf.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j < 10; j++)
                    {
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
                        if (j == 6 || j == 9)
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
                msg.Append("<b>The below table has the details for Travel Allowance.</b>");
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
                send.SendEmail(EmailId, "Travel Allowance Master Report at " + GetCurrentDateTimeByUserId(), Msgbody, ClientId);
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
                    send.SendEmail(txtMailTo.Text, "Travel Allowance Master Report at " + GetCurrentDateTimeByUserId(), Msgbody, ClientId);
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
    }
}
