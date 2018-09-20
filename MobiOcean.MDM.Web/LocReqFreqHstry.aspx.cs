using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class LocReqFreqHstry : Base
    {
        int ClientId, UserId, RoleId;
        GroupBAL grp;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            grp = new GroupBAL();
            grp.ClientId = ClientId;
            grdHstry.DataSource = grp.GetLocReqFreqHstry();
            grdHstry.DataBind();
        }
        protected void btnBackToLocationMgmt_Click(object sender, EventArgs e)
        {
            Response.Redirect("LocReqFreq.aspx");
        }
        protected void grdHstry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdHstry.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}