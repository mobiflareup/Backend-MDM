using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.ConveyanceDALTableAdapters;
using MobiOcean.MDM.Infrastructure;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System.Globalization;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for ConveyanceBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class ConveyanceBAL
    {

        tblConveyanceTableAdapter conveyance;
        tblModeOfTravelTableAdapter mode;        
        private int _IsLogin, _UserId, _ClientId, _CellId, _MNC, _MCC, _LAC, _IsApproved, _ConveyanceId, _createdbyKm, _ApprovedBy, _ConveyanceHistoryId, _ModeId;
        private string _LogDateTime, _Time, _Location, _Latitude, _Longitude, _appId, _UpdationDate, _TotalDistance, _ConveyanceRate, _ToLogDateTime;
        private string _Distance, _CreatedBy, _Remark, _ImagePath, _FromDate, _ToDate, _ModeOfTravel;
        private double _TotalAmount, _ConveyanceAmt, _KM, _VehicleReading;
        public int CellId
        {
            get { return _CellId; }
            set { _CellId = value; }
        }
        public int MNC
        {
            get { return _MNC; }
            set { _MNC = value; }
        }
        public int MCC
        {
            get { return _MCC; }
            set { _MCC = value; }
        }
        public int LAC
        {
            get { return _LAC; }
            set { _LAC = value; }
        }
        public string appId
        {
            get { return _appId; }
            set { _appId = value; }
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
        public string Time
        {
            get { return _Time; }
            set { _Time = value; }
        }
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        public string Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public string Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }

        public string Distance
        {
            get { return _Distance; }
            set { _Distance = value; }
        }

        public int IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
        public int ApprovedBy
        {
            get { return _ApprovedBy; }
            set { _ApprovedBy = value; }
        }
        public string CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        public int IsLogin
        {
            get { return _IsLogin; }
            set { _IsLogin = value; }
        }
        public int ConveyanceId
        {
            get { return _ConveyanceId; }
            set { _ConveyanceId = value; }
        }
        public string UpdationDate
        {
            get { return _UpdationDate; }
            set { _UpdationDate = value; }
        }
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public double KM
        {
            get { return _KM; }
            set { _KM = value; }
        }
        public int createdbyKm
        {
            get { return _createdbyKm; }
            set { _createdbyKm = value; }
        }
        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        public string TotalDistance
        {
            get { return _TotalDistance; }
            set { _TotalDistance = value; }
        }
        public string ConveyanceRate
        {
            get { return _ConveyanceRate; }
            set { _ConveyanceRate = value; }
        }
        public double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        public int ConveyanceHistoryId
        {
            get { return _ConveyanceHistoryId; }
            set { _ConveyanceHistoryId = value; }
        }
        public string ToLogDateTime
        {
            get { return _ToLogDateTime; }
            set { _ToLogDateTime = value; }
        }
        public string ModeOfTravel
        {
            get { return _ModeOfTravel; }
            set { _ModeOfTravel = value; }
        }
        public double ConveyanceAmt
        {
            get { return _ConveyanceAmt; }
            set { _ConveyanceAmt = value; }
        }
        public int ModeId
        {
            get { return _ModeId; }
            set { _ModeId = value; }
        }
        public double VehicleReading
        {
            get { return _VehicleReading; }
            set { _VehicleReading = value; }
        }
        public ConveyanceBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int insertConveyanceKM()
        {
            conveyance = new tblConveyanceTableAdapter();
            return Convert.ToInt32(conveyance.InsertConveyanceKM(_createdbyKm, _UserId, _ClientId, _KM).ToString());
        }
        public DataTable getfromConveyanceKm()
        {
            conveyance = new tblConveyanceTableAdapter();
            return conveyance.GetConveyanceKm(_ClientId);
        }
        public int InsertConveyance()
        {
            conveyance = new tblConveyanceTableAdapter();
            return Convert.ToInt32(conveyance.InsertConveyance(_Latitude, _Longitude, _IsLogin, _ClientId, _Time, _Location, _LogDateTime, _UserId, _ConveyanceId).ToString());
        }
        public DataTable InsertConveyanceLocation()
        {
            conveyance = new tblConveyanceTableAdapter();
            return conveyance.InsertConveyanceLocation(_Latitude, _Longitude, _IsLogin, _ClientId, _Time, _Location, _LogDateTime, _UserId, _ConveyanceId);
        }
        public int UpdateDistanceInTblConveyance()
        {
            conveyance = new tblConveyanceTableAdapter();
            return conveyance.UpdateDistanceInTblConveyance(Convert.ToDouble(_Distance), _ConveyanceId);
        }
        public int UpdateDistanceInConveyance()
        {
            conveyance = new tblConveyanceTableAdapter();
            return conveyance.UpdateDistanceInConveyance(Convert.ToDouble(_Distance), _ConveyanceId);
        }
        public int UpdateToLocationIntblConveyance()
        {
            conveyance = new tblConveyanceTableAdapter();
            return conveyance.UpdateToLocationIntblConveyance(Convert.ToDouble(_Distance), _Latitude, _Longitude, _Location, _UserId.ToString(), _ToLogDateTime, _ConveyanceId);
        }
        public int UpdateRemarkInConveyance()
        {
            conveyance = new tblConveyanceTableAdapter();
            return Convert.ToInt32(conveyance.UpdateRemarkInConveyance(_ConveyanceId, _Remark, _ImagePath, _UserId).ToString());
        }
        public int InsertApprovedConveyanceDetails()
        {
            conveyance = new tblConveyanceTableAdapter();
            return Convert.ToInt32(conveyance.InsertApprovedConveyanceDetails(_FromDate, _ToDate, Convert.ToDouble(_TotalDistance), Convert.ToDouble(_ConveyanceRate), Convert.ToDouble(_TotalAmount), _ApprovedBy, _UserId, _ClientId, _ConveyanceId).ToString());
        }
        public int UpdateConveyance()
        {
            conveyance = new tblConveyanceTableAdapter();
            return conveyance.UpdateConveyance(_ConveyanceHistoryId, _ApprovedBy.ToString(), _UserId.ToString(), _ConveyanceId);
        }
        public int IU_ModeOfTravel()
        {
            mode = new tblModeOfTravelTableAdapter();
            return Convert.ToInt32(mode.IU_ModeOfTravel(_ModeId, _ClientId, _ModeOfTravel, _ConveyanceAmt, Convert.ToInt32(_CreatedBy)).ToString());
        }
        public DataTable GetModeOfTravelDtls()
        {
            mode = new tblModeOfTravelTableAdapter();
            return mode.GetModeOfTravelByClientId(_ClientId);
        }
        public int DeleteModeOfTravel()
        {
            mode = new tblModeOfTravelTableAdapter();
            return mode.DeleteModeOfTravel(_CreatedBy, Convert.ToDateTime(_UpdationDate), _ModeId);
        }
        //public void Test()
        //{
        //    test = new TestConveyanceTableAdapter();
        //    test.InsertQuery(_ConveyanceId, _Latitude, _Longitude, _CellId.ToString(), _LAC.ToString(), _MCC.ToString(), _MNC.ToString(), IsFirst);
        //}
        public DataTable GetConveyanceLocationByConveyanceId()
        {
            conveyance = new tblConveyanceTableAdapter();
            return conveyance.GetConveyanceLocationByConveyanceId(ConveyanceId);
        }
        public int InsertConveyanceDetails()
        {
            conveyance = new tblConveyanceTableAdapter();
            return Convert.ToInt32(conveyance.InsertConveyanceDetails(_VehicleReading, _ImagePath, _Remark, _ConveyanceId).ToString());
        }
        public int UpdateConveyanceDetails()
        {
            conveyance = new tblConveyanceTableAdapter();
            return Convert.ToInt32(conveyance.UpdateConveyanceDetails(_VehicleReading, _ImagePath, _Remark, _ConveyanceId).ToString());
        }
        //public int? IsFirst { get; set; }
        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
          
            string RouteStop = "";
            int skipped = 0, skip = 0;
            if (table.Rows.Count > 2)
            {
                skip = (int)table.Rows.Count / 2;
            }
            foreach (DataRow row in table.Rows)
            {
                if (skipped == skip)// Latitude, Longitude,
                {
                    RouteStop +=RouteStop==""?row["Latitude"].ToString() + "," + row["Longitude"].ToString():"$"+ row["Latitude"].ToString() + "," + row["Longitude"].ToString();
                    skipped = 0;
                }
                else
                {
                    skipped++;
                }

            }
            return RouteStop;
        }
        public string EngglocTracting(int ConveyanceId,Stream RouteImage)
        {
            AnuSearch srch = new AnuSearch();
            DataTable dt,dt1 = new DataTable();            
            dt = srch.GetConveyanceDetails(ConveyanceId);
            conveyance = new tblConveyanceTableAdapter();
            dt1 = conveyance.GetConveyanceLocationByConveyanceId(ConveyanceId);
            string RouteLocation= DataTableToJSONWithJavaScriptSerializer(dt1);
            if (dt.Rows.Count >= 0)
            {
                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["EnggId"].ToString()))
                {
                    string logdate = DateTime.ParseExact(dt.Rows[0]["LogDateTime"].ToString().Substring(0,11), "dd'-'MMM'-'yyyy", CultureInfo.InvariantCulture).ToString("ddMMyyyy");
                    string url =Constant.GenusAPI + @"EngineerLocationTracking/" + dt.Rows[0]["callsrno"].ToString() + "/" + logdate + "/" + dt.Rows[0]["StartLocarion"].ToString() + "/" + dt.Rows[0]["EndLocation"].ToString() + "/" + RouteLocation + "/?engrid=" + dt.Rows[0]["EnggId"].ToString() + "&Distance=" + dt.Rows[0]["Distance"].ToString() + "&Time=" + dt.Rows[0]["time"].ToString() + "";
                    var client = new RestClient(url, HttpVerb.POST, RouteImage);
                    //client.PostData=
                    string json = client.MakeRequest();
                    dynamic testObj = JArray.Parse(JsonConvert.DeserializeObject(json).ToString());
                    foreach (var data in testObj)
                    {
                        if (data.Message == "Success")
                        {
                            return "Success";
                        }
                    }
                    return "fail";
                }
                 
                return "Engineering Id Not Exists";
            }
            else
            {
                return "ConveyanceId Not valid";
            }
        }
    }
}
