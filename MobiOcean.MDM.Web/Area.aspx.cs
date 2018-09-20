using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class Area : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        LocationBAL Loc;
        AnuSearch srch;

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
            srch = new AnuSearch();
            grdArea.DataSource = srch.GetArea(txtSrchArea.Text.Trim(), ClientId);
            grdArea.DataBind();
        }


        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void grdArea_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdArea.EditIndex = e.NewEditIndex;
            //BindGrid();
        }
        protected void grdArea_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdArea.EditIndex = -1;
            BindGrid();
        }
        protected void grdArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdArea.Rows[e.RowIndex];
            try
            {
                lblkeyid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                mpdelete.Show();
            }
            catch (Exception)
            {

            }
            finally
            {
                Loc = null;
            }
        }
        protected void grdArea_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }
        protected void grdArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdArea.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void EditButton_click(object sender, EventArgs e)
        {
            LinkButton lnkbtnEdit = sender as LinkButton;
            GridViewRow gvr = lnkbtnEdit.NamingContainer as GridViewRow;
            int areaId = Convert.ToInt32(grdArea.DataKeys[gvr.RowIndex].Value.ToString());
            Session["AreaId"] = areaId.ToString();
            Response.Redirect("EditArea.aspx");
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            Loc = new LocationBAL();
            Loc.AreaId = Convert.ToInt32(lblkeyid.Text);
            if (Loc.DeleteAreaDtls() == 1)
            {
                lblMsg.Text = "Deleted successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Not deleted successfully";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                BindGrid();

            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblkeyid.Text = "";
        }
    }
}