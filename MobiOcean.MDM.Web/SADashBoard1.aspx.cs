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
    public partial class SADashBoard1 : Base
    {
        usrBAL user;
        GingerboxSrch srch;
        int ClientId, RoleId, UserId;


        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            lblmsg.Text = string.Empty;

            if (!IsPostBack)
            {
                if (RoleId == 1)
                {
                    BindClientDDL();
                    BindRoleId();
                    BindGrid();
                }

            }
        }

        protected void BindGrid()
        {
            srch = new GingerboxSrch();
            dt = new DataTable();
            dt = srch.SuperAdminUserDetails(dtClientId.SelectedValue.ToString(), dtRoleId.SelectedValue.ToString(), txtSrchUserName.Text, txtSrchEmailId.Text);
            grdUsr.DataSource = dt;
            grdUsr.DataBind();
        }
        private void BindClientDDL()
        {
            try
            {
                user = new usrBAL();
                System.Web.UI.WebControls.ListItem li3 = new System.Web.UI.WebControls.ListItem("All", "0");
                dtClientId.Items.Clear();
                dtClientId.Items.Add(li3);
                user.ClientId = ClientId;
                dtClientId.DataSource = user.GetClientName();
                dtClientId.DataTextField = "ClientName";
                dtClientId.DataValueField = "ClientId";
                dtClientId.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                user = null;

            }
        }
        private void BindRoleId()
        {
            try
            {
                user = new usrBAL();
                System.Web.UI.WebControls.ListItem li3 = new System.Web.UI.WebControls.ListItem("All", "0");
                dtRoleId.Items.Clear();
                dtRoleId.Items.Add(li3);
                user.ClientId = ClientId;
                dtRoleId.DataSource = user.GetRoleName();
                dtRoleId.DataTextField = "RoleName";
                dtRoleId.DataValueField = "RoleId";
                dtRoleId.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                user = null;

            }
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
            //txtSrchUserName.Text = string.Empty;
            //txtSrchUserCode.Text = string.Empty;
            //txtSrchMobileNo.Text = string.Empty;
            txtSrchEmailId.Text = string.Empty;
            dtClientId.ClearSelection();
            dtRoleId.ClearSelection();
        }




        protected void btnSrch3_Click(object sender, EventArgs e)
        {
            BindGrid();
            txtSrchUserName.Text = string.Empty;
            //txtSrchUserCode.Text = string.Empty;
            //txtSrchMobileNo.Text = string.Empty;
            //txtSrchEmailId.Text = string.Empty;
            dtRoleId.ClearSelection();
            dtClientId.ClearSelection();
        }


        protected void dtClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
            txtSrchUserName.Text = string.Empty;
            //txtSrchUserCode.Text = string.Empty;
            //txtSrchMobileNo.Text = string.Empty;
            dtRoleId.ClearSelection();
            txtSrchEmailId.Text = string.Empty;
        }
        protected void dtRoleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
            txtSrchUserName.Text = string.Empty;
            //txtSrchUserCode.Text = string.Empty;
            //txtSrchMobileNo.Text = string.Empty;
            dtClientId.ClearSelection();
            txtSrchEmailId.Text = string.Empty;
        }
        protected void grdUsr_RowEditing(object sender, GridViewEditEventArgs e)
        {

            grdUsr.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void grdUsr_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdUsr.Rows[e.RowIndex];
            try
            {
                user = new usrBAL();
                user.UserId = Convert.ToInt32(((Label)gvr.FindControl("lblUId")).Text.Trim());
                user.UserCode = ((TextBox)gvr.FindControl("txtUserCode")).Text.Trim();
                user.UserName = ((TextBox)gvr.FindControl("txtUserName")).Text.Trim();
                user.MobileNo = ((TextBox)gvr.FindControl("txtMobileNo")).Text.Trim();
                user.EmailId = ((TextBox)gvr.FindControl("txtEmailId")).Text.Trim();
                string res = "0";// user.Insertuser();
                if (int.Parse(res) > 0)
                {
                    lblmsg.Text = "Updated Successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    grdUsr.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblmsg.Text = "UserCode or EmailId Already exists";
                    lblmsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
            }
            finally
            {
                user = null;
            }
        }

        protected void grdUsr_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdUsr.EditIndex = -1;
            BindGrid();
        }

        protected void grdUsr_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdUsr.Rows[e.RowIndex];
            try
            {
                //lblkeyid.Text = ((Label)gvr.FindControl("lblUId")).Text.Trim();
                //lblroleid.Text = ((Label)gvr.FindControl("lblRole")).Text.Trim();
                //mpdelete.Show();
            }
            catch (Exception)
            {
            }
        }
        protected void grdUsr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUsr.PageIndex = e.NewPageIndex;
            BindGrid();

        }
        protected void grdUsr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    LinkButton lnkbtnAttachments = (LinkButton)e.Row.FindControl("lbtnactivate");
            //    if (lnkbtnAttachments.Text == "Active")
            //    {
            //        lnkbtnAttachments.ForeColor = System.Drawing.Color.Green;
            //    }
            //    else
            //    {
            //        lnkbtnAttachments.ForeColor = System.Drawing.Color.Red;
            //    }

            //}
        }

    }
}