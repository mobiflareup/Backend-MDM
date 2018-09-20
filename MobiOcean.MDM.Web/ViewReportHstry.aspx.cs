using MobiOcean.MDM.BAL.BAL;
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
    public partial class ViewReportHstry : Base
    {
        int ClientId, UserId, RoleId, ProfileId, DeptId;
        string ProfileName;
        ProfileBAL profile;       
        AnuSearch srch;
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
                lblProfileName.Text = ProfileName.ToString();
                BindDateTime();
                BindDataList();
            }
        }
        protected void BindDateTime()
        {
            profile = new ProfileBAL();
            profile.ProfileId = ProfileId;
            ddlCreationDate.Items.Clear();
            ddlCreationDate.DataSource = profile.GetCreationDateByProfileId();
            ddlCreationDate.DataTextField = "DateTime";
            ddlCreationDate.DataValueField = "DateTime";
            ddlCreationDate.DataBind();
        }
       
        protected void BindDataList()
        {
            srch = new AnuSearch();
            dlReport.DataSource = srch.GetFeatureHistoryDtls(ProfileId, ddlCreationDate.SelectedItem.ToString());
            dlReport.DataBind();
        }
        protected void BindGrid(GridView grdrpt, int ProfileFeatureMappingId)
        {
            srch = new AnuSearch();
            grdrpt.DataSource = srch.GetFeatureTimingHistoryDtls(ProfileFeatureMappingId);
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
        protected void ddlCreationDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataList();
        }
    }
}