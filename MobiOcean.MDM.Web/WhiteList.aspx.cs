using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class WhiteList : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AllowPhNoBAL phno;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
                reset();
            }
        }
        protected void BindGrid()
        {
            phno = new AllowPhNoBAL();
            phno.ClientId = ClientId;
            phno.IsWhiteList = 1;
            grdNo.DataSource = phno.GetAllowedPhNoByClientId();
            grdNo.DataBind();
            grdNo.PageIndex = 0;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ChkValidation())
            {
                if (chkIncoming.Checked || chkOutgoing.Checked || chkSms.Checked)
                {
                    if (txtMobileNo.Text.Length > 2 && txtMobileNo.Text.Length < 16)
                    {
                        phno = new AllowPhNoBAL();
                        phno.ClientId = ClientId;
                        phno.UserId = UserId;
                        phno.Name = txtName.Text.Trim();
                        phno.MobileNo = txtMobileNo.Text.Trim();
                        phno.IsWhiteList = 1;
                        if (chkIncoming.Checked == true)
                        {
                            phno.IsIncoming = 1;
                        }
                        else
                        {
                            phno.IsIncoming = 0;
                        }
                        if (chkOutgoing.Checked == true)
                        {
                            phno.IsOutgoing = 1;
                        }
                        else
                        {
                            phno.IsOutgoing = 0;
                        }
                        if (chkSms.Checked == true)
                        {
                            phno.IsForSms = 1;
                        }
                        else
                        {
                            phno.IsForSms = 0;
                        }
                        int res = phno.IU_AllowedPhNo();
                        if (res > 0)
                        {
                            lblmsg.Text = "Inserted Successfully";
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            BindGrid();
                            reset();
                        }
                        else
                        {
                            lblmsg.Text = "Already Exists!!!";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Only allowed 3-15 Digit Mobile No";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblmsg.Text = "Select checkboxes from Incoming call or outgoing call or SMS for white list!";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblmsg.Text = "Enter name and mobileno!";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            grdNo.EditIndex = -1;
        }
        public void reset()
        {
            txtName.Text = "";
            txtMobileNo.Text = "";
            chkIncoming.Checked = false;
            chkOutgoing.Checked = false;
            chkSms.Checked = false;
        }
        protected bool ChkValidation()
        {
            if (txtName.Text.Trim() == "" || txtMobileNo.Text.Trim() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected bool GetStatus(string str)
        {
            if (str == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void grdNo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdNo.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdNo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdNo.EditIndex = -1;
            BindGrid();
        }
        protected void grdNo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                phno = new AllowPhNoBAL();
                GridViewRow gvr = grdNo.Rows[e.RowIndex];
                CheckBox ChkEIncoming = ((CheckBox)gvr.FindControl("ChkEIncoming"));
                CheckBox ChkEOutgoing = ((CheckBox)gvr.FindControl("ChkEOutgoing"));
                CheckBox ChkESms = ((CheckBox)gvr.FindControl("ChkESms"));
                phno.ClientId = ClientId;
                phno.UserId = UserId;
                phno.Name = ((TextBox)gvr.FindControl("txtEName")).Text.Trim();
                phno.MobileNo = ((Label)gvr.FindControl("lblEMobileNo")).Text.Trim();
                phno.Status = Convert.ToInt32(((Label)gvr.FindControl("lblEStatus")).Text.Trim());
                phno.IsWhiteList = 1;
                if (!string.IsNullOrEmpty(phno.Name) && !string.IsNullOrEmpty(phno.MobileNo))
                {
                    if (ChkEIncoming.Checked || ChkEOutgoing.Checked || ChkESms.Checked)
                    {
                        if (ChkEIncoming.Checked == true)
                        {
                            phno.IsIncoming = 1;
                        }
                        else
                        {
                            phno.IsIncoming = 0;
                        }
                        if (ChkEOutgoing.Checked == true)
                        {
                            phno.IsOutgoing = 1;
                        }
                        else
                        {
                            phno.IsOutgoing = 0;
                        }
                        if (ChkESms.Checked == true)
                        {
                            phno.IsForSms = 1;
                        }
                        else
                        {
                            phno.IsForSms = 0;
                        }
                        int res = phno.IU_AllowedPhNo();
                        if (res >= 0)
                        {
                            lblmsg.Text = "Updated Successfully";
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            grdNo.EditIndex = -1;
                            BindGrid();
                            reset();
                        }
                        else
                        {
                            lblmsg.Text = "Not Updated";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Select for which the mobile no is white list!";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblmsg.Text = "Enter name!";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                lblmsg.Text = "Not Updated";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void grdNo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                grdNo.EditIndex = -1;
                phno = new AllowPhNoBAL();
                GridViewRow gvr = grdNo.Rows[e.RowIndex];
                phno.AllowedPhNoId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                int res = phno.DeleteAllowedPhNo();
                if (res > 0)
                {
                    lblmsg.Text = "Deleted Successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    grdNo.EditIndex = -1;
                    BindGrid();
                    reset();
                }
                else
                {
                    lblmsg.Text = "Not Deleted";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void grdNo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdNo.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            reset();
            grdNo.EditIndex = -1;
        }
    }
}