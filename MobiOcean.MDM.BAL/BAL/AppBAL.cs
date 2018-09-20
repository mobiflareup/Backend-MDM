using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MobiOcean.MDM.DAL.DAL.AppDALTableAdapters;

/// <summary>
/// Summary description for AppBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class AppBAL
    {
        tblChatAppTableAdapter chatapptableadapter;
        tblAppGroupTableAdapter appgrp;
        AssignGrpNameTableAdapter assgngrp;
        UnAssignGrpNameTableAdapter unassgngrp;
        AssignProfileGrpNameTableAdapter assgnprofilegrp;
        UnAssignProfileGrpNameTableAdapter unassgnprofilegrp;
        GetAppNameByProfileIdTableAdapter getappname;
        tblAppMasterTableAdapter Appmstr;
        GetWebsitUrlByProfileIdTableAdapter getwebsite;
        GetSosContactsByProfileIdTableAdapter GetSosContacts;
        GetAreaByProfileIdTableAdapter getarea;
        AppMarketTableAdapter appmarket;
        public string AppId { get; set; }
        public int DeviceId { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public List<ChatAppLst> chatAppLst { get; set; }
        public DataTable dtAppLst { get; set; }
        private string _LoggedBy;
        public string LoggedBy
        {
            get { return _LoggedBy; }
            set { _LoggedBy = value; }
        }
        public AppBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int spChatAppList()
        {
            try
            {
                chatapptableadapter = new tblChatAppTableAdapter();
                return Convert.ToInt32(chatapptableadapter.spChatAppList(ClientId, UserId, DeviceId, dtAppLst).ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private int _AppGroupId;
        private int _ProfileId;
        public int ProfileId
        {
            get { return _ProfileId; }
            set { _ProfileId = value; }
        }
        public int AppGroupId
        {
            get { return _AppGroupId; }
            set { _AppGroupId = value; }
        }
        private string _AppGroupCode, _AppGroupName, _ApplicationIdList;
        public string AppGroupCode
        {
            get { return _AppGroupCode; }
            set { _AppGroupCode = value; }
        }
        public string AppGroupName
        {
            get { return _AppGroupName; }
            set { _AppGroupName = value; }
        }
        public string ApplicationIdList
        {
            get { return _ApplicationIdList; }
            set { _ApplicationIdList = value; }
        }
        public string AppName { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? UseStatus { get; set; }
        public bool? ChargeStatus { get; set; }
        public int? AppPrice { get; set; }
        public string ApkPath { get; set; }
        public bool? AutoPush { get; set; }
        public string ImagesPath { get; set; }
        public string Developer { get; set; }
        public string AppType { get; set; }
        public string AppIntroduce { get; set; }
        public string AppPackage { get; set; }
        public string AppVersion { get; set; }
        public string AppVersionNo { get; set; }
        public string Os_path { get; set; }
        public string AppSize { get; set; }
        public string Remark { get; set; }
        public bool? AllowDownload { get; set; }
        public bool? MandatoryUpdate { get; set; }
        public int? ApplyDeviceType { get; set; }
        public int AppMarketId { get; set; }
        public int OsId { get; set; }
        public DataTable GetGrpNameAndGrpCode()
        {
            appgrp = new tblAppGroupTableAdapter();
            return appgrp.GetGrpNameAndGrpCode();
        }
        public int DeleteAppGrpDtls()
        {
            appgrp = new tblAppGroupTableAdapter();
            return appgrp.DeleteAppGrpDtls(_AppGroupId);
        }
        public string spAppGrpDtls()
        {
            appgrp = new tblAppGroupTableAdapter();
            return appgrp.spAppGrpDtls(_AppGroupId, _AppGroupCode, _AppGroupName).ToString();
        }
        public int AssignGrpName()
        {
            try
            {
                assgngrp = new AssignGrpNameTableAdapter();
                assgngrp.AssignGrpName(_AppGroupId, _AppGroupName, _ApplicationIdList, ClientId, _LoggedBy);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                assgngrp = null;
            }
        }
        public int UnAssignGrpName()
        {
            try
            {
                unassgngrp = new UnAssignGrpNameTableAdapter();
                unassgngrp.UnAssignGrpName(_AppGroupId, _AppGroupName, _ApplicationIdList, ClientId, _LoggedBy);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                assgngrp = null;
            }
        }
        public int AssignProfileGrpName()
        {
            try
            {
                assgnprofilegrp = new AssignProfileGrpNameTableAdapter();
                assgnprofilegrp.AssignProfileGrpName(_AppGroupId, _AppGroupName, _ApplicationIdList, ClientId, _LoggedBy, _ProfileId);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                assgngrp = null;
            }
        }
        public int UnAssignProfileGrpName()
        {
            try
            {
                unassgnprofilegrp = new UnAssignProfileGrpNameTableAdapter();
                unassgnprofilegrp.UnAssignProfileGrpName(_AppGroupId, _AppGroupName, _ApplicationIdList, ClientId, _LoggedBy, _ProfileId);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                assgngrp = null;
            }
        }
        public int GetAppNameByProfileId()
        {
            try
            {
                getappname = new GetAppNameByProfileIdTableAdapter();
                getappname.GetAppNameByProfileId(ClientId, _ProfileId);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                getappname = null;
            }
        }
        public int GetWebsiteUrlByProfileId()
        {
            try
            {
                getwebsite = new GetWebsitUrlByProfileIdTableAdapter();
                getwebsite.GetWebsitUrlByProfileId(ClientId, _ProfileId);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {

            }
        }
        public int GetSosContactsByProfileId()
        {
            try
            {
                GetSosContacts = new GetSosContactsByProfileIdTableAdapter();
                GetSosContacts.GetSosContactsByProfileId(ClientId, ProfileId);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public DataTable GetMiscData()
        {
            Appmstr = new tblAppMasterTableAdapter();
            return Appmstr.GetMiscApp();
        }
        public DataTable GetAppGrpData()
        {
            appgrp = new tblAppGroupTableAdapter();
            return appgrp.GetAppGrpData();
        }
        public DataTable GetAreaByProfileId()
        {
            getarea = new GetAreaByProfileIdTableAdapter();
            return getarea.GetAreaByProfileId(ClientId, _ProfileId);
        }
        public void InsertAppMarket()
        {
            appmarket = new AppMarketTableAdapter();
            appmarket.InserAppMarket(AppMarketId, AppName, UseStatus, ChargeStatus, AppPrice, ApkPath, AutoPush,
               ImagesPath, Developer, AppType, AppIntroduce, AppPackage, AppVersion, AppSize, Remark, 0, UserId, CreationDate, ClientId);
        }
        public int? AppVersionExists()
        {
            appmarket = new AppMarketTableAdapter();
            return appmarket.AppVersionExists(AppPackage, AppVersion);
        }
        public DataTable GetPackageName()
        {
            appmarket = new AppMarketTableAdapter();
            return appmarket.GetPackageNames();
        }
        public DataTable GetDeviceType()
        {
            appmarket = new AppMarketTableAdapter();
            return appmarket.GetDeviceType();
        }
        public DataTable GetVersions()
        {
            appmarket = new AppMarketTableAdapter();
            return appmarket.GetVersionByPackageName(AppPackage);
        }
        public int InsertOTAPackage()
        {
            appmarket = new AppMarketTableAdapter();
            return appmarket.InsertAppOSMarket(OsId, AppPackage, AppVersion, AppVersionNo, Os_path, Remark, UserId, CreationDate, ClientId);
        }
        public int DeleteAppMarke()
        {
            appmarket = new AppMarketTableAdapter();
            return (int)appmarket.DeleteAppMarket(AppMarketId);
        }
        public int AppMarketDeviceMapping()
        {
            appmarket = new AppMarketTableAdapter();
            return appmarket.AppmarketDeviceMapping(ApplyDeviceType, AppMarketId);
        }
        public int DeleteOTAApp()
        {
            appmarket = new AppMarketTableAdapter();
            return (int)appmarket.DeleteOTGApp(OsId);
        }
        public int AssignDeviceAppMarket(DataTable dt)
        {
            appmarket = new AppMarketTableAdapter();
            return appmarket.Insert_AssignAppMarket(dt);
        }

        public int AssignDeviceAppMarket(DataTable dt, int btn)
        {
            if (btn == 0)
            {
                appmarket = new AppMarketTableAdapter();
                return appmarket.Insert_AssignAppMarketOsUpgrade(dt);
            }
            else if (btn == 1)
            {
                appmarket = new AppMarketTableAdapter();
                return appmarket.Insert_AssignAppMarketAppInstall(dt);
            }
            else if (btn == 2)
            {
                appmarket = new AppMarketTableAdapter();
                return appmarket.Insert_AssignAppMarketAppUpdate(dt);
            }
            else if (btn == 3)
            {
                appmarket = new AppMarketTableAdapter();
                return appmarket.Insert_AssignAppMarketAppUnInstall(dt);
            }
            return 0;
        }
    }
    public class ChatAppLst
    {
        public string ChatApp { get; set; }
        public int AppIndx { get; set; }
        public int IsInstalled { get; set; }
        public int LogDateTime { get; set; }
    }
}
