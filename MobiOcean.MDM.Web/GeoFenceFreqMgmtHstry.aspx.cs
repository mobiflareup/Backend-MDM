using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class GeoFenceFreqMgmtHstry : Base
    {
        int ClientId, UserId, RoleId;
        AnuSearch srch;
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
            srch = new AnuSearch();
            grdHstry.DataSource = srch.GetGeoFenceFreqMgmtHstry(ClientId);
            grdHstry.DataBind();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("GeoFenceFreqManagement.aspx");
        }
        protected void grdHstry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdHstry.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}