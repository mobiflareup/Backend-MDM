using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.ContactDALTableAdapters;
using MobiOcean.MDM.BAL.Model;

/// <summary>
/// Summary description for ContactBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class ContactBAL
    {
        DataTable1TableAdapter tblusrdevice;
        tblCalanderSyncTableAdapter calsync;
        tblContactSyncTableAdapter contsync;
        tblUserContactTableAdapter usrcntct;
        tblSosContactsTableAdapter SosContacts;
        private int _ClientId, _UserId, _DeviceId, _Contact_Id, _IsWhiteList, _ProfileId, _Status;
        string _AppId, _SosIdList;
        string _LogDateTime, _MobileNo;
        string _Name, _Designation, _EmailId, _Company_Name, _TypeOfIndustry, _Country, _Remark, _ContactPersonName, _ContactNo;

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        public string TypeOfIndustry
        {
            get { return _TypeOfIndustry; }
            set { _TypeOfIndustry = value; }
        }
        public string Company_Name
        {
            get { return _Company_Name; }
            set { _Company_Name = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public int ProfileId
        {
            get { return _ProfileId; }
            set { _ProfileId = value; }
        }
        public string LogDateTime
        {
            get { return _LogDateTime; }
            set { _LogDateTime = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public string ContactPersonName
        {
            get { return _ContactPersonName; }
            set { _ContactPersonName = value; }
        }
        public string ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }
        public List<ContLst> contactlst { get; set; }
        public DataTable dtContactLst { get; set; }
        public string AppId
        {
            get { return _AppId; }
            set { _AppId = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        public int Contact_Id
        {
            get { return _Contact_Id; }
            set { _Contact_Id = value; }
        }
        public int IsWhiteList
        {
            get { return _IsWhiteList; }
            set { _IsWhiteList = value; }
        }
        public string SosIdList
        {
            get { return _SosIdList; }
            set { _SosIdList = value; }
        }
        public DataTable GetChartIncomingOutGoing()
        {
            calsync = new tblCalanderSyncTableAdapter();
            return calsync.GetChartIncomingOutGoing(_UserId);
        }
        public DataTable GetIncomingOutGoingCalls()
        {
            calsync = new tblCalanderSyncTableAdapter();
            return calsync.GetIncomingOutGoingCalls(_UserId);
        }
        public DataTable GetUsrNameByClientId()
        {
            tblusrdevice = new DataTable1TableAdapter();
            return tblusrdevice.GetUsrNameByClientId(_ClientId);
        }
        public DataTable GetUsrNameByUsrIdAndClientId()
        {
            tblusrdevice = new DataTable1TableAdapter();
            return tblusrdevice.GetUsrNameByUsrIdAndClientId(_ClientId, _UserId);
        }
        public DataTable GetSyncDateTime()
        {
            tblusrdevice = new DataTable1TableAdapter();
            return tblusrdevice.GetSyncDateTime(_DeviceId);
        }
        public int DeleteContactDtls()
        {
            tblusrdevice = new DataTable1TableAdapter();
            return tblusrdevice.DeleteContactDtls(_Contact_Id);
        }
        public int UpdateContactDtls()
        {
            tblusrdevice = new DataTable1TableAdapter();
            return tblusrdevice.UpdateContactDtls(_IsWhiteList, _MobileNo, _DeviceId);
        }
        public DataTable GetCalendarSyncDateTime()
        {
            calsync = new tblCalanderSyncTableAdapter();
            return calsync.GetCalendarSyncDateTime(_DeviceId);
        }
        public ContactBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int InsertContactUs()
        {
            usrcntct = new tblUserContactTableAdapter();
            return Convert.ToInt32(usrcntct.InsertContactUs(_Name, _MobileNo, _EmailId, _Company_Name, _TypeOfIndustry, _Country, _Remark));
        }
        public int spContactList()
        {
            tblusrdevice = new DataTable1TableAdapter();
            return Convert.ToInt32(tblusrdevice.spContactList(_DeviceId, _ClientId, _AppId, _MobileNo, _LogDateTime, dtContactLst));
        }
        public int spUserContactList()
        {
            usrcntct = new tblUserContactTableAdapter();
            return Convert.ToInt32(usrcntct.spUserContactList(_DeviceId, _ClientId, _AppId, _MobileNo, _LogDateTime, dtContactLst));
        }
        public List<ContactList> GetContactListData()
        {
            try
            {
                DataTable dt1;
                contsync = new tblContactSyncTableAdapter();
                dt1 = new DataTable();
                dt1 = contsync.GetContactDataByAppIdAndDate(_AppId, _LogDateTime);
                List<ContactList> lst = new List<ContactList>();
                ContactList contactList;
                foreach (DataRow row in dt1.Rows)
                {
                    contactList = new ContactList
                    {
                        ContactId = Convert.ToInt32(row["Contact_Id"]),
                        ClientId = Convert.ToInt32(row["ClientId"]),
                        DeviceId = Convert.ToInt32(row["DeviceId"]),
                        AppId = row["APPId"].ToString(),
                        MobileNo = row["Mobile_No"].ToString(),
                        LogDateTime = row["LogDateTime"].ToString(),
                        ContactName = row["Contact_Name"].ToString(),
                        ContactMobileNo1 = row["Contact_Mobile_No1"].ToString(),
                        ContactMobileNo2 = row["Contact_Mobile_No2"].ToString(),
                        ContactMobileNo3 = row["Contact_Mobile_No3"].ToString(),
                        ContactMobileNo4 = row["Contact_Mobile_No4"].ToString(),
                        EmailId = row["Email_Id"].ToString(),
                        MessangerName = row["Messanger_Name"].ToString(),
                        MessangerId = row["Messanger_Id"].ToString(),
                        Address = row["Address"].ToString(),
                        OrganizationName = row["Organization_Name"].ToString(),
                        Website = row["Website_Link"].ToString(),
                        NickName = row["Nick_Name"].ToString(),
                        IsWhiteList = Convert.ToInt32(row["IsWhiteList"])
                    };
                    lst.Add(contactList);
                }
                return lst;


            }
            finally
            {
                contsync = null;
            }

        }
        public List<ContactList> GetSyncDateTimeByAppId()
        {
            try
            {
                DataTable dt;
                tblusrdevice = new DataTable1TableAdapter();
                dt = new DataTable();
                try
                {
                    dt = tblusrdevice.GetContactSyncDate(_AppId);

                    List<ContactList> lst1 = new List<ContactList>();
                    ContactList contactList;
                    foreach (DataRow row in dt.Rows)
                    {
                        contactList = new ContactList
                        {
                            LogDateTime = row["LogDateTime"].ToString()
                        };
                        lst1.Add(contactList);
                    }
                    return lst1;
                }
                catch (Exception)
                {
                    List<ContactList> lst1 = new List<ContactList>();
                    return lst1;
                }
            }


            finally
            {
                contsync = null;
            }
        }
        public DataTable GetSosContactDetails()
        {
            SosContacts = new tblSosContactsTableAdapter();
            return SosContacts.GetSosContactDetails();
        }
        public int IU_SosContacts()
        {
            SosContacts = new tblSosContactsTableAdapter();
            return Convert.ToInt32(SosContacts.IU_SosContacts(_Name, _Designation, _MobileNo, _EmailId, _UserId, _Contact_Id, _ProfileId, _ClientId).ToString());
        }
        public int DeleteSosContact()
        {
            SosContacts = new tblSosContactsTableAdapter();
            return Convert.ToInt32(SosContacts.DeleteSosContact(_Contact_Id).ToString());
        }
        public int InsertSosContactsByProfile()
        {
            SosContacts = new tblSosContactsTableAdapter();
            return Convert.ToInt32(SosContacts.InsertSosContactsByProfile(_ClientId, _ProfileId, _ContactPersonName, _ContactNo, _Designation, _EmailId, _UserId));
        }
        public int AssignSosContactsToProfile()
        {
            SosContacts = new tblSosContactsTableAdapter();
            return Convert.ToInt32(SosContacts.AssignSosContactsToProfile(_SosIdList, _UserId, _Status).ToString());
        }
    }
    public class ContLst
    {
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
    }
}
