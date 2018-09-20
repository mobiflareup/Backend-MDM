using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.AllowPhNoDALTableAdapters;

/// <summary>
/// Summary description for AllowPhNo
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class AllowPhNoBAL
    {
        tblAllowedPhNoTableAdapter phno;
        tblAllowedPhNotempTableAdapter phnotemp;
        tblProfileAllowedPhNoTableAdapter profilephno;
        private int _AllowedPhNoId, _ClientId, _DeviceId, _UserId, _ProfileId, _IsIncoming, _IsWhiteList, _IsOutgoing, _IsForSms;
        private string _MobileNo, _AndroidAppId, _Name;
        object _dt;
        private string _AllowPhNo, _AllowedPhNo;
        private int _Status;
        public string AllowPhNo
        {
            get { return _AllowPhNo; }
            set { _AllowPhNo = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string AllowedPhNo
        {
            get { return _AllowedPhNo; }
            set { _AllowedPhNo = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public int ProfileId
        {
            get { return _ProfileId; }
            set { _ProfileId = value; }
        }
        public object dt
        {
            get { return _dt; }
            set { _dt = value; }
        }
        public int AllowedPhNoId
        {
            get { return _AllowedPhNoId; }
            set { _AllowedPhNoId = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public string AndroidAppId
        {
            get { return _AndroidAppId; }
            set { _AndroidAppId = value; }
        }
        public int IsIncoming
        {
            get { return _IsIncoming; }
            set { _IsIncoming = value; }
        }
        public int IsOutgoing
        {
            get { return _IsOutgoing; }
            set { _IsOutgoing = value; }
        }
        public int IsWhiteList
        {
            get { return _IsWhiteList; }
            set { _IsWhiteList = value; }
        }
        public int IsForSms
        {
            get { return _IsForSms; }
            set { _IsForSms = value; }
        }
        public string CountryId { get; set; }
        public AllowPhNoBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetAllowedPhoneNo()
        {
            phnotemp = new tblAllowedPhNotempTableAdapter();
            return phnotemp.GetAllowedPhoneNo(_ClientId);
        }
        public DataTable GetMobileNoByClientId()
        {
            phno = new tblAllowedPhNoTableAdapter();
            return phno.GetMobileNoByClientId(_ClientId);
        }
        public int DeleteMobileNo()
        {
            phno = new tblAllowedPhNoTableAdapter();
            return phno.DeleteMobileNo(_AllowedPhNoId);
        }
        public int InsertintotblAllowedPhNo()
        {
            phno = new tblAllowedPhNoTableAdapter();
            return Convert.ToInt32(phno.InsertintotblAllowedPhNo(_ClientId, _dt));
        }
        public int ApplyAllowedPhoneNoChanges()
        {
            phno = new tblAllowedPhNoTableAdapter();
            return Convert.ToInt32(phno.ApplyAllowedPhoneNoChanges(_ClientId, _UserId));
        }
        public int CancelAllowedPhoneNoChanges()
        {
            phno = new tblAllowedPhNoTableAdapter();
            return Convert.ToInt32(phno.CancelAllowedPhoneNoChanges(_ClientId, _UserId));
        }
        public int IU_tblAllowedPhNo()
        {
            try
            {
                phno = new tblAllowedPhNoTableAdapter();
                return Convert.ToInt32(phno.IU_tblAllowedPhNo(_ClientId, _AllowPhNo, _Name, _Status));
            }
            catch (Exception)
            {
                return 1;
            }
        }
        public DataTable GetProfileAllowedPhNo()
        {
            profilephno = new tblProfileAllowedPhNoTableAdapter();
            return profilephno.GetProfileAllowedPhNoData(_ProfileId, _IsWhiteList);
        }
        public int GetAllowedPhNoByProfileId()
        {
            profilephno = new tblProfileAllowedPhNoTableAdapter();
            profilephno.GetAllowedPhNoByProfileId(_ProfileId, _ClientId);
            return 1;
        }
        public int IU_ProfileAllowedPhNo()
        {
            profilephno = new tblProfileAllowedPhNoTableAdapter();
            profilephno.IU_ProfileAllowedPhNo(_ProfileId, _AllowedPhNo, _IsIncoming, _IsWhiteList, _UserId);
            return 1;
        }
        public int InsertProfileAllowedNo()
        {
            profilephno = new tblProfileAllowedPhNoTableAdapter();
            return Convert.ToInt32(profilephno.InsertProfileAllowedNo(_ProfileId, _ClientId, _MobileNo, _Name, _Status, _IsIncoming, _IsOutgoing, _IsForSms, _UserId, _IsWhiteList).ToString());

        }
        public int InsertProfileAllowedNoRaj()
        {
            profilephno = new tblProfileAllowedPhNoTableAdapter();
            return Convert.ToInt32(profilephno.InsertProfileAllowedNoRaj(_ProfileId, _ClientId, _MobileNo, _Name, _Status, _IsIncoming, _IsOutgoing, _IsForSms, _UserId, _IsWhiteList, CountryId).ToString());

        }
        public DataTable GetAllowedPhNoByClientId()
        {
            phno = new tblAllowedPhNoTableAdapter();
            return phno.GetAllowedPhno(_ClientId, _IsWhiteList);
        }
        public int IU_AllowedPhNo()
        {
            phno = new tblAllowedPhNoTableAdapter();
            return Convert.ToInt32(phno.IU_AllowedPhNo(_ClientId, _MobileNo, _Name, _Status, _IsIncoming, _IsOutgoing, _IsForSms, _UserId, _IsWhiteList).ToString());
        }
        public int DeleteAllowedPhNo()
        {
            phno = new tblAllowedPhNoTableAdapter();
            return Convert.ToInt32(phno.DeleteAllowedPhNo(_AllowedPhNoId));
        }
    }
}