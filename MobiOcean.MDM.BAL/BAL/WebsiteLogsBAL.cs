using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.WebsiteLogsDALTableAdapters;
using MobiOcean.MDM.BAL.Model;

/// <summary>
/// Summary description for WebsiteLogsBAL
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.BAL
{
    public class WebsiteLogsBAL //: ConstantBAL
    {
        /**************Call classes***************************/
        tblWebsiteLogTableAdapter weblog;
        tblBlacklisturlTableAdapter rstctUrl;
        tblBlacklisturlTempTableAdapter rstctUrlTemp;
        tblWebCategoryTableAdapter webctgry;



        tblProfileBlackListUrlTableAdapter profileblacklisturl;
        tblCategortWebsiteTableAdapter webcat;
        tblVPNTableAdapter vpn;
        tblVPNServerStausTableAdapter vpnServer;



        /*********Variable Initialization**************/
        private int _DeviceId, _ClientId, _UserId, _IsWhiteList, _UrlId, _ProfileId, _webCategoryId, _Status, _CategoryId;
        private string _AppId, _WebsiteUrl, _LogDateTime, _Url, _WebsiteUrlList, _CategoryCode, _CategoryName, _CategoryDesc, _LoggedBy, _CategoryList, _UrlIdList;        
        private object _dt;        

        public string IPAddress { get; set; }
        public int IsEnabled { get; set; }
        public DateTime creationDateTime { get; set; }
        public int webCategoryId
        {
            get { return _webCategoryId; }
            set { _webCategoryId = value; }
        }
        public int ProfileId
        {
            get { return _ProfileId; }
            set { _ProfileId = value; }
        }
        public string WebsiteUrlList
        {
            get { return _WebsiteUrlList; }
            set { _WebsiteUrlList = value; }
        }
        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }
        
        public string CategoryCode
        {
            get { return _CategoryCode; }
            set { _CategoryCode = value; }
        }
        
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
       

        public string CategoryDesc
        {
            get { return _CategoryDesc; }
            set { _CategoryDesc = value; }
        }       
        public string UrlIdList
        {
            get { return _UrlIdList; }
            set { _UrlIdList = value; }
        }
        public string CategoryList
        {
            get { return _CategoryList; }
            set { _CategoryList = value; }
        }

        public string LoggedBy
        {
            get { return _LoggedBy; }
            set { _LoggedBy = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public int UrlId
        {
            get { return _UrlId; }
            set { _UrlId = value; }
        }
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        public object dt
        {
            get { return _dt; }
            set { _dt = value; }
        }
        public int IsWhiteList
        {
            get { return _IsWhiteList; }
            set { _IsWhiteList = value; }
        }
        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public string AppId
        {
            get { return _AppId; }
            set { _AppId = value; }
        }
        public string WebsiteUrl
        {
            get { return _WebsiteUrl; }
            set { _WebsiteUrl = value; }
        }

        public string LogDateTime
        {
            get { return _LogDateTime; }
            set { _LogDateTime = value; }
        }

        public string InsertWebCategory()
        {
            webcat = new tblCategortWebsiteTableAdapter();
            try
            {
                return webcat.InsertWebCategory(_ClientId, _UserId, _CategoryId, _Url, _webCategoryId).ToString();
            }
            finally
            {
                webcat = null;
            }

        }
        public DataTable GetWebCategoryNames()
        {
            webcat = new tblCategortWebsiteTableAdapter();
            try
            {
                return webcat.GetCategorywebsitenames(_CategoryId);
            }
            finally
            {
                webcat = null;
            }

        }
        public string InsertWebsiteLogs()
        {
            weblog = new tblWebsiteLogTableAdapter();
            try
            {
                return weblog.sp_InsertWebsiteLogs(_AppId, _WebsiteUrl, _LogDateTime, _DeviceId, _ClientId, _UserId, creationDateTime).ToString();
            }
            catch (Exception)
            {
                return "0";
            }
            finally
            {
                weblog = null;
            }

        }

        public int IU_WebCategory()
        {
            webctgry = new tblWebCategoryTableAdapter();
            try
            {
                return Convert.ToInt32(webctgry.IU_WebCategory(_CategoryId, _ClientId, _CategoryCode, _CategoryName, _CategoryDesc, _Status, _LoggedBy.ToString()).ToString());// _UserId, DateTime.Now.AddMinutes(Constant.addMinutes)).ToString();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                webctgry = null;
            }

        }
        public string CategoryEnableChangesApply()
        {
            webctgry = new tblWebCategoryTableAdapter();
            try
            {
                return webctgry.CategoryEnableChangesApply(_ClientId, _CategoryList, _LoggedBy.ToString()).ToString();// _UserId, DateTime.Now.AddMinutes(Constant.addMinutes)).ToString();
            }
            catch (Exception)
            {
                return "0";
            }
            finally
            {
                webctgry = null;
            }

        }
        public int MoveWebCategoryInEnable()
        {
            webctgry = new tblWebCategoryTableAdapter();
            try
            {
                return Convert.ToInt32(webctgry.MoveWebCategoryInEnable(_ClientId).ToString());// _UserId, DateTime.Now.AddMinutes(Constant.addMinutes)).ToString();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                weblog = null;
            }

        }
        public DataTable GetRstrctWebsite()
        {
            rstctUrl = new tblBlacklisturlTableAdapter();
            try
            {
                return rstctUrl.GetRstrctWebsite(_ClientId);
            }
            finally
            {
                rstctUrl = null;
            }

        }
        public DataTable GetRstrctWebsiteTemp()
        {
            rstctUrlTemp = new tblBlacklisturlTempTableAdapter();
            try
            {
                return rstctUrlTemp.GetBlackListUrltemp(_ClientId);
            }
            finally
            {
                rstctUrlTemp = null;
            }

        }
        public int InsertWebsite()
        {
            rstctUrl = new tblBlacklisturlTableAdapter();
            return Convert.ToInt32(rstctUrl.InsertBlackListWeb(_ClientId, _dt));
        }
        public int ApplyBlackListUrlChanges()
        {
            rstctUrl = new tblBlacklisturlTableAdapter();
            return Convert.ToInt32(rstctUrl.ApplyBlackListUrlChanges(_ClientId, _UserId));
        }
        public int CancelBlackListURLChanges()
        {
            rstctUrl = new tblBlacklisturlTableAdapter();
            return Convert.ToInt32(rstctUrl.CancelBlackListURLChanges(_ClientId, _UserId));
        }
        public int IU_tblBlacklisturl()
        {
            rstctUrl = new tblBlacklisturlTableAdapter();
            return Convert.ToInt32(rstctUrl.IU_tblBlacklisturl(_UrlId, _Url, _CategoryId, _ClientId, Convert.ToInt32(_LoggedBy)).ToString());
        }
        public int IU_tblBlacklisturlRaj()
        {
            rstctUrl = new tblBlacklisturlTableAdapter();
            return Convert.ToInt32(rstctUrl.IU_tblBlacklisturlRaj(_UrlId, _Url, _IsWhiteList, _ClientId, Convert.ToInt32(_LoggedBy)).ToString());
        }
        public DataTable GetWebsite()
        {
            rstctUrl = new tblBlacklisturlTableAdapter();
            try
            {
                return rstctUrl.GetWebsite(_ClientId);
            }
            finally
            {
                rstctUrl = null;
            }

        }
        public DataTable sp_BlackListUrl()
        {
            rstctUrl = new tblBlacklisturlTableAdapter();
            try
            {
                return rstctUrl.sp_BlackListUrl(_DeviceId, _ClientId);
            }
            finally
            {
                rstctUrl = null;
            }

        }
        public DataTable GetProfileBlackListUrlData()
        {
            profileblacklisturl = new tblProfileBlackListUrlTableAdapter();
            return profileblacklisturl.GetUrlByProfileId(_ProfileId);
        }
        public int AssignUrlToProfile()
        {
            profileblacklisturl = new tblProfileBlackListUrlTableAdapter();
            return Convert.ToInt32(profileblacklisturl.AssignUrlToProfile(_ProfileId, _WebsiteUrlList, _LoggedBy));
        }
        public int IU_ProfileBlackListUrl()
        {
            profileblacklisturl = new tblProfileBlackListUrlTableAdapter();
            return Convert.ToInt32(profileblacklisturl.IU_ProfileBlackListUrl(_ProfileId, _Url, _LoggedBy, _ClientId));
        }
        public DataTable GetProfileBlackListUrlByProfileId()
        {
            profileblacklisturl = new tblProfileBlackListUrlTableAdapter();
            return profileblacklisturl.GetProfileBlackListUrlByProfileId(_ClientId);
        }
        public DataTable sp_ProfileBlackListUrl()
        {
            profileblacklisturl = new tblProfileBlackListUrlTableAdapter();
            return profileblacklisturl.sp_ProfileBlackListUrl(_DeviceId, _ClientId);
        }
        public DataTable GetCategoryNameForDDL()
        {
            webcat = new tblCategortWebsiteTableAdapter();
            return webcat.GetCategoryNameForDDL();
        }
        public int DeleteWebCategory()
        {
            webcat = new tblCategortWebsiteTableAdapter();
            return webcat.DeleteWebCategory(_webCategoryId);
        }
        public int DeleteUrl()
        {
            try
            {
                rstctUrl = new tblBlacklisturlTableAdapter();
                return rstctUrl.DeleteUrl(_LoggedBy, _UrlId);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int DeleteUrlRaj()
        {
            try
            {
                rstctUrl = new tblBlacklisturlTableAdapter();
                return rstctUrl.DeleteUrlRaj(_LoggedBy, _UrlId);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int Assigncategory()
        {
            try
            {
                rstctUrl = new tblBlacklisturlTableAdapter();
                rstctUrl.Assigncategory(_CategoryId, _UrlIdList, _ClientId, _LoggedBy);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int AssigncategoryByProfile()
        {
            try
            {
                rstctUrl = new tblBlacklisturlTableAdapter();
                rstctUrl.AssigncategoryByProfile(_CategoryId, _UrlIdList, _ProfileId, _LoggedBy);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int UnAssignCategory()
        {
            try
            {
                rstctUrl = new tblBlacklisturlTableAdapter();
                rstctUrl.UnAssignCategory(_CategoryId, _UrlIdList, _ClientId, _LoggedBy);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int UnAssignCategoryByProfile()
        {
            try
            {
                rstctUrl = new tblBlacklisturlTableAdapter();
                rstctUrl.UnAssignCategoryByProfile(_CategoryId, _UrlIdList, _ProfileId, _LoggedBy);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int InsertUrlByProfile()
        {
            rstctUrl = new tblBlacklisturlTableAdapter();
            return Convert.ToInt32(rstctUrl.InsertUrlByProfile(_CategoryId, _Url, _ClientId, _ProfileId, _UserId).ToString());
        }

        public int VPNUpdate()
        {
            vpn = new tblVPNTableAdapter();
            return Convert.ToInt16(vpn.InserttblVPNRaj(ClientId, IPAddress, IsWhiteList, IsEnabled, Convert.ToInt32(LoggedBy)));
        }
        public DataTable VPNDetailsByClientId()
        {
            vpn = new tblVPNTableAdapter();
            return vpn.GetVPNByClientId(ClientId);
        }

        public int ServerDetailsByClientId()
        {
            vpn = new tblVPNTableAdapter();
            return Convert.ToInt16(vpn.UpdateServerDetailsByClientId(ClientId, Status));
        }
        public DataTable GetEnabledVPNList()
        {
            vpn = new tblVPNTableAdapter();
            return vpn.GetServerEnabledList();
        }
        public DataTable VPNServerStatusByClientId()
        {
            vpnServer = new tblVPNServerStausTableAdapter();
            return vpnServer.GetVPNServerStatusByClientId(ClientId);
        }
    }
    public class ProfileWebsite
    {
        public int ProfileId { get; set; }
        public int CategoryId { get; set; }
        public string WebsiteUrl { get; set; }
        public int IsWhiteList { get; set; }
        public int Status { get; set; }
    }
}
