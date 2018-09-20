using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class SensorDetails : Base
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
            grdwifisensor.DataSource = srch.GetSensorDetailsfromtblsensorenable(txtUname.Text.Trim(), txtDname.Text.Trim(), txtSname.Text.Trim(), ClientId);
            grdwifisensor.DataBind();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdwifisensor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdwifisensor.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected string Format(string date)
        {
            return Convert.ToDateTime(date).ToString("dd-MMM-yyyy HH:mm");
        }
    
}
}