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
    public partial class WebCategory : Base
    {
        int ClientId, UserId, RoleId, DeptId;

        VikramSearch srch;
        WebsiteLogsBAL web, keybal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (RoleId == 1)
            {
                addappmaster.Visible = true;
            }
            else
            {
                addappmaster.Visible = false;
            }
            if (RoleId == 4)
            {
                Response.Redirect("userDashBoard.aspx");
            }
            if (!IsPostBack)
            {
                BindGrid();
                pnladdappMas.Visible = false;
            }
        }
        protected void BindGrid()
        {
            srch = new VikramSearch();
            GridFeature.DataSource = srch.SerchWebCategoryEnable(ClientId, txtGrpName.Text);
            GridFeature.DataBind();
            if (RoleId == 1)
            {
                GridFeature.Columns[5].Visible = true;
                GridFeature.Columns[4].Visible = false;
            }
            else
            {
                GridFeature.Columns[4].Visible = true;
                GridFeature.Columns[5].Visible = false;
            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void lnkPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkPopUp = sender as LinkButton;
                GridViewRow gvr = lnkPopUp.NamingContainer as GridViewRow;
                Label lbl = ((Label)GridFeature.Rows[gvr.RowIndex].FindControl("lblId"));
                Label lbl1 = ((Label)GridFeature.Rows[gvr.RowIndex].FindControl("lblCategoryName"));
                lblGrpId.Text = lbl.Text;
                lblGroupName.Text = lbl1.Text;
                BindRemoveSelectedGrid(Convert.ToInt32(lbl.Text));
                BindAddSelectedGrid(Convert.ToInt32(lbl.Text));
                lblPopMsg.Text = string.Empty;
                txtRemoveSearch.Text = txtSearch.Text = string.Empty;
                mp.Show();
            }
            catch (Exception)
            {

            }
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
                        lblMsg.Text = "Category saved successfully.";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Reset();
                        BindGrid();

                    }
                    else
                    {
                        lblMsg.Text = "Category already exists.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;

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
            Reset();
        }
        protected void GridFeature_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridFeature.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void GridFeature_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridFeature.EditIndex = -1;
            BindGrid();
        }
        protected void GridFeature_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = GridFeature.Rows[e.RowIndex];
            try
            {
                keybal = new WebsiteLogsBAL();
                keybal.ClientId = ClientId;
                keybal.CategoryId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                keybal.CategoryCode = ((TextBox)gvr.FindControl("txtEGrpCode")).Text.Trim();
                keybal.CategoryName = ((TextBox)gvr.FindControl("txtEGrpName")).Text.Trim();
                keybal.CategoryDesc = ((TextBox)gvr.FindControl("txtEDesc")).Text.Trim();
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
        protected void GridFeature_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = GridFeature.Rows[e.RowIndex];
            try
            {
                keybal = new WebsiteLogsBAL();
                keybal.ClientId = ClientId;
                keybal.CategoryId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                keybal.CategoryCode = ((TextBox)gvr.FindControl("txtEGrpCode")).Text.Trim();
                keybal.CategoryName = ((TextBox)gvr.FindControl("txtEGrpName")).Text.Trim();
                keybal.CategoryDesc = ((TextBox)gvr.FindControl("txtEDesc")).Text.Trim();
                keybal.LoggedBy = UserId.ToString();
                keybal.Status = 0;
                int res = keybal.IU_WebCategory();
                if (res > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    GridFeature.EditIndex = -1;
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
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void BindAddSelectedGrid(int GroupId)
        {
            srch = new VikramSearch();
            grdaddselected.DataSource = srch.GetWebsiteByClientIdNotAssigned(GroupId, ClientId, txtSearch.Text);
            grdaddselected.DataBind();
        }
        protected void BindRemoveSelectedGrid(int GroupId)
        {
            srch = new VikramSearch();
            grdremoveselected.DataSource = srch.GetWebsiteByClientIdAssigned(GroupId, ClientId, txtRemoveSearch.Text);
            grdremoveselected.DataBind();
        }
        protected void btnaddselected_Click(object sender, EventArgs e)
        {
            try
            {
                string ApplicationIdList = "";
                for (int idx = 0; idx < grdaddselected.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdaddselected.Rows[idx].FindControl("AchkRow_Parents"))).Checked)
                    {
                        ApplicationIdList = ApplicationIdList + ((Label)grdaddselected.Rows[idx].FindControl("lblAId")).Text + "~$";
                    }
                }
                if (ApplicationIdList != "")
                {
                    web = new WebsiteLogsBAL();
                    web.CategoryId = Convert.ToInt32(lblGrpId.Text);
                    web.UrlIdList = ApplicationIdList;
                    web.LoggedBy = UserId.ToString();
                    web.ClientId = ClientId;
                    if (web.Assigncategory() == 1)
                    {
                        lblPopMsg.Text = "Category assigned successfully to selected Websites.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Green;
                        BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        mp.Show();
                    }
                    else
                    {
                        lblPopMsg.Text = "Category not assigned to selected Websites.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Red;
                        mp.Show();
                    }


                }
                else
                {
                    lblPopMsg.Text = "Please select Websites";
                    lblPopMsg.ForeColor = System.Drawing.Color.Red;
                    mp.Show();
                }

            }
            catch (Exception)
            {
            }
        }
        protected void btnremoveselected_Click(object sender, EventArgs e)
        {
            try
            {
                string ApplicationIdList = "";
                for (int idx = 0; idx < grdremoveselected.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdremoveselected.Rows[idx].FindControl("RachkRow_Parents"))).Checked)
                    {
                        ApplicationIdList = ApplicationIdList + ((Label)grdremoveselected.Rows[idx].FindControl("RlblAId")).Text + "~$";
                    }
                }
                if (ApplicationIdList != "")
                {
                    web = new WebsiteLogsBAL();
                    web.CategoryId = 0;
                    web.UrlIdList = ApplicationIdList;
                    web.LoggedBy = UserId.ToString();
                    //if (RoleId != 1)
                    //{
                    web.ClientId = ClientId;
                    //}
                    //else
                    //{
                    //    app.ClientId = 0;
                    //}
                    if (web.UnAssignCategory() == 1)
                    {
                        lblPopMsg.Text = "Category unassigned successfully to selected Websites.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Green;
                        BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        mp.Show();
                    }
                    else
                    {
                        lblPopMsg.Text = "Category not unassigned to selected Websites.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Red;
                        mp.Show();
                    }


                }
                else
                {
                    lblPopMsg.Text = "Please select Websites";
                    lblPopMsg.ForeColor = System.Drawing.Color.Red;
                    mp.Show();
                }


            }
            catch (Exception)
            {
            }
        }
        protected void AchkHeader_Parents_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdaddselected.HeaderRow.FindControl("AchkHeader_Parents");
            foreach (GridViewRow row in grdaddselected.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("AchkRow_Parents");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
            mp.Show();
        }
        protected void RachkHeader_Parents_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdremoveselected.HeaderRow.FindControl("RachkHeader_Parents");
            foreach (GridViewRow row in grdremoveselected.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("RachkRow_Parents");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
            mp.Show();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text.Trim()));
            mp.Show();
        }
        protected void btnRemoveSearch_Click(object sender, EventArgs e)
        {
            BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text.Trim()));
            mp.Show();
        }
    }
}