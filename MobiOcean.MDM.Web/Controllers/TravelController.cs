using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace MobiOcean.MDM.Web.Controller
{
    public class TravelController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;
        LocationBAL gapi;

        [ActionName("ExtraAllowance")]
        public int Post([FromBody]TABAL tabal)
        {

            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(tabal.appId);
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
            if (DeviceId > 0 && tabal.ClaimedAmt != 0)
            {

                try
                {
                    tabal.UserId = UserId;
                    tabal.clientId = ClientId;
                    tabal.LogDate = Convert.ToDateTime(tabal.LogDateTime).ToString(Constant.DateFormat);
                    return tabal.InsertExtraTADetail();
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        [ActionName("VisitInfo")]
        public int Post([FromBody]TAVisitBAL value)
        {
            try
            {
                dt = new DataTable();
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
            if (UserId > 0)
            {
                //string latitude = "0", longitude = "0";
                gapi = new LocationBAL();
                LocationModel locationModel = gapi.GetLocation(value.Latitude, value.Longitude, value.CellId.ToString(), value.LAC.ToString(), value.MCC.ToString(), value.MNC.ToString(), ClientId, Constant.tblConveyanceDetails, UserId, false);
                //if (!string.IsNullOrEmpty(value.Latitude) && !string.IsNullOrEmpty(value.Longitude) && value.Latitude != "0" && value.Longitude != "0" && value.Latitude != "0.0" && value.Longitude != "0.0")
                //{
                //    latitude = value.Latitude;
                //    longitude = value.Longitude;
                //    //location = gapi.getGoogleLocationByLogLat(value.Longitude, value.Latitude);
                //}
                //else if (value.CellId != 0 && value.MCC != 0 && value.MNC != 0 && value.LAC != 0)
                //{
                //    string[] LatLong = gapi.CellId_To_Lat(value.CellId, value.LAC, value.MCC.ToString(), value.MNC.ToString(), ClientId, Constant.tblConveyanceDetails, Constant.CellToLatLong, UserId).Split(',');
                //    if (LatLong.Count() == 2)
                //    {
                //        latitude = LatLong[0].ToString();
                //        longitude = LatLong[1].ToString();
                //        //location = gapi.getGoogleLocationByLogLat(longitude, latitude);
                //    }
                //}
                dt = new DataTable();
                dt = value.GetCustomerFromVisitId();
                if (dt.Rows.Count > 0)
                {
                    double distance = 0;
                    try
                    {
                        distance = gapi.getDistanceFromLatLonInMtr(locationModel.latitude, locationModel.longitude, Convert.ToDouble(dt.Rows[0]["Latitude"].ToString()), Convert.ToDouble(dt.Rows[0]["Longitude"].ToString()));
                    }
                    catch (Exception)
                    {
                        distance = 0;
                    }
                    if (distance < 1000)
                    {
                        value.IsVisited = 1;
                    }
                    else
                    {
                        value.IsVisited = 0;
                    }
                }
                return value.UpdateVisitInfo();
            }
            else
            {
                return 0;
            }
        }

        [ActionName("Mode")]
        public List<Mode> Get(string appId)
        {
            Mode list;
            ConveyanceBAL conveyance;
            List<Mode> lstmode = new List<Mode>();
            try
            {
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
                conveyance = new ConveyanceBAL();
                conveyance.ClientId = ClientId;
                dt = conveyance.GetModeOfTravelDtls();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow obj in dt.Rows)
                    {
                        list = new Mode()
                        {
                            ModeId = Convert.ToInt32(obj["ModeId"].ToString()),
                            ModeOfTravel = obj["ModeOfTravel"].ToString()
                        };
                        lstmode.Add(list);
                    }
                }
            }
            return lstmode;
        }

        [ActionName("UpdateCustomer")]
        public int Post([FromBody]CustomerBAL value)
        {
            try
            {
                dt = getDeviceDtlByAppId(value.appId);
                if (dt.Rows.Count > 0)
                {
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                }
            }
            catch (Exception)
            {

            }
            if (UserId > 0)
            {
                value.UserId = UserId;
                value.CustomerTempId = 0;
                value.IsVisited = 0;
                return value.UpdateCustomerInTAMaster();
            }
            return 0;
        }
    }
    public class VisitController : APIBase
    {

        int ClientId = 0, UserId = 0;
        DataTable dt, dt1;
        CustomerBAL cust, custlst;
        LocationBAL gapi;
        DataTable dt2;
        LTCollegeVisitBAL ltBal;
        UserBAL user;
        SendSMSBAL sms;
        SendMailBAL mail;
        [ActionName("CustomerList")]
        public List<CustomerBAL> Get(string appId, int isTodayList = 0)
        {
            List<CustomerBAL> lst = new List<CustomerBAL>();
            int userid = 0;
            try
            {
                dt = getDeviceDtlByAppId(appId);
                if (dt.Rows.Count > 0)
                {
                    userid = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                }
                else
                {
                    userid = 0;
                }
            }
            catch (Exception)
            {
                userid = 0;
            }
            if (userid > 0)
            {
                cust = new CustomerBAL();
                cust.UserId = userid;
                cust.isTodayList = isTodayList;
                dt = cust.GetCustomerDetailsBasedOnIsTodayList();
                //dt = cust.GetAssignedCustomer();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            custlst = new CustomerBAL()
                            {
                                CustomerId = Convert.ToInt32(row["CustomerId"].ToString()),
                                UserId = userid,
                                ClientId = Convert.ToInt32(row["ClientId"].ToString()),
                                CustomerName = row["Name"].ToString(),
                                MobileNo = row["MobileNo"].ToString(),
                                ALtMobileNo = row["ALtMobileNo"].ToString(),
                                ContactPersion = row["ContactPersion"].ToString(),
                                AltContactPersion = row["AltContactPersion"].ToString(),
                                EmailId = row["EmailId"].ToString(),
                                AltEmailId = row["AltEmailId"].ToString(),
                                Address = row["Address"].ToString(),
                                AltAddress = row["AltAddress"].ToString(),
                                Latitude = row["Latitude"].ToString(),
                                Longitude = row["Longitude"].ToString(),
                                City = row["City"].ToString(),
                                District = row["District"].ToString(),
                                state = row["state"].ToString(),
                                country = row["country"].ToString(),
                                PinCode = row["PinCode"].ToString(),
                                TinNumber = row["TinNumber"].ToString(),
                                CreatedBy = Convert.ToInt32(row["CreatedBy"].ToString()),
                                Date = row["Date"].ToString(),
                                Time = row["Time"].ToString(),
                                AssignId = Convert.ToInt32(row["AssignId"].ToString()),
                                IsApproved = string.IsNullOrWhiteSpace(row["Approval"].ToString()) ? 2 : Convert.ToInt32(row["Approval"].ToString()), //2--Nothing happened
                                TaskDetail = row["TaskDetail"].ToString()
                            };
                            lst.Add(custlst);
                        }
                        catch (Exception)
                        { }
                    }
                }
            }
            return lst;
        }
        [ActionName("IsDailyCutomerAcceptRemark")]
        public int Post([FromBody]CustomerBAL value)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(value.appId);
                if (dt.Rows.Count > 0)
                {
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                }
            }
            catch (Exception)
            { }
            if (UserId > 0)
            {
                value.UserId = UserId;
                value.AssignDailyCutomerUpdate();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        [ActionName("Detail")]
        public int Post([FromBody]TAVisitBAL value)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(value.appId);
                if (dt.Rows.Count > 0)
                {
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                }
            }
            catch (Exception)
            { }
            if (UserId > 0)
            {
                //string location = "Location not found", latitude = "0", longitude = "0";
                gapi = new LocationBAL();
                LocationModel locationModel = gapi.GetLocation(value.Latitude, value.Longitude, value.CellId.ToString(), value.LAC.ToString(), value.MCC.ToString(), value.MNC.ToString(), ClientId, Constant.TA_VisitDetail, UserId);
                //if (!string.IsNullOrEmpty(value.Latitude) && !string.IsNullOrEmpty(value.Longitude) && value.Latitude != "0" && value.Longitude != "0" && value.Latitude != "0.0" && value.Longitude != "0.0")
                //{
                //    latitude = value.Latitude;
                //    longitude = value.Longitude;
                //    location = gapi.getGoogleLocationByLogLatFromApi(value.Longitude, value.Latitude, ClientId, Constant.TA_VisitDetail, Constant.GeoFence, UserId);
                //}
                //else if (value.CellId != 0 && value.MCC != 0 && value.MNC != 0 && value.LAC != 0)
                //{
                //    string[] LatLong = gapi.CellId_To_Lat(value.CellId, value.LAC, value.MCC.ToString(), value.MNC.ToString(), ClientId, Constant.TA_VisitDetail, Constant.CellToLatLong, UserId).Split(',');
                //    if (LatLong.Count() == 2)
                //    {
                //        latitude = LatLong[0].ToString();
                //        longitude = LatLong[1].ToString();
                //        location = gapi.getGoogleLocationByLogLatFromApi(longitude, latitude, ClientId, Constant.TA_VisitDetail, Constant.GeoFence, UserId);
                //    }
                //}
                return InsertConveyance(locationModel.latitude.ToString(), locationModel.longitude.ToString(), locationModel.location, value.LogDateTime, value.IsLogin, value.CustomerId, value.ModeOfTravel, value.Accuracy, value.Altitude, value.Bearing, value.ElapsedRealtimeNanos, value.Provider, value.Speed, value.Time, value.visitId, value.tempCustomerId);
            }
            else
            {
                return 0;
            }
        }
        private int InsertConveyance(string latitude, string longitude, string location, string logdatetime, int islogin, int customerId, int modeOfTravel, string Accuracy, string Altitude, string Bearing, string ElapsedRealtimeNanos, string Provider, string Speed, string Time, int visitId = 0, int tempCustomerId = 0, int AutocustomerId = 0, int AutotempCustomerId = 0)
        {
            TAVisitBAL TAvisit = new TAVisitBAL();
            gapi = new LocationBAL();
            TAvisit.IsLogin = islogin;
            TAvisit.ClientId = ClientId;
            TAvisit.UserId = UserId;
            TAvisit.location = location;
            TAvisit.LogDateTime = getDateInSQlServerFormat(logdatetime);
            TAvisit.Latitude = latitude;
            TAvisit.Longitude = longitude;
            //TAvisit.visitId = visitId;
            TAvisit.LogDate = Convert.ToDateTime(logdatetime).ToString(Constant.DateFormat);
            TAvisit.CustomerId = customerId;
            TAvisit.tempCustomerId = tempCustomerId;
            TAvisit.ModeOfTravel = modeOfTravel;
            TAvisit.Accuracy = Accuracy;
            TAvisit.Altitude = Altitude;
            TAvisit.Bearing = Bearing;
            TAvisit.ElapsedRealtimeNanos = ElapsedRealtimeNanos;
            TAvisit.Provider = Provider;
            TAvisit.Speed = Speed;
            TAvisit.Time = Time;
            if (islogin == 1)
            {
                TAvisit.visitId = 0;
                visitId = TAvisit.InsertVisitTADetail();
            }
            try
            {
                TAvisit.visitId = visitId;
                dt = TAvisit.InsertVisitTALocation();
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
                            TAvisit.Distance = Math.Round((distance / 1000), 2);
                            TAvisit.UpdateCoveredDistance();
                        }
                    }
                    //if (Convert.ToDouble(lat1) != 0 && Convert.ToDouble(long1) != 0 && Convert.ToDouble(lat2) != 0 && Convert.ToDouble(long2) != 0)
                    //{
                    //    double distance = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(lat1), Convert.ToDouble(long1), Convert.ToDouble(lat2), Convert.ToDouble(long2));
                    //    TAvisit.Distance = Math.Round((distance / 1000), 2);
                    //    TAvisit.UpdateCoveredDistance();
                    //}
                }
            }
            catch (Exception)
            { }
            if (islogin == 2)
            {
                if (ClientId == 266 || ClientId == 208)// RSM Client
                {
                    TAVisitBAL tavisibal = new TAVisitBAL();
                    tavisibal.visitId = visitId;
                    DataTable dtvisiDetail = tavisibal.GetVisitDetailByVisitId();
                    if (dtvisiDetail != null && dtvisiDetail.Rows.Count > 0)
                    {
                        modeOfTravel = Convert.ToInt32(dtvisiDetail.Rows[0]["ModeOfTravelId"].ToString());
                        int newvisitId = RSM(latitude, longitude, location, logdatetime, islogin, customerId, modeOfTravel, Accuracy, Altitude, Bearing, ElapsedRealtimeNanos, Provider, Speed, Time, visitId, tempCustomerId);
                        if (newvisitId != 0)
                        {
                            visitId = newvisitId;
                        }
                    }
                }
            }
            if (islogin == 3)
            {
                TAvisit.ClientId = ClientId;
                TAvisit.UserId = UserId;
                TAvisit.LogDate = Convert.ToDateTime(logdatetime).ToString(Constant.DateFormat);
                TAvisit.LogDateTime = logdatetime;
                int res = TAvisit.UpdateVisitEndDetail();
            }
            return visitId;
        }
        private int RSM(string latitude, string longitude, string location, string logdatetime, int islogin, int customerId, int modeOfTravel, string Accuracy, string Altitude, string Bearing, string ElapsedRealtimeNanos, string Provider, string Speed, string Time, int visitId = 0, int tempCustomerId = 0)
        {
            try
            {
                double distance = 0;
                customerId = 0;
                gapi = new LocationBAL();
                TABAL tabal = new TABAL();
                tabal.UserId = UserId;
                tabal.LogDate = Convert.ToDateTime(logdatetime).ToString(Constant.DateFormat);
                //DataTable dtcust = tabal.CheckLastVisitCustomer();
                DataTable dtcust = tabal.CheckLastVisitCustomerRaj();
                #region--- Check Customer ---
                cust = new CustomerBAL();
                cust.ClientId = ClientId;
                DataTable dt1 = cust.CustomerDetails();
                foreach (DataRow obj in dt1.Rows)
                {
                    distance = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(obj["Latitude"].ToString()), Convert.ToDouble(obj["Longitude"].ToString()), Convert.ToDouble(latitude), Convert.ToDouble(longitude));
                    if (distance < 250)
                    {
                        customerId = Convert.ToInt32(obj["CustomerId"].ToString());

                        if (dtcust != null && dtcust.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtcust.Rows)
                            {
                                if (row["AutoCustomerId"].ToString() != "0" && customerId == Convert.ToInt32(row["AutoCustomerId"].ToString()))
                                {
                                    customerId = 0;
                                    break;
                                }
                            }

                        }
                        if (customerId != 0)
                        {
                            break;
                        }

                    }
                }
                #endregion

                #region--- Check Temp Customer ---
                if (customerId == 0)
                {
                    DataTable dt2 = cust.TempCustomerDetailsbyClientId();
                    foreach (DataRow obj in dt2.Rows)
                    {
                        distance = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(obj["Latitude"].ToString()), Convert.ToDouble(obj["Longitude"].ToString()), Convert.ToDouble(latitude), Convert.ToDouble(longitude));
                        if (distance < 250)
                        {
                            tempCustomerId = Convert.ToInt32(obj["CustomerTempId"].ToString());

                            if (dtcust != null && dtcust.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtcust.Rows)
                                {
                                    if (row["AutoTempCustomerId"].ToString() != "0" && tempCustomerId == Convert.ToInt32(row["AutoTempCustomerId"].ToString()))
                                    {
                                        tempCustomerId = 0;
                                        break;
                                    }
                                }

                            }
                            if (tempCustomerId != 0)
                            {
                                break;
                            }

                        }
                    }
                }
                #endregion


                if (customerId != 0 || tempCustomerId != 0)
                {
                    CustomerBAL bal = new CustomerBAL();
                    bal.UserId = UserId;
                    //bal.CustomerId = customerId;
                    // bal.CustomerTempId = tempCustomerId;
                    bal.VisitId = visitId;
                    bal.IsVisited = 1;
                    bal.AutoCustomerId = customerId;
                    bal.AutotempCustomerId = tempCustomerId;
                    //bal.UpdateCustomerInTAMaster();
                    bal.UpdateCustomerInTAMasterRaj();
                    InsertConveyance(latitude, longitude, location, logdatetime, 3, customerId, modeOfTravel, Accuracy, Altitude, Bearing, ElapsedRealtimeNanos, Provider, Speed, Time, visitId, tempCustomerId);
                    visitId = InsertConveyance(latitude, longitude, location, logdatetime, 1, 0, modeOfTravel, Accuracy, Altitude, Bearing, ElapsedRealtimeNanos, Provider, Speed, Time, 0, 0);
                }
                return visitId;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        [ActionName("TravelCaluculation")]
        public int Post(int Id)
        {
            try
            {
                TAVisitBAL TAvisit = new TAVisitBAL();
                gapi = new LocationBAL();
                TAvisit.MasterId = Id;
                dt = new DataTable();
                dt1 = new DataTable();

                dt = TAvisit.GetVisitDetailByMasterId();
                double Masterdistance = 0.0;
                double MasterClaimedAmount = 0.0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TAvisit.visitId = Convert.ToInt32(dt.Rows[i]["VisitDetailId"].ToString());
                    dt1 = TAvisit.GetVisitLocationDetailsbyVisitId();
                    double Visteddistance = 0.0;
                    double VistedClaimedAmount = 0.0;
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if (j > 0)
                        {
                            string lat1 = dt1.Rows[j]["Latitude"].ToString();
                            string long1 = dt1.Rows[j]["Longitude"].ToString();
                            string lat2 = dt1.Rows[j - 1]["Latitude"].ToString();
                            string long2 = dt1.Rows[j - 1]["Longitude"].ToString();
                            DateTime LogDateTime1 = Convert.ToDateTime(dt1.Rows[j]["LogDateTime"]);
                            DateTime LogDateTime2 = Convert.ToDateTime(dt1.Rows[j - 1]["LogDateTime"]);
                            TimeSpan TsMin = (LogDateTime1 - LogDateTime2);
                            int min = (int)TsMin.TotalMinutes;
                            if (Convert.ToDouble(lat1) != 0 && Convert.ToDouble(long1) != 0 && Convert.ToDouble(lat2) != 0 && Convert.ToDouble(long2) != 0)
                            {
                                double distancej = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(lat1), Convert.ToDouble(long1), Convert.ToDouble(lat2), Convert.ToDouble(long2));

                                if (distancej < (min * 2000))
                                {
                                    Visteddistance += Math.Round((distancej / 1000), 2);
                                }
                            }
                        }
                    }
                    VistedClaimedAmount = Visteddistance * (Convert.ToInt32(dt.Rows[i]["RatePerKM"].ToString()));
                    TAvisit.Distance = Visteddistance;
                    TAvisit.ClaimedAmount = VistedClaimedAmount;
                    TAvisit.UpdateVisitDetailDistanceByVisitId();
                    MasterClaimedAmount += VistedClaimedAmount;
                    Masterdistance += Visteddistance;
                }
                TAvisit.MasterId = Id;
                TAvisit.Distance = Masterdistance;
                TAvisit.ClaimedAmount = MasterClaimedAmount;
                TAvisit.UpdateMasterDetailDistanceByMasterId();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        [ActionName("MasterTravelcalculation")]
        public int Get(int MasterId)
        {
            try
            {
                TAVisitBAL TAvisit = new TAVisitBAL();
                TAvisit.MasterId = MasterId;
                dt = new DataTable();
                dt1 = new DataTable();
                gapi = new LocationBAL();
                dt = TAvisit.GetVisitDetailByMasterId();
                double Masterdistance = 0.0;
                double MasterClaimedAmount = 0.0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TAvisit.visitId = Convert.ToInt32(dt.Rows[i]["VisitDetailId"].ToString());
                    dt1 = TAvisit.GetVisitLocationDetailsbyVisitId();
                    double Visteddistance = 0.0;
                    double VistedClaimedAmount = 0.0;
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if (j > 0)
                        {
                            string lat1 = dt1.Rows[j]["Latitude"].ToString();
                            string long1 = dt1.Rows[j]["Longitude"].ToString();
                            string lat2 = dt1.Rows[j - 1]["Latitude"].ToString();
                            string long2 = dt1.Rows[j - 1]["Longitude"].ToString();
                            DateTime LogDateTime1 = Convert.ToDateTime(dt1.Rows[j]["LogDateTime"]);
                            DateTime LogDateTime2 = Convert.ToDateTime(dt1.Rows[j - 1]["LogDateTime"]);
                            TimeSpan TsMin = (LogDateTime1 - LogDateTime2);
                            int min = (int)TsMin.TotalMinutes;
                            if (Convert.ToDouble(lat1) != 0 && Convert.ToDouble(long1) != 0 && Convert.ToDouble(lat2) != 0 && Convert.ToDouble(long2) != 0)
                            {
                                double distancej = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(lat1), Convert.ToDouble(long1), Convert.ToDouble(lat2), Convert.ToDouble(long2));

                                if (distancej < (min * 2000))
                                {
                                    Visteddistance += Math.Round((distancej / 1000), 2);
                                }
                            }
                        }
                        TAvisit.LocationId = Convert.ToInt32(dt1.Rows[j]["LocationId"]);
                        TAvisit.UpdateLocationDetailByLocationId();
                    }
                    VistedClaimedAmount = Visteddistance * (Convert.ToInt32(dt.Rows[i]["RatePerKM"].ToString()));
                    TAvisit.Distance = Visteddistance;
                    TAvisit.ClaimedAmount = VistedClaimedAmount;
                    TAvisit.UpdateVisitDetailDistanceByVisitId();
                    MasterClaimedAmount += VistedClaimedAmount;
                    Masterdistance += Visteddistance;
                }
                TAvisit.MasterId = MasterId;
                TAvisit.Distance = Masterdistance;
                TAvisit.ClaimedAmount = MasterClaimedAmount;
                TAvisit.UpdateMasterDetailDistanceByMasterId();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        [ActionName("InsertLTCollegeVisitDetails")]
        [HttpPost]
        public int Post([FromBody]LTCollegeVisitBAL value)
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
            if (ClientId > 0)//
            {
                try
                {
                    int res = 0;
                    string towwerLocation = value.CellId + "," + value.LAC + "," + value.MCC + "," + value.MNC;
                    gapi = new LocationBAL();
                    LocationModel locationModel = gapi.GetLocation(value.Latitude, value.Longitude, value.CellId.ToString(), value.LAC.ToString(), value.MCC.ToString(), value.MNC.ToString(), ClientId, Constant.TA_VisitDetail, UserId);
                    if (string.IsNullOrEmpty(value.Location))
                    {
                        //manual=0                        
                        res = InsertLTVisitDetail(locationModel.latitude.ToString(), locationModel.longitude.ToString(), locationModel.location, value.LogDateTime, value.IsOutTime, value.CollegeId, value.Forgot, false, towwerLocation, value.Location);
                    }
                    else
                    {
                        // manual = 1
                        string locationByCellId = "";
                        if (locationModel.location != Constant.LocationNotFound)
                        {
                            value.Latitude = locationModel.latitude.ToString();
                            value.Longitude = locationModel.longitude.ToString();
                            locationByCellId = locationModel.location;
                        }
                        res = InsertLTVisitDetail(value.Latitude.ToString(), value.Longitude.ToString(), value.Location, value.LogDateTime, value.IsOutTime, value.CollegeId, value.Forgot, true, towwerLocation, locationByCellId);
                    }
                    LogExceptions("AuthController --> LTVisit", "Input: " + new JavaScriptSerializer().Serialize(value), "Response : 1" + "", res.ToString());
                    return res;
                }
                catch (Exception ex)
                {
                    LogExceptions("AuthController --> LTVisit", "Input: " + new JavaScriptSerializer().Serialize(value), "Response : 0" + "", ex.Message + "/n" + ex.StackTrace);
                    return 0;
                }
                // return 0;
            }
            else
            {
                LogExceptions("AuthController --> LTVisit", "Input: " + new JavaScriptSerializer().Serialize(value), "Response : ClientId=0 " + "", "Wrong APPId");
                return 0;
            }

        }
        public int InsertLTVisitDetail(string latitude, string longitude, string location, string logdatetime, int isinTime, int collegeId, int forgot, bool isLocationManuallyEntered, string towerlocation, string manualLocation)
        {
            double distance = 0;
            cust = new CustomerBAL();
            dt2 = new DataTable();
            cust.CustomerId = collegeId;
            dt2 = cust.CustomerDetailsbyCustomerid();
            try
            {
                distance = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(latitude), Convert.ToDouble(longitude), Convert.ToDouble(dt2.Rows[0][11]), Convert.ToDouble(dt2.Rows[0][12]));
            }
            catch (Exception)
            {
                distance = 0;
            }
            var d = distance != 0 ? distance < 1000 ? 1 : 0 : 0; // User is in 1KM range.
            string time = "", username = "";
            int dateofjoining = 0;
            ltBal = new LTCollegeVisitBAL();
            ltBal.IsOutTime = isinTime;
            ltBal.ClientId = ClientId;
            ltBal.UserId = UserId;
            ltBal.CollegeId = collegeId;
            ltBal.Forgot = forgot;
            ltBal.Location = location;
            logdatetime = getDateInSQlServerFormat(logdatetime);
            ltBal.Latitude = latitude;
            ltBal.Longitude = longitude;
            ltBal.LogDateTime = logdatetime;
            time = Convert.ToDateTime(logdatetime).ToString("HH:mm");
            ltBal.Time = time;
            ltBal.InVerification = d;
            ltBal.IsLocationManuallyEntered = isLocationManuallyEntered;
            ltBal.distance = distance;
            ltBal.TowerLocation = towerlocation;
            ltBal.ManualLoaction = manualLocation;
            int res = ltBal.InsertLTVisitDetails1();
            //            int res = ltBal.UpdateDailyAssign();
            try
            {
                user = new UserBAL();
                dt1 = new DataTable();
                user.UserId = UserId;
                dt1 = user.GetUserDtlByUserId();
                if (dt1.Rows.Count > 0)
                {
                    username = dt1.Rows[0]["UserName"].ToString();
                    dateofjoining = Convert.ToInt32(dt1.Rows[0]["DateOfJoining"].ToString());

                    if (dateofjoining == 1)
                    {
                        dt = new DataTable();
                        dt = user.GetRptngManagerByUserId();
                        if (dt.Rows.Count > 0)
                        {
                            string reportinguser = "", mblno = "", msg = "", custname = "", emailid = "";
                            reportinguser = dt.Rows[0]["UserName"].ToString();
                            mblno = dt.Rows[0]["MobileNo"].ToString();
                            emailid = dt.Rows[0]["EmailId"].ToString();
                            custname = dt2.Rows[0]["Name"].ToString();

                            if (location.Contains(Constant.LocationNotFound) && latitude != "0" && longitude != "0")
                            {
                                location = "https://www.google.com/maps?q=" + latitude + "," + longitude;
                            }
                            if (isinTime == 0)
                            {
                                msg = "Dear " + reportinguser + "" + System.Environment.NewLine + "" + username + " reached to " + custname + " at " + time + " at " + location + ".";
                            }
                            else
                            {
                                msg = "Dear " + reportinguser + System.Environment.NewLine + "" + username + " left from " + custname + " at " + time + " at " + location + ".";
                            }
                            try
                            {
                                sms = new SendSMSBAL();
                                sms.sendMsgUsingSMS(msg, mblno, ClientId,1);
                                mail = new SendMailBAL();
                                msg = msg + System.Environment.NewLine + "Thanks and regards, " + System.Environment.NewLine + "MobiOcean Team";
                                mail.SendEmail(emailid, "College visit Notification", msg, ClientId);
                            }
                            catch (Exception)
                            { }
                        }
                    }
                }
            }
            catch (Exception) { }
            return res;
        }

        [ActionName("InsertRemarkInLTCollegeVisit")]
        [HttpPost]
        public int Post([FromBody]LTCollegeVisitBAL value, int c = 0)
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
                try
                {

                    ltBal = new LTCollegeVisitBAL();
                    if (value.ImagePath.Length > 0)
                    {
                        foreach (var obj in value.ImagePath)
                        {
                            ltBal.Remark = value.Remark;
                            ltBal.imgpath = obj;
                            ltBal.LTCollegeVisitId = value.LTCollegeVisitId;
                            ltBal.UserId = UserId;
                            ltBal.LogDateTime = value.LogDateTime;
                            ltBal.InsertRemarkInLTCollegeVisitRemark();
                        }
                    }
                    else
                    {
                        ltBal.Remark = value.Remark;
                        ltBal.imgpath = null;
                        ltBal.LTCollegeVisitId = value.LTCollegeVisitId;
                        ltBal.UserId = UserId;
                        ltBal.LogDateTime = value.LogDateTime;
                        ltBal.InsertRemarkInLTCollegeVisitRemark();
                    }

                    return 1;
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
    }
}
