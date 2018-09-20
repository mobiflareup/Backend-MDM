using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.DeptDALTableAdapters;

/// <summary>
/// Summary description for DeptBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class DeptBAL
    {
        tblDepartmentTableAdapter dept;
        tblBranchTableAdapter branch;

        private int _DeptId, _ContactPersonId, _ClientId, _helpDelete, _UserId;        
        private string _DeptCode, _DeptName, _DeptEmailId,  _DeptPhNo, _ContactPerson, _NoOfEmployees;             
        
       
      
       
        public int ContactPersonId
        {
            get { return _ContactPersonId; }
            set { _ContactPersonId = value; }
        }
        public int DeptId
        {
            get { return _DeptId; }
            set { _DeptId = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public string DeptCode
        {
            get { return _DeptCode; }
            set { _DeptCode = value; }
        }
        public string DeptName
        {
            get { return _DeptName; }
            set { _DeptName = value; }
        }
        public string ContactPerson
        {
            get { return _ContactPerson; }
            set { _ContactPerson = value; }
        }
        public string DeptEmailId
        {
            get { return _DeptEmailId; }
            set { _DeptEmailId = value; }
        }
        public string DeptPhNo
        {
            get { return _DeptPhNo; }
            set { _DeptPhNo = value; }
        }
        public string NoOfEmployees
        {
            get { return _NoOfEmployees; }
            set { _NoOfEmployees = value; }
        }       
        public int helpDelete
        {
            get { return _helpDelete; }
            set { _helpDelete = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public DeptBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetData()
        {
            dept = new tblDepartmentTableAdapter();
            return dept.GetData();
        }
        //public string InsertDept()
        //{
        //    dept = new tblDepartmentTableAdapter();
        //    return dept.InsertDeptDtls(_DeptId,_DeptCode,_DeptName,_DeptPhNo,_DeptEmailId).ToString();
        //}
        public int DeleteDeptDtls()
        {
            dept = new tblDepartmentTableAdapter();
            return dept.DeleteQuery(_DeptId);
        }
        public int DeleteDeptByClientId()
        {
            dept = new tblDepartmentTableAdapter();
            return dept.DeleteDeptByClientId(_ClientId);
        }
        public int DeleteBranchByClientId()
        {
            branch = new tblBranchTableAdapter();
            return branch.DeleteBranchByClientId(_ClientId);
        }
        public DataTable GetDptNameDDL()
        {
            dept = new tblDepartmentTableAdapter();
            try
            {

                return dept.GetDeptNameForDDL(_ClientId);
            }

            finally
            {
                dept = null;
            }
        }

        public int insrtAddMaster()
        {

            dept = new tblDepartmentTableAdapter();
            return Convert.ToInt32(dept.InsertDeptDtls(_ClientId, _DeptId, _DeptCode, _DeptName, _DeptPhNo, _DeptEmailId, _NoOfEmployees, _ContactPerson, _ContactPersonId, _helpDelete, _UserId));
        }
        public int InsertBranchDtls()
        {

            branch = new tblBranchTableAdapter();
            return Convert.ToInt32(branch.InsertBranchDtls(_ClientId, _DeptId, _DeptCode, _DeptName, _DeptPhNo, _DeptEmailId, _NoOfEmployees, _ContactPerson, _ContactPersonId, _helpDelete, _UserId));
        }
        public DataTable GetBranchName()
        {
            branch = new tblBranchTableAdapter();
            return branch.GetData(_ClientId);
        }
    }
}
