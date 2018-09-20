using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controller
{
    public class ProfileController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        ProfileBAL profile;
        ProfileUserMappingBAL proUsr;
        DataTable dt;
        [ActionName("GetProfileList")]
        public List<ProfileBAL> Get(string AppId, int IsForUpdate = 0)
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
                        foreach (DataRow row in dt.Rows)
                        {
                            profile = new ProfileBAL
                            {
                                ProfileFeatureMappingId = Convert.ToInt32(row["ProfileFeatureMappingId"]),
                                ProfileId = Convert.ToInt32(row["ProfileId"]),
                                IsEnable = Convert.ToInt32(row["IsEnable"].ToString()),
                                ProfileNo = row["ProfileNo"].ToString(),
                                OldProfileNo = row["OldProfileNo"].ToString(),
                                Message = row["Message"].ToString(),
                                FeatureId = Convert.ToInt32(row["FeatureId"].ToString()),
                                FeatureStatus = Convert.ToInt32(row["FeatureStatus"].ToString()),
                                ProfileCode = row["ProfileCode"].ToString(),
                                ProfileName = row["ProfileName"].ToString(),
                                ProfilePurpose = row["ProfilePurpose"].ToString(),
                                ProfileStatus = Convert.ToInt32(row["ProfileStatus"].ToString()),
                                Duration = Convert.ToInt32(string.IsNullOrEmpty(row["Duration"].ToString()) ? "0" : row["Duration"].ToString())
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
        [ActionName("GetProfile")]
        public List<ProfileBAL> Get(string AppId, int IsForUpdate = 0, int xf = 0)
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
        [ActionName("GetActivatedProfile")]
        public ProfileUserMappingBAL Get(string AppId)
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
                        proUsr = new ProfileUserMappingBAL
                        {
                            ProfileUserId = Convert.ToInt32(dt.Rows[0]["ProfileUserId"]),
                            ProfileId = Convert.ToInt32(dt.Rows[0]["ProfileId"]),
                            IsEnable = Convert.ToInt32(dt.Rows[0]["IsEnable"].ToString()),
                            Status = Convert.ToInt32(dt.Rows[0]["Status"].ToString())

                        };
                    }
                    else
                    {
                        proUsr = new ProfileUserMappingBAL();
                    }
                    return proUsr;
                }
                catch (Exception)
                {
                    proUsr = new ProfileUserMappingBAL();
                    return proUsr;
                }
                finally
                {
                    proUsr = null;
                }
            }
            else
            {
                proUsr = new ProfileUserMappingBAL();
                return proUsr;
            }
        }
        [ActionName("GetProfileId")]
        public int Get(string appId, int x = 0, int y = 0, int z = 0)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(appId);
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
                profile = new ProfileBAL();
                profile.DeviceId = DeviceId;
                profile.UserId = UserId;
                int res = profile.SpGetProfileId();
                return res;
            }
            else
            {
                return 0;
            }
        }
    }
}
