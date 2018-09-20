using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using MobiOcean.MDM.DAL.DAL.LocationDALTableAdapters;
using MobiOcean.MDM.DAL.DAL.ThirdPartyDALTableAdapters;
using MobiOcean.MDM.Infrastructure;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.DAL;

namespace MobiOcean.MDM.BAL.BAL
{
    public class LocationBAL
    {
        tblDeviceLocationTableAdapter DeviceLoc;
        LatLongtoLocationTableAdapter lattoloc;
        tblCellIdtoLatLngTableAdapter CellIdtoLatLngTableAdapter;
        tblGoogleKeyTableAdapter GoogleKeyTableAdapter;
        tblSosContactsTableAdapter soscontactdtl;
        tblRouteGeofenceTableAdapter geofence;
        tblLocReqFreqMgmtByProfileIdTableAdapter locfreqmgmt;
        tblProfileRouteTableAdapter ProfileRoute;
        tblAreaTableAdapter Area;
        tblSetLocationTableAdapter setlocation;

        // UserBAL user;
        // SendSMSBAL sms;
        DataTable dt, dtRoutePoint;
        //APIBAL apibase;        


        int _DeviceId, _ClientId, _UserId, _LocReq, _RouteId, _LocReqFreqId, _LocReqFrequency, _ProfileId, _AreaId, _IsInsert, _Status, _IsLatLongFromCellId;
        string _CellId, _locationAreaCode, _mobileCountryCode, _mobileNetworkCode, _MobileNo, _Longitude, _Latitude, _LogDateTime, _Location;
        string _LocSource, _SrvcCalledBy, _AppId, _RouteName, _RouteCode, _RouteDesc, _LoggedBy, _EmpIdList, _FrmTime, _ToTime;
        string _RouteIdList, _AreaName, _AreaIdList;
        float _Radius;
        private object _LatLong;

