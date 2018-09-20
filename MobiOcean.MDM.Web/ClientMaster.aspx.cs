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
    public partial class ClientMaster : Base
    {
        ClientBAL clientbal;
        int ClientId, UserId, RoleId, DeptId;
        AnuSearch srch;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (Request.QueryString["Res"] != null)
            {
                int res = Convert.ToInt32(Request.QueryString["Res"].ToString());
                if (res > 0)
                {
                    lblMessage.Text = "App Successfully Assigned to Client!!!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Something went wrong!!!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            string ClientCode, ClientName;
            ClientCode = txtSrchCode.Text.Trim();
            ClientName = txtSrchName.Text.Trim();
            try
            {
                srch = new AnuSearch();
                if (RoleId == 1)
                {
                    grdclient.DataSource = srch.SearchClientDtls(ClientCode, ClientName);
                }
                else
                {
                    grdclient.DataSource = null;
                }
                grdclient.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                clientbal = null;
            }
        }
        protected void grdclient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdclient.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void grdclient_RowEditing(object sender, GridViewEditEventArgs e)
        {

            grdclient.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void grdclient_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            GridViewRow gvr = grdclient.Rows[e.RowIndex];
            try
            {
                clientbal = new ClientBAL();
                clientbal.ClientId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());

                clientbal.ClientCode = ((TextBox)gvr.FindControl("txtClientCode")).Text.Trim();
                clientbal.ClientName = ((TextBox)gvr.FindControl("txtClientName")).Text.Trim();
                clientbal.Address = ((TextBox)gvr.FindControl("txtAddress")).Text.Trim();
                clientbal.EmailId = ((TextBox)gvr.FindControl("txtEmailId")).Text.Trim();
                clientbal.ManagerName = ((TextBox)gvr.FindControl("txtManagerName")).Text.Trim();
                clientbal.ManagerContactNo = ((TextBox)gvr.FindControl("txtManagerContactNo")).Text.Trim();


                string res = clientbal.InsertClient();
                if (int.Parse(res) > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdclient.EditIndex = -1;
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
                clientbal = null;
            }
        }
        protected void grdclient_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            grdclient.EditIndex = -1;
            BindGrid();
        }
        protected void grdclient_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            clientbal = new ClientBAL();
            GridViewRow gvr = grdclient.Rows[e.RowIndex];
            try
            {
                clientbal.ClientId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                if (clientbal.DeleteClientDtls() > 0)
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
            finally
            {
                clientbal = null;
            }
        }
        protected void addToTable_Click1(object sender, EventArgs e)
        {
            Response.Redirect("AddClient.aspx");
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdclient_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Assign App")
            {
                int cId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("~/AssignClientCustomApp.aspx?Id=" + cId);
            }
        }
    }
}
