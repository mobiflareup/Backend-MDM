using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class BackToadmin : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserId"] = Session["AdminId"];
            Session["Role"] = Session["helpRoleId"];
            Session["UserName"] = Session["AdminName"];
            Session["helpRoleId"] = null;
            Response.Redirect("UserMaster.aspx");
        }
    }
}