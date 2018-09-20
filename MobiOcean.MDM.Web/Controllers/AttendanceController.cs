using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Web.Script.Serialization;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.BAL;
using System.Web.Http.Cors;

namespace MobiOcean.MDM.Web.Controller
{
    public class AttendanceController : APIBase
    {
        AttendanceBAL att;
        LocationBAL gapi;
        int ClientId = 0, UserId = 0;
        DataTable dt, dt1, dt2;
        [HttpPost]
        public int InsertAttendenceDetails([FromBody]AttendanceBAL value)
        {
            try
            {
                dt = getDeviceDtlByAppId(value.appId);
                if (dt.Rows.Count > 0)
                {
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                }
            }
            catch (Exception)
            { }
            if (ClientId > 0)
            {
                gapi = new LocationBAL();
                LocationModel locationModel = gapi.GetLocation(value.Latitude, value.Longitude, value.CellId.ToString(), value.LAC.ToString(), value.MCC.ToString(), value.MNC.ToString(), ClientId, Constant.Attendence, UserId);
                InsertAttendance(locationModel.latitude.ToString(), locationModel.longitude.ToString(), locationModel.location, value.LogDateTime, value.IsLogin);
                return 1;
            }
            else
            {
                return 0;
            }

        }
        [HttpPost]
        public int Insert([FromBody]AttendanceBAL value)
        {
            try
            {
                try
                {
                    dt = getDeviceDtlByAppId(value.appId);
                    if (dt.Rows.Count > 0)
                    {
                        ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                        UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    }
                }
                catch (Exception)
                { }
                if (ClientId > 0)
                {
                    value.Location = value.IsLogin == 1 ? value.InLocation : value.OutLocation;
                    string towwerLocation = value.CellId + "," + value.LAC + "," + value.MCC + "," + value.MNC;
                    gapi = new LocationBAL();
                    LocationModel locationModel = gapi.GetLocation(value.Latitude, value.Longitude, value.CellId.ToString(), value.LAC.ToString(), value.MCC.ToString(), value.MNC.ToString(), ClientId, Constant.Attendence, UserId);
                    if (string.IsNullOrEmpty(value.Location))
                    {
                        //manual=0   
                        return InsertAttendance1(locationModel.latitude.ToString(), locationModel.longitude.ToString(), locationModel.location, value.LogDateTime, value.IsLogin, false, towwerLocation, value.Location, value.AttendanceId, value.Imagepath);
                    }
                    else
                    {
                        //manual=1
                        string locationByCellId = "";
                        if (locationModel.location != Constant.LocationNotFound)
                        {
                            value.Latitude = locationModel.latitude.ToString();
                            value.Longitude = locationModel.longitude.ToString();
                            locationByCellId = locationModel.location;
                        }
                        return InsertAttendance1(value.Latitude.ToString(), value.Longitude.ToString(), value.Location, value.LogDateTime, value.IsLogin, true, towwerLocation, locationByCellId, value.AttendanceId, value.Imagepath);
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }
        [HttpGet]
        public int AttendanceEnable(string appId)
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
            if (ClientId > 0)
            {
                //FeatureBAL ftrBal = new FeatureBAL();
                //ftrBal.ClientId = ClientId;
                //return ftrBal.IsAttendanceEnable();
                return 1;
            }
            else
            {
                return 0;
            }
        }
        [HttpPost]
        public void UpdateAttendanceDetails()
        {
            att = new AttendanceBAL();
            dt = new DataTable();
            dt = att.GetVodaFoneUsersShiftList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    double InTimediff = (DateTime.UtcNow.AddMinutes(Constant.addMinutes) - Convert.ToDateTime(dt.Rows[i]["InDateTime"])).TotalMinutes;
                    double OutTimediff = (DateTime.UtcNow.AddMinutes(Constant.addMinutes) - Convert.ToDateTime(dt.Rows[i]["OutDateTime"])).TotalMinutes;
                    att.UserId = Convert.ToInt32(dt.Rows[i]["UserId"].ToString());
                    att.ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"].ToString());

                    if (InTimediff > 0 && InTimediff < 30)
                    {

                        dt1 = new DataTable();
                        att.Time = Convert.ToDateTime(dt.Rows[i]["InDateTime"]).ToString("dd-MMM-yyyy HH:mm");
                        dt1 = att.GetVodaFoneLocationFromUserId();
                        att.Time = Convert.ToDateTime(dt.Rows[i]["InDateTime"]).ToString("HH:mm");
                        att.IsLogin = 1;
                        att.AttendanceDate = dt.Rows[i]["Date"].ToString();
                        att.AttendanceDateTime = Convert.ToDateTime(dt.Rows[i]["InDateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss.fff");
                        if (dt1.Rows.Count > 0)
                        {
                            att.Latitude = dt1.Rows[0]["Latitude"].ToString();
                            att.Longitude = dt1.Rows[0]["Longitude"].ToString();
                            att.Location = dt1.Rows[0]["Location"].ToString();
                            if (IsWithinGeoFence(att.Latitude, att.Longitude, att.UserId))
                            {
                                att.InsertAttendanceDetailsWithVodaFoneDetails();
                            }
                        }
                    }
                    if (OutTimediff > 0 && OutTimediff < 30)
                    {
                        dt2 = new DataTable();
                        att.Time = Convert.ToDateTime(dt.Rows[i]["OutDateTime"]).ToString("dd-MMM-yyyy HH:mm");
                        dt2 = att.GetVodaFoneLocationFromUserId();
                        att.Time = Convert.ToDateTime(dt.Rows[i]["OutDateTime"]).ToString("HH:mm");
                        att.IsLogin = 0;
                        att.AttendanceDate = dt.Rows[i]["Date"].ToString();//dt.Rows[i]["OutDateTime"].ToString()
                        att.AttendanceDateTime =  Convert.ToDateTime(dt.Rows[i]["OutDateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss.fff");
                        if (dt2.Rows.Count > 0)
                        {
                            att.Latitude = dt2.Rows[0]["Latitude"].ToString();
                            att.Longitude = dt2.Rows[0]["Longitude"].ToString();
                            att.Location = dt2.Rows[0]["Location"].ToString();
                            if (IsWithinGeoFence(att.Latitude, att.Longitude, att.UserId))
                            {
                               att.InsertAttendanceDetailsWithVodaFoneDetails();
                            }
                        }
                    }
                }
                catch (Exception) { }
            }
        }
        private int InsertAttendance1(string latitude, string longitude, string location, string logdatetime, int islogin, bool isLocationManuallyEntered, string towerlocation, string manualLocation, int AttendanceId, string ImagePath)
        {
            att = new AttendanceBAL();
            att.IsLogin = islogin;
            att.ClientId = ClientId;
            att.UserId = UserId;
            att.Location = location;
            logdatetime = getDateInSQlServerFormat(logdatetime);
            att.AttendanceDate = Convert.ToDateTime(logdatetime).ToString(Constant.DateFormat);
            att.Latitude = latitude;
            att.Longitude = longitude;
            att.Time = Convert.ToDateTime(logdatetime).ToString("HH:mm");
            att.AttendanceDateTime = Convert.ToDateTime(logdatetime).ToString(Constant.DateTimeFormat);
            att.IsLocationManuallyEntered = isLocationManuallyEntered;
            att.TowerLocation = towerlocation;
            att.ManualLoaction = manualLocation;
            att.Imagepath = ImagePath;
            att.AttendanceId = AttendanceId;
            int res = att.InsertAttendanceDetails();
            return res;
        }
        private int InsertAttendance(string latitude, string longitude, string location, string logdatetime, int islogin)
        {
            att = new AttendanceBAL();
            att.IsLogin = islogin;
            att.ClientId = ClientId;
            att.UserId = UserId;
            att.Location = location;
            logdatetime = getDateInSQlServerFormat(logdatetime);
            att.AttendanceDate = Convert.ToDateTime(logdatetime).ToString(Constant.DateFormat);
            att.Latitude = latitude;
            att.Longitude = longitude;
            att.Time = Convert.ToDateTime(logdatetime).ToString("HH:mm");
            att.AttendanceDateTime = Convert.ToDateTime(logdatetime).ToString(Constant.DateTimeFormat);
            int res = att.InsertAttendanceDetailsWithDate();
            return 1;
        }
        private bool IsWithinGeoFence(string lat1, string long1, int UserId)
        {
            AttendanceBAL attgeo = new AttendanceBAL();
            DataTable geo = new DataTable();
            geo = attgeo.GeoFenceDataForUser(UserId);
            bool res = false;
            if (geo != null && geo.Rows.Count > 0)
            {
                foreach (DataRow row in geo.Rows)
                {
                    string lat2 = row["Latitude"].ToString();
                    string long2 = row["Longitude"].ToString();
                    int georadius = Convert.ToInt32(geo.Rows[0]["Radius"].ToString());
                    if (Convert.ToDouble(lat1) != 0 && Convert.ToDouble(long1) != 0 && Convert.ToDouble(lat2) != 0 && Convert.ToDouble(long2) != 0)
                    {
                        gapi = new LocationBAL();
                        double distance = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(lat1), Convert.ToDouble(long1), Convert.ToDouble(lat2), Convert.ToDouble(long2));
                        if (distance < (georadius * 1000))
                        {
                            res = true;
                            break;
                        }
                    }
                }
                return res;
            }
            else
            {
                return !res;
            }
        }
    }
}
