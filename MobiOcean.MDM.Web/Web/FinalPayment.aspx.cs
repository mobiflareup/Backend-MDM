using CCA.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using System.IO;

public partial class Web_FinalPayment1 : Page
{
    DateTime CurrentDateTime = DateTime.UtcNow.AddMinutes(Constant.addMinutes);
    int ClientId, UserId, RoleId, DeptId, Pclientid;
    int i = 0, a = 0, b = 0;
    string CategoryIds;
    UserBAL user;
    FeatureBAL ftrBal;
    DataTable dt, dt1;
    PaymentBAL PayBAL;
    ClientBAL client;
    string fromdate = "", todate = "";
    string solnid = "", minamt = "", PEmailId = "", TermsAndConditions = "";
    string[] cid = { };
    int fid = 0, nooflicense = 0, noofduration = 0;
    int isminamt = 0, isdiscount = 0, issoln = 0, price = 0, islicense = 0, isduration = 0, isnewuser = 0, count = 0;
    double totalamt = 0, sa = 0, fa = 0, sms = 0;
    int discount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null || Session["ClientId"] == null || Session["UserId"] == null || Session["Role"] == null)
        {
            Response.Redirect(Constant.MobiURL);
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
                    lbllicense.Text = string.IsNullOrEmpty(Request.QueryString["No"].ToString()) ? "0" : Request.QueryString["No"].ToString();
                    //lbllicense.Text = "12";
                }
                catch (Exception)
                {
                    lbllicense.Text = "0";
                }
                try
                {
                    lblduration.Text = string.IsNullOrEmpty(Request.QueryString["Time"].ToString()) ? "0" : Request.QueryString["Time"].ToString();
                    //lblduration.Text = "6";
                }
                catch (Exception)
                {
                    lblduration.Text = "0";
                }
                try
                {
                    CategoryIds = string.IsNullOrEmpty(Request.QueryString["Ftr"].ToString()) ? "" : Request.QueryString["Ftr"].ToString();
                    //CategoryIds = "5,8,2,6,10";
                }
                catch (Exception)
                {
                    CategoryIds = "";
                }
                ViewState["CategoryIds"] = CategoryIds;
                BindGrid();
                BindStates();
                Calculate();
                Session["CloudPrice"] = "/web/FinalPayment.aspx?No=" + lbllicense.Text + "&Time=" + lblduration.Text + "&Ftr=" + CategoryIds + ";";
            }

            //lblMsg.Text = "";

        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constant.MobiURL + "cloud-management.php?No=" + lbllicense.Text + "&Time=" + lblduration.Text + "&Ftr=" + ViewState["CategoryIds"] + "&UserName=" + lblUserName.Text);
    }
    protected void BindGrid()
    {
        ftrBal = new FeatureBAL();
        ftrBal.cid = CategoryIds;
        dt = ftrBal.GetSelectedCategory();
        ViewState["data"] = dt;
        grdPayment.DataSource = dt;
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
        if (!string.IsNullOrEmpty(txtpromocode.Text))
        {
            if (ViewState["promo"] != null)
            {
                a = Convert.ToInt32(ViewState["promo"].ToString());
            }
            if (a == 0)
            {
                lblmsg.Text = "Click Apply Button to apply promocode";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        if (!string.IsNullOrEmpty(txtSmsCost.Text))
        {
            if (ViewState["add"] != null)
            {
                b = Convert.ToInt32(ViewState["add"].ToString());
            }
            if (b == 0)
            {
                lblmsg.Text = "Click Add Button To Add Sms Cost";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        CategoryIds = ViewState["CategoryIds"].ToString();
        if (CategoryIds.IndexOf(',') == 0)
            CategoryIds = CategoryIds.Substring(1);

        PaymentResponse paymentResponse = new PaymentResponse();
        paymentResponse.ClientId = ClientId;
        paymentResponse.UserId = UserId;
        paymentResponse.EmailId = lblEmailId.Text;
        paymentResponse.Duration = string.IsNullOrEmpty(lblduration.Text) ? 0 : Convert.ToInt32(lblduration.Text);
        paymentResponse.NoOfLicense = Convert.ToInt32(lbllicense.Text);
        paymentResponse.SubTotal = Convert.ToDouble(lblsubtotal.Text);
        paymentResponse.SMSCost = Convert.ToDouble(lblSmsCost.Text);
        paymentResponse.DNAmount = Convert.ToDouble(lbltotalprice.Text);
        paymentResponse.StateId = Convert.ToInt32(ViewState["StateId"].ToString());
        paymentResponse.CGST = string.IsNullOrEmpty(lblCGST.Text) ? 0 : Convert.ToDouble(lblCGST.Text);
        paymentResponse.SGST = string.IsNullOrEmpty(lblSGST.Text) ? 0 : Convert.ToDouble(lblSGST.Text);
        paymentResponse.IGST = string.IsNullOrEmpty(lblIGST.Text) ? 0 : Convert.ToDouble(lblIGST.Text);
        paymentResponse.City = txtCity.Text;
        paymentResponse.ConfirmMail = txtConfEmail.Text;
        paymentResponse.GSTNo = txtGSTNo.Text;
        paymentResponse.Address = txtAddress.Text;
        paymentResponse.discountedprice = lbldiscountedprice.Text;
        int res = paymentResponse.OrderInsert(RoleId, CategoryIds, "Pending");
        if (res > 0)
        {

            //DoPayment(res.ToString(), lblsubtotal.Text.Trim());
            GoPayment(res.ToString());//current method used for payment
        }
        else
        {
            lblmsg.Text = "Something went wrong Please try again";
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }

    }
    private void DoPayment(string order_id, string amount)
    {
        PayBAL = new PaymentBAL();
        CCACrypto ccaCrypto = new CCACrypto();
        //DataTable dt = (DataTable)ViewState["UserInfo"];
        string ccaRequest = "";
        string tid = CurrentDateTime.ToString("HHmmssffff");
        ccaRequest = ccaRequest + "tid=" + tid + "&";
        ccaRequest = ccaRequest + "merchant_id=" + Constant.merchant_id + "&";
        ccaRequest = ccaRequest + "order_id=" + order_id + "&";
        ccaRequest = ccaRequest + "amount=" + amount + "&";
        ccaRequest = ccaRequest + "currency=INR&";
        ccaRequest = ccaRequest + "redirect_url=" + Constant.PaymentReturnURL + "&";
        ccaRequest = ccaRequest + "cancel_url=" + Constant.PaymentReturnURL + "&";


        ccaRequest = ccaRequest + "billing_name=" + lblUserName.Text + "&";
        ccaRequest = ccaRequest + "billing_address=" + lblAddr.Text + "&";
        ccaRequest = ccaRequest + "billing_city=&";
        ccaRequest = ccaRequest + "billing_state=&";
        ccaRequest = ccaRequest + "billing_zip=&";
        ccaRequest = ccaRequest + "billing_country=&";


        //ccaRequest = ccaRequest + "billing_address=Address" + dt.Rows[0]["PermanentAddress"] + "&";
        //ccaRequest = ccaRequest + "billing_city=City" + dt.Rows[0]["City"] + "&";
        //ccaRequest = ccaRequest + "billing_state=State" + dt.Rows[0]["State"] + "&";
        //ccaRequest = ccaRequest + "billing_zip=600078" + dt.Rows[0]["PinCode"] + "&";
        //ccaRequest = ccaRequest + "billing_country=India" + "&";
        ccaRequest = ccaRequest + "billing_tel=" + lblContNo.Text + "&";
        ccaRequest = ccaRequest + "billing_email=" + lblEmailId.Text + "&";


        //ccaRequest = ccaRequest + "delivery_name=" + txtUserName.Text + "&";
        //ccaRequest = ccaRequest + "delivery_address=" + txtaddress.Text + "&";
        //ccaRequest = ccaRequest + "delivery_city=" + txtcity.Text + "&";
        //ccaRequest = ccaRequest + "delivery_state=" + txtState.Text + "&";
        //ccaRequest = ccaRequest + "delivery_zip=" + txtpincode.Text + "&";
        //ccaRequest = ccaRequest + "delivery_country=" + txtCountry.Text + "&";
        //ccaRequest = ccaRequest + "delivery_tel=" + txtcontactno.Text + "&";
        //ccaRequest = ccaRequest + "merchant_param1=" + "" + "&";
        //ccaRequest = ccaRequest + "merchant_param2=" + "" + "&";
        //ccaRequest = ccaRequest + "merchant_param3=" + "" + "&";
        //ccaRequest = ccaRequest + "merchant_param4=" + "" + "&";
        //ccaRequest = ccaRequest + "merchant_param5=" + "" + "&integration_type=iframe_normal&";
        //ccaRequest = ccaRequest + "promo_code=" + "" + "&";
        //ccaRequest = ccaRequest + "customer_identifier=" + "" + "&";
        //ccaRequest = "";
        string Wkey = Constant.workingKey;
        //ccaRequest = @"tid=1470827991465&merchant_id=106160&order_id=123654789&amount=1.00&currency=INR&redirect_url=http://localhost:1044/ccavResponseHandler.aspx&cancel_url=http://localhost:1044/ccavResponseHandler.aspx&billing_name=Charli&billing_address=Room no 1101, near Railway station Ambad&billing_city=Indore&billing_state=MP&billing_zip=425001&billing_country=India&billing_tel=9896226054&billing_email=test@gmail.com&delivery_name=Chaplin&delivery_address=room no.701 near bus stand&delivery_city=Hyderabad&delivery_state=Andhra&delivery_zip=425001&delivery_country=India&delivery_tel=9896226054&merchant_param1=additional Info.&merchant_param2=additional Info.&merchant_param3=additional Info.&merchant_param4=additional Info.&merchant_param5=additional Info.&integration_type=iframe_normal&promo_code=&customer_identifier=&";
        string strEncRequest = ccaCrypto.Encrypt(ccaRequest, Wkey);
        Response.Redirect(Constant.MobiURL + "redirect.php?strEncRequest=" + strEncRequest);


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
            a = 1;
            ViewState["promo"] = a;
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
        int k = 0, l = 0, m = 0;
        CategoryIds = ViewState["CategoryIds"].ToString();
        string[] categoryid = CategoryIds.Split(',');
        if (issoln == 1)
        {
            l++;
        }
        if (isduration == 1)
        {
            l++;
        }
        if (islicense == 1)
        {
            l++;
        }
        if (isminamt == 1)
        {
            l++;
        }
        if (issoln == 1)
        {
            foreach (var id in categoryid)
            {
                if (cid.Contains(id))
                {
                    k++;
                    fid = Convert.ToInt32(id);
                    dt1 = (DataTable)ViewState["data"];
                    price = price + Convert.ToInt32(dt1.Rows[0]["CloudPrice"].ToString());
                }
            }
            if (k > 0)
            {
                m++;
            }
        }
        if (isduration == 1)
        {
            if (Convert.ToInt32(lblduration.Text) >= noofduration)
            {
                m++;
            }
        }
        if (islicense == 1)
        {
            if (Convert.ToInt32(lbllicense.Text) >= nooflicense)
            {
                m++;
            }
        }
        if (isminamt == 1)
        {
            double tprice = Convert.ToInt32(lbllicense.Text) * Convert.ToInt32(lblduration.Text) * price * 30;
            if (tprice >= Convert.ToInt32(minamt))
            {
                m++;
            }
        }
        if (l == m)
        {
            return 1;
        }

        return 0;
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
                i = 1;
                Calculate();
                return 1;
            }
        }
        return 0;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtSmsCost.Text))
        {
            b = 1;
            ViewState["add"] = b;
            smscost.Visible = true;
            lblSmsCost.Text = txtSmsCost.Text + ".00";
            txtSmsCost.Enabled = false;
            imgedit.Visible = true;
            btnAdd.Visible = false;
            Calculate();
            lblmsg.Text = "";
        }
        else
        {
            lblMessage.Text = "Please Enter Sms Cost";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    //private void Calculate()
    //{
    //    int StateId = 0;
    //    if (ViewState["StateId"] != null)
    //    {
    //        StateId = Convert.ToInt32(ViewState["StateId"].ToString());
    //    }

    //    PayBAL = new PaymentBAL();
    //    int count = 0;
    //    double Price = 0, ffa = 0, amt = 0;
    //    foreach (GridViewRow row in grdPayment.Rows)
    //    {
    //        Label lblPrc = (Label)row.FindControl("lblPrice");
    //        Label lblcatid = (Label)row.FindControl("lblId");
    //        Price = (Price) + float.Parse(lblPrc.Text);
    //        count++;
    //    }
    //    amt = Convert.ToInt32(lbllicense.Text) * Convert.ToInt32(lblduration.Text) * Price * 30;//30 days because price is 1rs/solution/day/person  not for 1 month
    //    if (i == 1)
    //    {
    //        if (ViewState["isdiscount"].ToString() == "0")
    //        {
    //            fa = Convert.ToDouble(ViewState["totalamt"].ToString());
    //        }
    //        else
    //        {
    //            fa = ((amt * Convert.ToDouble(ViewState["discount"].ToString())) / 100);
    //        }
    //        if (fa < 0)
    //        {
    //            fa = 0;
    //        }
    //        dcprice.Visible = true;
    //        lbldiscountedprice.Text = fa.ToString("F");
    //    }
    //    ViewState["discountprice"] = lbldiscountedprice.Text;
    //    if (Constant.isservicetax == 1)
    //    {
    //        double cgst = 0, sgst = 0;
    //        if (i == 1)
    //        {
    //            sms = SmsCost();
    //            if (sms != 0.0)
    //            {
    //                sms = Convert.ToDouble(txtSmsCost.Text);
    //                if (StateId == Constant.SID)
    //                {
    //                    cgst = (((amt - fa) + sms) * Constant.CGST) / 100;
    //                    sgst = (((amt - fa) + sms) * Constant.SGST) / 100;
    //                    sa = cgst + sgst;
    //                }
    //                else
    //                {
    //                    sa = (((amt - fa) + sms) * Constant.IGST) / 100;
    //                }
    //            }
    //            else
    //            {
    //                if (StateId == Constant.SID)
    //                {
    //                    cgst = (((amt - fa)) * Constant.CGST) / 100;
    //                    sgst = (((amt - fa)) * Constant.SGST) / 100;
    //                    sa = cgst + sgst;
    //                }
    //                else
    //                {
    //                    sa = (((amt - fa)) * Constant.IGST) / 100;
    //                }
    //                //sa = ((amt - fa) * Constant.CGST) / 100;
    //            }
    //            if (StateId == Constant.SID)
    //            {
    //                lblCGST.Text = cgst.ToString("F");
    //                lblSGST.Text = sgst.ToString("F");
    //            }
    //            else
    //            {
    //                lblIGST.Text = sa.ToString("F");
    //            }
    //        }
    //        else
    //        {
    //            if (b == 1)
    //            {
    //                if (!string.IsNullOrEmpty(ViewState["discountprice"].ToString()))
    //                {
    //                    sms = SmsCost();
    //                    double aa = ((amt - Convert.ToDouble(ViewState["discountprice"].ToString())) + sms);
    //                    if (StateId == Constant.SID)
    //                    {
    //                        cgst = (((amt - Convert.ToDouble(ViewState["discountprice"].ToString())) + sms) * Constant.CGST) / 100;
    //                        sgst = (((amt - Convert.ToDouble(ViewState["discountprice"].ToString())) + sms) * Constant.SGST) / 100;
    //                        sa = cgst + sgst;
    //                    }
    //                    else
    //                    {
    //                        sa = (((amt - Convert.ToDouble(ViewState["discountprice"].ToString())) + sms) * Constant.IGST) / 100;
    //                    }
    //                }
    //                else
    //                {
    //                    sms = SmsCost();
    //                    if (sms != 0.0)
    //                    {
    //                        if (StateId != 0)
    //                        {
    //                            if (StateId == Constant.SID)
    //                            {
    //                                cgst = ((amt + sms) * Constant.CGST) / 100;
    //                                sgst = ((amt + sms) * Constant.SGST) / 100;
    //                                sa = cgst + sgst;
    //                            }
    //                            else
    //                            {
    //                                sa = ((amt + sms) * Constant.IGST) / 100;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            sa = amt;
    //                        }
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                sms = SmsCost();
    //                if (sms != 0.0)
    //                {
    //                    sms = Convert.ToDouble(txtSmsCost.Text);
    //                    if (StateId != 0)
    //                    {
    //                        if (StateId == Constant.SID)
    //                        {
    //                            cgst = ((amt + sms) * Constant.CGST) / 100;
    //                            sgst = ((amt + sms) * Constant.SGST) / 100;
    //                            sa = cgst + sgst;
    //                        }
    //                        else
    //                        {
    //                            sa = ((amt + sms) * Constant.IGST) / 100;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        sa = amt;
    //                    }
    //                }
    //                else
    //                {
    //                    if (StateId != 0)
    //                    {
    //                        if (StateId == Constant.SID)
    //                        {
    //                            cgst = ((amt) * Constant.CGST) / 100;
    //                            sgst = ((amt) * Constant.SGST) / 100;
    //                            sa = cgst + sgst;
    //                        }
    //                        else
    //                        {
    //                            sa = ((amt + sms) * Constant.IGST) / 100;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        sa = amt;
    //                    }
    //                }
    //            }
    //            if (StateId == Constant.SID)
    //            {
    //                lblCGST.Text = cgst.ToString("F");
    //                lblSGST.Text = sgst.ToString("F");
    //            }
    //            else
    //            {
    //                lblIGST.Text = sa.ToString("F");
    //            }
    //        }
    //        if (StateId == Constant.SID)
    //        {
    //            ViewState["servicetax"] = sa.ToString();
    //        }
    //        else
    //        {
    //            ViewState["servicetax"] = lblIGST.Text;
    //        }
    //        ffa = Calculation(amt, ViewState["discountprice"].ToString(), sms, ViewState["servicetax"].ToString(), StateId);
    //    }
    //    else
    //    {
    //        if (i == 1)
    //        {
    //            ffa = Math.Round(fa, 2);
    //        }
    //        else
    //        {
    //            ffa = Math.Round(amt, 2);
    //        }
    //        labelservicetax.Visible = false;
    //        lblservicetax.Visible = false;
    //    }
    //    lblnoofsolns.Text = count.ToString();
    //    lbltotalprice.Text = amt.ToString("F");
    //    lblsubtotal.Text = ffa.ToString("F");
    //}
    private void Calculate()
    {
        int StateId = 0;
        if (ViewState["StateId"] != null)
        {
            StateId = Convert.ToInt32(ViewState["StateId"].ToString());
        }

        PayBAL = new PaymentBAL();
        int count = 0;
        double Price = 0, ffa = 0, amt = 0;
        foreach (GridViewRow row in grdPayment.Rows)
        {
            Label lblPrc = (Label)row.FindControl("lblPrice");
            Label lblcatid = (Label)row.FindControl("lblId");
            Price = (Price) + float.Parse(lblPrc.Text);
            count++;
        }
        amt = Convert.ToInt32(lbllicense.Text) * Convert.ToInt32(lblduration.Text) * Price * 30;//30 days because price is 1rs/solution/day/person  not for 1 month
        if (i == 1)
        {
            if (ViewState["isdiscount"].ToString() == "0")
            {
                fa = Convert.ToDouble(ViewState["totalamt"].ToString());
            }
            else
            {
                fa = ((amt * Convert.ToDouble(ViewState["discount"].ToString())) / 100);
            }
            if (fa < 0)
            {
                fa = 0;
            }
            dcprice.Visible = true;
            lbldiscountedprice.Text = fa.ToString("F");
        }
        ViewState["discountprice"] = lbldiscountedprice.Text;
        if (Constant.isservicetax == 1)
        {
            double cgst = 0, sgst = 0;

            if (!string.IsNullOrEmpty(ViewState["discountprice"].ToString()))
            {
                sms = SmsCost();
                double aa = ((amt - Convert.ToDouble(ViewState["discountprice"].ToString())) + sms);
                if (StateId == Constant.SID)
                {
                    cgst = (((amt - Convert.ToDouble(ViewState["discountprice"].ToString())) + sms) * Constant.CGST) / 100;
                    sgst = (((amt - Convert.ToDouble(ViewState["discountprice"].ToString())) + sms) * Constant.SGST) / 100;
                    sa = cgst + sgst;
                }
                else
                {
                    sa = (((amt - Convert.ToDouble(ViewState["discountprice"].ToString())) + sms) * Constant.IGST) / 100;
                }
            }
            else
            {
                sms = SmsCost();
                if (StateId != 0)
                {
                    if (StateId == Constant.SID)
                    {
                        cgst = ((amt + sms) * Constant.CGST) / 100;
                        sgst = ((amt + sms) * Constant.SGST) / 100;
                        sa = cgst + sgst;
                    }
                    else
                    {
                        sa = ((amt + sms) * Constant.IGST) / 100;
                    }
                }
            }

            if (StateId == Constant.SID)
            {
                lblCGST.Text = cgst.ToString("F");
                lblSGST.Text = sgst.ToString("F");
            }
            else
            {
                lblIGST.Text = sa.ToString("F");
            }
            if (StateId == Constant.SID)
            {
                ViewState["servicetax"] = sa.ToString();
            }
            else
            {
                ViewState["servicetax"] = lblIGST.Text;
            }
            ffa = Calculation(amt, ViewState["discountprice"].ToString(), sms, ViewState["servicetax"].ToString(), StateId);
        }
        else
        {
            if (i == 1)
            {
                ffa = Math.Round(fa, 2);
            }
            else
            {
                ffa = Math.Round(amt, 2);
            }
            labelservicetax.Visible = false;
            lblservicetax.Visible = false;
        }
        lblnoofsolns.Text = count.ToString();
        lbltotalprice.Text = amt.ToString("F");
        lblsubtotal.Text = ffa.ToString("F");
    }
    protected void imgedit_Click(object sender, EventArgs e)
    {
        txtSmsCost.Enabled = true;
        imgsave.Visible = true;
        imgedit.Visible = false;
    }
    protected void imgsave_Click(object sender, EventArgs e)
    {
        b = 1;
        ViewState["add"] = b;
        txtSmsCost.Enabled = true;
        if (txtSmsCost.Text == "")
        {
            lblSmsCost.Text = "0.00";
        }
        else
        {
            lblSmsCost.Text = txtSmsCost.Text + ".00";
        }
        Calculate();
        txtSmsCost.Enabled = false;
        imgedit.Visible = true;
        imgsave.Visible = false;
    }
    private double Calculation(double price, string discount, double sms, string servicetax, int StateId)
    {
        if (StateId != 0)
        {
            if (StateId == Constant.SID)
            {
                if (!string.IsNullOrEmpty(discount))
                {
                    return (price - Convert.ToDouble(discount)) + sms + Convert.ToDouble(servicetax);
                }
                else
                {
                    return price + sms + Convert.ToDouble(servicetax);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(discount))
                {
                    return (price - Convert.ToDouble(discount)) + sms + Convert.ToDouble(servicetax);
                }
                else
                {
                    return price + sms + Convert.ToDouble(servicetax);
                }
            }
        }
        else
        {
            //return price + sms;
            if (!string.IsNullOrEmpty(discount))
            {
                return (price - Convert.ToDouble(discount)) + sms;
            }
            else
            {
                return price + sms;
            }
        }
    }
    private double SmsCost()
    {
        if (!string.IsNullOrEmpty(txtSmsCost.Text))
        {
            sms = Convert.ToDouble(txtSmsCost.Text);
        }
        return sms;
    }
    protected void lnkbtnremove_Click(object sender, EventArgs e)
    {
        txtpromocode.Text = "";
        txtpromocode.Enabled = true;
        lblMessage.Text = "";
        btnapply.Visible = true;
        dcprice.Visible = false;
        lbldiscountedprice.Text = fa.ToString("F");
        Calculate();
        lnkbtnremove.Visible = false;
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
        string AmountForm = Convert.ToDecimal(lblsubtotal.Text.Trim()).ToString("g29");// eliminating trailing zeros
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
                case "udf2":
                    hash_string = hash_string + lblSmsCost.Text + "|";
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
            data.Add("surl", Constant.PayUPaymentReturnURL);
            data.Add("furl", Constant.PayUPaymentReturnURL);
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
            data.Add("udf2", lblSmsCost.Text);
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
        PaymentResponse payRes = new PaymentResponse();
        dt1 = new DataTable();
        dt1 = (DataTable)ViewState["data"];
        DataTable dttemp = new DataTable();
        dttemp.Columns.AddRange(new DataColumn[4] {
                         new DataColumn("SNo"),
                          new DataColumn("CategoryName"),
                                new DataColumn("HSN"),
                                new DataColumn("Price")});
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            dttemp.Rows.Add(i + 1, dt1.Rows[i]["CategoryName"].ToString(), "998319 (Other information technology services n.e.c )", dt1.Rows[i]["CloudPrice"].ToString());
        }
        byte[] pdfByte = payRes.CreatePDF(txtCity.Text, txtAddress.Text, txtGSTNo.Text, LoginUser.Text, lblEmailId.Text, lblContNo.Text, "ONLINE", string.IsNullOrWhiteSpace(lbldiscountedprice.Text) ? "0.00" : lbldiscountedprice.Text, lblSmsCost.Text, CurrentDateTime, "", lblnoofsolns.Text, lblduration.Text, lbllicense.Text, lbltotalprice.Text, lblCGST.Text, lblSGST.Text, lblIGST.Text, lblsubtotal.Text, dttemp);
        Response.Clear();
        MemoryStream ms = new MemoryStream(pdfByte);
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Performa_Invoice.pdf");
        Response.Buffer = true;
        ms.WriteTo(Response.OutputStream);
        Response.End();
    }

}
