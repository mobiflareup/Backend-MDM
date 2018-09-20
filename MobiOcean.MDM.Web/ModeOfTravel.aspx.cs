using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class ModeOfTravel : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        ConveyanceBAL cv;
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
            lblMsg.Text = string.Empty;
        }
        protected void grdMode_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMode.PageIndex = e.NewPageIndex;
            grdMode.EditIndex = -1;
            BindGrid();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                cv = new ConveyanceBAL();
                cv.ClientId = ClientId;
                cv.ModeOfTravel = txtMode.Text.Trim();
                cv.ConveyanceAmt = Convert.ToDouble(txtConveyanceAmt.Text.Trim());
                cv.CreatedBy = UserId.ToString();
                int res = cv.IU_ModeOfTravel();
                if (res > 0)
                {
                    lblMsg.Text = "Inserted Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Rest();
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Already Exists!!!";
                    Rest();
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    BindGrid();
                }
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            catch (Exception)
            {

            }
        }
        protected void BindGrid()
        {
            cv = new ConveyanceBAL();
            cv.ClientId = ClientId;
            grdMode.DataSource = cv.GetModeOfTravelDtls();
            grdMode.DataBind();
        }
        protected void grdMode_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdMode.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdMode_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow gvr = grdMode.Rows[e.RowIndex];
                Label lbl = ((Label)gvr.FindControl("lblId"));
                lblalerturlid.Text = lbl.Text.Trim();
                grdMode.EditIndex = -1;
                mpdelete.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void grdMode_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdMode.Rows[e.RowIndex];
            try
            {
                cv = new ConveyanceBAL();
                cv.ModeId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                cv.ModeOfTravel = ((TextBox)gvr.FindControl("txtModeOFTravel")).Text.Trim();
                cv.ConveyanceAmt = Convert.ToDouble(((TextBox)gvr.FindControl("txtConveyanceAmount")).Text.Trim());
                cv.ClientId = ClientId;
                cv.CreatedBy = UserId.ToString();
                int res = cv.IU_ModeOfTravel();
                if (res > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdMode.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Already Exists!!!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    grdMode.EditIndex = -1;
                    BindGrid();
                }
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            catch (Exception)
            {

            }
        }
        protected void grdMode_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdMode.EditIndex = -1;
            BindGrid();
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            cv = new ConveyanceBAL();
            cv.ModeId = Convert.ToInt32(lblalerturlid.Text);
            cv.CreatedBy = UserId.ToString();
            cv.UpdationDate = GetCurrentDateTimeByUserId().ToString();
            if (cv.DeleteModeOfTravel() > 0)
            {
                lblMsg.Text = "Deleted successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Not deleted ";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalerturlid.Text = "";
        }

        protected void rest_Click(object sender, EventArgs e)
        {
            Rest();
        }
        private void Rest()
        {
            txtMode.Text = "";
            txtConveyanceAmt.Text = "";
        }
    }
}