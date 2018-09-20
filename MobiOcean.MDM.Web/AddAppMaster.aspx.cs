using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddAppMaster : Base
    {
        GroupBAL grpbal;
        int ClientId, RoleId, UserId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (RoleId == 4)
            {
                Response.Redirect("userDashBoard");
            }
            if (!IsPostBack)
            {
                BindGroupDDL(ddlGroupName);
                Reset();
            }
        }
        protected void BindGroupDDL(DropDownList ddl)
        {
            ListItem ls = new ListItem("Select", "0");
            try
            {
                grpbal = new GroupBAL();
                ddlGroupName.Items.Clear();
                ddlGroupName.Items.Add(ls);
                ddlGroupName.DataSource = grpbal.GetAppGrpNameForDDL();
                ddlGroupName.DataTextField = "AppGroupName";
                ddlGroupName.DataValueField = "AppGroupId";
                ddlGroupName.DataBind();
            }
            catch (Exception) { }
            finally
            {
                ls = null;
                grpbal = null;
            }
        }
        protected void Reset()
        {
            ddlGroupName.SelectedIndex = 0;
            txtApplicationName.Text = string.Empty;
            txtApplicationCode.Text = string.Empty;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {

                grpbal = new GroupBAL();
                //grpbal.ClientId = ClientId;
                if (ChkValidation())
                {
                    grpbal.GroupName = ddlGroupName.SelectedItem.Text.Trim();
                    grpbal.GroupId = Convert.ToInt32(ddlGroupName.SelectedValue.ToString());
                    grpbal.ClientId = ClientId;
                    grpbal.AppCode = txtApplicationCode.Text.Trim();
                    grpbal.AppName = txtApplicationName.Text.Trim();
                    grpbal.LoggedBy = UserId.ToString();
                    int result = grpbal.iu_AppMster();
                    //if (int.Parse(result) > 0)
                    if (result > 0)
                    {
                        lblMsg.Text = "Application Added successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("AppMaster");
                    }
                    else
                    {
                        lblMsg.Text = "Application already exists!";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "Fill all mandatory fields!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

            catch (Exception) { }
            finally
            {
                grpbal = null;
            }
        }
        protected bool ChkValidation()
        {
            if (txtApplicationCode.Text.Trim() == "" || txtApplicationName.Text.Trim() == "" || ddlGroupName.SelectedIndex <= 0)
                return false;
            else
                return true;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AppMaster");
            //Server.Transfer("AppMaster.aspx", true);
        }
    }
}