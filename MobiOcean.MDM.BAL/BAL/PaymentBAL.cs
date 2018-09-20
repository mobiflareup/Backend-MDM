using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using MobiOcean.MDM.DAL.DAL.PaymentDALTableAdapters;
/// <summary>
/// Summary description for PaymentBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class PaymentBAL
    {
        DataTable dt;
        public int ClientId { get; set; }
        public int RoleId { get; set; }
        public int DeptId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public int UserID { get; set; }
        public string OrderNo { get; set; }
        public string categoryIdList { get; set; }
        public string txtDuration { get; set; }
        public int txtNoofLicense { get; set; }
        public string txtSubTotal { get; set; }
        public string statusMessage { get; set; }
        public string discountedAmount { get; set; }
        public int stateId { get; set; }
        public string cgst { get; set; }
        public string sgst { get; set; }
        public string igst { get; set; }
        public string pincode { get; set; }
        //CCAvenenu
        public string order_id { get; set; }
        public string tracking_id { get; set; }
        public string bank_ref_no { get; set; }
        public string order_status { get; set; }
        public string failure_message { get; set; }
        public string payment_mode { get; set; }
        public string card_name { get; set; }
        public string status_code { get; set; }
        public string status_message { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public string billing_name { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public string billing_state { get; set; }
        public string billing_zip { get; set; }
        public string billing_country { get; set; }
        public string billing_tel { get; set; }
        public string billing_email { get; set; }
        public string delivery_name { get; set; }
        public string delivery_address { get; set; }
        public string delivery_city { get; set; }
        public string delivery_state { get; set; }
        public string delivery_zip { get; set; }
        public string delivery_country { get; set; }
        public string delivery_tel { get; set; }
        public string merchant_param1 { get; set; }
        public string merchant_param2 { get; set; }
        public string merchant_param3 { get; set; }
        public string merchant_param4 { get; set; }
        public string merchant_param5 { get; set; }
        public string vault { get; set; }
        public string offer_type { get; set; }
        public string offer_code { get; set; }
        public string discount_value { get; set; }
        public string mer_amount { get; set; }
        public string eci_value { get; set; }
        public string retry { get; set; }
        public string response_code { get; set; }
        public string total { get; set; }
        public int cid { get; set; }
        public int userid { get; set; }
        public int status { get; set; }
        public string conformationemail { get; set; }
        public string promocode { get; set; }
        public double SmsCost { get; set; }

        //PayUMoney
        public string mode { get; set; }
        public string txnid { get; set; }
        public string productinfo { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string hash { get; set; }
        public string Error { get; set; }
        public string PG_TYPE { get; set; }
        public string bank_ref_num { get; set; }
        public string payuMoneyId { get; set; }
        public string additionalCharges { get; set; }
        public string status1 { get; set; }
        public string productkey { get; set; }
        public string GSTNo { get; set; }

        tblPaymentOrderTableAdapter payorder;
        tblPayementTableAdapter pay;
        tblFeatureCategoryTableAdapter feature;        
        LomentUserTableAdapter lta;
        PromoCodeTableAdapter pcode;
        tblPayementPayUMoneyTableAdapter payuMoney;

        public PaymentBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int insertPayOrder()
        {
            try
            {
                payorder = new tblPaymentOrderTableAdapter();
                int res = Convert.ToInt32(payorder.InsertPaymentOrder(UserID, ClientId, RoleId, EmailId, OrderNo, categoryIdList, txtDuration, txtNoofLicense, txtSubTotal, statusMessage, total, conformationemail, SmsCost, stateId, city, address1, discountedAmount, cgst, sgst, igst,GSTNo));
                return res;
            }
            catch(Exception)
            {
                return 0;
            }
        }
      

        


        public string insertPaymentdtl()
        {
            try
            {
                pay = new tblPayementTableAdapter();
                string res = pay.InsertPayment(order_id, tracking_id, bank_ref_no, order_status, failure_message, payment_mode, card_name, status_code, status_message, currency, amount, billing_name, billing_address, billing_city, billing_state, billing_zip, billing_country, billing_tel, billing_email, delivery_name, delivery_address, delivery_city, delivery_state, delivery_zip, delivery_country, delivery_tel, merchant_param1, merchant_param2, merchant_param3, merchant_param4, merchant_param5, vault, offer_type, offer_code, discount_value, mer_amount, eci_value, retry, response_code, cid, userid).ToString();
                return res;
            }
            catch
            {
                return "Catch";
            }
        }
        public string insertPaymentdtltoPayUMoney()
        {
            try
            {
                payuMoney = new tblPayementPayUMoneyTableAdapter();
                string res = payuMoney.InsertPaymentToPayUMoney(mode, status1, txnid, amount, productinfo, firstname, lastname, address1, address2, city, state, country, zipcode, email, phone, udf1, udf2, udf3, hash, Error, PG_TYPE, bank_ref_num, payuMoneyId, additionalCharges, cid, userid).ToString();
                return res;
            }
            catch
            {
                return "Catch";
            }
        }
        public DataTable insertPayOrderOffline()
        {
            dt = new DataTable();
            try
            {
                payorder = new tblPaymentOrderTableAdapter();
                dt = payorder.InsertAndGetPaymentOrderDetails(UserID, ClientId, RoleId, EmailId, OrderNo, categoryIdList, txtDuration, txtNoofLicense, txtSubTotal, statusMessage, total, conformationemail, SmsCost, stateId, cgst, sgst, igst);
                return dt;
            }
            catch
            {
                return dt;
            }
        }       
        public DataTable GetPaymentdtlandUpdate()
        {
            dt = new DataTable();
            try
            {
                payorder = new tblPaymentOrderTableAdapter();
                dt = payorder.UpdateandGetOrderdetails(Convert.ToInt32(order_id), order_status);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetOrderdtlbyOrderId()
        {
            dt = new DataTable();
            try
            {
                payorder = new tblPaymentOrderTableAdapter();
                dt = payorder.GetOrderDtlByOrderId(Convert.ToInt32(order_id));
                return dt;
            }
            catch
            {
                return dt;
            }
        }
        public DataTable FeatureList()
        {
            dt = new DataTable();
            try
            {
                feature = new tblFeatureCategoryTableAdapter();
                dt = feature.GetData();
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public DataTable CheckLomentUser()
        {
            dt = new DataTable();
            try
            {
                lta = new LomentUserTableAdapter();
                dt = lta.CheckLomentUser(userid);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public DataTable GetPromoCodeDtl()
        {
            pcode = new PromoCodeTableAdapter();
            return pcode.GetPromoCodeDtl(promocode);
        }
        public int GetTotalSubscriptionByClientId()
        {
            pcode = new PromoCodeTableAdapter();
            return Convert.ToInt32(pcode.GetTotalSubscriptionByClientId(ClientId));
        }
        public string GetInvoiceNoFromProductKey()
        {
            payorder = new tblPaymentOrderTableAdapter();
            return payorder.GetInvoiceNoFromProductKey(productkey);
        }
        
    }
}
