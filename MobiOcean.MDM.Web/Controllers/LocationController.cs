using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using System.Xml;

namespace MobiOcean.MDM.Web.Controller
{
    public class LocationController : APIBase
    {
        DataTable dt;
        UserBAL userBal;
        SendSMSBAL sms;
       
        int UserId = 0, ClientId = 0, DeviceId = 0;

        [ActionName("DeviceLoc")]
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
                try
                {
                    value.DeviceId = DeviceId;
                    value.UserId = UserId;
                    value.ClientId = ClientId;
                    value.LogDateTime = getDateInSQlServerFormat(value.LogDateTime);

                    LocationModel locationModel = value.GetLocation(value.Latitude, value.Longitude, value.CellId.ToString(), value.locationAreaCode.ToString(), value.mobileCountryCode.ToString(), value.mobileNetworkCode.ToString(), ClientId, Constant.tblDeviceLocation_tblDeviceLocationGeoFence, UserId);
                    value.Latitude = locationModel.latitude.ToString();
                    value.Longitude = locationModel.longitude.ToString();
                    value.Location = locationModel.location;
                    value.Status = 0;
                    value.IsLatLongFromCellId = locationModel.isLatLongFromCellId;
                    try
                    {
                        if (locationModel.latitude == 0 || locationModel.longitude == 0)
                        {
                            value.Status = 1; //latitude and longitude are zero, then make status=1
                        }
                        else
                        {
                            dt = value.GetLastLocationFromDeviceLocation(DeviceId);
                            if (dt.Rows.Count > 0 && Convert.ToDouble(dt.Rows[0]["Latitude"].ToString()) != 0 && Convert.ToDouble(dt.Rows[0]["Longitude"].ToString()) != 0)
                            {
                                try
                                {
                                    double distance = value.getDistanceFromLatLonInMtr(locationModel.latitude, locationModel.longitude, Convert.ToDouble(dt.Rows[0]["Latitude"].ToString()), Convert.ToDouble(dt.Rows[0]["Longitude"].ToString()));
                                    DateTime LogDateTime1 = Convert.ToDateTime(value.LogDateTime);
                                    DateTime LogDateTime2 = Convert.ToDateTime(dt.Rows[0]["LogDateTime"]);
                                    TimeSpan TsMin = (LogDateTime1 - LogDateTime2);
                                    int min = (int)TsMin.TotalMinutes;
                                    if (distance < (min * 2000))
                                    {
                                        value.Status = 0;
                                    }
                                    else
                                    {
                                        value.Status = 1;
                                    }
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                    catch (Exception)
                    { }
                    value.Status = value.LocReq == 10 ? 0 : value.Status;
                    int IsWithInGeoFence = 9;
                    if (ChkIFFeatureIsEnableAccordingDate(UserId, value.LogDateTime, 38) == 1) // Geo-Fence FeatureId
                    {
                        string geoFenceRes = value.ChkGeoFenceByRouteAndArea();
                        IsWithInGeoFence = Convert.ToInt32(geoFenceRes.Split('~')[0]);
                        int sendAlert = Convert.ToInt32(geoFenceRes.Split('~')[1]);
                        if (sendAlert == 1)
                        {
                            SendAlertOfGeofence(IsWithInGeoFence, locationModel.location, value.LogDateTime);
                        }
                    }
                    if (ChkIFFeatureIsEnableAccordingDate(UserId, value.LogDateTime, 60) == 1 && value.CheckLocationAndSendAlert() == 1) // Location Alert 
                    {
                        SendAlertOfGeofence(6, locationModel.location, value.LogDateTime);   //                    
                    }
                    int Result = Convert.ToInt32(value.TraceDeviceLocationAccordingTofreq(IsWithInGeoFence));

                    if (value.LocReq == 10 && ChkIFFeatureIsEnableAccordingDate(UserId, value.LogDateTime, 25) == 1)
                    {
                        dt = new DataTable();
                        userBal = new UserBAL();
                        userBal.UserId = UserId;
                        dt = userBal.GetUserDtlByUserId();
                        string location = locationModel.location;
                        //string UserName = dt.Rows[0]["UserName"].ToString();
                        if (string.IsNullOrEmpty(location) || location == Constant.LocationNotFound)
                        {
                            location = "https://maps.google.com/?q=" + locationModel.latitude + "," + locationModel.longitude;
                        }
                        else
                            location = location + " (" + "https://maps.google.com/?q=" + locationModel.latitude + "," + locationModel.longitude + ")";
                        string text = dt.Rows[0]["UserName"].ToString() + " might be into trouble at this time " + value.LogDateTime + " at " + location + ".";

                        SendSOSMessage(text);//locationModel.location, value.LogDateTime, locationModel.latitude, locationModel.longitude
                        SendGCMMessage(text);//locationModel.location, value.LogDateTime
                    }

                    return Result.ToString();
                }
                catch (Exception)
                {
                    return "0";
                }

            }
            else
            {
                return "-2";
            }
        }
        private void SendSOSMessage(string text)//string location, string logDateTime, double lat, double lng
        {
            
            try
            {
                //dt = new DataTable();
                userBal = new UserBAL();
                //userBal.UserId = UserId;
                //dt = userBal.GetUserDtlByUserId();
                ////Alert
                //string UserName = dt.Rows[0]["UserName"].ToString();
                //if (string.IsNullOrEmpty(location) || location == Constant.LocationNotFound)
                //{
                //    location = "http://maps.google.com/?q=" + lat + "," + lng;
                //}
                //else
                //    location = location + " (" + "http://maps.google.com/?q=" + lat + "," + lng+")";
                //text = dt.Rows[0]["UserName"].ToString() + " might be into trouble at this time " + logDateTime + " at " + location + "";
                IsAlertEnable(4, text, UserId, ClientId);
                dt = new DataTable();
                userBal.ClientId = ClientId;
                userBal.RoleId = 2;
                dt = userBal.GetUserByRoleId();

                sms = new SendSMSBAL();
                string text1 = "";
                foreach (DataRow row in dt.Rows)
                {
                    text1 = "Dear " + row["UserName"].ToString() + ", " + text;// + " might be into trouble at location " + location + " at this time " + logDateTime + ".";// http://maps.google.com?q=" + Latitude + "," + Longitude + "";
                    sms.sendMsgUsingSMS(text1, row["MobileNo"].ToString(), ClientId,1);
                }
                dt = new DataTable();
                LocationBAL locationBal = new LocationBAL();
                locationBal.UserId = UserId;
                dt = locationBal.GetContactDtlByUsrId();
                foreach (DataRow row in dt.Rows)
                {
                    text1 = "Dear " + row["Name"].ToString() + ", " + text;// + " might be into trouble at location " + location + " at this time " + logDateTime + ".";// http://maps.google.com?q=" + Latitude + "," + Longitude + "";
                    sms.sendMsgUsingSMS(text1, row["ContactNo"].ToString(), ClientId,1);
                }
            }
            catch (Exception)
            {
            }
        }
        private void SendGCMMessage(string text)//string location, string logDateTime, double lat, double lng
        {
            try
            {
                userBal = new UserBAL();                
                DataTable dt1 = new DataTable();
                userBal.ClientId = ClientId;
                dt1 = userBal.GetUserByClientId();
                sms = new SendSMSBAL();
                string text1 = "";
                foreach (DataRow row in dt1.Rows)
                {
                    text1 = "GBox set as WP14 @Dear " + row["UserName"].ToString() + ", " + text;// + " might be into trouble at location '" + location + "' at this time '" + logDateTime + "'.";
                    sms.sendFinalSMS(row["MobileNo"].ToString(), text1, ClientId);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void SendAlertOfGeofence(int IsWithInGeoFence, string location, string logDateTime)
        {
            try
            {
                string reportingMngrEmail, reportingMngrName, reprotngMngrMob;
                sms = new SendSMSBAL();
                userBal = new UserBAL();
                userBal.UserId = UserId;
                DataTable myRptStu = userBal.GetRptngManagerByUserId();
                DataTable usrdtl = userBal.GetUserDtlByUserId();
                string geoFenceStatus = " is out of geofence.";
                if (IsWithInGeoFence == 6)
                {
                    geoFenceStatus = " is within geofence.";
                }
                string myMsg = "your employee " + usrdtl.Rows[0]["UserName"].ToString() + geoFenceStatus + " His/her current location is " + location + " at " + logDateTime;
                if (myRptStu.Rows.Count > 0)
                {
                    reportingMngrEmail = myRptStu.Rows[0]["EmailId"].ToString();
                    reportingMngrName = myRptStu.Rows[0]["UserName"].ToString();
                    reprotngMngrMob = myRptStu.Rows[0]["MobileNo"].ToString();

                    //--- Send notification to repoting manager -----
                    myMsg = "Dear " + reportingMngrName + " " + myMsg;
                    sms.sendMsgUsingSMS(myMsg, reprotngMngrMob, ClientId,1);
                }
                IsAlertEnable(3, myMsg, UserId, ClientId);
            }
            catch (Exception) { }
        }
        [HttpGet]
        public string UpdateLocationVodafone()
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                BasicHttpBinding mybinding = new BasicHttpBinding();
                mybinding.Security.Mode = BasicHttpSecurityMode.Transport;
                EndpointAddress myendpoint = new EndpointAddress("https://locationtracker.vodafone.in/VLTAPI_BULK/MSISDN/");
                ChannelFactory<IRequestChannel> factory = new ChannelFactory<IRequestChannel>(mybinding, myendpoint);
                IRequestChannel channel1 = factory.CreateChannel();
                string file = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/Clients.xml"));
                DeviceInfoBAL device = new DeviceInfoBAL();
                DataTable vodnos = device.VodafoneNos(Constant.VodaFoneService);
                string XmlVodaNoStr = string.Empty;
                for (int i = 0; i < vodnos.Rows.Count; i++)
                {
                    XmlVodaNoStr += "<loc:addresses>" + vodnos.Rows[i]["MobileNo"] + "</loc:addresses>";
                }
                file = file.Replace("<loc:addresses></loc:addresses>", XmlVodaNoStr);
                XmlReader envelopeReader = XmlReader.Create(new StringReader(file));
                MessageVersion msg = MessageVersion.CreateVersion(EnvelopeVersion.Soap11, AddressingVersion.None);
                Message requestMsg = Message.CreateMessage(envelopeReader, int.MaxValue, msg);
                Message m = channel1.Request(requestMsg, new TimeSpan(0, 0, 840));
                XmlReader xReader = m.GetReaderAtBodyContents();

                DataSet ds = new DataSet();
                ds.ReadXml(xReader);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables["result"];
                    DataTable dt1 = ds.Tables["currentLocation"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["reportStatus"].ToString() == "Retrieved")
                        {
                            UserDeviceBAL userDeviceBal = new UserDeviceBAL();
                            userDeviceBal.MobileNo1 = dt.Rows[i]["address"].ToString();
                            DataTable deviceDetails = userDeviceBal.GetDeviceByMobileNo();
                            string Location = Constant.LocationNotFound;
                            LocationBAL loc = new LocationBAL();
                            if (dt1.Rows[i]["latitude"].ToString() == "0.0" && dt1.Rows[i]["longitude"].ToString() == "0.0")
                            {
                                Location = Constant.LocationNotFound;
                                loc.Status = 1;
                            }
                            else
                            {

                                LocationModel lom = loc.GetLocation(dt1.Rows[i]["latitude"].ToString(), dt1.Rows[i]["longitude"].ToString(), "0", "0", "0", "0", Convert.ToInt32(deviceDetails.Rows[0]["ClientId"].ToString()), Constant.tblDeviceLocation_tblDeviceLocationGeoFence, Convert.ToInt32(deviceDetails.Rows[0]["UserId"].ToString()));
                                Location = lom.location;
                                loc.Status = 0;
                            }
                            loc.IsLatLongFromCellId = 0;
                            loc.DeviceId = Convert.ToInt32(deviceDetails.Rows[0]["DeviceId"].ToString());
                            loc.MobileNo = dt.Rows[i]["address"].ToString();
                            loc.Longitude = dt1.Rows[i]["longitude"].ToString();
                            loc.Latitude = dt1.Rows[i]["latitude"].ToString();
                            loc.Location = Location;
                            loc.LogDateTime = DateTime.UtcNow.AddMinutes(330).ToString("dd-MMM-yyyy HH:mm");
                            loc.LocSource = "Vodafone";
                            loc.SrvcCalledBy = "Scheduler";
                            loc.LocReq = 0;
                            int Result = Convert.ToInt32(loc.TraceDeviceLocationAccordingTofreq(6));//IsWithInGeoFence
                                                                                                    //  device.UpdateDeviceLocation(Convert.ToInt32(deviceDetails.Rows[0]["ClientId"].ToString()), Convert.ToInt32(deviceDetails.Rows[0]["UserId"].ToString()), dt.Rows[i]["address"].ToString(), dt1.Rows[i]["latitude"].ToString(), dt1.Rows[i]["longitude"].ToString(), "Vodafone", deviceDetails.Rows[0]["APPId"].ToString(), Location);
                        }
                        else
                            return "Not Retrived";
                    }
                }
                else
                    return "No data";
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
