using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;

namespace MobiOcean.MDM.Web.Controller
{
    public class SendNotificationController : APIBase
    {
        DataTable dt, dt1, dt2;
        ClientBAL client;
        int clientid;
        NotificationBAL notify;
        UserDeviceBAL UDBal;
        SendMailBAL send;
        SendSMSBAL sendsms;

        [ActionName("ExpiryReminder")]
        public int Post([FromBody]string value)
        {
            dt = new DataTable();
            client = new ClientBAL();
            dt = client.getdata();
            foreach (DataRow obj in dt.Rows)
            {
                clientid = Convert.ToInt32(obj["ClientId"].ToString());
                string maibody = "";
                int ExpiryDay = Convert.ToInt32(obj["remainingdays"].ToString());
                if (ExpiryDay == 15)
                {
                    maibody = "Your MobiOcean subscription will expire in 15 days. To Continue our service Plesae Subscribe.";
                    InsertNotification(maibody, ExpiryDay);
                }
                else if (ExpiryDay == 5)
                {
                    maibody = "Your MobiOcean subscription will expire in 5 days. To Continue our service Plesae Subscribe.";
                    InsertNotification(maibody, ExpiryDay);
                }
                else if (ExpiryDay == 1)
                {
                    maibody = "Your MobiOcean subscription will expire Tomorrow. To Continue our service Plesae Subscribe.";
                    InsertNotification(maibody, ExpiryDay);
                }
                else if (ExpiryDay == 0)
                {
                    maibody = "Your MobiOcean Subscription has expired Today. To Continue our service Plesae Subscribe.";
                    InsertNotification(maibody, ExpiryDay);

                    #region ---- Send Expiry SmS to Device---
                    dt2 = new DataTable();
                    UDBal = new UserDeviceBAL();
                    dt2 = UDBal.GetDeviceWithMDM();
                    foreach (DataRow no in dt2.Rows)
                    {
                        string text = "";
                        text = "GBox set as EC " + Convert.ToDateTime(obj["ExpiryDate"].ToString()).ToString("dd MM yy");
                        sendsms = new SendSMSBAL();
                        sendsms.sendFinalSMS(no["MobileNo1"].ToString(), text, clientid);
                    }
                    #endregion
                }


            }
            try
            {
                GrideTable(dt);
            }
            catch (Exception) { }
            return 1;
        }
        private void InsertNotification(string maibody, int remainingDays)
        {
            try
            {

                dt1 = new DataTable();
                dt1 = GetAdminDetail(clientid);
                foreach (DataRow obj1 in dt1.Rows)
                {
                    notify = new NotificationBAL();
                    notify.userid = Convert.ToInt32(obj1["UserId"].ToString());
                    notify.clientid = clientid;
                    notify.Notification = maibody;
                    notify.InsertNotification();
                    send = new SendMailBAL();
                    send.ExpiryReminder(obj1["EmailId"].ToString(), remainingDays, clientid);
                }
            }
            catch (Exception) { }
        }
        private void GrideTable(DataTable dtpdf)
        {
            string Message = "";
            DataView view = new DataView(dtpdf);
            DataTable selected = view.ToTable("Selected", false, "ClientCode", "ClientName", "EmailId", "ManagerName", "ManagerContactNo", "ExpiryDate", "remainingdays");
            // ClientCode, ClientName, EmailId, ManagerName, ManagerContactNo,ExpiryDate, remainingdays

            if (selected.Rows.Count > 0)
            {
                // ClientCode, ClientName, EmailId, ManagerName, ManagerContactNo, ExpiryDate,remainingdays
                Message = ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style='background-color:#F1F1F1; font-size: 10px;'>");

                Message = Message + ("<tr style='background-color:#2A368B; color:White; font-size: 12px;'>");
                Message = Message + ("<td colspan='7' align='center'>");
                Message = Message + ("<b style='font-size: 20px;'>Client List whose subscription is going to expire in next 15 days</b>");
                Message = Message + ("</td>");
                Message = Message + ("</tr>");
                // Message = Message + ("</table >");

                // Message = Message + ("<table width='100%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;' border='1'>");

                Message = Message + ("<tr  style='background-color:#2A368B; color:White; font-size: 12px;'>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Client Code</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Client Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>E-mail ID</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>User Name</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Contact No</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Expiry Date</b>");
                Message = Message + ("</td>");

                Message = Message + ("<td align='center'>");
                Message = Message + ("<b>Remaining Days</b>");
                Message = Message + ("</td>");

                Message = Message + ("</tr>");

                for (int i = 0; i < selected.Rows.Count; i++)
                {
                    Message = Message + ("<tr>");
                    for (int j = 0; j < selected.Columns.Count; j++)
                    {

                        Message = Message + ("<td align='center' >");
                        try
                        {
                            string cellText = selected.Rows[i][j].ToString();
                            Message = Message + (cellText);
                        }
                        catch (Exception) { }
                        Message = Message + ("</td>");
                    }
                    Message = Message + ("</tr>");
                }

                Message = Message + ("</table>");
            }
            // string res = Message;
            send = new SendMailBAL();
            send.SendEmail(Constant.salesEmail, "List of clients", Message, 1, Constant.developerEmail+","+ Constant.ceoAndMDEmail);
        }
    }
}

