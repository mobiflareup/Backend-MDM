using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.PermisesDALTableAdapters;
/// <summary>
/// Summary description for PermisesBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class PermisesBAL
    {
        tblOnpremisesTableAdapter onpermises;
        CountryTableAdapter countryTableAdapter;
        private string _name, _mobileno, _emailid, _noofemployees, _period, _companyname, _industry;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string mobileno
        {
            get { return _mobileno; }
            set { _mobileno = value; }
        }
        public string emailid
        {
            get { return _emailid; }
            set { _emailid = value; }
        }
        public string noofemployees
        {
            get { return _noofemployees; }
            set { _noofemployees = value; }
        }
        public string period
        {
            get { return _period; }
            set { _period = value; }
        }
        public string companyname
        {
            get { return _companyname; }
            set { _companyname = value; }
        }
        public string industry
        {
            get { return _industry; }
            set { _industry = value; }
        }
        public string dtlist { get; set; }
        public List<categorylist> categorylist { get; set; }
        public PermisesBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int InsertOnPermises()
        {
            onpermises = new tblOnpremisesTableAdapter();
            return Convert.ToInt32(onpermises.InsertOnPermisesDtl(_name, _emailid, _mobileno, _companyname, _industry, _noofemployees, _period, dtlist).ToString());
        }
        public string GetCountry()
        {
            countryTableAdapter = new CountryTableAdapter();
            DataTable dt = new DataTable();
            dt = countryTableAdapter.GetData();


            return ConvertDataTabletoString(dt);
        }
        public DataTable GetCountries()
        {
            countryTableAdapter = new CountryTableAdapter();
            DataTable dt = new DataTable();
            dt = countryTableAdapter.GetData();
            return dt;
        }
        public string ConvertDataTabletoString(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);


        }
    }
    public class categorylist
    {
        public string CategoryName { get; set; }
    }
}