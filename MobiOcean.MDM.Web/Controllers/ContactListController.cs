using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;
using System.ComponentModel;

namespace MobiOcean.MDM.Web.Controller
{
    public class ContactListController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        List<MDM.BAL.Model.ContactList> contlst;
        ContactBAL contact;
        DataTable dt, dtlst;

        [ActionName("GetContactList")]
        public List<MDM.BAL.Model.ContactList> Get(string AppId, string Date)
        {
            contlst = new List<MDM.BAL.Model.ContactList>();
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
                    contact = new ContactBAL();
                    contact.AppId = AppId;
                    contact.LogDateTime = Date;
                    contlst = contact.GetContactListData();
                    return contlst;
                }
                catch (Exception)
                {
                    return contlst;
                }
                finally
                {
                    contact = null;
                }
            }
            else
            {
                return contlst;
            }

        }
        [ActionName("GetSyncDateTime")]
        public List<MDM.BAL.Model.ContactList> Get(string AppId)
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
                    List<MDM.BAL.Model.ContactList> contlst = new List<MDM.BAL.Model.ContactList>();
                    contact = new ContactBAL();
                    contact.AppId = AppId;
                    contlst = contact.GetSyncDateTimeByAppId();
                    return contlst;
                }
                catch (Exception)
                {
                    List<MDM.BAL.Model.ContactList> contlst = new List<MDM.BAL.Model.ContactList>();
                    return contlst;
                }
                finally
                {
                    contact = null;
                }
            }
            else
            {
                List<MDM.BAL.Model.ContactList> contlst = new List<MDM.BAL.Model.ContactList>();
                return contlst;
            }
        }
        [ActionName("InsertIntoContactList")]
        public string Post([FromBody]ContactBAL value, int Id = 0)
        {
            try
            {

                dt = new DataTable();
                dt = getDeviceDtlByAppId(value.AppId);
                if (dt.Rows.Count > 0)
                {
                    return InsertContactList(value.AppId, Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString()), Convert.ToInt32(dt.Rows[0]["UserId"].ToString()), Convert.ToInt32(dt.Rows[0]["ClientId"].ToString()), dt.Rows[0]["MobileNo1"].ToString(), value.LogDateTime, value.contactlst, Id).ToString();
                }
                else
                {

                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }
        private int InsertContactList(string AppId, int DeviceId, int UserId, int ClientId, string MobileNo, string LogDateTime, List<ContLst> contactlst, int Id = 0)
        {
            #region---------- Install App --------------------------
            try
            {
                contact = new ContactBAL();
                contact.AppId = AppId;
                contact.DeviceId = DeviceId;
                contact.UserId = UserId;
                contact.ClientId = ClientId;
                contact.MobileNo = MobileNo;
                contact.LogDateTime = LogDateTime;
                contact.contactlst = contactlst;
                dtlst = new DataTable();
                dtlst = ConvertToDataTable(contactlst);
                contact.dtContactLst = dtlst;
                if (Id == 0)
                    return contact.spContactList();
                else
                    return contact.spUserContactList();
            }
            catch (Exception)
            {
                return 0;
            }
            #endregion
        }
        private DataTable ConvertToDataTable(List<ContLst> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ContLst));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (ContLst item in data)
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
