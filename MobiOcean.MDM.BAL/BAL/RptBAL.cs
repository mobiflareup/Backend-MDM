using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobiOcean.MDM.DAL.DAL.RptDALTableAdapters;

/// <summary>
/// Summary description for RptBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class RptBAL
    {
        CheckUpdatedVersion1TableAdapter chkUpdatedVersionTA;
        public string InstalledVersion { get; set; }
        public int ApptypeId { get; set; }
        public RptBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string ChkforUpdate()
        {
            try
            {
                chkUpdatedVersionTA = new CheckUpdatedVersion1TableAdapter();
                return chkUpdatedVersionTA.CheckUpdatedVersion1(InstalledVersion, ApptypeId).Rows[0]["Version_No"].ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}
