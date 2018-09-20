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
    public partial class EditTempCustomer : Base
    {
        CustomerBAL customer;
        DataTable dt;
        int ClientId, UserId, RoleId, DeptId, CustomerTempId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            CustomerTempId = Convert.ToInt32(Request.QueryString["Id"]);
            if (!IsPostBack)
            {
                customer = new CustomerBAL();
                customer.CustomerTempId = CustomerTempId;
                dt = new DataTable();
                dt = customer.TempCustomerDetailsbyCustomerid();
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtmobile.Text = dt.Rows[0]["MobileNo"].ToString();
                    txtAltmobile.Text = dt.Rows[0]["ALtMobileNo"].ToString();
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
            customer = new CustomerBAL();
            customer.UserId = UserId;
            customer.ClientId = ClientId;
            customer.CustomerName = txtName.Text.Trim();
            customer.MobileNo = txtmobile.Text.Trim();
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
            customer.country = txtCountry.Text.Trim();
            customer.PinCode = txtPin.Text.Trim();
            customer.TinNumber = txttin.Text.Trim();
            customer.CustomerTempId = CustomerTempId;
            int res = customer.UpdatetblCustomerTemp();
            if (res > 0)
            {
                Response.Redirect("ApproveCustomer.aspx");
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
            Response.Redirect("ApproveCustomer.aspx");
        }
    }
}