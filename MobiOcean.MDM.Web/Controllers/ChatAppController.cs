using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;

namespace MobiOcean.MDM.Web.Controller
{
    public class ChatAppController : APIBase
    {
        AppBAL objappbal;
        DataTable dt, dtlst, dtClst;
        Calendar objCal;
        ContactBAL contact;

        int ClientId, UserId, DeviceId;

        // POST api/<controller>
        [ActionName("GetAppList")]
        public int Post([FromBody]AppBAL value)
        {
            dt = new DataTable();
            dt = getDeviceDtlByAppId(value.AppId);
            if (dt.Rows.Count > 0)
            {
                return installApplication(value.AppId, Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString()), Convert.ToInt32(dt.Rows[0]["UserId"].ToString()), Convert.ToInt32(dt.Rows[0]["ClientId"].ToString()), value.chatAppLst);
            }
            else
            {
                return -2;
            }

        }

        private int installApplication(string AppId, int DeviceId, int UserId, int ClientId, List<ChatAppLst> chatAppLst)
        {
            #region---------- Install App --------------------------
            try
            {
                objappbal = new AppBAL();
                objappbal.AppId = AppId;
                objappbal.DeviceId = DeviceId;
                objappbal.UserId = UserId;
                objappbal.ClientId = ClientId;
                objappbal.chatAppLst = chatAppLst;
                dtlst = new DataTable();
                dtlst = ConvertToDataTable(chatAppLst);
                objappbal.dtAppLst = dtlst;
                return objappbal.spChatAppList();
            }
            catch (Exception)
            {
                return 0;
            }
            #endregion
        }


        [ActionName("SyncCalendar")]
        public int Post([FromBody]Calendar value)
        {
            dt = new DataTable();
            dt = getDeviceDtlByAppId(value.AppId);
            if (dt.Rows.Count > 0)
            {
                return CalendarListData(value.AppId, Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString()), Convert.ToInt32(dt.Rows[0]["UserId"].ToString()), Convert.ToInt32(dt.Rows[0]["ClientId"].ToString()), value.calendarList);
            }
            else
            {
                return -2;
            }
        }
        private int CalendarListData(string AppId, int DeviceId, int UserId, int ClientId, List<CalendarKiList> calendarList)
        {
            try
            {
                if (getDeviceDtlByAppId(AppId).Rows.Count > 0)
                {
                    objCal = new Calendar();
                    objCal.AppId = AppId;
                    objCal.DeviceId = DeviceId;
                    objCal.UserId = UserId;
                    objCal.ClientId = ClientId;
                    objCal.calendarList = calendarList;
                    dtClst = new DataTable();
                    dtClst = ConvertInToDataTable(calendarList);
                    objCal.dtCalendarLst = dtClst;
                    return objCal.InsertIntoCalendar();
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        [ActionName("GetCalenderList")]
        public List<CalendarKiList> Get(string AppId, string Date)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
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
                    List<CalendarKiList> callst = new List<CalendarKiList>();
                    objCal = new Calendar();
                    objCal.DeviceId = DeviceId;
                    objCal.SyncDateTime = Date;
                    callst = objCal.CalenderSyncData();
                    return callst;
                }
                catch (Exception)
                {
                    List<CalendarKiList> callst = new List<CalendarKiList>();
                    return callst;
                }
                finally
                {
                    contact = null;
                }
            }
            else
            {
                List<CalendarKiList> callst = new List<CalendarKiList>();
                return callst;
            }
        }
        [ActionName("GetSyncDateTime")]
        public List<Calendar> Get(string AppId)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
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
                    List<Calendar> callst = new List<Calendar>();
                    dtlst = new DataTable();
                    contact = new ContactBAL();
                    contact.DeviceId = DeviceId;
                    dtlst = contact.GetCalendarSyncDateTime();
                    if (dtlst.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtlst.Rows)
                        {
                            objCal = new Calendar
                            {
                                SyncDateTime = row["SyncDateTime"].ToString()
                            };
                            callst.Add(objCal);
                        }
                    }
                    return callst;
                }
                catch (Exception)
                {
                    List<Calendar> callst = new List<Calendar>();
                    return callst;
                }
                finally
                {
                    contact = null;
                }
            }
            else
            {
                List<Calendar> callst = new List<Calendar>();
                return callst;
            }
        }

        private DataTable ConvertToDataTable(List<ChatAppLst> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ChatAppLst));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (ChatAppLst item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        private DataTable ConvertInToDataTable(List<CalendarKiList> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(CalendarKiList));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (CalendarKiList item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
