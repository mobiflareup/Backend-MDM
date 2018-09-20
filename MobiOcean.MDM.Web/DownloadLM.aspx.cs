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
    public partial class DownloadLM : System.Web.UI.Page
    {
        DataTable dt;
        AckBAL ack;
        protected void Page_Load(object sender, EventArgs e)
        {
            string filePath;
            string Id = Request.QueryString["Id"].ToString();
            ack = new AckBAL();
            dt = new DataTable();
            dt = ack.GetApKLatestDate(string.IsNullOrEmpty(Id) ? 1 : Convert.ToInt32(Id));
            filePath = dt.Rows[0]["ApkPath"].ToString();

            //filePath = "MobiLocationManagement.apk";// dt.Rows[0]["ApkPath"].ToString();
            //if (Id == "5")
            //{
            //    filePath = "EmployeeAttendance.apk";
            //}
            //else if (Id == "6")
            //{
            //    filePath = "FieldMovement.apk";
            //}

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