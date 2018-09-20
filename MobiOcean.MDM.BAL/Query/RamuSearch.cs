using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using MobiOcean.MDM.DAL;
/// <summary>
/// Summary description for RamuSearch
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Query
{
    public class RamuSearch
    {
        Search srch;
        string Query;
        public RamuSearch()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable getroalmaster(string RoleCode, string RoleName)
        {
            srch = new Search();
            Query = @"SELECT        RoleId, RoleCode, RoleName, Status, CreatedBy, CreationDate, UpdatedBy, UpdationDate, RowVer FROM dbo.tblRole WHERE        (Status = 0) ";

            if (RoleCode != "")
            {
                Query = Query + " and RoleCode like '%" + RoleCode + "%'";
            }
            if (RoleName != "")
            {
                Query = Query + " and RoleName like '%" + RoleName + "%'";
            }

            Query = Query + " Order by RoleId ";
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable getDeptMaster(int ClientId, string DeptCode, string DeptName)
        {
            srch = new Search();
            Query = @"SELECT        DeptId, ClientId, DeptCode, DeptName, ContactPerson, DeptPhNo, DeptEmailId, NoOfEmployees, Remarks, DeptLogoImage, Status, CreatedBy, CreationDate, 
                         UpdatedBy, UpdationDate, RowVer,ContactPersonId FROM tblDepartment WHERE (Status = 0) and ClientId= " + ClientId + " ";

            if (DeptCode != "")
            {
                Query = Query + " and DeptCode like '%" + DeptCode + "%'";
            }
            if (DeptName != "")
            {
                Query = Query + " and DeptName like '%" + DeptName + "%'";
            }

            Query = Query + " Order by DeptName";
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable getbranchMaster(int ClientId, string DeptCode, string DeptName)
        {
            srch = new Search();
            Query = @"SELECT        BranchId, ClientId, BranchCode, BranchName, ContactPerson, BranchPhNo, BranchEmailId, NoOfEmployees, Remarks, BranchLogoImage, Status, CreatedBy, CreationDate, 
                         UpdatedBy, UpdationDate, RowVer FROM tblBranch WHERE (Status = 0) and ClientId= " + ClientId + " ";

            if (DeptCode != "")
            {
                Query = Query + " and BranchCode like '%" + DeptCode + "%'";
            }
            if (DeptName != "")
            {
                Query = Query + " and BranchName like '%" + DeptName + "%'";
            }

            Query = Query + " Order by BranchName";
            return srch.SearchRecord(Query).Tables[0];
        }

        public DataTable GetAck(int ClientId, string DeviceId, int UserId, int DeptId)
        {
            srch = new Search();
            //        Query = @"Select u.UserName,d.DeviceName,a.AckDateTime,a.AckId,a.UserId,a.ClientId,a.UserDeviceId 
            //  from tblAcknowledgement as a left join tblUserDevice as d on a.UserDeviceId=d.DeviceId 
            //left join tblUser as u on u.UserId=d.UserId
            // WHERE (a.Status = 0) and a.ClientId= " + ClientId + " ";
            Query = @"Select distinct u.UserName,d.DeviceName,ack.AckDateTime,a.UserId,a.ClientId,a.UserDeviceId  from tblAcknowledgement as a
inner join (select distinct userid, max(a.AckDateTime) as AckDateTime  from tblAcknowledgement as a  
where clientid=@clientId group by userid) as ack on ack.AckDateTime=a.AckDateTime and ack.userid=a.userid
left join tblUserDevice as d on a.UserDeviceId=d.DeviceId 
left join tblUser as u on u.UserId=d.UserId 
WHERE (a.Status = 0) and a.ClientId= @clientId ";
            Query = Query.Replace("@clientId", ClientId + "");
            if (UserId > 0)
            {
                Query = Query + @" and ( d.UserId  = @UserId ) ";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( d.UserId in ( select UserId from tblUser where DeptId=@DeptId and  UserId!= @UserId )) ";
                Query = Query.Replace("@DeptId", DeptId + "");
                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeviceId != "0")
            {
                Query = Query + " and UserDeviceId =" + DeviceId + "";
            }
            //if (DeptName != "")
            //{
            //    Query = Query + " and DeptName like '%" + DeptName + "%'";
            //}

            //Query = Query + " Order by cast(AckDateTime as datetime) desc";
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable GetMoreUpdationDetail(int userId)
        {
            srch = new Search();
            Query = @"Select u.UserName,d.DeviceName,a.AckDateTime,a.AckId,a.UserId,a.ClientId,a.UserDeviceId 
          from tblAcknowledgement as a left join tblUserDevice as d on a.UserDeviceId=d.DeviceId 
        left join tblUser as u on u.UserId=d.UserId
        WHERE (a.Status = 0) and a.userid=" + userId + " order by ackdatetime desc";
            return srch.SearchRecord(Query).Tables[0];
        }
    }
}
