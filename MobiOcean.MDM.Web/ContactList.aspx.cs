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
    public partial class ContactList : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        DataTable dt;
        UserDeviceBAL usrdvcbal;
        ContactBAL contact;
        DDLBAL ddlbal;
        AnuSearch srch;
        private SendSMSBAL sms;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindUsrName();
                BindSyncDateTime();
                BindGrid();
            }

        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void BindUsrName()
        {
            try
            {
                ddlbal = new DDLBAL();
                ddlbal.UserId = UserId;
                ddlbal.ClientId = ClientId;
                ddlbal.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(new ListItem("All", "0"));
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = ddlbal.GetUserDeviceByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = ddlbal.GetUsrDeviceByDeptHead();
                }
                else
                {
                    ddlUserName.DataSource = ddlbal.GetUserDeviceByUserId();
                }
                ddlUserName.DataTextField = "DeviceName";
                ddlUserName.DataValueField = "DeviceId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                ddlbal = null;
            }
        }
        protected void BindSyncDateTime()
        {
            try
            {
                contact = new ContactBAL();
                contact.DeviceId = Convert.ToInt32(ddlUserName.SelectedValue.ToString());
                ddlSyncDateTime.Items.Clear();
                //ddlSyncDateTime.Items.Add(new ListItem("All", "0"));
                ddlSyncDateTime.DataSource = contact.GetSyncDateTime();
                ddlSyncDateTime.DataTextField = "LogDateTime";
                ddlSyncDateTime.DataValueField = "LogDateTime";
                ddlSyncDateTime.DataBind();

            }
            catch (Exception)
            {

            }
            finally
            {
                contact = null;
            }
        }
        protected void BindGrid()
        {
            try
            {
                string syncDateTime;
                if (ddlSyncDateTime.Items.Count > 0)
                {
                    syncDateTime = ddlSyncDateTime.SelectedItem.Text.Trim();
                }
                else
                {
                    syncDateTime = string.Empty;
                }

                srch = new AnuSearch();
                if (RoleId == 1 || RoleId == 2)
                {
                    grdContact.DataSource = srch.SrchContactDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), syncDateTime, txtContactName.Text.Trim(), txtMobileNo.Text.Trim(), ddlWhiteList.SelectedValue.ToString());
                }
                else if (RoleId == 3)
                {
                    grdContact.DataSource = srch.SrchContactDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), syncDateTime, txtContactName.Text.Trim(), txtMobileNo.Text.Trim(), ddlWhiteList.SelectedValue.ToString(), DeptId);
                }
                else
                {
                    grdContact.DataSource = srch.SrchContactDtls(ClientId, UserId, ddlUserName.SelectedValue.ToString(), syncDateTime, txtContactName.Text.Trim(), txtMobileNo.Text.Trim(), ddlWhiteList.SelectedValue.ToString());
                }
                grdContact.DataBind();
            }
            catch (Exception)
            {
            }

        }
        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSyncDateTime();
            BindGrid();
        }
        protected void grdContact_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdContact.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdContact_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                contact = new ContactBAL();
                GridViewRow gvr = grdContact.Rows[e.RowIndex];
                contact.Contact_Id = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                contact.IsWhiteList = Convert.ToInt32(((RadioButtonList)gvr.FindControl("rbtnIsWhiteList")).Text.Trim());
                contact.DeviceId = Convert.ToInt32(((Label)gvr.FindControl("lblDeviceId")).Text.Trim());
                contact.MobileNo = ((Label)gvr.FindControl("lblMobileNo")).Text.Trim();
                int res = contact.UpdateContactDtls();
                if (res >= 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdContact.EditIndex = -1;
                    SendUpdateMsg(contact.DeviceId);
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Not Updated";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {

            }
        }
        protected void SendUpdateMsg(int DeviceId)
        {
            usrdvcbal = new UserDeviceBAL();
            dt = new DataTable();
            usrdvcbal.DeviceId = DeviceId;
            dt = usrdvcbal.GetDevicefromDeviceID();
            try
            {
                sms = new SendSMSBAL();
                sms.sendFinalSMS(dt.Rows[0]["MobileNo1"].ToString(), "GBox set as WP7", ClientId);
            }
            catch (Exception)
            { }

        }

        protected void grdContact_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdContact.EditIndex = -1;
            BindGrid();
        }
        protected void grdContact_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdContact.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void grdContact_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                RadioButtonList rbtnIsWhiteList = (RadioButtonList)e.Row.FindControl("rbtnIsWhiteList");
                Label lblEWhiteList = (Label)e.Row.FindControl("lblEWhiteList");
                rbtnIsWhiteList.SelectedValue = lblEWhiteList.Text.Trim();
            }
        }
    }
}