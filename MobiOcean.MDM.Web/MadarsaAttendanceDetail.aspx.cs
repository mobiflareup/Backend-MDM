using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class MadarsaAttendanceDetail : Base
    {
        MadarsaBAL mbal;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            mbal = new MadarsaBAL();
            grdAttendance.DataSource = mbal.GetMadarsaAttendanceDetails();
            grdAttendance.DataBind();
        }
        protected void grdAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAttendance.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}