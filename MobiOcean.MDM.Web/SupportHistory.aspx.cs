using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class SupportHistory : Base
    {
        int ClientId, UserId, RoleId, SupportId;
        SupportBAL support;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            SupportId = Convert.ToInt32(Request.QueryString["Id"]);
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        public void BindGrid()
        {
            support = new SupportBAL();
            support.SupportId = SupportId;
            grdHstry.DataSource = support.GetSupportHistory();
            grdHstry.DataBind();
        }
        protected void lnkButton_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = sender as LinkButton;
            GridViewRow row = lnkButton.NamingContainer as GridViewRow;
            string SupportHistoryId = grdHstry.DataKeys[row.RowIndex].Value.ToString();
            try
            {
                string Path = ((Label)row.FindControl("lblfile")).Text.Trim();
                if (!Path.Contains("N/A"))
                {
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path + "\"");
                    Response.TransmitFile(Server.MapPath("~/" + Path));
                    Response.End();
                }
            }
            catch (System.Threading.ThreadAbortException lException)
            {
                lblMsg.Text = "Error Code :" + lException.ToString();
            }
            finally
            {

            }
        }
    }
}