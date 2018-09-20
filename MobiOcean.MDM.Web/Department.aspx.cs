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
    public partial class Department : Base
    {
        DeptBAL dept;
        RamuSearch rsr;
        DataTable dt;
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
                dt = rsr.getDeptMaster(ClientId, "", "");
                ViewState["Dept"] = dt;
                grdDept.DataSource = dt;
                grdDept.DataBind();
            }
            finally
            {
                dt = null;
                rsr = null;
            }
        }
        
        protected void grdDept_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdDept.Rows[e.RowIndex];
            try
            {
                lblkeyid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                lblbname.Text = ((TextBox)gvr.FindControl("txtgdv")).Text.Trim();
                mpdelete.Show();
            }
            catch (Exception)
            {
            }
            finally
            {
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

        }
        protected void grdDept_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdDept.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdDept_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdDept.Rows[e.RowIndex];
            try
            {
                dept = new DeptBAL();
                dept.ClientId = ClientId;
                dept.helpDelete = 0;
                dept.UserId = UserId;
                dept.DeptId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                dept.DeptName = ((TextBox)gvr.FindControl("txtgdv")).Text.Trim();
                int res = dept.insrtAddMaster();
                if (res > 0)
                {
                    lblpopmsg.Text = "Updated Successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    grdDept.EditIndex = -1;
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
        protected void grdDept_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdDept.EditIndex = -1;
            BindGrid();
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            dept = new DeptBAL();
            string brname = ((TextBox)grdDept.FooterRow.FindControl("txtgdvftr")).Text;
            if (brname != "")
            {
                BindGrid();
                dt = new DataTable();
                dt = (DataTable)ViewState["Dept"];
                bool canadd = true;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["DeptName"].ToString() == brname)
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
                    dept.insrtAddMaster();
                    BindGrid();
                    lblpopmsg.Text = "Department added successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblpopmsg.Text = "Already Exists!";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblpopmsg.Text = "Enter Department Name";
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void AddButton1_Click(object sender, EventArgs e)
        {
            dept = new DeptBAL();
            string brname = ((TextBox)grdDept.Controls[0].Controls[0].FindControl("txtgdvftre")).Text.ToString();// as Label;
            if (brname != "")
            {
                BindGrid();
                dt = new DataTable();
                dt = (DataTable)ViewState["Dept"];
                bool canadd = true;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["DeptName"].ToString() == brname)
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
                    dept.insrtAddMaster();
                    BindGrid();
                    lblpopmsg.Text = "Department added successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblpopmsg.Text = "Already Exists!";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblpopmsg.Text = "Enter Department Name";
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            dept = new DeptBAL();
            dept.ClientId = ClientId;
            dept.helpDelete = 1;
            dept.UserId = UserId;
            dept.DeptId = Convert.ToInt32(lblkeyid.Text);
            dept.DeptName = lblbname.Text;
            int res = dept.insrtAddMaster();
            if (res > 0)
            {
                lblpopmsg.Text = "Deleted Successfully";
                lblpopmsg.ForeColor = System.Drawing.Color.Green;
                grdDept.EditIndex = -1;
                BindGrid();
            }
            else
            {
                lblpopmsg.Text = "Not Deleted.";
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
                grdDept.EditIndex = -1;
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