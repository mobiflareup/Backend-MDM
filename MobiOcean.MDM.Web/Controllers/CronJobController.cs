using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;

/// <summary>
/// Summary description for CronJobController
/// </summary>
/// 
namespace MobiOcean.MDM.Web.Controller
{
    public class CronJobController : APIBase
    {
        DeviceInfoBAL devinfobal;
        UserBAL userBAL;
        SendSMSBAL sms;
        DataTable dt, dt2;
        GingerboxSrch gsearch;
        CustomerBAL cust;
        [ActionName("ServerStatusCheck")]
        public void Get()
        {
            WebsiteLogsBAL websitelogsBal = new WebsiteLogsBAL();
            dt = new DataTable();
            dt = websitelogsBal.GetEnabledVPNList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime CreatedDate = Convert.ToDateTime(dt.Rows[i]["ServerUpdationDate"]);
                TimeSpan TsMin = (GetCurrentDateTimeByUserId(1) - CreatedDate);
                int min = (int)TsMin.TotalMinutes;
                if (min < 8)
                {

                }
                else
                {
                    websitelogsBal.ClientId = (int)dt.Rows[i]["ClientId"];
                    websitelogsBal.Status = 0;
                    int res = websitelogsBal.ServerDetailsByClientId();
                    if (res > 0)
                    {
                        UserDeviceBAL usrdevice = new UserDeviceBAL();
                        dt2 = new DataTable();
                        usrdevice.ClientId = (int)dt.Rows[i]["ClientId"];
                        dt2 = usrdevice.GetDeviceWithMDM();
                        foreach (DataRow row in dt2.Rows)
                        {
                            try
                            {
                                sms = new SendSMSBAL();
                                sms.sendFinalSMS(row["MobileNo1"].ToString(), "GBox set as WP2", (int)dt.Rows[i]["ClientId"]);
                            }
                            catch (Exception)
                            { }
                        }
                    }
                }
                //}
            }
        }
        [HttpPost]
        public void ActiveDeviceInfo()
        {
            devinfobal = new DeviceInfoBAL();
            dt = new DataTable();
            try
            {
                dt = devinfobal.CheckActiveStatusofDeviceBatterInfo();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        userBAL = new UserBAL();
                        userBAL.UserId = (int)dt.Rows[i]["UserId"];
                        DataTable dtUser = userBAL.GetUserDtlByUserId();

                        string text = dtUser.Rows[i]["UserName"] + "( " + dt.Rows[i]["MobileNo1"] + " ) is not active. Sent at: " + GetCurrentDateTimeByUserId(Convert.ToInt32(dt.Rows[i]["UserId"]));
                        sms = new SendSMSBAL();
                        dt2 = new DataTable();
                        userBAL.ClientId = Convert.ToInt32(dt.Rows[i]["ClientId"].ToString());
                        userBAL.RoleId = 2;
                        dt2 = userBAL.GetUserByRoleId();
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            if (j == 0)
                                sms.sendMsgUsingSMS(text, dt2.Rows[j]["MobileNo"].ToString(), Convert.ToInt32(dt.Rows[i]["ClientId"].ToString()),1);
                            else
                                break;
                        }
                        devinfobal.DeviceId = Convert.ToInt32(dt.Rows[i]["DeviceId"].ToString());
                        devinfobal.InsertOrUpdateBatterInfoSMS1();

                    }
                }
            }
            catch { }
        }
        [HttpGet]
        public void CustomerVisitDelayAlert()
        {
            //devinfobal = new DeviceInfoBAL();
            gsearch = new GingerboxSrch();
            dt = new DataTable();
            try
            {
                dt = gsearch.GetDelayedCustomerVisit();
                if (dt.Rows.Count > 0)
                {
                    cust = new CustomerBAL();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string text = dt.Rows[i]["UserName"].ToString() + " not reached yet at " + dt.Rows[i]["CustomerName"].ToString() + ". Sent at: " + GetCurrentDateTimeByUserId(Convert.ToInt32(dt.Rows[i]["UserId"])).ToString("dd-MMM-yyyy HH:mm");
                        sms = new SendSMSBAL();
                        userBAL = new UserBAL();
                        dt2 = new DataTable();                        
                        userBAL.UserId = Convert.ToInt32(dt.Rows[i]["UserId"].ToString());                        
                        dt2 = userBAL.GetRptngManagerByUserId();
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            if (j == 0)
                                sms.sendMsgUsingSMS(text, dt2.Rows[j]["MobileNo"].ToString(), Convert.ToInt32(dt.Rows[i]["ClientId"].ToString()),1);
                            else
                                break;
                        }
                        cust.AssignId = Convert.ToInt32(dt.Rows[i]["AssignId"].ToString());
                        cust.UpdateAssignDailyCustomerAlertStatus();
                    }
                }
            }
            catch { }
        }
    }
}
