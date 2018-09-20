using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MobiOcean.MDM.DAL.DAL.AttendanceDALTableAdapters;

/// <summary>
/// Summary description for AttendanceBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class AttendanceBAL
    {
        tblAttendanceTableAdapter attendance;
        VodaFoneTableAdapter vod;
        tblGeoFenceTableAdapter geo;

        public string AttendanceDateTime { get; set; }
        public int CellId { get; set; }       
        public int MNC { get; set; }      
        public int MCC { get; set; }       
        public int LAC { get; set; }       
        public string appId { get; set; }    
        public string LogDateTime { get; set; }       
        public int UserId { get; set; }      
        public string Time { get; set; }      
        public string Location { get; set; }      
        public string Latitude { get; set; }       
        public int ClientId { get; set; }     
       
        public string Longitude { get; set; }        
        public string AttendanceDate { get; set; }      
        public string AttendanceStatus { get; set; }     
             
        public int IsLogin { get; set; }
        public int AttendanceId { get; set; }       
        public string UpdationDate { get; set; }
        public string InLocation { get; set; }
        public string OutLocation { get; set; }
        public string TowerLocation { get; set; }
        public string ManualLoaction { get; set; }
        public bool IsLocationManuallyEntered { get; set; }
        public string Imagepath { get; set; }
        public AttendanceBAL()
        {
            // 
            // TODO: Add constructor logic here
            //
        }
        public int InsertAttendanceDetails()
        {
            attendance = new tblAttendanceTableAdapter();
            return (int)attendance.InsertAttendanceDetails(Latitude, Longitude, IsLogin, ClientId, UserId, Time, Location, AttendanceDate, UserId, AttendanceDateTime, TowerLocation, ManualLoaction, IsLocationManuallyEntered, AttendanceId,Imagepath);
        }
        public int InsertAttendanceDetailsWithDate()
        {
            attendance = new tblAttendanceTableAdapter();
            return attendance.InsertAttendanceDetailsWithDate(Latitude, Longitude, IsLogin, ClientId, UserId, Time, Location, AttendanceDate, UserId, AttendanceDateTime);
        }
        public int UpdateAttendanceStatus()
        {
            attendance = new tblAttendanceTableAdapter();
            return attendance.UpdateAttendanceStatus(Convert.ToInt32(AttendanceStatus), UserId.ToString(), UpdationDate, AttendanceId);
        }
        //public int InsertAttendanceDetailsWithDateNew()
        //{
        //    attendance = new tblAttendanceTableAdapter();
        //    return (int)attendance.InsertAttendanceDetailsWithDateNew(Latitude, Longitude, IsLogin, ClientId, UserId, Time, Location, AttendanceDate, UserId, AttendanceDateTime, TowerLocation, ManualLoaction, IsLocationManuallyEntered, AttendanceId);
        //}

        public DataTable GetVodaFoneUsersShiftList()
        {
            vod = new VodaFoneTableAdapter();
            return vod.GetVodaFoneUsersShiftList(1);//Vodafone Type
        }
        public DataTable GetVodaFoneLocationFromUserId()
        {
            vod = new VodaFoneTableAdapter();
            return vod.GetVodaFoneLocationFromUserId(UserId, Time);
        }
        public void InsertAttendanceDetailsWithVodaFoneDetails()
        {
            vod = new VodaFoneTableAdapter();
            vod.InsertAttendanceDetailsWithVodaFoneDetails(IsLogin, ClientId, UserId, Latitude, Longitude, Location, Time , AttendanceDate, AttendanceDateTime);
        }
        public DataTable GeoFenceDataForUser(int userId)
        {
            geo = new tblGeoFenceTableAdapter();
            return geo.GetGeoFenceDetailsByUserId(userId.ToString());
        }
    }
}
