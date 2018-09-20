using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.DDLDALTableAdapters;
using MobiOcean.MDM.BAL.Query;
using MobiOcean.MDM.DAL;
using MobiOcean.MDM.BAL.Model;

/// <summary>
/// Summary description for DDLBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class DDLBAL : ConstantBAL
    {

        tblUserTableAdapter usrdal;
        tblUserDeviceTableAdapter usrdevice;
        Search sc;

        public string UserName { get; set; }
        public int RoleId { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public int DeptId { get; set; }




        public DDLBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetUserByClientId()
        {
            usrdal = new tblUserTableAdapter();
            return usrdal.GetUserByClientId(ClientId);
        }
        public DataTable Custom_GetUser()
        {
            sc = new Search();
            string query = @"  SELECT tu.UserId,tu.BranchId,tu.ClientId,tu.DeptId, tu.RoleId,tu.AttendanceTypeId,tu.UserCode,tu.UserName,tu.MobileNo,tu.EmailId, 
                        tb.BranchName,td.DeptName FROM dbo.tblUser tu 
                        left join tblBranch tb on tu.BranchId=tb.BranchId 
                        left join tblDepartment td on tu.DeptId=td.DeptId 
                        WHERE (tu.ClientId = " + ClientId + ") AND (tu.Status = 0) ";

            if (RoleId == 3)
            {
                query += " and (tu.DeptId = " + DeptId + ") and tu.UserId!=" + UserId;
            }
            if (RoleId > 3)
            {
                query = @" and tu.UserId=" + UserId;
            }
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                query += " and tu.UserName like '%" + UserName + "%'";
            }
            query += " ORDER BY tu.UserName";
            return sc.SearchRecord(query).Tables[0];
        }
        public int Custom_GetUser_For_CheckBox()
        {
            sc = new Search();
            string query = @"  SELECT Count(*) count
                         FROM dbo.tblUser tu 
                        WHERE (tu.ClientId = " + ClientId + ") AND (tu.Status = 0) ";

            if (RoleId == 3)
            {
                query += " and (tu.DeptId = " + DeptId + ") and tu.UserId!=" + UserId;
            }
            if (RoleId > 3)
            {
                query = @" and tu.UserId=" + UserId;
            }
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                query += " and tu.UserName like '%" + UserName + "%'";
            }
            query += "and tu.AttendanceTypeId  like '%7%' ";
            return Convert.ToInt32(sc.SearchRecord(query).Tables[0].Rows[0][0].ToString());
        }
        public bool Up_AttendType(int UserIdTo, string AttendanceType, int UserIdBy)
        {
            usrdal = new tblUserTableAdapter();
            int res = usrdal.sp_UpdateAttendanceType(UserIdTo, AttendanceType, UserIdBy, GetCurrentDateTimeByUserId(UserIdBy).ToString("yyyy-MM-dd HH:mm:ss"));
            return res > 0 ? true : false;
        }
        public DataTable GetUsrByRptMngrWithRpngMngr()
        {
            usrdal = new tblUserTableAdapter();
            return usrdal.GetUsrByRptMngrWithRpngMngr(ClientId, UserId);
        }
        public DataTable GetUserByReporntgMngrWITHOUTmngr()
        {
            usrdal = new tblUserTableAdapter();
            return usrdal.GetUsrByRptMngrWITHOUTrpngMngr(ClientId, UserId);
        }
        public DataTable GetUserByDptHead()
        {
            usrdal = new tblUserTableAdapter();
            return usrdal.GetUsrByDeptHead(ClientId, DeptId);
        }
        public DataTable GetUserWithoutDeptHead()
        {
            usrdal = new tblUserTableAdapter();
            return usrdal.GetUserWithoutDeptHead(ClientId, DeptId, UserId);
        }
        public DataTable GetUserByUserId()
        {
            usrdal = new tblUserTableAdapter();
            return usrdal.GetUserByUserId(ClientId, UserId);
        }
        public DataTable GetUserDeviceByClientId()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.GetUserDeviceByClientId(ClientId);
        }
        public DataTable GetUserDeviceByReprtngMngr()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.GetUserDeviceByRptngMngr(ClientId, UserId);
        }
        public DataTable GetUserDeviceByUserId()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.GetUserDeviceByUserId(ClientId, UserId);
        }
        public DataTable GetUserDeviceByReprtngMngrWITHOUT()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.GetUserDeviceByRptngMngrWITHOUTuserId(ClientId, UserId);
        }
        public DataTable GetUsrDeviceByDeptHead()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.GetUsrDeviceByDeptHead(ClientId, DeptId);
        }
        public DataTable GetUserDeviceWithoutDeptHead()
        {
            usrdevice = new tblUserDeviceTableAdapter();
            return usrdevice.GetUserDeviceWithoutDeptHead(ClientId, DeptId, UserId);
        }
    }
}
