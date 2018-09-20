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
    public partial class ApproveCustomer : Base
    {

        int ClientId, UserId, RoleId, DeptId;
        CustomerBAL cust;
        AnuSearch srch;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());

            if (!IsPostBack)
            {
                BindGrid();
                lblmsg.Text = string.Empty;
            }
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            grdapprove.DataSource = srch.GetCustomerFromtblCustomerTemp(ClientId, txtName.Text.Trim(), txtmblno.Text.Trim(), txtemail.Text.Trim(), txtcontactperson.Text.Trim());
            grdapprove.DataBind();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdapprove_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdapprove.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void btnapprove_click(object sender, EventArgs e)
        {
            string idlist = "";
            int k = 0;
            for (int idx = 0; idx < grdapprove.Rows.Count; idx++)
            {
                if (((CheckBox)(grdapprove.Rows[idx].FindControl("chkbox"))).Checked)
                {
                    idlist = idlist + ((Label)(grdapprove.Rows[idx].FindControl("lblId"))).Text + ",";
                }
            }
            if (idlist != "")
            {
                idlist = idlist.Trim(',');
                string[] ids = idlist.Split(',');
                foreach (var custid in ids)
                {
                    cust = new CustomerBAL();
                    cust.CustomerTempId = Convert.ToInt32(custid);
                    int res = cust.ApproveCustomer();
                    if (res > 0)
                    {
                        k++;
                    }
                }
                if (k > 0)
                {
                    lblmsg.Text = "Approved Successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    BindGrid();
                }
                else
                {
                    lblmsg.Text = "Not Approved";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    BindGrid();
                }
            }
        }
        protected void grdapprove_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int count = grdapprove.Rows.Count;
                int c = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkbox");
                    Label iscustomer = (Label)e.Row.FindControl("lbliscustomer");
                    if (iscustomer.Text == "0")
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        chk.Checked = true;
                        chk.Enabled = false;
                        LinkButton lnkbtn = (LinkButton)e.Row.FindControl("EditButton");
                        lnkbtn.Enabled = false;
                    }
                    for (int idx = 0; idx < grdapprove.Rows.Count; idx++)
                    {
                        if (((CheckBox)grdapprove.FindControl("chkbox")).Checked)
                        {
                            c++;
                        }
                    }
                    CheckBox chkheader = (CheckBox)grdapprove.HeaderRow.FindControl("chkheader");
                    if (c == count)
                    {

                        chkheader.Checked = true;
                    }
                    else
                    {
                        chkheader.Checked = false;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void lnkbtnView_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
            Label lbl = (Label)grdapprove.Rows[gvr.RowIndex].FindControl("lblId");
            int customertempid = Convert.ToInt32(lbl.Text);
            BindPopup(customertempid);
            mpeManFields.Show();
        }
        protected void BindPopup(int customertempid)
        {
            cust = new CustomerBAL();
            cust.CustomerTempId = Convert.ToInt32(customertempid);
            dt = cust.GetAllCustomerFromtblCustomerTemp();
            if (dt.Rows.Count > 0)
            {
                lblName.Text = string.IsNullOrEmpty(dt.Rows[0]["Name"].ToString()) ? "---" : dt.Rows[0]["Name"].ToString();
                lblaltmblno.Text = string.IsNullOrEmpty(dt.Rows[0]["AltMobileNo"].ToString()) ? "---" : dt.Rows[0]["AltMobileNo"].ToString();
                lblaltcontperson.Text = string.IsNullOrEmpty(dt.Rows[0]["AltContactPersion"].ToString()) ? "---" : dt.Rows[0]["AltContactPersion"].ToString();
                lblaltemailid.Text = string.IsNullOrEmpty(dt.Rows[0]["AltEmailId"].ToString()) ? "---" : dt.Rows[0]["AltEmailId"].ToString();
                lbllat.Text = string.IsNullOrEmpty(dt.Rows[0]["Latitude"].ToString()) ? "---" : dt.Rows[0]["Latitude"].ToString();
                lbllong.Text = string.IsNullOrEmpty(dt.Rows[0]["Longitude"].ToString()) ? "---" : dt.Rows[0]["Longitude"].ToString();
                lblcity.Text = string.IsNullOrEmpty(dt.Rows[0]["City"].ToString()) ? "---" : dt.Rows[0]["City"].ToString();
                lbldistrict.Text = string.IsNullOrEmpty(dt.Rows[0]["District"].ToString()) ? "---" : dt.Rows[0]["District"].ToString();
                lblstate.Text = string.IsNullOrEmpty(dt.Rows[0]["State"].ToString()) ? "---" : dt.Rows[0]["State"].ToString();
                lblcountry.Text = string.IsNullOrEmpty(dt.Rows[0]["Country"].ToString()) ? "---" : dt.Rows[0]["Country"].ToString();
                lblpin.Text = string.IsNullOrEmpty(dt.Rows[0]["PinCode"].ToString()) ? "---" : dt.Rows[0]["PinCode"].ToString();
                lbltin.Text = string.IsNullOrEmpty(dt.Rows[0]["TinNumber"].ToString()) ? "---" : dt.Rows[0]["TinNumber"].ToString();
                lblaltaddr.Text = string.IsNullOrEmpty(dt.Rows[0]["AltAddress"].ToString()) ? "---" : dt.Rows[0]["AltAddress"].ToString();
            }
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            LinkButton lnkbtnedit = sender as LinkButton;
            GridViewRow gvr = lnkbtnedit.NamingContainer as GridViewRow;
            string customertempid = grdapprove.DataKeys[gvr.RowIndex].Value.ToString();
            Response.Redirect("~/EditTempCustomer.aspx?Id=" + customertempid);
        }
        protected void chkheader_OnCheckedCHanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkheader = (CheckBox)grdapprove.HeaderRow.FindControl("chkheader");
                foreach (GridViewRow row in grdapprove.Rows)
                {
                    CheckBox chkrow = (CheckBox)row.FindControl("chkbox");
                    if (chkheader.Checked == true)
                    {
                        chkrow.Checked = true;
                    }
                    else
                    {
                        chkrow.Checked = false;
                        chkrow.Enabled = true;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}