using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using StudentDALTableAdapters;

/// <summary>
/// Summary description for Uninstall
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class Uninstall
    {
        public string AndroidAppId { get; set; }
        public string isUninstalled { get; set; }
    }    
}