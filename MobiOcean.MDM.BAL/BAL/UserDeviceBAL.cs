using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.UserDeviceDALTableAdapters;
/// <summary>
/// Summary description for UserDeviceBAL
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.BAL
{
    public class UserDeviceBAL
    {
        tblUserDeviceTableAdapter udta;
        tblUserTableAdapter uta;
        sp_utriggerTableAdapter trgr;
        tblTriggerTableAdapter tbltrgr;
        private int _DeviceId;
        private int _Status;
        private int _IsAppUninstalled;
        private string _UserName, _EmailId, _Gender, _ProfileImagePath, _EmpCompanyId;
        private string _DeviceName;
        private string _MobileNo1;
        private string _MobileNo2;
        private string _APPId;
        private int _UserId;
        private int _ClientId;
        private int _TriggerId;
        private string _TriggerType, _MsgTxt, _LogDateTime, _TriggedBy;
        private int _GroupId;
        private string _PIN;

        private int _CurrentUserId;

        public int ProfileId { get; set; }
        public int branchId { get; set; }

        public int deptId { get; set; }

        public string PIN
        {
            get { return _PIN; }
            set { _PIN = value; }
        }
        public string EmpCompanyId
        {
            get { return _EmpCompanyId; }
            set { _EmpCompanyId = value; }
        }
        public int CurrentUserId
        {
            get { return _CurrentUserId; }
            set { _CurrentUserId = value; }
        }
        public int GroupId
        {
            get { return _GroupId; }
            set { _GroupId = value; }
        }
        public int TriggerId
        {
            get { return _TriggerId; }
            set { _TriggerId = value; }
        }
        public string TriggerType
        {
            get { return _TriggerType; }
            set { _TriggerType = value; }
        }
        public string MsgTxt
        {
            get { return _MsgTxt; }
            set { _MsgTxt = value; }
        }
        public string LogDateTime
        {
            get { return _LogDateTime; }
            set { _LogDateTime = value; }
        }
        public string TriggedBy
        {
            get { return _TriggedBy; }
            set { _TriggedBy = value; }
        }
        public int IsAppUninstalled
        {
            get { return _IsAppUninstalled; }
            set { _IsAppUninstalled = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string APPId
        {
            get { return _APPId; }
            set { _APPId = value; }
        }
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }
        public string MobileNo1
        {
            get { return _MobileNo1; }
            set { _MobileNo1 = value; }
        }
        public string MobileNo2
        {
            get { return _MobileNo2; }
            set { _MobileNo2 = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public string ProfileImagePath
        {
            get { return _ProfileImagePath; }
            set { _ProfileImagePath = value; }
        }
        public UserDeviceBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetDevicefromDeviceID()
        {
            try
            {
                udta = new tblUserDeviceTableAdapter();
                return udta.GetData(_DeviceId);

            }
            finally
            {
                udta = null;
            }
        }
        public DataTable GetDeviceByMobileNo()
        {
            try
            {
                udta = new tblUserDeviceTableAdapter();
                return udta.GetDeviceByMobileNo(_MobileNo1);

            }
            finally
            {
                udta = null;
            }
        }
        public DataTable CheckRemoteLockPin()
        {
            try
            {
                udta = new tblUserDeviceTableAdapter();
                return udta.CheckRemoteLockPin(_APPId);
            }
            finally
            {
                udta = null;
            }
        }
        public DataTable GetUNameDDL()
        {
            try
            {
                uta = new tblUserTableAdapter();
                return uta.GetData();
            }
            finally
            {
                uta = null;
            }
        }
        public DataTable GetDeviceWithMDM()
        {
            try
            {
                udta = new tblUserDeviceTableAdapter();
                return udta.GetUserDeviceWithMDM(_ClientId);
            }
            finally
            {
                udta = null;
            }
        }
        public DataTable GetUserDeviceToSendUpdate()
        {
            try
            {
                udta = new tblUserDeviceTableAdapter();
                return udta.GetUserDeviceToSendUpdate(ProfileId, _ClientId);
            }
            finally
            {
                udta = null;
            }
        }
        public DataTable GetDevicewithMDMByUserId()
        {
            try
            {
                udta = new tblUserDeviceTableAdapter();
                return udta.GetDevicewithMDMByUserId(_UserId);
            }
            finally
            {
                udta = null;
            }
        }
        public DataTable GetUserDeviceByDeptAndBranch()
        {
            try
            {
                udta = new tblUserDeviceTableAdapter();
                return udta.GetUserDeviceByDeptAndBranch(branchId, deptId);
            }
            finally
            {
                udta = null;
            }
        }
        public DataTable GetUserDevicebyGroupId()
        {
            try
            {
                udta = new tblUserDeviceTableAdapter();
                return udta.GetUserDevicebyGroupId(_GroupId);
            }
            finally
            {
                udta = null;
            }
        }
        public string GetUserdevice()
        {
            try
            {
                udta = new tblUserDeviceTableAdapter();
                return udta.InsertUserDeviceDtls(DeviceId, ClientId, UserId, UserName, DeviceName, MobileNo1, UserId).ToString();
            }
            catch (Exception)
            {
                return "0";
            }
            finally
            {
                udta = null;
            }
        }
        public int setAndroidAppInstalledStatus()
        {
            udta = new tblUserDeviceTableAdapter();
            try
            {
                return Convert.ToInt32(udta.setAndroidAppInstalledStatus(_IsAppUninstalled, _APPId));
            }
            finally
            {

                udta = null;
            }
        }
        public DataTable GetUserNameByRpntMngr()
        {
            try
            {
                uta = new tblUserTableAdapter();
                return uta.GetUserNameByRptnmgMngr(_ClientId, _UserId);
            }
            finally
            {
                uta = null;
            }
        }
        public int InsertIntoTblTrigger()
        {
            tbltrgr = new tblTriggerTableAdapter();
            return Convert.ToInt32(tbltrgr.InsertIntoTblTrigger(_TriggerId, _TriggerType, _MsgTxt, _UserId, _LogDateTime, _DeviceId));
        }
        public DataTable GetLastDeviceLocation()
        {
            udta = new tblUserDeviceTableAdapter();
            return udta.GetLastLocationofDevice(_DeviceId);
        }
        public DataTable GetFeatureIdAndIsEnable()
        {
            trgr = new sp_utriggerTableAdapter();
            return trgr.sp_utrigger(_DeviceId);
        }
        public DataTable GetUserCodeClientCodeByUserId()
        {
            udta = new tblUserDeviceTableAdapter();
            return udta.GetUserandClientCodeByUserId(_UserId);
        }
        /// <summary>
        /// Get Device Details by giving appID
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetDeviceDataByAppId()
        {
            udta = new tblUserDeviceTableAdapter();
            return udta.GetDeviceDataByAppId(_APPId);
        }
        public int UpdateGetUserDevice()
        {
            udta = new tblUserDeviceTableAdapter();
            return Convert.ToInt32(udta.UpdateProfileImage(_APPId, "/Files/Android_Files" + _ProfileImagePath).ToString());
        }
        public int UpdatePIN()
        {
            udta = new tblUserDeviceTableAdapter();
            return Convert.ToInt32(udta.UpdatePIN(PIN, _UserId.ToString(), _DeviceId).ToString());
        }
    }
}
