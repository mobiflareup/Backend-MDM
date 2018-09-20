using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Changepass
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class Changepass
    {
        public string EmailId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}