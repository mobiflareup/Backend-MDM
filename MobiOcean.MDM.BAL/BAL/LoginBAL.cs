using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.LoginDALTableAdapters;
using MobiOcean.MDM.BAL.Model;

/// <summary>
/// Summary description for LoginBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class LoginBAL //: ConstantBAL
    {
        tblUserTableAdapter validate;
        LoginDetailTableAdapter login;
        FirstLoginDetailTableAdapter frstlgn;
        tblUrlTableAdapter tblurl;
        tblClientTableAdapter tblclient;
        DataTable dt;
        public int IswhiteList { get; set; }
        public string Payment { get; set; }
        private string _EmailId, _LoginKey;
        private string _Password;
        private int _UserID;
        private string _NewPassword;
        public int ClientId { get; set; }
        public int RoleId { get; set; }
        public int DeptId { get; set; }
        public string UserName { get; set; }
        public string LoginKey
        {
            get { return _LoginKey; }
            set { _LoginKey = value; }

        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }

        }
        public string Password
        {

            get { return _Password; }
            set { _Password = value; }
        }
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }

        }
        public string NewPassword
        {

            get { return _NewPassword; }
            set { _NewPassword = value; }
        }

        public DateTime currentDateTime { get; set; }

        public LoginBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int AdminValidation()
        {
            validate = new tblUserTableAdapter();
            dt = new DataTable();
            try
            {
                return validate.AdminValidation(_EmailId, _Password) ?? 0;
            }
            finally
            {
                dt = null;
                validate = null;
            }
        }
        public DataTable WhiteListAndBlackList()
        {
            tblurl = new tblUrlTableAdapter();
            dt = new DataTable();
            try
            {
                dt = tblurl.WhiteListAndBlackList(ClientId, IswhiteList);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                return null;
            }
            finally
            {
                dt = null;
                tblurl = null;
            }
        }
        public int IsWhiteListByClientId()
        {
            tblclient = new tblClientTableAdapter();
            try
            {
                return (int)tblclient.GetIsWhiteListByClientId(ClientId);
            }
            finally
            {
                tblclient = null;
            }
        }
        public int IsWhiteListUpdatedByClientId()
        {
            tblclient = new tblClientTableAdapter();
            try
            {
                return (int)tblclient.UpdateIsWhiteListByClientId(IswhiteList, ClientId);
            }
            finally
            {
                tblclient = null;
            }
        }
        public string CheckIsFirstLoginFromUser()
        {
            validate = new tblUserTableAdapter();
            dt = new DataTable();
            try
            {
                dt = validate.GetIsFirstLoginByUser(_EmailId);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["IsFirstLogin"].ToString() == "1" ? "1" : "0";
                }
                return "0";
            }
            finally
            {
                dt = null;
                validate = null;
            }
        }
        public DataTable ValidateLogin()
        {
            validate = new tblUserTableAdapter();
            dt = new DataTable();
            try
            {
                dt = validate.ValidateLogin(_EmailId, _Password);
                return dt;
            }
            finally
            {
                dt = null;
                validate = null;
            }
        }
        public DataTable ValidateLogin1()
        {
            validate = new tblUserTableAdapter();
            dt = new DataTable();
            try
            {
                dt = validate.ValidateLogin1(_EmailId, _Password);
                return dt;
            }
            finally
            {
                dt = null;
                validate = null;
            }
        }
        public DataTable CheckIsFirstLogin()
        {
            validate = new tblUserTableAdapter();
            dt = new DataTable();
            try
            {
                dt = validate.ValidateLogin(_EmailId, _Password);
                return dt;
            }
            finally
            {
                dt = null;
                validate = null;
            }
        }
        public DataTable ChkValidation()
        {
            login = new LoginDetailTableAdapter();
            dt = new DataTable();
            try
            {
                dt = login.CheckValidation(_EmailId, _LoginKey);
                return dt;
            }
            finally
            {
                dt = null;
                login = null;
            }
        }
        public DataTable ChkValidation1()
        {
            login = new LoginDetailTableAdapter();
            dt = new DataTable();
            try
            {
                dt = login.CheckValidation1(_EmailId, _LoginKey);
                return dt;
            }
            finally
            {
                dt = null;
                login = null;
            }
        }
        public DataTable ChkFirstLoginValidation()
        {
            frstlgn = new FirstLoginDetailTableAdapter();
            dt = new DataTable();
            try
            {
                dt = frstlgn.CheckFirstTimeValidation(_EmailId, _LoginKey);
                return dt;
            }
            finally
            {
                dt = null;
                login = null;
            }
        }
        //public int InsertLoginData()
        //{
        //    login = new LoginDetailTableAdapter();
        //    try
        //    {
        //        return login.InsertLoginData(ClientId, UserID, DeptId, RoleId, UserName, _LoginKey, GetCurrentDateTimeByUserId(UserID));
        //    }
        //    finally
        //    {
        //        login = null;
        //    }
        //}
        public int InsertLoginData1()
        {
            login = new LoginDetailTableAdapter();
            try
            {
                return Convert.ToInt16(login.InsertLoginDtls(ClientId, UserID, DeptId, RoleId, UserName, _LoginKey, currentDateTime));
            }
            finally
            {
                login = null;
            }
        }
        public int InsertFirstLoginData()
        {
            frstlgn = new FirstLoginDetailTableAdapter();
            try
            {
                return frstlgn.InsertFirstLoginData(ClientId, UserID, DeptId, RoleId, UserName, _LoginKey, currentDateTime.AddDays(3));
            }
            finally
            {
                login = null;
            }
        }
        public void UpdateFirstLogin()
        {
            validate = new tblUserTableAdapter();
            validate.UpdateFirstLogin(_EmailId);
        }



    }
}
