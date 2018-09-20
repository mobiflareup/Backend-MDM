using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.Infrastructure;
using MobiOcean.MDM.BAL.Model;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace MobiOcean.MDM.Web.Controller
{
    public class ConveyanceController : APIBase
    {
        ConveyanceBAL conveyance;
        LocationBAL gapi;
        int ClientId = 0, UserId = 0;
        DataTable dt;
        [ActionName("InsertConveyanceDetails")]
        public int Post([FromBody]ConveyanceBAL value)
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
            {

            }
            if (ClientId > 0)
            {
                //string location= "Location not found";
                gapi = new LocationBAL();
                LocationModel locationModel = gapi.GetLocation(value.Latitude, value.Longitude, value.CellId.ToString(), value.LAC.ToString(), value.MCC.ToString(), value.MNC.ToString(), ClientId, Constant.tblConveyanceDetails, UserId);
                //if (!string.IsNullOrEmpty(value.Latitude) && !string.IsNullOrEmpty(value.Longitude) && value.Latitude != "0" && value.Longitude != "0" && value.Latitude != "0.0" && value.Longitude != "0.0")
                //{
                //    location = gapi.getGoogleLocationByLogLatFromApi(value.Longitude, value.Latitude, ClientId, Constant.tblConveyanceDetails, Constant.GeoFence, UserId);
                //}
                //else if (value.CellId != 0 && value.MCC != 0 && value.MNC != 0 && value.LAC != 0)
                //{
                //    string[] LatLong = gapi.CellId_To_Lat(value.CellId, value.LAC, value.MCC.ToString(), value.MNC.ToString(), ClientId, Constant.tblConveyanceDetails, Constant.CellToLatLong, UserId).Split(',');
                //    if (LatLong.Count() == 2)
                //    {
                //        value.Latitude = LatLong[0].ToString();
                //        value.Longitude = LatLong[1].ToString();
                //        location = gapi.getGoogleLocationByLogLatFromApi(value.Longitude, value.Latitude, ClientId, Constant.tblConveyanceDetails, Constant.GeoFence, UserId);
                //    }                    
                //}
                return InsertConveyance(locationModel.latitude.ToString(), locationModel.longitude.ToString(), locationModel.location, value.LogDateTime, value.IsLogin, value.ConveyanceId, value.VehicleReading, value.ImagePath, value.Remark);
            }
            else
            {
                //value.IsFirst = 4;
                //value.Test();
                return 0;
            }
        }
        private int InsertConveyance(string latitude, string longitude, string location, string logdatetime, int islogin, int conveyanceId, double vehicleReading, string imagePath, string remark)
        {

            conveyance = new ConveyanceBAL();
            gapi = new LocationBAL();
            conveyance.IsLogin = islogin;
            conveyance.ClientId = ClientId;
            conveyance.UserId = UserId;
            conveyance.Location = location;
            conveyance.LogDateTime = getDateInSQlServerFormat(logdatetime);
            conveyance.Latitude = latitude;
            conveyance.Longitude = longitude;
            conveyance.ConveyanceId = conveyanceId;
            conveyance.VehicleReading = vehicleReading;
            conveyance.ImagePath = imagePath;
            conveyance.Remark = remark;
            if (islogin == 1)
            {
                conveyance.ConveyanceId = 0;
                int res = conveyance.InsertConveyance();
                conveyance.ConveyanceId = res;
                conveyance.InsertConveyanceDetails();
                return res;
            }
            else if (islogin == 2)
            {
                List<ConveyanceBAL> lst = new List<ConveyanceBAL>();
                conveyance.ConveyanceId = conveyanceId;
                dt = conveyance.InsertConveyanceLocation();
                if (dt.Rows.Count > 1)
                {
                    string lat1 = dt.Rows[0]["Latitude"].ToString();
                    string long1 = dt.Rows[0]["Longitude"].ToString();
                    string lat2 = dt.Rows[1]["Latitude"].ToString();
                    string long2 = dt.Rows[1]["Longitude"].ToString();
                    DateTime LogDateTime1 = Convert.ToDateTime(dt.Rows[0]["LogDateTime"]);
                    DateTime LogDateTime2 = Convert.ToDateTime(dt.Rows[1]["LogDateTime"]);
                    TimeSpan TsMin = (LogDateTime1 - LogDateTime2);
                    int min = (int)TsMin.TotalMinutes;
                    if (Convert.ToDouble(lat1) != 0 && Convert.ToDouble(long1) != 0 && Convert.ToDouble(lat2) != 0 && Convert.ToDouble(long2) != 0)
                    {

                        double distance = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(lat1), Convert.ToDouble(long1), Convert.ToDouble(lat2), Convert.ToDouble(long2));

                        if (distance < (min * 2000))
                        {
                            conveyance.Distance = (distance / 1000).ToString();
                            int res = conveyance.UpdateDistanceInTblConveyance();
                        }
                    }
                }
                return conveyanceId;
            }
            else
            {
                conveyance.ConveyanceId = conveyanceId;
                dt = conveyance.InsertConveyanceLocation();
                if (dt.Rows.Count > 0)
                {
                    string lat1 = dt.Rows[0]["Latitude"].ToString();
                    string long1 = dt.Rows[0]["Longitude"].ToString();
                    string lat2 = dt.Rows[1]["Latitude"].ToString();
                    string long2 = dt.Rows[1]["Longitude"].ToString();
                    DateTime LogDateTime1 = Convert.ToDateTime(dt.Rows[0]["LogDateTime"]);
                    DateTime LogDateTime2 = Convert.ToDateTime(dt.Rows[1]["LogDateTime"]);
                    TimeSpan TsMin = (LogDateTime1 - LogDateTime2);
                    int min = (int)TsMin.TotalMinutes;
                    if (Convert.ToDouble(lat1) != 0 && Convert.ToDouble(long1) != 0 && Convert.ToDouble(lat2) != 0 && Convert.ToDouble(long2) != 0)
                    {
                        double distance = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(lat1), Convert.ToDouble(long1), Convert.ToDouble(lat2), Convert.ToDouble(long2));

                        if (distance < (min * 2000))
                        {
                            conveyance.Distance = (distance / 1000).ToString();
                        }
                    }

                }
                conveyance.ToLogDateTime = logdatetime;
                int res = conveyance.UpdateToLocationIntblConveyance();
                conveyance.UpdateConveyanceDetails();
                if (ClientId == 244)
                {
                    try
                    {
                        Image image = Image.FromFile(HttpContext.Current.Server.MapPath("~/image/Field-management.png"));
                        using (MemoryStream stream = new MemoryStream())
                        {
                            // Save image to stream.
                            image.Save(stream, ImageFormat.Jpeg);
                            Stream RoutImage = stream;
                            conveyance.EngglocTracting(conveyanceId, RoutImage);
                        }
                    }
                    catch(Exception ex)
                    { }
                }
                return conveyanceId;
            }

        }
        [ActionName("InsertCustomerDetails")]
        public int Post([FromBody]CustomerBAL value)
        {
            int cid;
            try
            {
                dt = getDeviceDtlByAppId(value.appId);
                if (dt.Rows.Count > 0)
                {
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                return 0;
            }
            if (UserId > 0)
            {
                value.ClientId = ClientId;
                value.UserId = UserId;
                cid = value.InsertCustomerDetails();
                return cid;
            }
            else
            {
                return 0;
            }
        }
    }
    public class ConveyanceRemarkController : APIBase
    {
        ConveyanceBAL conveyance;
        int ClientId = 0, UserId = 0;
        DataTable dt;
        [ActionName("InsertRemarkIntoConveyance")]
        public int Post([FromBody]ConveyanceBAL value)
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
            {

            }
            if (ClientId > 0)
            {
                conveyance = new ConveyanceBAL();
                conveyance.Remark = value.Remark;
                conveyance.ImagePath = value.ImagePath;
                conveyance.ConveyanceId = value.ConveyanceId;
                conveyance.UserId = UserId;
                return conveyance.UpdateRemarkInConveyance();
            }
            else
            {
                return 0;
            }
        }
    }
}
