using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.DAL;
using MobiOcean.MDM.DAL.DAL.EkycDALTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiOcean.MDM.BAL.BAL
{
    public class EkycBAL
    {
        private string query = "";
        Search sc;
        public string Aadhar { get; set; }
        public string Name { get; set; }
        public bool Ins_Ekyc(string Name, string Adhaar, string dob, string Img, string ParentName, string gender, string houseNo, string street, string landMark, string locality, string vtc, string PostOffice, string subdist, string dist, string state, string pincode, string country, string kycRes_code, string kycRes_ts, int ClientId,int UserId)
        {
            string Address = "";
            if (!string.IsNullOrWhiteSpace(houseNo))
            {
                Address += "No:" + houseNo + "- ";
            }
            if (!string.IsNullOrWhiteSpace(street))
            {
                Address += street + ", \n";
            }
            if (!string.IsNullOrWhiteSpace(landMark))
            {
                Address += landMark + "(landmark), \n";
            }
            if (!string.IsNullOrWhiteSpace(locality))
            {
                Address += locality + ", \n";
            }
            if (!string.IsNullOrWhiteSpace(vtc))
            {
                Address += vtc + ", \n";
            }
            if (!string.IsNullOrWhiteSpace(PostOffice))
            {
                Address += PostOffice + "(Post), \n";
            }
            if (!string.IsNullOrWhiteSpace(subdist))
            {
                Address += subdist + ", \n";
            }
            if (!string.IsNullOrWhiteSpace(dist))
            {
                if (!string.IsNullOrWhiteSpace(pincode))
                {
                    Address += dist + "-" + pincode + ", \n";
                }
                else
                {
                    Address += dist + ", \n";
                }
            }
            if (!string.IsNullOrWhiteSpace(state))
            {
                Address += state + ", ";
            }
            if (!string.IsNullOrWhiteSpace(country))
            {
                Address += country + ".";
            }
            using (DataTable1TableAdapter DTA = new DataTable1TableAdapter())
            {

                int i = DTA.Sp_Ins_EKYC(Name, Adhaar, Address, null, ParentName, Convert.ToDateTime(dob), Img, UserId, ClientId, gender, DateTime.UtcNow.AddMinutes(Constant.addMinutes), kycRes_code, kycRes_ts);
                return i > 0 ? true : false;
            }

        }
        public DataTable Get_Ekyc(string ClientId)
        {
            query = "select * from [dbo].[EKYC] where clientId='" + ClientId + "'";
            if (!string.IsNullOrWhiteSpace(Aadhar))
            {
                query += " and AadharNo='"+Aadhar+"'";
            }
            if (!string.IsNullOrWhiteSpace(Name))
            {
                query += " and Name='"+Name+"'";
            }
            sc = new Search();
            return sc.SearchRecord(query).Tables[0];

        }
    }
}
