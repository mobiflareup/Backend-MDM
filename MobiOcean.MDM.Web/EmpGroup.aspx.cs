using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class EmpGroup : Base
    {
        GroupBAL grpbal;
        int ClientId, UserId, RoleId, DeptId;
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
            {
                try
                {
                    grpbal = new GroupBAL();
                    grpbal.ClientId = ClientId;
                    grdEmployeeGrp.DataSource = grpbal.GetGroupDtls();
                    grdEmployeeGrp.DataBind();

                    //if (grdEmployeeGrp.Rows.Count > 0)
                    //{
                    //}
                    //else
                    //{
                    //    addToTable.Class = "disableBtn";
                    //    addToTable.Enabled = false;
                    //}
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message.ToString();
                }
                finally
                {
                    grpbal = null;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            grpbal = new GroupBAL();
            try
            {
                if (txtEmpGrpName.Text == "")
                {
                    lblMsg.Text = "Please enter Group Name";
                }
                else
                {
                    grpbal.ClientId = ClientId;
                    grpbal.GrouppName = txtEmpGrpName.Text.Trim();
                    grpbal.Description = txtDescription.Text.Trim();
                    int Result = Convert.ToInt32(grpbal.InsertGroupDtls());
                    if (Result == 1)
                    {
                        lblMsg.Text = "Saved Successfully";
                        BindGrid();
                        txtEmpGrpName.Text = "";
                        txtDescription.Text = "";
                        Response.Redirect("EmpGroup.aspx");
                    }
                    else
                    {
                        lblMsg.Text = "Already Exists.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
            finally
            {
                grpbal = null;
            }
        }
        protected void grdEmployeeGrp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdEmployeeGrp.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdEmployeeGrp_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdEmployeeGrp.Rows[e.RowIndex];
            try
            {
                grpbal = new GroupBAL();
                grpbal.GroupId = Convert.ToInt32(((Label)gvr.FindControl("lblEId")).Text.Trim());
                grpbal.ClientId = ClientId;
                grpbal.GrouppName = ((TextBox)gvr.FindControl("txtEGpName")).Text.Trim();
                grpbal.Description = ((TextBox)gvr.FindControl("txtEDescription")).Text.Trim();


                int res = grpbal.InsertGroupDtls();
                if (res == 1)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdEmployeeGrp.EditIndex = -1;
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
                grpbal = null;
            }
        }
        protected void grdEmployeeGrp_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            grpbal = new GroupBAL();
            GridViewRow gvr = grdEmployeeGrp.Rows[e.RowIndex];
            try
            {
                grpbal.GroupId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                grpbal.DeleteGroupDtls();
                BindGrid();
            }
            catch (Exception)
            {
            }
            finally
            {
                grpbal = null;
            }
        }
        protected void grdEmployeeGrp_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            grdEmployeeGrp.EditIndex = -1;
            BindGrid();
        }
        protected void grdEmployeeGrp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void grdEmployeeGrp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEmployeeGrp.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void lbAddPrsns_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = sender as LinkButton;
            GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
            string GroupId = grdEmployeeGrp.DataKeys[row.RowIndex].Value.ToString();
            string url = "EmpGroupMgmt.aspx?Id=" + GroupId;
            Response.Redirect(url);
        }
    }
}