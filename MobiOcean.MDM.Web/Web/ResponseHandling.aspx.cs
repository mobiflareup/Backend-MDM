using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web.Web
{
    public partial class ResponseHandling : System.Web.UI.Page
    {

        int ClientId, UserId, RoleId;

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                Session["result"] = "Failure";
                ClientId = Convert.ToInt32(Session["ClientId"].ToString());
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                RoleId = Convert.ToInt32(Session["Role"].ToString());
                string merc_hash_string = string.Empty;
                string test = Request.Form["status"];
                if (Request.Form["status"] == "success")
                {
                    string[] merc_hash_vars_seq = Constant.hashSequence.Split('|'); // spliting hash sequence from co
                    Array.Reverse(merc_hash_vars_seq);
                    merc_hash_string = Constant.SALT + "|" + Request.Form["status"];

                    foreach (string merc_hash_var in merc_hash_vars_seq)
                    {
                        merc_hash_string += "|";
                        merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");

                    }
                    merc_hash_string = Generatehash512(merc_hash_string).ToLower();

                    string hashstring = Request.Form["hash"];

                    if (merc_hash_string != Request.Form["hash"])
                    {
                        //Value didn't match that means some paramter value change between transaction 
                        //Response.Write("Hash value did not matched");
                        Response.Redirect("IsPayementResult.aspx");

                    }
                    else
                    {
                        //if hash value match for before transaction data and after transaction data
                        //that means success full transaction  , see more in response
                        //  InsertPayment();
                        string additionalCharges = "";
                        PaymentResponse paymentResponse = new PaymentResponse();
                        paymentResponse.ClientId = ClientId;
                        paymentResponse.UserId = UserId;
                        try
                        {
                            additionalCharges = Request.Form["additionalCharges"].ToString();
                        }
                        catch { }
                        int res = paymentResponse.PaymentInsert(Request.Form["mode"].ToString(), Request.Form["status"].ToString(), Request.Form["txnid"].ToString(), Request.Form["amount"].ToString(),
                          Request.Form["productinfo"].ToString(), Request.Form["firstname"].ToString(), Request.Form["lastname"].ToString(), Request.Form["address1"].ToString(), Request.Form["address2"].ToString(), Request.Form["city"].ToString(), Request.Form["state"].ToString(), Request.Form["country"].ToString(), Request.Form["zipcode"].ToString(), Request.Form["email"].ToString(), Request.Form["phone"].ToString(), Request.Form["udf1"].ToString(), Request.Form["udf2"].ToString(), Request.Form["udf3"].ToString(), Request.Form["hash"].ToString(), Request.Form["Error"].ToString(), Request.Form["PG_TYPE"].ToString(), Request.Form["bank_ref_num"].ToString(), Request.Form["payuMoneyId"].ToString(), additionalCharges);

                        if (res > 0)
                        {
                            Session["result"] = "Success";
                            Response.Redirect("IsPayementResult.aspx");
                        }
                        else
                        {
                            RedirectToFail();
                        }
                    }

                }

                else
                {
                    Response.Redirect("IsPayementResult.aspx");

                }
            }
            catch (Exception)
            {
                Response.Redirect("IsPayementResult.aspx");
            }
        }
        /// <summary>
        /// Generate HASH for encrypt all parameter passing while transaction
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }
        private void RedirectToFail()
        {
            Session["result"] = "Failure";
            Response.Redirect("IsPayementResult.aspx");
        }

    }
}