        public string FrmTime
        {
            get { return _FrmTime; }
            set { _FrmTime = value; }
        }
        public string ToTime
        {
            get { return _ToTime; }
            set { _ToTime = value; }
        }
        public int LocReqFreqId
        {
            get { return _LocReqFreqId; }
            set { _LocReqFreqId = value; }
        }
        public int LocReqFrequency
        {
            get { return _LocReqFrequency; }
            set { _LocReqFrequency = value; }
        }
        public int ProfileId
        {
            get { return _ProfileId; }
            set { _ProfileId = value; }
        }
        public int RouteId
        {
            get { return _RouteId; }
            set { _RouteId = value; }
        }
        public string RouteName
        {
            get { return _RouteName; }
            set { _RouteName = value; }
        }
        public string RouteCode
        {
            get { return _RouteCode; }
            set { _RouteCode = value; }
        }
        public string RouteDesc
        {
            get { return _RouteDesc; }
            set { _RouteDesc = value; }
        }
        public string LoggedBy
        {
            get { return _LoggedBy; }
            set { _LoggedBy = value; }
        }
        public string EmpIdList
        {
            get { return _EmpIdList; }
            set { _EmpIdList = value; }
        }
        public object LatLong
        {
            get { return _LatLong; }
            set { _LatLong = value; }
        }
        public int LocReq
        {
            get { return _LocReq; }
            set { _LocReq = value; }
        }
        public string AppId
        {
            get { return _AppId; }
            set { _AppId = value; }
        }
        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public string Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }
        public string Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }
        public string LogDateTime
        {
            get { return _LogDateTime; }
            set { _LogDateTime = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public string LocSource
        {
            get { return _LocSource; }
            set { _LocSource = value; }
        }
        public string SrvcCalledBy
        {
            get { return _SrvcCalledBy; }
            set { _SrvcCalledBy = value; }
        }
        public string CellId
        {
            get { return _CellId; }
            set { _CellId = value; }
        }
        public string locationAreaCode
        {
            get { return _locationAreaCode; }
            set { _locationAreaCode = value; }
        }
        public string mobileCountryCode
        {
            get { return _mobileCountryCode; }
            set { _mobileCountryCode = value; }
        }
        public string mobileNetworkCode
        {
            get { return _mobileNetworkCode; }
            set { _mobileNetworkCode = value; }
        }
        public string RouteIdList
        {
            get { return _RouteIdList; }
            set { _RouteIdList = value; }
        }
        public int AreaId
        {
            get { return _AreaId; }
            set { _AreaId = value; }
        }
        public float Radius
        {
            get { return _Radius; }
            set { _Radius = value; }
        }
        public string AreaName
        {
            get { return _AreaName; }
            set { _AreaName = value; }
        }
        public string AreaIdList
        {
            get { return _AreaIdList; }
            set { _AreaIdList = value; }
        }
        public int IsInsert
        {
            get { return _IsInsert; }
            set { _IsInsert = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public int IsLatLongFromCellId
        {
            get { return _IsLatLongFromCellId; }
            set { _IsLatLongFromCellId = value; }
        }


        public string makeLocEntryByAppId()
        {
            // int IsWithInGeoFence = ChkGeoFence();
            int Result = Convert.ToInt32(TraceStudentLocation(9));

            //if ((Result > 0) && (LocReq == 10))
            //{

            //    return Result + "~" + SendSOSMessage();
            //}
            return Result.ToString();


        }
        //public string makeLocEntryByAppId1()
        //{
        //    int IsWithInGeoFence = 9;
        //    if (ChkIFFeatureIsEnableAccordingDate(38) == 1) // Geo-Fence FeatureId
        //    {
        //        IsWithInGeoFence = ChkGeoFence();
        //    }
        //    if (ChkIFFeatureIsEnableAccordingDate(60) == 1) // Location Alert 
        //    {
        //        CheckLocationAndSendAlert();
        //    }
        //    int Result = Convert.ToInt32(TraceDeviceLocationAccordingTofreq(IsWithInGeoFence));

        //    #region -- If SOS is Enable--
        //    if ((Result > 0) && (LocReq == 10) && ChkIFFeatureIsEnableAccordingDate(25) == 1)
        //    {
        //        string res= SendSOSMessage();
        //        if (res == "")
        //            return Result.ToString();                
        //        return Result + "~" + res;
        //        //dt = new DataTable();
        //        //dt = GetUsernameByUsrId();
        //        ////Alert
        //        //string UserName = dt.Rows[0]["UserName"].ToString();
        //        //string myMsg = dt.Rows[0]["UserName"].ToString() + " might be into trouble at location '" + Location + "' at this time '" + _LogDateTime;
        //        //dt = new DataTable();
        //        //dt = GetAdminDetail();
        //        //foreach (DataRow row in dt.Rows)
        //        //{
        //        //    SendSMSOfLocationforEmergency(row["MobileNo"].ToString(), _LogDateTime, _Location, _ClientId, row["UserName"].ToString(), _UserId, UserName);
        //        //}
        //        //dt = new DataTable();
        //        //dt = GetContactDtlByUsrId();
        //        //foreach (DataRow row in dt.Rows)
        //        //{
        //        //    SendSMSOfLocationforEmergency(row["ContactNo"].ToString(), _LogDateTime, _Location, _ClientId, row["Name"].ToString(), _UserId, UserName);
        //        //}              
        //        //return Result + "~" + myMsg;
        //    }
        //    #endregion

        //    return Result.ToString();


        //}
        public int IU_RouteGeoFence()
        {
            geofence = new tblRouteGeofenceTableAdapter();
            return Convert.ToInt32(geofence.IU_RouteGeofence(_RouteId, _ClientId, _RouteName, _RouteCode, _RouteDesc, _LoggedBy, _LatLong).ToString());
        }
        public DataTable IsRoteCodeExists()
        {
            geofence = new tblRouteGeofenceTableAdapter();
            return geofence.IsRouteCodeExists(_ClientId, _RouteCode);
        }
        public DataTable IsRoteNameExists()
        {
            geofence = new tblRouteGeofenceTableAdapter();
            return geofence.IsRouteNameExists(_ClientId, _RouteName);
        }
        public DataTable IsAreaNameExists()
        {
            Area = new tblAreaTableAdapter();
            return Area.IsAreaNameExists(_ClientId, _AreaName);
        }
        public DataTable GetGeoFenceRouteDetail()
        {
            geofence = new tblRouteGeofenceTableAdapter();
            return geofence.GetGeoFenceRouteDetail(_RouteId);
        }
        public int DeleteGeoFenceRoute()
        {
            geofence = new tblRouteGeofenceTableAdapter();
            return Convert.ToInt32(geofence.DeleteGeoFenceRoute(_RouteId, _LoggedBy).ToString());
        }
        public int IU_AsgnRoutetoEmp()
        {
            geofence = new tblRouteGeofenceTableAdapter();
            return Convert.ToInt32(geofence.IU_AsgnRoutetoEmp(_ClientId, _RouteId, _EmpIdList, _LoggedBy).ToString());
        }
        public DataTable GetLocFreqMgmt()
        {
            locfreqmgmt = new tblLocReqFreqMgmtByProfileIdTableAdapter();
            return locfreqmgmt.GetLocFreqMgmt(_ProfileId);
        }
        public int IU_LocReqFreqMgmtByProfileId()
        {
            locfreqmgmt = new tblLocReqFreqMgmtByProfileIdTableAdapter();
            return Convert.ToInt32(locfreqmgmt.IU_LocReqFreqMgmtByProfileId(_LocReqFreqId, _ProfileId, _ClientId, _FrmTime, _ToTime, _LocReqFrequency, _UserId).ToString());
        }
        public int DeleteLocReqFreqMgmt()
        {
            locfreqmgmt = new tblLocReqFreqMgmtByProfileIdTableAdapter();
            return Convert.ToInt32(locfreqmgmt.DeleteLocReqFreqMgmt(_LocReqFreqId));
        }
        public DataTable GetRouteNameByClientIdAndProfileId()
        {
            geofence = new tblRouteGeofenceTableAdapter();
            return geofence.GetRouteNameByClientIdAndProfileId(_ClientId, _ProfileId);
        }
        public int AssignRouteToProfile()
        {
            ProfileRoute = new tblProfileRouteTableAdapter();
            return Convert.ToInt32(ProfileRoute.AssignRouteToProfile(_ProfileId, _RouteIdList, _ClientId, _UserId));
        }
        public int AddRouteToProfile()
        {
            ProfileRoute = new tblProfileRouteTableAdapter();
            return Convert.ToInt32(ProfileRoute.AddRouteToProfile(_RouteId, _ClientId, _RouteName, _RouteCode, _RouteDesc, _LoggedBy, _LatLong, _ProfileId));
        }
        public int IU_Area()
        {
            Area = new tblAreaTableAdapter();
            return Convert.ToInt32(Area.IU_Area(_ClientId, _AreaName, _Location, _Radius, _AreaId, _UserId, _Latitude, _Longitude).ToString());
        }
        public int DeleteAreaDtls()
        {
            Area = new tblAreaTableAdapter();
            return Convert.ToInt32(Area.DeleteAreaDtls(_AreaId));
        }
        public int IU_tblProfileArea()
        {
            Area = new tblAreaTableAdapter();
            return Convert.ToInt32(Area.IU_tblProfileArea(_ClientId, _AreaName, _Location, _Radius, _AreaId, _UserId, _Latitude, _Longitude, _ProfileId));
        }
        public DataTable GetAreaNameByClientIdAndProfileId()
        {
            Area = new tblAreaTableAdapter();
            return Area.GetAreaNameByClientIdAndProfileId(_ClientId, _ProfileId);
        }
        public int AssignAreaToProfile()
        {
            Area = new tblAreaTableAdapter();
            return Convert.ToInt32(Area.AssignAreaToProfile(_ProfileId, _UserId, _AreaIdList));
        }

        public DataTable GetSetLocation()
        {
            setlocation = new tblSetLocationTableAdapter();
            return setlocation.GetSetLocation(_ProfileId);
        }
        public int InsertSetLocation()
        {
            setlocation = new tblSetLocationTableAdapter();
            return Convert.ToInt32(setlocation.InsertSetLocation(_ProfileId, _Latitude, _Longitude, _Location, _IsInsert).ToString());
        }
        public LocationModel GetLocation(string latitude, string longitude, string cellId, string lac, string mcc, string mnc, int ClientId, int tableId, int UserId, bool isLocationRequired = true)
        {

            #region---- Get Lat long From Cell Id---
            string Latlng = "0";
            int IsLatLongFromCellId = 0;
            string location = Constant.LocationNotFound;
            double dlatitude = 0, dlongitude = 0;
            long dcellId = 0, dLAC = 0, dMCC = 0, dMNC = 0;
            try
            {
                if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude))
                {
                    try
                    {
                        dlatitude = Convert.ToDouble(latitude);
                        dlongitude = Convert.ToDouble(longitude);
                    }
                    catch (Exception) { }
                }
                if (dlatitude == 0 && dlongitude == 0)
                {

                    if (!string.IsNullOrEmpty(cellId) && !string.IsNullOrEmpty(lac) && !string.IsNullOrEmpty(mcc) && !string.IsNullOrEmpty(mnc))
                    {
                        try
                        {
                            dcellId = Convert.ToInt64(cellId);
                            dLAC = Convert.ToInt64(lac);
                            dMCC = Convert.ToInt64(mcc);
                            dMNC = Convert.ToInt64(mnc);
                        }
                        catch (Exception) { }
                    }

                    if (dcellId != 0 && dLAC != 0 && dMCC != 0 && dMNC != 0)
                    {
                        Latlng = CellId_To_Lat(dcellId, dLAC, dMCC.ToString(), dMNC.ToString(), ClientId, tableId, Constant.CellToLatLong, UserId);
                        if (Latlng != "0")
                        {
                            dlatitude = Convert.ToDouble(Latlng.Substring(0, Latlng.IndexOf(",")));
                            dlongitude = Convert.ToDouble(Latlng.Substring(Latlng.IndexOf(",") + 1));
                        }
                        IsLatLongFromCellId = 1;
                    }
                }

                #region---- Get Location From lat long---
                if (isLocationRequired && dlatitude != 0 && dlongitude != 0)
                {
                    try
                    {
                        location = getGoogleLocationByLogLatFromApi(dlongitude.ToString(), dlatitude.ToString(), ClientId, tableId, Constant.GeoFence, UserId);                        
                    }
                    catch (Exception) { location = "Latitude: " + dlatitude + ", Longitude: " + dlongitude + ""; }
                }               

                #endregion
            }
            catch (Exception)
            { }
            #endregion
            return new LocationModel { latitude = dlatitude, longitude = dlongitude, location = location, isLatLongFromCellId = IsLatLongFromCellId };
        }
        public double getDistanceFromLatLonInMtr(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = deg2rad(lat2 - lat1);  // deg2rad below
            var dLon = deg2rad(lon2 - lon1);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d * 1000;
        }
        public int TraceDeviceLocationAccordingTofreq(int IsWithInGeoFence)
        {
            DeviceLoc = new tblDeviceLocationTableAdapter();
            try
            {
                return Convert.ToInt32(DeviceLoc.TraceDeviceLocationAccordingTofreq1(_DeviceId, _MobileNo, _Longitude, _Latitude, _LogDateTime, _Location, _LocSource, _SrvcCalledBy, IsWithInGeoFence, _CellId, _locationAreaCode, _mobileCountryCode, _mobileNetworkCode, _LocReq, _Status, _IsLatLongFromCellId));// TraceDeviceLocationAccordingTofreq
            }
            finally
            {
                DeviceLoc = null;
            }
        }
        public int CheckLocationAndSendAlert()
        {
            //6-- Within Geo-fence 9-- Out Of Geo-Fence
            int IsWithInGeoFence = 9;
            setlocation = new tblSetLocationTableAdapter();
            dt = new DataTable();
            double distance;
            dt = setlocation.GetLocationForAlert(_UserId);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    distance = getDistanceFromLatLonInMtr(Convert.ToDouble(_Latitude), Convert.ToDouble(_Longitude), Convert.ToDouble(row["Latitude"]), Convert.ToDouble(row["Longitude"]));
                    if (distance < 250) // 250 Meter to Check that the user is near to predefined Location
                    {
                        IsWithInGeoFence = 6;
                        break;
                    }
                    else
                    {
                        IsWithInGeoFence = 9;
                    }
                }
            }
            if (IsWithInGeoFence == 6)
            {
                int re = GetLastGeofenceStatus(_DeviceId);
                if (re != IsWithInGeoFence)
                {
                    InsertGeoFence(IsWithInGeoFence);
                    //-------- Send notification to reproting manager -------
                    //SendAlertOfGeofence(IsWithInGeoFence);
                    return 1;
                }
            }
            return 0;

        }
        public string ChkGeoFenceByRouteAndArea()
        {
            //6-- Within Geo-fence 9-- Out Of Geo-Fence
            geofence = new tblRouteGeofenceTableAdapter();
            dt = new DataTable();
            int IsWithInGeoFence = chkGeoFenceArea();
            if (IsWithInGeoFence == 9)
            {
                dt = geofence.GetRouteListIfEnableWithDate(_ClientId, _UserId, _LogDateTime);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        dtRoutePoint = new DataTable();
                        dtRoutePoint = geofence.GetGeoFenceRouteDetail(Convert.ToInt32(row["RouteId"].ToString()));
                        bool res = loadData(dtRoutePoint);
                        if (res)
                        {
                            IsWithInGeoFence = 6;
                            break;
                        }
                        else
                        {
                            IsWithInGeoFence = 9;
                        }
                    }
                }
            }
            int re = GetLastGeofenceStatus(_DeviceId);
            if (re != IsWithInGeoFence)
            {
                InsertGeoFence(IsWithInGeoFence);
                //-------- Send notification to reproting manager -------
                //SendAlertOfGeofence(IsWithInGeoFence);
                return IsWithInGeoFence + "~" + 1;
            }
            return IsWithInGeoFence + "~" + 0;
        }
        private int chkGeoFenceArea()
        {
            int IsWithInGeoFence = 9;
            Area = new tblAreaTableAdapter();
            dt = new DataTable();
            double distance;
            dt = Area.GetAreaListIfEnableWithDate(_ClientId, _UserId, _LogDateTime);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    distance = getDistanceFromLatLonInMtr(Convert.ToDouble(_Latitude), Convert.ToDouble(_Longitude), Convert.ToDouble(row["Latitude"]), Convert.ToDouble(row["Longitude"]));
                    if (distance < Convert.ToDouble(row["Radius"]))
                    {
                        IsWithInGeoFence = 6;
                        break;
                    }
                    else
                    {
                        IsWithInGeoFence = 9;
                    }
                }
            }
            return IsWithInGeoFence;

        }
        private int TraceStudentLocation(int IsWithInGeoFence)
        {
            DeviceLoc = new tblDeviceLocationTableAdapter();
            try
            {
                return Convert.ToInt32(DeviceLoc.TraceDeviceLocation(_DeviceId, _MobileNo, _Longitude, _Latitude, _LogDateTime, _Location, _LocSource, _SrvcCalledBy, IsWithInGeoFence, _CellId, _locationAreaCode, _mobileCountryCode, _mobileNetworkCode, _LocReq));
            }
            finally
            {
                DeviceLoc = null;
            }
        }

        //private int ChkGeoFence()
        //{
        //    //6-- Within Geo-fence 9-- Out Of Geo-Fence
        //    geofence = new tblRouteGeofenceTableAdapter();
        //    dt = new DataTable();
        //    int IsWithInGeoFence = chkGeoFenceArea();
        //    if (IsWithInGeoFence == 9)
        //    {
        //        dt = geofence.GetRouteListIfEnableWithDate(_ClientId, _UserId, _LogDateTime);
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                dtRoutePoint = new DataTable();
        //                dtRoutePoint = geofence.GetGeoFenceRouteDetail(Convert.ToInt32(row["RouteId"].ToString()));
        //                bool res = loadData(dtRoutePoint);
        //                if (res)
        //                {
        //                    IsWithInGeoFence = 6;
        //                    break;
        //                }
        //                else
        //                {
        //                    IsWithInGeoFence = 9;
        //                }
        //            }
        //        }
        //    }

        //    int re = GetLastGeofenceStatus(_DeviceId);
        //    if (re != IsWithInGeoFence)
        //    {
        //        InsertGeoFence(IsWithInGeoFence);
        //        //-------- Send notification to reproting manager -------
        //        SendAlertOfGeofence(IsWithInGeoFence);
        //    }
        //    return IsWithInGeoFence;
        //}

        //private void SendAlertOfGeofence(int IsWithInGeoFence)
        //{
        //    try
        //    {
        //        string reportingMngrEmail, reportingMngrName, reprotngMngrMob;
        //        user = new UserBAL();
        //        user.UserId = UserId;                
        //        DataTable myRptStu = user.GetRptngManagerByUserId();
        //        DataTable usrdtl = user.GetUserDtlByUserId();
        //        string geoFenceStatus = " is out of geofence.";
        //        if (IsWithInGeoFence == 6)
        //        {
        //            geoFenceStatus = " is within geofence.";
        //        }
        //        string myMsg = "your employee " + usrdtl.Rows[0]["UserName"].ToString() + geoFenceStatus + " His/her current location is " + _Location + " at " + _LogDateTime;
        //        if (myRptStu.Rows.Count > 0)
        //        {
        //            reportingMngrEmail = myRptStu.Rows[0]["EmailId"].ToString();
        //            reportingMngrName = myRptStu.Rows[0]["UserName"].ToString();
        //            reprotngMngrMob = myRptStu.Rows[0]["MobileNo"].ToString();

        //            //--- Send notification to repoting manager -----
        //            myMsg = "Dear " + reportingMngrName + " " + myMsg;
        //            SendSMSOfGeoFence(reprotngMngrMob, myMsg, _ClientId);
        //        }

        //        apibase = new APIBAL();
        //        apibase.IsAlertEnable(3, myMsg, _UserId, _ClientId);
        //    }
        //    catch (Exception) { }
        //}


        //private string SendSOSMessage()
        //{
        //    string myMsg = "";
        //    try
        //    {
        //        dt = new DataTable();
        //        dt = GetUsernameByUsrId();
        //        //Alert
        //        string UserName = dt.Rows[0]["UserName"].ToString();
        //         myMsg = dt.Rows[0]["UserName"].ToString() + " might be into trouble at location '" + _Location + "' at this time '" + _LogDateTime;
        //        dt = new DataTable();
        //        dt = GetAdminDetail();
        //        string text = "";
        //        sms = new SendSMSBAL();
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            text = "Dear " + row["UserName"].ToString() + ", " + UserName + " might be into trouble at location '" + _Location + "' at this time '" + _LogDateTime + "'.";// http://maps.google.com?q=" + Latitude + "," + Longitude + "";
        //            sms.sendMsgUsingSMS(text, row["MobileNo"].ToString(), ClientId);
        //        }
        //        dt = new DataTable();
        //        dt = GetContactDtlByUsrId();
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            text = "Dear " + row["Name"].ToString() + ", " + UserName + " might be into trouble at location '" + _Location + "' at this time '" + _LogDateTime + "'.";// http://maps.google.com?q=" + Latitude + "," + Longitude + "";
        //            sms.sendMsgUsingSMS(text, row["ContactNo"].ToString(), ClientId);
        //        }
        //        return myMsg;
        //    }
        //    catch (Exception)
        //    {
        //        return myMsg;
        //    }
        //}


        //private DataTable GetUsernameByUsrId()
        //{
        //    try
        //    {
        //        user = new UserBAL();
        //        user.UserId = UserId;                
        //        return user.GetUserDtlByUserId();
        //    }
        //    catch (Exception)
        //    {
        //        dt = new DataTable();
        //        return dt;
        //    }
        //}
        private int GetLastGeofenceStatus(int dvcId)
        {
            DeviceLoc = new tblDeviceLocationTableAdapter();
            DataTable dtgeo = DeviceLoc.GetLastGeoFenceStatus(dvcId);
            if (dtgeo.Rows.Count > 0)
            {
                return Convert.ToInt32(dtgeo.Rows[0]["IsWithinGeoFence"].ToString()) == 1 ? 6 : 9;
            }
            return 0;

        }
        //private void SendSMSOfGeoFence(string RcpntContactNo, string msg, int ClientId)
        //{
        //    try
        //    {
        //        sms = new SendSMSBAL();
        //        sms.sendMsgUsingSMS(msg, RcpntContactNo, ClientId);
        //    }
        //    catch (Exception) { }
        //    finally
        //    {
        //        sms = null;
        //    }
        //}
        //private void SendSMSOfLocationforEmergency(string ContactNo, string resTime, string Location, int ClientId, string ContactPersonName, int UserId, string UserName = "")
        //{
        //    try
        //    {
        //        //string UserName = "";
        //        //user = new UserBAL();
        //        //dt = new DataTable();
        //        //user.UserId = UserId;
        //        //dt = user.GetUserDtlByUserId();

        //        //try
        //        //{
        //        //    UserName = dt.Rows[0]["UserName"].ToString();
        //        //}
        //        //catch (Exception)
        //        //{
        //        //    UserName = "SomeOne";
        //        //}
        //        sms = new SendSMSBAL();
        //        string text = "Dear " + ContactPersonName + ", " + UserName + " might be into trouble at location '" + Location + "' at this time '" + resTime + "'.";// http://maps.google.com?q=" + Latitude + "," + Longitude + "";
        //        sms.sendMsgUsingSMS(text, ContactNo, ClientId);
        //    }
        //    catch (Exception) { }
        //}
        public DataTable GetContactDtlByUsrId()
        {
            try
            {
                soscontactdtl = new tblSosContactsTableAdapter();
                return soscontactdtl.GetSosContactByUserId(_UserId);
            }
            catch (Exception)
            {
                dt = new DataTable();
                return dt;
            }
        }
        //private DataTable GetAdminDetail()
        //{
        //    try
        //    {
        //        user = new UserBAL();
        //        user.ClientId = _ClientId;
        //        user.RoleId = 2;
        //        return user.GetUserByRoleId();
        //    }
        //    catch (Exception)
        //    {
        //        dt = new DataTable();
        //        return dt;
        //    }
        //}
        private DataTable GetAllGoogleKey()
        {
            GoogleKeyTableAdapter = new tblGoogleKeyTableAdapter();
            return GoogleKeyTableAdapter.GetAllGoogleKey();
        }
        private static bool IsPointInPolygon(List<Loc> poly, Loc point)
        {
            int i, j;
            bool c = false;
            for (i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
            {
                if ((((poly[i].Lg <= point.Lg) && (point.Lg < poly[j].Lg)) || ((poly[j].Lg <= point.Lg) && (point.Lg < poly[i].Lg))) &&
                    (point.Lt < (poly[j].Lt - poly[i].Lt) * (point.Lg - poly[i].Lg) / (poly[j].Lg - poly[i].Lg) + poly[i].Lt))
                    c = !c;
            }
            return c;
        }
        private bool loadData(DataTable dtpoint)
        {
            List<Loc> points = new List<Loc>();
            foreach (DataRow dr in dtpoint.Rows)
            {
                Loc p = new Loc();
                p.Lt = Double.Parse(dr["Latiude"].ToString());
                p.Lg = Double.Parse(dr["Longitude"].ToString());
                points.Add(p);

            }
            Loc po = new Loc();
            po.Lt = double.Parse(_Latitude);
            po.Lg = double.Parse(_Longitude);
            bool res = IsPointInPolygon(points, po);
            return res;
        }
        private int InsertGeoFence(int IsWithInGeoFence)
        {
            DeviceLoc = new tblDeviceLocationTableAdapter();
            return DeviceLoc.InsertGeoFence(_DeviceId, _MobileNo, _Longitude, _Latitude, _LogDateTime, _Location, _LocSource, _SrvcCalledBy, IsWithInGeoFence, _CellId, _locationAreaCode, _mobileCountryCode, _mobileNetworkCode, _LocReq);
        }
        private DataTable GetApIByType(int ClientId, int ApiType, int TableId, int UserId)
        {
            DataTable dt = new DataTable();
            try
            {
                GetAPIAndConsumeTableAdapter ta = new GetAPIAndConsumeTableAdapter();
                dt = (DataTable)ta.GetAPIAndConsume(ClientId, ApiType, TableId, UserId);
                return dt;
            }
            catch (Exception)
            {
                return dt;
            }
        }
        private string getGoogleLocationByLogLatFromApi(string log, string lat, int ClientId, int tableId, int ApiType, int UserId)
        {
            GoogleAPI gapi = new GoogleAPI();
            string location = "";
            location = getLocFrmDBByLngLat(log, lat);
            if (location.Trim() == "")
            {
                try
                {
                    location = gapi.GetAddressFromLatLong(Convert.ToDouble(lat), Convert.ToDouble(log));
                }
                catch (Exception) { }

                if (location.Trim() == "")
                {
                    DataTable dts = GetApIByType(ClientId, ApiType, tableId, UserId);
                    dt = new DataTable();
                    string url = dts.Rows[0]["API"].ToString();
                    bool IsGoogleAPI = false;
                    if (dts.Rows[0]["API"].ToString().Contains("https://maps.googleapis.com"))
                    {
                        dt = GetAllGoogleKey();
                        IsGoogleAPI = true;
                    }
                    else
                    {
                        url = url.Replace("<API_KEY>", dts.Rows[0]["Key"].ToString());
                    }
                    url = url.Replace("<lat>", lat).Replace("<log>", log);
                    /*End Here*/
                    location = gapi.getGoogleLocationByLogLat(url, dt, IsGoogleAPI);
                }
                if (location.Trim() == "")
                {
                    location = "Location not found";
                }
                else
                {
                    Updatelatlonglocation(log, lat, location);

                }
            }
            return location;

        }
        private string getLocFrmDBByLngLat(string Lng, string Lat)
        {
            string query = "", DbLocation = "";

            try
            {
                Search srch = new Search();
                query = @" select top 1 Location from LatLongtoLocation where Longitude='@Longitude' and Latitude='@Latitude' ";
                query = query.Replace("@Longitude", Lng);
                query = query.Replace("@Latitude", Lat);
                DataTable dt = new DataTable();
                dt = srch.SearchRecord(query).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DbLocation = dt.Rows[0]["Location"].ToString();
                }
            }
            catch (Exception) { }
            return DbLocation;
        }
        private void Updatelatlonglocation(string log, string lat, string location)
        {
            try
            {
                lattoloc = new LatLongtoLocationTableAdapter();
                lattoloc.InsertLatongToLocation(lat, log, location).ToString();
            }
            catch
            {
            }

        }
        private double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
        private string CellId_To_Lat(long cellId1, long locationAreaCode1, string mobileCountryCode1, string mobileNetworkCode1, int ClientId, int tableId, int ApiType, int UserId)
        {
            string Latlng;
            Latlng = getlatlngfromdbbycellid(cellId1, locationAreaCode1, mobileCountryCode1, mobileNetworkCode1);
            if (Latlng == "0")
            {

                DataTable dts = GetApIByType(ClientId, ApiType, tableId, UserId);
                dt = new DataTable();
                string url = dts.Rows[0]["API"].ToString();
                bool IsGoogleAPI = false;
                if (dts.Rows[0]["API"].ToString().Contains("https://www.googleapis.com"))
                {

                    dt = GetAllGoogleKey();
                    IsGoogleAPI = true;
                }
                else
                {
                    url = url.Replace("<API_KEY>", dts.Rows[0]["Key"].ToString());
                }
                GoogleAPI gapi = new GoogleAPI();
                Latlng = gapi.CellId_To_Lat(cellId1, locationAreaCode1, mobileCountryCode1, mobileNetworkCode1, url, dt, IsGoogleAPI);
                if (Latlng != "0")
                {
                    try
                    {
                        CellIdtoLatLngTableAdapter = new tblCellIdtoLatLngTableAdapter();
                        return CellIdtoLatLngTableAdapter.InsertCellToLatLong(cellId1.ToString(), locationAreaCode1.ToString(), mobileCountryCode1, mobileNetworkCode1, Latlng.Substring(0, Latlng.IndexOf(",")), Latlng.Substring(Latlng.IndexOf(",") + 1)).ToString();
                    }
                    finally
                    {
                        CellIdtoLatLngTableAdapter = null;
                    }
                }
            }
            return Latlng;

        }
        private string getlatlngfromdbbycellid(long cellId1, long locationAreaCode1, string mobileCountryCode1, string mobileNetworkCode1)
        {

            DataTable dt = new DataTable();
            CellIdtoLatLngTableAdapter = new tblCellIdtoLatLngTableAdapter();
            dt = CellIdtoLatLngTableAdapter.GetLatLngByCellId(cellId1.ToString(), locationAreaCode1.ToString(), mobileCountryCode1, mobileNetworkCode1);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Latitude"].ToString() + "," + dt.Rows[0]["Longitude"].ToString();
            }
            else
            {
                return "0";
            }
        }
        public DataTable GetLastLocationFromDeviceLocation(int ClientId)
        {
            DeviceLoc = new tblDeviceLocationTableAdapter();
            return DeviceLoc.GetLastLocationFromDeviceLocation(ClientId);
        }
    }
}
