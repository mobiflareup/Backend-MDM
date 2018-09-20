using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class ProfileUserMappingHstry : Base
    {
        ProfileBAL profile;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void BindGrid()
        {
            profile = new ProfileBAL();
            grdProfileusrmpng.DataSource = profile.GetProfileUsrMappingHstry();
            grdProfileusrmpng.DataBind();
        }
        protected void grdProfileusrmpng_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //grdRole.PageIndex = e.NewPageIndex;
            //BindGrid();
        }
        protected void grdProfileusrmpng_RowEditing(object sender, GridViewEditEventArgs e)
        {

            //grdRole.EditIndex = e.NewEditIndex;
            //BindGrid();
        }

        protected void grdProfileusrmpng_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            //GridViewRow gvr = grdRole.Rows[e.RowIndex];
            //try
            //{
            //    rolebal = new RoleBAL();
            //    rolebal.RoleId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());

            //    rolebal.RoleCode = ((TextBox)gvr.FindControl("txtRoleCode")).Text.Trim();
            //    rolebal.RoleName = ((TextBox)gvr.FindControl("txtRoleName")).Text.Trim();


            //    string res = rolebal.InsertRole();
            //    if (int.Parse(res) > 0)
            //    {

            //        grdRole.EditIndex = -1;
            //        BindGrid();
            //    }
            //    else
            //    {

            //        BindGrid();

            //    }
            //}
            //finally
            //{
            //    rolebal = null;
            //}
        }
        protected void grdProfileusrmpng_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            //grdRole.EditIndex = -1;
            //BindGrid();
        }
        protected void grdProfileusrmpng_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //    rolebal = new RoleBAL();
            //    GridViewRow gvr = grdRole.Rows[e.RowIndex];
            //    try
            //    {
            //        rolebal.RoleId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
            //        rolebal.DeleteRoleType();

            //        BindGrid();
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //    finally
            //    {
            //        rolebal = null;
            //    }
            //}
        }
    }
}