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
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class Setting : Base
    {
       
        ChangePasswordBAL cpbal;
        UserBAL us;
        DataTable dt, dt1;
        SubscribeBAL subscribe;
        FeatureBAL feature;
        AnuSearch srch;

        int ClientId, RoleId, UserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            txtFrmDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Id"].ToString()) && Request.QueryString["Id"].ToString() == "1")
                    {
                        ShowSubscriptionPanel();
                    }
                    else
                    {
                        ShowPwdPanel();
                    }
                }
                catch (Exception)
                {
                    ShowPwdPanel();
                }

                //BindGrid();           
            }
        }
        protected void btnChange_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            cpbal = new ChangePasswordBAL();
            dt = new DataTable();
            us = new UserBAL();
            try
            {
                cpbal.UserId = UserId;
                cpbal.Password = txtOldPassword.Text;
                cpbal.NewPassword = txtNewPassword.Text;
                cpbal.ChangePassword();
                us.UserId = UserId;
                dt = us.GetUserDtlByUserId();
                if (dt.Rows.Count > 0)
                {
                    string emailid = dt.Rows[0]["EmailId"].ToString();
                    Changepass ch = new Changepass()
                    {
                        EmailId = emailid,
                        OldPassword = txtOldPassword.Text,
                        NewPassword = txtNewPassword.Text
                    };
                    var jsondata1 = new JavaScriptSerializer().Serialize(ch);
                    var client = new RestClient(Constant.MobiMoveSchool + "/api/User/ChangePassword", HttpVerb.POST, jsondata1.ToString(), 1);
                    var json = client.MakeRequest();
                }
                lblMsg.Text = "Password changed successfully!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            finally
            {
                cpbal = null;
            }
        }
        protected void btnForm_Click(object sender, EventArgs e)
        {
            ShowPwdPanel();
        }

        private void ShowPwdPanel()
        {
            Chng.Attributes["class"] = "active sdmtm";
            Subsc.Attributes["class"] = "sdmtm";
            AddAdmin.Attributes["class"] = "sdmtm";
            subcribe.Attributes["class"] = "sdmtm";
            MultiView1.ActiveViewIndex = 0;
            pswd_info.Visible = true;
        }
        protected void btnsubcrib_Click(object sender, EventArgs e)
        {
            Response.Redirect("Subscribe.aspx");
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ShowSubscriptionPanel();
        }

        private void ShowSubscriptionPanel()
        {
            Chng.Attributes["class"] = "sdmtm";
            Subsc.Attributes["class"] = "active sdmtm";
            AddAdmin.Attributes["class"] = "sdmtm";
            subcribe.Attributes["class"] = "sdmtm";
            MultiView1.ActiveViewIndex = 1;
            pswd_info.Visible = false;
            BindTextboxes();
            BindGridSolutions();
            BindGridActiveSolutions();
            BindBillingHstryGrid();
        }
        protected void btnAddAdmin_Click(object sender, EventArgs e)
        {
            Chng.Attributes["class"] = "sdmtm";
            Subsc.Attributes["class"] = "sdmtm";
            AddAdmin.Attributes["class"] = "active sdmtm";
            MultiView1.ActiveViewIndex = 2;            
        }
        protected void BindTextboxes()
        {
            try
            {
                subscribe = new SubscribeBAL();
                //subscribe.CurrentDateTime = DateTime.Now.AddMinutes(Constant.addMinutes).ToString("dd-MMM-yyyy HH:mm");
                subscribe.ClientId = ClientId;
                dt = subscribe.GetAppliedSubscriptionDtl();// GetSubscriptionDtlByCurrentDateTime();
                if (dt.Rows.Count > 0)
                {
                    txtNoOfLicense.Text = dt.Rows[0]["NoOfEmployees"].ToString();
                    txtTimePeriod.Text = dt.Rows[0]["Duration"].ToString();
                    txtRemainingTimePeriod.Text = dt.Rows[0]["RemainingTimePeriod"].ToString();
                    txtSmsCount.Text = dt.Rows[0]["SMSCount"].ToString();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void BindGridSolutions()
        {
            feature = new FeatureBAL();
            grdSoln.DataSource = feature.GetCategoryName();
            grdSoln.DataBind();
        }
        protected void BindGridActiveSolutions()
        {
            subscribe = new SubscribeBAL();
            subscribe.ClientId = ClientId;
            grdActiveSoln.DataSource = subscribe.GetAppliedActiveSolutions();// GetActiveSolutions();
            grdActiveSoln.DataBind();
        }
        protected void BindBillingHstryGrid()
        {
            srch = new AnuSearch();
            grdBillingHstry.DataSource = srch.GetSubscriptionDtls(txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), ClientId);
            grdBillingHstry.DataBind();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindBillingHstryGrid();
        }
        protected string MyFormat(string date)
        {
            return Convert.ToDateTime(date).ToString("dd-MMM-yyyy HH:mm");
        }
        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            int count = 0;
            string catids = "";
            foreach (GridViewRow row in grdSoln.Rows)
            {

                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkboxRows_Parents");
                Label lblcatid = (Label)row.FindControl("lblCategoryId");
                if (ChkBoxRows.Checked)
                {
                    catids += lblcatid.Text + ",";
                    count++;
                }

            }
            catids = catids.TrimEnd(',');
            string Data = "[{\"Id\":1,\"Features\":{\"FeatureIds\":\""+ catids + "\", \"Quantity\": \"1\", \"Duration\":\"1\"}},{\"Id\":2,\"Features\":null},{\"Id\":3,\"Features\":null}]";
            //Set a name for the form
            string formID = "MobiPayment";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + Constant.MobiURL + "cloud-management.php" +
                           "\" method=\"POST\">");

            strForm.Append("<input type=\"hidden\" name=\"Payment" +
                           "\" value=\"" + Data + "\">");

            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            Page.Controls.Add(new LiteralControl(strForm.ToString() + strScript.ToString()));
            //Response.Redirect(Constant.MobiURL + "cloud-management.php?No=0&Time=0&Ftr=" + catids);
        }

        protected void chkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdSoln.HeaderRow.FindControl("chkboxHeader_Parents");
            foreach (GridViewRow row in grdSoln.Rows)
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
        protected void lkbtndwnPDF_Click(object sender, EventArgs e)
        {
            srch = new AnuSearch();
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblsubId = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblgvSubscriptionId"));
            Label lblCity = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblCity"));
            Label lblAddress = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblAddress"));
            Label lblGSTNo = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblGSTNo"));
            Label lblEmailId = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblEmailId"));
            Label lblTotalAmount = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblTotalAmount"));
            Label lblDiscountAmount = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblDiscountAmount"));
            Label lblCGST = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblCGST"));
            Label lblSGST = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblSGST"));
            Label lblIGST = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblIGST"));
            Label lblSubTotal = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblSubTotal"));
            Label lblCreatedtime = ((Label)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lblCreatedtime"));
            LinkButton lnkbtn1 = ((LinkButton)grdBillingHstry.Rows[gvr.RowIndex].FindControl("lkbtndwnPDF"));
            PaymentResponseNew payRes = new PaymentResponseNew();
            dt1 = new DataTable();
            dt1 = srch.GetSubscriptionDetailsBySubId(lblsubId.Text);
            DataTable dttemp = new DataTable();
            dttemp.Columns.AddRange(new DataColumn[] {
                         new DataColumn("SNo"), new DataColumn("CategoryName"), new DataColumn("HSN"), new DataColumn("Price"),  new DataColumn("License"), new DataColumn("Duration"),
                    new DataColumn("TotalPrice")});
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dttemp.Rows.Add(i + 1, dt1.Rows[i]["CategoryName"].ToString(), "998319 (Other information technology services n.e.c )", dt1.Rows[i]["CloudPrice"].ToString(), dt1.Rows[i]["License"].ToString() == "0" ? "NA" : dt1.Rows[i]["License"].ToString()
                       , dt1.Rows[i]["Duration"].ToString() == "0" ? "NA" : dt1.Rows[i]["Duration"].ToString(), dt1.Rows[i]["PaidAmount"].ToString());
            }
            byte[] pdfByte = payRes.CreatePDF(lblCity.Text, lblAddress.Text, lblGSTNo.Text, Session["UserName"].ToString(), lblEmailId.Text, "", "ONLINE", string.IsNullOrWhiteSpace(lblDiscountAmount.Text) ? "0.00" : lblDiscountAmount.Text, lnkbtn1.Text, lblTotalAmount.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, lblSubTotal.Text, dttemp,Convert.ToDateTime(lblCreatedtime.Text));
            Response.Clear();
            MemoryStream ms = new MemoryStream(pdfByte);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename="+ lnkbtn1.Text + "_Invoice.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
        }
    }
}
