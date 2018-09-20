using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class ccavRequestHandler : Base
    {
        public string strEncRequest = "";
        public string strAccessCode = Constant.strAccessCode;
        public string paymentUrl = Constant.PaymentURL;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    strEncRequest = Request.QueryString["Id"].ToString();
            //}
        }
    }
}