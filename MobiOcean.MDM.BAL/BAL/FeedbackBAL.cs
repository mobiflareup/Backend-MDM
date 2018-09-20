using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.FeedbackDALTableAdapters;

/// <summary>
/// Summary description for FeedbackBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class FeedbackBAL
    {
        private int _FeedbackId, _PartnerId, _Type_Id;
        private string _Name, _EmailId, _MobileNo, _Feedback, _CompanyName, _Details, _Industry;
        tblFeedbackTableAdapter feedback;
        tblPartnerTableAdapter partner;
        public int Type_Id
        {
            get { return _Type_Id; }
            set { _Type_Id = value; }
        }
        public string Industry
        {
            get { return _Industry; }
            set { _Industry = value; }
        }
        public int FeedbackId
        {
            get { return _FeedbackId; }
            set { _FeedbackId = value; }
        }
        public int PartnerId
        {
            get { return _PartnerId; }
            set { _PartnerId = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public string Feedback
        {
            get { return _Feedback; }
            set { _Feedback = value; }
        }
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }
        public FeedbackBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string IU_Feedback()
        {
            feedback = new tblFeedbackTableAdapter();
            return feedback.IU_Feedback(_CompanyName, _Name, _EmailId, _MobileNo, Feedback, FeedbackId).ToString();
        }
        public string InsertPartnerDetails()
        {
            partner = new tblPartnerTableAdapter();
            return partner.InsertPartnerDetails(_Name, _CompanyName, _EmailId, _MobileNo, _Industry, _Type_Id).ToString();
        }
    }
}
