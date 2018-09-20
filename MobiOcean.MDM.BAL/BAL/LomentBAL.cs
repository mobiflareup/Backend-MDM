using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.LomentDALTableAdapters;

/// <summary>
/// Summary description for LomentBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class LomentBAL
    {
        LomentUserTableAdapter insert;
        DataTable dt;
        public int adminUserId { get; set; }
        public int userid { get; set; }
        public int clientid { get; set; }
        public int lomentuserid { get; set; }
        public int isadmin { get; set; }
        public string regpassword { get; set; }
        public string lomentusername { get; set; }
        public string keys { get; set; }
        public int lomentid { get; set; }
        public DateTime subscriptionstarttime { get; set; }
        public DateTime subscriptionendtime { get; set; }
        public int noofusers { get; set; }
        public int featureid { get; set; }
        public string paymenttype { get; set; }
        public string paymentdetails { get; set; }
        public string billdetails { get; set; }
        public string deviceid { get; set; }
        public string DeviceName { get; set; }
        public string Products { get; set; }
        public string subscriptiontype { get; set; }
        public string devicestatus { get; set; }
        public LomentBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int InsertLomentUser()
        {
            insert = new LomentUserTableAdapter();
            return insert.InsertLomentUser(userid, clientid, lomentuserid, isadmin, regpassword, lomentusername, keys);
        }
        public DataTable GetLomentUserByUserId()
        {
            insert = new LomentUserTableAdapter();
            return insert.GetLomentUserByUserId(userid);
        }
        public int InsertLomentSubscription()
        {
            insert = new LomentUserTableAdapter();
            return insert.InsertLomentSubscription(lomentid, clientid, lomentusername, subscriptionstarttime, subscriptionendtime, noofusers, featureid, paymenttype, paymentdetails, billdetails);
        }
        public int UpdateKeyInLomentUser()
        {
            insert = new LomentUserTableAdapter();
            return insert.UpdateKeyInLomentUser(keys, lomentusername);
        }
        public int UnLinkUser()
        {
            insert = new LomentUserTableAdapter();
            //return insert.UnLinkUser(lomentusername);
            return insert.UpdateLomentuserFeature(featureid, lomentuserid);
        }
        public DataTable GetLomentIdByLUserName()
        {
            insert = new LomentUserTableAdapter();
            return insert.GetLomentIdByLUserName(lomentusername, featureid);
        }
        public int UnLinkDevice()
        {
            insert = new LomentUserTableAdapter();
            return insert.UnLinkDevice(lomentid);
        }
        public int InsertLomentUserDevice()
        {
            insert = new LomentUserTableAdapter();
            return insert.InsertLomentUserDevice(lomentid, deviceid, DeviceName, lomentuserid, Products, subscriptiontype, subscriptionstarttime, subscriptionendtime, devicestatus, adminUserId, featureid);
            //        (
            //@LomentId int,
            //@DeviceId varchar(max),
            //@DeviceName varchar(max),
            //@LomentUserId int,
            //@Products varchar(max),
            //@SubscriptionType varchar(max),
            //@SubscriptionStartDate datetime,
            //@SubscriptionEndDate datetime,
            //@DeviceStatus varchar(max)
            //)
        }
        public DataTable GetLomentSubscriptionByClientId()
        {
            insert = new LomentUserTableAdapter();
            return insert.GetLomentSubscriptionByClientId(clientid);
        }
        public DataTable GetDeviceIdByUserId()
        {
            insert = new LomentUserTableAdapter();
            return insert.GetDeviceIdByUserId(userid, featureid);
        }

        public string UpdateLomentUserStatus()
        {
            try
            {
                insert = new LomentUserTableAdapter();
                int res = insert.UpdateLomentUser(userid);
                return "1";
            }
            catch
            { return "0"; }
        }
        public DataTable GetLomentAllocateUser()
        {
            try
            {
                dt = new DataTable(); ;
                insert = new LomentUserTableAdapter();
                dt = insert.GetLomentAllocateUser(userid);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public int InsertLomentUserFeature()
        {
            try
            {
                insert = new LomentUserTableAdapter();
                int res = insert.InsertLomentUserFeature(lomentuserid, featureid, keys, adminUserId, clientid);
                return res;
            }
            catch
            {
                return 0;
            }

        }
    }
}
