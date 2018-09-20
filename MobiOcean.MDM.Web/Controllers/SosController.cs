
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;


namespace MobiOcean.MDM.Web.Controller
{
    public class SosController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;
        WipePhoneBAL WipePhone, ListWipePhone;

        [ActionName("SOSmsg")]
        public string Post([FromBody]LocationBAL value)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(value.AppId);
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
                value.DeviceId = DeviceId;
                value.UserId = UserId;
                value.ClientId = ClientId;
                LocationModel locationModel = value.GetLocation(value.Latitude, value.Longitude, value.CellId.ToString(), value.locationAreaCode.ToString(), value.mobileCountryCode.ToString(), value.mobileNetworkCode.ToString(), ClientId, Constant.tblDeviceLocation_tblDeviceLocationGeoFence, UserId);
                value.Latitude = locationModel.latitude.ToString();
                value.Longitude = locationModel.longitude.ToString();
                value.Location = locationModel.location;
                if (Regex.Match(value.LocSource, "Google : dailyLimitExceeded", RegexOptions.IgnoreCase).Success)
                {
                    value.LocSource = "Failed to get Location";
                }
                if (Regex.Match(value.LocSource, "Google : keyInvalid", RegexOptions.IgnoreCase).Success)
                {
                    value.LocSource = "Failed to get Location";
                }
                if (Regex.Match(value.LocSource, "Google: keyInvalid", RegexOptions.IgnoreCase).Success)
                {
                    value.LocSource = "Failed to get Location";
                }
                if (Regex.Match(value.LocSource, "GoogLe  : Failed to get response from Google", RegexOptions.IgnoreCase).Success)
                {
                    value.LocSource = "Failed to get Location";
                }
                if (Regex.Match(value.LocSource, "GoogLe : Failed to get response from Google", RegexOptions.IgnoreCase).Success)
                {
                    value.LocSource = "Failed to get location";
                }
                return value.makeLocEntryByAppId() + "";

            }
            else
            {
                return "-2";
            }
        }
        [ActionName("InsertSosContactDetails")]
        public string Post([FromBody]WipePhoneBAL WipePhone)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(WipePhone.appID);
                if (dt.Rows.Count > 0)
                {
                    return InsertSosContacts(Convert.ToInt32(dt.Rows[0]["UserId"].ToString()), Convert.ToInt32(dt.Rows[0]["ClientId"].ToString()), WipePhone.SosContacts).ToString();
                }
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }

        }
        private int InsertSosContacts(int UserId, int ClientId, SosContacts[] SosContacts)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ContactPersonName");
                dt.Columns.Add("MobileNo");
                dt.Columns.Add("Status");
                for (int i = 0; i < SosContacts.Length; i++)
                {
                    dt.Rows.Add(SosContacts[i].ContactPersonName, SosContacts[i].MobileNo, 0);
                }
                WipePhone = new WipePhoneBAL();
                WipePhone.UserId = UserId;
                WipePhone.ClientId = ClientId;
                WipePhone.SosContacts = SosContacts;
                WipePhone.dt = dt;
                return WipePhone.InsertSosDtl();
            }
            catch (Exception)
            {
                return 0;
            }

        }
        [ActionName("GetSosContactList")]
        public List<WipePhoneBAL> Get(string appID)
        {
            List<WipePhoneBAL> WipePhoneList = new List<WipePhoneBAL>();
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(appID);
                if (dt.Rows.Count > 0)
                {
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
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
                WipePhone = new WipePhoneBAL();
                WipePhone.UserId = UserId;
                dt = WipePhone.GetSosContacts();
                foreach (DataRow row in dt.Rows)
                {
                    ListWipePhone = new WipePhoneBAL
                    {
                        ContactId = Convert.ToInt32(row["ContactId"].ToString()),
                        ContactPersonName = row["ContactPersonName"].ToString(),
                        MobileNo = row["ContactNo"].ToString()
                    };
                    WipePhoneList.Add(ListWipePhone);
                }
                return WipePhoneList;
            }
            return WipePhoneList;
        }
        [ActionName("InsertSosCameraDetails")]
        public string Post([FromBody]SOSBAL sos)
        {
            try
            {
                int res = sos.InsertSosCamera();
                if (res > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch
            {
                return "0";
            }
        }
    }
}
