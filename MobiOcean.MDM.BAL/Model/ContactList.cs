using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.ContactDALTableAdapters;


/// <summary>
/// Summary description for ContactList
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class ContactList
    {
        spGetAllowedNoListTableAdapter alwdno;
        DataTable dt1;


        public int IsIncoming { get; set; }
        public int IsOutGoing { get; set; }
        public int IsSms { get; set; }
        public int ProfileId { get; set; }
        public string LogDateTime { get; set; }
        public int IsWhiteList { get; set; }
        public int ContactId { get; set; }
        public int ClientId { get; set; }
        public int DeviceId { get; set; }
        public int UserId { get; set; }
        public string AppId { get; set; }
        public string MobileNo { get; set; }
        public string contact_name { get; set; }
        public string SyncDateTime { get; set; }
        public string ContactName { get; set; }
        public string ContactMobileNo1 { get; set; }
        public string ContactMobileNo2 { get; set; }
        public string ContactMobileNo3 { get; set; }
        public string ContactMobileNo4 { get; set; }
        public string EmailId { get; set; }
        public string MessangerName { get; set; }
        public string MessangerId { get; set; }
        public string Address { get; set; }
        public string OrganizationName { get; set; }
        public string Website { get; set; }
        public string NickName { get; set; }
        public string Status { get; set; }

        public List<ContactList> GetAllowedNoList(int IsForUpdate = 0)
        {
            List<ContactList> lst;
            ContactList contactList;
            try
            {
                alwdno = new spGetAllowedNoListTableAdapter();
                dt1 = new DataTable();
                if (IsForUpdate == 0)
                {
                    dt1 = alwdno.spGetProfileAllowedNoList(DeviceId, ClientId);
                    //ContactNo,IsWhiteList,ProfileId,Name,IsIncoming,IsOutGoing,IsSms,Status
                    lst = new List<ContactList>();
                    foreach (DataRow row in dt1.Rows)
                    {
                        contactList = new ContactList
                        {
                            ContactMobileNo1 = row["ContactNo"].ToString(),
                            IsWhiteList = Convert.ToInt32(row["IsWhiteList"].ToString()),
                            ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                            ContactName = row["Name"].ToString(),
                            Status = row["Status"].ToString(),
                            IsIncoming = Convert.ToInt32(row["IsIncoming"].ToString()),
                            IsOutGoing = Convert.ToInt32(row["IsOutGoing"].ToString()),
                            IsSms = Convert.ToInt32(row["IsSms"].ToString())
                        };
                        lst.Add(contactList);
                    }
                }
                else
                {
                    dt1 = alwdno.GetProfileAllowedPhNo(DeviceId, ClientId);
                    lst = new List<ContactList>();
                    foreach (DataRow row in dt1.Rows)
                    {
                        contactList = new ContactList
                        {
                            ContactMobileNo1 = row["ContactNo"].ToString(),
                            IsWhiteList = Convert.ToInt32(row["IsWhiteList"].ToString()),
                            ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                            ContactName = row["Name"].ToString(),
                            Status = row["Status"].ToString(),
                            IsIncoming = Convert.ToInt32(row["IsIncoming"].ToString()),
                            IsOutGoing = Convert.ToInt32(row["IsOutGoing"].ToString()),
                            IsSms = Convert.ToInt32(row["IsSms"].ToString())
                        };
                        lst.Add(contactList);
                    }
                }


                return lst;


            }
            catch (Exception)
            {
                lst = new List<ContactList>();
                return lst;
            }
            finally
            {
                //contctlistTableAdapter = null;
            }

        }
        public List<ProfileContactList> GetAllowedNoList1(int IsForUpdate = 0)
        {
            List<ProfileContactList> lst;
            ProfileContactList contactList;
            try
            {
                alwdno = new spGetAllowedNoListTableAdapter();
                dt1 = new DataTable();
                if (IsForUpdate == 0)
                {
                    dt1 = alwdno.spGetProfileAllowedNoList(DeviceId, ClientId);
                    //ContactNo,IsWhiteList,ProfileId,Name,IsIncoming,IsOutGoing,IsSms,Status
                    lst = new List<ProfileContactList>();
                    foreach (DataRow row in dt1.Rows)
                    {
                        contactList = new ProfileContactList
                        {
                            ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                            ContactNumber = row["ContactNo"].ToString(),
                            IsWhiteList = Convert.ToInt32(row["IsWhiteList"].ToString()),
                            Status = Convert.ToInt32(row["Status"].ToString()),
                            IsIncoming = Convert.ToInt32(row["IsIncoming"].ToString()),
                            IsOutGoing = Convert.ToInt32(row["IsOutGoing"].ToString()),
                            IsSms = Convert.ToInt32(row["IsSms"].ToString())
                        };
                        lst.Add(contactList);
                    }
                }
                else
                {
                    dt1 = alwdno.GetProfileAllowedPhNo(DeviceId, ClientId);
                    lst = new List<ProfileContactList>();
                    foreach (DataRow row in dt1.Rows)
                    {
                        contactList = new ProfileContactList
                        {
                            ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                            ContactNumber = row["ContactNo"].ToString(),
                            IsWhiteList = Convert.ToInt32(row["IsWhiteList"].ToString()),
                            Status = Convert.ToInt32(row["Status"].ToString()),
                            IsIncoming = Convert.ToInt32(row["IsIncoming"].ToString()),
                            IsOutGoing = Convert.ToInt32(row["IsOutGoing"].ToString()),
                            IsSms = Convert.ToInt32(row["IsSms"].ToString())
                        };
                        lst.Add(contactList);
                    }
                }


                return lst;


            }
            catch (Exception)
            {
                lst = new List<ProfileContactList>();
                return lst;
            }
            finally
            {
                //contctlistTableAdapter = null;
            }

        }
    }
    public class ProfileContactList
    {
        public int ProfileId { get; set; }
        public string ContactNumber { get; set; }
        public int IsIncoming { get; set; }
        public int IsOutGoing { get; set; }
        public int IsSms { get; set; }
        public int IsWhiteList { get; set; }
        public int Status { get; set; }
    }
}
