using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddFeature : Base
    {
        int ClientId, UserId, RoleId;
        FeatureBAL feature;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            if (RoleId == 2 || RoleId == 3)
            {
                Response.Redirect("AdminDashBoard.aspx");
            }
            else if (RoleId == 4)
            {
                Response.Redirect("userDashBoard.aspx");
            }
            else
            { }
            if (!IsPostBack)
            {
                BindCategoryName();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkValidation())
                {
                    feature = new FeatureBAL();
                    feature.CategoryId = Convert.ToInt32(ddlCategoryName.SelectedValue.ToString());
                    feature.FeatureCode = txtFeatureCode.Text.Trim();
                    feature.FeatureName = txtFeatureName.Text.Trim();
                    feature.FeatureDesc = txtFeatureDescription.Text.Trim();
                    string res = feature.InsertUpdateFeatureDtls();
                    if (int.Parse(res) > 0)
                    {
                        lblMsg.Text = "Feature Details Saved Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;

                    }
                    else
                    {
                        lblMsg.Text = "Feature Details Already exists";
                        lblMsg.ForeColor = System.Drawing.Color.Red;

                    }
                }
                else
                {
                    lblMsg.Text = "Please Fill Mandatory Fields";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FeatureMaster.aspx");
        }
        protected void BindCategoryName()
        {
            feature = new FeatureBAL();
            ListItem li = new ListItem("--Select--", "0");
            ddlCategoryName.Items.Clear();
            ddlCategoryName.Items.Add(li);
            ddlCategoryName.DataSource = feature.GetCategoryName();
            ddlCategoryName.DataTextField = "CategoryName";
            ddlCategoryName.DataValueField = "CategoryId";
            ddlCategoryName.DataBind();
        }
        protected bool ChkValidation()
        {
            if (txtFeatureCode.Text.Trim() == "" || txtFeatureName.Text.Trim() == "" || ddlCategoryName.SelectedIndex <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}