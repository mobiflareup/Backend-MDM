using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.DAL.DAL.SubscribeDALTableAdapters;
/// <summary>
/// Summary description for SubscribeBAL
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.BAL
{
    public class SubscribeBAL
    {
        
        tblSubscriptionTableAdapter sub;
        tblSubscriptionDtlTableAdapter subdtl;
        string _RequestDateTime, _CategoryIdList, _ProductKey;
        int _NoOfUsers, _Duration, _TotalAmount, _UserId, _ClientId, _NoOfLicense;
        double _TotalPaid, _SmsCost;

        public string Address { get; set; }
        public int Durations { get; set; }
        public int Noofemployee { get; set; }
        public string EmailId { get; set; }
        public int Categoryid { get; set; }
        public string AppliedDateTime { get; set; }
        public string ExpiryDateTime { get; set; }
     

        public string CategoryIdList
        {
            get { return _CategoryIdList; }
            set { _CategoryIdList = value; }
        }        
        public string RequestDateTime
        {
            get { return _RequestDateTime; }
            set { _RequestDateTime = value; }
        }
        public int NoOfUsers
        {
            get { return _NoOfUsers; }
            set { _NoOfUsers = value; }
        }
        public int Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }
        public int TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public int NoOfLicense
        {
            get { return _NoOfLicense; }
            set { _NoOfLicense = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public string ProductKey
        {
            get { return _ProductKey; }
            set { _ProductKey = value; }
        }
        public double TotalPaid
        {
            get { return _TotalPaid; }
            set { _TotalPaid = value; }
        }
        public double SmsCost
        {
            get { return _SmsCost; }
            set { _SmsCost = value; }
        }

        public string categoryDuration { get; set; }
        public string categoryNoOfLicense { get; set; }
        public string CategoryTotalAmount { get; set; }
        public string PricePerUnit { get; set; }

        public SubscribeBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int ChkIFFeatureIsEnableAccordingDate(int featureId)
        {
            sub = new tblSubscriptionTableAdapter();
            return Convert.ToInt32(sub.ChkIFFeatureIsEnableAccordingDate(_UserId, _RequestDateTime, featureId).ToString());
        }       
        public DataTable GetAppliedSubscriptionDtl()
        {
            sub = new tblSubscriptionTableAdapter();
            return sub.GetAppliedSubscription(_ClientId);
        }
        public DataTable GetSubscriptionByClientId()
        {
            sub = new tblSubscriptionTableAdapter();
            return sub.GetSubscriptionByClientId(_ClientId);
        }
        public DataTable GetActiveSolutions()
        {
            subdtl = new tblSubscriptionDtlTableAdapter();
            return subdtl.GetActiveSolutions(_ClientId);
        }
        public DataTable GetAppliedActiveSolutions()
        {
            subdtl = new tblSubscriptionDtlTableAdapter();
            return subdtl.GetAppliedActiveSolutions(_ClientId);
        }

        public DataTable GetSubscriptionDtlData()
        {
            subdtl = new tblSubscriptionDtlTableAdapter();
            return subdtl.GetSubscriptionDtlData();
        }
        public int InsertSubscriptionDtls(int IsInMonth = 1)
        {
            sub = new tblSubscriptionTableAdapter();
            return sub.InsertSubscriptionDtl(_CategoryIdList, _ClientId, _NoOfLicense, _Duration, _TotalAmount, TotalPaid, _ProductKey, _UserId, SmsCost, IsInMonth);
        }
        public int InsertSubscriptionDtls()
        {
            sub = new tblSubscriptionTableAdapter();
            //return sub.InsertSubscriptionDtl(_CategoryIdList, _ClientId, _NoOfLicense, _Duration, _TotalAmount, TotalPaid, _ProductKey, _UserId, SmsCost, IsInMonth);
            string OrderNo = DateTime.UtcNow.AddMinutes(330).ToString("ddmmyyyyHHmmssffff");
            PaymentNewBAL PayBAL = new PaymentNewBAL();
            PayBAL.ClientId = _ClientId;
            PayBAL.UserID = _UserId;
            PayBAL.EmailId = EmailId;
            PayBAL.city = "";
            PayBAL.address1 = Address;
            PayBAL.pincode = "";
            PayBAL.OrderNo = OrderNo;

            PayBAL.ProductKey = _ProductKey;
            PayBAL.categoryIdList = _CategoryIdList;
            PayBAL.categoryDuration = categoryDuration;
            PayBAL.categoryNoofLicense = categoryNoOfLicense;
            PayBAL.CategoryTotalAmount = CategoryTotalAmount;
            PayBAL.PricePerUnit = PricePerUnit;

            PayBAL.total = "0.00";
            PayBAL.promocode = "";
            PayBAL.discountedAmount = "0.00";
            PayBAL.SubTotal = "0.00";
            PayBAL.IsTrail = 1;
            PayBAL.statusMessage = "success";
            PayBAL.igst = "0.00";
            PayBAL.cgst = "0.00";
            PayBAL.sgst = "0.00";
            PayBAL.stateId = 35;
            PayBAL.GSTNo = "";

            PayBAL.conformationemail = EmailId;
            return PayBAL.insertPayOrder1();

        }
        public int SubcribtionCheckandInsert()
        {
            sub = new tblSubscriptionTableAdapter();
            return Convert.ToInt32(sub.SubscribeInsert(Durations, Noofemployee, EmailId, Categoryid, Convert.ToDateTime(AppliedDateTime), Convert.ToDateTime(ExpiryDateTime)));
        }
        public int DisableProfileFeature()
        {
            subdtl = new tblSubscriptionDtlTableAdapter();
            return Convert.ToInt32(subdtl.DisableProfileFeature(_ClientId));
        }
    }
}
