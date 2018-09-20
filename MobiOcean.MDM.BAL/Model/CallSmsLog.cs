using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.CallSMSDALTableAdapters;
using MobiOcean.MDM.BAL.BAL;

/// <summary>
/// Summary description for CallSmsLog
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.Model
{
    public class CallSmsLog
    {

        tblCallLogTableAdapter tblCalllog;

        public int ClientId { get; set; }
        public int DeviceId { get; set; }
        public int IsIncoming { get; set; }
        public string AppId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string startDateTime { get; set; }
        public int duration { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string cellId { get; set; }
        public string locationAreaCode { get; set; }
        public string mobileCountryCode { get; set; }
        public string mobileNetworkCode { get; set; }
        public string location { get; set; }
        public int UserId { get; set; }


        public int insertCallLogs()
        {
            try
            {
                tblCalllog = new tblCallLogTableAdapter();
                return Convert.ToInt32(tblCalllog.InsertCallLogDtls(DeviceId, IsIncoming, From, To, DateTime.Parse(startDateTime), duration, Lat, Long, location, cellId, locationAreaCode, mobileCountryCode, mobileNetworkCode).ToString());
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                tblCalllog = null;
            }
        }

    }
    public class SmsCallLog
    {

        tblSMSLogsTableAdapter tblsmslog;

        public int UserId { get; set; }
        public int ClientId { get; set; }
        public int IsIncoming { get; set; }
        public string AppId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string startDateTime { get; set; }
        public string MsgText { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string cellId { get; set; }
        public string locationAreaCode { get; set; }
        public string mobileCountryCode { get; set; }
        public string mobileNetworkCode { get; set; }
        public int DeviceId { get; set; }
        public string LogDateTime { get; set; }
        public string MobileNo { get; set; }
        public string location { get; set; }


        public int insertSMSLogs()
        {
            try
            {
                tblsmslog = new tblSMSLogsTableAdapter();
                return Convert.ToInt32(tblsmslog.InsertSMSLosgDtls(DeviceId, IsIncoming, From, To, startDateTime, MsgText, Lat, Long, location, cellId, locationAreaCode, mobileCountryCode, mobileNetworkCode, LogDateTime).ToString());
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                tblsmslog = null;
            }
        }
    }
    public class BulkSMS
    {
        UserSMSLogTableAdapter tblsmslog;
        DataTable dt;


        public SmsCallLog[] SMS { get; set; }
        public string AppId { get; set; }
        public string LogDateTime { get; set; }
        public int UserId { get; set; }
        public int DeviceId { get; set; }



        public int insertSMSLogs()
        {
            try
            {
                dt = new DataTable();
                dt.Columns.Add("MobileNo");
                dt.Columns.Add("SMS");
                dt.Columns.Add("IsIncoming");
                dt.Columns.Add("SMSDateTime");

                foreach (SmsCallLog msg in SMS)
                {
                    dt.Rows.Add(msg.MobileNo, msg.MsgText, msg.IsIncoming, msg.startDateTime);
                }

                try
                {
                    tblsmslog = new UserSMSLogTableAdapter();
                    return Convert.ToInt32(tblsmslog.InsertUserSms(AppId, UserId, DeviceId, LogDateTime, dt).ToString());

                }
                catch (Exception)
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

    }
}
