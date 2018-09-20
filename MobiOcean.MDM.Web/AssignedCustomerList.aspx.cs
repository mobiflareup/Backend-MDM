using MobiOcean.MDM.BAL;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AssignedCustomerList : Base
    {
        int ClientId, UserId, RoleId;
        DataTable dt;
        AnuSearch srch;
        UserBAL userBal;
        //SendSMSBAL sms;
        CustomerBAL custb;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            txtFrDt.Attributes.Add("readonly", "readonly");
            txtToDt.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                txtFrDt.Text = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy");
                txtToDt.Text = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy");
                ViewState["userid"] = Convert.ToInt32(Request.QueryString["Id"]);
                HiddenField.Text = Request.QueryString["Id"].ToString();
                dt = new DataTable();
                userBal = new UserBAL();
                userBal.UserId = Convert.ToInt32(HiddenField.Text);
                dt = userBal.GetUserDtlByUserId();
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblusernamee.Text = "Employee Name : " + dt.Rows[0]["UserName"].ToString() + "   ";
                }
                BindGrid();
            }
        }

        private void BindGrid()
        {
            dt = new DataTable();
            srch = new AnuSearch();
            dt = srch.SrchAssignedDailyCustomerListByUserId(ClientId, HiddenField.Text, txtFrDt.Text, txtToDt.Text);
            grdCustomer.DataSource = dt;
            grdCustomer.DataBind();
        }


        protected void btnAssigned_Click(object sender, EventArgs e)
        {
            bool check = false;
            int res = 0;
            for (int idx = 0; idx < grdCustomer.Rows.Count; idx++)
            {
                if (((Label)(grdCustomer.Rows[idx].FindControl("lblCheckId"))).Text == "1" || ((Label)(grdCustomer.Rows[idx].FindControl("lbldrop"))).Text == "1" || ((Label)(grdCustomer.Rows[idx].FindControl("lbltask"))).Text == "1")
                {
                    int status = ((CheckBox)(grdCustomer.Rows[idx].FindControl("AchkRow_Parents"))).Checked ? 0 : 1;
                    string idlist = ((Label)(grdCustomer.Rows[idx].FindControl("lblAId"))).Text;                    
                    string dd1 = ((DropDownList)(grdCustomer.Rows[idx].FindControl("ddlFromHour"))).SelectedItem.Text == "HH" ? "00" : ((DropDownList)(grdCustomer.Rows[idx].FindControl("ddlFromHour"))).SelectedItem.Text;
                    string dd2 = ((DropDownList)(grdCustomer.Rows[idx].FindControl("ddlFromMin"))).SelectedItem.Text == "MM" ? "00" : ((DropDownList)(grdCustomer.Rows[idx].FindControl("ddlFromMin"))).SelectedItem.Text;
                    string TaskDtl = ((TextBox)grdCustomer.Rows[idx].FindControl("txtTaskDetail")).Text;
                    string ATime = dd1 + ":" + dd2;
                    if (!string.IsNullOrWhiteSpace(idlist) && !string.IsNullOrWhiteSpace(ATime))
                    {
                        custb = new CustomerBAL();
                        custb.AssignId = Convert.ToInt32(idlist);
                        custb.Time = ATime;
                        custb.CreatedBy = UserId;
                        custb.status = status;
                        custb.TaskDetail = TaskDtl;
                        res = custb.UpdateCustomerDailyTask();
                        if (res > 0)
                        {
                            check = true;
                        }
                    }
                }

            }
            //if (check)
            //{
            //    userBal = new UserBAL();
            //    dt = new DataTable();
            //    userBal.UserId = Convert.ToInt32(HiddenField.Text);
            //    dt = userBal.GetUserDtlByUserId();

            //    sms = new SendSMSBAL();
            //    sms.sendFinalSMS(dt.Rows[0]["MobileNo"].ToString(), "GBox set as CU1", Convert.ToInt32(Session["ClientId"].ToString()));
            //}
            BindGrid();
            lblMsg.Text = "Customer Assigned Successfully to User";
            lblMsg.ForeColor = System.Drawing.Color.Green;
        }

        protected void grdCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddlFromHour = (DropDownList)e.Row.FindControl("ddlFromHour");
            DropDownList ddlFromMin = (DropDownList)e.Row.FindControl("ddlFromMin");

            Label lblFromTime = (Label)e.Row.FindControl("lblFromTime");
            if (lblFromTime != null)
            {
                if (string.IsNullOrEmpty(lblFromTime.Text))
                {
                    ddlFromHour.SelectedIndex = 0;
                    ddlFromMin.SelectedIndex = 0;
                }
                else
                {
                    ddlFromHour.SelectedValue = lblFromTime.Text.Substring(0, 2);
                    ddlFromMin.SelectedValue = lblFromTime.Text.Substring(3, 2);
                }
                Label lblApproval = (Label)e.Row.FindControl("AlblApproval");
                Label lblIsVisited = (Label)e.Row.FindControl("lblIsVisited");
                if (lblApproval != null)
                {
                    if (string.IsNullOrWhiteSpace(lblApproval.Text) || string.IsNullOrWhiteSpace(lblIsVisited.Text)|| lblIsVisited.Text=="1")//|| lblApproval.Text == "2"||
                    {
                        CompareValidator CompareValidator1 = (CompareValidator)e.Row.FindControl("CompareValidator1");
                        CompareValidator CompareValidator2 = (CompareValidator)e.Row.FindControl("CompareValidator2");
                        CompareValidator1.Enabled = true;
                        CompareValidator2.Enabled = true;
                    }
                    else
                    {
                        ddlFromHour.Enabled = false;
                        ddlFromMin.Enabled = false;
                        CheckBox ChkBoxRows = (CheckBox)e.Row.FindControl("AchkRow_Parents");
                        ChkBoxRows.Enabled = false;
                    }
                }

            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ManageCustomer.aspx");
        }
        protected void AchkHeader_Parents_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdCustomer.HeaderRow.FindControl("AchkHeader_Parents");
            foreach (GridViewRow row in grdCustomer.Rows)
            {
                Label lblCheck = (Label)row.FindControl("lblCheckId");
                lblCheck.Text = (lblCheck.Text == "0" ? "1" : "0");
                Label lblApproval = (Label)row.FindControl("AlblApproval");
                if (string.IsNullOrWhiteSpace(lblApproval.Text))//|| lblApproval.Text == "2"
                {
                    CheckBox ChkBoxRows = (CheckBox)row.FindControl("AchkRow_Parents");
                    CompareValidator CompareValidator1 = (CompareValidator)row.FindControl("CompareValidator1");
                    CompareValidator CompareValidator2 = (CompareValidator)row.FindControl("CompareValidator2");
                    if (ChkBoxHeader.Checked == true)
                    {
                        ChkBoxRows.Checked = true;
                        CompareValidator1.Enabled = true;
                        CompareValidator2.Enabled = true;
                    }
                    else
                    {
                        ChkBoxRows.Checked = false;
                        CompareValidator1.Enabled = false;
                        CompareValidator2.Enabled = false;
                    }
                }
            }
        }

        protected void AchkRow_Parents_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox ddlStatus = (CheckBox)sender;
            GridViewRow row = (GridViewRow)ddlStatus.NamingContainer;
            CheckBox ChkBoxHeader = (CheckBox)grdCustomer.HeaderRow.FindControl("AchkHeader_Parents");
            CheckBox ChkBoxRow = (CheckBox)row.FindControl("AchkRow_Parents");
            CompareValidator CompareValidator1 = (CompareValidator)row.FindControl("CompareValidator1");
            CompareValidator CompareValidator2 = (CompareValidator)row.FindControl("CompareValidator2");
            Label lblCheck = (Label)row.FindControl("lblCheckId");
            lblCheck.Text = (lblCheck.Text == "0" ? "1" : "0");
            if (ChkBoxRow.Checked == false)
            {
                ChkBoxHeader.Checked = false;
                CompareValidator1.Enabled = false;
                CompareValidator2.Enabled = false;
            }
            else
            {
                CompareValidator1.Enabled = true;
                CompareValidator2.Enabled = true;
            }
        }

        protected void ddlFromHour_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddlStatus = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlStatus.NamingContainer;
            Label lbldrop = (Label)row.FindControl("lbldrop");
            lbldrop.Text = "1";
        }

        protected void ddlFromMin_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddlStatus = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlStatus.NamingContainer;
            Label lbldrop = (Label)row.FindControl("lbldrop");
            lbldrop.Text = "1";
        }

        protected void btnAssignCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AssignDailyCustomer.aspx?Id=" + HiddenField.Text);
        }

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void txtTaskDetail_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = (TextBox)sender;
            GridViewRow gvr = (GridViewRow)txtbox.NamingContainer;
            Label lbltask = (Label)gvr.FindControl("lbltask");
            lbltask.Text = "1";
        }

    }
}
