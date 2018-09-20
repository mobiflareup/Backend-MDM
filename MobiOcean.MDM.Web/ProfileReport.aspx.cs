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
    public partial class ProfileReport : Base
    {
        int ClientId, UserId, RoleId, ProfileId, DeptId;
        string ProfileName;
        VikramSearch srch;
        AnuSearch asrch;
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
               // BindProfileDetail();
                //MoveDatainTempTableToOriginalTable();
                BindGrid();
            }
        }       
        protected void BindGrid()
        {
            srch = new VikramSearch();
            grdCategory.DataSource = srch.GetCategoryName();
            grdCategory.DataBind();
        }


        protected void GridFeature_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView GridFeature = (DataRowView)e.Row.DataItem;
                    ImageButton imgbtnyes = (ImageButton)e.Row.FindControl("btnyes");
                    ImageButton imgbtnno = (ImageButton)e.Row.FindControl("btnNo");
                    CheckBox chk3 = (CheckBox)e.Row.FindControl("switchsize");
                    Label lblIsScheduleNeed = (Label)e.Row.FindControl("lblIsScheduleNeed");
                    try
                    {
                        chk3.Checked = Convert.ToBoolean(GridFeature["IsEnable"]);
                    }
                    catch (Exception) { chk3.Checked = false; }
                    if (chk3.Checked)
                    {
                        imgbtnyes.Visible = true;
                    }
                    else
                    {
                        imgbtnno.Visible = true;
                    }
                    Label lblProfileFeatureId = ((Label)e.Row.FindControl("lblProfileFeatureId"));
                    int ProfileFeatureMappingId = Convert.ToInt32(lblProfileFeatureId.Text);
                    GridView grdrpt = e.Row.FindControl("grdrpt") as GridView;
                    asrch = new AnuSearch();
                    grdrpt.DataSource = asrch.GetFeatureTimingDtls(ProfileFeatureMappingId);
                    grdrpt.DataBind();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int CategoryId = Convert.ToInt32(grdCategory.DataKeys[e.Row.RowIndex].Value.ToString());
                    GridView GridFeature1 = e.Row.FindControl("GridFeature") as GridView;
                    srch = new VikramSearch();
                    GridFeature1.DataSource = srch.srchProfilefeaturebycategoryid(ProfileId, CategoryId);
                    GridFeature1.DataBind();
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
