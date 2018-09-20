using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class EditCustomer : Base
    {
        CustomerBAL customer;
        DataTable dt, dt1;
        PermisesBAL perm;
        int ClientId, UserId, RoleId, DeptId, Customerid;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            Customerid = Convert.ToInt32(Request.QueryString["Id"]);
            if (!IsPostBack)
            {
                perm = new PermisesBAL();
                dt1 = new DataTable();
                dt1 = perm.GetCountries();
                ViewState["GetCountry"] = dt1;
                BindCountryddl();
                customer = new CustomerBAL();
                customer.CustomerId = Customerid;
                dt = new DataTable();
                dt = customer.CustomerDetailsbyCustomerid();
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtmobile.Text = dt.Rows[0]["MobileNo"].ToString();
                    ddlCountry.SelectedIndex = string.IsNullOrEmpty(dt.Rows[0]["CountryId"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["CountryId"].ToString());
                    txtAltmobile.Text = dt.Rows[0]["ALtMobileNo"].ToString();
                    ddlaltcontact.SelectedIndex = string.IsNullOrEmpty(dt.Rows[0]["AltCountryId"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["AltCountryId"].ToString());
                    txtcontact.Text = dt.Rows[0]["ContactPersion"].ToString();
                    txtaltcontactpersion.Text = dt.Rows[0]["AltContactPersion"].ToString();
                    txtemail.Text = dt.Rows[0]["EmailId"].ToString();
                    txtAltEmail.Text = dt.Rows[0]["AltEmailId"].ToString();
                    txtAddress.Text = dt.Rows[0]["Address"].ToString();
                    txtLat.Text = dt.Rows[0]["Latitude"].ToString();
                    txtLong.Text = dt.Rows[0]["Longitude"].ToString();
                    txtcity.Text = dt.Rows[0]["City"].ToString();
                    txtDist.Text = dt.Rows[0]["District"].ToString();
                    txtState.Text = dt.Rows[0]["state"].ToString();
                    txtCountry.Text = dt.Rows[0]["country"].ToString();
                    txtPin.Text = dt.Rows[0]["PinCode"].ToString();
                    txttin.Text = dt.Rows[0]["TinNumber"].ToString();

                }
                else
                {
                    Response.Redirect("CustomerDetails.aspx");
                }

            }
        }
        protected void BindCountryddl()
        {
            #region--------- Get School List --------
            try
            {
                ListItem li = new ListItem("Select", "0");
                ddlCountry.Items.Clear();
                ddlCountry.Items.Add(li);
                ddlCountry.DataSource = (DataTable)ViewState["GetCountry"];
                ddlCountry.DataTextField = "Country";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ListItem li1 = new ListItem("Select", "0");
                ddlaltcontact.Items.Clear();
                ddlaltcontact.Items.Add(li1);
                ddlaltcontact.DataSource = (DataTable)ViewState["GetCountry"];
                ddlaltcontact.DataTextField = "Country";
                ddlaltcontact.DataValueField = "CountryId";
                ddlaltcontact.DataBind();
            }
            catch (Exception)
            {
            }
            #endregion
        }
        private void alertbox(string msg)
        {
            string message = msg;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAltmobile.Text))
            {
                if (Convert.ToInt32(ddlaltcontact.SelectedItem.Value) > 0)
                    SaveCustomer();
                else
                {
                    lblMsg.Text = "Please Select Country for Alternate Contact ";
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                SaveCustomer();
            }
        }
        private void SaveCustomer()
        {
            customer = new CustomerBAL();
            customer.UserId = UserId;
            customer.ClientId = ClientId;
            customer.CustomerName = txtName.Text.Trim();
            customer.CountryId = ddlCountry.SelectedItem.Value;
            customer.MobileNo = txtmobile.Text.Trim();
            customer.AltCountryId = ddlaltcontact.SelectedItem.Value;
            customer.ALtMobileNo = txtAltmobile.Text.Trim();
            customer.ContactPersion = txtcontact.Text.Trim();
            customer.AltContactPersion = txtaltcontactpersion.Text.Trim();
            customer.EmailId = txtemail.Text.Trim();
            customer.AltEmailId = txtAltEmail.Text.Trim();
            customer.Address = txtAddress.Text.Trim();

            customer.Latitude = txtLat.Text.Trim();
            customer.Longitude = txtLong.Text.Trim();
            customer.City = txtcity.Text.Trim();
            customer.District = txtDist.Text.Trim();
            customer.state = txtState.Text.Trim();
            customer.country = txtcontact.Text.Trim();
            customer.PinCode = txtPin.Text.Trim();
            customer.TinNumber = txttin.Text.Trim();
            customer.CustomerId = Customerid;
            int res = customer.customerInsertRaj();
            if (res > 0)
            {
                Response.Redirect("CustomerDetails.aspx");
            }
            else
            {
                lblMsg.Text = "Something Went Wrong.. ";
                lblMsg.Visible = true;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerDetails.aspx");
        }
    }
}