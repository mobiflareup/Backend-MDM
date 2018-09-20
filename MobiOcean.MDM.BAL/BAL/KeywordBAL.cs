using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.KeywordDalTableAdapters;
/// <summary>
/// Summary description for KeywordBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class KeywordBAL
    {
        tblWebKeywordTableAdapter webkeywrd;
        tblKeywordGroupTableAdapter keywrd;
        tblKeywordTableAdapter keyword;
        tblUserTableAdapter usr;
        DataTable dt;
        private int _KeywordId;
        private int _GroupId;
        private int _ClientId;
        private string _KeywordCode;
        private string _KeywordName;
        private string _Description;
        private int _Status;
        private int _UserId;
        private string _LoggedBy;
        private string _RowVer;

        private string _GroupName;

        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int KeywordId
        {
            get { return _KeywordId; }
            set { _KeywordId = value; }
        }
        public int GroupId
        {
            get { return _GroupId; }
            set { _GroupId = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public string KeywordCode
        {
            get { return _KeywordCode; }
            set { _KeywordCode = value; }
        }
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        public string KeywordName
        {
            get { return _KeywordName; }
            set { _KeywordName = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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

        public KeywordBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable getdata()
        {
            keywrd = new tblKeywordGroupTableAdapter();
            dt = new DataTable();
            try
            {
                dt = keywrd.GetData();
                return dt;
            }
            finally
            {
                dt = null;
                keywrd = null;
            }
        }
        public int ChangeKeyStatus()
        {
            try
            {
                keywrd = new tblKeywordGroupTableAdapter();
                return Convert.ToInt32(keywrd.DeleteQuery(_GroupId));
            }
            finally
            {
                keywrd = null;
            }
        }
        public DataTable GetKeywordNameByClientId()
        {
            keyword = new tblKeywordTableAdapter();
            return keyword.GetKeywordNameByClientId(_ClientId);
        }
        public DataTable GetWebKeywordNameByClientId()
        {
            webkeywrd = new tblWebKeywordTableAdapter();
            return webkeywrd.GetWebKeyWordData(_ClientId);
        }
        public DataTable GetKeywordListIfEnable()
        {
            keyword = new tblKeywordTableAdapter();
            return keyword.GetKeywordListIfEnable(_ClientId, _UserId);
        }
        public DataTable GetReprtMngNo()
        {
            usr = new tblUserTableAdapter();
            return usr.GetReprtMngNo(_UserId);
        }
        public string insertIntoKeyword()
        {
            try
            {
                keywrd = new tblKeywordGroupTableAdapter();
                return keywrd.InsertQuery(_ClientId, _KeywordCode, _KeywordName, _Description).ToString();
            }
            finally
            {
                keywrd = null;
            }
        }
        public string IU_WebKeyword()
        {
            try
            {
                webkeywrd = new tblWebKeywordTableAdapter();
                return webkeywrd.IU_WebKeyword(_KeywordId, _ClientId, _KeywordCode, _KeywordName, _Description).ToString();
            }
            finally
            {
                keywrd = null;
            }
        }
        public int DeletekeyDtls()
        {
            try
            {
                keyword = new tblKeywordTableAdapter();
                return Convert.ToInt32(keyword.DeleteQuery(_KeywordId));
            }
            finally
            {
                keyword = null;
            }
        }
        public int DeleteWebkeyWord()
        {
            try
            {
                webkeywrd = new tblWebKeywordTableAdapter();
                return Convert.ToInt32(webkeywrd.DeleteWebKeyWord(_LoggedBy, _KeywordId));
            }
            finally
            {
                keyword = null;
            }
        }
        public string insertKeywordDtl()
        {
            try
            {
                keyword = new tblKeywordTableAdapter();
                return keyword.InsertKeyword(_KeywordId, _ClientId, _KeywordCode, _KeywordName, _Description).ToString();
            }
            finally
            {
                keyword = null;
            }
        }
        public DataTable GetKeywordData()
        {
            keyword = new tblKeywordTableAdapter();
            return keyword.GetData();
        }
    }
}
