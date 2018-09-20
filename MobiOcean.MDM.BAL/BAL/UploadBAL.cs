using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.UploadDALTableAdapters;

/// <summary>
/// Summary description for UploadBAL
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.BAL
{
    public class UploadBAL
    {
       
        tblApkTableAdapter apk;        
      
        public int UserId { get; set; }        
        public string APKPath { get; set; }
        public string VersionName { get; set; }


        public UploadBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }       
        public DataTable getdata()
        {
            apk = new tblApkTableAdapter();
            return apk.GetDataBy1();
        }
        public int InsertAPK()
        {
            apk = new tblApkTableAdapter();
            return Convert.ToInt16(apk.APKUpload(VersionName, APKPath, UserId));
        }
    }
}