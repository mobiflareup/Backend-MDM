using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.SupportDALTableAdapters;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.BAL;

/// <summary>
/// Summary description for SupportBAL
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.BAL
{
    public class SupportBAL
    {
        public SupportBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        tblSupportTableAdapter support;        
        


        public string appId { get; set; }
        public string CompanyName { get; set; }       
        public int SupportId { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string DocPath { get; set; }
        public string DefectName { get; set; }
        public string ErrorURL { get; set; }
        public string DefectDesc { get; set; }
        public string DefectType { get; set; }
        public string Response { get; set; }
        public string ResponseDate { get; set; }
        public int RequestStatus { get; set; }

        public int InsertIntoSupport()
        {
            support = new tblSupportTableAdapter();
            return Convert.ToInt32(support.IU_Support(SupportId, UserId, ClientId, UserName, EmailId, MobileNo, DefectName, ErrorURL, DefectDesc, DefectType, DocPath, RequestStatus, Response, ResponseDate));
        }       
        public DataTable getdata()
        {
            support = new tblSupportTableAdapter();
            return support.GetData();
        }
        public int Insert_Support()
        {
            support = new tblSupportTableAdapter();
            DataTable dt = support.IU_AllSupport(SupportId, UserId, ClientId, UserName, EmailId, MobileNo, DefectName, ErrorURL, DefectDesc, DefectType, DocPath, RequestStatus, Response, ResponseDate, CompanyName);
            if (dt != null && dt.Rows.Count > 0)
            {
                GetSupportDetails(dt, ClientId);
                return 1;
            }
            else
            {
                return 0;
            }
            //return support.SP_InsertIntoSupport(_ClientId, _UserName, _EmailId, _MobileNo, _DefectName, _ErrorURL, _DefectDesc, _DefectType, _DocPath, _CompanyName).ToString();
        }


        public string GetSupportDetails(DataTable dt, int rolid)
        {
            string subject = "Support Details";
            string mailbody = "", msgbody = "";
            SendMailBAL sendmail = new SendMailBAL();
            if (dt.Rows.Count > 0)
            {
                try
                {
                    string status = dt.Rows[0]["RequestStatus"].ToString() == "0" ? "Open" : "Closed";
                    msgbody = msgbody + "<table><tr><td>Dear Support Team,</td></tr><tr><td>The support details are., </td></tr><tr><td>Name</td><td>" + dt.Rows[0]["UserName"].ToString() + "</td></tr><tr><td>Email Id</td><td>" + dt.Rows[0]["EmailId"].ToString() + "</td></tr><tr><td>Mobile No</td><td>" + dt.Rows[0]["MobileNo"].ToString() + "</td></tr><tr><td>Defect Name</td><td>" + dt.Rows[0]["DefectName"].ToString() + "</td></tr><tr><td>Error url</td><td>" + dt.Rows[0]["ErrorURL"].ToString() + "</td></tr><tr><td>Defect Description</td><td>" + dt.Rows[0]["DefectDesc"].ToString() + "</td></tr><tr><td>Defect Type</td><td>" + dt.Rows[0]["DefectType"].ToString() + "</td></tr></table>";
                    mailbody = mailbody + "<table><tr><td>Support Ticket Id</td><td>:</td><td>" + dt.Rows[0]["SupportId"].ToString() + "</td></tr><tr><td>Followup details (as per provided on the website) are as follows:</td><tr><tr><td>Mode of support </td><td>:</td><td>Email</td>";
                    mailbody = mailbody + "<tr><td>Name</td><td>:</td><td>" + dt.Rows[0]["UserName"].ToString() + "</td></tr><tr><td>Email Id</td><td>:</td><td>" + dt.Rows[0]["EmailId"].ToString() + "</td></tr><tr><td>Mobile No</td><td>:</td><td>" + dt.Rows[0]["MobileNo"].ToString() + "</td></tr>";
                    mailbody = mailbody + "<tr><td>Problem description</td><td>:</td><td>" + dt.Rows[0]["DefectDesc"].ToString() + "</td></tr>";
                    mailbody = mailbody + "<tr><td>Ticket Status</td><td>:</td><td>" + status + "</td></tr>";


                    if (rolid != 1)
                    {
                        // mailbody = mailbody + "</table>";
                        sendmail.SendEmail(Constant.supportEmail, subject, msgbody, 1, Constant.developerEmail);
                    }
                    mailbody = mailbody + "<tr><td>Response</td><td>:</td><td>" + dt.Rows[0]["Response"].ToString() + "</td></tr>";
                    mailbody = mailbody + "</table>";
                    sendmail.SupportMail(dt.Rows[0]["EmailId"].ToString(), dt.Rows[0]["UserName"].ToString(), mailbody);
                }
                catch (Exception)
                { }

            }

            return "1";

        }       
        public DataTable GetSupportDtlsBySupportId()
        {
            support = new tblSupportTableAdapter();
            return support.GetSupportDtlsBySupportId(SupportId);
        }
        public DataTable GetSupportDetailByUserId()
        {
            support = new tblSupportTableAdapter();
            return support.GetSupportDetailByUserId(UserId);
        }
        public DataTable GetSupportHistory()
        {
            support = new tblSupportTableAdapter();
            return support.GetSupportHistory(SupportId);
        }       
         


    }
}
