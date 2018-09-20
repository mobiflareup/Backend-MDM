using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class ChangePassword : Base
    {
        ChangePasswordBAL cpbal;

        int ClientId, RoleId, UserId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                //BindGrid();

            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            cpbal = new ChangePasswordBAL();
            try
            {
                cpbal.UserId = UserId;
                cpbal.Password = txtOldPassword.Text;
                cpbal.NewPassword = txtNewPassword.Text;
                cpbal.ChangePassword();
                lblMsg.Text = "Password Changed Successfully..!";
            }
            finally
            {
                cpbal = null;
            }
        }
    }
}