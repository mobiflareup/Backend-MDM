using CCA.Util;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class ccavResponseHandler : Base
    {

        PaymentBAL PayBAL;
        int ClientId, UserId, RoleId;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["result"] = "Failure";
                ClientId = Convert.ToInt32(Session["ClientId"].ToString());
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                RoleId = Convert.ToInt32(Session["Role"].ToString());

                CCACrypto ccaCrypto = new CCACrypto();
                string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], Constant.workingKey);
                NameValueCollection Params = new NameValueCollection();
                string[] segments = encResponse.Split('&');
                foreach (string seg in segments)
                {
                    string[] parts = seg.Split('=');
                    if (parts.Length > 0)
                    {
                        string Key = parts[0].Trim();
                        string Value = parts[1].Trim();
                        Params.Add(Key, Value);
                    }
                }
                InsertPayment(Params);
            }
            catch (Exception)
            {
                Response.Redirect("Web/IsPayementResult.aspx");
            }
        }
        private void RedirectToFail()
        {
            Session["result"] = "Failure";
            Response.Redirect("Web/IsPayementResult.aspx");
        }
        private void InsertPayment(NameValueCollection Params)
        {
            dt = new DataTable();
            string paystring = "";
            PayBAL = new PaymentBAL();

            #region --- Assign Variable----

            for (int i = 0; i < Params.Count; i++)
            {
                //Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
                paystring = Params.Keys[i].ToString();
                switch (paystring)
                {
                    case "billing_city":
                        PayBAL.billing_city = Params[i].ToString();
                        break;
                    case "tracking_id":
                        PayBAL.tracking_id = Params[i].ToString();
                        break;
                    case "order_id":
                        PayBAL.order_id = Params[i].ToString();
                        break;
                    case "order_status":
                        PayBAL.order_status = Params[i].ToString();
                        break;
                    case "bank_ref_no":
                        PayBAL.bank_ref_no = Params[i].ToString();
                        break;

                    case "failure_message":
                        PayBAL.failure_message = Params[i].ToString();
                        break;
                    case "payment_mode":
                        PayBAL.payment_mode = Params[i].ToString();
                        break;
                    case "card_name":
                        PayBAL.card_name = Params[i].ToString();
                        break;
                    case "status_code":
                        PayBAL.status_code = Params[i].ToString();
                        break;
                    case "status_message":
                        PayBAL.status_message = Params[i].ToString();
                        break;
                    case "currency":
                        PayBAL.currency = Params[i].ToString();
                        break;
                    case "amount":
                        PayBAL.amount = Params[i].ToString();
                        break;
                    case "billing_name":
                        PayBAL.billing_name = Params[i].ToString();
                        break;
                    case "billing_address":
                        PayBAL.billing_address = Params[i].ToString();
                        break;
                    case "billing_state":
                        PayBAL.billing_state = Params[i].ToString();
                        break;
                    case "billing_zip":
                        PayBAL.billing_zip = Params[i].ToString();
                        break;
                    case "billing_country":
                        PayBAL.billing_country = Params[i].ToString();
                        break;
                    case "billing_tel":
                        PayBAL.billing_tel = Params[i].ToString();
                        break;
                    case "billing_email":
                        PayBAL.billing_email = Params[i].ToString();
                        break;
                    case "delivery_name":
                        PayBAL.delivery_name = Params[i].ToString();
                        break;
                    case "delivery_address":
                        PayBAL.delivery_address = Params[i].ToString();
                        break;
                    case "delivery_city":
                        PayBAL.delivery_city = Params[i].ToString();
                        break;
                    case "delivery_state":
                        PayBAL.delivery_state = Params[i].ToString();
                        break;
                    case "delivery_zip":
                        PayBAL.delivery_zip = Params[i].ToString();
                        break;
                    case "delivery_country":
                        PayBAL.delivery_country = Params[i].ToString();
                        break;
                    case "delivery_tel":
                        PayBAL.delivery_tel = Params[i].ToString();
                        break;
                    case "merchant_param1":
                        PayBAL.merchant_param1 = Params[i].ToString();
                        break;
                    case "merchant_param2":
                        PayBAL.merchant_param2 = Params[i].ToString();
                        break;
                    case "merchant_param3":
                        PayBAL.merchant_param3 = Params[i].ToString();
                        break;
                    case "merchant_param4":
                        PayBAL.merchant_param4 = Params[i].ToString();
                        break;
                    case "merchant_param5":
                        PayBAL.merchant_param5 = Params[i].ToString();
                        break;
                    case "vault":
                        PayBAL.vault = Params[i].ToString();
                        break;
                    case "offer_type":
                        PayBAL.offer_type = Params[i].ToString();
                        break;
                    case "offer_code":
                        PayBAL.offer_code = Params[i].ToString();
                        break;
                    case "discount_value":
                        PayBAL.discount_value = Params[i].ToString();
                        break;
                    case "mer_amount":
                        PayBAL.mer_amount = Params[i].ToString();
                        break;
                    case "eci_value":
                        PayBAL.eci_value = Params[i].ToString();
                        break;
                    case "retry":
                        PayBAL.retry = Params[i].ToString();
                        break;
                    case "response_code":
                        PayBAL.response_code = Params[i].ToString();
                        break;

                }


            }
            #endregion
            PayBAL.cid = ClientId;
            PayBAL.userid = UserId;
            string res = PayBAL.insertPaymentdtl();
            if (res == "Success")
            {
                dt = PayBAL.GetPaymentdtlandUpdate();
                PaymentResponse paymentResponse = new PaymentResponse();
                paymentResponse.ClientId = ClientId;
                paymentResponse.UserId = UserId;

                paymentResponse.ResponseHandle(dt, PayBAL.billing_name, PayBAL.billing_email, PayBAL.billing_tel, PayBAL.payment_mode);
            }
            else
            {
                RedirectToFail();
            }

        }
    }
}
