using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controller
{
    public class AllowedController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        MDM.BAL.Model.ContactList contactList;
        WebsiteLogsBAL webbal;
        DataTable dt;
        [ActionName("MobileNo")]
        public List<MDM.BAL.Model.ContactList> Get(string AppId, int IsForUpdate = 0)
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
                    List<MDM.BAL.Model.ContactList> contlst = new List<MDM.BAL.Model.ContactList>();
                    contactList = new MDM.BAL.Model.ContactList();
                    contactList.DeviceId = DeviceId;
                    contactList.ClientId = ClientId;
                    contlst = contactList.GetAllowedNoList(IsForUpdate);
                    return contlst;
                }
                catch (Exception)
                {
                    List<MDM.BAL.Model.ContactList> contlst = new List<MDM.BAL.Model.ContactList>();
                    return contlst;
                }
                finally
                {
                    contactList = null;
                }
            }
            else
            {
                List<MDM.BAL.Model.ContactList> contlst = new List<MDM.BAL.Model.ContactList>();
                return contlst;
            }
        }

        [ActionName("Website")]
        public List<WebsiteLogsBAL> Get(string AppId, int IsForUpdate = 0, int IsWeb = 1)
        {
            List<WebsiteLogsBAL> lst = new List<WebsiteLogsBAL>();
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
                            webbal = new WebsiteLogsBAL
                            {
                                WebsiteUrl = row["Url"].ToString().Trim().Replace("www.", "") + " ",
                                IsWhiteList = Convert.ToInt32(row["IsWhiteList"].ToString()),
                                ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                                UrlId = Convert.ToInt32(row["UrlId"].ToString()),
                                Status = Convert.ToInt32(row["Status"].ToString()),
                                CategoryId = Convert.ToInt32(string.IsNullOrEmpty(row["CategoryId"].ToString()) ? "0" : row["CategoryId"].ToString()),
                                CategoryName = row["CategoryName"].ToString()
                            };
                            lst.Add(webbal);
                        }
                    }
                    else
                    {
                        dt = webbal.sp_ProfileBlackListUrl();
                        foreach (DataRow row in dt.Rows)
                        {
                            webbal = new WebsiteLogsBAL
                            {
                                WebsiteUrl = row["Url"].ToString().Trim().Replace("www.", "") + " ",
                                IsWhiteList = Convert.ToInt32(row["IsWhiteList"].ToString()),
                                ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                                UrlId = Convert.ToInt32(row["UrlId"].ToString()),
                                Status = Convert.ToInt32(row["Status"].ToString()),
                                CategoryId = Convert.ToInt32(string.IsNullOrEmpty(row["CategoryId"].ToString()) ? "0" : row["CategoryId"].ToString()),
                                CategoryName = row["CategoryName"].ToString()
                            };
                            lst.Add(webbal);
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

    }
}
