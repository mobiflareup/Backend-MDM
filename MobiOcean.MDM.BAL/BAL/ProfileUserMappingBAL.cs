using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.ProfileUserMappingDALTableAdapters;
/// <summary>
/// Summary description for ProfileUserMappingBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class ProfileUserMappingBAL
    {

        tblProfileUserMappingTableAdapter pumtp;
        tblProfileTableAdapter tpta;
        tblProfileDeviceMappingTableAdapter pdm;

        public ProfileUserMappingBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _ProfileId;
        private int _DeviceId;
        private int _ClientId;
        private int _UserId;
        private int _IsEnable;
        private int _LoggedBy;
        private int _ProfileUserId, _Status;
        private string _ClientName;
        private string _UserName;
        private string _ProfileName;
        private string _AppliedDateTime;
        private string _CreatedBy;
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
        public int LoggedBy
        {
            get { return _LoggedBy; }
            set { _LoggedBy = value; }
        }
        public int ProfileId
        {
            get { return _ProfileId; }
            set { _ProfileId = value; }
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
        public int IsEnable
        {
            get { return _IsEnable; }
            set { _IsEnable = value; }
        }
        public int ProfileUserId
        {
            get { return _ProfileUserId; }
            set { _ProfileUserId = value; }
        }
        public string AppliedDateTime
        {
            get { return _AppliedDateTime; }
            set { _AppliedDateTime = value; }
        }
        public string ClientName
        {
            get { return _ClientName; }
            set { _ClientName = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string ProfileName
        {
            get { return _ProfileName; }
            set { _ProfileName = value; }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public DataTable GetProfileuserMapping()
        {
            pumtp = new tblProfileUserMappingTableAdapter();
            return pumtp.GetProfileUserWithName(_ClientId);
        }
        public int UpdateRequestDetails()
        {
            pumtp = new tblProfileUserMappingTableAdapter();
            try
            {

                return pumtp.UpdateQuery(ProfileId, IsEnable, ProfileUserId);
            }
            finally
            {
                pumtp = null;
            }
        }
        public DataTable GetProfileName()
        {
            try
            {
                tpta = new tblProfileTableAdapter();
                return tpta.GetData();
            }
            finally
            {
                tpta = null;
            }
        }
        public string spProfileUserMapping()
        {
            try
            {
                pumtp = new tblProfileUserMappingTableAdapter();
                return pumtp.spProfileUserMapping(_ProfileUserId, _ProfileId, _ClientId, _UserId, _IsEnable, _AppliedDateTime, _LoggedBy.ToString(), _Status).ToString();
            }
            catch (Exception)
            {
                return "0";
            }
            finally
            {
                tpta = null;
            }
        }
        public string DeleteProfileUserMapping()
        {
            try
            {
                pumtp = new tblProfileUserMappingTableAdapter();
                return pumtp.DeleteProfileUserMapping(_IsEnable, _LoggedBy.ToString(), _AppliedDateTime, _ProfileUserId).ToString();
            }
            catch (Exception)
            {
                return "0";
            }
            finally
            {
                tpta = null;
            }
        }
        public DataTable GetProfileDeviceMappingByDeviceId()
        {
            try
            {
                pumtp = new tblProfileUserMappingTableAdapter();
                return pumtp.GetProfileDeviceMappingByDeviceId(_DeviceId);
            }
            finally
            {
                tpta = null;
            }
        }
        public int spProfileDeviceMapping()
        {
            pdm = new tblProfileDeviceMappingTableAdapter();
            return Convert.ToInt32(pdm.spProfileDeviceMapping(ProfileId, ClientId, DeviceId, IsEnable, AppliedDateTime, CreatedBy, Status).ToString());
        }
    }
}
