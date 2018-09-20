using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class SupportMaster : Base
    {
        SupportBAL support;
        int ClientId, UserId, RoleId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            lblMsg.Text = string.Empty;
            //if (RoleId != 1)
            //{
            //    Response.Redirect("SupportForm.aspx");
            //}
            //else
            //{ }
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            support = new SupportBAL();
            if (RoleId != 1)
            {
                support.UserId = UserId;
                grdSupport.DataSource = support.GetSupportDetailByUserId();
                grdSupport.DataBind();
            }
            else
            {
                grdSupport.DataSource = support.getdata();
                grdSupport.DataBind();
            }

        }
        protected void grdSupport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSupport.PageIndex = e.NewPageIndex;
            BindGrid();
        }        
        protected void ViewButton_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = sender as LinkButton;
            GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
            string SupportId = grdSupport.DataKeys[row.RowIndex].Value.ToString();
            string url = "~/SupportView.aspx?Id=" + SupportId;
            Response.Redirect(url);
        }
        protected void btnHistory_Click(object sender, EventArgs e)
        {

            LinkButton lnkBtn = sender as LinkButton;
            GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
            string SupportId = grdSupport.DataKeys[row.RowIndex].Value.ToString();
            string url = "~/SupportHistory.aspx?Id=" + SupportId;
            Response.Redirect(url, false);
        }
    }
}