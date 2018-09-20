using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AllowedPhNoHstry : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AnuSearch srch;
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
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            grdHstry.DataSource = srch.GetAllowedPhNoHstry(ClientId);
            grdHstry.DataBind();
        }
        protected string MyFormat(string CreationDate)
        {
            return Convert.ToDateTime(CreationDate).ToString("dd MMM yyyy HH:mm");
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("scall.aspx");
        }
        protected void grdHstry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdHstry.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}