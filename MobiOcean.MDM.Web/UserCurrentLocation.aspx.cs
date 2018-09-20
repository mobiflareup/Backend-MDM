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
    public partial class UserCurrentLocation : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AnuSearch srch;
        DDLBAL dropbal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                ddlBind();
                GridBind();
                
            }
        }

        protected void grdUsercrntloc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUsercrntloc.PageIndex = e.NewPageIndex;
            GridBind();
        }

        public void GridBind()
        {
            srch = new AnuSearch();
            int s = Convert.ToInt32(Session["ClientId"]);
            grdUsercrntloc.DataSource=srch.SrchUserWiseCrntLocation(Convert.ToInt32(Session["ClientId"]),txtDeviceName.Text);
            grdUsercrntloc.DataBind();
        }
        public void ddlBind()
        {
            try
            {
                System.Web.UI.WebControls.ListItem ls = new System.Web.UI.WebControls.ListItem("--- Select User ---", "0");
                dropbal = new DDLBAL();
                dropbal.ClientId = ClientId;
                dropbal.UserId = UserId;
                dropbal.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = dropbal.GetUserByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = dropbal.GetUserWithoutDeptHead();
                }
                else
                {
                    ddlUserName.Items.Clear();
                    ddlUserName.DataSource = dropbal.GetUserByUserId();
                }
                ddlUserName.DataTextField = "UserName";
                ddlUserName.DataValueField = "UserId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }
        }

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            srch = new AnuSearch();
            if (RoleId == 1 || RoleId == 2)
            {
                grdUsercrntloc.DataSource = srch.SrchUserWiseCrntLocation(Convert.ToInt32(Session["ClientId"]), txtDeviceName.Text.Trim(), Convert.ToInt32(ddlUserName.SelectedValue));
            }
            else if (RoleId == 3)
            {
                grdUsercrntloc.DataSource = srch.SrchUserWiseCrntLocation(Convert.ToInt32(Session["ClientId"]), txtDeviceName.Text.Trim(), Convert.ToInt32(ddlUserName.SelectedValue),0,DeptId);
            }
            else
            {
                grdUsercrntloc.DataSource = srch.SrchUserWiseCrntLocation(Convert.ToInt32(Session["ClientId"]), txtDeviceName.Text.Trim(), Convert.ToInt32(ddlUserName.SelectedValue),UserId);
            }
            grdUsercrntloc.DataBind();
        }
    }
}