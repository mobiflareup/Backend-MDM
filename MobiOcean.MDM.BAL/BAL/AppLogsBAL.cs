using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.AppLogsTableAdapters;
using MobiOcean.MDM.BAL.Model;

/// <summary>
/// Summary description for AppLogsBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class AppLogsBAL
    {
        tblAppLogDtlTableAdapter applogta;
        

        public int DeviceId { get; set; }        
        public int ClientId { get; set; }       
        public int UserId { get; set; }      
        public int Duration { get; set; }      
        public string AppId { get; set; }       
        public int AppIndx { get; set; }
        public string MobileNo { get; set; }      
        public string AppName { get; set; }       
        public string StartTime { get; set; }       
        public string EndTime { get; set; }        
        public string LogDateTime { get; set; }
        public DateTime currentDateTime { get; set; }

        public string InsertAppLogs()
        {
            applogta = new tblAppLogDtlTableAdapter();
            try
            {
                return applogta.sp_InsertAppLogs(ClientId, DeviceId, UserId, AppId, MobileNo, AppName, StartTime, EndTime, Duration, LogDateTime, AppIndx, currentDateTime).ToString();
            }
            catch (Exception)
            {
                return "0";
            }
            finally
            {
                applogta = null;
            }
        }
    }
}