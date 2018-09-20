using MobiOcean.MDM.Web.Log.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class KycBrowserForward : System.Web.UI.Page
    {
        private ILog _ILog;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LogExceptions(string Controller, string input, string output, string Expecption)
        {
            _ILog = Log.Repository.Log.GetInstance;
            StringBuilder sb = new StringBuilder();
            sb.Append(Controller);
            sb.AppendLine();
            sb.Append(input);
            sb.AppendLine();
            if (!string.IsNullOrEmpty(output))
            {
                sb.Append(output);
                sb.AppendLine();
            }
            if (!string.IsNullOrEmpty(Expecption))
            {
                sb.Append(Expecption);
            }
            _ILog.LogAPIData(sb.ToString());
        }
    }
}