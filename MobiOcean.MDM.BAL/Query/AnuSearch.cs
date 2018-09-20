using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL;
using MobiOcean.MDM.BAL.Model;

/// <summary>
/// Summary description for AnuSearch
/// </summary>
namespace MobiOcean.MDM.BAL.Query
{
    public class AnuSearch
    {
        string Query;
        Search sc;
        DataTable dt;

        public AnuSearch()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable SrchAssignedDailyCustomerListByUserId(int clientId, string UserId, string FrDate, string Todate)
        {
            sc = new Search();
            Query = @"select tdc.*,tc.Name from tblCustomerAssignDaily tdc left join tblCustomer tc on tdc.CustomerId = tc.CustomerId 
                      where tdc.status =0 and tdc.clientid =" + clientId + " and tdc.UserId = " + UserId;
            if (!string.IsNullOrWhiteSpace(FrDate) && !string.IsNullOrWhiteSpace(Todate))
            {

                Query = Query + " and tdc.AssignDate between cast('" + FrDate + "' as date) and cast('" + Todate + "' as date) ";
            }
            Query += @" order by CONVERT(DateTime, AssignDate,101) Desc , CONVERT(DateTime, AssignTime,101)  DESC ";
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable UserDashCallChart(int ClientId, int UserId)
        {
            sc = new Search();
            Query = @"  SELECT Top 7  dt1.date1, dt2.date2, dt1.OutGoing, dt2.InComing FROM 
                                            (SELECT  CAST(CreationDate AS date) AS date1, COUNT(IsIncomingCall) AS OutGoing, IsIncomingCall FROM dbo.tblCallLog WHERE 
                                            (UserId = " + UserId + ") AND (IsIncomingCall = 0) and [ClientId]=" + ClientId + " GROUP BY CAST(CreationDate AS DATE), IsIncomingCall) AS dt1 FULL  JOIN (SELECT CAST(CreationDate AS date) AS date2, COUNT(IsIncomingCall) AS InComing, IsIncomingCall FROM  dbo.tblCallLog AS dt2 WHERE (UserId = " + UserId + ") AND (IsIncomingCall = 1) and [ClientId]=" + ClientId + " GROUP BY CAST(CreationDate AS DATE), IsIncomingCall) AS dt2 ON dt1.date1 = dt2.date2";
            //        Query = @"  SELECT Top 7  dt1.date1, dt2.date2, dt1.OutGoing, dt2.InComing FROM 
            //  (SELECT  CAST(CreationDate AS date) AS date1, COUNT(IsIncomingCall) AS OutGoing, IsIncomingCall FROM dbo.tblCallLog WHERE 
            //(UserId = 43) AND (IsIncomingCall = 0) and [ClientId]=1 GROUP BY CAST(CreationDate AS DATE), IsIncomingCall) AS dt1 FULL  JOIN (SELECT CAST(CreationDate AS date) AS date2, COUNT(IsIncomingCall) AS InComing, IsIncomingCall FROM  dbo.tblCallLog AS dt2 WHERE (UserId = 43) AND (IsIncomingCall = 1) and [ClientId]=1 GROUP BY CAST(CreationDate AS DATE), IsIncomingCall) AS dt2 ON dt1.date1 = dt2.date2";

            return sc.SearchRecord(Query).Tables[0];
        }



        public DataTable UserDashSmsChart(int ClientId, int UserId)
        {
            sc = new Search();
            Query = @"select top 7 dt1.Date1 ,dt2.Date2 ,dt1.OutGoing ,dt2.InComing  from 
                                            (select (cast([LogDateTime] as date)) as date1,count(IsIncoming) as OutGoing ,[IsIncoming] from 
                                            [dbo].[tblSMSLogs] where UserId=" + UserId + " and [IsIncoming]=0 and [ClientId]=" + ClientId + " group by CAST(LogDateTime AS DATE),[IsIncoming]) as dt1  full join (select (cast([LogDateTime] as date)) as date2,count(IsIncoming) as InComing ,[IsIncoming] from [dbo].[tblSMSLogs] where UserId=" + UserId + " and [IsIncoming]=1 and [ClientId]=" + ClientId + " group by CAST(LogDateTime AS DATE),[IsIncoming]) as dt2 on dt1.date1=dt2.date2 ";
            //        Query = @"select top 7 dt1.Date1 ,dt2.Date2 ,dt1.OutGoing ,dt2.InComing  from 
            //  (select (cast([LogDateTime] as date)) as date1,count(IsIncoming) as OutGoing ,[IsIncoming] from 
            //  [dbo].[tblSMSLogs] where UserId=43 and [IsIncoming]=0 and [ClientId]=1 group by CAST(LogDateTime AS DATE),[IsIncoming]) as dt1  full join (select (cast([LogDateTime] as date)) as date2,count(IsIncoming) as InComing ,[IsIncoming] from [dbo].[tblSMSLogs] where UserId=43 and [IsIncoming]=1 and [ClientId]=1 group by CAST(LogDateTime AS DATE),[IsIncoming]) as dt2 on dt1.date1=dt2.date2 ";

            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable UserDashAppChart(int ClientId, int UserId)
        {
            sc = new Search();
            Query = @" SELECT top 7 CAST(CreationDate AS date) AS date ,Sum(Duration/60) AS Durtion  FROM dbo.tblAppLogDtl WHERE
                                            (UserId = " + UserId + ")  and [ClientId]=" + ClientId + " GROUP BY CAST(CreationDate AS DATE)";
            //        Query = @" SELECT top 7 CAST(CreationDate AS date) AS date ,Sum(Duration/60) AS Durtion  FROM dbo.tblAppLogDtl WHERE
            //  (UserId = 43)  and [ClientId]=1 GROUP BY CAST(CreationDate AS DATE)";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SearchClientDtls(string ClientCode, string ClientName)
        {
            sc = new Search();
            Query = @"SELECT ClientId, ClientCode, ClientName, Address, EmailId, PhoneNo, FaxNo, Website, ManagerName, ManagerContactNo, LogoFilepath, Status, CreatedBy, CreationDate, 
                                                 UpdatedBy, UpdationDate, RowVer, DeviceId FROM tblClient WHERE (Status = 0)";
            if (ClientCode != "")
            {
                Query = Query + "and ClientCode like '%" + ClientCode + "%'";
            }
            if (ClientName != "")
            {
                Query = Query + "and ClientName like '%" + ClientName + "%'";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SearchProfileUsrGrpDtls(int ClientId, string GroupId, string ProfileId)
        {
            sc = new Search();
            Query = @"SELECT m.ProfileGroupId, m.GroupId, m.ProfileId, m.ClientId,m.IsEnable, m.AppliedDateTime, m.CancelDateTime, m.Status, g.GrouppName, p.ProfileName 
                                        FROM tblProfileUserGroupMapping as m Left JOIN tblGroup AS g ON g.GroupId = m.GroupId Left JOIN tblProfile AS p ON p.ProfileId = m.ProfileId 
                                        WHERE (m.Status = 0) and m.ClientId=@ClientId";
            Query = Query.Replace("@ClientId", ClientId + "");
            if (GroupId != "0")
            {
                Query = Query + "and m.GroupId=" + GroupId + "";
            }
            if (ProfileId != "0")
            {
                Query = Query + "and m.ProfileId=" + ProfileId + "";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchUserLocationDtls(int ClientId, string UserName, string DeviceName, string FromDate, string ToDate, int UserId, int DeptId = 0)
        {
            sc = new Search();
            Query = @"SELECT        u.UserName,ud.DeviceName,dl.Location,Substring(LogDateTime,1,11) as LogDate,Substring(LogDateTime,13,5) as LogTime,
                                            dl.LocationSource, dl.SrvcCalledBy, dl.Status,ud.UserId,ud.DeviceId,
                        dl.DeviceLocId, dl.ClientId, dl.DeviceId, dl.UserId, dl.MobileNo, dl.Longitude, dl.Latitude, dl.LogDateTime 
                                            FROM            dbo.tblDeviceLocation as dl
                                            left join tblUserDevice as ud on ud.DeviceId=dl.DeviceId left join tblUser as u on u.UserId=ud.UserId where dl.Status=0 and dl.ClientId=@ClientId ";
            Query = Query.Replace("@ClientId", ClientId + "");
            if (UserId > 0)
            {
                Query = Query + @" and ( dl.UserId = @UserId ) ";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( dl.UserId in ( select UserId from tblUser where DeptId=@DeptId and  UserId!= @UserId )) ";
                Query = Query.Replace("@DeptId", DeptId + "");
                Query = Query.Replace("@UserId", UserId + "");
            }
            if (Convert.ToInt32(UserName) > 0)
            {
                Query = Query + " and u.UserId = " + UserName + " ";
            }
            if (DeviceName != "")
            {
                Query = Query + " and ud.DeviceName like '%" + DeviceName + "%' ";
            }
            if (FromDate != "")
            {
                Query = Query + " and cast(dl.LogDateTime as datetime)>=cast('" + FromDate + "' as datetime) ";
            }
            if (ToDate != "")
            {
                Query = Query + " and cast(dl.LogDateTime as datetime)<=cast('" + ToDate + "' as datetime) ";
            }
            Query = Query + " Order by cast(dl.LogDateTime as datetime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchUserWiseCrntLocation(int ClientId, string DeviceName, int? UserName = 0, int UserId = 0, int DeptId = 0)
        {
            sc = new Search();
            Query = @"  select temp.MaxLog as LogDateTime,td.UserId,td.DeviceLocId,ud.DeviceName,tu.UserName,td.ClientId,td.MobileNo,td.Longitude,td.Latitude,td.Location,substring(LogDateTime,1,11) [Date],substring(LogDateTime,13,5) [Time],td.LocationSource,td.SrvcCalledBy from [dbo].[tblDeviceLocation] td join (select UserId as MaxUserId,max(cast(LogDateTime as datetime)) as MaxLog from dbo.tblDeviceLocation where status=0 group by UserId) temp
  on td.UserId=temp.MaxUserId and td.LogDateTime=temp.MaxLog join [dbo].[tblUserDevice] as ud on ud.DeviceId=td.DeviceId
  join [dbo].[tblUser] tu on td.UserId=tu.UserId where  td.ClientId=" + ClientId + " and td.status=0 and tu.status=0";
            if (UserName > 0)
            {
                Query = Query + " and tu.UserId=" + UserName;
            }
            if (!string.IsNullOrWhiteSpace(DeviceName))
            {
                Query += " and ud.DeviceName Like '%" + DeviceName + "%'";
            }
            if (UserId > 0)
            {
                Query = Query + @" and ( dl.UserId = " + UserId + " ) ";

            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( dl.UserId in ( select UserId from tblUser where DeptId=" + DeptId + " and  UserId!= " + UserId + " )) ";
            }
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable GetSubscriptionDetailsBySubId(string SubscribtionId)
        {
            sc = new Search();
            Query = @"select FC.CategoryName, CloudPrice, Isnull(License,'0')License, Isnull(Duration,'0')Duration,Isnull(PaidAmount,'0')PaidAmount from tblSubscriptionDtl sub left join
			  tblFeatureCategory FC on FC.CategoryId = sub.CategoryId
			  where sub.SubscriptionId = " + SubscribtionId;

            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable GetDeviceUserMap(int ClientId, string DeviceName, int? UserName = 0)
        {
            sc = new Search();
            Query = @"
  select temp.MaxLog as LogDateTime,td.UserId,td.DeviceLocId,ud.DeviceName,tu.UserName,td.ClientId,td.MobileNo,td.Longitude,td.Latitude,td.Location,substring(LogDateTime,1,11) [Date],substring(LogDateTime,13,5) [Time],td.LocationSource,td.SrvcCalledBy from [dbo].[tblDeviceLocation] td join (select UserId as MaxUserId,max(cast(LogDateTime as datetime)) as MaxLog from dbo.tblDeviceLocation where status=0 group by UserId) temp
  on td.UserId=temp.MaxUserId and td.LogDateTime=temp.MaxLog join [dbo].[tblUserDevice] as ud on ud.DeviceId=td.DeviceId
  join [dbo].[tblUser] tu on td.UserId=tu.UserId where  td.ClientId=" + ClientId + " and td.status=0 and tu.status=0 and td.Location!= 'Location not found'";
            if (UserName > 0)
            {
                Query = Query + " and tu.UserId=" + UserName;
            }
            if (!string.IsNullOrWhiteSpace(DeviceName))
            {
                Query += " and ud.DeviceName Like '%" + DeviceName + "%'";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchUserSoSLocationDtls(int ClientId, string UserName, string DeviceName, string FromDate, string ToDate, int UserId, int DeptId = 0)
        {
            sc = new Search();
            Query = @"SELECT u.UserName,ud.DeviceName ,dl.Location,Substring(LogDateTime,1,11) as LogDate,
                        Substring(LogDateTime,13,5) as LogTime, dl.Latitude,dl.Longitude,dl.DeviceLocId,u.UserId, dl.ClientId, dl.DeviceId, dl.UserId, ud.MobileNo1,   dl.LogDateTime,
                                             dl.LocationSource, dl.SrvcCalledBy, dl.Status,ud.UserId,ud.DeviceId
                                            FROM  dbo.tblDeviceLocation as dl
                                            left join tblUserDevice as ud on ud.DeviceId=dl.DeviceId 
                        left join tblUser as u on u.UserID=ud.UserID 
                        where dl.ClientId=@ClientId and LocReq=10 ";

            Query = Query.Replace("@ClientId", ClientId + "");
            if (UserId > 0)
            {
                Query = Query + @" and ( dl.UserId = @UserId ) ";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( dl.UserId in ( select UserId from tblUser where DeptId=@DeptId and  UserId!= @UserId )) ";
                Query = Query.Replace("@DeptId", DeptId + "");
                Query = Query.Replace("@UserId", UserId + "");
            }
            if (Convert.ToInt32(UserName) > 0)
            {
                Query = Query + " and u.UserId = " + UserName + " ";
            }
            if (DeviceName != "")
            {
                Query = Query + " and ud.DeviceName like '%" + DeviceName + "%' ";
            }
            if (FromDate != "")
            {
                Query = Query + " and cast(dl.LogDateTime as datetime)>=cast('" + FromDate + "' as datetime) ";
            }
            if (ToDate != "")
            {
                Query = Query + " and cast(dl.LogDateTime as datetime)<=cast('" + ToDate + "' as datetime) ";
            }
            Query = Query + " Order by cast(dl.LogDateTime as datetime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable SrchAppGrp(string GrpCode, string GrpName)
        {
            sc = new Search();
            Query = @"SELECT AppGroupId, AppGroupName, AppGroupCode FROM dbo.tblAppGroup WHERE (Status = 0)";
            if (GrpCode != "")
            {
                Query = Query + "and AppGroupCode like '%" + GrpCode + "%'";
            }
            if (GrpName != "")
            {
                Query = Query + "and AppGroupName like '%" + GrpName + "%'";
            }
            Query = Query + " Order By AppGroupName";
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable GetAppNameByClientIdNotAssigned(int GroupId, int ClientId, string Appname = "")
        {
            sc = new Search();
            Query = "Select a.AppName,a.GroupId,g.AppGroupName from tblAppMaster as a left join tblAppGroup as g on a.GroupId=g.AppGroupId where  a.ClientId=@ClientId and a.status=0 and a.GroupId !=@GroupId  ";
            Query = Query.Replace("@GroupId", GroupId + "");
            Query = Query.Replace("@ClientId", ClientId + "");
            if (Appname != "")
            {
                Query = Query + " and a.AppName like '%" + Appname + "%' ";
            }
            return sc.SearchRecord(Query + " order by AppName").Tables[0];
        }
        public DataTable GetAppNameByClientIdAssigned(int GroupId, int ClientId, string Appname = "")
        {
            sc = new Search();
            Query = "Select a.AppName,a.GroupId,g.AppGroupName from tblAppMaster as a left join tblAppGroup as g on a.GroupId=g.AppGroupId where a.ClientId=@ClientId and a.status=0 and a.GroupId =@GroupId ";
            Query = Query.Replace("@GroupId", GroupId + "");
            Query = Query.Replace("@ClientId", ClientId + "");
            if (Appname != "")
            {
                Query = Query + " and a.AppName like '%" + Appname + "%' ";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetAppNameByProfileIdNotAssigned(int GroupId, int ClientId, int ProfileId, string Appname = "")
        {
            sc = new Search();
            Query = "Select a.AppName,a.GroupId,a.GroupName from tblProfileAppMaster as a where a.ProfileId=@ProfileId and a.status=0 and a.GroupId !=@GroupId ";
            Query = Query.Replace("@GroupId", GroupId + "");
            Query = Query.Replace("@ClientId", ClientId + "");
            Query = Query.Replace("@ProfileId", ProfileId + "");
            if (Appname != "")
            {
                Query = Query + " and a.AppName like '%" + Appname + "%' ";
            }
            return sc.SearchRecord(Query + "  order by a.AppName").Tables[0];
        }
        public DataTable GetAppNameByProfileIdAssigned(int GroupId, int ClientId, int ProfileId, string Appname = "")
        {
            sc = new Search();
            Query = "Select a.AppName,a.GroupId,a.GroupName from tblProfileAppMaster as a where a.ProfileId=@ProfileId and a.status=0 and a.GroupId =@GroupId  ";
            Query = Query.Replace("@GroupId", GroupId + "");
            Query = Query.Replace("@ClientId", ClientId + "");
            Query = Query.Replace("@ProfileId", ProfileId + "");
            if (Appname != "")
            {
                Query = Query + " and a.AppName like '%" + Appname + "%' ";
            }
            return sc.SearchRecord(Query + "  order by a.AppName").Tables[0];
        }
        public DataTable SrchContactDtls(int ClientId, int UserId, string DeviceId, string SyncDateTime, string ContactName, string MobileNo, string IswhiteList, int DeptId = 0)
        {
            sc = new Search();
            Query = @"select c.Contact_Id,c.DeviceId,c.ClientId,c.APPId,c.Mobile_No,c.Contact_Name,c.Contact_Mobile_No1,c.Contact_Mobile_No2
                                       ,c.Contact_Mobile_No3,c.Contact_Mobile_No4,c.Email_Id,c.Messanger_Name,c.Messanger_Id,c.Address,c.Organization_Name,c.Website_link,c.Nick_Name,c.Status,c.LogDateTime,c.IsWhiteList,d.DeviceName,u.UserName
                                       from tblContactSync as c inner join tblUserDevice as d on c.DeviceId=d.DeviceId  Left join tblUser as u on u.UserId=d.UserId   where c.Status=0 and c.ClientId=" + ClientId + " ";
            if (UserId > 0)
            {
                Query = Query + @" and ( d.UserId = @UserId )";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and (u.DeptId=@DeptId  ) ";

                Query = Query.Replace("@DeptId", DeptId + "");

            }
            if (DeviceId != "0")
            {
                Query = Query + " and c.DeviceId= " + DeviceId + " ";
            }
            if (SyncDateTime != "All" && !string.IsNullOrEmpty(SyncDateTime))
            {
                Query = Query + " and c.LogDateTime like '%" + SyncDateTime + "%' ";
            }
            if (ContactName != "")
            {
                Query = Query + " and c.Contact_Name like '%" + ContactName + "%' ";
            }
            if (MobileNo != "")
            {
                Query = Query + " and c.Contact_Mobile_No1 like '%" + MobileNo + "%' ";
            }
            if (IswhiteList != "100")
            {
                Query = Query + " and c.IsWhiteList= " + IswhiteList + " ";
            }
            Query = Query + " Order by c.Contact_Name,cast(c.LogDateTime as DateTime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchCalendarDtls(int ClientId, int UserId, string DeviceId, string SyncDateTime, int DeptId = 0)
        {
            sc = new Search();
            Query = @"select c.CalenderSyncId,c.ClientId,c.DeviceId,c.UserId,c.AppId,c.MobileNo,c.EventName,c.Location,c.StartDateTime,c.EndDateTime,c.Repetition,c.Description,c.SyncDateTime
                              ,c.Status, u.Username,d.DeviceName  FROM tblCalanderSync as c inner join tblUser as u on c.UserId=u.UserId inner join
	                          tblUserDevice as d on c.DeviceId=d.DeviceId where c.Status=0 and  c.ClientId = " + ClientId + "";
            if (UserId > 0)
            {
                Query = Query + @" and ( c.UserId  = @UserId ) ";  //in ( select UserId from tblUser where (UserId

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( u.DeptId = @DeptId)";

                Query = Query.Replace("@DeptId", DeptId + "");
            }
            if (DeviceId != "0")
            {
                Query = Query + " and c.DeviceId= " + DeviceId + " ";
            }
            if (SyncDateTime != "All" && !string.IsNullOrEmpty(SyncDateTime))
            {
                Query = Query + " and c.SyncDateTime like '%" + SyncDateTime + "%' ";
            }
            Query = Query + " Order by cast(c.SyncDateTime as DateTime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchWebsiteLogsDtls(int ClientId, int UserId, string DeviceId, string FrmDate, string ToDate, int DeptId = 0)
        {
            sc = new Search();
            Query = @"select u.UserName,ud.DeviceName,w.Url,w.LogDateTime,
                                             w.Status,ud.UserId,ud.DeviceId,w.WebsiteLogId, w.ClientId from tblWebsiteLog as w
                                        left Join tblUserDevice as ud on ud.DeviceId=w.DeviceId left join tblUser as u on u.Userid=ud.UserId
                                        where w.Status=0 and  ud.ClientId = " + ClientId + "";
            if (UserId > 0)
            {
                Query = Query + @" and ( ud.UserId = @UserId  )";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( ud.UserId in ( select UserId from tblUser where DeptId  = @DeptId ) )";

                Query = Query.Replace("@DeptId", DeptId + "");
            }
            if (DeviceId != "0")
            {
                Query = Query + " and w.DeviceId=" + DeviceId + " ";
            }
            if (FrmDate.Trim() != "")
            {
                Query = Query + " and (cast(w.LogDateTime as date))>=cast('" + FrmDate + "' as date)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + " and (cast(w.LogDateTime as date))<=cast('" + ToDate + "' as date)";
            }
            Query = Query + " Order by cast(w.LogDateTime as DateTime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable srchAppList(int ClientId, int UserId, string ChatGroupId, string ChatApp, int DeviceId, int DeptId = 0)
        {
            sc = new Search();
            Query = @"Select ca.*,u.UserName,ud.DeviceName,ag.AppGroupName from tblChatApp as ca
                                        left join tblUserDevice as ud on ud.DeviceId=ca.DeviceId
                                        left join tblUser as u on u.UserId=ud.UserId
                                        left join tblAppGroup as ag on ag.AppGroupId=ca.ChatGroupId
                                        where ca.Status=0 and  ca.ClientId = " + ClientId + " ";
            if (UserId > 0)
            {
                Query = Query + @" and ( ca.UserId = @UserId)";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( ud.UserId in ( select UserId from tblUser where DeptId=@DeptId and UserId!=@UserId ))";

                Query = Query.Replace("@DeptId", DeptId + "");
                Query = Query.Replace("@UserId", UserId + "");
            }
            if (ChatGroupId != "0")
            {
                Query = Query + " and ca.ChatGroupId= " + ChatGroupId + " ";
            }
            if (ChatApp != "")
            {
                Query = Query + " and ca.ChatApp like '%" + ChatApp + "%' ";
            }
            if (DeviceId != 0)
            {
                Query = Query + " and ca.DeviceId= " + DeviceId + " ";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public string getLocReqFreqMsgByGroupId(int ClientId, int GroupId)
        {
            try
            {
                string msgTxt = "";
                sc = new Search();
                dt = new DataTable();
                Query = @"SELECT l.LocReqFreqId, CONVERT(varchar(5), l.StrtTime) AS StrtTime, CONVERT(Varchar(5), l.EndTime) AS EndTime, l.LocReqFrequency, 
                                      CEILING(ROUND(DATEDIFF(minute, l.StrtTime, l.EndTime) / CAST(l.LocReqFrequency AS float), 0)) AS ttlNoOfReq, ch.GroupId, ch.GrouppName
                                        FROM            tblLocReqFreqMgmt AS l LEFT OUTER JOIN
                                                                 tblGroup AS ch ON ch.GroupId = l.GroupId
                                        WHERE        (l.ClientId = @ClientId) AND (l.Status = 0) 
                                                      and ch.GroupId =" + GroupId;
                Query = Query.Replace("@ClientId", ClientId + "");
                dt = sc.SearchRecord(Query).Tables[0];
                #region----- Make SMS text------
                /*
                                         * GBox set as GP8 TS1640 TE1655 TF10 TS1741 TE1750 TF2
                                         * */
                if (dt.Rows.Count > 0)
                {
                    for (int idx = 0; idx < dt.Rows.Count; idx++)
                    {
                        msgTxt = msgTxt + " TS" + dt.Rows[idx]["StrtTime"].ToString().Replace(":", "") + " TE" + dt.Rows[idx]["EndTime"].ToString().Replace(":", "") + " TF" + dt.Rows[idx]["ttlNoOfReq"].ToString();
                    }

                    msgTxt = "GBox set as GP8" + msgTxt;
                }
                else
                {
                    msgTxt = "NA";
                }
                #endregion

                return msgTxt;
            }
            finally
            {

                dt = null;
            }
        }
        public string getLocReqFreqMsgByProfileId(int ClientId, int ProfileId)
        {
            try
            {
                string msgTxt = "";
                sc = new Search();
                dt = new DataTable();
                Query = @"SELECT l.LocReqFreqId, CONVERT(varchar(5), l.StrtTime) AS StrtTime, CONVERT(Varchar(5), l.EndTime) AS EndTime, l.LocReqFrequency, 
                                      CEILING(ROUND(DATEDIFF(minute, l.StrtTime, l.EndTime) / CAST(l.LocReqFrequency AS float), 0)) AS ttlNoOfReq
                                        FROM           tblLocReqFreqMgmtByProfileId AS l
                                        WHERE l.ClientId=" + ClientId + " and (l.Status = 0) and l.ProfileId =" + ProfileId + "";
                dt = sc.SearchRecord(Query).Tables[0];
                #region----- Make SMS text------
                /*
                                         * GBox set as GP8 TS1640 TE1655 TF10 TS1741 TE1750 TF2
                                         * */
                if (dt.Rows.Count > 0)
                {
                    for (int idx = 0; idx < dt.Rows.Count; idx++)
                    {
                        msgTxt = msgTxt + " TS" + dt.Rows[idx]["StrtTime"].ToString().Replace(":", "") + " TE" + dt.Rows[idx]["EndTime"].ToString().Replace(":", "") + " TF" + dt.Rows[idx]["ttlNoOfReq"].ToString();
                    }

                    msgTxt = "GBox set as GP8" + msgTxt;
                }
                else
                {
                    msgTxt = "NA";
                }
                #endregion

                return msgTxt;
            }
            finally
            {

                dt = null;
            }
        }
        public DataTable SrchReportCallDtls(int ClientId, int UserId, string DeviceId, string SrchNo, string direction, string FrmDate, string ToDate, string FrmTime, string ToTime, int DeptId = 0)
        {
            sc = new Search();
            string Query = @"Select u.UserName,ud.DeviceName,cl.CallFrom,cl.CallTo,cl.StartTime,cl.EndTime,cl.Duration,cl.Location,cl.IsIncomingCall,
                                             cl.Status,ud.UserId,ud.DeviceId,cl.CallLogId, cl.ClientId, cl.DeviceId, cl.UserId, cl.MobileNo 
					                          from tblCallLog as cl
                                                left join tblUserDevice as ud on ud.DeviceId=cl.DeviceId 
                        left join tblUser as u on u.UserId=ud.UserId
                                                where cl.Status=0 and cl.ClientId = " + ClientId + "";

            if (UserId > 0)
            {
                Query = Query + @" and ( cl.UserId  = @UserId)";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( u.DeptId = @DeptId )";

                Query = Query.Replace("@DeptId", DeptId + "");
            }
            if (DeviceId != "0")
            {
                Query = Query + " and cl.DeviceId= " + DeviceId + " ";
            }
            if (SrchNo != "")
            {
                Query = Query + " and ( cl.CallFrom like '%" + SrchNo + "%' or cl.CallTo like '%" + SrchNo + "%' ) ";
            }
            if (direction != "100")
            {
                Query = Query + " and cl.IsIncomingCall= " + direction + " ";
            }
            if (FrmDate.Trim() != "")
            {
                Query = Query + " and (cast(cl.StartTime as date))>=cast('" + FrmDate + "' as date)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + " and (cast(cl.EndTime as date))<=cast('" + ToDate + "' as date)";
            }
            if (FrmTime.Trim() != "HH:MM")
            {
                Query = Query + " and (cast(cl.StartTime as time))>=cast('" + FrmTime + "' as time)";
            }
            if (ToTime.Trim() != "HH:MM")
            {
                Query = Query + " and (cast(cl.EndTime as time))<=cast('" + ToTime + "' as time)";
            }
            Query = Query + " Order by cast(cl.StartTime as DateTime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchReportCallDtlsForChart(int ClientId, int UserId, string DeviceId, string SrchNo, string direction, string FrmDate, string ToDate, string FrmTime, string ToTime, int DeptId = 0)
        {
            sc = new Search();
            string Query = "", Query1 = "", Query2 = "";
            //if (direction == "100")
            //{
            Query = @"Select dt1.date1, dt2.date2, dt2.OutGoing, dt1.InComing from ";
            //}
            //if(direction=="1")
            //{
            //    Query = @"Select dt1.date1, dt1.Incoming from ";
            //}
            //if(direction=="0")
            //{
            //    Query = @"Select  dt2.date2, dt2.InComing from ";
            //}
            Query1 = @"( Select CAST(cl.CreationDate AS date) AS date1,COUNT(cl.IsIncomingCall) as InComing from tblCallLog as cl left join tblUserDevice as ud on 
                        ud.DeviceId=cl.DeviceId  where cl.Status=0 and ud.Status=0 and cl.ClientId = " + ClientId + " and (cl.IsIncomingCall = 1)";
            Query2 = @"( Select CAST(cl1.CreationDate AS date) AS date2,COUNT(cl1.IsIncomingCall) as OutGoing from tblCallLog as cl1 left join tblUserDevice as ud1 on 
                        ud1.DeviceId=cl1.DeviceId  where cl1.Status=0 and ud1.Status=0 and cl1.ClientId = " + ClientId + " and (cl1.IsIncomingCall = 0)";
            if (UserId > 0)
            {
                Query1 = Query1 + @" and ( cl.UserId  = @UserId)";
                Query1 = Query1.Replace("@UserId", UserId + "");
                Query2 = Query2 + @" and ( cl1.UserId  = @UserId)";
                Query2 = Query2.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query1 = Query1 + @" and ( cl.UserId in ( select UserId from tblUser where DeptId = @DeptId ))";
                Query1 = Query1.Replace("@DeptId", DeptId + "");
                Query2 = Query2 + @" and ( cl1.UserId in ( select UserId from tblUser where DeptId = @DeptId ))";
                Query2 = Query2.Replace("@DeptId", DeptId + "");
            }
            if (DeviceId != "0")
            {
                Query1 = Query1 + " and cl.DeviceId= " + DeviceId + " ";
                Query2 = Query2 + " and cl1.DeviceId= " + DeviceId + " ";
            }
            if (SrchNo != "")
            {
                Query1 = Query1 + " and cl.CallFrom like '%" + SrchNo + "%' or cl.CallTo like '%" + SrchNo + "%' ";
                Query2 = Query2 + " and cl1.CallFrom like '%" + SrchNo + "%' or cl1.CallTo like '%" + SrchNo + "%' ";
            }
            if (FrmDate.Trim() != "")
            {
                Query1 = Query1 + " and (cast(cl.StartTime as date))>=cast('" + FrmDate + "' as date)";
                Query2 = Query2 + " and (cast(cl1.StartTime as date))>=cast('" + FrmDate + "' as date)";
            }
            if (ToDate.Trim() != "")
            {
                Query1 = Query1 + " and (cast(cl.EndTime as date))<=cast('" + ToDate + "' as date)";
                Query2 = Query2 + " and (cast(cl1.EndTime as date))<=cast('" + ToDate + "' as date)";
            }
            if (FrmTime.Trim() != "HH:MM")
            {
                Query1 = Query1 + " and (cast(cl.StartTime as time))>=cast('" + FrmTime + "' as time)";
                Query2 = Query2 + " and (cast(cl1.StartTime as time))>=cast('" + FrmTime + "' as time)";
            }
            if (ToTime.Trim() != "HH:MM")
            {
                Query1 = Query1 + " and (cast(cl.EndTime as time))<=cast('" + ToTime + "' as time)";
                Query2 = Query2 + " and (cast(cl1.EndTime as time))<=cast('" + ToTime + "' as time)";
            }
            Query1 = Query1 + " GROUP BY CAST(cl.CreationDate AS DATE), cl.IsIncomingCall ) AS dt1 FULL  JOIN ";
            Query2 = Query1 + Query2 + "GROUP BY CAST(cl1.CreationDate AS DATE), cl1.IsIncomingCall)AS dt2 ON dt1.date1 = dt2.date2 ";
            Query = Query + Query2;
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchReportSmsDtlsForChart(int ClientId, int UserId, string DeviceId, string SrchNo, string direction, string FrmDate, string ToDate, string FrmTime, string ToTime, int DeptId = 0)
        {
            sc = new Search();
            string Query = "", Query1 = "", Query2 = "";
            //if (direction == "100")
            //{
            Query = @"Select dt1.date1, dt2.date2, dt2.OutGoing, dt1.InComing from ";
            //}
            //if(direction=="1")
            //{
            //    Query = @"Select dt1.date1, dt1.Incoming from ";
            //}
            //if(direction=="0")
            //{
            //    Query = @"Select  dt2.date2, dt2.InComing from ";
            //}
            Query1 = @"( Select CAST(cl.CreationDate AS date) AS date1,COUNT(cl.IsIncoming) as InComing from tblSMSLogs as cl left join tblUserDevice as ud on 
                        ud.DeviceId=cl.UserDeviceId  where cl.Status=0 and ud.Status=0 and cl.ClientId = " + ClientId + " and (cl.IsIncoming = 1)";
            Query2 = @"( Select CAST(cl1.CreationDate AS date) AS date2,COUNT(cl1.IsIncoming) as OutGoing from tblSMSLogs as cl1 left join tblUserDevice as ud1 on 
                        ud1.DeviceId=cl1.UserDeviceId  where cl1.Status=0 and ud1.Status=0 and cl1.ClientId = " + ClientId + " and (cl1.IsIncoming = 0)";
            if (UserId > 0)
            {
                Query1 = Query1 + @" and ( cl.UserId  = @UserId)";
                Query1 = Query1.Replace("@UserId", UserId + "");
                Query2 = Query2 + @" and ( cl1.UserId  = @UserId)";
                Query2 = Query2.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query1 = Query1 + @" and ( cl.UserId in ( select UserId from tblUser where DeptId = @DeptId ))";
                Query1 = Query1.Replace("@DeptId", DeptId + "");
                Query2 = Query2 + @" and ( cl1.UserId in ( select UserId from tblUser where DeptId = @DeptId ))";
                Query2 = Query2.Replace("@DeptId", DeptId + "");
            }
            if (DeviceId != "0")
            {
                Query1 = Query1 + " and cl.UserDeviceId= " + DeviceId + " ";
                Query2 = Query2 + " and cl1.UserDeviceId= " + DeviceId + " ";
            }
            if (SrchNo != "")
            {
                Query1 = Query1 + " and cl.SmsFrom like '%" + SrchNo + "%' or cl.SmsTo like '%" + SrchNo + "%' ";
                Query2 = Query2 + " and cl1.SmsFrom like '%" + SrchNo + "%' or cl1.SmsTo like '%" + SrchNo + "%' ";
            }
            if (FrmDate.Trim() != "")
            {
                Query1 = Query1 + " and (cast(cl.LogDateTime as date))>=cast('" + FrmDate + "' as date)";
                Query2 = Query2 + " and (cast(cl1.LogDateTime as date))>=cast('" + FrmDate + "' as date)";
            }
            if (ToDate.Trim() != "")
            {
                Query1 = Query1 + " and (cast(cl.LogDateTime as date))<=cast('" + ToDate + "' as date)";
                Query2 = Query2 + " and (cast(cl1.LogDateTime as date))<=cast('" + ToDate + "' as date)";
            }
            if (FrmTime.Trim() != "HH:MM")
            {
                Query1 = Query1 + " and (cast(cl.LogDateTime as time))>=cast('" + FrmTime + "' as time)";
                Query2 = Query2 + " and (cast(cl1.LogDateTime as time))>=cast('" + FrmTime + "' as time)";
            }
            if (ToTime.Trim() != "HH:MM")
            {
                Query1 = Query1 + " and (cast(cl.LogDateTime as time))<=cast('" + ToTime + "' as time)";
                Query2 = Query2 + " and (cast(cl1.LogDateTime as time))<=cast('" + ToTime + "' as time)";
            }
            Query1 = Query1 + " GROUP BY CAST(cl.CreationDate AS DATE), cl.IsIncoming ) AS dt1 FULL  JOIN ";
            Query2 = Query1 + Query2 + "GROUP BY CAST(cl1.CreationDate AS DATE), cl1.IsIncoming)AS dt2 ON dt1.date1 = dt2.date2 ";
            Query = Query + Query2;
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable BackupSmsReport(int UserId, string MobileNo, string direction, string FrmDate, string ToDate)
        {
            sc = new Search();
            string Query = @"Select u.UserName,ud.DeviceName,cl.SMS,cl.SMSDateTime,cl.IsIncoming,cl.LogId,
                                             cl.Status,ud.UserId,ud.DeviceId, cl.MobileNo from UserSMSLog as cl
                                                left join tblUserDevice as ud on ud.DeviceId=cl.DeviceId 
                        left join tbluser as u on u.UserId=ud.UserId
                                                where cl.Status=0 and cl.UserId = " + UserId + "";

            if (MobileNo != "")
            {
                Query = Query + " and ( cl.MobileNo like '%" + MobileNo + "%' ) ";
            }
            if (direction != "100")
            {
                Query = Query + " and cl.IsIncoming = " + direction + " ";
            }
            if (FrmDate.Trim() != "")
            {
                Query = Query + " and (cast(cl.SMSDateTime as date))>=cast('" + FrmDate + "' as date)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + " and (cast(cl.SMSDateTime as date))<=cast('" + ToDate + "' as date)";
            }


            Query = Query + " Order by cast(cl.SMSDateTime as DateTime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchReportSmsDtls(int ClientId, int UserId, string DeviceId, string SrchNo, string direction, string FrmDate, string ToDate, string FrmTime, string ToTime, int DeptId = 0)
        {
            sc = new Search();
            string Query = @"Select u.UserName,ud.DeviceName,cl.SmsFrom,cl.SmsTo,cl.LogDateTime,cl.MessageText,cl.Location,cl.IsIncoming,
                                             cl.Status,ud.UserId,ud.DeviceId,
                        cl.SMSLogId, cl.ClientId, cl.UserDeviceId, cl.UserId, cl.MobileNo from tblSMSLogs as cl
                                                left join tblUserDevice as ud on ud.DeviceId=cl.UserDeviceId 
                        left join tbluser as u on u.UserId=ud.UserId
                                                where cl.Status=0 and cl.ClientId = " + ClientId + "";
            if (UserId > 0)
            {
                Query = Query + @" and ( cl.UserId  = @UserId) ";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( u.DeptId = @DeptId )";

                Query = Query.Replace("@DeptId", DeptId + "");
            }
            if (DeviceId != "0")
            {
                Query = Query + " and cl.UserDeviceId= " + DeviceId + " ";
            }
            if (SrchNo != "")
            {
                Query = Query + " and ( cl.SmsFrom like '%" + SrchNo + "%' or cl.SmsTo like '%" + SrchNo + "%' ) ";
            }
            if (direction != "100")
            {
                Query = Query + " and cl.IsIncoming = " + direction + " ";
            }
            if (FrmDate.Trim() != "")
            {
                Query = Query + " and (cast(cl.LogDateTime as date))>=cast('" + FrmDate + "' as date)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + " and (cast(cl.LogDateTime as date))<=cast('" + ToDate + "' as date)";
            }
            if (FrmTime.Trim() != "HH:MM")
            {
                Query = Query + " and (cast(cl.LogDateTime as time))>=cast('" + FrmTime + "' as time)";
            }
            if (ToTime.Trim() != "HH:MM")
            {
                Query = Query + " and (cast(cl.LogDateTime as time))<=cast('" + ToTime + "' as time)";
            }
            Query = Query + " Order by cast(cl.LogDateTime as DateTime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchAppLogsDtls(int ClientId, int UserId, string DeviceId, string AppName, string FrmDate, string ToDate, int DeptId = 0)
        {
            sc = new Search();
            Query = @"select u.UserName,ud.DeviceName,w.AppName,w.StartTime,w.EndTime,w.Duration,w.LogDateTime,
                                             w.Status,w.AppLogId, w.ClientId, w.DeviceId, w.UserId, w.MobileNo from tblAppLogDtl as w left Join tblUserDevice as ud on ud.DeviceId=w.DeviceId left join tblUser as u on u.Userid=ud.UserId
                                      inner join ( select AppIndx,DeviceId,cast(LogDateTime as Date) as Date,  MAX(cast(EndTime as datetime)) as EndTime from tblAppLogDtl where ClientId=" + ClientId + " group by AppIndx,DeviceId,cast(LogDateTime as Date))as m on w.AppIndx = m.AppIndx "
               + " and w.DeviceId = m.DeviceId			and w.EndTime=m.EndTime where w.Status=0";
            //Query = Query.Replace("@ClientId", ClientId + "");

            if (UserId > 0)
            {
                Query = Query + @" and ( w.UserId = @UserId )";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( u.DeptId=@DeptId )";

                Query = Query.Replace("@DeptId", DeptId + "");
                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeviceId != "0")
            {
                Query = Query + " and w.DeviceId=" + DeviceId + " ";
            }
            if (AppName != "")
            {
                Query = Query + " and w.AppName like '%" + AppName + "%' ";
            }
            if (FrmDate.Trim() != "")
            {
                Query = Query + " and (cast(w.LogDateTime as date))>=cast('" + FrmDate + "' as date)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + " and (cast(w.LogDateTime as date))<=cast('" + ToDate + "' as date)";
            }
            Query = Query + " Order by cast(w.LogDateTime as DateTime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public string getGeoFenceFreqMsgByCmncatnGpHdrId(int ClientId, int GroupId)
        {
            try
            {
                Query = "";

                sc = new Search();
                dt = new DataTable();



                Query = @"             SELECT         CONVERT(varchar(5), l.StrtTime) AS StrtTime, CONVERT(Varchar(5), l.EndTime) AS EndTime, 
                                                             ch.GroupId, ch.GrouppName, l.Lat,l.Lng, l.LocRadius
                                        FROM            GeoFenceReqFreqMgmt AS l LEFT OUTER JOIN
                                                                 tblGroup AS ch ON ch.GroupId = l.GroupId
                                        WHERE        (l.ClientId = @ClientId) AND (l.Status = 0) 
                                                      and ch.GroupId =" + GroupId;


                //query = query.Replace("@AndroidAppIdList", AndroidAppIdList);
                Query = Query.Replace("@ClientId", ClientId + "");


                dt = sc.SearchRecord(Query).Tables[0];



                return generateMsgfor_GeoFence(dt);
            }
            finally
            {
                sc = null;
                dt = null;
            }
        }
        protected string generateMsgfor_GeoFence(DataTable dt)
        {
            string msgTxt = "";
            try
            {
                #region----- Make SMS text------
                /*
                                         * GBox set as GF TS1212 TE2258 TG10.53274 80.543 1
                                         * */
                if (dt.Rows.Count > 0)
                {
                    for (int idx = 0; idx < dt.Rows.Count; idx++)
                    {
                        msgTxt = msgTxt + " TS" + dt.Rows[idx]["StrtTime"].ToString().Replace(":", "") + " TE" + dt.Rows[idx]["EndTime"].ToString().Replace(":", "") + " TG" + dt.Rows[idx]["Lat"].ToString() + " " + dt.Rows[idx]["Lng"].ToString() + " " + dt.Rows[idx]["LocRadius"].ToString();
                    }

                    msgTxt = "GBox set as GF" + msgTxt;
                }
                else
                {
                    msgTxt = "NA";
                }
                #endregion
            }
            catch (Exception) { }
            return msgTxt;
        }
        public DataTable srchUsrGeoFenceLoc(int ClientId, int UserId, int DeviceId, string FrmDateTime, string ToDateTime, int GeoFence, int DeptId = 0)
        {
            sc = new Search();
            Query = @"Select u.UserName,ud.DeviceName,gf.Location,gf.LogDateTime,gf.IsWithInGeoFence,
                                            gf.LocationSource, gf.SrvcCalledBy, gf.Status,ud.UserId,ud.DeviceId,
                        gf.GeofenceLocId, gf.ClientId, gf.DeviceId, gf.UserId, gf.MobileNo, gf.Longitude, gf.Latitude, gf.LogDateTime  from tblDeviceLocationGeoFence as gf
                                        left join tblUserDevice as ud on ud.DeviceId=gf.DeviceId
                        left join tblUser as u on ud.UserId=u.UserId
                                        where gf.Status=0 and  gf.ClientId = " + ClientId + " ";
            if (UserId > 0)
            {
                Query = Query + @" and (  gf.UserId  = @UserId  )";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( u.DeptId  = @DeptId  )";

                Query = Query.Replace("@DeptId", DeptId + "");
            }
            if (DeviceId != 0)
            {
                Query = Query + " and u.UserId= " + DeviceId + " ";
            }
            if (FrmDateTime.Trim() != "")
            {
                Query = Query + " and (cast(gf.LogDateTime as datetime))>=cast('" + FrmDateTime + "' as datetime)";
            }
            if (ToDateTime.Trim() != "")
            {
                Query = Query + " and (cast(gf.LogDateTime as datetime))<=cast('" + ToDateTime + "' as datetime)";
            }
            if (GeoFence != 100)
            {
                Query = Query + " and gf.IsWithInGeoFence= " + GeoFence + " ";
            }
            Query = Query + " Order by cast(gf.LogDateTime as DateTime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable DeviceLocationSrchForMapPointers(int ClientId, string UserName, string DeviceName, string FromDate, string ToDate)
        {
            sc = new Search();
            Query = @"SELECT        dl.DeviceLocId, dl.ClientId, dl.DeviceId, dl.UserId, dl.MobileNo, dl.Longitude, dl.Latitude, dl.LogDateTime,
                                            Substring(LogDateTime,1,11) as LogDate,Substring(LogDateTime,13,5) as LogTime,
                                            dl.Location, dl.LocationSource, dl.SrvcCalledBy, dl.Status,ud.UserId,ud.UserName,ud.DeviceId,ud.DeviceName 
                                            FROM            dbo.tblDeviceLocation as dl
                                            left join tblUserDevice as ud on ud.DeviceId=dl.DeviceId where dl.Status=0 and  dl.Location != 'Location not found' And dl.ClientId=@ClientId ";
            Query = Query.Replace("@ClientId", ClientId + "");

            if (Convert.ToInt32(UserName) > 0)
            {
                Query = Query + " and dl.UserId = " + UserName + " ";
            }
            if (DeviceName != "")
            {
                Query = Query + " and ud.DeviceName like '%" + DeviceName + "%' ";
            }
            if (FromDate != "")
            {
                Query = Query + " and cast(dl.LogDateTime as datetime)>=cast('" + FromDate + "' as datetime) ";
            }
            if (ToDate != "")
            {
                Query = Query + " and cast(dl.LogDateTime as datetime)<=cast('" + ToDate + "' as datetime) ";
            }
            Query = Query + " order by cast(dl.LogDateTime as datetime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable UserDashBoardDtls(int UserId)
        {
            sc = new Search();
            Query = @"select ud.DeviceId,u.UserName,ud.DeviceName,ud.IsAppInstalled,pum.ProfileId,p.ProfileName,pum.IsEnable,ll.Location,ll.LogDateTime,
                         ll.Latitude,ll.Longitude from tblUserDevice as ud
                        left join tblUser as u on u.UserId=ud.UserId
                                           left join tblProfileUserMapping as pum on pum.UserId=ud.UserId
                                            left join tblProfile as p on p.ProfileId=pum.ProfileId
                                            left join(select dl.DeviceLocId,dl.DeviceId,dl.Longitude,dl.Latitude,dl.Location,dl.LogDateTime from tblDeviceLocation as dl
                                            inner join (Select DeviceId,max(cast(LogDateTime as DateTime)) as LogDateTime from tblDeviceLocation
                                            Where Location !='Location not found' and Status = 0 and Latitude is not Null and Longitude is not Null group by DeviceId) as dlloc on dl.DeviceId=dlloc.DeviceId
					                        and dlloc.LogDateTime=dl.LogDateTime) as ll on ll.DeviceId=ud.DeviceId
					                        where ud.UserId=@ClientId and ud.Status=0 ";
            Query = Query.Replace("@ClientId", UserId + "");
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchTriggerDtls(int ClientId, int UserId, string DeviceId, string FromDate, string ToDate, string TriggerType, int DeptId = 0)
        {
            sc = new Search();
            Query = @" select t.TriggerType,u.UserName,udu.UserName as UName,ud.DeviceName,t.TriggerTime,t.TriggedBy,t.TriggerToUserId,t.TriggerToDeviceId,t.Sms,t.Status,
                                        t.TriggerId
                                        from tblTrigger as t
                                        left join tblUserDevice as ud on ud.DeviceId=t.TriggerToDeviceId and ud.UserId=t.TriggerToUserId
                        left join tblUser as udu on  udu.UserId=t.TriggerToUserId
                                        left join tblUser as u on u.UserId=t.TriggedBy
                                        where t.Status=0 and u.ClientId=@ClientId";
            Query = Query.Replace("@ClientId", ClientId + "");
            if (UserId > 0)
            {
                Query = Query + @" and (  t.TriggerToUserId = @UserId )";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( udu.DeptId = @DeptId )";

                Query = Query.Replace("@DeptId", DeptId + "");
            }

            if (DeviceId != "0")
            {
                Query = Query + " and t.TriggerToDeviceId=" + DeviceId + " ";
            }
            if (TriggerType != "")
            {
                Query = Query + " and t.TriggerType like '%" + TriggerType + "%' ";
            }
            if (FromDate.Trim() != "")
            {
                Query = Query + " and (cast(t.TriggerTime as date))>=cast('" + FromDate + "' as date)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + " and (cast(t.TriggerTime as date))<=cast('" + ToDate + "' as date)";
            }
            //if (TriggerTime != "")
            //{
            //    Query = Query + " and (cast(t.TriggerTime as datetime))>=cast('" + TriggerTime + " 00:00' as datetime) and (cast(t.TriggerTime as datetime))<=cast('" + TriggerTime + " 23:59' as datetime)";
            //}
            Query = Query + " Order by t.TriggerId desc";
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable GetFeatureDtls(int ProfileId)
        {
            sc = new Search();
            Query = @"select f.FeatureId,f.FeatureCode,f.FeatureName,f.FeatureDesc,f.CategoryId,f.IsScheduleNeed,
                                            fc.CategoryName,fm.ProfileId,fm.IsEnable,fm.NotificationOn,fm.LogOn,fm.AutoSyncOn,fm.ProfileFeatureMappingId FROM tblFeature as f 
                                            left join tblFeatureCategory as fc on f.CategoryId=fc.CategoryId 
					                        left join tblProfileFeatureMapping as fm on f.FeatureId=fm.FeatureId and fm.ProfileId=" + ProfileId + " WHERE (f.Status = 0)";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetFeatureTimingDtls(int ProfileFeatureMappingId)
        {
            sc = new Search();
            Query = @"select ProfileFeatureTimingId,FromDay,FromTime,ToTime,Duration from tblProfileFeatureTiming 
                                          where ProfileFeatureMappingId=" + ProfileFeatureMappingId + " and Status=0";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetFeatureHistoryDtls(int ProfileId, string CreationDate)
        {
            sc = new Search();
            Query = @"select f.FeatureId,f.FeatureName,fm.ProfileId,fm.IsEnable,fm.NotificationOn,fm.LogOn,fm.AutoSyncOn,fm.ProfileFeatureMappingId FROM tblFeature as f                   
					                        left join tblProfileFeatureHistory as fm on f.FeatureId=fm.FeatureId WHERE fm.ProfileId=@ProfileId and 
				                            CONVERT(VARCHAR(17),fm.CreationDate,13)='" + CreationDate + "' and (f.Status = 0)";
            Query = Query.Replace("@ProfileId", ProfileId.ToString());
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetFeatureTimingHistoryDtls(int ProfileFeatureMappingId)
        {
            sc = new Search();
            Query = @"select ProfileFeatureTimingId,FromDay,FromTime,ToTime,Duration,Status from tblProfileFeatureTimingHstry  
                                          where ProfileFeatureMappingId=" + ProfileFeatureMappingId + "";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchFileDtls(int ClientId, int UserId, string DeviceId, string FrmDate, string Todate, int IsForSos, int DeptId = 0)
        {
            sc = new Search();
            Query = @"select u.UserName,ud.DeviceName,fd.FTPFileName,fd.LogDateTime,fd.Location,fd.IsAudio,fd.FilePath,fd.IsForSos,fd.Status,
				                        fd.FileId,fd.DeviceId
                                            from tblFilesDetail as fd Left Join tblUserDevice as ud on fd.DeviceId=ud.DeviceId 
                        left join tblUser as u on u.UserId=ud.UserId
                        where fd.Status=0 and 
                        fd.ClientId=" + ClientId + " and fd.IsForSos=" + IsForSos + " ";
            if (UserId > 0)
            {
                Query = Query + @" and ( fd.UserId =@UserId  )";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (DeptId > 0)
            {
                Query = Query + @" and ( u.DeptId= @DeptId ) ";

                Query = Query.Replace("@DeptId", DeptId + "");
            }
            if (DeviceId != "0")
            {
                Query = Query + " and fd.DeviceId=" + DeviceId + " ";
            }
            if (FrmDate != "")
            {
                Query = Query + " and cast(fd.LogDateTime as datetime)>=cast('" + FrmDate + " 00:00' as datetime) ";
            }
            if (Todate != "")
            {
                Query = Query + " and cast(fd.LogDateTime as datetime)<=cast('" + Todate + " 23:59' as datetime) ";
            }
            Query = Query + " order by cast(fd.LogDateTime as datetime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetProfileUserHistory(int ClientId, int ProfileUserId)
        {
            sc = new Search();
            Query = @"select ph.ProfileUserHstryId,ph.UserId,ph.ProfileId,ph.ClientId,ph.IsEnable,ph.CreationDate,
                                        ph.ProfileUserId,ph.DeviceId,u.UserName,p.ProfileName,ud.DeviceName
                                        from tblProfileUserHistory as ph left join tblUser as u on u.UserId=ph.UserId
                                        left join tblUserDevice as ud on ud.DeviceId=ph.DeviceId
                                        left join tblProfile as p on p.ProfileId=ph.ProfileId
                                        where ph.Status=0 and ph.ProfileUserId=" + ProfileUserId + " and ph.ClientId=" + ClientId + "";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetGeoFenceFreqMgmtHstry(int ClientId)
        {
            sc = new Search();
            Query = @"SELECT l.GeoFenceReqFreqHstryId, CONVERT(varchar(5), l.StrtTime) AS StrtTime, CONVERT(Varchar(5), l.EndTime) AS EndTime, l.LocRadius, l.Lat, l.Lng, 
                                        l.LocationName, l.Remarks, g.GroupId, g.GrouppName FROM dbo.GeoFenceReqFreqHstryMgmt AS l LEFT OUTER JOIN
                                        dbo.tblGroup AS g ON g.GroupId = l.GroupId WHERE l.ClientId = " + ClientId + " AND l.Status = 0 ORDER BY g.GrouppName, StrtTime";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetAllowedPhNoHstry(int ClientId)
        {
            sc = new Search();
            Query = @"select AllowedPhNoHstryId,AllowPhnNo,CreationDate,Status from tblAllowedPhNoHstry where ClientId=" + ClientId + " order by  CreationDate desc";
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable GetAlertForDtls(int ClientId, string MobileNo, string AlertFor)
        {
            sc = new Search();
            Query = @"Select TA.*,TC.[CountryName]+'- ('+TC.[PhoneCode]+')' as Country from tblAlertForCallMsg TA
  left join[Country]  TC on TC.CountryId = TA.CountryId
  where TA.Status = 0 and TA.ClientId = " + ClientId + "";
            if (MobileNo != "")
            {
                Query = Query + " and TA.MobileNo like '%" + MobileNo + "%'";
            }
            if (AlertFor != "0")
            {
                Query = Query + " and TA.AlertFor=" + AlertFor + "";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchFeedbackDetails(string FromDate, string ToDate)
        {
            sc = new Search();
            Query = @"SELECT FeedbackId, Name, MobileNo, EmailId, Feedback, Status, CreatedBy, CreationDate, 
                                        UpdatedBy, UpdationDate, RowVer, CompanyName, UserId FROM dbo.tblFeedback WHERE(Status = 0)";
            if (FromDate != "")
            {
                Query = Query + " and Cast(CreationDate as datetime)>=Cast('" + FromDate + " 00:00' as datetime)";
            }
            if (ToDate != "")
            {
                Query = Query + " and Cast(CreationDate as datetime)<=Cast('" + ToDate + " 23:59' as datetime)";
            }
            Query = Query + "order by FeedbackId desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchPartnerDetails(string Name, string FromDate, string ToDate)
        {
            sc = new Search();
            Query = @"SELECT PartnerId, Name, CompanyName, EmailId, MobileNo, Details, Status, CreatedBy, 
                                        CreationDate, UpdatedBy, UpdationDate, RowVer FROM dbo.tblPartner WHERE (Status = 0)";
            if (Name != "")
            {
                Query = Query + " and Name like '" + Name + "'";
            }
            if (FromDate != "")
            {
                Query = Query + " and cast(CreationDate as datetime)>=Cast('" + FromDate + " 00:00' as datetime)";
            }
            if (ToDate != "")
            {
                Query = Query + " and Cast(CreationDate as datetime)<=Cast('" + ToDate + " 23:59' as datetime)";
            }
            Query = Query + "order by PartnerId desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetContactUsDetails(string FromDate, string ToDate)
        {
            sc = new Search();
            Query = "Select * from tblContactUs where Status=0";

            if (FromDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)>=Cast('" + FromDate + " 00:00' as datetime)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)<=Cast('" + ToDate + " 23:59' as datetime)";
            }
            Query = Query + "Order by CreationDate";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetContactSalesDetails(string FromDate, string ToDate)
        {
            sc = new Search();
            Query = "select * from tblPartner where Status=0 and Type_P_Af_CS=3";
            if (FromDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)>=Cast('" + FromDate + " 00:00' as datetime)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)<=Cast('" + ToDate + " 23:59' as datetime)";
            }
            Query = Query + "order by CreationDate";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetPartnerDetailsByType(string FromDate, string ToDate)
        {
            sc = new Search();
            Query = "select * from tblPartner where Status=0 and Type_P_Af_CS=1";
            if (FromDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)>=Cast('" + FromDate + " 00:00' as datetime)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)<=Cast('" + ToDate + " 23:59' as datetime)";
            }
            Query = Query + "order by CreationDate";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetAffiliateDetails(string FromDate, string ToDate)
        {
            sc = new Search();
            Query = "select * from tblPartner where Status=0 and Type_P_Af_CS=2";
            if (FromDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)>=Cast('" + FromDate + " 00:00' as datetime)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)<=Cast('" + ToDate + " 23:59' as datetime)";
            }
            Query = Query + "order by CreationDate";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetDemoProductDetails(string FromDate, string ToDate)
        {
            sc = new Search();
            Query = "select * from tblDemoProduct where Status=0";
            if (FromDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)>=Cast('" + FromDate + " 00:00' as datetime)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)<=Cast('" + ToDate + " 23:59' as datetime)";
            }
            Query = Query + "order by CreationDate";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetSubscriptionDetails(string FromDate, string ToDate)
        {
            sc = new Search();
            Query = "select * from tblNewssubscription where Status=0";
            if (FromDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)>=Cast('" + FromDate + " 00:00' as datetime)";
            }
            if (ToDate.Trim() != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)<=Cast('" + ToDate + " 23:59' as datetime)";
            }
            Query = Query + "order by CreationDate";
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable GetUserReportDtls(int ClientId, string UserName, string MobileNo)
        {
            sc = new Search();
            Query = @"SELECT        u.UserCode, u.UserName, u.EmailId, u.MobileNo,a.UserName as CreatedByUserName,  u.CreationDate,b.UserName as UpdatedByUserName, u.UpdationDate,u.CreatedBy, u.UpdatedBy, u.UserId, u.ClientId, u.DeptId, u.RoleId, u.EmpCompanyId, u.PreferredContactNo, 
                        u.Password, u.DOB, u.Gender, u.RptMngrId, u.DateOfJoining, 
                                                 u.TempAddress, u.PermanentAddress, u.Country, u.State, u.City, u.PinCode, u.ProfileImagePath, u.Status, 
						                         u.RowVer
                        FROM            dbo.tblUser as u
                        left join tblUser as a on a.UserId=u.CreatedBy
                        left join tblUser as b on b.UserId=u.UpdatedBy
                        WHERE        (u.ClientId = " + ClientId + ") AND (u.Status = 0)";
            if (UserName != "")
            {
                Query = Query + " and u.UserName like '%" + UserName + "%' ";
            }
            if (MobileNo != "")
            {
                Query = Query + " and u.MobileNo like '%" + MobileNo + "%'";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetDeptReportDtls(int ClientId, string DeptName)
        {
            sc = new Search();
            Query = @"SELECT        d.DeptName,u.UserName as CreatedByUserName, d.CreationDate, uu.UserName as UpdatedByUserName,d.UpdationDate,d.DeptId, d.ClientId, d.DeptCode,  d.ContactPerson, d.DeptPhNo, d.DeptEmailId, d.NoOfEmployees, d.Remarks, d.DeptLogoImage, d.Status, d.CreatedBy,
                                                 d.UpdatedBy,  d.RowVer
                                FROM   dbo.tblDepartment as d left join tblUser as u on u.UserId=d.CreatedBy
                                left join tblUser as uu on uu.UserId=d.UpdatedBy
                                WHERE        (d.Status = 0) and d.ClientId=" + ClientId + "";
            if (DeptName != "")
            {
                Query = Query + " and d.DeptName like '%" + DeptName + "%' ";
            }

            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetProfileReportDtls(int ClientId, string ProfileName)
        {
            sc = new Search();
            Query = @"SELECT       p.ProfileCode, p.ProfileName, p.ProfilePurpose,u.UserName as CreatedByUserName, p.CreationDate,u.UserName as UpdatedByUserName,p.UpdationDate,p.ProfileId, p.ClientId,  p.Status, p.CreatedBy, 
                                 p.UpdatedBy, p.RowVer, p.ProfileNo, p.OldProfileNo
                                FROM            dbo.tblProfile as p
                                left join tblUser as u on u.UserId=p.CreatedBy
                                left join tblUser as uu on uu.UserId=p.UpdatedBy
                                WHERE        (p.Status = 0) AND (p.ClientId = " + ClientId + ")";
            if (ProfileName != "")
            {
                Query = Query + " and p.ProfileName like '%" + ProfileName + "%' ";
            }

            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetBranchReportDtls(int ClientId, string BranchName)
        {
            sc = new Search();
            Query = @"SELECT        b.BranchName,u.UserName as CreatedByUserName, b.CreationDate,uu.UserName as UpdatedByUserName, b.UpdationDate, b.BranchId, b.ClientId, b.BranchCode, b.ContactPerson, b.BranchPhNo, b.BranchEmailId, b.NoOfEmployees, b.Remarks, b.BranchLogoImage, b.Status, b.CreatedBy, 
                                                 b.UpdatedBy, b.RowVer
                        FROM            dbo.tblBranch as b left join tblUser as u on u.UserId=b.CreatedBy
                        left join tblUser as uu on uu.UserId=b.UpdatedBy WHERE        (b.Status = 0) AND (b.ClientId = " + ClientId + ")";
            if (BranchName != "")
            {
                Query = Query + " and BranchName like '%" + BranchName + "%' ";
            }

            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchUserLastLocationByUserId(int UserId, int ClientId, string LocReq, int Duration = 0, int isIncludeUser = 1, string SOSDateTime = "")
        {
            sc = new Search();
            Query = @"  select ud.UserId,u.UserName,u.MobileNo,ud.DeviceName,ll.LocReq,ll.Location,ll.Latitude,ll.Longitude,ll.LogDateTime from tblUserDevice as ud
                                            inner join tblUser as u on u.UserId=ud.UserId
                                            inner join(select dl.DeviceLocId,dl.DeviceId,dl.Longitude,dl.Latitude,dl.Location,dl.LogDateTime,dl.LocReq from tblDeviceLocation as dl
                                            inner join (Select DeviceId,max(cast(LogDateTime as DateTime)) as LogDateTime from tblDeviceLocation
                                            Where Location !='Location not found'  and Status = 0 and Latitude is not Null and Longitude is not Null group by DeviceId) as dlloc on dl.DeviceId=dlloc.DeviceId
					                        and dlloc.LogDateTime=dl.LogDateTime) as ll on ll.DeviceId=ud.DeviceId
					                        where  ud.ClientId=" + ClientId + " ";
            if (UserId != 0 && isIncludeUser == 1)
            {
                Query = Query + " and ud.UserId = " + UserId + "";
            }
            if (UserId != 0 && isIncludeUser == 0)
            {
                Query = Query + " and ud.UserId != " + UserId + "";
            }
            if (Duration != 0)
            {
                Query = Query + " and DateDiff(mi,cast( \"" + SOSDateTime + "\" as DateTime),cast(LogDateTime as DateTime))<=" + Duration + " and DateDiff(mi,cast(\"" + SOSDateTime + "\" as DateTime),cast(LogDateTime as DateTime)) >=-" + Duration + "";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetSubscriptionDtls(string FromDate, string ToDate, int ClientId)
        {
            sc = new Search();
            Query = @"SELECT SubscriptionId, ClientId, InvoiceNo, SubTotal,  CreationDate, City, Address, GSTNo, EmailId, DiscountAmount, TotalAmount, CGST, SGST, IGST
                                          FROM dbo.tblSubscription
                                          WHERE (Status = 0) and ClientId = @ClientId and IsTrial = 0";
            Query = Query.Replace("@ClientId", ClientId.ToString());
            if (FromDate != "")
            {
                Query = Query + " and Cast(CreationDate as datetime)>=Cast('" + FromDate + " 00:00' as datetime) ";
            }
            if (ToDate != "")
            {
                Query = Query + "and Cast(CreationDate as datetime)<=Cast('" + ToDate + " 23:59' as datetime)";
            }
            Query = Query + " Order By CreationDate desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetArea(string AreaName, int ClientId = 0)
        {
            sc = new Search();
            Query = @"Select AreaId,AreaName,Location,Radius,ClientId,Status,Latitude,Longitude from tblArea where Status=0 and ClientId = @ClientId ";
            if (AreaName != "")
            {
                Query = Query + " and AreaName like '%" + AreaName + "%'";
            }
            Query = Query.Replace("@ClientId", ClientId.ToString());
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetAreaByAreaId(int AreaId)
        {
            sc = new Search();
            Query = @"Select AreaId,AreaName,Location,Radius,ClientId,Status,Latitude,Longitude from tblArea where Status=0 and AreaId=" + AreaId + "";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetSensorDetails(string SensorName, int ClientId)
        {
            sc = new Search();
            Query = @"SELECT        SensorId, ClientId, BranchId, DepartmentId, SensorName, Descripition, BSSID, SSID, Password, Status, CreatedBy, CreationDate, UpdatedBy, UpdationDate, 
                         RowVer FROM            dbo.tblSensor WHERE        (ClientId = " + ClientId + ") AND (Status = 0)";
            if (SensorName != "")
            {
                Query = Query + " and SensorName like '%" + SensorName + "%'";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetSensorDetailsfromtblsensorenable(string username, string devicename, string sensorname, int clientid)
        {
            sc = new Search();
            Query = @"select s.*,u.username,ud.devicename,ss.sensorname from tblSensorEnable as s left join 
    tblUser as u on u.userid=s.userid left join tbluserdevice as ud on ud.deviceid=s.deviceid left join tblsensor as ss on s.sensorid=ss.sensorid
    where s.status=0 and s.ClientId=" + clientid + "";
            if (username != "")
            {
                Query = Query + "and u.username='%" + username + "%'";
            }
            if (devicename != "")
            {
                Query = Query + "and ud.devicename='%" + devicename + "%'";
            }
            if (sensorname != "")
            {
                Query = Query + "and ss.sensorname='%" + sensorname + "%'";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetAttendanceDetails(int clientId, int userId, string username, string mobileno, string fromdate, string todate, string employeeid, int deptId = 0)
        {

            sc = new Search();
            Query = @"Select u.EmpCompanyId,u.UserName,a.AttendanceDate,a.InTime,a.OutTime,a.InLocation,a.OutLocation,a.AttendanceId,
                a.AttendanceStatus,u.mobileno,a.Status,a.AttendanceId,a.ClientId,a.EmployeeId,a.InDateTime,a.OutDateTime,a.InLatitude,
                a.InLongitude ,a.OutLatitude ,a.OutLongitude,IsNull(a.IsInLocationManuallyEntered,0) as IsInLocationManuallyEntered,
                IsNull(a.IsOutLocationManuallyEntered,0) as IsOutLocationManuallyEntered,a.InImagePath,a.OutImagePath
                from tblAttendance as a left join tbluser as u on u.userid=a.Employeeid 
                where a.clientId=" + clientId + "";
            if (userId > 0)
            {
                Query = Query + @" and ( u.UserId  = @UserId)";

                Query = Query.Replace("@UserId", userId + "");
            }
            if (deptId > 0)
            {
                Query = Query + @" and ( u.DeptId  = @DeptId  )";

                Query = Query.Replace("@DeptId", deptId + "");
            }
            if (username != "0")
            {
                Query = Query + " and u.userid = " + username + "";
            }
            if (mobileno != "")
            {
                Query = Query + " and u.mobileno like '%" + mobileno + "%'";
            }
            if (fromdate != "")
            {
                Query = Query + " and cast(a.attendanceDate as Date) >= cast('" + fromdate + "' as Date)";
            }
            if (todate != "")
            {
                Query = Query + " and cast(a.attendanceDate as Date) <= cast('" + todate + "' as Date)";
            }
            if (employeeid != "")
            {
                Query = Query + " and u.EmpCompanyId like'%" + employeeid + "%'";
            }
            Query = Query + " Order By a.CreationDate Desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetConveyaRt(int userid)
        {
            sc = new Search();
            Query = "Select * from [dbo].[tblConveyanceKM] where userid = " + userid;
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetConveyanceDetails(int clientId, int userId, string username, string mobileno, string fromdate, string todate, string employeeid, int deptId = 0)
        {
            sc = new Search();
            //Query = @"SELECT        u.empcompanyid,u.username,c.LogDateTime,c.ToLogDateTime,c.FromLocation, c.ToLocation,c.Distance,c.Remark,u.mobileno,c.ConveyanceId, c.ClientId, c.UserId,
            //        c.FromLatitude, c.ToLatitude, c.FromLongitude, c.ToLongitude,  
            //        c.IsApproved, c.ApprovedBy,  c.Status,  c.ImagePath,c.UpdationDate,c.ConveyanceHistoryId,c.UserId
            //        FROM            dbo.tblConveyance as c left join tblUser as u on u.userid=c.userid
            //        WHERE        (c.Status = 0) and c.IsApproved=0 and c.clientid=" + clientId + " ";
            Query = @"SELECT        u.empcompanyid,u.username,c.LogDateTime,c.ToLogDateTime,c.FromLocation, c.ToLocation,c.Distance,c.Remark,cd.VehicleStartReading,
                cd.StartTimeRemark,cd.VehicleStopReading,cd.StopTimeRemark,cd.Distance as VehicleReadingDistance,cd.StartTimeImage,cd.StopTimeImage,u.mobileno,c.ConveyanceId, c.ClientId, c.UserId,
                c.FromLatitude, c.ToLatitude, c.FromLongitude, c.ToLongitude,  
                c.IsApproved, c.ApprovedBy,  c.Status,  c.ImagePath,c.UpdationDate,c.ConveyanceHistoryId,c.UserId
                FROM            dbo.tblConveyance as c 
				left join tblconveyancedetails as cd on cd.conveyanceid=c.conveyanceid
				left join tblUser as u on u.userid=c.userid
                WHERE        (c.Status = 0) and c.IsApproved=0 and c.clientid=" + clientId + "";
            if (userId > 0)
            {
                Query = Query + @" and ( u.UserId  = @UserId)";

                Query = Query.Replace("@UserId", userId + "");
            }
            if (deptId > 0)
            {
                Query = Query + @" and ( u.DeptId  = @DeptId  )";

                Query = Query.Replace("@DeptId", deptId + "");
            }
            if (username != "0")
            {
                Query = Query + " and u.userid = " + username + "";
            }
            if (mobileno != "")
            {
                Query = Query + " and u.mobileno like '%" + mobileno + "%'";
            }
            if (fromdate != "")
            {
                Query = Query + " and cast(c.LogDateTime as datetime)>=cast('" + fromdate + " 00:00' as datetime)";
            }
            if (todate != "")
            {
                Query = Query + " and cast(c.LogDateTime as datetime)<=cast('" + todate + " 23:59' as datetime)";
            }
            if (employeeid != "")
            {
                Query = Query + " and u.EmpCompanyId like'%" + employeeid + "%'";
            }
            Query = Query + " order by cast(c.LogDateTime as datetime) desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetApprovedConveyanceDetails(int clientId, int userId, string username, string fromdate, string todate, int deptId = 0)
        {
            sc = new Search();
            Query = @"SELECT        u.UserName, ac.FromDate, ac.ToDate, ac.TotalDistance, ac.ConveyanceRate, 
                    ac.TotalAmount ,a.UserName as ApprovedByUserName ,ac.ApprovedBy,ac.ApprovedConveyanceId, ac.UserId,ac.Status FROM  dbo.tblApprovedConveyance as ac left join tblUser as u on u.UserId=ac.UserId
                    left join tblUser as a on a.UserId=ac.approvedBy
                    WHERE        (ac.Status = 0) and ac.clientid=" + clientId + "";
            if (userId > 0)
            {
                Query = Query + @" and ( u.UserId  = @UserId)";

                Query = Query.Replace("@UserId", userId + "");
            }
            if (deptId > 0)
            {
                Query = Query + @" and ( u.DeptId  = @DeptId  )";

                Query = Query.Replace("@DeptId", deptId + "");
            }
            if (username != "0")
            {
                Query = Query + " and u.userid = " + username + "";
            }

            if (fromdate != "")
            {
                Query = Query + " and cast(ac.FromDate as date)>=cast('" + fromdate + " ' as date)";
            }
            if (todate != "")
            {
                Query = Query + " and cast(ac.ToDate as date)<=cast('" + todate + " ' as date)";
            }

            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetConveyanceDetailsForPopup(int ApprovedConveyanceId)
        {
            sc = new Search();
            Query = @"Select ConveyanceId,LogDateTime,Fromlocation,Tolocation,Remark,Imagepath,Distance from tblConveyance 
                where conveyancehistoryid=" + ApprovedConveyanceId + "";
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable GetLomentUserbymailid(string emailid)
        {
            sc = new Search();
            //Query = @"SELECT lu.* from Lomentuser as lu where lu.LomentUserName = '" + emailid+"'";
            Query = @"SELECT lu.*,ls.keys as keyss from Lomentuser as lu   left join 
[LomentUserFeature]  as ls on lu.LomentUserId=ls.LomentUserId 
where lu.status=0 and Lomentusername='" + emailid + "'";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetCommunictionDetail(int clientid, int userid, int deptid, int branchid, int CategoryId)
        {
            sc = new Search();
            Query = @"select DISTINCT userid, * from (select u.Emailid, u.username,u.password,u.userid,u.MobileNo,s.LomentId,s.Lomentusername,lsP.featureid as Peanut,lsW.featureid2 as Walnut,lsC.featureid3 as Cashew,lsK.Keys
                    from tbluser as u left join (select * from Lomentuser where status = 0) as s on s.userid=u.userid
                    left join (select * from [LomentUserFeature] where status = 0 and featureid = 1) as lsP on s.LomentUserId=lsP.LomentUserId 
                    left join (select *,1 as featureid2 from [LomentUserFeature] where status = 0 and featureid = 2) as lsW on s.LomentUserId=lsW.LomentUserId 
                    left join (select *,1 as featureid3 from [LomentUserFeature] where status = 0 and featureid = 3) as lsC on s.LomentUserId=lsC.LomentUserId 
                    left join (select * from [LomentUserFeature] where status = 0) as lsK on s.LomentUserId=lsK.LomentUserId 
                     where u.status=0 
                    and u.clientid=" + clientid + " ";
            if (userid > 0)
            {
                Query = Query + @" and (u.UserId in ( select UserId from tblUser where status=0 and  RptMngrId = @UserId ) ) ";

                Query = Query.Replace("@UserId", userid + "");
            }
            if (deptid > 0)
            {
                Query = Query + @" and u.DeptId = @DeptId ";

                Query = Query.Replace("@DeptId", deptid + "");

            }
            if (branchid > 0)
            {
                Query = Query + @" and u.BranchId = @BranchId ";

                Query = Query.Replace("@BranchId", branchid + "");

            }
            Query = Query + ") as a ";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetUserByClientId(int clientid, string username)
        {
            sc = new Search();
            Query = @"SELECT        UserId, ClientId, DeptId, RoleId, EmpCompanyId, UserCode, UserName, MobileNo, PreferredContactNo, EmailId, 
                  Password, DOB, Gender, RptMngrId, DateOfJoining,TempAddress, PermanentAddress, Country, State, City, PinCode, ProfileImagePath,
                  Status, CreatedBy, CreationDate, UpdatedBy, UpdationDate, RowVer
                  FROM  dbo.tblUser WHERE (ClientId = " + clientid + ") AND (Status = 0) ";
            if (username != "")
            {
                Query = Query + " and username like'%" + username + "%' ";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public int Notify(string UserId, string Notify)
        {
            sc = new Search();
            Query = @"Update dbo.tblUser set DateOfJoining = " + Notify + " where  UserId = " + UserId + " ";
            return sc.notifysrc(Query);
        }
        public DataTable GetCustomerByClientIdNotAssigned(int userid, int clientid, string custname)
        {
            sc = new Search();
            Query = @"select c.* from tblcustomer as c where
                c.customerid not in (select customerid from tblCustomerAssignment where status=0 and userid=" + userid + ") and  c.clientid=" + clientid + " and c.status=0";
            if (custname != "")
            {
                Query = Query + " and Name like '%" + custname + "%'";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetCustomerByClientIdAssigned(int userid, int clientid, string custname)
        {
            sc = new Search();
            Query = @"select c.* from tblcustomer as c where
                 c.customerid in (select customerid from tblCustomerAssignment where status=0 
				 and userid=" + userid + ")  and c.clientid=" + clientid + " and c.status=0 ";
            if (custname != "")
            {
                Query = Query + "and Name like'%" + custname + "%'";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetAllCustomerByClientId(int userid, int clientid, string custname)
        {
            sc = new Search();
            Query = @"select c.* from tblcustomer as c where  
                 c.clientid=" + clientid + " and c.status=0 ";
            if (custname != "")
            {
                Query = Query + " and c.Name like '%" + custname + "%'";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetOrderDetails(string custname, string srchuserid, string payment, string approve, string fromdate, string todate)
        {
            sc = new Search();
            Query = @"SELECT        u.username,OrderMasterId, CustomerId, CustomerName, PersonName, ContactNo, Address, OrderStatus, ApprovedBy, OrderNo, OrderDate, ExpectedDate, OrderType, OrderBy, 
                         TotalAmount, TotalItem, Latitude, Longitude, Location, IsPaymentReceived, Freight, PaymentInfo, Vat,
                         LogDateTime FROM            dbo.tblOrderMaster as om 
						 left join tbluser as u on om.orderby=u.userid
						  WHERE        (om.Status = 0) ";
            if (custname != "")
            {
                Query = Query + " and CustomerName like '%" + custname + "%' ";
            }
            if (srchuserid != "0")
            {
                Query = Query + " and u.UserId like '%" + srchuserid + "%' ";
            }
            if (payment != "100")
            {
                Query = Query + " and IsPaymentReceived like '%" + payment + "%' ";
            }
            if (approve != "100")
            {
                Query = Query + " and OrderStatus like '%" + approve + "%' ";
            }
            if (fromdate != "")
            {
                Query = Query + " and Cast(LogDateTime as datetime)>=cast( '" + fromdate + " 00:00' as datetime) ";
            }
            if (todate != "")
            {
                Query = Query + " and Cast(LogDateTime as datetime)<=cast( '" + todate + " 23:59' as datetime)  ";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetCustomerFromtblCustomerTemp(int clientid, string name, string mblno, string emailid, string contactperson)
        {
            sc = new Search();
            Query = @"SELECT        u.username,c.CustomerTempId, c.CustomerId, c.ClientId, c.Name, c.MobileNo, c.ALtMobileNo, c.ContactPersion, c.AltContactPersion, c.EmailId, 
                         c.AltEmailId, c.Address, c.AltAddress, c.Latitude, 
                         c.Longitude, c.City, c.District, c.state, c.country, c.PinCode, c.TinNumber, c.Status, c.IsCustomer, c.CreatedBy
                         FROM            dbo.tblCustomerTemp as c left join tbluser as u on u.userid=c.CreatedBy
                         WHERE        (c.Status = 0) AND (c.ClientId = " + clientid + ") ";
            if (name != "")
            {
                Query = Query + " and c.name like '%" + name + "%' ";
            }
            if (mblno != "")
            {
                Query = Query + " and c.MobileNo like '%" + mblno + "%' ";
            }
            if (emailid != "")
            {
                Query = Query + " and c.EmailId like '%" + emailid + "%' ";
            }
            if (contactperson != "")
            {
                Query = Query + " and c.ContactPersion like '%" + contactperson + "%' ";
            }
            Query = Query + " order by c.Iscustomer asc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetCustomerDetails(int clientid, string cname, string mblno, string emailid, string contactperson)
        {
            sc = new Search();
            Query = @"  Select  TC.*,TC1.[CountryName]+'- ('+TC1.[PhoneCode]+')' as CountryId1,TC2.[CountryName]+'- ('+TC2.[PhoneCode]+')' as AltCountryId1
                FROM            dbo.tblCustomer TC 
				left join [Country]  TC1 on TC1.CountryId = TC.CountryId
				left join [Country]  TC2 on TC2.CountryId = TC.AltCountryId  where (TC.ClientId = " + clientid + ") AND (TC.Status = 0)";
            if (cname != "")
            {
                Query = Query + "and TC.Name like'%" + cname + "%'";
            }
            if (mblno != "")
            {
                Query = Query + "and TC.MobileNo like'%" + mblno + "%'";
            }
            if (emailid != "")
            {
                Query = Query + "and TC.EmailId like'%" + emailid + "%'";
            }
            if (contactperson != "")
            {
                Query = Query + "and TC.ContactPersion like'%" + contactperson + "%'";
            }
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetConveyanceDetailsByConveyanceId(int conveyanceId)
        {
            sc = new Search();
            Query = @"SELECT        u.empcompanyid,u.username,c.LogDateTime,c.ToLogDateTime,c.FromLocation, c.ToLocation,c.Distance,c.Remark,cd.VehicleStartReading,
                cd.StartTimeRemark,cd.VehicleStopReading,cd.StopTimeRemark,cd.Distance as VehicleReadingDistance,cd.StartTimeImage,cd.StopTimeImage,u.mobileno,c.ConveyanceId, c.ClientId, c.UserId,
                c.FromLatitude, c.ToLatitude, c.FromLongitude, c.ToLongitude,  
                c.IsApproved, c.ApprovedBy,  c.Status,  c.ImagePath,c.UpdationDate,c.ConveyanceHistoryId,c.UserId
                FROM            dbo.tblConveyance as c 
				left join tblconveyancedetails as cd on cd.conveyanceid=c.conveyanceid
				left join tblUser as u on u.userid=c.userid
                WHERE        (c.Status = 0) and c.IsApproved=0 and c.conveyanceid=" + conveyanceId + "";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetFileUploadSearchList(string FileName, string Type, int ClientId)
        {
            sc = new Search();
            Query = @"SELECT Id, UserFileName, ApplicatioFileName, FileType, FileSize, FilePath, convert(varchar(10), CreationDate, 120) CreationDate FROM dbo.tblFileUpload WHERE ClientId =" + ClientId;
            if (FileName != "")
            {
                Query = Query + " and UserFileName like '%" + FileName + "%'";
            }
            if (Type != "")
            {
                Query = Query + " and FileType like '%" + Type + "%'";
            }
            Query = Query + " Order By Id desc";
            return sc.SearchRecord(Query).Tables[0];
        }

        public DataTable GetFileUploadUserSearchList(string UserName, string UserCode, int ClientId, int FileId, string BranchName, string DeptName)
        {
            try
            {
                sc = new Search();
                Query = @"SELECT td.DeptName,tb.BranchName,c1.UserId, c1.EmpCompanyId, c1.UserCode, c1.UserName, c1.Gender, c1.EmailId, c2.Permission, c2.UserId AS AssignUser FROM dbo.tblUser c1 LEFT JOIN dbo.tblAssignFileToUser c2 ON c1.UserId = c2.UserId and c2.FileId = " + FileId + @" 
                left join  tblBranch tb on tb.branchid = c1.branchid left join tblDepartment td on td.DeptId = c1.DeptId WHERE c1.ClientId =" + ClientId;
                if (UserName != "")
                {
                    Query = Query + " and c1.UserName like '%" + UserName + "%'";
                }
                if (UserCode != "")
                {
                    Query = Query + " and c1.UserCode like '%" + UserCode + "%'";
                }
                if (BranchName.Trim() != "")
                {
                    Query = Query + " and tb.BranchName like '%" + BranchName + "%' ";
                }
                if (DeptName.Trim() != "")
                {
                    Query = Query + " and td.DeptName like '%" + DeptName + "%' ";
                }
                Query = Query + "order by c1.Username";
                return sc.SearchRecord(Query).Tables[0];
            }
            finally
            {
                sc = null;
            }
        }
        public DataTable GetLTCollegeVisitDetails(int clientId, int userId, string username, string mobileno, string customername, string fromdate, string todate, string employeeid, int deptId = 0)
        {
            sc = new Search();
            Query = @"SELECT       c.Name,u.empcompanyid,u.username,lt.InLogDateTime,lt.OutLogDateTime,lt.InLocation,lt.OutLocation, lt.InVerification, lt.OutVerification,lt.InTime,lt.OutTime,
                lt.LtCollegeVisitId,c.latitude as cusomerlatitude,c.longitude as customerlongitude,IsNull(lt.IsInLocationManuallyEntered,0) as IsInLocationManuallyEntered,IsNull(lt.IsOutLocationManuallyEntered,0) as IsOutLocationManuallyEntered,lt.InLatitude,lt.OutLatitude,lt.InLongitude,lt.OutLongitude              
                FROM            dbo.LTCollegeVisit as lt
                left join tbluser as u on u.userid=lt.userid
                left join tblcustomer as c on c.customerid=lt.collegeid
                WHERE (lt.Status = 0) and lt.ClientId=" + clientId + " ";
            if (userId > 0)
            {
                Query = Query + @" and ( u.UserId  = @UserId)";

                Query = Query.Replace("@UserId", userId + "");
            }
            if (deptId > 0)
            {
                Query = Query + @" and ( u.DeptId  = @DeptId  )";

                Query = Query.Replace("@DeptId", deptId + "");
            }
            if (username != "0")
            {
                Query = Query + " and u.userid = " + username + "";
            }
            if (mobileno != "")
            {
                Query = Query + " and u.mobileno like '%" + mobileno + "%'";
            }
            if (customername != "0")
            {
                Query = Query + " and c.CustomerId  = " + customername + "";
            }
            if (fromdate != "")
            {
                Query = Query + " and cast(lt.InLogDateTime as Date) >= cast('" + fromdate + "' as Date)";
            }
            if (todate != "")
            {
                Query = Query + " and cast(lt.InLogDateTime as Date) <= cast('" + todate + "' as Date)";
            }
            if (employeeid != "")
            {
                Query = Query + " and u.EmpCompanyId like'%" + employeeid + "%'";
            }
            Query = Query + " Order By lt.CreationDate Desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetLTCollegeVisitDetailsToday(int clientId, string visitdate)
        {
            sc = new Search();
            Query = @"select tu.UserName As Name, count(tu.UserName) As Visit_Count  from tbluser tu 
                    left join 
                    dbo.LTCollegeVisit  LT
                    on tu.UserId = LT.UserId where LT.ClientId = " + clientId + " and cast(LT.CreationDate as Date)  >= cast(getdate() as Date) group by tu.UserName";
            //Query = Query + " Order By lt.CreationDate Desc";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetUniqueLTCollegeVisitDetailsToday(int clientId, string fromdate, string todate)
        {
            sc = new Search();
            Query = @"select SUM(Name) As Unique_User_Count, isnull(sum(Visit_Count), 0) As Total_Visit_Count, isnull(count(College_Count), 0) As Unique_College_Count from
                    (
                    select count(userid)Name,sum(Visit_Count)Visit_Count,count(CollegeId)College_Count from
                    (
                    select LT.UserId as userid, count(tu.UserName) As Visit_Count, count(LT.CollegeId) As College_Count, LT.CollegeId  from tbluser tu 
                    left join 
                    dbo.LTCollegeVisit  LT
                    on tu.UserId = LT.UserId
                    where LT.ClientId = " + clientId + " and cast(LT.CreationDate as Date)  >= cast('" + fromdate + "' as Date) and cast(LT.CreationDate as Date)  <= cast('" + todate + "' as Date)  group by LT.UserId, LT.CollegeId )           as a group by CollegeId	 )as b";

            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable CheckRemarkANdImageByVisitId(string VisitId)
        {
            sc = new Search();
            string Query = @"select ltc.LTCollegeVisitId, ltc.Remark, ltc1.ImagePath from 
(select top 1 LTCollegeVisitId, Remark from LTCollegeVisitRemark where LTCollegeVisitId =" + VisitId + @" and Remark is not null) 
ltc left join	(select top 1  LTCollegeVisitId,ImagePath from LTCollegeVisitRemark where LTCollegeVisitId =" + VisitId + " and ImagePath is not null) ltc1 on 1=1";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable ViewCustomerDailyTask(int clientId, string EmployeeName, string CustomerName, string FrDate, string Todate)
        {
            sc = new Search();
            Query = @"select tdc.*,tc.Name,u.UserName,u.UserId from tblCustomerAssignDaily tdc left join tblCustomer tc on tdc.CustomerId = tc.CustomerId 
                      left join tbluser as u on u.userid=tdc.userid
                      where tdc.status =0 and tdc.clientid =" + clientId + " ";
            if (!string.IsNullOrEmpty(EmployeeName))
            {
                Query = Query + " and u.username like '%" + EmployeeName + "%'";
            }
            if (!string.IsNullOrEmpty(CustomerName))
            {
                Query = Query + " and tc.Name like '%" + CustomerName + "%'";
            }
            if (!string.IsNullOrWhiteSpace(FrDate) && !string.IsNullOrWhiteSpace(Todate))
            {
                Query = Query + " and tdc.AssignDate between cast('" + FrDate + "' as date) and cast('" + Todate + "' as date) ";
            }
            Query += @" order by CONVERT(DateTime, AssignDate,101) Desc , CONVERT(DateTime, AssignTime,101)  DESC ";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable CategoryGrp(int clientId)
        {
            sc = new Search();
            string Query = @"
  select  AppDtl.CategoryId,AppDtl.License,FeaCat.CategoryName
from tblAppliedSubscription as AppSub
 left join tblAppliedSubscriptionDtl as AppDtl 
on AppSub.AppliedSubscriptionId = AppDtl.AppliedSubscriptionId
left join tblFeatureCategory as FeaCat 
  on FeaCat.CategoryId= AppDtl.CategoryId 
 where FeaCat.status=0 and AppSub.ClientId=" + clientId + "and (AppDtl.ExpiryDateTime between (dateadd(minute,(330),getutcdate())) and AppDtl.ExpiryDateTime) ";
            return sc.SearchRecord(Query).Tables[0];
        }
        public DataTable GetCategoryInfo(int CategoryId, int ClientId)
        {
            sc = new Search();
            string query = @"  select AppSubDtl.License,AppSubDtl.ExpiryDateTime from
	 tblAppliedSubscriptionDtl as AppSubDtl left join tblAppliedSubscription as AppSub 
	 on 	AppSubDtl.AppliedSubscriptionId=AppSub.AppliedSubscriptionId
	where AppSub.ClientId=" + ClientId + " and AppSubDtl.CategoryId=" + CategoryId + " and  AppSubDtl.Status=0 ";

            return sc.SearchRecord(query).Tables[0];
        }

        public DataTable GetCategoryUserList(int ClientId, int CategoryId)
        {
            sc = new Search();
            // string query = @"select tblusr.UserId,tblusr.UserName from tblUser as tblusr where tblusr.ClientId=" + ClientId + " and tblusr.Status=0 ";
            string query = @"select tblusr.UserId,tblMap.Status,tblusr.UserName from tblUser as tblusr left join 
                                tblAppliedCategoryMapping as tblMap on tblusr.UserId =tblMap.UserId and tblMap.CategoryId=" + CategoryId + " where tblusr.ClientId=" + ClientId + " and tblusr.Status=0 ";
            return sc.SearchRecord(query).Tables[0];
        }

        public int GetCategoryUserListCount(int ClientId, int CategoryId)
        {
            sc = new Search();
            // string query = @"select tblusr.UserId,tblusr.UserName from tblUser as tblusr where tblusr.ClientId=" + ClientId + " and tblusr.Status=0 ";
            string query = @"select count(*) as count from tblUser as tblusr left join tblAppliedCategoryMapping as  tblMap
	                         on tblusr.UserId =tblMap.UserId and tblMap.CategoryId=" + CategoryId + " where tblusr.ClientId = " + ClientId + " and tblusr.Status=0 and tblMap.Status=0";
            return Convert.ToInt32(sc.SearchRecord(query).Tables[0].Rows[0][0].ToString());
        }
        public DataTable SelectedCategoryUser(int ClientId, int CategoryId)
        {
            sc = new Search();
            string query = @"select tblusr.UserId,tblusr.UserName from tblUser as tblusr left join 
                                tblAppliedCategoryMapping as  tblMap 
	                            on tblusr.UserId =tblMap.UserId
	                            where tblusr.ClientId=" + ClientId + " and tblusr.Status=0 and tblMap.CategoryId=" + CategoryId + "";
            return sc.SearchRecord(query).Tables[0];
        }
        public DataTable LoginChanges()
        {
            sc = new Search();
            string query = @"select CategoryId, CategoryName from tblFeatureCategory where Status = 0";
            return sc.SearchRecord(query).Tables[0];
        }

        public DataTable RemarExtData(int ClientId)
        {
            sc = new Search();
            string query = @"  select RemarkID,Remark from tblRemarkExt where IsActive=1 and ClientId =" + ClientId + " ";
            return sc.SearchRecord(query).Tables[0];
        }

        //public int DeleteRemark(int ClientId,int RemarkId)
        //{
        //    sc = new Search();
        //    string query = @" update tblRemarkExt set IsActive=0, UpdationDate=dateadd(minute,330,getutcdate()) where ClientId="+ClientId +" and RemarkId = "+RemarkId +" ";
        //    return Convert.ToInt32(sc.SearchRecord(query).Tables[0].Rows[0][0].ToString());
        //}
        public DataTable GetConveyanceDetails(int ConveyanceId)
        {
            sc = new Search();
            string query = @"select tc.Distance,MobileNo,MobileNo2 EnggId,case FromLocation when '" + Constant.LocationNotFound + @"' then [FromLatitude]+','+[FromLongitude] else FromLocation end StartLocarion ,
                              case ToLocation when '" + Constant.LocationNotFound + @"' then [ToLatitude] + ',' +[ToLongitude] else ToLocation end EndLocation, tcd.StartTimeRemark callsrno,  
                              [LogDateTime],(select dbo.fn_CalculateDateTime([LogDateTime],[ToLogDateTime])) [time] from [dbo].[tblConveyance] tc
                              left join[tblConveyanceDetails] tcd on tcd.ConveyanceId=tc.ConveyanceId
                              left join tbluser tu on tc.UserId= tu.UserId and tu.[status]= 0
                              left join tbluserDevice tud on tud.MobileNo1= tu.MobileNo and tud.[status]= 0
                              where tc.[ConveyanceId]= " + ConveyanceId + " and tc.[status]= 0";
            return sc.SearchRecord(query).Tables[0];
        }
}
}
