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
    public partial class API : System.Web.UI.Page
    {
        AckBAL ack;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ack = new AckBAL();
            dt = new DataTable();
            string filePath, datetime;
            dt = ack.GetApKLatestDate(1);
            datetime = dt.Rows[0]["CreationDate"].ToString();
            filePath = dt.Rows[0]["ApkPath"].ToString();

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