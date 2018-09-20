using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.SecureDALTableAdapters;

/// <summary>
/// Summary description for SecureBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class SecureBAL
    {
        tblSecureCommunicationTableAdapter secure;
        private int _ClientId, _SecureId, _UpdatedBy, _UserId, _RegId, _CategoryId;
        private DateTime _SubscriptionStartDate, _SubscriptionEndDate, _UpdationDate;
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public int SecureId
        {
            get { return _SecureId; }
            set { _SecureId = value; }
        }
        public int UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int RegId
        {
            get { return _RegId; }
            set { _RegId = value; }
        }
        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        public DateTime SubscriptionStartDate
        {
            get { return _SubscriptionStartDate; }
            set { _SubscriptionStartDate = value; }
        }
        public DateTime SubscriptionEndDate
        {
            get { return _SubscriptionEndDate; }
            set { _SubscriptionEndDate = value; }
        }
        public DateTime UpdationDate
        {
            get { return _UpdationDate; }
            set { _UpdationDate = value; }
        }
        public SecureBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int UpdateIsRegistered()
        {
            secure = new tblSecureCommunicationTableAdapter();
            return secure.UpdateIsRegistered(_SubscriptionStartDate, _SubscriptionEndDate, _UpdatedBy.ToString(), _UpdationDate, _SecureId);
        }
        public DataTable CheckUserByUserId()
        {
            secure = new tblSecureCommunicationTableAdapter();
            return secure.CheckUserByUserId(_UserId);
        }
        public DataTable GetSubscriptionByClientId()
        {
            secure = new tblSecureCommunicationTableAdapter();
            return secure.GetSubscriptionByClientId(_ClientId);
        }
        public int InsertSecureCommunication()
        {
            secure = new tblSecureCommunicationTableAdapter();
            return secure.InsertSecureCommunication(_RegId, _UserId, _CategoryId);
        }
        public DataTable GetCategoryIdFromAppliedSubscriptionDtl()
        {
            secure = new tblSecureCommunicationTableAdapter();
            return secure.GetCategoryIdFromAppliedSubscriptionDtl();
        }
    }
}
