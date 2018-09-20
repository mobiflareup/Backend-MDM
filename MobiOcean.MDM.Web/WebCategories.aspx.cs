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
    public partial class WebCategories : Base
    {
        WebsiteLogsBAL keybal;
        VikramSearch srch;
        int ClientId, UserId, RoleId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["Role"].ToString());
            lblMsg.Text = string.Empty;

            if (!IsPostBack)
            {
                BindGrid();
                pnladdappMas.Visible = false;
            }

        }
        public void BindGrid()
        {
            srch = new VikramSearch();
            grdKey.DataSource = srch.srchWebCategory(ClientId, txtSrchKCode.Text.Trim(), txtSrchKName.Text.Trim());
            grdKey.DataBind();

        }
        protected void grdKey_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdKey.EditIndex = -1;
            BindGrid();
        }
        protected void grdKey_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdKey.Rows[e.RowIndex];
            try
            {
                keybal = new WebsiteLogsBAL();
                keybal.ClientId = ClientId;
                keybal.CategoryId = Convert.ToInt32(((Label)gvr.FindControl("lblUId")).Text.Trim());
                keybal.CategoryCode = ((TextBox)gvr.FindControl("txtKeywordCode")).Text.Trim();
                keybal.CategoryName = ((TextBox)gvr.FindControl("txtKeywordName")).Text.Trim();
                keybal.CategoryDesc = ((TextBox)gvr.FindControl("txtDescription")).Text.Trim();
                keybal.LoggedBy = UserId.ToString();
                keybal.Status = 1;
                int res = keybal.IU_WebCategory();
                if (res == 1)
                {
                    lblMsg.Text = "Deleted Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Not Deleted";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception)
            {
            }
        }
        protected void grdKey_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdKey.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdKey_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdKey.Rows[e.RowIndex];
            try
            {
                keybal = new WebsiteLogsBAL();
                keybal.ClientId = ClientId;
                keybal.CategoryId = Convert.ToInt32(((Label)gvr.FindControl("lblUId")).Text.Trim());
                keybal.CategoryCode = ((TextBox)gvr.FindControl("txtKeywordCode")).Text.Trim();
                keybal.CategoryName = ((TextBox)gvr.FindControl("txtKeywordName")).Text.Trim();
                keybal.CategoryDesc = ((TextBox)gvr.FindControl("txtDescription")).Text.Trim();
                keybal.LoggedBy = UserId.ToString();
                keybal.Status = 0;
                int res = keybal.IU_WebCategory();
                if (res > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdKey.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Already exists";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
            }
            finally
            {
                keybal = null;
            }
        }
        protected void grdKey_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKey.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKCode.Text.Trim() == "" || txtKName.Text.Trim() == "" || txtKDesc.Text.Trim() == "")
                {
                    lblMsg.Text = "Fill all mandatory fields!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    keybal = new WebsiteLogsBAL();
                    keybal.CategoryId = 0;
                    keybal.ClientId = 0;
                    keybal.CategoryCode = txtKCode.Text.Trim();
                    keybal.CategoryName = txtKName.Text.Trim();
                    keybal.CategoryDesc = txtKDesc.Text.Trim();
                    keybal.Status = 0;
                    keybal.LoggedBy = UserId.ToString();
                    int res = keybal.IU_WebCategory();
                    if (res > 0)
                    {
                        msglbll.Text = "Category saved successfully.";
                        msglbll.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("WebCategories.aspx");

                    }
                    else
                    {
                        msglbll.Text = "Category already exists.";
                        msglbll.ForeColor = System.Drawing.Color.Red;

                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                keybal = null;
            }
        }
        public void Reset()
        {
            txtKName.Text = "";
            txtKCode.Text = "";
            txtKDesc.Text = "";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnladdappMas.Visible = false;
        }
        protected void addappmaster_Click(object sender, EventArgs e)
        {
            if (pnladdappMas.Visible)
            {
                pnladdappMas.Visible = false;
            }
            else
            {
                pnladdappMas.Visible = true;
            }
        }
    }
}