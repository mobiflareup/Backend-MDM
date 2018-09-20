using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddAppGrp : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AppBAL app;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (RoleId == 2 || RoleId == 3)
            {
                Response.Redirect("AdminDashBoard");
            }
            else if (RoleId == 4)
            {
                Response.Redirect("userDashBoard");
            }
            else
            { }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkValidations())
                {
                    app = new AppBAL();
                    app.AppGroupCode = txtGrpCode.Text.Trim();
                    app.AppGroupName = txtGrpName.Text.Trim();

                    string res = app.spAppGrpDtls();
                    if (int.Parse(res) > 0)
                    {
                        lblMsg.Text = "Group Details Saved Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("AppGroup");
                    }
                    else
                    {
                        lblMsg.Text = "Group Details Already exists";
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
        protected bool ChkValidations()
        {
            if (txtGrpCode.Text.Trim() == "" || txtGrpName.Text.Trim() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AppGroup");
        }
    }
}