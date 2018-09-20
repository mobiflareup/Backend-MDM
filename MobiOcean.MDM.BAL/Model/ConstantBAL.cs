using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MobiOcean.MDM.DAL.DAL.TimeZoneDALTableAdapters;

/// <summary>
/// Summary description for ConstantBAL
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{

    public class ConstantBAL
    {
        TimeZoneTableAdapter Tzone;
        DataTable dt;
        private DateTime CurrentDateTimeBAL1 = DateTime.UtcNow.AddMinutes(Constant.addMinutes);
        public DateTime GetCurrentDateTimeByUserId(int UserId)
        {
            try
            {
                dt = new DataTable();
                Tzone = new TimeZoneTableAdapter();
                dt = Tzone.GetTimeZoneByUserID(UserId);
                if (dt.Rows.Count > 0)
                {
                    CurrentDateTimeBAL1 = DateTime.UtcNow.AddMinutes((int)dt.Rows[0]["TimeZoneMinute"]);
                }
                else
                {
                    CurrentDateTimeBAL1 = DateTime.UtcNow.AddMinutes(Constant.addMinutes);
                }
            }
            catch (Exception)
            {
                CurrentDateTimeBAL1 = DateTime.UtcNow.AddMinutes(Constant.addMinutes);
            }
            return CurrentDateTimeBAL1;
        }
        public DateTime GetCurrentDateTimeByCountry(int CountryId)
        {
            try
            {
                dt = new DataTable();
                Tzone = new TimeZoneTableAdapter();
                dt = Tzone.GetTimeZoneByCountryId(CountryId);
                if (dt.Rows.Count > 0)
                {
                    CurrentDateTimeBAL1 = DateTime.UtcNow.AddMinutes((int)dt.Rows[0]["TimeZoneMinute"]);
                }
                else
                {
                    CurrentDateTimeBAL1 = DateTime.UtcNow.AddMinutes(Constant.addMinutes);
                }

            }
            catch (Exception)
            {
                CurrentDateTimeBAL1 = DateTime.UtcNow.AddMinutes(Constant.addMinutes);
            }
            return CurrentDateTimeBAL1;
        }
    }

}