using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.RoleDALTableAdapters;

/// <summary>
/// Summary description for RoleBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class RoleBAL
    {
        tblRoleTableAdapter tblRole;
        tblRoleTableAdapter role;
        tblOwnershipTableAdapter Owner;
        DataTable dt;
        private int _RoleId, _ClientId;
        private string _RoleCode, _RoleName;
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
        public string RoleCode
        {
            get { return _RoleCode; }
            set { _RoleCode = value; }
        }
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        public RoleBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetRoleDDL()
        {
            tblRole = new tblRoleTableAdapter();
            return tblRole.GetRoleNameForDDL();
        }
        public DataTable getdata()
        {
            role = new tblRoleTableAdapter();
            dt = new DataTable();
            try
            {
                dt = role.GetData();
                return dt;
            }
            finally
            {
                dt = null;
                role = null;
            }
        }
        public string InsertRole()
        {
            role = new tblRoleTableAdapter();
            return role.InsertRoleData(_RoleId, _RoleCode, _RoleName).ToString();
        }
        public int DeleteRoleType()
        {
            role = new tblRoleTableAdapter();
            return role.UpdateRole(_RoleId);
        }

        public DataTable GetOwnerShipName()
        {
            dt = new DataTable();
            Owner = new tblOwnershipTableAdapter();
            return Owner.GetOwnereshipName();
        }
    }
}
