using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL;

/// <summary>
/// Summary description for VikramSearch
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Query
{
    public class VikramSearch
    {
        Search srch;
        string Query, Subquery;



        public DataTable srchUsrProfileMapping(int ClientId, int UserId, int ProfileId, int SrchUserId, int DeptId = 0)
        {
            srch = new Search();
            Query = @"select m.ProfileUserId, m.UserId, m.ProfileId, m.ClientId,m.IsEnable, m.AppliedDateTime, m.CancelDateTime, m.Status,m.DeviceId,m.GroupId, u.UserName, p.ProfileName
                    FROM tblProfileUserMapping as m Left JOIN tblUser AS u ON u.UserId = m.UserId Left JOIN tblProfile AS p ON p.ProfileId = m.ProfileId WHERE (m.Status = 0) and m.ClientId = " + ClientId + "";

            if (UserId > 0)
            {
                Query = Query + @" and ( m.UserId in ( select UserId from tblUser where status=0 and  RptMngrId = @UserId ))";

                Query = Query.Replace("@UserId", UserId + "");
            }

            if (DeptId > 0)
            {
                Query = Query + @" and ( m.UserId in ( select UserId from tblUser where status=0 and  DeptId  = @DeptId ))";

                Query = Query.Replace("@UserId", UserId + "");
            }
            if (SrchUserId != 0)
            {
                Query = Query + " and m.UserId =" + SrchUserId + "";
            }
            if (ProfileId != 0)
            {
                Query = Query + " and m.ProfileId =" + ProfileId + "";
            }
            //Query = Query + Subquery;
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable GetCategoryName()
        {
            srch = new Search();
            Query = @"select CategoryId,CategoryName from tblFeatureCategory where Status=0";
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable srchProfilefeaturebycategoryid(int ProfileId, int CategoryId)
        {
            srch = new Search();
            Query = @" select f.FeatureId,f.FeatureCode,f.FeatureName,f.FeatureDesc,f.CategoryId,f.IsScheduleNeed,f.IsManageNeed,f.DeviceId,
                   fc.CategoryName,fm.ProfileId,fm.IsEnable,fm.NotificationOn,fm.LogOn,fm.AutoSyncOn,fm.Duration,fm.ProfileFeatureMappingId,
                   0 as IsChanged,IsForRooted FROM tblFeature as f
left join tblFeatureCategory as fc on f.CategoryId=fc.CategoryId 
left join tblProfileFeatureMapping as fm on f.FeatureId=fm.FeatureId and fm.ProfileId=" + ProfileId + @" 
WHERE (f.Status = 0) and fc.CategoryId=" + CategoryId + "  Order by f.CategoryId,f.FeatureId";

            //if (ProfileId != 0)
            //{
            //Query = Query + " and f.ProfileId =" + ProfileId + "";
            //}
            //Query = Query + Subquery;
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable srchAppMstrDtls(int Clientid, string GroupId, string AppName)
        {
            srch = new Search();
            Query = "select * from tblAppMaster where Status=0 and ClientId=@ClientId";
            Query = Query.Replace("@ClientId", Clientid.ToString());


            if (GroupId != "0")
            {
                Subquery = Subquery + "and GroupId like '%" + GroupId + "%'";
            }

            if (AppName != "")
            {
                Subquery = Subquery + "and AppName like '%" + AppName + "%'";
            }
            Query = Query + Subquery;
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable SerchWebCategoryEnable(int ClientId, string Category)
        {
            srch = new Search();
            Query = @" select f.CategoryId,f.CtegoryCode,f.CategoryName,f.CategoryDesc FROM tblWebCategory as f 
                      WHERE (f.Status = 0) ";

            if (Category != "")
            {
                Query = Query + " and f.CategoryName like '%" + Category + "%'";
            }
            Query = Query + " Order by f.CategoryId";
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable srchKeywordDtls(int ClientId, string KeywordCode, string KeywordName)
        {
            srch = new Search();
            Query = "select * from tblKeyword where  ClientId = " + ClientId + " and Status=0 ";

            if (KeywordCode != "")
            {
                Subquery = Subquery + " and KeywordCode like '%" + KeywordCode + "%'";
            }
            if (KeywordName != "")
            {
                Subquery = Subquery + " and KeywordName like '%" + KeywordName + "%'";
            }
            Query = Query + Subquery;
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable srchWebKeywordDtls(int ClientId, string KeywordCode, string KeywordName)
        {
            srch = new Search();
            Query = "select * from tblWebKeyword where  ClientId = " + ClientId + " and Status=0 ";

            if (KeywordCode != "")
            {
                Subquery = Subquery + " and KeywordCode like '%" + KeywordCode + "%'";
            }
            if (KeywordName != "")
            {
                Subquery = Subquery + " and KeywordName like '%" + KeywordName + "%'";
            }
            Query = Query + Subquery;
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable srchWebCategory(int ClientId, string KeywordCode, string KeywordName)
        {
            srch = new Search();
            Query = "select * from tblWebCategory where  Status=0 ";

            if (KeywordCode != "")
            {
                Subquery = Subquery + " and ctegorycode like '%" + KeywordCode + "%'";
            }
            if (KeywordName != "")
            {
                Subquery = Subquery + " and CategoryName like '%" + KeywordName + "%'";
            }
            Query = Query + Subquery;
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchWebsites(int Clientid, int CategoryId, string Url)
        {
            srch = new Search();
            Query = @"select u.UrlId,u.Url,u.IsWhiteList,u.CategoryId,c.CategoryName from tblBlacklisturl as u Left join tblWebCategory as c on u.CategoryId=c.CategoryId where
        u.Status=0 and u.ClientId=@ClientId";
            Query = Query.Replace("@ClientId", Clientid.ToString());
            if (CategoryId != 0)
            {
                Subquery = Subquery + "and u.CategoryId like '%" + CategoryId + "%'";
            }

            if (Url != "")
            {
                Subquery = Subquery + "and u.Url like '%" + Url + "%'";
            }
            Query = Query + Subquery;
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable SrchWebsitesRaj(int Clientid, int IsWhiteList, string Url)
        {
            srch = new Search();
            Query = @"select  UrlId,ClientId,Url,Status,IsWhiteList from tblUrl where
        Status=0 and ClientId=" + Clientid;
            if (IsWhiteList != -1)
            {
                Query = Query + " and IsWhiteList=" + IsWhiteList;
            }
            if (Url != "")
            {
                Query = Query + " and Url like '%" + Url + "%'";
            }
            return srch.SearchRecord(Query).Tables[0];
        }
        public DataTable GetWebsiteByClientIdNotAssigned(int GroupId, int ClientId, string Appname = "")
        {
            srch = new Search();
            Query = "Select u.UrlId,u.Url,u.CategoryId,c.CategoryName from tblBlacklisturl as u left join tblWebCategory as c on c.CategoryId=u.CategoryId where  u.ClientId=@ClientId and u.status=0 and (u.CategoryId !=@GroupId or (u.CategoryId is null) )  ";
            Query = Query.Replace("@GroupId", GroupId + "");
            Query = Query.Replace("@ClientId", ClientId + "");
            if (Appname != "")
            {
                Query = Query + " and u.Url like '%" + Appname + "%' ";
            }
            return srch.SearchRecord(Query + " order by UrlId").Tables[0];
        }
        public DataTable GetWebsiteByClientIdAssigned(int GroupId, int ClientId, string Appname = "")
        {
            srch = new Search();
            Query = "Select u.UrlId,u.Url,u.CategoryId,c.CategoryName from tblBlacklisturl as u left join tblWebCategory as c on c.CategoryId=u.CategoryId where  u.ClientId=@ClientId and u.status=0 and u.CategoryId =@GroupId ";
            Query = Query.Replace("@GroupId", GroupId + "");
            Query = Query.Replace("@ClientId", ClientId + "");
            if (Appname != "")
            {
                Query = Query + " and u.Url like '%" + Appname + "%' ";
            }
            return srch.SearchRecord(Query + " order by UrlId").Tables[0];
        }
        public DataTable GetWebsiteByProfileIdNotAssigned(int GroupId, int ProfileId, string Appname = "")
        {
            srch = new Search();
            Query = "Select u.UrlId,um.Url,u.CategoryId,c.CategoryName from tblProfileBlacklisturl as u left join tblBlacklisturl as um on um.UrlId=u.UrlMasterId left join tblWebCategory as c on c.CategoryId=u.CategoryId where  u.ProfileId=@ProfileId and u.status=0 and (u.CategoryId !=@GroupId or (u.CategoryId is null) )  ";
            Query = Query.Replace("@GroupId", GroupId + "");
            Query = Query.Replace("@ProfileId", ProfileId + "");
            if (Appname != "")
            {
                Query = Query + " and um.Url like '%" + Appname + "%' ";
            }
            return srch.SearchRecord(Query + " order by UrlId").Tables[0];
        }
        public DataTable GetWebsiteByProfileIdAssigned(int GroupId, int ProfileId, string Appname = "")
        {
            srch = new Search();
            Query = "Select u.UrlId,um.Url,u.CategoryId,c.CategoryName from tblProfileBlacklisturl as u left join tblBlacklisturl as um on um.UrlId=u.UrlMasterId left join tblWebCategory as c on c.CategoryId=u.CategoryId where  u.ProfileId=@ProfileId and u.status=0 and u.CategoryId =@GroupId  ";
            Query = Query.Replace("@GroupId", GroupId + "");
            Query = Query.Replace("@ProfileId", ProfileId + "");
            if (Appname != "")
            {
                Query = Query + " and um.Url like '%" + Appname + "%' ";
            }
            return srch.SearchRecord(Query + " order by UrlId").Tables[0];
        }


    }
}
