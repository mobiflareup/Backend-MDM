using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.DAL.DAL.NotificationDALTableAdapters;

/// <summary>
/// Summary description for NotificationBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class NotificationBAL //: ConstantBAL
    {
        SendNotificationMailTableAdapter send;
        private string _Notification;
        private int _userid, _clientid;

        public string Notification
        {
            get { return _Notification; }
            set { _Notification = value; }
        }
        public int userid
        {
            get { return _userid; }
            set { _userid = value; }
        }
        public int clientid
        {
            get { return _clientid; }
            set { _clientid = value; }
        }
        public NotificationBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        //public DataTable SendNotificationMail()
        //{
        //    send = new SendNotificationMailTableAdapter();
        //    return send.SendNotificationMail(GetCurrentDateTimeByCountry(1).ToString(), 208);
        //}
        public int InsertNotification()
        {
            send = new SendNotificationMailTableAdapter();
            return send.InsertNotification(_clientid, _userid, _Notification);
        }
        //public DataTable GetNotificationDetails()
        //{
        //    send = new SendNotificationMailTableAdapter();
        //    return send.GetNotificationDetails(_currentdatetime, _clientid);
        //}
    }
}