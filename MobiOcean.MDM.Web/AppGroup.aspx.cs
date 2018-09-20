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
    public partial class AppGroup : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AnuSearch srch;
        AppBAL app;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (RoleId == 1)
            {
                //btnAddGroup.Visible = true;
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
            srch = new AnuSearch();
            grdAppGrp.DataSource = srch.SrchAppGrp(txtGrpCode.Text, txtGrpName.Text);
            grdAppGrp.DataBind();
            if (RoleId == 1)
            {
                grdAppGrp.Columns[4].Visible = true;
                grdAppGrp.Columns[3].Visible = false;
            }
            else
            {
                grdAppGrp.Columns[3].Visible = true;
                grdAppGrp.Columns[4].Visible = false;
            }
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdAppGrp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAppGrp.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdAppGrp_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                app = new AppBAL();
                GridViewRow gvr = grdAppGrp.Rows[e.RowIndex];
                app.AppGroupId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                app.AppGroupCode = ((TextBox)gvr.FindControl("txtGrpCode")).Text.Trim();
                app.AppGroupName = ((TextBox)gvr.FindControl("txtGrpName")).Text.Trim();

                string res = app.spAppGrpDtls();
                if (int.Parse(res) > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdAppGrp.EditIndex = -1;
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
        }
        protected void grdAppGrp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAppGrp.EditIndex = -1;
            BindGrid();
        }
        protected void grdAppGrp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            app = new AppBAL();
            GridViewRow gvr = grdAppGrp.Rows[e.RowIndex];
            try
            {
                app.AppGroupId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                if (app.DeleteAppGrpDtls() > 0)
                {
                    lblMsg.Text = "Deleted Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Not deleted";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
            }
        }
        protected void grdAppGrp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAppGrp.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void lnkbtnManageApp_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtnManageApp = sender as LinkButton;
                GridViewRow gvr = lnkbtnManageApp.NamingContainer as GridViewRow;
                Label lbl = ((Label)grdAppGrp.Rows[gvr.RowIndex].FindControl("lblId"));
                Label lbl1 = ((Label)grdAppGrp.Rows[gvr.RowIndex].FindControl("lblGrpName"));
                lblGrpId.Text = lbl.Text;
                lblGroupName.Text = lbl1.Text;
                BindRemoveSelectedGrid(Convert.ToInt32(lbl.Text));
                BindAddSelectedGrid(Convert.ToInt32(lbl.Text));
                lblPopMsg.Text = string.Empty;
                txtSearch.Text = txtRemoveSearch.Text = string.Empty;
                mp.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void BindAddSelectedGrid(int GroupId)
        {
            srch = new AnuSearch();            
            grdaddselected.DataSource = srch.GetAppNameByClientIdNotAssigned(GroupId, ClientId, txtSearch.Text);           
            grdaddselected.DataBind();
        }
        protected void BindRemoveSelectedGrid(int GroupId)
        {
            srch = new AnuSearch();           
            grdremoveselected.DataSource = srch.GetAppNameByClientIdAssigned(GroupId, ClientId, txtRemoveSearch.Text);           
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
                        ApplicationIdList = ApplicationIdList + ((Label)grdaddselected.Rows[idx].FindControl("AlblAppName")).Text + "~$";
                    }
                }
                if (ApplicationIdList != "")
                {
                    app = new AppBAL();
                    app.AppGroupId = Convert.ToInt32(lblGrpId.Text);
                    app.AppGroupName = lblGroupName.Text;
                    app.ApplicationIdList = ApplicationIdList;
                    app.LoggedBy = UserId.ToString();
                    app.ClientId = ClientId;
                    if (app.AssignGrpName() == 1)
                    {
                        lblPopMsg.Text = "Group assigned successfully to selected App.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Green;
                        BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        mp.Show();
                    }
                    else
                    {
                        lblPopMsg.Text = "Group not assigned to selected App.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Red;
                        mp.Show();
                    }


                }
                else
                {
                    lblPopMsg.Text = "Please select App";
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
                        ApplicationIdList = ApplicationIdList + ((Label)grdremoveselected.Rows[idx].FindControl("RlblAppName")).Text + "~$";
                    }
                }
                if (ApplicationIdList != "")
                {
                    app = new AppBAL();
                    app.AppGroupId = 0;
                    app.ApplicationIdList = ApplicationIdList;
                    app.LoggedBy = UserId.ToString();
                    //if (RoleId != 1)
                    //{
                    app.ClientId = ClientId;
                    //}
                    //else
                    //{
                    //    app.ClientId = 0;
                    //}
                    if (app.UnAssignGrpName() == 1)
                    {
                        lblPopMsg.Text = "Group unassigned successfully to selected App.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Green;
                        BindAddSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        BindRemoveSelectedGrid(Convert.ToInt32(lblGrpId.Text));
                        mp.Show();
                    }
                    else
                    {
                        lblPopMsg.Text = "Group not unassigned to selected App.";
                        lblPopMsg.ForeColor = System.Drawing.Color.Red;
                        mp.Show();
                    }


                }
                else
                {
                    lblPopMsg.Text = "Please select App";
                    lblPopMsg.ForeColor = System.Drawing.Color.Red;
                    mp.Show();
                }


            }
            catch (Exception)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkValidations())
                {
                    app = new AppBAL();
                    app.AppGroupCode = txtGrpCode1.Text.Trim();
                    app.AppGroupName = txtGrpName1.Text.Trim();

                    string res = app.spAppGrpDtls();
                    if (int.Parse(res) > 0)
                    {
                        lblMsg.Text = "Group Details Saved Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        pnladdappMas.Visible = false;
                        BindGrid();
                    }
                    else
                    {
                        lblMsg.Text = "Group Details Already exists";
                        lblMsg.ForeColor = System.Drawing.Color.Red;

                    }
                }
                else
                {
                    lblMsg.Text = "Please Fill Mandatory Fields";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
            }
        }
        protected bool ChkValidations()
        {
            if (txtGrpCode1.Text.Trim() == "" || txtGrpName1.Text.Trim() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
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