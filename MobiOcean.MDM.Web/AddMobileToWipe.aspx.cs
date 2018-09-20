
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
    public partial class AddMobileToWipe : Base
    {
        UserDeviceBAL udbl;
        WipePhoneBAL wipeBAL;
        int ClientId, RoleId, UserId;
        DataTable dt, dtemp, dtstu;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                BindUserDeviceDDL(ddlUserName);
                BindGrid();
            }
        }
        protected void BindUserDeviceDDL(DropDownList ddl)
        {
            try
            {
                ddlUserName.Items.Clear();
                udbl = new UserDeviceBAL();
                //ListItem li = new ListItem("--Select User--", "0");
                ddlUserName.Items.Clear();
                //ddlUserName.Items.Add(li);
                udbl.ClientId = ClientId;
                udbl.UserId = UserId;
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = udbl.GetUNameDDL();

                }
                else
                {
                    ddlUserName.DataSource = udbl.GetUserNameByRpntMngr();
                }
                ddlUserName.DataTextField = "UserName";
                ddlUserName.DataValueField = "UserId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                udbl = null;
            }
        }
        protected void BindGrid()
        {
            try
            {
                wipeBAL = new WipePhoneBAL();
                wipeBAL.UserId = Convert.ToInt32(ddlUserName.SelectedValue.ToString());
                dt = wipeBAL.GetWipeRequester();
                ViewState["Sos"] = dt;
                dtemp = dt;
                grdAddMobile.DataSource = dt;
                grdAddMobile.DataBind();
            }
            catch (Exception)
            {
                lblMsg.Text = "Error";
            }
            finally
            {
                wipeBAL = null;
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    DataTable dtemp;
                    dtemp = new DataTable();
                    dtemp = (DataTable)ViewState["Sos"];
                    wipeBAL = new WipePhoneBAL();
                    dtemp.Columns["PersonNo"].Unique = true;
                    dtemp.Rows.Add(txtRequestorName.Text.Trim(), txtMobileNo.Text.Trim(), 0);
                    grdAddMobile.DataSource = dtemp;
                    grdAddMobile.DataBind();
                    ViewState["Sos"] = dtemp;
                    CleartextBox();
                }
                catch (Exception)
                {
                    lblMsg.Text = "The Mobile Number Already Exists";
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                }
                finally
                {
                    wipeBAL = null;
                }
            }
        }
        protected void CleartextBox()
        {

            txtRequestorName.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
        }
        protected void grdAddMobile_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void grdAddMobile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                wipeBAL = new WipePhoneBAL();
                dtemp = new DataTable();
                dtemp = (DataTable)ViewState["Sos"];
                GridViewRow gvr = grdAddMobile.Rows[e.RowIndex];
                Label lblMobileNo = (Label)gvr.FindControl("lblMobileNo");
                wipeBAL.WipeDataId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                wipeBAL.DeleteFromWipe();
                BindGrid();
                DataRow[] row = dtemp.Select("PersonNo='" + lblMobileNo.Text.Trim() + "'");
                foreach (DataRow row1 in row)
                {
                    dtemp.Rows.Remove(row1);
                }
                grdAddMobile.DataSource = dtemp;
                grdAddMobile.DataBind();
                ViewState["Sos"] = dtemp;
            }
            catch (Exception) { }
            finally
            {

            }
        }
        protected void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                dt = new DataTable();
                dt = (DataTable)ViewState["Sos"];
                dt.Columns.Remove("WipeDataId");
                wipeBAL = new WipePhoneBAL();
                wipeBAL.ClientId = ClientId;
                wipeBAL.UserId = Convert.ToInt32(ddlUserName.SelectedValue.ToString());
                wipeBAL.dt = dt;
                wipeBAL.InsertWipeDtl();
                lblMsg.Text = "Added Successfuly";
                lblMsg.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.Visible = true;
            }
        }
        protected DataTable GetUsrdetailBy(int UserId)
        {
            wipeBAL = new WipePhoneBAL();
            dtstu = new DataTable();
            wipeBAL.UserId = UserId;
            dtstu = wipeBAL.GetUsrDetail();
            return dtstu;
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            CleartextBox();
        }
        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}