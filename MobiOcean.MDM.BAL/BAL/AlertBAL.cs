using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.AlertDALTableAdapters;

/// <summary>
/// Summary description for AlertBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class AlertBAL
    {
        tblAlertForCallMsgTableAdapter insertalertdtls;
        GetMobileNoIfEnableTableAdapter getmob;
        tblProfileAlertNoTableAdapter profilealert;
        tblProfileKeywordTableAdapter prokeyword;
        tblAlertTableAdapter alert;
        tblAlertSettingTableAdapter alertst;
        private int _UserId, _AlertId, _ClientId, _ProfileId, _AlertType, _LoggedBy, _ForUserId, _AlerttypeId, _IsEmail;
        private string _MobileNo, _AlertFor, _AlertIdList, _AlertText, _LogDateTime, _AppId;
        private string _KeywordCode;
        private string _KeywordName;
        private string _KeywordDesc;
        private string _IncomingList;
        private string _OutgoingList;

        public int IsEmail
        {
            get { return _IsEmail; }
            set { _IsEmail = value; }
        }
        public int AlerttypeId
        {
            get { return _AlerttypeId; }
            set { _AlerttypeId = value; }
        }



        public string IncomingList
        {
            get { return _IncomingList; }
            set { _IncomingList = value; }
        }
        public string OutgoingList
        {
            get { return _OutgoingList; }
            set { _OutgoingList = value; }
        }
        public string KeywordCode
        {
            get { return _KeywordCode; }
            set { _KeywordCode = value; }
        }
        public string KeywordName
        {
            get { return _KeywordName; }
            set { _KeywordName = value; }
        }
        public string KeywordDesc
        {
            get { return _KeywordDesc; }
            set { _KeywordDesc = value; }
        }
        public string AlertIdList
        {
            get { return _AlertIdList; }
            set { _AlertIdList = value; }
        }
        public int ProfileId
        {
            get { return _ProfileId; }
            set { _ProfileId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int AlertId
        {
            get { return _AlertId; }
            set { _AlertId = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public string AlertFor
        {
            get { return _AlertFor; }
            set { _AlertFor = value; }
        }
        public int AlertType
        {
            get { return _AlertType; }
            set { _AlertType = value; }
        }
        public string AlertText
        {
            get { return _AlertText; }
            set { _AlertText = value; }
        }
        public string LogDateTime
        {
            get { return _LogDateTime; }
            set { _LogDateTime = value; }
        }
        public string AppId
        {
            get { return _AppId; }
            set { _AppId = value; }
        }
        public int LoggedBy
        {
            get { return _LoggedBy; }
            set { _LoggedBy = value; }
        }
        public int ForUserId
        {
            get { return _ForUserId; }
            set { _ForUserId = value; }
        }
        public int IswhiteList { get; set; }
        public int featureId { get; set; }
        public string CountryId { get; set; }
        public AlertBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int InsertAlertDtlsRaj()
        {
            insertalertdtls = new tblAlertForCallMsgTableAdapter();
            return Convert.ToInt32(insertalertdtls.InsertAlertDtlsRaj(_MobileNo, _AlertFor, _AlertId, _ClientId, _UserId, CountryId).ToString());
        }
        public int DeleteAlertDtls()
        {
            insertalertdtls = new tblAlertForCallMsgTableAdapter();
            return Convert.ToInt32(insertalertdtls.DeleteAlertDtls(_AlertId).ToString());
        }
        public int GetMobileNoIfEnable()
        {
            if (_MobileNo.Contains('+'))
            {
                _MobileNo = _MobileNo.Substring(3);
            }
            if (_MobileNo.StartsWith("0"))
            {
                _MobileNo = _MobileNo.Substring(1);
            }
            getmob = new GetMobileNoIfEnableTableAdapter();
            DataTable dt = new DataTable();
            dt = getmob.GetMobileNoIfEnable(_UserId, _ClientId, _MobileNo);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["AlertId"].ToString());

            }
            return 0;
        }
        public DataTable GetAlertNoByProfileId()
        {
            profilealert = new tblProfileAlertNoTableAdapter();
            return profilealert.GetAlertNoByProfileId(_ProfileId);
        }
        public string InsertProfileAlertNoRaj()
        {
            profilealert = new tblProfileAlertNoTableAdapter();
            return profilealert.InsertProfileAlertNoRaj(_ProfileId, _ClientId, _MobileNo, _UserId.ToString(), CountryId).ToString();
        }
        public string MoveAlertDatatoProfile()
        {
            insertalertdtls = new tblAlertForCallMsgTableAdapter();
            return insertalertdtls.MoveAlertDatatoProfile(_ClientId, _ProfileId).ToString();
        }
        public string AssignAlertNoToProfile()
        {
            profilealert = new tblProfileAlertNoTableAdapter();
            return profilealert.AssignAlertNoToProfile(_ProfileId, _AlertIdList, _UserId.ToString()).ToString();
        }

        public string MoveKeywordDatatoProfile()
        {
            prokeyword = new tblProfileKeywordTableAdapter();
            return prokeyword.MoveKeywordDatatoProfile(_ClientId, _ProfileId).ToString();
        }
        public string InsertProfileKeyWord()
        {
            prokeyword = new tblProfileKeywordTableAdapter();
            return prokeyword.InsertProfileKeyWord(_ProfileId, _ClientId, _KeywordCode, _KeywordName, _KeywordDesc, _UserId.ToString()).ToString();
        }
        public string AssignKeyWordToProfile()
        {
            prokeyword = new tblProfileKeywordTableAdapter();
            return prokeyword.AssignKeyWordToProfile(_ProfileId, _AlertIdList, _UserId.ToString()).ToString();
        }
        public DataTable GetKeyWordForProfile()
        {
            prokeyword = new tblProfileKeywordTableAdapter();
            return prokeyword.GetKeyWordForProfile(_ProfileId);
        }
        public string AssignAllowedNoToProfile()
        {
            insertalertdtls = new tblAlertForCallMsgTableAdapter();
            return insertalertdtls.AssignAllowedNoToProfile(_ProfileId, IswhiteList, _IncomingList, _OutgoingList, _UserId.ToString(), featureId, _ClientId).ToString();
        }
        public string AssignAllowedNoToProfileSMS()
        {
            insertalertdtls = new tblAlertForCallMsgTableAdapter();
            return insertalertdtls.AssignAllowedNoToProfileSMS(_ProfileId, IswhiteList, _IncomingList, _OutgoingList, _UserId.ToString(), featureId, _ClientId).ToString();
        }
        public int InsertIntotblAlert()
        {
            alert = new tblAlertTableAdapter();
            DataTable dt = alert.InsertIntotblAlert(_ClientId, _UserId, _AlertType, _AlertText, _LogDateTime, _LoggedBy, _ForUserId);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["IsEmail"].ToString()) ? "0" : dt.Rows[0]["IsEmail"].ToString());
            }
            return 0;
        }
        public DataTable GetDeptHeadAndAdmin()
        {
            alert = new tblAlertTableAdapter();
            return alert.GetDeptHeadAndAdmin(_ClientId, _UserId);
        }
        public DataTable GetAlertDetailsByUserId()
        {
            alert = new tblAlertTableAdapter();
            return alert.GetAlertDetailsByUserId(_UserId);
        }
        public int MarkAlertDetails()
        {
            alert = new tblAlertTableAdapter();
            return Convert.ToInt32(alert.MarkAlertDetails(_AlertIdList, _UserId).ToString());
        }
        public DataTable GetNotificationByUserId()
        {
            alert = new tblAlertTableAdapter();
            return alert.GetNotificationByUserId(_UserId);
        }
        public DataTable GetNotificationCountByUserId()
        {
            alert = new tblAlertTableAdapter();
            return alert.GetNotification1tCountByForeUserId(_UserId);
        }
        public DataTable GetNotification_1ByUserId()
        {
            alert = new tblAlertTableAdapter();
            return alert.GetDNotification1ByForeUserId(_UserId);
        }
        public DataTable GetAlertDetailsByUserIdAndIsRead()
        {
            alert = new tblAlertTableAdapter();
            return alert.GetAlertDetailsByUserIdAndIsRead(_UserId);
        }
        public int EmailStatusUp1()
        {
            alertst = new tblAlertSettingTableAdapter();
            return Convert.ToInt32(alertst.IU_AlertSetting(_IsEmail, _AlerttypeId, _UserId, _LoggedBy.ToString()).ToString());
        }
        public int UpdateAlert_1ByUserId()
        {
            alert = new tblAlertTableAdapter();
            return alert.UpdateAlert(_UserId);
        }

    }
}
