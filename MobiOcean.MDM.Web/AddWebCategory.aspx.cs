using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddWebCategory : Base
    {
        WebsiteLogsBAL keybal;
        int ClientId, UserId, RoleId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
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
                Reset();
            }
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKCode.Text.Trim() == "" || txtKName.Text.Trim() == "" || txtKDesc.Text.Trim() == "")
                {
                    lblMsg.Text = "Fill all mandatory fields!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    keybal = new WebsiteLogsBAL();
                    keybal.CategoryId = 0;
                    keybal.ClientId = 0;
                    keybal.CategoryCode = txtKCode.Text.Trim();
                    keybal.CategoryName = txtKName.Text.Trim();
                    keybal.CategoryDesc = txtKDesc.Text.Trim();
                    keybal.Status = 0;
                    keybal.LoggedBy = UserId.ToString();
                    int res = keybal.IU_WebCategory();
                    if (res > 0)
                    {
                        lblMsg.Text = "Category saved successfully.";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("WebCategories.aspx");

                    }
                    else
                    {
                        lblMsg.Text = "Category already exists.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;

                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                keybal = null;
            }
        }
        public void Reset()
        {
            txtKName.Text = "";
            txtKCode.Text = "";
            txtKDesc.Text = "";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebCategories.aspx");
        }
    }
}