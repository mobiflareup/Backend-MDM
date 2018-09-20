
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
    public partial class AddMobile : Base
    {
        WipePhoneBAL wipeBAL;
        int ClientId, RoleId, UserId, DeptId;
        DataTable dt1;
        PermisesBAL perm;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                dt1 = new DataTable();
                perm = new PermisesBAL();
                dt1 = perm.GetCountries();
                ViewState["GetCountry"] = dt1;
                BindGrid();
                BindCountryddl();
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
            }
            catch (Exception)
            {
            }
            #endregion
        }
        protected void BindGrid()
        {
            try
            {
                wipeBAL = new WipePhoneBAL();
                wipeBAL.ClientId = ClientId;
                grdAddMobile.DataSource = wipeBAL.GetSosContactDetailsByClientId();
                grdAddMobile.DataBind();
            }
            catch (Exception)
            {

            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "" && txtMobileNo.Text != "")
                {
                    wipeBAL = new WipePhoneBAL();
                    wipeBAL.ClientId = ClientId;
                    wipeBAL.ContactPersonName = txtName.Text.Trim();
                    wipeBAL.ContactNo = txtMobileNo.Text.Trim();
                    wipeBAL.EmailId = txtEmailId.Text.Trim();
                    wipeBAL.Designation = txtDesignation.Text.Trim();
                    wipeBAL.UserId = UserId;
                    wipeBAL.CountryId = ddlCountry.SelectedItem.Value;
                    int res = wipeBAL.InsertSosDetailsByClientIdRaj();
                    if (res > 0)
                    {
                        lblMsg.Text = "Added Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        BindGrid();
                        CleartextBox();
                    }
                    else
                    {
                        lblMsg.Text = "Already Exists!!!";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "Please Enter Name and Mobile No";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception)
            {

            }
        }
        protected void CleartextBox()
        {
            txtName.Text = "";
            txtMobileNo.Text = "";
            txtDesignation.Text = "";
            txtEmailId.Text = "";
            ddlCountry.SelectedIndex = 0;
        }
        protected void grdAddMobile_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAddMobile.PageIndex = e.NewPageIndex;
            grdAddMobile.EditIndex = -1;
            BindGrid();
        }
        protected void grdAddMobile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow gvr = grdAddMobile.Rows[e.RowIndex];
                lblkeyid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                mpdelete.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            wipeBAL = new WipePhoneBAL();
            string alertid = lblkeyid.Text;
            wipeBAL.ContactId = Convert.ToInt32(alertid);
            int res = wipeBAL.DeleteSosContacts();
            if (res > 0)
            {
                lblMsg.Text = "Deleted Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Not Deleted!!!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblkeyid.Text = "";
        }

    }
}