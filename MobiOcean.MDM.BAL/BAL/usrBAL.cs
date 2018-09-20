using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.DAL.DAL.usrDALTableAdapters;

/// <summary>
/// Summary description for usrBAL
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.BAL
{
    public class usrBAL
    {
        private int _GroupId, _ClientId, _RoleId, _DeviceId, _UserId, _DeptId, _RptMngrId, _UserAppGroupId, _Status, _Branch, _DeviceOwnerShip;
        private string _UserCode, _UserName, _MobileNo, _PreferredContactNo, _EmailId, _Password, _EmpCompanyId;
        private string _DOB, _Gender, _DateOfJoining, _TempAddress, _PermanentAddress, _Country, _PinCode, _ProfileImagePath;
        private string _DeviceName, _MobileNo1, _MobileNo2, _SimNo1, _SimNo2, _APPId;
        private string _Message, _LoggedBy, _RowVer, _ClientCode, _Designation;
        
        tblUserTableAdapter user;
        tblClientTableAdapter tblclient;
        tblUserDeviceTableAdapter usrdevice;
        tblRoleTableAdapter tblRole;
        DataTable dt;
        private string _MobileNoList;
        public string Token { get; set; }
        public int OTP { get; set; }
        public DateTime currentDateTime { get; set; }       
        public string MobileNoList
        {
            get { return _MobileNoList; }
            set { _MobileNoList = value; }
        }
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }
        public int Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }
        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        public int RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public int UserAppGroupId
        {
            get { return _UserAppGroupId; }
            set { _UserAppGroupId = value; }
        }
        public int GroupId
        {
            get { return _GroupId; }
            set { _GroupId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int DeptId
        {
            get { return _DeptId; }
            set { _DeptId = value; }
        }
        public string EmpCompanyId
        {
            get { return _EmpCompanyId; }
            set { _EmpCompanyId = value; }
        }
        public int RptMngrId
        {
            get { return _RptMngrId; }
            set { _RptMngrId = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public string PreferredContactNo
        {
            get { return _PreferredContactNo; }
            set { _PreferredContactNo = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public string APPId
        {
            get { return _APPId; }
            set { _APPId = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string DOB
        {
            get { return _DOB; }
            set { _DOB = value; }
        }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        public string DateOfJoining
        {
            get { return _DateOfJoining; }
            set { _DateOfJoining = value; }
        }
        public string TempAddress
        {
            get { return _TempAddress; }
            set { _TempAddress = value; }
        }
        public string PermanentAddress
        {
            get { return _PermanentAddress; }
            set { _PermanentAddress = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string PinCode
        {
            get { return _PinCode; }
            set { _PinCode = value; }
        }
        public string ProfileImagePath
        {
            get { return _ProfileImagePath; }
            set { _ProfileImagePath = value; }
        }
        public string SimNo1
        {
            get { return _SimNo1; }
            set { _SimNo1 = value; }
        }
        public string SimNo2
        {
            get { return _SimNo2; }
            set { _SimNo2 = value; }
        }
        public string ClientCode
        {
            get { return _ClientCode; }
            set { _ClientCode = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
      
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string LoggedBy
        {
            get { return _LoggedBy; }
            set { _LoggedBy = value; }
        }
        public string RowVer
        {
            get { return _RowVer; }
            set { _RowVer = value; }
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

        public int DeviceOwnerShip
        {
            get { return _DeviceOwnerShip; }
            set { _DeviceOwnerShip = value; }
        }
        public string CountryId { get; set; }
        public string APPIdKSWD { get; set; }
        public usrBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetData()
        {
            user = new tblUserTableAdapter();
            return user.GetData();
        }
        public DataTable GetUserDeviceData()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.GetUsrDeviceData();
        }
        public string InsertUserDeviceData()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.InsertUserDeviceDtls(_DeviceId, _ClientId, _UserId, _UserName, _DeviceName, _MobileNo1, _DeviceOwnerShip).ToString();
        }
        public string InsertUserDeviceDataRaj()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.InsertUserDeviceDtlsRaj(_DeviceId, _ClientId, _UserId, _UserName, _DeviceName, _MobileNo1, CountryId, _DeviceOwnerShip).ToString();
        }
        public string InsertUserDeviceDataRaj1()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.InsertUserDeviceDtlsRaj1(_DeviceId, _ClientId, _UserId, _UserName, _DeviceName, _MobileNo1, CountryId, _DeviceOwnerShip, APPIdKSWD).ToString();
        }
        public string UpdateOwnerShip()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.UpdateOwnerShip(_DeviceOwnerShip, _UserId).ToString();
        }
        public int DeleteUserDevice()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return Convert.ToInt32(usrdevice.DeleteuserdeviceDetails(_DeviceId).ToString());
        }
        public DataTable Insertuser()
        {
            user = new tblUserTableAdapter();
            return user.InsertUser(_UserId, _ClientId, _DeptId, _RoleId, _EmpCompanyId, _PreferredContactNo, _UserCode, _UserName, _MobileNo, _EmailId, _Password, _Gender, _RptMngrId, _TempAddress, _Country, _ProfileImagePath, _Branch, _Designation);
        }
        public DataTable InsertUserWithMultipleDevice()
        {
            user = new tblUserTableAdapter();
            return user.InsertUserWithMultipleDevice(_UserId, _ClientId, _DeptId, _RoleId, _EmpCompanyId, _PreferredContactNo, _UserCode, _UserName, _MobileNo, _MobileNoList, _EmailId, _Password, _Gender, _RptMngrId, _TempAddress, _Country, _ProfileImagePath, _Branch, _Designation, _LoggedBy);
        }
        public int DeleteUserDtls()
        {
            user = new tblUserTableAdapter();
            return Convert.ToInt32(user.DeleteUserDtls(_UserId).ToString());
        }
        public DataTable GetReportingMgr()
        {
            user = new tblUserTableAdapter();
            return user.GetRptngManagerForDDL(_ClientId);
        }
        public DataTable GetPassword()
        {
            dt = new DataTable();
            user = new tblUserTableAdapter();
            try
            {
                dt = user.GetPassword(_EmailId, _Password);
                return dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                dt = null;
                user = null;
            }
        }
        public int InsertIntoForgetPwdKey()
        {
            user = new tblUserTableAdapter();
            return Convert.ToInt32(user.InsertIntoForgetPasswordKey(_EmailId, Token, OTP, currentDateTime.AddMinutes(1440)));
        }
        //public int CheckOTPusingTokenAndEmailId()
        //{
        //    user = new tblUserTableAdapter();
        //    return Convert.ToInt32(user.CheckOTPUsingTokenAndEmailID(_EmailId, OTP, Token));
        //}
        public DataTable CheckIsPasswordChanged()
        {
            user = new tblUserTableAdapter();
            return user.ChkIsPasswordChanged(_EmailId);
        }
        //public int UpdateUserPassword()
        //{
        //    user = new tblUserTableAdapter();
        //    return Convert.ToInt32(user.UpdateUserPassword(_EmailId, _Password));
        //}
        public int CheckCountNoOfEmployees()
        {
            user = new tblUserTableAdapter();
            return Convert.ToInt32(user.CheckCountNoOfEmployees(_ClientId));
        }
        public DataTable GetClientName()
        {
            tblclient = new tblClientTableAdapter();
            return tblclient.GetActiveClient();
        }
        public DataTable GetRoleName()
        {
            tblRole = new tblRoleTableAdapter();
            return tblRole.GetRoleDetails();
        }
    }

    public class ForgetPassword
    {
        tblUserTableAdapter user;
        public string EmailId { get; set; }
        public string OTP { get; set; }
        public string AccessToken { get; set; }
        public DataTable CheckOTPusingTokenAndEmailId()
        {
            user = new tblUserTableAdapter();
            return user.CheckOTPUsingTokenAndEmailID(EmailId, OTP, AccessToken);
        }
    }
    public class UpdatePwd
    {
        tblUserTableAdapter user;
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string CnfrmPassword { get; set; }
        public string AccessToken { get; set; }

        public int UpdateUserPassword()
        {
            user = new tblUserTableAdapter();
            return Convert.ToInt32(user.UpdateUserPassword(EmailId, Password, AccessToken).ToString());
        }
        //public DataTable CheckOTPusingTokenAndEmailId()
        //{
        //    user = new tblUserTableAdapter();
        //    return user.CheckOTPUsingTokenAndEmailID(EmailId, Password, AccessToken);
        //}
    }
}
