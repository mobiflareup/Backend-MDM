using MobiOcean.MDM.BAL.BAL;
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
    public partial class ManageCustomer : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AnuSearch srch;
        CustomerBAL cust;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
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
            srch = new AnuSearch();
            grdUser.DataSource = srch.GetUserByClientId(ClientId, txtUserName.Text.Trim());
            grdUser.DataBind();
           
        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void lnkbtnManageCust_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lblid = ((Label)grdUser.Rows[gvr.RowIndex].FindControl("lblId"));
            Label lblname = ((Label)grdUser.Rows[gvr.RowIndex].FindControl("lblUserName"));
            lblGrpId.Text = lblid.Text;
            lblGroupName.Text = lblname.Text;
            //(lblid.Text);
            BindRemoveSelectedGrid(Convert.ToInt32(lblid.Text));
            BindAddSelectedGrid(Convert.ToInt32(lblid.Text));
            lblPopMsg.Text = string.Empty;
            txtSearch.Text = txtRemoveSearch.Text = string.Empty;
            mp.Show();
        }
        protected void BindAddSelectedGrid(int userid)
        {
            srch = new AnuSearch();
            grdaddselected.DataSource = srch.GetCustomerByClientIdNotAssigned(userid, ClientId, txtSearch.Text.Trim());
            grdaddselected.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text));
            mp.Show();
        }
        protected void BindRemoveSelectedGrid(int userid)
        {
            srch = new AnuSearch();
            grdremoveselected.DataSource = srch.GetCustomerByClientIdAssigned(userid, ClientId, txtRemoveSearch.Text.Trim());
            grdremoveselected.DataBind();
        }
        protected void btnRemoveSearch_Click(object sender, EventArgs e)
        {
            BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text));
            mp.Show();
        }
        protected void btnaddselected_Click(object sender, EventArgs e)
        {
            try
            {
                int res = 0;
                string idlist = "";
                for (int idx = 0; idx < grdaddselected.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdaddselected.Rows[idx].FindControl("AchkRow_Parents"))).Checked)
                    {
                        idlist = idlist + ((Label)(grdaddselected.Rows[idx].FindControl("lblAId"))).Text + ",";
                    }
                }
                if (idlist != "")
                {
                    idlist = idlist.Trim(',');
                    string[] id = idlist.Split(',');
                    foreach (var custid in id)
                    {
                        cust = new CustomerBAL();
                        cust.ClientId = ClientId;
                        cust.UserId = Convert.ToInt32(lblGrpId.Text);
                        cust.CustomerId = Convert.ToInt32(custid);
                        cust.CreatedBy = UserId;
                        res = cust.AssignCustomerToUser();
                    }
                    if (res > 0)
                    {
                        lblPopMsg.Text = "Customer Assigned Successfully to User";
                        lblPopMsg.ForeColor = System.Drawing.Color.Green;
                        BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        mp.Show();
                    }
                    else
                    {
                        lblPopMsg.Text = "Customer not assigned to selected User.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Red;
                        mp.Show();
                    }
                }
                else
                {
                    lblPopMsg.Text = "Please select Customer";
                    lblPopMsg.ForeColor = System.Drawing.Color.Red;
                    mp.Show();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void AchkHeader_Parents_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdaddselected.HeaderRow.FindControl("AchkHeader_Parents");
            foreach (GridViewRow row in grdaddselected.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("AchkRow_Parents");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
            mp.Show();
        }

       
        protected void grdUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName== "Daily Customer")
            {
                long s =Convert.ToInt32(e.CommandArgument);
                Response.Redirect("~/AssignedCustomerList.aspx?Id=" + s);
            }
        }

        protected void btnAssignCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AssignDailyCustomerByExcel.aspx");
        }
        protected void btnViewDailyTask_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ViewCustomerDailyTask.aspx");
        }

        protected void RachkHeader_Parents_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdremoveselected.HeaderRow.FindControl("RachkHeader_Parents");
            foreach (GridViewRow row in grdremoveselected.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("RachkRow_Parents");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
            mp.Show();
        }

       
        protected void btnremoveselected_Click(object sender, EventArgs e)
        {
            try
            {
                string idlist = "";
                int res = 0;
                for (int idx = 0; idx < grdremoveselected.Rows.Count; idx++)
                {
                    if (((CheckBox)grdremoveselected.Rows[idx].FindControl("RachkRow_Parents")).Checked)
                    {
                        idlist = idlist + ((Label)(grdremoveselected.Rows[idx].FindControl("RlblAId"))).Text + ",";
                    }
                }
                if (idlist != "")
                {
                    idlist = idlist.Trim(',');
                    string[] id = idlist.Split(',');
                    foreach (var custid in id)
                    {
                        cust = new CustomerBAL();
                        cust.CustomerId = (Convert.ToInt32(custid));
                        cust.ClientId = ClientId;
                        cust.UserId = (Convert.ToInt32(lblGrpId.Text));
                        cust.CreatedBy = UserId;
                        res = cust.UnAssignCustomerToUser();
                    }
                    if (res > 0)
                    {
                        lblPopMsg.Text = "Customer UnAssigned Successfully to User";
                        lblPopMsg.ForeColor = System.Drawing.Color.Green;
                        BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        mp.Show();
                    }
                    else
                    {
                        lblPopMsg.Text = "Customer not unassigned to selected User.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Red;
                        mp.Show();
                    }
                }
                else
                {
                    lblPopMsg.Text = "Please select Customer";
                    lblPopMsg.ForeColor = System.Drawing.Color.Red;
                    mp.Show();
                }
            }
            catch (Exception)
            {

            }

        }
       

        protected void Notchk_change_checkedchange(object sender, EventArgs e)
        {
            GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
            string requestid = grdUser.DataKeys[Row.RowIndex].Value.ToString();
            //string cellvalue = Row.Cells[4].Text;
            CheckBox Notchk = ((CheckBox)grdUser.Rows[Row.RowIndex].FindControl("Notchk"));
            srch = new AnuSearch();




            string Notify;
            if (Notchk.Checked == true)
            {

                Notify = "1";
                
                int s = srch.Notify(requestid, Notify);
            }
            else
            {

                Notify = "0";
                
                int s = srch.Notify(requestid, Notify);
            }




        }
       
    }
}
