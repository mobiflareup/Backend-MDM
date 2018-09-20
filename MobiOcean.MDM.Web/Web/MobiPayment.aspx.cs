using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public partial class MobiPayment : Page
    {
        DateTime CurrentDateTime = DateTime.UtcNow.AddMinutes(Constant.addMinutes);
        int ClientId, UserId, RoleId, DeptId, Pclientid;
        UserBAL user;
        FeatureBAL ftrBal;
        DataTable dt, dt1;
        PaymentBAL PayBAL;
        ClientBAL client;
        GingerboxSrch search;
        string fromdate = "", todate = "";
        string solnid = "", minamt = "", PEmailId = "", TermsAndConditions = "";
        string[] cid = { };
        int nooflicense = 0, noofduration = 0;
        int isminamt = 0, isdiscount = 0, issoln = 0, islicense = 0, isduration = 0, isnewuser = 0, count = 0;
        double totalamt = 0;
        int discount = 0;
        dynamic obj;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["ClientId"] == null || Session["UserId"] == null || Session["Role"] == null)
            {
                if (Request.Params["Payment"] != null)
                {
                    Session["MOOrderDetail"] = Request.Params["Payment"].ToString();
                }
                Response.Redirect(Constant.MobiURL + "login.php");
            }
            else
            {
                ClientId = Convert.ToInt32(Session["ClientId"].ToString());
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                RoleId = Convert.ToInt32(Session["Role"].ToString());
                DeptId = Convert.ToInt32(Session["DeptId"].ToString());
                if (!IsPostBack)
                {
                    dt = new DataTable();
                    user = new UserBAL();
                    user.UserId = UserId;
                    dt = user.GetUserDtlByUserId();
                    if (dt.Rows.Count > 0)
                    {
                        lblEmailId.Text = dt.Rows[0]["EmailId"].ToString();
                        lblUserName.Text = dt.Rows[0]["UserName"].ToString();
                        LoginUser.Text = dt.Rows[0]["UserName"].ToString();
                        lblContNo.Text = dt.Rows[0]["MobileNo"].ToString();
                        lblAddr.Text = dt.Rows[0]["PermanentAddress"].ToString();
                        lblPin.Text = dt.Rows[0]["PinCode"].ToString();
                    }
                    try
                    {
                        if (Request.Params["Payment"] != null)
                        {
                            obj = JsonConvert.DeserializeObject(Request.Params["Payment"].ToString());
                            ViewState["PostData"] = Request.Params["Payment"].ToString();
                            //lbltest.Text = Request.Params["Payment"].ToString();
                        }
                        else if (Session["MOOrderDetail"] != null)
                        {
                            string data = Session["MOOrderDetail"].ToString();
                            obj = JsonConvert.DeserializeObject(data);
                            ViewState["PostData"] = data;
                            Session["MOOrderDetail"] = null;
                        }
                        else
                        {
                            string data = ViewState["PostData"].ToString();
                            obj = JsonConvert.DeserializeObject(data);
                            //ViewState["PostData"] = data;
                        }

                        BindGrid(obj);
                        BindStates();
                        Calculate();
                    }
                    catch (Exception ex) { }
                }

                //lblMsg.Text = "";

            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //Set a name for the form
            string formID = "MobiPayment";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + Constant.MobiURL + "cloud-managed" +
                           "\" method=\"POST\">");
            //string datatopost = ViewState["PostData"].ToString();
            strForm.Append("<input type=\"hidden\" name=\"Payment" +
                           "\" value=\"" + Server.HtmlEncode((ViewState["PostData"].ToString())) + "\">");

            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            Page.Controls.Add(new LiteralControl(strForm.ToString() + strScript.ToString()));
        }
        protected void BindGrid(dynamic obj)
        {
            search = new GingerboxSrch();
            string CategoryList = "", SCategoryList = "", CategoryDuration = "", CategoryNoOfLicense = "", CategoryTotalAmount = "", PricePerUnit = "";
            foreach (var obj1 in obj)
            {
                if (obj1.Features.FeatureIds != null)
                {
                    CategoryList = CategoryList + obj1.Features.FeatureIds.ToString() + ",";
                }
            }
            dt = search.GetSelectedCategoryByClientId(ClientId, CategoryList.TrimEnd(','));
            double TotalAmount = 0;
            DataTable data = new DataTable();
            data.Columns.AddRange(new DataColumn[] { new DataColumn("CategoryId"), new DataColumn("CategoryName"), new DataColumn("Price"), new DataColumn("License"), new DataColumn("Duration"), new DataColumn("TotalPrice") });
            foreach (var obj1 in obj)
            {
                int i = 0;

                if (obj1.Id.ToString() == "1" && obj1.Features.FeatureIds != null)
                {

                    CategoryList = obj1.Features.FeatureIds.ToString();
                    string[] Quantity = obj1.Features.Quantity != null ? obj1.Features.Quantity.ToString().Split(',') : null;
                    string[] Duration = !string.IsNullOrWhiteSpace(obj1.Features.Duration.ToString()) ? obj1.Features.Duration.ToString().Split(',') : null;
                    foreach (var cat in CategoryList.Split(','))
                    {
                        DataRow foundRows;
                        foundRows = dt.Select("CategoryId = " + cat).FirstOrDefault();
                        if (foundRows != null)
                        {
                            string duration = Duration != null ? Duration[i] : "0";
                            string license = Quantity != null ? Quantity[i] : "0";
                            SCategoryList += "," + cat;
                            CategoryDuration += "," + duration;
                            CategoryNoOfLicense += "," + license;

                            double amount = Convert.ToDouble(foundRows["Price"]);
                            if (duration != "0")
                            {
                                amount = amount * Convert.ToDouble(duration) * 30;
                            }
                            if (license != "0")
                            {
                                amount = amount * Convert.ToDouble(license);
                            }
                            string typeofcost = obj1.Id.ToString() == "1" ? " /License/Day" : obj1.Id.ToString() == "2" ? " /License/Month" : " /Unit";
                            PricePerUnit += "," + Convert.ToDouble(foundRows["Price"]).ToString("F") + typeofcost;

                            data.Rows.Add(foundRows["CategoryId"], foundRows["CategoryName"], Convert.ToDouble(foundRows["Price"]).ToString("F") + typeofcost, license == "0" ? "NA" : license, duration == "0" ? "NA" : duration, amount.ToString("F"));
                            TotalAmount += amount;
                            CategoryTotalAmount += "," + amount;
                        }
                        i++;
                    }

                }
                else
                // if (obj1.Id.ToString() == "2")
                {
                    if (obj1.Features.FeatureIds != null)
                    {
                        i = 0;
                        CategoryList = obj1.Features.FeatureIds.ToString();
                        string[] Quantity = obj1.Features.Quantity != null ? obj1.Features.Quantity.ToString().Split(',') : null;
                        string[] Duration = !string.IsNullOrWhiteSpace(obj1.Features.Duration.ToString()) ? obj1.Features.Duration.ToString().Split(',') : null;
                        foreach (var cat in CategoryList.Split(','))
                        {
                            DataRow foundRows;
                            foundRows = dt.Select("CategoryId = " + cat).FirstOrDefault();
                            if (foundRows != null)
                            {
                                string duration = Duration != null ? Duration[i] : "0";
                                string license = Quantity != null ? Quantity[i] : "0";
                                SCategoryList += "," + cat;
                                CategoryDuration += "," + duration;
                                CategoryNoOfLicense += "," + license;

                                double amount = Convert.ToDouble(foundRows["Price"]);
                                if (duration != "0")
                                {
                                    amount = amount * Convert.ToDouble(duration);
                                }
                                if (license != "0")
                                {
                                    amount = amount * Convert.ToDouble(license);
                                }
                                string typeofcost = obj1.Id.ToString() == "1" ? " /License/Day" : obj1.Id.ToString() == "2" ? " /License/Month" : " /Unit";
                                PricePerUnit += "," + Convert.ToDouble(foundRows["Price"]).ToString("F") + typeofcost;

                                data.Rows.Add(foundRows["CategoryId"], foundRows["CategoryName"], Convert.ToDouble(foundRows["Price"]).ToString("F") + typeofcost, license == "0" ? "NA" : license, duration == "0" ? "NA" : duration, amount.ToString("F"));
                                TotalAmount += amount;
                                CategoryTotalAmount += "," + amount;
                            }
                            i++;
                        }
                    }
                    //}
                }
            }
            lbltotalprice.Text = TotalAmount.ToString("F");
            ViewState["TotalAmount"] = TotalAmount.ToString("F");
            ViewState["CategoryList"] = SCategoryList.TrimStart(',');
            ViewState["CategoryDuration"] = CategoryDuration.TrimStart(',');
            ViewState["CategoryNoOfLicense"] = CategoryNoOfLicense.TrimStart(',');
            ViewState["CategoryTotalAmount"] = CategoryTotalAmount.TrimStart(',');
            ViewState["PricePerUnit"] = PricePerUnit.TrimStart(',');
            ViewState["data"] = data;
            grdPayment.DataSource = data;
            grdPayment.DataBind();
        }
        protected void BindStates()
        {
            client = new ClientBAL();
            ListItem li = new ListItem("--- Select State ---", "0");
            ddlState.Items.Clear();
            ddlState.Items.Add(li);
            ddlState.DataSource = client.GetAllStates();
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
        }

        protected void btnProcessToPayment_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            if (!string.IsNullOrEmpty(txtpromocode.Text))
            {
                if (ViewState["promo"] == null)
                {
                    lblmsg.Text = "Click Apply Button to apply promocode";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            if (lblmsg.Text == "")
            {
                PaymentResponseNew paymentResponse = new PaymentResponseNew();
                paymentResponse.ClientId = ClientId;
                paymentResponse.UserId = UserId;
                paymentResponse.EmailId = lblEmailId.Text;
                paymentResponse.ConfirmMail = txtConfEmail.Text;
                paymentResponse.City = txtCity.Text;
                paymentResponse.Address = txtAddress.Text;
                paymentResponse.Pincode = lblPin.Text;

                paymentResponse.categoryIdList = ViewState["CategoryList"] != null ? ViewState["CategoryList"].ToString() : null;
                paymentResponse.categoryDuration = ViewState["CategoryDuration"] != null ? ViewState["CategoryDuration"].ToString() : null;
                paymentResponse.categoryNoOfLicense = ViewState["CategoryNoOfLicense"] != null ? ViewState["CategoryNoOfLicense"].ToString() : null;
                paymentResponse.CategoryTotalAmount = ViewState["CategoryTotalAmount"] != null ? ViewState["CategoryTotalAmount"].ToString() : null;
                paymentResponse.PricePerUnit = ViewState["PricePerUnit"] != null ? ViewState["PricePerUnit"].ToString() : null;


                paymentResponse.TotalAmount = Convert.ToDouble(ViewState["TotalAmount"]);
                paymentResponse.PromoCode = txtpromocode.Text;
                paymentResponse.discountedprice = lbldiscountedprice.Text;
                paymentResponse.SubTotal = Convert.ToDouble(ViewState["SubTotal"]);

                paymentResponse.StateId = Convert.ToInt32(ViewState["StateId"].ToString());
                paymentResponse.CGST = string.IsNullOrEmpty(lblCGST.Text) ? 0 : Convert.ToDouble(lblCGST.Text);
                paymentResponse.SGST = string.IsNullOrEmpty(lblSGST.Text) ? 0 : Convert.ToDouble(lblSGST.Text);
                paymentResponse.IGST = string.IsNullOrEmpty(lblIGST.Text) ? 0 : Convert.ToDouble(lblIGST.Text);
                paymentResponse.GSTNo = txtGSTNo.Text;
                paymentResponse.IsTrail = 0;

                int res = paymentResponse.OrderInsert("Pending");
                if (res > 0)
                {
                    GoPayment(res.ToString());//current method used for payment
                }
                else
                {
                    lblmsg.Text = "Something went wrong Please try again";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("" + Constant.MobiURL + "/login.php");
        }
        protected void btnProcessToPaymentcancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/setting.aspx");
        }
        protected void btnapply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpromocode.Text))
            {
                lblMessage.Text = "Please Enter Promo Code";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                ViewState["promo"] = txtpromocode.Text;
                lblmsg.Text = "";
                PayBAL = new PaymentBAL();
                PayBAL.promocode = txtpromocode.Text.Trim();
                dt = PayBAL.GetPromoCodeDtl();
                ViewState["PromocodeDtls"] = dt;
                if (dt.Rows.Count > 0)
                {
                    PEmailId = dt.Rows[0]["EmailId"].ToString();
                    Pclientid = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    issoln = Convert.ToInt16(string.IsNullOrEmpty(dt.Rows[0]["IsSolutions"].ToString()) ? "0" : dt.Rows[0]["IsSolutions"].ToString());
                    solnid = dt.Rows[0]["SolutionIds"].ToString();
                    cid = dt.Rows[0]["CategoryIds"].ToString().Split(',');
                    isminamt = Convert.ToInt32(string.IsNullOrEmpty(dt.Rows[0]["isMinBillAmount"].ToString()) ? "0" : dt.Rows[0]["isMinBillAmount"].ToString());
                    minamt = dt.Rows[0]["MinBillAmount"].ToString();
                    noofduration = Convert.ToInt16(string.IsNullOrEmpty(dt.Rows[0]["Duration"].ToString()) ? "0" : dt.Rows[0]["Duration"].ToString());
                    isduration = Convert.ToInt16(string.IsNullOrEmpty(dt.Rows[0]["IsDuration"].ToString()) ? "0" : dt.Rows[0]["IsDuration"].ToString());
                    islicense = Convert.ToInt16(string.IsNullOrEmpty(dt.Rows[0]["IsLicense"].ToString()) ? "0" : dt.Rows[0]["IsLicense"].ToString());
                    nooflicense = Convert.ToInt16(string.IsNullOrEmpty(dt.Rows[0]["NoofLicense"].ToString()) ? "0" : dt.Rows[0]["NoofLicense"].ToString());
                    isdiscount = Convert.ToInt16(string.IsNullOrEmpty(dt.Rows[0]["IsDiscountInPercentage"].ToString()) ? "0" : dt.Rows[0]["IsDiscountInPercentage"].ToString());
                    fromdate = dt.Rows[0]["ValidFrom"].ToString();
                    todate = dt.Rows[0]["ValidTo"].ToString();
                    isnewuser = Convert.ToInt16(string.IsNullOrEmpty(dt.Rows[0]["IsForNewUser"].ToString()) ? "0" : dt.Rows[0]["IsForNewUser"].ToString());
                    totalamt = Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                    discount = Convert.ToInt32(dt.Rows[0]["Discount"].ToString());
                    TermsAndConditions = dt.Rows[0]["TermsAndCondition"].ToString();
                    ViewState["TermsAndCondition"] = TermsAndConditions;
                    ViewState["IsDuration"] = isduration;
                    ViewState["IsLicense"] = islicense;
                    ViewState["IsMinAmt"] = isminamt;
                    ViewState["IsNewUser"] = isnewuser;
                    ViewState["noofduration"] = noofduration;
                    ViewState["nooflicense"] = nooflicense;
                    ViewState["minamt"] = minamt;
                    ViewState["isdiscount"] = isdiscount;
                    ViewState["discount"] = discount;
                    ViewState["totalamt"] = totalamt;
                    ViewState["CategoryId"] = dt.Rows[0]["CategoryIds"].ToString();
                    CheckConditions(dt);
                }
                else
                {
                    lblMessage.Text = "Invalid Promo code";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    imgapplied.Visible = false;
                    lnkbtnview.Visible = false;
                    imgnotapplied.Visible = false;
                }
            }
        }
        protected string CheckConditions(DataTable dt)
        {
            int res = 0;
            if (CurrentDateTime.Date >= Convert.ToDateTime(fromdate) && CurrentDateTime.Date <= Convert.ToDateTime(todate))
            {
                if (isnewuser == 1)
                {
                    count = CheckNewUser();
                    if (count == 0)
                    {
                        res = ApplyAndCalculatePrice();
                    }
                }
                else
                {
                    res = ApplyAndCalculatePrice();
                }
                if (res > 0)
                {
                    lblMessage.Text = "This promo code is applied Successfully, click details icon to view details";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    btnapply.Visible = false;
                    imgapplied.Visible = true;
                    imgnotapplied.Visible = false;
                    lnkbtnview.Visible = true;
                    txtpromocode.Enabled = false;
                    lnkbtnremove.Visible = true;
                }
                else
                {
                    lblMessage.Text = "This promo code is not applied, click details icon to view details";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    imgnotapplied.Visible = true;
                    btnapply.Visible = true;
                    lnkbtnview.Visible = true;
                    ViewState["isdiscount"] = null;
                }
            }
            else
            {
                lblMessage.Text = "Promo code has Expired.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                imgnotapplied.Visible = false;
                btnapply.Visible = true;
                lnkbtnview.Visible = false;
                imgnotapplied.Visible = false;
                ViewState["isdiscount"] = null;
            }
            return lblMessage.Text;
        }
        protected void lnkbtnview_Click(object sender, EventArgs e)
        {
            string categoryname = "";
            ftrBal = new FeatureBAL();
            ftrBal.cid = ViewState["CategoryId"].ToString();
            dt = ftrBal.GetSelectedCategory();
            foreach (DataRow obj in dt.Rows)
            {
                categoryname += obj["CategoryName"].ToString().Trim() + ",";
            }
            lbltermsandcondition.Text = "* " + ViewState["TermsAndCondition"].ToString();
            lblpromocodedtl.Text = "This Promo Code is applied if it satisfy the following - ";
            lblCategories.Text = categoryname.TrimEnd(',');
            if (ViewState["isdiscount"].ToString() == "1")
            {
                lbldiscount.Text = ViewState["discount"].ToString() + "% OFF";
                lbldiscountamount.Text = lbldiscount.Text;
            }
            else
            {
                lbldiscount.Text = ViewState["totalamt"].ToString() + "Rs. OFF";
                lbldiscountamount.Text = lbldiscount.Text;
            }
            if (ViewState["IsNewUser"].ToString() == "1")
            {
                newuser.Visible = true;
                lblisnewuser.Text = "Applicable only for new users";
            }
            if (ViewState["IsDuration"].ToString() == "1")
            {
                duration.Visible = true;
                lblnoofdurations.Text = "Minimum " + ViewState["noofduration"] + " months ";
            }
            if (ViewState["IsLicense"].ToString() == "1")
            {
                licenses.Visible = true;
                lblnooflicenses.Text = "Minimum " + ViewState["nooflicense"].ToString() + " Licenses";
            }
            if (ViewState["IsMinAmt"].ToString() == "1")
            {
                minimumamt.Visible = true;
                lblminimumamount.Text = ViewState["minamt"].ToString();
            }
            mp1.Show();
        }
        public int GetConditions()
        {
            bool flag = true;
            string data = ViewState["PostData"].ToString();
            obj = JsonConvert.DeserializeObject(data);
            string CategoryIds = "", duration = "", license = "";
            foreach (var obj1 in obj)
            {
                if (obj1.Id.ToString() == "1")
                {
                    if (obj1.Features.FeatureIds != null)
                    {
                        CategoryIds = obj1.Features.FeatureIds.ToString();
                        duration = obj1.Features.License.ToString();
                        license = obj1.Features.Duration.ToString();
                    }
                    break;
                }
            }
            int k = 0;
            CategoryIds = CategoryIds.TrimEnd(',');
            string[] categoryid = CategoryIds.Split(',');
            if (issoln == 1 && flag)
            {
                foreach (var id in cid)
                {
                    if (CategoryIds.Contains(id))
                    {
                        k++;
                    }
                }
                if (k != cid.Count())
                {
                    flag = false;
                }
            }
            if (isduration == 1 && flag)
            {
                if (Convert.ToInt32(duration) < noofduration)
                {
                    flag = false;
                }
            }
            if (islicense == 1 && flag)
            {
                if (Convert.ToInt32(license) < nooflicense)
                {
                    flag = false;
                }
            }
            if (isminamt == 1)
            {
                if (Convert.ToDouble(ViewState["TotalAmount"].ToString()) < Convert.ToDouble(minamt))
                {
                    flag = false;
                }
            }
            return flag ? 1 : 0;
        }
        public int CheckNewUser()
        {
            PayBAL = new PaymentBAL();
            PayBAL.ClientId = ClientId;
            return count = PayBAL.GetTotalSubscriptionByClientId();
        }
        public int CheckIsApplicable()
        {
            if (Pclientid == 0)
            {
                if (PEmailId == lblEmailId.Text)
                {
                    return 1;
                }
            }
            else if (Pclientid == -1)
            {
                return 1;
            }
            else if (Pclientid == ClientId)
            {
                return 1;
            }
            return 0;
        }
        public int ApplyAndCalculatePrice()
        {
            if (CheckIsApplicable() == 1)
            {
                if (GetConditions() == 1)
                {
                    Calculate();
                    return 1;
                }
            }
            return 0;
        }
        private void Calculate()
        {
            int StateId = 0;
            if (ViewState["StateId"] != null)
            {
                StateId = Convert.ToInt32(ViewState["StateId"].ToString());
            }

            double discountPrice = 0, subtotal = 0, amt = 0;
            amt = Convert.ToDouble(ViewState["TotalAmount"]);

            if (ViewState["isdiscount"] != null && ViewState["isdiscount"].ToString() == "1")
            {
                discountPrice = ((amt * Convert.ToDouble(ViewState["discount"].ToString())) / 100);
                dcprice.Visible = true;
                lbldiscountedprice.Text = discountPrice.ToString("F");
            }
            if (ViewState["isdiscount"] != null && ViewState["isdiscount"].ToString() == "0")
            {
                discountPrice = Convert.ToDouble(ViewState["totalamt"].ToString());
                dcprice.Visible = true;
                lbldiscountedprice.Text = discountPrice.ToString("F");
            }
            if (StateId > 0)
            {
                if (Constant.isservicetax == 1)
                {
                    double cgst = 0, sgst = 0, igst = 0;
                    if (StateId == Constant.SID)
                    {
                        cgst = (((amt - Convert.ToDouble(discountPrice))) * Constant.CGST) / 100;
                        sgst = (((amt - Convert.ToDouble(discountPrice))) * Constant.SGST) / 100;
                        lblCGST.Text = cgst.ToString("F");
                        lblSGST.Text = sgst.ToString("F");
                        Div1.Visible = true;
                        Div2.Visible = true;
                    }
                    else
                    {
                        igst = (((amt - Convert.ToDouble(discountPrice))) * Constant.IGST) / 100;
                        lblIGST.Text = igst.ToString("F");
                        Div3.Visible = true;
                    }
                    subtotal = amt + cgst + sgst + igst - discountPrice;
                }
            }
            else
            {
                subtotal = amt - discountPrice;
                labelservicetax.Visible = false;
                lblservicetax.Visible = false;
            }
            lbltotalprice.Text = amt.ToString("F");
            lblsubtotal.Text = subtotal.ToString("F");
            ViewState["SubTotal"] = subtotal.ToString("F");
        }

        protected void lnkbtnremove_Click(object sender, EventArgs e)
        {
            ViewState["isdiscount"] = null;
            txtpromocode.Text = "";
            txtpromocode.Enabled = true;
            lblMessage.Text = "";
            btnapply.Visible = true;
            dcprice.Visible = false;
            //lbldiscountedprice.Text = fa.ToString("F");
            Calculate();
            lnkbtnremove.Visible = false;
            imgapplied.Visible = false;
            lnkbtnview.Visible = false;
            imgnotapplied.Visible = false;
        }

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
        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            //Set a name for the form
            string formID = "MobiPayment";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }


            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
        private void GoPayment(string order_id)
        {
            Random rnd = new Random();
            string strHash = Generatehash512(rnd.ToString() + CurrentDateTime);
            string txnid = strHash.ToString().Substring(0, 20);
            string[] hashVarsSeq = Constant.hashSequence.Split('|'); // spliting hash sequence from config
            string hash_string = "";
            string[] Uname = lblUserName.Text.Split(' ');
            string AmountForm = Convert.ToDecimal(ViewState["SubTotal"]).ToString("g29");// eliminating trailing zeros
                                                                                         // string SName = ViewState["StateName"].ToString();

            foreach (string hash_var in hashVarsSeq)
            {
                switch (hash_var)
                {
                    case "key":
                        hash_string = hash_string + Constant.MERCHANT_KEY + "|";
                        break;
                    case "txnid":
                        hash_string = hash_string + txnid + "|";
                        break;
                    case "amount":
                        hash_string = hash_string + AmountForm + "|";
                        break;
                    case "productinfo":
                        hash_string = hash_string + "MobiOcean|";
                        break;
                    case "firstname":
                        hash_string = hash_string + Uname[0] + "|";
                        break;
                    case "email":
                        hash_string = hash_string + lblEmailId.Text + "|";
                        break;
                    case "udf1":
                        hash_string = hash_string + order_id + "|";
                        break;
                    default:
                        hash_string = hash_string + "|";
                        break;
                }
            }
            hash_string += Constant.SALT;// appending SALT
            hash_string = Generatehash512(hash_string).ToLower();         //generating hash

            if (!string.IsNullOrEmpty(hash_string))
            {
                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in hash table for data post
                data.Add("hash", hash_string);
                data.Add("key", Constant.MERCHANT_KEY);
                data.Add("txnid", txnid);
                data.Add("amount", AmountForm);
                data.Add("productinfo", "MobiOcean");
                data.Add("firstname", Uname[0]);
                data.Add("email", lblEmailId.Text);
                data.Add("phone", lblContNo.Text);
                data.Add("surl", Constant.PayUPaymentReturnURLNew);
                data.Add("furl", Constant.PayUPaymentReturnURLNew);
                data.Add("service_provider", "payu_paisa");
                data.Add("lastname", Uname.Length > 2 ? Uname[1] : "");
                data.Add("curl", "");
                data.Add("address1", lblAddr.Text);
                data.Add("address2", "");
                data.Add("city", "");
                data.Add("state", "");
                data.Add("country", "");
                data.Add("zipcode", "");
                data.Add("udf1", order_id);
                data.Add("udf3", "");
                data.Add("udf4", "");
                data.Add("udf5", "");
                data.Add("pg", "");

                string strForm = PreparePOSTForm(Constant.PAYU_BASE_URL + "/_payment", data);
                Page.Controls.Add(new LiteralControl(strForm));
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stateId = Convert.ToInt32(ddlState.SelectedValue.ToString());
            string stateName = ddlState.SelectedItem.Text;
            ViewState["StateId"] = stateId;
            ViewState["StateName"] = stateName;
            if (stateId == Constant.SID)
            {
                Div1.Visible = true;
                Div2.Visible = true;
                Div3.Visible = false;
            }
            else
            {
                Div1.Visible = false;
                Div2.Visible = false;
                Div3.Visible = true;
            }
            Calculate();
        }

        protected void imgDownloadPDF_Click(object sender, ImageClickEventArgs e)
        {
            PaymentResponseNew payRes = new PaymentResponseNew();
            dt1 = new DataTable();
            dt1 = (DataTable)ViewState["data"];
            DataTable dttemp = new DataTable();
            dttemp.Columns.AddRange(new DataColumn[] {
                         new DataColumn("SNo"), new DataColumn("CategoryName"), new DataColumn("HSN"), new DataColumn("Price"),  new DataColumn("License"), new DataColumn("Duration"),
                    new DataColumn("TotalPrice")});
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dttemp.Rows.Add(i + 1, dt1.Rows[i]["CategoryName"].ToString(), "998319 (Other information technology services n.e.c )", dt1.Rows[i]["Price"].ToString(), dt1.Rows[i]["License"].ToString() == "0" ? "NA" : dt1.Rows[i]["License"].ToString()
                       , dt1.Rows[i]["Duration"].ToString() == "0" ? "NA" : dt1.Rows[i]["Duration"].ToString(), dt1.Rows[i]["TotalPrice"].ToString());
            }
            byte[] pdfByte = payRes.CreatePDF(txtCity.Text, txtAddress.Text, txtGSTNo.Text, LoginUser.Text, lblEmailId.Text, lblContNo.Text, "ONLINE", string.IsNullOrWhiteSpace(lbldiscountedprice.Text) ? "0.00" : lbldiscountedprice.Text, "", lbltotalprice.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, lblsubtotal.Text, dttemp, DateTime.UtcNow.AddMinutes(Constant.addMinutes));
            Response.Clear();
            MemoryStream ms = new MemoryStream(pdfByte);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Performa_Invoice.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
        }

    }
}
