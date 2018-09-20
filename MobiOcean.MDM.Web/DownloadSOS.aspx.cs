using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class DownloadSOS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string filePath = "MobiSoS.apk";

            filePath = "/APK/" + filePath;
            System.IO.FileStream fs = null;
            fs = System.IO.File.Open(Server.MapPath("~" + filePath), System.IO.FileMode.Open);
            byte[] btFile = new byte[fs.Length];
            fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=" + filePath.Substring(5));
            Response.ContentType = "application/apk";
            Response.BinaryWrite(btFile);
            Response.End();
        }
    }
}