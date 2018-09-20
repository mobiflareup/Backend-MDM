using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel;
using MobiOcean.MDM.DAL.DAL.CalendarListTableAdapters;

/// <summary>
/// Summary description for Calendar
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class Calendar
    {
        tblCalanderSyncTableAdapter calendarDataDAL;
        CalendarKiList calendarLst;       

        public string AppId { get; set; }
        public string APPId { get; set; }
        public string MobileNo { get; set; }
        public int DeviceId { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public int CalenderSyncId { get; set; }
        public List<CalendarKiList> calendarList { get; set; }
        public DataTable dtCalendarLst { get; set; }
        public string SyncDateTime { get; set; }

        public Calendar()
        {

        }

        public List<CalendarKiList> CalenderSyncData()
        {
            try
            {
                DataTable dt1;
                calendarDataDAL = new tblCalanderSyncTableAdapter();
                dt1 = new DataTable();
                dt1 = calendarDataDAL.GetCalenderDataForApp(DeviceId, SyncDateTime);
                List<CalendarKiList> lst = new List<CalendarKiList>();

                foreach (DataRow row in dt1.Rows)
                {
                    calendarLst = new CalendarKiList()
                    {
                        //CalenderSyncId = Convert.ToInt32(row["CalenderSyncId"]),

                        EventName = row["EventName"].ToString(),
                        Location = row["Location"].ToString(),
                        StartDateTime = row["StartDateTime"].ToString(),
                        EndDateTime = row["EndDateTime"].ToString(),
                        Repetition = row["Repetition"].ToString(),
                        Description = row["Description"].ToString(),
                    };
                    lst.Add(calendarLst);
                }
                return lst;
            }
            finally
            {
                calendarDataDAL = null;
            }
        }
        public int InsertIntoCalendar()
        {
            calendarDataDAL = new tblCalanderSyncTableAdapter();
            return Convert.ToInt32(calendarDataDAL.spCalenderList(DeviceId, ClientId, UserId, AppId, MobileNo, SyncDateTime, dtCalendarLst).ToString());
        }

    }
    public class CalendarKiList
    {
        public string EventName { get; set; }
        public string Location { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string Repetition { get; set; }
        public string Description { get; set; }
        public string SyncDateTime { get; set; }
    }
}