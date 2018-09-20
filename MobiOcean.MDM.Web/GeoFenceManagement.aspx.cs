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
    public partial class GeoFenceManagement : Base
    {
        LocationBAL location;
        GingerboxSrch Srch;
        int ClientId, UserId, RoleId, DeptId;



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
            try
            {
                Srch = new GingerboxSrch();
                if (RoleId == 1 || RoleId == 2)
                {
                    grdclient.DataSource = Srch.GeoFenceRoute(ClientId, txtPcode.Text.Trim(), txtPname.Text.Trim());
                    grdclient.DataBind();
                }
                else
                {
                    grdclient.DataSource = null;
                    grdclient.DataBind();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                Srch = null;
            }
        }
        protected void grdclient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdclient.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdclient_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdclient.Rows[e.RowIndex];
            try
            {
                lblkeyid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                mpdelete.Show();
            }
            catch (Exception)
            {
            }
            finally
            {
            }
        }


        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        

        protected void EditButton_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = sender as LinkButton;
            GridViewRow row = lnkBtn.NamingContainer as GridViewRow;
            string ProfileId = grdclient.DataKeys[row.RowIndex].Value.ToString();
            Response.Redirect("EditGeoFence.aspx?Id=" + ProfileId + "");
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            location = new LocationBAL();
            string alertid = lblkeyid.Text;
            location.RouteId = Convert.ToInt32(alertid);
            location.LoggedBy = UserId.ToString();
            if (location.DeleteGeoFenceRoute() == 1)
            {
                lblmsg.Text = "Route has deleted!";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
            else
            {
                lblmsg.Text = "Route not deleted!";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblkeyid.Text = "";
        }
    }
}