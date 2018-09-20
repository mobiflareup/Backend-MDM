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
    public partial class AppMaster : Base
    {
        VikramSearch Srch;
        GroupBAL grpbal;
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
                BindGroupDDL(ddlSrchGroup, 1);
                BindGroupDDL(ddlGroupName, 0);
                Reset();
                BindGrid();
            }
        }
        protected void Reset()
        {
            ddlGroupName.SelectedIndex = 0;
            txtApplicationName.Text = string.Empty;
            txtApplicationCode.Text = string.Empty;
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            grdAppMaster.PageIndex = 0;
            grdAppMaster.EditIndex = -1;
            BindGrid();
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {

                grpbal = new GroupBAL();
                //grpbal.ClientId = ClientId;
                if (ChkValidation())
                {
                    grpbal.GroupName = ddlGroupName.SelectedItem.Text.Trim();
                    grpbal.GroupId = Convert.ToInt32(ddlGroupName.SelectedValue.ToString());
                    grpbal.ClientId = ClientId;
                    grpbal.AppCode = txtApplicationCode.Text.Trim();
                    grpbal.AppName = txtApplicationName.Text.Trim();
                    grpbal.LoggedBy = UserId.ToString();
                    int result = grpbal.iu_AppMster();
                    //if (int.Parse(result) > 0)
                    if (result > 0)
                    {
                        Reset();
                        lblMsg.Text = "Application Added successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        // Response.Redirect("AppMaster.aspx");
                        BindGrid();
                    }
                    else
                    {
                        lblMsg.Text = "Application already exists!";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "Fill all mandatory fields!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

            catch (Exception) { }
            finally
            {
                grpbal = null;
            }
        }
        protected bool ChkValidation()
        {
            if (txtApplicationCode.Text.Trim() == "" || txtApplicationName.Text.Trim() == "" || ddlGroupName.SelectedIndex <= 0)
                return false;
            else
                return true;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AppMaster.aspx");
            //Server.Transfer("AppMaster.aspx", true);
        }
        protected void BindGrid()
        {
            Srch = new VikramSearch();
            grdAppMaster.DataSource = Srch.srchAppMstrDtls(ClientId, ddlSrchGroup.SelectedValue.ToString(), txtSrchApplication.Text.Trim());
            grdAppMaster.DataBind();
        }
        protected void BindGroupDDL(DropDownList ddl, int IsSearch)
        {
            ListItem ls = new ListItem("Select", "0");
            ListItem lsall = new ListItem("All", "0");
            try
            {
                grpbal = new GroupBAL();
                ddl.Items.Clear();
                if (IsSearch == 1)
                {
                    ddl.Items.Add(lsall);
                }
                else
                {
                    ddl.Items.Add(ls);
                }
                ddl.DataSource = grpbal.GetAppGrpNameForDDL();
                ddl.DataTextField = "AppGroupName";
                ddl.DataValueField = "AppGroupId";
                ddl.DataBind();
            }
            catch (Exception) { }
            finally
            {
                ls = null;
                grpbal = null;
            }
        }
        protected void grdAppMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAppMaster.PageIndex = e.NewPageIndex;
            grdAppMaster.EditIndex = -1;
            BindGrid();
        }
        protected void grdAppMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAppMaster.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdAppMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdAppMaster.Rows[e.RowIndex];
            try
            {
                if (((DropDownList)gvr.FindControl("EddlGroupName")).SelectedIndex > 0)
                {
                    grpbal = new GroupBAL();
                    dt = new DataTable();
                    grpbal.ClientId = ClientId;
                    grpbal.ApplicationId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                    grpbal.GroupName = ((DropDownList)gvr.FindControl("EddlGroupName")).SelectedItem.Text.Trim();
                    grpbal.GroupId = Convert.ToInt32(((DropDownList)gvr.FindControl("EddlGroupName")).SelectedValue.ToString());
                    grpbal.LoggedBy = UserId.ToString();
                    int res = grpbal.iu_AppMster();
                    //if (int.Parse(res) > 0)
                    if (res > 0)
                    {
                        lblMsg.Text = "Updated Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                        grdAppMaster.EditIndex = -1;
                        BindGrid();
                    }
                    else
                    {
                        lblMsg.Text = "Details already exists";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                    }
                }
                else
                {
                    lblMsg.Text = "Please select application group.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                }
            }
            finally
            {
                grpbal = null;
            }
        }
        protected void grdAppMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            grdAppMaster.EditIndex = -1;
            BindGrid();
        }
        protected void grdAppMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            grpbal = new GroupBAL();
            GridViewRow gvr = grdAppMaster.Rows[e.RowIndex];
            try
            {
                grdAppMaster.EditIndex = -1;
                grpbal.LoggedBy = UserId.ToString();
                lblalertapplnid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                mpdelete.Show();
            }
            catch (Exception)
            {
            }
            finally
            {
                grpbal = null;
            }
        }
        protected void grdAppMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ListItem ls = new ListItem("Select", "0");
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{

                //    GridViewRow gvrow = ((GridViewRow)e.Row);

                //    LinkButton lnkBtnDelete = gvrow.FindControl("DeleteButton") as LinkButton;

                //    lnkBtnDelete.OnClientClick = "popup(this,\'" + lnkBtnDelete.UniqueID + "\');";

                //}
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList EddlGroupName = (DropDownList)e.Row.FindControl("EddlGroupName");
                    Label ElblGroupName = (Label)e.Row.FindControl("ElblGroupName");
                    BindGroupDDL(EddlGroupName, 0);
                    //grpbal = new GroupBAL();                
                    //EddlGroupName.Items.Clear();
                    //EddlGroupName.Items.Add(ls);
                    //EddlGroupName.DataSource = grpbal.GetAppGrpNameForDDL();
                    //EddlGroupName.DataTextField = "AppGroupName";
                    //EddlGroupName.DataValueField = "AppGroupId";
                    //EddlGroupName.DataBind();
                    EddlGroupName.SelectedValue = ElblGroupName.Text;
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "Something Went Wrong!";
            }
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            grpbal = new GroupBAL();
            grpbal.ApplicationId = Convert.ToInt32(lblalertapplnid.Text);
            if (grpbal.DeleteAppMster() == 1)
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
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalertapplnid.Text = "";
        }
    }
}