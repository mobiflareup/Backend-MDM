using MobiOcean.MDM.DAL.DAL.TADALTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for TABAL
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.BAL
{
    public class TABAL
    {
        TA_ExtraDetailTableAdapter ta_ExtraDetailTableAdapter;
        TA_MasterTableAdapter ta_MasterTableAdapter;
        public int clientId { get; set; }

        public int UserId { get; set; }

        public string LogDateTime { get; set; }

        public string LogDate { get; set; }

        public double? ClaimedAmt { get; set; }

        public string filePath { get; set; }
        public string appId { get; set; }
        public string remark { get; set; }

        public TABAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable CheckLastVisitCustomer()
        {
            try
            {
                ta_MasterTableAdapter = new TA_MasterTableAdapter();
                return ta_MasterTableAdapter.CheckLastVisitCustomer(UserId, LogDate);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataTable CheckLastVisitCustomerRaj()
        {
            try
            {
                ta_MasterTableAdapter = new TA_MasterTableAdapter();
                return ta_MasterTableAdapter.CheckLastVisitCustomerRaj(UserId, LogDate);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int InsertExtraTADetail()
        {
            try
            {
                ta_ExtraDetailTableAdapter = new TA_ExtraDetailTableAdapter();
                return Convert.ToInt32(ta_ExtraDetailTableAdapter.InsertExtraTADetail(clientId, UserId, LogDateTime, LogDate, ClaimedAmt, filePath, remark).ToString());
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }

    public class TAVisitBAL
    {
        TA_MasterTableAdapter ta_MasterTableAdapter;
        TA_VisitDetailTableAdapter ta_VisitDetailTableAdapter;
        TA_LocationDetailTableAdapter ta_LocationDetailTableAdapter;
        tblCustomerTableAdapter tblcustomerTableadapter;
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int ClientId { get; set; }

        public string LogDate { get; set; }

        public string location { get; set; }

        public string LogDateTime { get; set; }
        public string remark { get; set; }
        public string filePath { get; set; }

        public int UserId { get; set; }

        public int CustomerId { get; set; }

        public int tempCustomerId { get; set; }

        public int ModeOfTravel { get; set; }
        public string appId { get; set; }
        public int IsLogin { get; set; }

        public int visitId { get; set; }

        public int CellId { get; set; }

        public int MCC { get; set; }

        public int MNC { get; set; }

        public int LAC { get; set; }
        public int IsVisited { get; set; }
        public double Distance { get; set; }
        public double ClaimedAmount { get; set; }
        public int MasterId { get; set; }

        public int tempcustomerid { get; set; }
        public int isvisited { get; set; }
        public string filepath { get; set; }
        public string Accuracy { get; set; }
        public string Altitude { get; set; }
        public string Bearing { get; set; }
        public string ElapsedRealtimeNanos { get; set; }
        public string Provider { get; set; }
        public string Speed { get; set; }
        public string Time { get; set; }
        public int LocationId { get; set; }

        public int InsertVisitTADetail()
        {
            try
            {
                ta_VisitDetailTableAdapter = new TA_VisitDetailTableAdapter();
                return Convert.ToInt32(ta_VisitDetailTableAdapter.InsertVisitTADetail(Latitude, Longitude, ClientId, LogDate, location, LogDateTime, UserId, CustomerId, tempCustomerId, ModeOfTravel).ToString());
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int InsertVisitTADetailRaj()
        {
            try
            {
                ta_VisitDetailTableAdapter = new TA_VisitDetailTableAdapter();
                return Convert.ToInt32(ta_VisitDetailTableAdapter.InsertVisitTADetailRaj(Latitude, Longitude, ClientId, LogDate, location, LogDateTime, UserId, CustomerId, tempCustomerId, AutoCustomerId, AutotempCustomerId, ModeOfTravel).ToString());
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public DataTable InsertVisitTALocation()
        {
            try
            {
                ta_LocationDetailTableAdapter = new TA_LocationDetailTableAdapter();
                //return ta_LocationDetailTableAdapter.InsertVisitTALocation(Latitude, Longitude,  location, LogDateTime,visitId);
                return ta_LocationDetailTableAdapter.InsertVisitTALocationRaj(Latitude, Longitude, location, LogDateTime, visitId, Accuracy, Altitude, Bearing, ElapsedRealtimeNanos, Provider, Speed, Time);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int UpdateCoveredDistance()
        {
            try
            {
                ta_VisitDetailTableAdapter = new TA_VisitDetailTableAdapter();
                return ta_VisitDetailTableAdapter.UpdateCoveredDistance(Distance, visitId);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public DataTable GetVisitDetailByVisitId()
        {
            try
            {
                ta_VisitDetailTableAdapter = new TA_VisitDetailTableAdapter();
                return ta_VisitDetailTableAdapter.GetVisitDetailByVisitId(visitId);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int UpdateVisitEndDetail()
        {
            try
            {
                ta_VisitDetailTableAdapter = new TA_VisitDetailTableAdapter();
                return ta_VisitDetailTableAdapter.UpdateVisitEndDetail(visitId, Latitude, Longitude, location, LogDateTime, ClientId, UserId, LogDate);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public DataTable GetCustomerFromVisitId()
        {
            try
            {
                tblcustomerTableadapter = new tblCustomerTableAdapter();
                return tblcustomerTableadapter.GetCustomerFromVisitId(visitId);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int UpdateVisitInfo()
        {
            try
            {
                ta_VisitDetailTableAdapter = new TA_VisitDetailTableAdapter();
                return ta_VisitDetailTableAdapter.UpdateVisitInfo(visitId, remark, filePath, LogDateTime, IsVisited);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataTable GetVisitDetailByMasterId()
        {

            ta_VisitDetailTableAdapter = new TA_VisitDetailTableAdapter();
            return ta_VisitDetailTableAdapter.GetVisitDetailsByMasterId(MasterId);

        }
        public DataTable GetVisitLocationDetailsbyVisitId()
        {
            ta_LocationDetailTableAdapter = new TA_LocationDetailTableAdapter();
            return ta_LocationDetailTableAdapter.GetData(visitId);
        }

        public void UpdateVisitDetailDistanceByVisitId()
        {
            ta_VisitDetailTableAdapter = new TA_VisitDetailTableAdapter();
            ta_VisitDetailTableAdapter.UpdateVisitDetailDistanceByVisitId(Distance, ClaimedAmount, visitId);
        }
        public void UpdateMasterDetailDistanceByMasterId()
        {
            ta_MasterTableAdapter = new TA_MasterTableAdapter();
            ta_MasterTableAdapter.UpdateMasterDetailDistanceByMasterId(ClaimedAmount, Distance, MasterId);
        }

        public int AutoCustomerId { get; set; }

        public int AutotempCustomerId { get; set; }

        public void UpdateLocationDetailByLocationId()
        {
            ta_LocationDetailTableAdapter = new TA_LocationDetailTableAdapter();
            ta_LocationDetailTableAdapter.UpdateLocationByLocationId(LocationId);
        }
    }
    public class Mode
    {
        private int _ModeId;
        private string _ModeOfTravel;
        public int ModeId
        {
            get { return _ModeId; }
            set { _ModeId = value; }
        }

        public string ModeOfTravel
        {
            get { return _ModeOfTravel; }
            set { _ModeOfTravel = value; }
        }
    }
}
