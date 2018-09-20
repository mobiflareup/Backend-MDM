using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AppGroup
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class AppGroup
    {
        public int ProfileId { get; set; }
        public int IsEnable { get; set; }
        public string Message { get; set; }
        public int ChatGroupId { get; set; }       
    }

    public class RemarkTable
    {
        public string Remark { get; set; }
        public int RemarkId { get; set; }
    }
}
