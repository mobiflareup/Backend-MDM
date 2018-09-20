using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class LogOut : Page
    {
        string logOurUrl = Constant.MobiURL;

        protected void Page_Load(object sender, EventArgs e)
        {
            //  sendStngUpdateSMStoParents();
            try
            {
                if (Session["isLoginByOther"].ToString().Trim().Length > 0)
                    logOurUrl = Constant.MobiURL;
            }
            catch (Exception) { }

            Session.RemoveAll();
            Session.Abandon();

            Response.Redirect(logOurUrl);

        }
    }
}