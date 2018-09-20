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
    public partial class CustomerDetails : Base
    {
        CustomerBAL customer;
        int ClientId, UserId, RoleId, DeptId;
        DataTable dt;
        AnuSearch srch;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void lnkPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkPopUp = sender as LinkButton;
                GridViewRow gvr = lnkPopUp.NamingContainer as GridViewRow;
                Label lbl = ((Label)grdCustomer.Rows[gvr.RowIndex].FindControl("lblId"));
                //customer = new CustomerBAL();
                //customer.CustomerId = Convert.ToInt32(lbl.Text);
                //dt = new DataTable();
                //dt = customer.CustomerDetailsbyCustomerid();
                DataRow dataRow = ((DataTable)ViewState["GetCostomer"]).AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["CustomerId"]) == Convert.ToInt32(lbl.Text));
                txtAltmobile.Text = string.IsNullOrEmpty(dataRow["ALtMobileNo"].ToString()) ? "---" : dataRow["AltCountryId1"].ToString() + " " + dataRow["ALtMobileNo"].ToString();
                txtaltcontactpersion.Text = string.IsNullOrEmpty(dataRow["AltContactPersion"].ToString()) ? "---" : dataRow["AltContactPersion"].ToString();
                txtAltEmail.Text = string.IsNullOrEmpty(dataRow["AltEmailId"].ToString()) ? "---" : dataRow["AltEmailId"].ToString();
                txtLat.Text = string.IsNullOrEmpty(dataRow["Latitude"].ToString()) ? "---" : dataRow["Latitude"].ToString();
                txtLong.Text = string.IsNullOrEmpty(dataRow["Longitude"].ToString()) ? "---" : dataRow["Longitude"].ToString();
                txtcity.Text = string.IsNullOrEmpty(dataRow["City"].ToString()) ? "---" : dataRow["City"].ToString();
                txtDist.Text = string.IsNullOrEmpty(dataRow["District"].ToString()) ? "---" : dataRow["District"].ToString();
                txtState.Text = string.IsNullOrEmpty(dataRow["state"].ToString()) ? "---" : dataRow["state"].ToString();
                txtCountry.Text = string.IsNullOrEmpty(dataRow["country"].ToString()) ? "---" : dataRow["country"].ToString();
                txtPin.Text = string.IsNullOrEmpty(dataRow["PinCode"].ToString()) ? "---" : dataRow["PinCode"].ToString();
                mpdtl.Show();
            }
            catch (Exception)
            {

            }
        }
        void Popup(bool isDisplay)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //if (isDisplay)
            //{
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#myModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
            //}
            //else
            //{
            //   // sb.Append(@"<script type='text/javascript'>");

            //   // sb.Append(" $('#myModal').removeClass('show');");
            //   //// sb.Append(" $('#myModal').removeClass('fade');");
            //   // //sb.Append("$('#myModal').modal('hide');");
            //   // sb.Append(@"</script>");
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", "openModal();", false);
            //}

        }
        protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCustomer.PageIndex = e.NewPageIndex;
            grdCustomer.EditIndex = -1;
            BindGrid();
        }

        private void BindGrid()
        {
            srch = new AnuSearch();
            dt = new DataTable();
            dt = srch.GetCustomerDetails(ClientId, txtName.Text.Trim(), txtMblNo.Text.Trim(), txtEmail.Text.Trim(), txtcontperson.Text.Trim());
            grdCustomer.DataSource = dt;
            grdCustomer.DataBind();
            ViewState["GetCostomer"] = dt;
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = sender as LinkButton;
            GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
            string CustomerId = grdCustomer.DataKeys[row.RowIndex].Value.ToString();
            string url = "~/EditCustomer.aspx?Id=" + CustomerId;
            Response.Redirect(url);
        }


        protected void grdCustomer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdCustomer.Rows[e.RowIndex];
            try
            {
                Label lbl = ((Label)gvr.FindControl("lblCname"));
                lblUser.Text = "Are you sure to delete " + lbl.Text + " details?";
                lblalertprofileid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                mpdelete.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            customer = new CustomerBAL();
            customer.CustomerId = Convert.ToInt32(lblalertprofileid.Text);
            int res = customer.DeleteCustomerDetailsbyCustomerid();
            if (res > 0)
            {
                lblMsg.Text = "Successfully Deleted";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Something Went Wrong";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalertprofileid.Text = "";
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    
}
}