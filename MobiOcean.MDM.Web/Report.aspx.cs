using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class Report : Base
    {
        int ClientId, UserId, RoleId, ProfileId, DeptId;
        string ProfileName;
        AnuSearch srch;
        DataTable dt;        
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            try
            {
                ProfileId = Convert.ToInt32(Session["ProfileId"].ToString());
                ProfileName = Session["ProfileName"].ToString();
            }
            catch (Exception) { ProfileId = 0; }
            if (ProfileId == 0)
            {
                Response.Redirect("ProfileMaster.aspx");
            }
            if (!IsPostBack)
            {
                txtProfileName.Text = ProfileName.ToString();
                BindDataList();
            }
        }
     
        protected void BindDataList()
        {
            srch = new AnuSearch();
            dt = new DataTable();
            dt = srch.GetFeatureDtls(ProfileId);
            dlReport.DataSource = dt;
            dlReport.DataBind();
        }
        protected void BindGrid(GridView grdrpt, int ProfileFeatureMappingId)
        {
            srch = new AnuSearch();
            grdrpt.DataSource = srch.GetFeatureTimingDtls(ProfileFeatureMappingId);
            grdrpt.DataBind();

        }
        protected void dlReport_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {

                GridView gvr = (GridView)e.Item.FindControl("grdrpt");
                BindGrid(gvr, Convert.ToInt32(dlReport.DataKeys[e.Item.ItemIndex]));
            }
            catch (Exception)
            {

            }
        }
    }
}