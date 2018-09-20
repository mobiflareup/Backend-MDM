using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.KSWDDALTableAdapters;
/// <summary>
/// Summary description for KSWDBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class KSWDBAL
    {
        Usr_DetailsTableAdapter usrdtl;
        public KSWDBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string MobileNo { get; set; }
        public string GetAppIdByMobileNo()
        {
            usrdtl = new Usr_DetailsTableAdapter();
            try
            {
                DataTable dt = usrdtl.GetAppIdByMobileNo(MobileNo);
                return dt.Rows.Count > 0 ? dt.Rows[0]["AndroidAppId"].ToString() : null;
            }
            catch
            {
                return null;
            }
            finally
            {
                usrdtl = null;
            }
        }
    }
}