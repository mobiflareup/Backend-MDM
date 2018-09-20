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
    public partial class Branch : Base
    {
        DeptBAL dept;
        RamuSearch rsr;
        DataTable dt;
        protected string Values = "";
        int ClientId, RoleId, UserId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            //lblMsg.Text = string.Empty;
            if (RoleId > 3)
            {
                Response.Redirect("UserDashBoard.aspx");
            }
            if (!IsPostBack)
            {
                BindGrid();
            }
            lblpopmsg.Text = string.Empty;
        }
        protected void BindGrid()
        {
            try
            {
                //Dept dep;
                rsr = new RamuSearch();
                dt = new DataTable();
                dt = rsr.getbranchMaster(ClientId, "", "");
                ViewState["Dept"] = dt;               
                grdBranch.DataSource = dt;
                grdBranch.DataBind();                
            }
            finally
            {
                dt = null;
                rsr = null;
            }
        }
        protected void grdBranch_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdBranch.Rows[e.RowIndex];
            try
            {
                lblkeyid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                lblbname.Text = ((TextBox)gvr.FindControl("txtgdva")).Text.Trim();
                mpdelete.Show();
            }
            catch (Exception)
            {
            }
            finally
            {
                dept = null;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void grdBranch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdBranch.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdBranch_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdBranch.Rows[e.RowIndex];
            try
            {
                dept = new DeptBAL();
                dept.ClientId = ClientId;
                dept.helpDelete = 0;
                dept.UserId = UserId;
                dept.DeptId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                dept.DeptName = ((TextBox)gvr.FindControl("txtgdv")).Text.Trim();
                int res = dept.InsertBranchDtls();
                if (res > 0)
                {
                    lblpopmsg.Text = "Updated Successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    grdBranch.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblpopmsg.Text = "Already Exists!";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
            }
            finally
            {
                dept = null;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void grdBranch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdBranch.EditIndex = -1;
            BindGrid();
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            dept = new DeptBAL();
            string brname = ((TextBox)grdBranch.FooterRow.FindControl("txtgdvftr")).Text;
            if (brname != "")
            {
                BindGrid();
                dt = new DataTable();
                dt = (DataTable)ViewState["Dept"];
                bool canadd = true;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["BranchName"].ToString() == brname)
                    {
                        canadd = false;
                        break;
                    }
                }
                if (canadd)
                {
                    dept.ClientId = ClientId;
                    dept.DeptName = brname;
                    dept.UserId = UserId;
                    dept.InsertBranchDtls();
                    BindGrid();
                    lblpopmsg.Text = "Branch added successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    //txtbranch.Text = string.Empty;
                }
                else
                {
                    lblpopmsg.Text = "Alredy Exists!";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblpopmsg.Text = "Enter Branch Name";
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void AddButton1_Click(object sender, EventArgs e)
        {
            dept = new DeptBAL();
            string brname = ((TextBox)grdBranch.Controls[0].Controls[0].FindControl("txtgdvftre")).Text.ToString();// as Label;
            if (brname != "")
            {
                BindGrid();
                dt = new DataTable();
                dt = (DataTable)ViewState["Dept"];
                bool canadd = true;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["BranchName"].ToString() == brname)
                    {
                        canadd = false;
                        break;
                    }
                }
                if (canadd)
                {
                    dept.ClientId = ClientId;
                    dept.DeptName = brname;
                    dept.UserId = UserId;
                    dept.InsertBranchDtls();
                    BindGrid();
                    lblpopmsg.Text = "Branch added successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    //txtbranch.Text = string.Empty;
                }
                else
                {
                    lblpopmsg.Text = "Alredy Exists!";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblpopmsg.Text = "Enter Branch Name";
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            dept = new DeptBAL();
            dept = new DeptBAL();
            dept.ClientId = ClientId;
            dept.helpDelete = 1;
            dept.UserId = UserId;
            dept.DeptId = Convert.ToInt32(lblkeyid.Text);
            dept.DeptName = lblbname.Text;
            int res = dept.InsertBranchDtls();
            if (res > 0)
            {
                lblpopmsg.Text = "Deleted Successfully";
                lblpopmsg.ForeColor = System.Drawing.Color.Green;
                grdBranch.EditIndex = -1;
                BindGrid();
            }
            else
            {
                lblpopmsg.Text = "Not Deleted.";
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
                grdBranch.EditIndex = -1;
                BindGrid();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblpopmsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblkeyid.Text = "";
        }
    }
}