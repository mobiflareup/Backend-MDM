using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

/// <summary>
/// Summary description for PolicyController
/// </summary>
/// 
namespace MobiOcean.MDM.Web.Controller
{
    public class PolicyController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        ProfileBAL profile;
        Policy policy;
        ProfileUserMappingBAL proUsr;
        DataTable dt;
        MDM.BAL.Model.AppGroup appgroup;
        GroupBAL grpbal;
        MDM.BAL.Model.ContactList contactList;
        MDM.BAL.BAL.SensorDetails profilesensorlst;
        WebsiteLogsBAL webbal;
        SensorBAL sensor;
        ProfileWebsite pw;

        [HttpGet]
        public List<Policy> GetProfileList(string AppId, int IsForUpdate = 0)
        {
            List<Policy> profilelist = new List<Policy>();
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                DeviceId = 0;
            }
            if (DeviceId > 0)
            {
                try
                {

                    dt = new DataTable();
                    profile = new ProfileBAL();
                    profile.ClientId = ClientId;
                    profile.DeviceId = DeviceId;
                    if (IsForUpdate == 0)
                    {
                        dt = profile.GetProfileByClient();
                    }
                    else
                    {
                        dt = profile.GetProfileByClientUpdate();
                    }
                    if (dt.Rows.Count > 0)
                    {

                        DateTime dateTime = GetCurrentDateTimeByUserId(UserId);
                        foreach (DataRow row in dt.Rows)
                        {
                            policy = new Policy
                            {
                                //ProfileFeatureMappingId = Convert.ToInt32(row["ProfileFeatureMappingId"]),
                                ProfileId = Convert.ToInt32(row["ProfileId"]),
                                IsEnable = Convert.ToInt32(row["IsEnable"].ToString()),
                                Message = row["Message"].ToString(),
                                FeatureId = Convert.ToInt32(row["FeatureId"].ToString()),
                                FeatureStatus = Convert.ToInt32(row["FeatureStatus"].ToString()),
                                ProfileName = row["ProfileName"].ToString(),
                                IsBlackList = Convert.ToInt32(string.IsNullOrEmpty(row["Duration"].ToString()) ? "0" : row["Duration"].ToString())
                            };
                            //try
                            //{
                            //    MobiOceanProfileUpdateBAL MPBAL = new MobiOceanProfileUpdateBAL();
                            //    MPBAL.InsertProfileList(UserId, policy, dateTime);
                            //}
                            //catch(Exception)
                            //{ }
                            profilelist.Add(policy);
                        }
                    }
                    return profilelist;
                }
                catch (Exception)
                {
                    return profilelist;
                }
                finally
                {
                    profile = null;
                }
            }
            else
            {
                return profilelist;
            }
        }

        [HttpGet]
        public List<ProfileBAL> GetProfile(string AppId, int IsForUpdate = 0)
        {

            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                DeviceId = 0;
            }
            if (DeviceId > 0)
            {
                try
                {
                    List<ProfileBAL> profilelist = new List<ProfileBAL>();
                    dt = new DataTable();
                    profile = new ProfileBAL();
                    profile.ClientId = ClientId;
                    profile.DeviceId = DeviceId;
                    profile.IsForUpdate = IsForUpdate;
                    dt = profile.GetProfileForApp();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            profile = new ProfileBAL
                            {
                                ProfileId = Convert.ToInt32(row["ProfileId"]),
                                ProfileCode = row["ProfileCode"].ToString(),
                                ProfileName = row["ProfileName"].ToString(),
                                ProfilePurpose = row["ProfilePurpose"].ToString(),
                                ProfileStatus = Convert.ToInt32(row["ProfileStatus"].ToString())
                            };
                            profilelist.Add(profile);
                        }
                    }
                    return profilelist;
                }
                catch (Exception)
                {
                    List<ProfileBAL> profilelist = new List<ProfileBAL>();
                    return profilelist;
                }
                finally
                {
                    profile = null;
                }
            }
            else
            {
                List<ProfileBAL> profilelist = new List<ProfileBAL>();
                return profilelist;
            }
        }

        [HttpGet]
        public int GetActivatedProfile(string AppId)
        {

            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                DeviceId = 0;
            }
            if (DeviceId > 0)
            {
                try
                {
                    proUsr = new ProfileUserMappingBAL();
                    proUsr.DeviceId = DeviceId;
                    dt = proUsr.GetProfileDeviceMappingByDeviceId();
                    if (dt.Rows.Count > 0)
                    {
                        return Convert.ToInt32(dt.Rows[0]["ProfileId"]);
                    }
                    return 0;
                }
                catch (Exception)
                {
                    return 0;
                }

            }
            else
            {
                return 0;
            }
        }

        [HttpGet]
        public List<MDM.BAL.Model.AppGroup> GetAppGroup(string AppId, int IsForUpdate = 0)
        {
            List<MDM.BAL.Model.AppGroup> grplst = new List<MDM.BAL.Model.AppGroup>();
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                DeviceId = 0;
            }
            if (DeviceId > 0)
            {
                grpbal = new GroupBAL();
                dt = new DataTable();
                grpbal.DeviceId = DeviceId;
                grpbal.ClientId = ClientId;
                grpbal.AppGrouping();
                grpbal.IsForUpdate = IsForUpdate;
                dt = grpbal.GetAppGroupByDeviceIdUpdate();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        appgroup = new MDM.BAL.Model.AppGroup
                        {
                            ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                            Message = row["Message"].ToString(),
                            ChatGroupId = Convert.ToInt32(row["GroupId"].ToString()),
                            IsEnable = Convert.ToInt32(row["IsEnable"].ToString()),
                        };
                        grplst.Add(appgroup);
                    }

                }


            }
            return grplst;
        }

        [HttpGet]
        public List<ProfileContactList> MobileNo(string AppId, int IsForUpdate = 0)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                DeviceId = 0;
            }
            if (DeviceId > 0)
            {
                try
                {
                    List<ProfileContactList> contlst = new List<ProfileContactList>();
                    contactList = new MDM.BAL.Model.ContactList();
                    contactList.DeviceId = DeviceId;
                    contactList.ClientId = ClientId;
                    contlst = contactList.GetAllowedNoList1(IsForUpdate);
                    return contlst;
                }
                catch (Exception)
                {
                    List<ProfileContactList> contlst = new List<ProfileContactList>();
                    return contlst;
                }
                finally
                {
                    contactList = null;
                }
            }
            else
            {
                List<ProfileContactList> contlst = new List<ProfileContactList>();
                return contlst;
            }
        }

        [HttpGet]
        public List<MDM.BAL.BAL.SensorDetails> GetWifiSensorDtls(string appId)
        {
            List<MDM.BAL.BAL.SensorDetails> lst = new List<MDM.BAL.BAL.SensorDetails>();
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(appId);
                if (dt.Rows.Count > 0)
                {
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
                else
                {
                    ClientId = 0;
                }
            }
            catch (Exception)
            {
                ClientId = 0;
            }
            if (ClientId > 0)
            {
                sensor = new SensorBAL();
                sensor.ClientId = ClientId;
                dt = sensor.GetWifiSensorDetails();
                foreach (DataRow row in dt.Rows)
                {
                    profilesensorlst = new MDM.BAL.BAL.SensorDetails
                    {
                        ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                        BSSID = row["BSSID"].ToString(),
                        SSID = row["SSID"].ToString(),
                        Password = row["Password"].ToString()
                    };
                    lst.Add(profilesensorlst);
                }
            }
            return lst;
        }

        [HttpGet]
        public List<ProfileWebsite> Website(string AppId, int IsForUpdate = 0)
        {
            List<ProfileWebsite> lst = new List<ProfileWebsite>();
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                DeviceId = 0;
            }
            if (DeviceId > 0)
            {
                try
                {
                    webbal = new WebsiteLogsBAL();
                    dt = new DataTable();
                    webbal.ClientId = ClientId;
                    webbal.DeviceId = DeviceId;
                    if (IsForUpdate == 0)
                    {
                        dt = webbal.GetProfileBlackListUrlByProfileId();
                        // p.IsWhiteList, p.ProfileId, u.Url, p.UrlId,p.Status,p.CategoryId,c.CategoryName
                        foreach (DataRow row in dt.Rows)
                        {
                            pw = new ProfileWebsite
                            {
                                WebsiteUrl = row["Url"].ToString().Trim().Replace("www.", "") + " ",
                                IsWhiteList = Convert.ToInt32(row["IsWhiteList"].ToString()),
                                ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                                Status = Convert.ToInt32(row["Status"].ToString()),
                                CategoryId = Convert.ToInt32(string.IsNullOrEmpty(row["CategoryId"].ToString()) ? "0" : row["CategoryId"].ToString()),
                            };
                            lst.Add(pw);
                        }
                    }
                    else
                    {
                        dt = webbal.sp_ProfileBlackListUrl();
                        foreach (DataRow row in dt.Rows)
                        {
                            pw = new ProfileWebsite
                            {
                                WebsiteUrl = row["Url"].ToString().Trim().Replace("www.", "") + " ",
                                IsWhiteList = Convert.ToInt32(row["IsWhiteList"].ToString()),
                                ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                                Status = Convert.ToInt32(row["Status"].ToString()),
                                CategoryId = Convert.ToInt32(string.IsNullOrEmpty(row["CategoryId"].ToString()) ? "0" : row["CategoryId"].ToString()),
                            };
                            lst.Add(pw);
                        }
                    }

                    return lst;
                }
                catch (Exception)
                {
                    return lst;
                }
                finally
                {
                    lst = null;
                }
            }
            else
            {
                return lst;
            }
        }

        [HttpPost]
        public int AdminLogin([FromBody]LoginBAL val)
        {
            return val.AdminValidation();
        }

        [HttpGet]
        public int IsWhiteList(int clientid)
        {
            LoginBAL val = new LoginBAL();
            val.ClientId = clientid;
            return val.IsWhiteListByClientId();
        }

        [HttpGet]
        public WebsiteUrlBase WhiteListAndBlackList(int clientid)
        {
            WebsiteUrlBase urlBase = new WebsiteUrlBase();
            WebsiteLogsBAL websitelogsBal = new WebsiteLogsBAL();
            bool IsEnabled = false;
            websitelogsBal.ClientId = clientid;
            dt = new DataTable();
            dt = websitelogsBal.VPNDetailsByClientId();
            if (dt.Rows.Count > 0)
            {
                IsEnabled = Convert.ToBoolean(dt.Rows[0]["IsEnabled"]);
                urlBase.IsWhiteList = Convert.ToInt16(dt.Rows[0]["IsWhiteList"]);
            }
            if (IsEnabled)
            {
                LoginBAL val = new LoginBAL();
                val.ClientId = clientid;
                val.IswhiteList = Convert.ToInt16(urlBase.IsWhiteList);
                List<WebsiteUrlChild> url = new List<WebsiteUrlChild>();
                dt = val.WhiteListAndBlackList();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        WebsiteUrlChild ur = new WebsiteUrlChild()
                        {
                            Url = dt.Rows[i]["Url"].ToString(),
                            //IsWhiteList = (int)dt.Rows[i]["IsWhiteList"]
                        };
                        url.Add(ur);
                    }
                    urlBase.urls = url;
                }
            }
            else
            {
                urlBase.IsWhiteList = 0;
            }
            return urlBase;
        }

        [HttpGet]
        public AttendanceEnable AttendanceEnable(string appId)
        {
            try
            {
                dt = getDeviceDtlByAppId(appId);
                if (dt.Rows.Count > 0)
                {
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                ClientId = 0;
            }
            AttendanceEnable at = new AttendanceEnable
            {
                IsAttendance = false,
                IsConveyance = false,
                IsTravelAllowance = false,
                IsSecureStorage = false,
                IsGenus = false,
                IsFMMEnable = false,
                IsCameraEnable = false

            };
            if (ClientId > 0)
            {
                FeatureBAL ftrBal = new FeatureBAL();
                if (ftrBal.CheckFeature(ClientId, "22") == 1)
                {
                    try
                    {
                        UserBAL usr = new UserBAL();
                        usr.UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                        DataTable dtUser = usr.GetUserDtlByUserId();
                        if (dtUser != null && dtUser.Rows.Count > 0 && dtUser.Rows[0]["AttendanceTypeId"].ToString().Contains('7'))
                        {
                            at.IsCameraEnable = true;
                        }
                    }
                    catch (Exception) { }
                    at.IsAttendance = true;
                }
                if (ftrBal.CheckFeature(ClientId, "25") == 1)
                {
                    at.IsTravelAllowance = true;
                    at.IsConveyance = true;
                }
                if (ftrBal.CheckFeature(ClientId, "29") == 1)
                {
                    at.IsSecureStorage = true;
                }
                if (ftrBal.CheckFeature(ClientId, "30") == 1)
                {
                    at.IsFMMEnable = true;
                }
                //if (ClientId == 244)
                //{
                //    at.IsGenus = true;
                //}
            }
            return at;
            //else
            //{
            //    if (appId.Contains("G"))//Genus
            //    {
            //        return 0;
            //    }
            //    return 0;
            //}
        }

        [HttpGet]
        public VPNModel VPNEnable(string appId)
        {
            try
            {
                dt = getDeviceDtlByAppId(appId);
                if (dt.Rows.Count > 0)
                {
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                ClientId = 0;
            }
            VPNModel vpn = new VPNModel
            {
                IsEnabled = false,
                IpAddress = null
            };
            if (ClientId > 0)
            {
                WebsiteLogsBAL websitelogsBal = new WebsiteLogsBAL();
                websitelogsBal.ClientId = ClientId;
                dt = new DataTable();
                dt = websitelogsBal.VPNDetailsByClientId();
                if (dt.Rows.Count > 0)
                {
                    vpn.IpAddress = dt.Rows[0]["IPAddress"].ToString();
                    if (Convert.ToInt16(dt.Rows[0]["IsEnabled"]) == 1)
                        vpn.IsEnabled = true;
                    else
                        vpn.IsEnabled = false;
                }
            }
            return vpn;
        }

        [HttpGet]
        public int ServerStatus(int ClientId, int Status)
        {
            WebsiteLogsBAL websitelogsBal = new WebsiteLogsBAL();
            websitelogsBal.ClientId = ClientId;
            websitelogsBal.Status = Status;
            int res = websitelogsBal.ServerDetailsByClientId();
            if (res > 0)
                return 1;
            return 0;
        }
    }
}
