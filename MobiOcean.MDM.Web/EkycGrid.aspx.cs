using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class EkycGrid :Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }
        private void Bind()
        {
            EkycBAL ekyc = new EkycBAL();
            ekyc.Aadhar = txtaadhar.Text;
            ekyc.Name = txtnme.Text;
            grdEkyc.DataSource= ekyc.Get_Ekyc(Session["ClientId"].ToString());//Session["ClientId"].ToString()
            grdEkyc.DataBind();
        }

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            Bind();
        }

        protected void grdEkyc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEkyc.PageIndex = e.NewPageIndex;
            Bind();
        }

        
    }
}