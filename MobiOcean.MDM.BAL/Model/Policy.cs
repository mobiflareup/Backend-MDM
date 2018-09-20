using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Policy
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class Policy
    {
        //public int ProfileFeatureMappingId { get; set; }
        public int ProfileId { get; set; }
        public int IsEnable { get; set; }
        public string Message { get; set; }
        public int FeatureId { get; set; }
        public int FeatureStatus { get; set; }
        public string ProfileName { get; set; }
        public int IsBlackList { get; set; }
    }
    public class AttendanceEnable
    {
        public bool IsAttendance { get; set; }
        public bool IsTravelAllowance { get; set; }
        public bool IsConveyance { get; set; }
        public bool IsSecureStorage { get; set; }
        public bool IsGenus { get; set; }
        public bool IsCameraEnable { get; set; }// Camera Enable
        public bool IsFMMEnable { get; set; }//Field Movement Management
    }
    public class DownloadUrlInfo
    {
        public string IsAttendance { get; set; }
        public string IsTravelAllowance { get; set; }
        public string IsConveyance { get; set; }
        public string IsSecureStorage { get; set; }
        //public bool IsCameraEnable { get; set; }// Camera Enable
        // public bool IsFMMEnable { get; set; }//Field Movement Management
    }
}
