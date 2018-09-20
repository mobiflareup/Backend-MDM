using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class UserProfile : Base
    {
        UserBAL user;
        DataTable dt;



        int ClientId, UserId, RoleId, EmpId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblpopmsg.Text = string.Empty;
            EmpId = Convert.ToInt32(Request.QueryString["Id"]);
            if (EmpId == 0)
            {
                EmpId = UserId;
            }
            if (!IsPostBack)
            {
                BindOldData();

            }
        }

        protected void BindOldData()
        {
            try
            {
                user = new UserBAL();
                dt = new DataTable();

                user.UserId = EmpId;
                dt = user.GetUserDtlByUserId();
                ViewState["Dt"] = dt;
                UTextBox1.Text = dt.Rows[0]["EmpCompanyId"].ToString();
                UTextBox2.Text = dt.Rows[0]["UserName"].ToString();
                UTextBox3.Text = dt.Rows[0]["MobileNo"].ToString();
                UTextBox4.Text = dt.Rows[0]["EmailId"].ToString();
                txtDept.Text = dt.Rows[0]["DeptName"].ToString();
                txtRole.Text = dt.Rows[0]["RoleName"].ToString();
                txtManager.Text = dt.Rows[0]["ManagerName"].ToString();
                txtGender.Text = dt.Rows[0]["Designation"].ToString();
                txtBranch.Text = dt.Rows[0]["BranchName"].ToString();                
                if (string.IsNullOrEmpty(dt.Rows[0]["ProfileImagepath"].ToString()))
                {
                    profileImage.ImageUrl = "~/image/NoPic.png";
                }
                else
                {
                    profileImage.ImageUrl = "~" + dt.Rows[0]["ProfileImagepath"].ToString();
                    lblimagepath.Text = dt.Rows[0]["ProfileImagepath"].ToString();
                }


            }
            catch (Exception) { }
            finally
            {
                dt = null;
                user = null;
            }
        }

        protected void addToTable_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfile.aspx");
            btnCancel.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditUser.aspx?Id=" + UserId + "");
            btnCancel.Visible = true;
        }
    }
}