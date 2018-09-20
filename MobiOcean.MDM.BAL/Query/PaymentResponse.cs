using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Web.UI;
using HtmlAgilityPack;
using iTextSharp.tool.xml;
using Newtonsoft.Json;

/// <summary>
/// Summary description for PaymentResponse
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.Query
{
    public class PaymentResponse
    {
        SubscribeBAL Subscribe;
        PaymentBAL PayBAL;
        int NoOfSolution = 0;
        string Message = "";
        DataTable dt, dt1;
        SendMailBAL send;
        LomentAPI lomapi;
        public string discountedprice;
        public string categoryIdList;

        public PaymentResponse()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string City { get; set; }
        public string ConfirmMail { get; set; }
        public string Pincode { get; set; }
        public int[] CategoryIds { get; set; }
        public Nullable<int> Duration { get; set; }
        public Nullable<int> NoOfLicense { get; set; }
        public Nullable<double> DNAmount { get; set; }
        public Nullable<double> ServiceTax { get; set; }
        public Nullable<double> SMSCost { get; set; }
        public Nullable<double> SubTotal { get; set; }       
        public string Remark { get; set; }
        public string PaymentType { get; set; }
        public string Transcationno { get; set; }
        public int StateId { get; set; }
        public Nullable<double> CGST { get; set; }
        public Nullable<double> SGST { get; set; }
        public Nullable<double> IGST { get; set; }
        public string Address { get; set; }
        public string GSTNo { get; set; }

        //public void OfflinePaymentHandleNew()
        //{
        //        PaymentResponseNew paymentResponse = new PaymentResponseNew();
        //        paymentResponse.ClientId = ClientId;
        //        paymentResponse.UserId = UserId;
        //        paymentResponse.EmailId = lblEmailId.Text;
        //        paymentResponse.ConfirmMail = txtConfEmail.Text;
        //        paymentResponse.City = txtCity.Text;
        //        paymentResponse.Address = txtAddress.Text;
        //        paymentResponse.Pincode = lblPin.Text;

        //        paymentResponse.categoryIdList = ViewState["CategoryList"] != null ? ViewState["CategoryList"].ToString() : null;
        //        paymentResponse.categoryDuration = ViewState["CategoryDuration"] != null ? ViewState["CategoryDuration"].ToString() : null;
        //        paymentResponse.categoryNoOfLicense = ViewState["CategoryNoOfLicense"] != null ? ViewState["CategoryNoOfLicense"].ToString() : null;
        //        paymentResponse.CategoryTotalAmount = ViewState["CategoryTotalAmount"] != null ? ViewState["CategoryTotalAmount"].ToString() : null;
        //        paymentResponse.PricePerUnit = ViewState["PricePerUnit"] != null ? ViewState["PricePerUnit"].ToString() : null;


        //        paymentResponse.TotalAmount = Convert.ToDouble(ViewState["TotalAmount"]);
        //        paymentResponse.PromoCode = txtpromocode.Text;
        //        paymentResponse.discountedprice = lbldiscountedprice.Text;
        //        paymentResponse.SubTotal = Convert.ToDouble(ViewState["SubTotal"]);

        //        paymentResponse.StateId = Convert.ToInt32(ViewState["StateId"].ToString());
        //        paymentResponse.CGST = string.IsNullOrEmpty(lblCGST.Text) ? 0 : Convert.ToDouble(lblCGST.Text);
        //        paymentResponse.SGST = string.IsNullOrEmpty(lblSGST.Text) ? 0 : Convert.ToDouble(lblSGST.Text);
        //        paymentResponse.IGST = string.IsNullOrEmpty(lblIGST.Text) ? 0 : Convert.ToDouble(lblIGST.Text);
        //        paymentResponse.GSTNo = txtGSTNo.Text;
        //        paymentResponse.IsTrail = 0;

        //        int res = paymentResponse.OrderInsert("Pending");
        //        if (res > 0)
        //        {
        //            GoPayment(res.ToString());//current method used for payment
        //        }
        //        else
        //        {
        //            lblmsg.Text = "Something went wrong Please try again";
        //            lblmsg.ForeColor = System.Drawing.Color.Red;
        //        }
            
        //}
        public string OfflinePaymentHandle()
        {
            try
            {
                int res = OrderInsert(2, string.Join(",", CategoryIds), "Pending");
                if (res > 0)
                {
                    Random rnd = new Random();
                    string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                    string txnid = strHash.ToString().Substring(0, 20);
                    res = PaymentInsert(PaymentType, "success", txnid, SubTotal.ToString(), "MobiOcean", UserName, "", "", "", City, "", "", Pincode, EmailId, MobileNo, res.ToString(), SMSCost.ToString(), Remark, "", "", "Offline", Transcationno, "", "");
                    if (res > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "False";
                    }

                }
                else
                {
                    return "False";
                }
            }
            catch (Exception)
            {
                return "False";
            }
        }
        private string GenProductKey()
        {
            Random myrandom = new Random();
            int Size = 25;
            string input = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ9876543210";
            string pass = "";
            try
            {

                StringBuilder builder = new StringBuilder();
                char ch;
                for (int i = 0; i < Size; i++)
                {
                    ch = input[myrandom.Next(0, input.Length)];
                    builder.Append(ch);
                }
                pass = builder.ToString();
            }
            catch (Exception)
            {
                int myNum = myrandom.Next(10000000, 99999999);
                pass = "btyutyuuyerewvb" + myNum + "yhdguyfgd";
            }

            return pass;
        }
        private void CallLomentAPI(Dictionary<string, string> lomData, int userId, string duration)
        {
            dt = new DataTable();
            PayBAL = new PaymentBAL();
            try
            {
                PayBAL.userid = userId;
                dt = PayBAL.CheckLomentUser();
                if (dt.Rows.Count > 0)
                {
                    CallLomentBuyLicenseAPI(lomData, dt, duration);
                }
                else
                {
                    UserBAL user = new UserBAL();
                    user.UserId = Convert.ToInt32(userId);
                    dt = user.GetUserDtlByUserId();
                    lomapi.username = dt.Rows[0]["UserName"].ToString();
                    lomapi.password = dt.Rows[0]["Password"].ToString();
                    lomapi.primary_email = dt.Rows[0]["EmailId"].ToString();                    
                    lomapi.isadmin = 1;
                    lomapi.clientid = ClientId;
                    lomapi.userid = userId;
                    lomapi.Postdata = Encoding.UTF8.GetBytes(WebUtility.UrlDecode("name=" + dt.Rows[0]["UserName"].ToString() + "&password=" + dt.Rows[0]["Password"].ToString() + "&primary_email=" + dt.Rows[0]["EmailId"].ToString() + "&primary_mobile_number=91" + dt.Rows[0]["[MobileNo"].ToString() + "&country_abbrev=IN" + "&partner_id=8"));
                    lomapi.RegisterCompanyAdmin();
                    CallLomentBuyLicenseAPI(lomData, dt, duration);
                }
            }
            catch
            {
            }
        }
        private void CallLomentBuyLicenseAPI(Dictionary<string, string> lomData, DataTable dt, string duration)
        {
            lomapi = new LomentAPI();
            lomapi.username = dt.Rows[0]["EmailId"].ToString();
            lomapi.Postdata = Encoding.UTF8.GetBytes(WebUtility.UrlDecode("subscription_start_date=" + DateTime.UtcNow.AddMinutes(Constant.addMinutes).ToString("yyyy-MM-dd") + "&subscription_end_date=" + DateTime.UtcNow.AddMinutes(Constant.addMinutes).AddMonths(Convert.ToInt32(duration)).ToString("yyyy-MM-dd") + "&purchase_data=" +JsonConvert.SerializeObject(lomData) + "&type=P"));
            lomapi.userid = Convert.ToInt32(dt.Rows[0]["UserId"]);
            lomapi.clientid = Convert.ToInt32(dt.Rows[0]["ClientId"]);
            lomapi.noofusers = NoOfSolution;
            string res = lomapi.BuyLicense();
        }
        private string Generatehash512(string text)
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
        private string FixBrokenMarkup(string broken)
        {
            HtmlDocument h = new HtmlDocument()
            {
                OptionAutoCloseOnEnd = true,
                OptionFixNestedTags = true,
                OptionWriteEmptyNodes = true
            };
            h.LoadHtml(broken);

            // UPDATED to remove HtmlCommentNode
            var comments = h.DocumentNode.SelectNodes("//comment()");
            if (comments != null)
            {
                foreach (var node in comments) { node.Remove(); }
            }

            return h.DocumentNode.SelectNodes("child::*") != null
                //                            ^^^^^^^^^^
                // XPath above: string plain-text or contains markup/tags
                ? h.DocumentNode.WriteTo()
                : string.Format("<span>{0}</span>", broken);
        }

        public int OrderInsert(int RoleId, string categoryIdList, string statusMessage)
        {
            try
            {
                //UserID,  RoleId, EmailId, OrderNo, categoryIdList, txtDuration, txtNoofLicense, txtSubTotal, statusMessage, total, conformationemail, SmsCost, stateId, city, pincode, discountedAmount, cgst, sgst, igst));
                string OrderNo = DateTime.UtcNow.AddMinutes(330).ToString("ddmmyyyyHHmmssffff");
                PayBAL = new PaymentBAL();
                PayBAL.ClientId = ClientId;
                PayBAL.UserID = UserId;
                PayBAL.RoleId = RoleId;
                PayBAL.EmailId = EmailId;
                PayBAL.OrderNo = OrderNo;
                PayBAL.categoryIdList = categoryIdList;
                PayBAL.txtDuration = Duration.ToString();
                PayBAL.txtNoofLicense = (int)NoOfLicense;
                PayBAL.txtSubTotal = SubTotal.ToString();
                PayBAL.SmsCost = Convert.ToDouble(SMSCost);
                PayBAL.statusMessage = statusMessage;
                PayBAL.total = DNAmount.ToString();
                PayBAL.discountedAmount = discountedprice;
                PayBAL.igst = IGST.ToString();
                PayBAL.cgst = CGST.ToString();
                PayBAL.sgst = SGST.ToString();
                PayBAL.stateId = StateId;
                PayBAL.city = City;
                PayBAL.address1 = Address;
                PayBAL.GSTNo = GSTNo;
                if (string.IsNullOrEmpty(ConfirmMail))
                {
                    PayBAL.conformationemail = EmailId;
                }
                else
                {
                    PayBAL.conformationemail = ConfirmMail;
                }
                int res = PayBAL.insertPayOrder();
                if (res > 0)
                {
                    return res;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int PaymentInsert(string paymentMode, string paymentStatus, string txnId, string amount, string Product, string firstName, string lastName, string address1,
            string address2, string city, string state, string country, string zipcode, string email, string phone, string OrderId, string smsCost, string udf3, string hash,
            string error, string PG_TYPE, string bank_ref_num, string payuMoneyId, string additionalCharges)
        {
            dt = new DataTable();
            PayBAL = new PaymentBAL();

            #region --- Assign Variable----
            PayBAL.mode = paymentMode;
            PayBAL.status1 = paymentStatus;
            PayBAL.txnid = txnId;
            PayBAL.amount = amount;
            PayBAL.productinfo = Product;
            PayBAL.firstname = firstName;
            PayBAL.lastname = lastName;
            PayBAL.address1 = address1;
            PayBAL.address2 = address2;
            PayBAL.city = city;
            PayBAL.state = state;
            PayBAL.country = country;
            PayBAL.zipcode = zipcode;
            PayBAL.email = email;
            PayBAL.phone = phone;
            PayBAL.udf1 = OrderId;
            PayBAL.udf2 = smsCost;
            PayBAL.udf3 = udf3;
            PayBAL.hash = hash;
            PayBAL.Error = error;
            PayBAL.PG_TYPE = PG_TYPE;
            PayBAL.bank_ref_num = bank_ref_num;
            PayBAL.payuMoneyId = payuMoneyId;
            try
            {
                PayBAL.additionalCharges = additionalCharges;
            }
            catch { }
            PayBAL.cid = ClientId;
            PayBAL.userid = UserId;
            #endregion
            string res = PayBAL.insertPaymentdtltoPayUMoney();
            if (res == "success")
            {
                PayBAL.order_id = OrderId;
                PayBAL.order_status = paymentStatus;
                dt = PayBAL.GetPaymentdtlandUpdate();
                return ResponseHandle(dt, firstName + " " + lastName, email, phone, paymentMode);
            }
            else
            {
                return 0;
            }

        }


        public int ResponseHandle(DataTable dt, string userName, string IEmail, string Iphone, string paymentMode)
        {
            try
            {
                string productkey = "", discountedprice = "", smscost = "";
                discountedprice = string.IsNullOrEmpty(dt.Rows[0]["DiscountedPrice"].ToString()) ? "0.0" : dt.Rows[0]["DiscountedPrice"].ToString();
                smscost = string.IsNullOrEmpty(dt.Rows[0]["SmsCost"].ToString()) ? "0.0" : dt.Rows[0]["SmsCost"].ToString();
                DateTime CurrentDateTime = DateTime.UtcNow.AddMinutes(Constant.addMinutes);
                NoOfSolution = Convert.ToInt32(dt.Rows[0]["NoofLicense"]);
                Subscribe = new SubscribeBAL();
                Subscribe.NoOfLicense = NoOfSolution;
                Subscribe.CategoryIdList = dt.Rows[0]["CategoryIdList"].ToString();
                Subscribe.ClientId = ClientId;
                Subscribe.Duration = Convert.ToInt32(dt.Rows[0]["Duration"]);
                productkey = GenProductKey();
                Subscribe.ProductKey = productkey;
                Subscribe.TotalPaid = Convert.ToDouble(dt.Rows[0]["SubTotal"].ToString());
                Subscribe.SmsCost = Convert.ToDouble(smscost);
                Subscribe.UserId = UserId;
                int res1 = Subscribe.InsertSubscriptionDtls(1);
                if (res1 > 0)
                {
                    PayBAL = new PaymentBAL();
                    PayBAL.productkey = productkey;
                    string Invoice = PayBAL.GetInvoiceNoFromProductKey();
                    dt1 = PayBAL.FeatureList();
                    string[] catid = Subscribe.CategoryIdList.Split(',');
                    string categorylist = "";
                    int count = 0, j = 0;
                    DataTable dttemp = new DataTable();
                    dttemp.Columns.AddRange(new DataColumn[4] {
                         new DataColumn("SNo"),
                          new DataColumn("CategoryName"),
                                new DataColumn("HSN"),
                                new DataColumn("Price")});
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        foreach (string a in catid)
                        {
                            if (dt1.Rows[i]["CategoryId"].ToString() == a)
                            {
                                categorylist += dt1.Rows[i]["CategoryName"].ToString() + ",";
                                count++;
                                j++;
                                dttemp.Rows.Add(j, dt1.Rows[i]["CategoryName"].ToString(), "998319 (Other information technology services n.e.c )", dt1.Rows[i]["CloudPrice"].ToString());
                                continue;
                            }
                        }
                    }
                    categorylist = categorylist.TrimEnd(',');
                    string[] CName = categorylist.Split(',');

                    #region --- Create Mail----
                    Message = Message + ("<table width='65%' cellspacing='0' cellpadding='0' border=1 style=' background-color:#F1F1F1; font-size: 10px;'>");

                    Message = Message + ("<tr  style='font-size: 12px;'>");

                    Message = Message + ("<td align='center'>");
                    Message = Message + ("<b>Order No</b>");
                    Message = Message + ("</td>");
                    Message = Message + ("<td align='center'>");
                    Message = Message + (dt.Rows[0]["OrderNo"].ToString());
                    Message = Message + ("</td>");

                    Message = Message + ("</tr>");
                    Message = Message + ("<tr  style='font-size: 12px;'>");

                    Message = Message + ("<td align='center'>");
                    Message = Message + ("<b>Categories</b>");
                    Message = Message + ("</td>");
                    Message = Message + ("<td align='center'>");
                    Message = Message + (categorylist);
                    Message = Message + ("</td>");

                    Message = Message + ("</tr>");
                    Message = Message + ("<tr  style='font-size: 12px;'>");

                    Message = Message + ("<td align='center'>");
                    Message = Message + ("<b>Duration</b>");
                    Message = Message + ("</td>");
                    Message = Message + ("<td align='center'>");
                    Message = Message + (dt.Rows[0]["Duration"].ToString());
                    Message = Message + ("</td>");

                    Message = Message + ("</tr>");
                    Message = Message + ("<tr  style='font-size: 12px;'>");

                    Message = Message + ("<td align='center'>");
                    Message = Message + ("<b>Number of License</b>");
                    Message = Message + ("</td>");
                    Message = Message + ("<td align='center'>");
                    Message = Message + (dt.Rows[0]["NoofLicense"].ToString());
                    Message = Message + ("</td>");

                    Message = Message + ("</tr>");
                    Message = Message + ("<tr  style='font-size: 12px;'>");

                    Message = Message + ("<td align='center'>");
                    Message = Message + ("<b>Total Amount</b>");
                    Message = Message + ("</td>");
                    Message = Message + ("<td align='center'>");
                    Message = Message + (dt.Rows[0]["SubTotal"].ToString());
                    Message = Message + ("</td>");

                    Message = Message + ("</tr>");
                    Message = Message + ("<tr  style='font-size: 12px;'>");

                    Message = Message + ("<td align='center'>");
                    Message = Message + ("<b>Order Status</b>");
                    Message = Message + ("</td>");
                    Message = Message + ("<td align='center'>");
                    Message = Message + (dt.Rows[0]["statusMessage"].ToString());
                    Message = Message + ("</td>");

                    Message = Message + ("</tr>");
                    Message = Message + ("<tr  style='font-size: 14px;'>");

                    Message = Message + ("<td align='center'>");
                    Message = Message + ("<b>Product Key</b>");
                    Message = Message + ("</td>");
                    Message = Message + ("<td align='center'>");
                    Message = Message + (productkey);
                    Message = Message + ("</td>");

                    Message = Message + ("</tr>");

                    Message = Message + ("</table>");
                    #endregion
                    send = new SendMailBAL();
                    byte[] bytes = CreatePDF(dt.Rows[0]["City"].ToString(), dt.Rows[0]["Address"].ToString(), dt.Rows[0]["GSTNo"].ToString(), userName, IEmail, Iphone, paymentMode, discountedprice, smscost, CurrentDateTime, Invoice, catid.Count().ToString(), dt.Rows[0]["Duration"].ToString(), dt.Rows[0]["NoofLicense"].ToString(), dt.Rows[0]["Total_Price"].ToString(), dt.Rows[0]["CGST"].ToString(), dt.Rows[0]["SGST"].ToString(), dt.Rows[0]["IGST"].ToString(), dt.Rows[0]["SubTotal"].ToString(), dttemp);
                    try
                    {
                        if (dt.Rows[0]["EmailId"].ToString() == dt.Rows[0]["conformationemail"].ToString())
                        {
                            send.NewMobiOrderMail(dt.Rows[0]["EmailId"].ToString(), Message, userName, bytes);
                            //send.NewMobiOrderMail1("anupriya@mobi-move.com", Message, Session["UserName"].ToString(), bytes);
                        }
                        else
                        {
                            send.NewMobiOrderMail(dt.Rows[0]["EmailId"].ToString(), Message, userName, bytes);
                            send.NewMobiOrderMail(dt.Rows[0]["conformationemail"].ToString(), Message, userName, bytes);
                            //send.NewMobiOrderMail1("anupriya@mobi-move.com", Message, Session["UserName"].ToString(), bytes);
                        }
                    }
                    catch { }
                    Dictionary<string, string> LomData = new Dictionary<string, string>();
                    string[] featurids = dt.Rows[0]["CategoryIdList"].ToString().Split(',');
                    foreach (string obj in featurids)
                    {
                        if (!string.IsNullOrEmpty(obj) && (obj == "13" || obj == "14" || obj == "15"))
                        {
                            switch (obj)
                            {
                                case "13":
                                    LomData.Add("1", NoOfSolution.ToString());
                                    break;
                                case "14":
                                    LomData.Add("2", NoOfSolution.ToString());
                                    break;
                                case "15":
                                    LomData.Add("3", NoOfSolution.ToString());
                                    break;
                            }

                        }
                    }
                    if (LomData.Count > 0)
                    {
                        if (!LomData.Keys.Contains("1"))
                        {
                            LomData.Add("1", "0");
                        }
                        if (!LomData.Keys.Contains("2"))
                        {
                            LomData.Add("2", "0");
                        }
                        if (!LomData.Keys.Contains("3"))
                        {
                            LomData.Add("3", "0");
                        }
                        CallLomentAPI(LomData, UserId, dt.Rows[0]["Duration"].ToString());
                    }
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public byte[] CreatePDF(string City, string Address, string GSTNo1, string userName, string IEmail, string Iphone, string paymentMode, string discountedprice, string smscost, DateTime CurrentDateTime, string Invoice, string count, string Duration, string NoOfLicense1, string Total_Price, string CGST1, string SGST1, string IGST1, string SubTotal, DataTable dttemp)
        {
            #region ---New Invoice Content---
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html>");
            sb.Append("<html lang='en'>");
            sb.Append("<head>");
            sb.Append("<meta charset='utf-8' />");
            sb.Append("<meta http-equiv='X-UA-Compatible' content='IE=edge' />");
            sb.Append("<meta name='viewport' content='width=device-width, initial-scale=1' />");
            sb.Append("<meta name='description' content=''><meta name='author' content='' />");
            sb.Append("<title>" + (!string.IsNullOrWhiteSpace(Invoice) ? "Tax Invoice" : "Performa Invoice") + "</title></head><body>");
            sb.Append("<table style='padding:0px 30px 30px 30px;font-size:16px;color:#555;width: 100%;' align='center'>");
            sb.Append("<tr><td>");
            sb.Append("<table border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#FFFFFF' style='border-radius: 5px;width: 100%;'><tbody>");
            sb.Append("<tr><td> <table border='0' align='center' cellpadding='0' cellspacing='0' style='width: 100 %;'><tr><td valign='top' style='color:#404041;line-height:16px;padding:25px 20px 15px 20px'>");
            sb.Append("<p><h2 align='center'><span> " + (!string.IsNullOrWhiteSpace(Invoice) ? "Tax Invoice" : "Performa Invoice") + "</span></h2></p>");
            sb.Append("</td></tr></table></td></tr>");
            sb.Append("<tr><td><br/>  <table align='left' cellpadding='0' cellspacing='0' style='border:1px solid #ccc;width: 100%;'>");
            sb.Append("<tr><td>");
            sb.Append(" <table class='table table-bordered custom-table' style='border-collapse:collapse;width: 100%;'><tbody>");
            sb.Append("<tr><td style='padding:8px;'><h4><b> Mobiocean Mobility Software Solutions LLP </b></h4><p> A - 12, THIRD FLOOR MOHAN COOPERATIVE INDUSTRIAL ESTATE, MATHURA ROAD NEW DELHI - 110044 <br> GSTIN / UIN:07ABEFM3211G1ZP </p></td></tr> ");
            sb.Append("<tr><td style='border-top:1px solid #ccc;padding:8px;'>");
            sb.Append("<h4 style='margin:0px;'>" + userName + "</h4>");
            sb.Append("<p style='margin:0px;'><b>" + IEmail + "</b></p>");
            sb.Append("<p style='margin:0px;'>" + Iphone + "</p>");
            sb.Append("<p style='margin:0px;'>" + City + "</p>");
            sb.Append("<p style='margin:0px;'>" + Address + "</p>");
            sb.Append("</td></tr></tbody></table></td>");
            sb.Append("<td><table class='table table-bordered custom-table' style='border-left:1px solid #ccc;border-collapse:collapse;width: 100%;'><tbody>");
            if (!string.IsNullOrWhiteSpace(Invoice))
            {
                sb.Append("<tr><td style='border-bottom:1px solid #ccc;border-right:1px solid #ccc;padding:8px;'>Invoice No.:</td>");
                sb.Append("<td style='border-bottom:1px solid #ccc;padding:8px;'>" + Invoice + "</td>");
                sb.Append("</tr>");
            }
            if (!string.IsNullOrWhiteSpace(GSTNo1))
            {
                sb.Append("<tr><td style='border-bottom:1px solid #ccc;border-right:1px solid #ccc;padding:8px;'>GSTIN/UIN:</td>");
                sb.Append("<td style='border-bottom:1px solid #ccc;padding:8px;'>" + GSTNo1 + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("<tr><td style='border-bottom:1px solid #ccc;border-right:1px solid #ccc;padding:8px;'>Dated:</td>");
            sb.Append("<td style='border-bottom:1px solid #ccc;padding:8px;'>" + CurrentDateTime + "</td>");
            sb.Append("</tr>");
            if (!string.IsNullOrWhiteSpace(Invoice))
            {
                sb.Append("<tr><td style='border-bottom:1px solid #ccc;border-right:1px solid #ccc;padding:8px;'>Mode/Terms of Payment</td>");
                sb.Append("<td style='border-bottom:1px solid #ccc;padding:8px;'>" + paymentMode + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("<tr><td colspan='2' style='height:125px; padding:8px;'>Terms of Delivery : As per the MobiOcean T&C </td>");
            sb.Append("</tr></tbody></table></td></tr></table></td></tr> ");

            sb.Append("<tr><td valign='top' style='color:#404041;font-size:12px;line-height:16px;'><br/>");
            sb.Append("<table cellpadding='5' style='border:1px solid #ccc;border-collapse:collapse;width: 100%;'>");
            sb.Append("<tr style='border:1px solid #ccc;background:#ddd;'>");
            sb.Append("<th style='border:1px solid #ccc;padding:5px;font-weight:bold;text-align:center'>S.No</th>");
            sb.Append("<th style='border:1px solid #ccc;padding:5px;font-weight:bold;text-align:center'>Description of Goods</th>");
            sb.Append("<th style='border:1px solid #ccc;padding:5px;font-weight:bold;text-align:center'>HSN/SAC</th>");
            sb.Append("<th style='border:1px solid #ccc;padding:5px;font-weight:bold;text-align:center'>Price(Rs.)/Person/Day</th>");
            sb.Append("</tr>");
            foreach (DataRow row in dttemp.Rows)
            {
                sb.Append("<tr style='border:1px solid #ccc;padding:5px;text-align:center;'>");
                foreach (DataColumn column in dttemp.Columns)
                {
                    sb.Append("<td style='border:1px solid #ccc;padding:5px;text-align:center;'>");
                    sb.Append(row[column]);
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table></td></tr>");

            sb.Append("<tr style=''><td> <br/><table align='center' cellpadding='0' cellspacing='0' style='width: 100%;'>");
            sb.Append("<tr><td style='color:#404041;font-size:12px;line-height:16px;padding:10px 25px 20px 18px'>");

            sb.Append("<table cellpadding='0' cellspacing='0' class='' style='width:100%'><tbody><tr class='information'> <td colspan='2'> <b>Company's Bank Details : </b></td></tr><tr class=''> <td>&nbsp;</td> </tr><tr class='heading'> <td> Bank Name</td><td>: <b> ICICI BANK LTD </b></td> </tr> ");
            sb.Append("<tr class='details'> <td> A/c No.</td><td>: <b> 003105030453 </b></td> </tr> ");
            sb.Append("<tr class='heading'> <td> Branch  </td><td>: <b> SECTOR-18, NOIDA (U.P)</b></td> </tr></tbody>");
            sb.Append("<tr class='details'> <td> IFS Code</td><td>: <b> <p> ICICI0000031 </b></td> </tr> ");
            sb.Append("</table>");
            sb.Append("</td><td style='color:#404041;font-size:12px;line-height:16px;padding:10px 20px 20px 18px'><table border='0' cellpadding='0' cellspacing='0' style='width:100%'><tbody>");
            sb.Append("<tr><td class='text-right' colspan='3' style='padding:1px; text-align:left;'>No. Of Solutions</td>");
            sb.Append("<th class='text-right' style='padding:1px; text-align:left;'>:&nbsp;" + count + "</th>");
            sb.Append("</tr>");
            sb.Append("<tr><td class='text-right' colspan='3' style='padding:1px; text-align:left;'>Duration(In Month)</td>");
            sb.Append("	<th class='text-right' style='padding:1px; text-align:left;'>:&nbsp;" + Convert.ToInt32(Duration) + " </th>");
            sb.Append("</tr>");
            sb.Append("<tr><td class='text-right' colspan='3' style='padding:1px; text-align:left;'>No. Of License(S)</td>");
            sb.Append("	<th class='text-right' style='padding:1px; text-align:left;'>:&nbsp;" + NoOfLicense1 + "</th>");
            sb.Append("</tr>");
            sb.Append("<tr><td class='text-right' colspan='3' style='padding:1px; text-align:left;'>Price(Rs.)</td>");
            sb.Append("<th class='text-right' style='padding:1px; text-align:left;'>:&nbsp;" + Total_Price + "</th>");
            sb.Append("</tr>");
            sb.Append("<tr><td class='text-right' colspan='3' style='padding:1px; text-align:left;'>Discount(Rs.)</td>");
            sb.Append("<th class='text-right' style='padding:1px; text-align:left;'>:&nbsp;" + discountedprice + "</th>");
            sb.Append("</tr>");
            sb.Append("<tr><td class='text-right' colspan='3' style='padding:1px; text-align:left;'>SMS Cost(Rs.)</td>");
            sb.Append("<th class='text-right' style='padding:1px; text-align:left;'>:&nbsp;" + smscost + "</th>");
            sb.Append("</tr>");
            if (!string.IsNullOrEmpty(CGST1))
            {
                sb.Append("<tr><td class='text-right' colspan='3' style='padding:1px; text-align:left;'>CGST(Rs.)- @9%</td>");
                sb.Append("<th class='text-right' style='padding:1px; text-align:left;'>:&nbsp;" + CGST1 + "</th>");
                sb.Append("</tr>");
                sb.Append("<tr><td class='text-right' colspan='3' style='padding:1px; text-align:left;'>SGST(Rs.)- @9%</td>");
                sb.Append("<th class='text-right' style='padding:1px; text-align:left;'>:&nbsp;" + SGST1 + "</th>");
                sb.Append("</tr>");
            }
            else
            {
                sb.Append("<tr><td class='text-right' colspan='3' style='padding:1px; text-align:left;'>IGST(Rs.)- @18%</td>");
                sb.Append("<th class='text-right' style='padding:1px; text-align:left;'>:&nbsp;" + IGST1 + "</th>");
                sb.Append("</tr>");
            }
            sb.Append("<tr><td class='text-right' colspan='3' style='padding:1px; text-align:left;'>Sub Total(Rs.)</td>");
            sb.Append("<th class='text-right' style='padding:1px; text-align:left;'>:&nbsp;" + SubTotal + "</th>");
            sb.Append("</tr></tbody>");
            sb.Append("</table></td></tr></table></td></tr>");
            sb.Append("<tr><td><table border='0' align='center' cellpadding='0' cellspacing='0' style='color:#404041;font-size:12px;line-height:16px;padding:10px 16px 20px 18px;text-align:right;border-bottom:1px solid #ccc;width: 100%;'><tr><td><br />E. & O.E </td></tr></table></td></tr>");
            sb.Append("<tr><td></br>");

            sb.Append("<table border='0' align='center' cellpadding='0' cellspacing='0' style='width: 100 %;'><tr><td><table border='0' cellspacing='0' cellpadding='0' style='width:100%'><tbody> <tr>");

            sb.Append("<td style='color:#404041;font-size:12px;line-height:16px;padding:15px 5px 5px 10px'><p class='custom-declare'>");
            sb.Append("<b>Declaration:</b></p><p>We declare that this " + (!string.IsNullOrWhiteSpace(Invoice) ? "invoice" : "performa invoice") + " shows the actual price of the goods described and that all particulars are true and correct.</p>");
            sb.Append("</td>");
            sb.Append("  </tr></tbody></table>");

            sb.Append("</td><td><table cellspacing='5' cellpadding='5' style='border: 1px solid #ccc;width:100%'><tbody><tr>");
            sb.Append("<td style='color:#404041;font-size:12px;line-height:16px;'><p style='margin:0px;text-align:right;'><b>for Mobiocean Mobility Software Solutions LLP </b></p><br> <p style=text-align:right> Authorised Signatory </p></td></tr></tbody></table></td>");
            sb.Append("</tr></table>");

            sb.Append("</td></tr><tr> <td><table width='100%' border='0' align='center' cellpadding='0' cellspacing='0' style='color:#404041;font-size:12px;line-height:16px;padding:10px 16px 20px 18px;text-align:center;border-bottom:1px solid #ccc;'><tr><td><br /><hr/>SUBJECT TO DELHI JURISDICTION</td></tr> </table> </td> </tr>");
            if (!string.IsNullOrWhiteSpace(Invoice))
            {
                sb.Append("<tr> <td><table width='100%' border='0' align='center' cellpadding='0' cellspacing='0' style='color:#404041;font-size:12px;line-height:16px;padding:10px 16px 20px 18px;text-align:center;border-bottom:1px solid #ccc;'><tr><td><br />This is a Computer Generated Invoice</td></tr> </table> </td> </tr>");
            }
            sb.Append("</tbody></table></td></tr> </table>");
            sb.Append("</body></html>");

                    //StringReader sr = new StringReader(sb.ToString());

            var fixedMarkup = FixBrokenMarkup(sb.ToString());

            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                #region-- Add watermark--
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Watermark writerEvent = new Watermark("MobiOcean");

                writer.PageEvent = writerEvent;
                #endregion

                document.Open();
                using (var stringReader = new StringReader(fixedMarkup))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);
                }
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                return bytes;
            }
            #endregion
        }
    }
}
