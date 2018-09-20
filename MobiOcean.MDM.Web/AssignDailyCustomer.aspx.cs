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
    public partial class AssignDailyCustomer : Base
    {
        int ClientId, UserId, RoleId, userid;
        DataTable dt;
        AnuSearch anusrch;
        //SendSMSBAL sendSmsBal;
        CustomerBAL customerBal;
        UserBAL userBal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            txtFrDt.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                //txtFrDt.Text = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy");
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
                BindGrid(true);
            }
        }



        protected void btnAssigned_Click(object sender, EventArgs e)
        {
            bool check = false;
            int res = 0;
            string idlist = "";
            string ATime = "";
            for (int idx = 0; idx < grdCustomer.Rows.Count; idx++)
            {
                if (((Label)(grdCustomer.Rows[idx].FindControl("lblCheckId"))).Text == "1" || ((Label)(grdCustomer.Rows[idx].FindControl("lbldrop"))).Text == "1")
                {
                    int status = ((CheckBox)(grdCustomer.Rows[idx].FindControl("AchkRow_Parents"))).Checked ? 0 : 1;
                    idlist = ((Label)(grdCustomer.Rows[idx].FindControl("lblAId"))).Text;
                    //Aidlist = ((Label)(grdCustomer.Rows[idx].FindControl("lbAId"))).Text;
                    string TaskDtl = ((TextBox)grdCustomer.Rows[idx].FindControl("txtTaskDetail")).Text;
                    string dd1 = ((DropDownList)(grdCustomer.Rows[idx].FindControl("ddlFromHour"))).SelectedItem.Text == "HH" ? "00" : ((DropDownList)(grdCustomer.Rows[idx].FindControl("ddlFromHour"))).SelectedItem.Text;
                    string dd2 = ((DropDownList)(grdCustomer.Rows[idx].FindControl("ddlFromMin"))).SelectedItem.Text == "MM" ? "00" : ((DropDownList)(grdCustomer.Rows[idx].FindControl("ddlFromMin"))).SelectedItem.Text;
                    ATime = dd1 + ":" + dd2;
                    if (!string.IsNullOrWhiteSpace(idlist) && !string.IsNullOrWhiteSpace(ATime))
                    {
                        customerBal = new CustomerBAL();
                        //custb.AssignId = Convert.ToInt32(Aidlist);
                        customerBal.ClientId = ClientId;
                        customerBal.UserId = Convert.ToInt32(HiddenField.Text);
                        customerBal.CustomerId = Convert.ToInt32(idlist);
                        customerBal.Date = txtFrDt.Text;
                        customerBal.Time = ATime;
                        customerBal.CreatedBy = UserId;
                        customerBal.status = status;
                        customerBal.currentDateTime = GetCurrentDateTimeByUserId();
                        customerBal.TaskDetail = TaskDtl;
                        res = customerBal.AssignCustomerDaily();
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
            //    sendSmsBal = new SendSMSBAL();
            //    sendSmsBal.sendFinalSMS(dt.Rows[0]["MobileNo"].ToString(), "GBox set as CU1", ClientId);
            //}
            bool s = RDBAssigned.Checked ? true : false;
            BindGrid(s);
            lblPopMsg.Text = "Customer Assigned Successfully to User";
            lblPopMsg.ForeColor = System.Drawing.Color.Green;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ManageCustomer.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AssignedCustomerList.aspx?Id=" + HiddenField.Text);
        }
        protected void AchkHeader_Parents_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdCustomer.HeaderRow.FindControl("AchkHeader_Parents");
            foreach (GridViewRow row in grdCustomer.Rows)
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

        public void BindGrid(bool IsAssigned)
        {
            anusrch = new AnuSearch();
            dt = new DataTable();
            userid = Convert.ToInt32(ViewState["userid"]);
            if (IsAssigned)
            {
                grdCustomer.DataSource = anusrch.GetCustomerByClientIdAssigned(userid, ClientId, "");
                grdCustomer.DataBind();
            }
            else
            {
                grdCustomer.DataSource = anusrch.GetAllCustomerByClientId(userid, ClientId, "");
                grdCustomer.DataBind();
            }
            ViewState["userid"] = userid;
        }

        protected void RDBAssigned_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(true);
        }

        protected void RDBAll_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid(false);
        }
    }
}
