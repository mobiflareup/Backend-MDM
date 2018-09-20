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
    public partial class AlertManagement : Base
    {
        int ClientId, UserId, RoleId;
        AnuSearch srch;
        AlertBAL alert;
        PermisesBAL perm;
        DataTable dt1;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
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
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
            grdAlert.EditIndex = -1;
        }

        protected void grdAlert_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAlert.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            grdAlert.DataSource = srch.GetAlertForDtls(ClientId, txtMobileNo.Text.Trim(), "0");
            grdAlert.DataBind();
        }
        protected void grdAlert_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAlert.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdAlert_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow gvr = grdAlert.Rows[e.RowIndex];
                alert = new AlertBAL();
                alert.ClientId = ClientId;
                alert.AlertId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                alert.MobileNo = ((TextBox)gvr.FindControl("txtEMobileNo")).Text.Trim();
                alert.AlertFor = ((DropDownList)gvr.FindControl("ddlEAlertFor")).Text.Trim();
                alert.UserId = UserId;
                alert.CountryId = ((DropDownList)gvr.FindControl("ddlCountryEdit")).SelectedItem.Value;
                int res = alert.InsertAlertDtlsRaj();
                if (res > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdAlert.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Not Updated";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    grdAlert.EditIndex = -1;
                    BindGrid();
                }
            }
            catch (Exception)
            {
            }
        }
        protected void grdAlert_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAlert.EditIndex = -1;
            BindGrid();
        }
        protected void grdAlert_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow gvr = grdAlert.Rows[e.RowIndex];
                lblalertid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                mpdelete.Show();
            }
            catch (Exception)
            {
            }
        }
        protected void grdNo_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlCountryedit");
                    ListItem li = new ListItem("------Select Country------", "0");
                    ddList.Items.Clear();
                    ddList.Items.Add(li);
                    //return DataTable havinf department data
                    DataTable dt = (DataTable)ViewState["GetCountry"];
                    ddList.DataSource = dt;
                    ddList.DataTextField = "CountryName";
                    ddList.DataValueField = "CountryId";
                    ddList.DataBind();

                    DataRowView dr = e.Row.DataItem as DataRowView;
                    ddList.SelectedValue = dr["CountryId"].ToString();
                    TextBox txt = (TextBox)e.Row.FindControl("txtCodeedit");
                    txt.Text = dt.Rows[Convert.ToInt32(ddList.SelectedValue) - 1]["PhoneCode"].ToString();
                    //ddList.Items.FindByValue((e.Row.FindControl("txtCodeedit") as TextBox).Text).Selected = true;
                }
            }
        }
        protected void grdAlert_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlCountryEdit");
                    ListItem li = new ListItem("Select", "0");
                    ddList.Items.Clear();
                    ddList.Items.Add(li);
                    //return DataTable havinf department data
                    DataTable dt = (DataTable)ViewState["GetCountry"];
                    ddList.DataSource = dt;
                    ddList.DataTextField = "Country";
                    ddList.DataValueField = "CountryId";
                    ddList.DataBind();

                    DataRowView dr = e.Row.DataItem as DataRowView;
                    ddList.SelectedValue = dr["CountryId"].ToString();

                    DropDownList ddlEAlertFor = (DropDownList)e.Row.FindControl("ddlEAlertFor");
                    Label lblEAlertFor = (Label)e.Row.FindControl("lblEAlertFor");

                    string alert = lblEAlertFor.Text;
                    ddlEAlertFor.Items.FindByValue(alert).Selected = true;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            alert = new AlertBAL();
            if (txtAddMobileNo.Text.Length > 3 && txtAddMobileNo.Text.Length < 16)
            {
                alert.ClientId = ClientId;
                alert.MobileNo = txtAddMobileNo.Text.Trim();
                alert.CountryId = ddlCountry.SelectedItem.Value;
                alert.AlertFor = "0";
                alert.UserId = UserId;
                int res = Convert.ToInt32(alert.InsertAlertDtlsRaj());
                if (res > 0)
                {
                    lblMsg.Text = "Saved Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Response.Redirect("AlertManagement.aspx");
                }
                else
                {
                    lblMsg.Text = "Not Saved";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

            else
            {
                lblMsg.Text = "Only allowed 3-15 Digit Mobile No";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            grdAlert.EditIndex = -1;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            grdAlert.EditIndex = -1;
        }
        private void reset()
        {
            txtAddMobileNo.Text = "";
            txtAddMobileNo.Text = "";
            reset();
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            alert = new AlertBAL();
            string alertid = lblalertid.Text;
            alert.AlertId = Convert.ToInt32(alertid);
            int res = alert.DeleteAlertDtls();
            if (res > 0)
            {
                lblMsg.Text = "Deleted Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                grdAlert.EditIndex = -1;
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Not Deleted";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                grdAlert.EditIndex = -1;
                BindGrid();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalertid.Text = "";
        }
    }
}