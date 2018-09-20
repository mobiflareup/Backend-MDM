using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.LTCollegeVisitDALTableAdapters;

/// <summary>
/// Summary description for LTCollegeVisitBAL
/// </summary>

namespace MobiOcean.MDM.BAL.BAL
{
    public class LTCollegeVisitBAL
    {
        LTCollegeVisitTableAdapter lt;
        public string appId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        public string LogDateTime { get; set; }
        public int ClientId { get; set; }
        public int CollegeId { get; set; }
        public int UserId { get; set; }
        public int IsOutTime { get; set; }
        public int CellId { get; set; }
        public int LAC { get; set; }
        public int MCC { get; set; }
        public int MNC { get; set; }
        public string Remark { get; set; }
        public int LTCollegeVisitId { get; set; }
        public string imgpath { get; set; }
        public string[] ImagePath { get; set; }
        public int Forgot { get; set; }
        public int InVerification { get; set; }
        public bool IsLocationManuallyEntered { get; set; }
        public double distance { get; set; }
        public string TowerLocation { get; set; }
        public string ManualLoaction { get; set; }
        public int visitId { get; set; }
        public LTCollegeVisitBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //public int InsertLTVisitDetails()
        //{
        //    lt = new LTCollegeVisitTableAdapter();
        //    return Convert.ToInt32(lt.InsertLTVisitDetails(Latitude, Longitude, Location, UserId, ClientId, CollegeId, Time, IsOutTime, UserId, Convert.ToDateTime(LogDateTime), Forgot).ToString());
        //}
        public int InsertLTVisitDetails1()
        {
            try
            {
                lt = new LTCollegeVisitTableAdapter();
                return Convert.ToInt32(lt.InsertLTVisitDetails1(Latitude, Longitude, Location, TowerLocation, ManualLoaction, UserId, ClientId, CollegeId, Time, IsOutTime, UserId, Convert.ToDateTime(LogDateTime), Forgot, InVerification, IsLocationManuallyEntered, distance).ToString());
            }
            catch (Exception ex)
            {
                string d = ex.Message;
                return 0;
            }
        }
        public int InsertLTVisitDetails2()
        {
            lt = new LTCollegeVisitTableAdapter();
            return Convert.ToInt32(lt.InsertLTVisitDetails2(Latitude, Longitude, Location, TowerLocation, ManualLoaction, UserId, ClientId, CollegeId, Time, IsOutTime, UserId, Convert.ToDateTime(LogDateTime), Forgot, InVerification, IsLocationManuallyEntered, distance,visitId).ToString());
        }
        public int UpdateDailyAssign()
        {
            lt = new LTCollegeVisitTableAdapter();
            return Convert.ToInt32(lt.sp_Update_LtcId(Latitude, Longitude, Location, UserId, ClientId, CollegeId, Time, IsOutTime, UserId, Convert.ToDateTime(LogDateTime), Forgot, InVerification).ToString());
        }
        public int InsertRemarkInLTCollegeVisitRemark()
        {
            lt = new LTCollegeVisitTableAdapter();
            return Convert.ToInt32(lt.InsertRemarkInLTCollegeVisitRemark(UserId, Remark, imgpath, LTCollegeVisitId, Convert.ToDateTime(LogDateTime)).ToString());
        }
        public DataTable GetLTCollegeVisitRemarkDetailsByLTCollegeVisitId()
        {
            lt = new LTCollegeVisitTableAdapter();
            return lt.GetLTCollegeVisitRemarkDetailsByLTCollegeVisitId(LTCollegeVisitId);
        }
    }
}
