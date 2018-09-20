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
    public partial class RoleMaster : Base
    {
        RoleBAL rolebal;
        RamuSearch rsr;
        DataTable dt;
        int ClientId, RoleId, UserId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                BindGrid();

            }
        }

        protected void BindGrid()
        {
            try
            {

                rsr = new RamuSearch();
                dt = new DataTable();
                dt = rsr.getroalmaster(txtRcode.Text.Trim(), txtRname.Text.Trim());
                grdRole.DataSource = dt;
                grdRole.DataBind();

            }
            finally
            {
                dt = null;
                rsr = null;
            }

        }
        protected void grdRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRole.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void grdRole_RowEditing(object sender, GridViewEditEventArgs e)
        {

            grdRole.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void grdRole_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdRole.Rows[e.RowIndex];
            try
            {
                rolebal = new RoleBAL();
                rolebal.RoleId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());

                rolebal.RoleCode = ((TextBox)gvr.FindControl("txtRoleCode")).Text.Trim();
                rolebal.RoleName = ((TextBox)gvr.FindControl("txtRoleName")).Text.Trim();


                string res = rolebal.InsertRole();
                if (int.Parse(res) > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdRole.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Already exists";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            finally
            {
                rolebal = null;
            }
        }
        protected void grdRole_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            grdRole.EditIndex = -1;
            BindGrid();
        }
        protected void grdRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            rolebal = new RoleBAL();
            GridViewRow gvr = grdRole.Rows[e.RowIndex];
            try
            {
                rolebal.RoleId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                rolebal.DeleteRoleType();
                BindGrid();
            }
            catch (Exception)
            {
            }
            finally
            {
                rolebal = null;
            }
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();

        }
        protected void addToTable_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddRole.aspx");
        }
    }
}