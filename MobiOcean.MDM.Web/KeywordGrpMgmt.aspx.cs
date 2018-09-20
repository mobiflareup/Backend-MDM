using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class KeywordGrpMgmt : Base
    {
        KeywordBAL keywrdbal;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            try
            {
                keywrdbal = new KeywordBAL();
                grdRole.DataSource = keywrdbal.getdata();
                grdRole.DataBind();
            }

            catch (Exception)
            {

            }
            finally
            {
                keywrdbal = null;
            }
        }
        //#region--------- Grid Events ---------
        //protected void grdRole_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{

        //    grdRole.EditIndex = -1;
        //    BindGrid();
        //}

        //protected void grdRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{

        //    GridViewRow gvr = grdRole.Rows[e.RowIndex];
        //    try
        //    {
        //        keywrdbal = new KeywordBAL();
        //        keywrdbal.GroupId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
        //        //keywrdbal.Status = 1;

        //        if (keywrdbal.ChangeKeyStatus() == 1)
        //        {

        //            BindGrid();
        //        }
        //        else
        //        {
        //            keywrdbal = null;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    finally
        //    {
        //        keywrdbal = null;
        //    }
        //}

        //protected void grdRole_RowEditing(object sender, GridViewEditEventArgs e)
        //{

        //    grdRole.EditIndex = e.NewEditIndex;
        //    BindGrid();
        //}

        //protected void grdRole_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{


        //    GridViewRow gvr = grdRole.Rows[e.RowIndex];
        //    try
        //    {
        //        keywrdbal = new KeywordBAL();
        //        keywrdbal.GroupId = Convert.ToInt32(((Label)gvr.FindControl("lblEId")).Text.Trim());

        //        keywrdbal.GroupName = ((TextBox)gvr.FindControl("txtGroupName")).Text.Trim();
        //        keywrdbal.KeywordName = ((TextBox)gvr.FindControl("txtKeywordName")).Text.Trim();

        //        keywrdbal.Description = ((TextBox)gvr.FindControl("txtDescription")).Text.Trim();

        //        if (keywrdbal.IU_keywrd() == 1)
        //        {

        //            grdRole.EditIndex = -1;
        //            BindGrid();
        //        }
        //        else
        //        {
        //            keywrdbal = null;
        //        }
        //    }

        //    catch (Exception )
        //    {

        //    }
        //    finally
        //    {
        //        keywrdbal = null;
        //    }
        //}



        //protected void gdv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{

        //    grdRole.PageIndex = e.NewPageIndex;
        //    BindGrid();
        //}
        //#endregion
    }
}