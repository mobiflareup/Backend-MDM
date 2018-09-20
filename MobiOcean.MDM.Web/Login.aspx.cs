using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class Login : LoginBase
    {
        LoginBAL LgnBAL;
        DataTable dt;
        //string IsPayment;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnlogin_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            LgnBAL = new LoginBAL();
            try
            {
                string a = txtusername.Text.Trim().Trim();
                LgnBAL.EmailId = txtusername.Text.Trim().Trim();
                LgnBAL.Password = txtpassword.Text.Trim();
                dt = LgnBAL.ValidateLogin();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    lblmsg.Text = "Invalid User ID or Password";
                    lblmsg.Visible = true;
                }
                else
                {
                    MaintainSession(dt);
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
            finally
            {
                LgnBAL = null;
                dt = null;
            }
        }                
    }
}