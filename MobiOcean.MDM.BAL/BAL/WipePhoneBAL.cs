using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.WipePhoneDALTableAdapters;

/// <summary>
/// Summary description for WipePhoneBAL
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.BAL
{
    public class WipePhoneBAL
    {
        tblSosContactsTableAdapter sosDal;
        tblSosContacts1TableAdapter sosDDal;
        tblWipeDataTableAdapter wipedal;
        tblRemoteDataTableAdapter remotdal;
        tblProfileSosContactsTableAdapter ProfileSosContacts;
        private object _dt;
        private int _ClientId,  _UserId, _ContactId, _WipeDataId, _RemoteDataId, _ProfileId;
        private string _MobileNo, _ContactNo, _appID, _ContactPersonName, _EmailId, _Designation;
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public int ProfileId
        {
            get { return _ProfileId; }
            set { _ProfileId = value; }
        }      
        public int ContactId
        {
            get { return _ContactId; }
            set { _ContactId = value; }
        }
        public int WipeDataId
        {
            get { return _WipeDataId; }
            set { _WipeDataId = value; }
        }
        public int RemoteDataId
        {
            get { return _RemoteDataId; }
            set { _RemoteDataId = value; }
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
        public string ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }
        public string appID
        {
            get { return _appID; }
            set { _appID = value; }
        }
        public string ContactPersonName
        {
            get { return _ContactPersonName; }
            set { _ContactPersonName = value; }
        }       
        public object dt
        {
            get { return _dt; }
            set { _dt = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }
        public string CountryId { get; set; }
        public WipePhoneBAL()
        {

        }
        public DataTable GetSosContactDetailsByClientId()
        {
            sosDDal = new tblSosContacts1TableAdapter();
            return sosDDal.GetSosContactDetailsByClientId(_ClientId);
        }
        public DataTable GetUsrDetail()
        {
            sosDal = new tblSosContactsTableAdapter();
            return sosDal.GetUserDetail(_UserId);
        }
        public int InsertSosDtl()
        {
            sosDDal = new tblSosContacts1TableAdapter();
            return Convert.ToInt32(sosDDal.InsertSosContactdtls(_ClientId, _UserId, _dt));
        }
        public DataTable GetWipeRequester()
        {
            wipedal = new tblWipeDataTableAdapter();
            return wipedal.GetWipeRequester(_UserId);
        }
        public int InsertWipeDtl()
        {
            wipedal = new tblWipeDataTableAdapter();
            return Convert.ToInt32(wipedal.InsertWipeDataDtls(_ClientId, _UserId, _dt));
        }
        public int DeleteFromWipe()
        {
            wipedal = new tblWipeDataTableAdapter();
            return Convert.ToInt32(wipedal.DeleteQuery(_WipeDataId));
        }
        public DataTable GetRemoteRequester()
        {
            remotdal = new tblRemoteDataTableAdapter();
            return remotdal.GetRemoteUserData(_UserId);
        }
        public int InsertRemoteDtl()
        {
            remotdal = new tblRemoteDataTableAdapter();
            return Convert.ToInt32(remotdal.InsertRemoteDataDtls(_ClientId, _UserId, _dt));
        }
        public int DeleteFromRemote()
        {
            remotdal = new tblRemoteDataTableAdapter();
            return Convert.ToInt32(remotdal.DeleteteQueryRemote(_RemoteDataId));
        }
        private SosContacts[] _SosContacts;
        public SosContacts[] SosContacts
        {
            get { return _SosContacts; }
            set { _SosContacts = value; }
        }
        public DataTable GetSosContacts()
        {
            sosDal = new tblSosContactsTableAdapter();
            return sosDal.GetSosContactByUserId(_UserId);
        }
        public DataTable GetSosContactByProfileId()
        {
            sosDal = new tblSosContactsTableAdapter();
            return sosDal.GetSosContactByProfileId(_ProfileId);
        }
        public int InsertSosDetailsByClientId()
        {
            sosDDal = new tblSosContacts1TableAdapter();
            return Convert.ToInt32(sosDDal.InsertSosDetailsByClientId(_ClientId, _ContactPersonName, _Designation, _ContactNo, _EmailId, _UserId).ToString());
        }
        public int InsertSosDetailsByClientIdRaj()
        {
            sosDDal = new tblSosContacts1TableAdapter();
            return Convert.ToInt32(sosDDal.InsertSosDetailsByClientIdRaj(_ClientId, _ContactPersonName, _Designation, _ContactNo, _EmailId, _UserId, CountryId).ToString());
        }
        public int DeleteSosContacts()
        {
            sosDDal = new tblSosContacts1TableAdapter();
            return sosDDal.DeleteSosContacts(_ContactId);
        }
        public DataTable GetProfileSosContacts()
        {
            ProfileSosContacts = new tblProfileSosContactsTableAdapter();
            return ProfileSosContacts.GetProfileSosContacts(_ProfileId);
        }
    }
    public class SosContacts
    {
        public string ContactPersonName { get; set; }
        public string MobileNo { get; set; }
    }
}
