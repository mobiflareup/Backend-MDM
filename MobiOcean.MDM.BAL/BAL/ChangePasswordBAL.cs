using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.ChangePasswordDALTableAdapters;
/// <summary>
/// Summary description for ChangePasswordBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class ChangePasswordBAL
    {
        ChangePasswordTableAdapter cpta;
        tblUserTableAdapter user;
        DataTable dt;
        private int _UserId;
        private string _Password, _NewPassword, _EmailId;
        public ChangePasswordBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public string NewPassword
        {
            get { return _NewPassword; }
            set { _NewPassword = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public DataTable ChangePassword()
        {
            cpta = new ChangePasswordTableAdapter();
            try
            {
                return cpta.ChangePassword(_UserId, _Password, _NewPassword);
            }
            finally
            {
                cpta = null;
            }
        }

        public int GetUserIdByEmailId()
        {
            user = new tblUserTableAdapter();
            dt = user.GetUserIdByEmailId(_EmailId);
            if (dt.Rows.Count > 0)
            {
                UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
            }
            return UserId;
        }

    }
}
