using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class SADashBoard : Base
    {
        int ClientId, UserId, RoleId;
        UserBAL usrbal;
        ClientBAL cl;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
                ChkPasswordExpiry();
                // BindListView();            
            }
        }
        private void BindListView()
        {
            //cl = new ClientBAL();
            //lstview.DataSource = cl.getdata();
            //lstview.DataBind();
        }
        void Popup(bool isDisplay)
        {
            mp.Show();
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            ////if (isDisplay)
            ////{
            //sb.Append(@"<script type='text/javascript'>");
            //sb.Append("$('#myModal').modal('show');");
            //sb.Append(@"</script>");
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
        }
        private void ChkPasswordExpiry()
        {
            usrbal = new UserBAL();
            usrbal.UserId = Convert.ToInt32(Session["UserId"].ToString());
            string ExpiryDay = usrbal.GetRemainngDaysOfExpiryPwd();
            if (!string.IsNullOrEmpty(ExpiryDay))
            {
                if (Convert.ToInt32(ExpiryDay) <= 0)
                {
                    Response.Redirect("Setting.aspx");
                }
                else if (Convert.ToInt32(ExpiryDay) <= 15)
                {
                    lblpwdexpry.Text = "Your password is expiring in " + ExpiryDay + " days. So please <a href=\"ChangePassword.aspx\" >change your password</a> .";
                    Popup(true);
                }

            }
            else
            {
                Response.Redirect("setting.aspx");
            }
        }
        protected void BindGrid()
        {
            cl = new ClientBAL();
            cl.ClientId = ClientId;
            grdClient.DataSource = cl.spCountUserByClient();
            grdClient.DataBind();
        }
        protected void grdClient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdClient.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}