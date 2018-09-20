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
    public partial class CalendarList : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        DDLBAL ddl;
        ContactBAL contact;
        AnuSearch srch;
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
                ListItem ls = new ListItem("--All--", "0");
                ddl = new DDLBAL();
                ddl.ClientId = ClientId;
                ddl.UserId = UserId;
                ddl.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = ddl.GetUserDeviceByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = ddl.GetUsrDeviceByDeptHead();
                }
                else
                {
                    ddlUserName.DataSource = ddl.GetUserDeviceByUserId();
                }
                ddlUserName.DataTextField = "DeviceName";
                ddlUserName.DataValueField = "deviceId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

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
                ddlSyncDateTime.DataSource = contact.GetCalendarSyncDateTime();
                ddlSyncDateTime.DataTextField = "SyncDateTime";
                ddlSyncDateTime.DataValueField = "SyncDateTime";
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
                string SyncDateTime;
                if (ddlSyncDateTime.Items.Count > 0)
                {
                    SyncDateTime = ddlSyncDateTime.SelectedItem.Text.Trim();
                }
                else
                {
                    SyncDateTime = string.Empty;
                }
                srch = new AnuSearch();
                if (RoleId == 1 || RoleId == 2)
                {
                    grdCalender.DataSource = srch.SrchCalendarDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), SyncDateTime);
                }
                else if (RoleId == 3)
                {
                    grdCalender.DataSource = srch.SrchCalendarDtls(ClientId, 0, ddlUserName.SelectedValue.ToString(), SyncDateTime, DeptId);
                }
                else
                {
                    grdCalender.DataSource = srch.SrchCalendarDtls(ClientId, UserId, ddlUserName.SelectedValue.ToString(), SyncDateTime);
                }
                grdCalender.DataBind();
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

        protected void grdCalender_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCalender.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    
}
}