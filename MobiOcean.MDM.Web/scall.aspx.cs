using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class scall : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        DataTable dt;
        AllowPhNoBAL phno;
        UserDeviceBAL usrdevice;
        SendSMSBAL sms;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                ChkPendingChanges();
            }
        }
        protected void ChkPendingChanges()
        {
            try
            {
                //phno = new AllowPhNoBAL();
                //dt = new DataTable();
                //phno.ClientId = ClientId;

                //dt = phno.GetNotSyncedDfltRstWebsites();

                //if (dt.Rows.Count > 0)
                //{
                //    //-------- Some changes are pending ----------
                //    mpUnapldChngs.Show();
                //}
                //else
                //{
                //    //-------- No changes are pending ----------
                BindGrid();
                //}
            }
            catch (Exception) { }
            finally
            {
                //dt = null;
                //phstng = null;
            }
        }
        protected void btnAddNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNo.Text.Trim()))
                {
                    phno = new AllowPhNoBAL();
                    phno.ClientId = ClientId;
                    phno.AllowPhNo = txtNo.Text.Trim();
                    phno.Status = 0;
                    int res = phno.IU_tblAllowedPhNo();
                    if (res > 0)
                    {
                        lblMsg.Text = "Phone No. added successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                        BindGrid();
                        txtNo.Text = string.Empty;
                    }
                    else
                    {
                        lblMsg.Text = "Phone No. not added ";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                    }

                    //dtemp = new DataTable();
                    //dtemp = (DataTable)ViewState["Number"];
                    //dtemp.Columns["AllowPhnNo"].Unique = true;
                    //dtemp.Rows.Add(txtNo.Text.Trim(), 0);
                    //grdNo.DataSource = dtemp;
                    //grdNo.DataBind();
                    //ViewState["Number"] = dtemp;

                }
                else
                {
                    lblMsg.Text = "Please enter Phone No.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "The Phone No. Already Exists";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
        }
        protected void BindGrid()
        {
            phno = new AllowPhNoBAL();
            phno.ClientId = ClientId;
            dt = phno.GetAllowedPhoneNo();
            ViewState["Number"] = dt;
            grdNo.DataSource = dt;
            grdNo.DataBind();
        }
        protected void grdNo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                GridViewRow gvr = grdNo.Rows[e.RowIndex];
                Label lblMobileNo = (Label)gvr.FindControl("lblNo");
                phno = new AllowPhNoBAL();
                phno.ClientId = ClientId;
                phno.AllowPhNo = lblMobileNo.Text.Trim();
                phno.Status = 1;

                if (phno.IU_tblAllowedPhNo() > 0)
                {
                    lblMsg.Text = "Phone No. deleted successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                    BindGrid();
                    //txtNo.Text = string.Empty;
                }
                else
                {
                    lblMsg.Text = "Phone No. not deleted ";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                }



                //dtemp = new DataTable();
                //dtemp = (DataTable)ViewState["Number"];

                //DataRow[] row = dtemp.Select("AllowPhnNo='" + lblMobileNo.Text.Trim() + "'");
                //foreach (DataRow row1 in row)
                //{
                //    dtemp.Rows.Remove(row1);
                //}
                //grdNo.DataSource = dtemp;
                //grdNo.DataBind();
                //ViewState["Sos"] = dtemp;
            }
            catch (Exception) { }
            finally
            {

            }

        }
        protected void btnApplyChanges_Click(object sender, EventArgs e)
        {
            try
            {
                phno = new AllowPhNoBAL();
                phno.ClientId = ClientId;
                phno.UserId = UserId;
                //dt = new DataTable();
                //dt = (DataTable)ViewState["Number"];
                //phno.dt = dt;            
                int res = phno.ApplyAllowedPhoneNoChanges();
                if (res > 0)
                {
                    lblMsg.Text = "Changes applied Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    SendUpdateMsg();
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Changes Not applied";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void SendUpdateMsg()
        {
            usrdevice = new UserDeviceBAL();
            dt = new DataTable();
            usrdevice.ClientId = ClientId;
            dt = usrdevice.GetDeviceWithMDM();
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(row["MobileNo1"].ToString(), "GBox set as WP7", ClientId);
                }
                catch (Exception)
                { }
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                phno = new AllowPhNoBAL();
                phno.ClientId = ClientId;
                phno.UserId = UserId;
                //dt = new DataTable();
                //dt = (DataTable)ViewState["Number"];
                //phno.dt = dt;            
                int res = phno.CancelAllowedPhoneNoChanges();
                if (res > 0)
                {
                    lblMsg.Text = "Changes has canceled";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Something went wrong";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnViewHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllowedPhNoHstry.aspx");
        }
    }
}