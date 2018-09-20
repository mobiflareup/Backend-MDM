using MobiOcean.MDM.DAL;
using System;
using System.Data;

/// <summary>
/// Summary description for GingerboxSrch
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Query
{
    public class GingerboxSrch
    {
        Search srch;
        DataTable dt;
        string query, subquery;
        string passwrd = "";
        public GingerboxSrch()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetSubscriptionDetailsBysubcriptionId(string subscriptionId)
        {
            try
            {
                srch = new Search();
                query = @"select sd.CategoryId, sd.PricePerUnit, sd.Duration, sd.License, sd.PaidAmount ,fc.CategoryName from [dbo].[tblSubscriptionDtl]  sd left join tblFeatureCategory fc on 
                        fc.CategoryId = sd.CategoryId where SubscriptionId = " + subscriptionId;
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable GetPaymentHeading()
        {
            try
            {
                srch = new Search();
                query = @"SELECT [PHId] ,[PHName] ,[Status], [TypeOfCost] FROM [tblPayementHeading] where status = 0";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }

        }
        public DataTable GetSolutionList(string pHId)
        {
            try
            {
                srch = new Search();
                query = "SELECT [SolutionId],[SolutionName],[PHId] FROM [tblSolutionCategory] Where  status =0 and PHId = " + pHId;
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable GetCategoryList(string solutionId)
        {

            try
            {
                srch = new Search();
                query = @"SELECT  [CategoryId],[CategoryName],[CloudPrice],[Android],[IOS],[BuyNow],[BuyNowLink],[PageLink]
                        FROM[tblFeatureCategory] where status = 0 and SolutionId = " + solutionId;
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable SrchUserDeviceTerminalList(int ClientId, string UserName, string DeviceName, string BranchName, string DeptName)
        {
            try
            {//,apm.AppPackage,apm.AppVersion
                srch = new Search();
                query = @"select 1 Status,tc.ClientId,us.UserId,ud.DeviceId,ud.MobileNo1,us.Username,tc.ClientName,ud.DeviceName,ud.APPId,ud.CountryId,tb.BranchName,td.DeptName 
from tblUserDevice ud left join  tblClient tc on ud.clientId = tc.clientid left join  tblUser us on us.UserId = ud.UserId  left join  tblBranch tb on tb.branchid = us.branchid left join tblDepartment td on td.DeptId = us.DeptId 
where ud.status = 0  and us.status = 0 and tc.status = 0 and ud.ClientId=" + ClientId;//and ud.IsOTPVerified =1
                if (UserName.Trim() != "")
                {
                    query = query + " and us.UserName like'%" + UserName + "%' ";
                }
                if (DeviceName.Trim() != "")
                {
                    query = query + " and ud.DeviceName like '%" + DeviceName + "%' ";
                }
                if (BranchName.Trim() != "")
                {
                    query = query + " and tb.BranchName like '%" + BranchName + "%' ";
                }
                if (DeptName.Trim() != "")
                {
                    query = query + " and td.DeptName like '%" + DeptName + "%' ";
                }
                query = query + "order by tc.ClientName, us.Username";
                return srch.SearchRecord(query).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable SrchUserDeviceList(int ClientId, string UserName, string DeviceName, string BranchName, string DeptName, int PageId, int PageClickId, string MarketId)
        {
            try
            {//,apm.AppPackage,apm.AppVersion
                srch = new Search();
                query = @"select tc.ClientId,us.UserId,ud.DeviceId,ud.MobileNo1,us.Username,tc.ClientName,ud.DeviceName,ud.APPId,ud.GCMId,ud.CountryId,tb.BranchName,td.DeptName ,IsNull(ap1.status,1) Status,IsNull(ap.AppMarketAssignId,0) AppMarketAssignId,apm.Path,apm.AppPackage
	 ,ap.AppInstall,ap.AppUpdate,ap.AppUnInstall,ap.OsUpgrade
from tblUserDevice ud left join  tblClient tc on ud.clientId = tc.clientid left join  tblUser us on us.UserId = ud.UserId  left join  tblBranch tb on tb.branchid = us.branchid left join tblDepartment td on td.DeptId = us.DeptId left join";
                if (PageId == 1)
                {
                    query += @"(select *,ApkPath Path from AppMarket )apm on apm.AppMarketId = " + MarketId + @" left join 
    
     AppMarketAssign ap on ap.AppMarketId = " + MarketId + @" and ap.clientId = ud.clientId and ap.UserId = ud.UserId and ap.IsAppMarket = 1 left join  ";
                    if (PageClickId == 1)
                        query += "AppMarketAssign ap1 on ap1.AppMarketId = " + MarketId + @" and ap1.clientId = ud.clientId and ap1.UserId = ud.UserId and ap1.IsAppMarket = 1  and ap1.AppInstall = 1";
                    else if (PageClickId == 2)
                        query += "AppMarketAssign ap1 on ap1.AppMarketId = " + MarketId + @" and ap1.clientId = ud.clientId and ap1.UserId = ud.UserId and ap1.IsAppMarket = 1  and ap1.AppUpdate = 1";

                    else if (PageClickId == 3)
                        query += "AppMarketAssign ap1 on ap1.AppMarketId = " + MarketId + @" and ap1.clientId = ud.clientId and ap1.UserId = ud.UserId and ap1.IsAppMarket = 1  and ap1.AppUnInstall = 1";
                }
                else if (PageId == 2)
                {
                    query += @"(select *,Os_Path Path from AppOSMarket) apm on apm.OsId = " + MarketId + @" left join 
     AppMarketAssign ap on ap.AppMarketId = " + MarketId + @" and ap.clientId = ud.clientId and ap.UserId = ud.UserId and ap.IsAppMarket = 0  left join 
      AppMarketAssign ap1 on ap1.AppMarketId = " + MarketId + @" and ap1.clientId = ud.clientId and ap1.UserId = ud.UserId and ap1.IsAppMarket = 0  and ap1.OsUpgrade = 1";
                }
                query += " where ud.status = 0  and us.status = 0 and tc.status = 0 and ud.ClientId=" + ClientId;//and ud.IsOTPVerified =1
                if (UserName.Trim() != "")
                {
                    query = query + " and us.UserName like'%" + UserName + "%' ";
                }
                if (DeviceName.Trim() != "")
                {
                    query = query + " and ud.DeviceName like '%" + DeviceName + "%' ";
                }
                if (BranchName.Trim() != "")
                {
                    query = query + " and tb.BranchName like '%" + BranchName + "%' ";
                }
                if (DeptName.Trim() != "")
                {
                    query = query + " and td.DeptName like '%" + DeptName + "%' ";
                }
                query = query + "order by tc.ClientName, us.Username";
                return srch.SearchRecord(query).Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                srch = null;
            }

        }

        public DataTable GetSelectedCategoryByClientId(int clientId, string CategoryList)
        {
            try
            {
                srch = new Search();
                query = @"Select CT.CategoryId, CT.CategoryName, ISNULL(TCA.Amount, CT.CloudPrice)Price, CT.SolutionId from (SELECT CategoryId, CategoryCode, CategoryName, CloudPrice, 
                        SolutionId,CreationDate FROM tblFeatureCategory WHERE (Status = 0) AND (BuyNow = 0) AND CategoryId IN (" + CategoryList + @") AND (Status = 0)) CT left join tblClientAmt TCA 
                        on TCA.CategoryId = CT.CategoryId and TCA.ClientId = " + clientId + " and TCA.status = 0 ORDER BY CT.CreationDate";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable GetAppListByClientId(int clientId)
        {
            try
            {
                srch = new Search();
                query = @"select * from AppMarket where clientid=" + clientId;
                query = query + "order by IsNull(UpdationDate,CreationDate) desc";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable SrchOSList1(int clientid)
        {
            try
            {
                srch = new Search();
                query = @" select top 1 * from AppOSMarket where status = 0 and clientid =" + clientid + " order by CreationDate desc";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable SrchOSList(string Package, string AppVersion, int clientid)
        {
            try
            {
                srch = new Search();
                query = @" select * from AppOSMarket where status = 0 and clientid =" + clientid;
                if (Package.Trim() != "")
                {
                    query = query + " and AppPackage like'%" + Package + "%' ";
                }
                if (AppVersion.Trim() != "")
                {
                    query = query + " and AppVersion like '%" + AppVersion + "%' ";
                }
                query = query + " order by IsNull(UpdationDate,CreationDate) desc";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable SrchOTGPackageData(int OsId)
        {
            try
            {
                srch = new Search();
                query = @"select * from AppOSMarket where status = 0 and OsId =" + OsId;
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable SrchApplicationMarketData(int appMarketId)
        {
            try
            {
                srch = new Search();
                query = @"select * from AppMarket where status = 0 and AppMarketId =" + appMarketId;
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable SrchApplicationMarketList(string AppName, string Package, string Developer, int clientId)
        {
            try
            {
                srch = new Search();
                query = @"select am.*,dt.DeviceType DeviceTypeName  from AppMarket am left join
  DeviceType as dt on am.DeviceType = dt.DeviceTypeId where status =0 and clientid=" + clientId;
                if (AppName.Trim() != "")
                {
                    query = query + " and AppName like'%" + AppName + "%' ";
                }
                if (Package.Trim() != "")
                {
                    query = query + " and AppPackage like'%" + Package + "%' ";
                }
                if (Developer.Trim() != "")
                {
                    query = query + " and Developer like '%" + Developer + "%' ";
                }
                query = query + "order by IsNull(am.UpdationDate,am.CreationDate) desc";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable CheckAppId(string appId)
        {
            try
            {
                srch = new Search();
                query = @"SELECT * from tblUserDevice where APPId ='" + appId + "'";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable SrchMobileStatusInfo(string ClientId, string UserName, string UserEmail, string FromDate, string ToDate, int FunctionalityId)
        {
            try
            {
                srch = new Search();
                query = @"SELECT tc.ClientName, tc.ClientCode, tu.UserId, tu.ClientId, tu.DeptId, tu.RoleId, tu.EmpCompanyId, tu.UserCode, tu.UserName, tu.MobileNo, tu.EmailId ,abt.Location
      ,abt.LogDatetime, abt.Remarks, abt.DeviceId, abt.FunctionalityId, abt.BtnPresslId
FROM            dbo.tblUser AS tu LEFT OUTER JOIN
                         dbo.tblClient AS tc ON tc.ClientId = tu.ClientId left join

                         dbo.AndroidBtnPressDtls as abt on tu.UserId = abt.UserId
WHERE tu.Status = 0  and abt.FunctionalityId = " + FunctionalityId;
                if (ClientId.Trim() != "0")
                {
                    query = query + " and abt.ClientId = " + ClientId;
                }
                if (UserName.Trim() != "")
                {
                    query = query + " and tu.UserName like'%" + UserName + "%' ";
                }
                if (UserEmail.Trim() != "")
                {
                    query = query + " and tu.EmailId like '%" + UserEmail + "%' ";
                }
                if (FromDate != "")
                {
                    query = query + " and cast(abt.LogDateTime as datetime)>=cast('" + FromDate + " 00:00' as datetime) ";
                }
                if (ToDate != "")
                {
                    query = query + " and cast(abt.LogDateTime as datetime)<=cast('" + ToDate + " 23:59' as datetime) ";
                }
                query = query + "order by cast(abt.LogDateTime as datetime) desc";
                return srch.SearchRecord(query).Tables[0];



            }
            finally
            {
                srch = null;
            }
        }

        public DataTable GetUser1(int ClientId, string UserCode, string UserName, string UserMob, string UserEmail, int UserId, int BranchId, int DepartmentId, int ProfileId, int DeptId = 0)
        {
            try
            {
                srch = new Search();
                query = @"Select u.UserCode,u.UserName,u.MobileNo,u.EmailId,r.RoleName,b.BranchName ,d.DeptName,p.ProfileName,pum.IsEnable,u.UserId,u.ClientId,u.DeptId,u.RoleId,u.EmpCompanyId,u.PreferredContactNo,u.Password,u.DOB,u.Gender,u.RptMngrId,u.DateOfJoining
                      ,u.TempAddress,u.PermanentAddress,u.Country,u.State,u.City,u.PinCode,u.ProfileImagePath,u.Status,pum.ProfileId,c.IsPasswordVisible
	                  FROM tblUser as u Left join tblUser as usr on usr.UserId=u.RptMngrId
	                  left join tblBranch as b on b.BranchId=u.BranchId
	                  left join tblDepartment as d on d.DeptId=u.DeptId
	                  left join tblProfileUserMapping as pum on pum.UserId=u.UserId
	                  left join tblProfile as p on p.ProfileId=pum.ProfileId
                      left join tblRole as r on u.RoleId=r.RoleId
                      left join tblClient as c on c.ClientId=u.ClientId
                      WHERE u.ClientId = " + ClientId + " and u.Status = 0 ";

                if (UserId > 0)
                {
                    query = query + @" and (u.UserId in ( select UserId from tblUser where status=0 and  RptMngrId = @UserId ) ) ";

                    query = query.Replace("@UserId", UserId + "");
                }
                if (DeptId > 0)
                {
                    query = query + @" and u.DeptId = @DeptId ";

                    query = query.Replace("@DeptId", DeptId + "");
                    query = query.Replace("@ClientId", ClientId + "");
                }
                if (BranchId > 0)
                {
                    query = query + "and u.BranchId=" + BranchId + "";
                }
                if (DepartmentId > 0)
                {
                    query = query + "and u.DeptId=" + DepartmentId + "";
                }
                if (ProfileId > 0)
                {
                    query = query + "and p.ProfileId=" + ProfileId + "";
                }
                #region--- Stu and Parents Srch argument --------------
                if (UserCode.Trim() != "")
                {
                    subquery = subquery + " and u.UserCode like '%" + UserCode + "%' ";
                }
                if (UserName.Trim() != "")
                {
                    subquery = subquery + " and u.UserName like'%" + UserName + "%' ";
                }
                if (UserMob.Trim() != "")
                {
                    subquery = subquery + " and u.MobileNo like '%" + UserMob + "%' ";
                }
                if (UserEmail.Trim() != "")
                {
                    subquery = subquery + " and u.EmailId like '%" + UserEmail + "%' ";
                }
                #endregion



                query = query + subquery;

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public string getUninstallationPasswrdByAndroidAppId(string AndroidAppId)
        {

            try
            {
                #region----------------------- main Query -----------
                srch = new Search();
                subquery = "";

                query = @" select Password from tblUser where UserId in  
                        (
                            Select RptMngrId from tblUser where UserId in (Select UserId from tblUserDevice where APPId = '@AndroidAppId'
                        )
                    ";
                //Select UserId from tblUser where UserId in                            ( 
                query = query.Replace("@AndroidAppId", AndroidAppId);

                #endregion



                try
                {
                    passwrd = srch.SearchRecord(query).Tables[0].Rows[0]["Password"].ToString();
                }
                catch (Exception) { }

                if (passwrd.Trim() == "")
                {
                    passwrd = getAdminPasswrdForUninstallationPasswrdByAndroidAppId(AndroidAppId);
                }

                return passwrd;

            }
            finally
            {
                srch = null;
            }
        }
        public string getAdminPasswrdForUninstallationPasswrdByAndroidAppId(string AndroidAppId)
        {
            dt = new DataTable();
            try
            {



                #region----------------------- main Query -----------
                srch = new Search();
                subquery = "";

                query = @" select distinct Password from tblUser where ClientId in  
                        (
                            Select ClientId from tblUserDevice where AppId = '@AndroidAppId'
                        ) and RoleId = '2'
                    ";

                query = query.Replace("@AndroidAppId", AndroidAppId);

                #endregion




                dt = srch.SearchRecord(query).Tables[0];

                for (int idx = 0; idx < dt.Rows.Count; idx++)
                {
                    if (passwrd.Trim() != "")
                    {
                        passwrd = passwrd + ",";
                    }
                    passwrd = passwrd + dt.Rows[idx]["Password"].ToString();
                }

                return passwrd;

            }
            finally
            {
                srch = null;
            }
        }

        public DataTable GetProfile(int ClientId, string ProfileCode, string ProfileName, int DeptId = 0)
        {
            try
            {
                srch = new Search();

                query = @"SELECT p.ProfileId, p.ClientId, c.ClientName, p.ProfileCode, p.ProfileName, p.ProfilePurpose, p.Status FROM tblProfile AS p INNER JOIN
                         dbo.tblClient AS c ON p.ClientId = c.ClientId WHERE        (p.Status = 0) and p.ClientId=" + ClientId + " ";


                #region--- Stu and Parents Srch argument --------------
                if (DeptId != 0)
                {
                    subquery = subquery + " and ( p.CreatedBy =" + DeptId + " or p.UpdatedBy = " + DeptId + ") ";
                }
                if (ProfileCode.Trim() != "")
                {
                    subquery = subquery + " and p.ProfileCode like '%" + ProfileCode + "%' ";
                }
                if (ProfileName.Trim() != "")
                {
                    subquery = subquery + " and p.ProfileName like'%" + ProfileName + "%' ";
                }

                #endregion



                query = query + subquery;

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable GetProfileByProfileId(int ProfileId)
        {
            try
            {
                srch = new Search();

                query = @"SELECT        p.ProfileId, p.ClientId, c.ClientName, p.ProfileCode, p.ProfileName, p.ProfilePurpose, p.Status FROM            dbo.tblProfile AS p INNER JOIN
                         dbo.tblClient AS c ON p.ClientId = c.ClientId WHERE        (p.Status = 0) and p.ProfileId=" + ProfileId + " and p.Status = 0";



                query = query + subquery;

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
            //    protected void BindProfileDetail()
            //{
            //    try
            //    {
            //        GSrch = new GingerboxSrch();
            //        dt = new DataTable();
            //        dt = GSrch.GetProfileByProfileId(ProfileId);
            //        txtProfileName.Text = dt.Rows[0]["ProfileName"].ToString();
            //    }
            //    catch (Exception)
            //    {
            //        Response.Redirect("ProfileMaster.aspx");
            //    }
            //}
        }
        public DataTable FeatureTimingfromTemp(int ProfileFeatureMappingId, int GroupId)
        {
            try
            {
                srch = new Search();
                query = @"SELECT        t.ProfileFeatureTimingTempId, t.ProfileFeatureTimingId, t.ProfileFeatureMappingId, t.ClientId, t.ProfileId, t.FeatureId, t.IsEnable, t.FromDay, t.ToDay, t.FromTime, t.ToTime, t.Duration,
                          t.IsDayControlled, t.IsTimeControlled, t.IsDurationControlled, t.Message, t.Status, t.CreatedBy, t.CreationDate, t.UpdatedBy, t.UpdationDate, t.RowVer, t.IsChanged,t.GroupId
,g.AppgroupName,w.CategoryName FROM            dbo.tblProfileFeatureTimingTemp as t left join tblAppGroup as g on t.GroupId=g.AppGroupId  left join tblWebCategory as w on t.GroupId=w.CategoryId
WHERE        ProfileFeatureMappingId = " + ProfileFeatureMappingId + " And t.Status=0 ";


                if (GroupId != 0)
                {
                    subquery = subquery + " and t.GroupId =" + GroupId + " ";
                }
                //if (WebCategoryId != 0)
                //{
                //    subquery = subquery + " and t.GroupId =" + GroupId + " ";
                //}
                query = query + subquery + " Order By t.FromDay,t.FromTime ";

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public int GetProfileFeatureMappingId(int ProfileId, int FeatureId)
        {
            try
            {
                srch = new Search();
                query = @"SELECT TOP 1 ProfileFeatureMappingId FROM tblProfileFeatureMapping 
                        WHERE ProfileId = " + ProfileId + " And FeatureId = " + FeatureId + " And Status=0 ";
                return Convert.ToInt32(srch.SearchRecord(query).Tables[0].Rows[0]["ProfileFeatureMappingId"]);

            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable Userdetails(int ClientId, string UserName, string UserDeviceName, string UserMob, int Intalledsts, int UserId, int BranchId, int DepartmentId, int ProfileId, int Ownerid, string OsVersion, int DeptId = 0)
        {
            try
            {
                srch = new Search();
                query = @" select u.UserName,TUD.DeviceName,TUD.MobileNo1,di.DeviceModel,di.OSVersion,b.BranchName ,d.DeptName,p.ProfileName,pum.IsEnable,TUD.IsAppInstalled,TUD.DeviceId,TUD.UserId,TUD.ClientId,TUD.CreationDate,
                        TUD.IsAndroid,TUD.PIN ,pum.ProfileId from tblUserDevice as TUD 
	                  left join tblUser as u on u.UserId=TUD.UserId
	                  left join tblBranch as b on b.BranchId=u.BranchId
	                  left join tblDepartment as d on d.DeptId=u.DeptId
	                  left join tblProfileUserMapping as pum on pum.UserId=u.UserId
	                  left join tblProfile as p on p.ProfileId=pum.ProfileId
                      left join tblDeviceInfo as di on TUD.DeviceId=di.DeviceId
                      WHERE u.ClientId = " + ClientId + " and TUD.Status = 0 and u.Status=0";
                if (UserId > 0)
                {
                    query = query + @" and (u.UserId in ( select UserId from tblUser where status=0 and  RptMngrId = @UserId ) ) ";

                    query = query.Replace("@UserId", UserId + "");
                }
                if (DeptId > 0)
                {
                    query = query + @" and  u.DeptId = @DeptId ";

                    query = query.Replace("@DeptId", DeptId + "");
                    query = query.Replace("@ClientId", ClientId + "");
                }
                if (BranchId > 0)
                {
                    query = query + "and u.BranchId=" + BranchId + "";
                }
                if (DepartmentId > 0)
                {
                    query = query + "and u.DeptId=" + DepartmentId + "";
                }
                if (ProfileId > 0)
                {
                    query = query + "and p.ProfileId=" + ProfileId + "";
                }
                if (Ownerid > 0)
                {
                    query = query + "and TUD.OwnershipId=" + Ownerid + "";
                }
                if (OsVersion != "")
                {
                    query = query + "and di.OSversion=" + "'" + OsVersion + "'" + "";
                }
                #region--- Stu and Parents Srch argument --------------
                if (Intalledsts == 1)
                {
                    subquery = subquery + " and TUD.IsAppInstalled =1 ";
                }
                if (Intalledsts == 2)
                {
                    subquery = subquery + " and TUD.IsAppInstalled =0 ";
                }
                if (UserName.Trim() != "")
                {
                    subquery = subquery + " and u.UserName like'%" + UserName + "%' ";
                }
                if (UserMob.Trim() != "")
                {
                    subquery = subquery + " and TUD.MobileNo1 like '%" + UserMob + "%' ";
                }
                if (UserDeviceName.Trim() != "")
                {
                    subquery = subquery + " and TUD.DeviceName like '%" + UserDeviceName + "%' ";
                }
                #endregion



                query = query + subquery;

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable GeoFenceRoute(int ClientId, string RouteCode, string RouteName)
        {
            try
            {
                srch = new Search();
                query = @"SELECT       RouteId, ClientId,RouteName, RouteCode, RouteDesc from tblRouteGeofence 
                        WHERE        ClientId = " + ClientId + " And Status=0 ";

                if (RouteCode != "")
                {
                    subquery = subquery + " and RouteCode like '%" + RouteCode + "%' ";
                }
                if (RouteName != "")
                {
                    subquery = subquery + " and RouteName like '%" + RouteName + "%' ";
                }
                query = query + subquery + " Order By RouteName ";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable SrchUser(int ClientId, string UserCode, string UserName, string UserMob, string UserEmail, int UserId, int DeptId = 0, int BranchId = 0)
        {
            try
            {
                srch = new Search();
                query = @"Select distinct u.UserId,u.ClientId,u.DeptId,u.RoleId,u.EmpCompanyId,u.UserCode,u.UserName,u.MobileNo,u.PreferredContactNo,u.EmailId,u.Password,u.DOB,u.Gender,u.RptMngrId,u.DateOfJoining
                          ,u.TempAddress,u.PermanentAddress,u.Country,u.State,u.City,u.PinCode,u.ProfileImagePath,u.Status,b.BranchId,b.BranchName,u.Designation,d.DeptName,p.ProfileId,p.IsEnable FROM tblUser as u Left join tblUser as usr on usr.UserId=u.RptMngrId
                    Left join tblDepartment as d on d.DeptId=u.DeptId
                    Left join tblBranch as b on b.BranchId=u.BranchId
                    Left join tblProfileUserMapping as p on p.UserId=u.UserId
                    WHERE u.ClientId = " + ClientId + " and u.Status = 0";

                if (UserId > 0)
                {
                    query = query + @" and (u.UserId in ( select UserId from tblUser where status=0 and  RptMngrId = @UserId ) ) ";

                    query = query.Replace("@UserId", UserId + "");
                }
                if (DeptId > 0)
                {
                    query = query + @" and u.DeptId = @DeptId ";

                    query = query.Replace("@DeptId", DeptId + "");
                    // query = query.Replace("@ClientId", ClientId + "");
                }
                if (BranchId > 0)
                {
                    query = query + @" and u.BranchId = @BranchId ";

                    query = query.Replace("@BranchId", BranchId + "");
                    // query = query.Replace("@ClientId", ClientId + "");
                }
                #region--- Stu and Parents Srch argument --------------
                if (UserCode.Trim() != "")
                {
                    subquery = subquery + " and u.UserCode like '%" + UserCode + "%' ";
                }
                if (UserName.Trim() != "")
                {
                    subquery = subquery + " and u.UserName like'%" + UserName + "%' ";
                }
                if (UserMob.Trim() != "")
                {
                    subquery = subquery + " and u.MobileNo like '%" + UserMob + "%' ";
                }
                if (UserEmail.Trim() != "")
                {
                    subquery = subquery + " and u.EmailId like '%" + UserEmail + "%' ";
                }
                #endregion



                query = query + subquery;

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable AlertDetails(int UserId)
        {
            try
            {
                srch = new Search();
                query = @"select a.AlertTypeId,a.AlertType,s.IsEmail from tblAlertType as a left join (Select AlertTypeId,IsEmail from tblAlertSetting where userId =" + UserId + ") as s on a.AlertTypeId=s.AlertTypeId";

                query = query + subquery;
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }

        }

        public DataTable GetSosCamera(int clientid, string FromDate, string ToDate, string PersonName, string ContactNo)
        {
            try
            {
                srch = new Search();
                query = @"SELECT  sos.PersonName,sos.ContactNo ,sos.Latitude ,sos.Longitude ,sos.Location ,sos.LogDateTime ,sos.CameraId ,sos.UserId FROM dbo.SosCameraDetail as sos
                        WHERE sos.userid in (select userid from tbluser where clientid = " + clientid + ") ";
                if (PersonName != "")
                {
                    query = query + " and PersonName like '%" + PersonName + "%' ";
                }
                if (ContactNo != "")
                {
                    query = query + " and ContactNo like '%" + ContactNo + "%' ";
                }
                if (FromDate != "")
                {
                    query = query + " and cast(LogDateTime as datetime)>=cast('" + FromDate + "' as datetime) ";
                }
                if (ToDate != "")
                {
                    query = query + " and cast(LogDateTime as datetime)<=cast('" + ToDate + "' as datetime) ";
                }

                return srch.SearchRecord(query + " Order By Cast(LogDateTime as datetime) Desc").Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable GetTAMaster(int ClientId, int UserId, string FromDate, string ToDate, string username, string IsApproval, string isPaid, int deptId = 0)
        {
            try
            {
                srch = new Search();
                query = @"SELECT  u.EmpCompanyId, u.UserName , t.LogDate,  t.TotalDistance, t.ClaimedAmt, t.ApprovedAmt, t.IsApproved, us.UserName AS approvedby, t.ApproverRemark, t.IsPaid,  
                    t.IsTripEnd,  t.Status, t.CreationDateTime, t.UpdatedBy, t.UpdationDateTime, u.MobileNo, t.MasterId, t.ClientId, t.UserId, t.ApprovedBy AS approvedbyid
                    FROM            dbo.TA_Master AS t LEFT OUTER JOIN
                                             dbo.tblUser AS u ON t.UserId = u.UserId LEFT OUTER JOIN
                                             dbo.tblUser AS us ON t.ApprovedBy = us.UserId
                    WHERE        (t.ClientId = " + ClientId + ")";
                if (UserId > 0)
                {
                    query = query + @" and ( u.UserId  = @UserId)";

                    query = query.Replace("@UserId", UserId + "");
                }
                if (deptId > 0)
                {
                    query = query + @" and ( u.DeptId  = @DeptId  )";

                    query = query.Replace("@DeptId", deptId + "");
                }
                if (username != "")
                {
                    query = query + " and u.UserName like '%" + username + "%' ";
                }
                if (FromDate != "")
                {
                    query = query + " and cast(t.LogDate as datetime)>=cast('" + FromDate + "' as datetime) ";
                }
                if (ToDate != "")
                {
                    query = query + " and cast(t.LogDate as datetime)<=cast('" + ToDate + "' as datetime) ";
                }
                if (IsApproval == "0" || IsApproval == "1")
                {
                    query = query + " and t.IsApproved= " + IsApproval + " ";
                }
                if (isPaid == "0" || isPaid == "1")
                {
                    query = query + " and t.IsPaid= " + isPaid + " ";
                }
                query += "Order by t.LogDate desc";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable GetMDMAppList(string UserId, string DeviceId, int clientID)
        {
            try
            {
                srch = new Search();
                query = @"Select  MA.AppTypeId, Name ,Details ,DownloadURL,AppVersion, InstallStatus ,CCA.ClientId from tblMobioceanAppTypes MA
                          left join tblUserDeviceAppInstallStatus APS on MA.AppTypeId = APS.AppTypeId and  APS.status = 0 and APS.UserId = " + UserId + " and DeviceId = " + DeviceId + @"and InstallStatus = 0
                          left join tblClientCustomApp CCA on CCA.AppTypeId = MA.AppTypeId and CCA.ClientId=" + clientID + "where MA.status = 1 and (MA.IsCustom=0 or CCA.ClientId is not null)";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable GetTAMasterDetailsByMasterId(int ClientId, int MasterId)
        {
            try
            {
                srch = new Search();
                query = @"SELECT us.UserName AS approvedby, u.UserName, u.EmpCompanyId, u.MobileNo, t.MasterId, t.ClientId, t.UserId, t.LogDate, t.IsTripEnd, t.IsApproved, t.IsPaid, t.ApprovedBy AS Expr2, t.ClaimedAmt, t.TotalDistance, 
                         t.ApprovedAmt, t.ApproverRemark, t.Status, t.CreationDateTime, t.UpdatedBy, t.UpdationDateTime
                        FROM   dbo.TA_Master AS t LEFT OUTER JOIN
                                                 dbo.tblUser AS u ON t.UserId = u.UserId LEFT OUTER JOIN
                                                 dbo.tblUser AS us ON t.ApprovedBy = us.UserId
                        WHERE         (t.MasterId =" + MasterId + ") and (t.ClientId = " + ClientId + ")";

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable GetVistDetailsbyMasterId(int MasterId)
        {
            try
            {
                srch = new Search();
                query = @"SELECT  tc.Name,tc1.Name as AutoName, tav.FromDateTime,   tav.VisitDateTime, tav.ToDateTime,  tav.IsVisited, tav.FilePath,tav.Remark,tav.TotalDistance, tav.ClaimedTravelAmt,Tmt.ModeOfTravel, tav.IsReturn, tav.VisitDetailId, tav.MasterId, tav.CustomerId, tav.TempCustomerId,
                                                   tav.ModeOfTravelId, tav.RatePerKM,  tav.ApprovedTravelAmt, tav.ApproverRemark, tav.FromLat, tav.FromLong, tav.FromLocation, tav.ToLat, tav.ToLong, 
                                                 tav.ToLocation, tav.Status, tav.CreationDateTime, tav.UpdatedBy, tav.UpdationDateTime , Tu.UserName, Tm.LogDate,tempC.Name as TempCustomer,tempC1.Name as AutoTempCustomer
                        FROM            dbo.TA_VisitDetail AS tav LEFT OUTER JOIN
                                                 dbo.tblCustomer AS tc ON tc.CustomerId = tav.CustomerId LEFT OUTER JOIN
                                                 dbo.tblCustomer AS tc1 ON tc1.CustomerId = tav.AutoCustomerId LEFT OUTER JOIN
                                                 dbo.TA_Master AS Tm ON tav.MasterId = Tm.MasterId LEFT OUTER JOIN
                                                 dbo.tblCustomerTemp AS tempC ON tempC.CustomerTempId = tav.TempCustomerId LEFT OUTER JOIN
                                                 dbo.tblCustomerTemp AS tempC1 ON tempC1.CustomerTempId = tav.AutoTempCustomerId LEFT OUTER JOIN
                                                 dbo.tblUser AS Tu ON Tu.UserId = Tm.UserId LEFT OUTER JOIN
                                                 dbo.tblModeOfTravel AS TmT ON TmT.ModeId = tav.ModeOfTravelId
                         WHERE        (tav.MasterId = " + MasterId + ") Order by tav.FromDateTime desc";

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable GetExtraDetailsbyMasterId(int MasterId)
        {
            try
            {
                srch = new Search();
                query = @"SELECT    TE.Remark,TE.ClaimedAmt,TE.CreationDateTime, TE.FilePath, TE.ExtraDetailId, TE.MasterId, TE.LogDateTime,  TE.ApprovedAmt,  TE.ApproverRemark,  Tu.UserName, Tm.LogDate,  TE.IsApproved, TE.ApprovedBy, TE.Status, 
                                                  TE.UpdatedBy, TE.UpdationDateTime
                        FROM            dbo.TA_ExtraDetail AS TE LEFT OUTER JOIN
                                                 dbo.TA_Master AS Tm ON TE.MasterId = Tm.MasterId LEFT OUTER JOIN
                                                 dbo.tblUser AS Tu ON Tu.UserId = Tm.UserId
                        where te.MasterId = " + MasterId + " Order by TE.CreationDateTime desc";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable GetLocationDtlbyVisitId(int VisitId)
        {
            try
            {
                srch = new Search();
                query = @"SELECT   TL.Latitude ,TL.Longitude ,TL.Location,TL.LogDateTime,TL.LocationId ,tc.Name, tav.FromDateTime,   tav.VisitDateTime, tav.ToDateTime,  tav.IsVisited, tav.FilePath,tav.Remark,tav.TotalDistance, tav.ClaimedTravelAmt,Tmt.ModeOfTravel, tav.IsReturn, tav.VisitDetailId, tav.MasterId, tav.CustomerId, tav.TempCustomerId,
                                                   tav.ModeOfTravelId, tav.RatePerKM,  tav.ApprovedTravelAmt, tav.ApproverRemark, tav.FromLat, tav.FromLong, tav.FromLocation, tav.ToLat, tav.ToLong, 
                                                 tav.ToLocation, tav.Status, tav.CreationDateTime, tav.UpdatedBy, tav.UpdationDateTime , Tu.UserName, Tm.LogDate,tempC.Name as TempCustomer
                        FROM            [dbo].[TA_LocationDetail] as TL Left OUTER JOIN
						 dbo.TA_VisitDetail AS tav on TL.VisitDetailId = tav.VisitDetailId LEFT OUTER JOIN 
                                                 dbo.tblCustomer AS tc ON tc.CustomerId = tav.CustomerId LEFT OUTER JOIN
                                                 dbo.TA_Master AS Tm ON tav.MasterId = Tm.MasterId LEFT OUTER JOIN
                                                 dbo.tblCustomerTemp AS tempC ON tempC.CustomerTempId = tav.TempCustomerId LEFT OUTER JOIN
                                                 dbo.tblUser AS Tu ON Tu.UserId = Tm.UserId LEFT OUTER JOIN
                                                 dbo.tblModeOfTravel AS TmT ON TmT.ModeId = tav.ModeOfTravelId
                         WHERE        (TL.VisitDetailId = " + VisitId + ") Order by TL.LogDateTime desc";

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable GetFullTAMasterdtlsWithLocation(string ClientId, string username, string FromDate, string ToDate)
        {
            try
            {
                srch = new Search();
                query = @"select Tu.EmpCompanyId, Tu.UserName , Tm.LogDate as MLogdate,  Tm.TotalDistance, Tm.ClaimedAmt, Tm.ApprovedAmt, Tm.IsApproved, TuA.UserName AS approvedby
						 , Tm.ApproverRemark, Tm.IsPaid,  Tm.IsTripEnd, Tu.MobileNo, Tm.MasterId, Tm.ClientId, Tm.UserId 
						 ,tc.Name,tav.FromDateTime,   tav.VisitDateTime, tav.ToDateTime,tav.IsVisited, tav.FilePath,tav.Remark,tav.TotalDistance as VTotalDistance, tav.ClaimedTravelAmt,Tmt.ModeOfTravel,
						  TL.Latitude ,TL.Longitude ,TL.Location,TL.LogDateTime as locationcreatedatetime
					 from [dbo].[TA_LocationDetail] as TL  JOIN
						 dbo.TA_VisitDetail AS tav on TL.VisitDetailId = tav.VisitDetailId  JOIN 
                                                 dbo.tblCustomer AS tc ON tc.CustomerId = tav.CustomerId   JOIN
                                                 dbo.TA_Master AS Tm ON tav.MasterId = Tm.MasterId left outer JOIN
                                                 dbo.tblCustomerTemp AS tempC ON tempC.CustomerTempId = tav.TempCustomerId  JOIN
                                                 dbo.tblUser AS Tu ON Tu.UserId = Tm.UserId  left outer JOIN
												 dbo.tblUser AS TuA ON TuA.UserId = Tm.ApprovedBy  JOIN
                                                 dbo.tblModeOfTravel AS TmT ON TmT.ModeId = tav.ModeOfTravelId where Tu.status=0";

                if (username != "")
                {
                    query = query + " and Tu.UserName like '%" + username + "%' ";
                }
                if (FromDate != "")
                {
                    query = query + " and cast(Tm.LogDate as datetime)>=cast('" + FromDate + "' as datetime) ";
                }
                if (ToDate != "")
                {
                    query = query + " and cast(Tm.LogDate as datetime)<=cast('" + ToDate + "' as datetime) ";
                }
                if (ClientId.Trim() != "0")
                {
                    query = query + " and Tu.ClientId = " + ClientId;
                }
                query = query + "Order by TL.LogDateTime desc";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable SuperAdminUserDetails(string ClientId, string RoleId, string UserName, string UserEmail)
        {
            try
            {
                srch = new Search();
                query = @"SELECT        tc.ClientName, tc.ClientCode, tu.UserId, tu.ClientId, tu.DeptId, tu.RoleId, tu.EmpCompanyId, tu.UserCode, tu.UserName, tu.MobileNo, tu.PreferredContactNo, tu.EmailId, tu.Password, tu.DOB, tu.Gender, 
                                         tu.RptMngrId, tu.DateOfJoining, tu.TempAddress, tu.PermanentAddress, tu.Country, tu.State, tu.City, tu.PinCode, tu.ProfileImagePath, tu.Status, tu.CreatedBy, tu.CreationDate, tu.UpdatedBy, tu.UpdationDate, 
                                         tu.RowVer, tu.PswrdExpiryDate, tu.IsFirstLogin, tu.BranchId, tu.Designation, tu.NumberOfEmployees, tu.TypeOfClient ,tr.RoleName
                FROM            dbo.tblUser AS tu LEFT OUTER JOIN
                                         dbo.tblClient AS tc ON tc.ClientId = tu.ClientId left join 
                   dbo.tblRole as tr on tr.RoleId = tu.RoleId 
                WHERE        (tu.Status = 0) ";
                if (RoleId.Trim() != "0")
                {
                    subquery = subquery + " and tu.RoleId =" + RoleId;
                }
                if (UserName.Trim() != "")
                {
                    subquery = subquery + " and tu.UserName like'%" + UserName + "%' ";
                }
                //if (UserMob.Trim() != "")
                //{
                //    subquery = subquery + " and tu.MobileNo like '%" + UserMob + "%' ";
                //}
                if (UserEmail.Trim() != "")
                {
                    subquery = subquery + " and tu.EmailId like '%" + UserEmail + "%' ";
                }

                if (ClientId.Trim() != "0")
                {
                    subquery = subquery + " and tu.ClientId = " + ClientId;
                }


                query = query + subquery;

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable DashboardSOS(int ClientId, string FromDate, string ToDate)
        {
            try
            {
                srch = new Search();
                query = @"SELECT u.UserName, ud.DeviceName , dl.Location, Substring(LogDateTime,1,11) as LogDate,
                        Substring(LogDateTime,13,5) as LogTime, dl.DeviceLocId,u.UserId, dl.ClientId, dl.DeviceId, dl.UserId, ud.MobileNo1, dl.Longitude, dl.Latitude, dl.LogDateTime,
                                             dl.LocationSource, dl.SrvcCalledBy, dl.Status,ud.UserId,ud.DeviceId
                                            FROM  dbo.tblDeviceLocation as dl
                                            left join tblUserDevice as ud on ud.DeviceId=dl.DeviceId
                        left join tblUser as u on u.UserID=ud.UserID
                        where dl.ClientId=" + ClientId + " and dl.LocReq=10 ";

                if (FromDate != "")
                {
                    query = query + " and Cast(dl.LogDateTime as datetime)>=Cast('" + FromDate + " 00:00' as datetime) ";
                }
                if (ToDate != "")
                {
                    query = query + "and Cast(dl.LogDateTime as datetime)<=Cast('" + ToDate + " 23:59' as datetime)";
                }



                query = query + "Order by cast(dl.LogDateTime as datetime) desc";

                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }

        public DataTable GetDeviceList(int ClientId, int UserId, int DeptId = 0, int BranchId = 0)
        {
            try
            {
                srch = new Search();
                query = @"Select distinct ud.DeviceId,ud.DeviceName,ud.MobileNo1,u.UserId,u.ClientId,u.DeptId,u.RoleId,u.EmpCompanyId,u.UserCode,u.UserName,u.MobileNo,u.PreferredContactNo,u.EmailId,u.Password,u.DOB,u.Gender,u.RptMngrId,u.DateOfJoining
                          ,u.TempAddress,u.PermanentAddress,u.Country,u.State,u.City,u.PinCode,u.ProfileImagePath,u.Status,u.Designation,p.ProfileId,p.IsEnable FROM tblUser as u Left join tblUser as usr on usr.UserId=u.RptMngrId
						  left join tbluserdevice as ud on ud.userid=u.userid
                    Left join tblProfileDeviceMapping as p on p.deviceid=ud.deviceid
                    WHERE u.ClientId = " + ClientId + " and u.Status = 0";
                if (UserId > 0)
                {
                    query = query + @" and (u.UserId in ( select UserId from tblUser where status = 0 and  RptMngrId = @UserId ) ) ";

                    query = query.Replace("@UserId", UserId + "");
                }
                if (DeptId > 0)
                {
                    query = query + @" and u.DeptId = @DeptId ";

                    query = query.Replace("@DeptId", DeptId + "");
                }
                if (BranchId > 0)
                {
                    query = query + @" and u.BranchId = @BranchId ";

                    query = query.Replace("@BranchId", BranchId + "");
                }
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
        public DataTable GetDelayedCustomerVisit()
        {
            try
            {
                srch = new Search();
                query = @"select tca.AssignId ,tu.UserName, tc.Name CustomerName, tu.MobileNo,tca.UserId, tca.ClientId , tca.CustomerId, datediff(mi, DATEADD(day, DATEDIFF(day, 0, Cast(tca.AssignDate as datetime)), tca.AssignTime), dateadd (mi,330,getutcdate())) [Delay] from tblCustomerAssignDaily tca
  left join tbluser tu on tu.UserId = tca.UserId
  left join tblCustomer tc on tc.CustomerId = tca.CustomerId
   where (tca.Approval = 0 or tca.Approval is null) and tca.IsVisited is  null  and  datediff(mi, DATEADD(day, DATEDIFF(day, 0, Cast(tca.AssignDate as datetime)), tca.AssignTime), dateadd (mi,330,getutcdate())) > 60 and AlertStatus is  null";
                return srch.SearchRecord(query).Tables[0];
            }
            finally
            {
                srch = null;
            }
        }
    }
}

