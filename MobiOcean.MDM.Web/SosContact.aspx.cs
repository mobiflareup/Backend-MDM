using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class SosContact : Base
    {
        SOSBAL sos;
        DataTable dt;
        protected string Values = "";
        int ClientId, RoleId, UserId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
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
                sos = new SOSBAL();
                sos.UserId = UserId;
                dt = sos.GetSosContactDeetails();
                ViewState["Dept"] = dt;
                grdsoscontact.DataSource = dt;
                grdsoscontact.DataBind();
            }
            finally
            {
                dt = null;

            }
        }

        protected void grdsoscontact_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdsoscontact.Rows[e.RowIndex];
            try
            {
                grdsoscontact.EditIndex = -1;
                sos = new SOSBAL();

                sos.ContactId = Convert.ToInt32(((Label)gvr.FindControl("lblContactId")).Text.Trim());
                int res = sos.DeleteUserSosContact();
                if (res > 0)
                {
                    lblpopmsg.Text = "Deleted Successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    grdsoscontact.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblpopmsg.Text = "Not Deleted.";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                    grdsoscontact.EditIndex = -1;
                    BindGrid();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                sos = null;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

        }
        protected void grdsoscontact_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdsoscontact.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdsoscontact_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdsoscontact.Rows[e.RowIndex];
            try
            {
                sos = new SOSBAL();

                sos.ContactId = Convert.ToInt32(((Label)gvr.FindControl("lblContactId")).Text.Trim());
                sos.UserId = Convert.ToInt32(((Label)gvr.FindControl("lblUserId")).Text.Trim());
                sos.contactNo = ((TextBox)gvr.FindControl("txtgdv")).Text.Trim();
                int res = sos.InsertuserSosContact();
                if (res == 0)
                {
                    lblpopmsg.Text = "Inserted Successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    grdsoscontact.EditIndex = -1;
                    BindGrid();
                }
                else
                    if (res == 1)
                {
                    lblpopmsg.Text = "Updated Successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    grdsoscontact.EditIndex = -1;
                    BindGrid();
                }
                else
                        if (res == 2)
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
                sos = null;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

        }
        protected void grdsoscontact_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdsoscontact.EditIndex = -1;
            BindGrid();
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            string brname = ((TextBox)grdsoscontact.FooterRow.FindControl("txtgdvftr")).Text;
            if (brname != "")
            {
                BindGrid();
                sos = new SOSBAL();

                sos.ContactId = 0;
                sos.UserId = UserId;
                sos.contactNo = brname;
                int res = sos.InsertuserSosContact();
                if (res == 0)
                {
                    lblpopmsg.Text = "Inserted Successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    grdsoscontact.EditIndex = -1;
                    BindGrid();
                }
                else
                    if (res == 1)
                {
                    lblpopmsg.Text = "Updated Successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    grdsoscontact.EditIndex = -1;
                }
                else
                        if (res == 2)
                {
                    lblpopmsg.Text = "Already Exists!";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            else
            {
                lblpopmsg.Text = "Enter Contact No Name";
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void AddButton1_Click(object sender, EventArgs e)
        {
            string brname = ((TextBox)grdsoscontact.Controls[0].Controls[0].FindControl("txtgdvftre")).Text.ToString();// as Label;
            if (brname != "")
            {
                BindGrid();
                sos = new SOSBAL();

                sos.ContactId = 0;
                sos.UserId = UserId;
                sos.contactNo = brname;
                int res = sos.InsertuserSosContact();
                if (res == 0)
                {
                    lblpopmsg.Text = "Inserted Successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    grdsoscontact.EditIndex = -1;
                    BindGrid();
                }
                else
                    if (res == 1)
                {
                    lblpopmsg.Text = "Updated Successfully";
                    lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    grdsoscontact.EditIndex = -1;
                }
                else
                        if (res == 2)
                {
                    lblpopmsg.Text = "Already Exists!";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            else
            {
                lblpopmsg.Text = "Enter Contact No Name";
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
    }
}