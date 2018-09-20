using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class PWCcommuniction : Base
    {
        int ClientId, UserId, RoleId, DeptId, CategoryId = 14;// IsCashew = 3;//, IsP = 0, IsW = 0, IsC = 0
        private string ParentId = "8";
        SecureBAL secure;
        AnuSearch srch;
        FeatureBAL feature;
        DataTable dt, dt1;
        LomentBAL lomentbal;
        SendSMSBAL sms;
        string cid = "", fcid = "";
        DataTable temptbl;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (chkIsApplicable())
            {
                gridmsg.Visible = false;
                if (!IsPostBack)
                {
                    ViewState["UserIdList"] = "";
                    BindGrid();
                }
            }
            else
            {
                Response.Redirect("SubscribePeanut.aspx");
            }
        }

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int i = 0;
                Label lblPeanut = (Label)e.Row.FindControl("IsPeanut");
                Label lblWalnut = (Label)e.Row.FindControl("IsWalnut");
                Label lblCashew = (Label)e.Row.FindControl("IsCashew");
                CheckBox chk = (CheckBox)e.Row.FindControl("AchkRow_Parents");
                CheckBox chk1 = (CheckBox)e.Row.FindControl("AchkRow_Parents1");
                CheckBox chk2 = (CheckBox)e.Row.FindControl("AchkRow_Parents2");
                CheckBox chk3 = (CheckBox)e.Row.FindControl("AchkRow_Parents3");
                LinkButton lnkp = (LinkButton)e.Row.FindControl("lnkUnLinkPeanut");
                LinkButton lnkw = (LinkButton)e.Row.FindControl("lnkUnLinkWalnut");
                LinkButton lnkc = (LinkButton)e.Row.FindControl("lnkUnLinkCashew");

                secure = new SecureBAL();
                dt = secure.GetCategoryIdFromAppliedSubscriptionDtl();
                ViewState["CategoryList"] = dt;
                foreach (DataRow obj in dt.Rows)
                {
                    cid = cid + Convert.ToInt32(obj["CategoryId"].ToString()) + ",";
                }
                fcid = cid.TrimEnd(',');
                string[] categoryid = cid.Split(',');
                if (categoryid.Contains("13"))
                {
                    //chk1.Enabled = true;
                    if (lblPeanut.Text == "1")
                    {
                        chk1.Checked = true;
                        //chk.Checked = true;
                        i++;
                    }
                    else
                    {
                        chk1.Checked = false;

                    }
                }
                else
                {
                    chk1.Enabled = false;
                    lnkp.Text = "---";
                    lnkp.Enabled = false;
                }
                if (categoryid.Contains("14"))
                {
                    //chk2.Enabled = true;
                    if (lblWalnut.Text == "1")
                    {
                        chk2.Checked = true;
                        //chk.Checked = true;
                        i++;
                    }
                    else
                    {
                        chk2.Checked = false;

                    }
                }
                else
                {
                    chk2.Enabled = false;
                    lnkw.Text = "---";
                    lnkw.Enabled = false;
                }
                if (categoryid.Contains("15"))
                {
                    // chk3.Enabled = true;
                    if (lblCashew.Text == "1")
                    {
                        chk3.Checked = true;
                        i++;
                        //chk.Checked = true;
                    }
                    else
                    {
                        chk3.Checked = false;
                        ////i++;
                    }
                }
                else
                {
                    chk3.Enabled = false;
                    lnkc.Text = "---";
                    lnkc.Enabled = false;
                }


                chk.Enabled = false;
                if (i == 3) { chk.Checked = true; chk.Enabled = true; }
                i = 0;
            }
        }

        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            ViewState["UserIdList"] = "";
            BindGrid();
        }

        protected void BindGrid()
        {
            srch = new AnuSearch();
            dt = new DataTable();
            //DataTable dtSelect = new DataTable();

            if (RoleId == 1 || RoleId == 2)
            {
                dt = srch.GetCommunictionDetail(ClientId, 0, 0, 0, CategoryId);

            }
            else if (RoleId == 3)
            {
                dt = srch.GetCommunictionDetail(ClientId, 0, DeptId, 0, CategoryId);
            }
            else
            {
                dt = srch.GetCommunictionDetail(ClientId, UserId, 0, 0, CategoryId);

            }
            ////dtSelect = dt.Clone();
            ////dtSelect = dt.Select("FeatureId = " + IsCashew + " or  FeatureId is null").CopyToDataTable();

            grdUser.DataSource = dt;
            grdUser.DataBind();
        }

        protected void ApplyChanges_Click(object sender, EventArgs e)
        {
            temptbl = new DataTable();
            temptbl.Columns.Add("name");
            temptbl.Columns.Add("result");
            //string name = "", pwd = "", emailid = "", mblno = "", result = "";
            try
            {
                //DataTable temptbl = new DataTable();            
                //temptbl.Columns.Add("Id");
                //temptbl.Columns.Add("IsPeanut");
                //temptbl.Columns.Add("IsWalnut");
                //temptbl.Columns.Add("IsCashew");
                string result = "";
                int check = 0;
                for (int idx = 0; idx < grdUser.Rows.Count; idx++)
                {
                    if (((Label)(grdUser.Rows[idx].FindControl("lblISChecked"))).Text == "1")
                    {
                        string id = "", name = "", EmailId = "", Password = "", MobileNo = "";
                        int ISCashew = 0, IsPeanut = 0, IsWalnut = 0;
                        IsPeanut = Convert.ToInt16(((CheckBox)grdUser.Rows[idx].FindControl("AchkRow_Parents1")).Checked);
                        IsWalnut = Convert.ToInt16(((CheckBox)grdUser.Rows[idx].FindControl("AchkRow_Parents2")).Checked);
                        ISCashew = Convert.ToInt16(((CheckBox)grdUser.Rows[idx].FindControl("AchkRow_Parents3")).Checked);
                        id = ((Label)grdUser.Rows[idx].FindControl("lblUserId")).Text;
                        EmailId = ((Label)grdUser.Rows[idx].FindControl("lblEmailid")).Text;
                        Password = ((Label)grdUser.Rows[idx].FindControl("lblpwd")).Text;
                        MobileNo = ((Label)grdUser.Rows[idx].FindControl("lblMobileno")).Text;
                        name = ((Label)grdUser.Rows[idx].FindControl("lblName")).Text;
                        int pcheck = 2, ccheck = 2, wcheck = 2;
                        if (((Label)(grdUser.Rows[idx].FindControl("lblIsP"))).Text == "1")
                        {
                            pcheck = IsPeanut;
                        }
                        if (((Label)(grdUser.Rows[idx].FindControl("lblIsW"))).Text == "1")
                        {
                            wcheck = IsWalnut;
                        }
                        if (((Label)(grdUser.Rows[idx].FindControl("lblIsC"))).Text == "1")
                        {
                            ccheck = ISCashew;
                        }
                        if (pcheck != 2 || ccheck != 2 || wcheck != 2)
                        {
                            check++;
                            //ISCashew = Convert.ToInt16(chk1.Checked).ToString();
                            //IsPeanut = Convert.ToInt16(chk2.Checked).ToString();
                            //IsWalnut = Convert.ToInt16(chk3.Checked).ToString();
                            //user = new UserBAL();
                            //user.UserId = Convert.ToInt32(id);
                            //dt = new DataTable();
                            //dt = user.GetUserDtlByUserId();
                            //if (dt.Rows.Count > 0)
                            // {
                            //  name = dt.Rows[0]["UserName"].ToString();
                            //  pwd = dt.Rows[0]["Password"].ToString();
                            //  emailid = dt.Rows[0]["EmailId"].ToString();
                            //  mblno = dt.Rows[0]["MobileNo"].ToString();
                            //}
                            if (pcheck == 0)
                            {
                                result = UnlinkUser(id, EmailId, 1, 0, IsPeanut, IsWalnut, ISCashew, EmailId, Password, MobileNo);
                                temptbl.Rows.Add(new object[] { name, result });
                                //ResultString(name, result, 1);
                            }
                            if (wcheck == 0)
                            {
                                result = UnlinkUser(id, EmailId, 2, 0, IsPeanut, IsWalnut, ISCashew, EmailId, Password, MobileNo);
                                temptbl.Rows.Add(new object[] { name, result });
                                //ResultString(name, result, 2);
                            }
                            if (ccheck == 0)
                            {
                                result = UnlinkUser(id, EmailId, 3, 0, IsPeanut, IsWalnut, ISCashew, EmailId, Password, MobileNo);
                                temptbl.Rows.Add(new object[] { name, result });
                                //ResultString(name, result, 3);
                            }
                            if (pcheck == 1 || ccheck == 1 || wcheck == 1)
                            {
                                dt1 = new DataTable();
                                srch = new AnuSearch();
                                dt1 = srch.GetLomentUserbymailid(EmailId);
                                if (dt1.Rows.Count > 0)
                                {
                                    if (pcheck == 1)
                                    {
                                        result = UnlinkUser(id, EmailId, 1, 1, IsPeanut, IsWalnut, ISCashew, EmailId, Password, MobileNo);
                                        temptbl.Rows.Add(new object[] { name, result });
                                        //ResultString(name, result, 1);
                                    }

                                    if (wcheck == 1)
                                    {
                                        result = UnlinkUser(id, EmailId, 2, 1, IsPeanut, IsWalnut, ISCashew, EmailId, Password, MobileNo);
                                        temptbl.Rows.Add(new object[] { name, result });
                                        //ResultString(name, result, 2);
                                    }
                                    if (ccheck == 1)
                                    {
                                        result = UnlinkUser(id, EmailId, 3, 1, IsPeanut, IsWalnut, ISCashew, EmailId, Password, MobileNo);
                                        temptbl.Rows.Add(new object[] { name, result });
                                        //ResultString(name, result, 3);
                                    }
                                }
                                else
                                {
                                    result = Registration(id, name, Password, EmailId, MobileNo);
                                    temptbl.Rows.Add(new object[] { name, result });
                                    if (result != "0")
                                    {
                                        result = AllocateUser(id, IsPeanut, IsWalnut, ISCashew, EmailId, Password, MobileNo);
                                        temptbl.Rows.Add(new object[] { name, result });
                                        //ResultString(name, result, 4);
                                    }
                                }
                            }
                        }
                    }
                }
                if (temptbl.Rows.Count > 0)
                {
                    lblMsg.Text = "Result";
                    gridmsg.DataSource = temptbl;
                    gridmsg.DataBind();
                    gridmsg.Visible = true;
                    //if (result == "1")
                    //{
                    //  lblMsg.Text = "Changes Applied Successfully";
                    //  lblMsg.ForeColor = System.Drawing.Color.Green;

                }
                //else if (result == "0")
                //{
                // lblMsg.Text = "Something went wrong";
                // lblMsg.ForeColor = System.Drawing.Color.Red;
                //}
                else //if (check == 0)
                {
                    lblMsg.Text = "No Changes Found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                //else
                //{
                //   lblMsg.Text = result;
                //   lblMsg.ForeColor = System.Drawing.Color.Red;
                //}
                BindGrid();
            }
            catch (Exception)
            {
                lblMsg.Text = "Something went wrong";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                BindGrid();
            }
        }


        protected string AllocateUser(string id, int IsPeanut, int IsWalnut, int ISCashew, string EmailId, string Password, string MobileNo)
        {
            LomentAPI loment = new LomentAPI();
            lomentbal = new LomentBAL();
            dt = new DataTable();
            lomentbal.userid = Convert.ToInt32(id);
            dt = lomentbal.GetLomentAllocateUser();
            if (dt.Rows.Count > 0)
            {
                loment.IsPeanut = IsPeanut;
                loment.IsWalnut = IsWalnut;
                loment.IsCashew = ISCashew;
                loment.username = dt.Rows[0]["adminuser"].ToString();
                loment.s_username = dt.Rows[0]["suser"].ToString();
                loment.bill_id = dt.Rows[0]["Bill_Id"].ToString();
                loment.feature_id = 4;
                loment.lomentUserId = Convert.ToInt32(dt.Rows[0]["LomentUserId"]);
                loment.AdminUserId = Convert.ToInt32(dt.Rows[0]["adminuserid"]);
                loment.Postdata = Encoding.UTF8.GetBytes(Server.UrlDecode("username=" + dt.Rows[0]["adminuser"].ToString() + "&bill_id=" + dt.Rows[0]["Bill_Id"].ToString() + "&s_username=" + dt.Rows[0]["suser"].ToString() + "&peanut_devices=" + IsPeanut + "&walnut_devices=" + IsWalnut + "&cashewnut_devices=" + ISCashew + "&walnut_outlook_plugin_devices=" + 0));
                loment.clientid = ClientId;
                Dictionary<string, string> dict = loment.AllocateUser();
                //if (!string.IsNullOrEmpty(res) || res != "Given bill id is Invalid")
                if (dict.ContainsKey("1") == true)
                {
                    string Link = " Download the app from following link : ";
                    if (IsPeanut == 1)
                    {
                        Link += "Secure SMS: " + Constant.SecureSMSAndroidLink + " ";// https://play.google.com/store/apps/details?id=com.loment.peanut.mobile&hl=en ";
                    }
                    if (IsWalnut == 1)
                    {
                        Link += "Secure Email:  " + Constant.SecureEmailAndroidLink + " ";//https://play.google.com/store/apps/details?id=com.loment&hl=en ";

                    }
                    if (ISCashew == 1)
                    {
                        Link += "Secure Messenger:  " + Constant.SecureMessengerAndroidLink + " ";//https://play.google.com/store/apps/details?id=com.loment.cashew&hl=en ";
                    }
                    string text = "Dear " + dt.Rows[0]["SUserName"].ToString() + ", You have been registered on MobiOcean Secure Communication by " + dt.Rows[0]["AUserName"].ToString() + ". APP download link: " + Link + " . Use the below information for login: Email-ID : " + EmailId + " Password : " + Password + " , Use the  key (" + dict["1"].ToString() + ") to subscribe the APP.";
                    sendMsgUsingSMS(text, MobileNo, ClientId);
                    text = "Dear " + dt.Rows[0]["SUserName"].ToString() + ",<br/> You have been registered on MobiOcean Secure Communication by " + dt.Rows[0]["AUserName"].ToString() + ".<br/> APP download link: " + Link + " Use the below information for login:<br/> Email-ID : " + EmailId + "<br/> Password : " + Password + "<br/> Use the  key (" + dict["1"].ToString() + ") to subscribe the APP.<br/><br/> Have a nice day <br/>Regards <br/>MobiOcean Team";
                    SendEmailForEmailToSMS(EmailId, "MobiOcean Secure Communication Registration Information", text);
                    return "User allocated successfully on selected solution.";
                }
                else if (dict.ContainsKey("2") == true)
                    return dict["2"].ToString();
                else
                    return "Something went wrong! Please contact our support team.";
            }
            else
            {
                return "User not registered.";
            }
        }

        protected bool chkIsApplicable()
        {
            feature = new FeatureBAL();
            dt = new DataTable();
            feature.ClientId = ClientId;
            feature.SolutionId = "2";
            dt = feature.GetActiveSolutions();
            if (dt != null)
            {
                //int categoryid;
                foreach (DataRow row in dt.Rows)
                {
                    //categoryid = Convert.ToInt32(row["CategoryId"].ToString());
                    if (!string.IsNullOrEmpty(row["SubscriptionId"].ToString()))//CategoryId == categoryid
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected string Registration(string id, string name, string pwd, string emailid, string mblno)
        {
            LomentAPI loment = new LomentAPI();
            loment.userid = Convert.ToInt32(id);
            loment.clientid = ClientId;
            loment.isadmin = 0;
            //loment.feature_id = IsCashew;
            //loment.name = name;
            loment.password = pwd;
            loment.primary_email = emailid;
            //loment.primary_mobile_number = mblno;
            loment.Postdata = Encoding.UTF8.GetBytes(Server.UrlDecode("name=" + name + "&password=" + pwd + "&primary_email=" + emailid + "&primary_mobile_number=91" + mblno + "&country_abbrev=IN" + "&partner_id=" + ParentId));
            string res = loment.RegisterCompanyAdmin();
            return res;
        }

        protected void AchkRow_Parents1_CheckedChanged(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox cb = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents1");
            CheckBox cbb = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents2");
            CheckBox cbbb = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents3");
            CheckBox cb1 = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents");
            ((Label)((CheckBox)sender).Parent.FindControl("lblISChecked")).Text = "1";
            if (((Label)((CheckBox)sender).Parent.FindControl("lblIsP")).Text == "1")
            {
                ((Label)((CheckBox)sender).Parent.FindControl("lblIsP")).Text = "0";
            }
            else
            {
                ((Label)((CheckBox)sender).Parent.FindControl("lblIsP")).Text = "1";
            }


            if (cb.Checked && cbb.Checked && cbbb.Checked)
            {
                cb1.Checked = true;
            }
            else
                cb1.Checked = false;

        }

        protected void AchkRow_Parents2_CheckedChanged(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox cb = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents2");
            CheckBox cbb = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents1");
            CheckBox cbbb = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents3");
            CheckBox cb1 = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents");
            ((Label)((CheckBox)sender).Parent.FindControl("lblISChecked")).Text = "1";


            if (((Label)((CheckBox)sender).Parent.FindControl("lblIsW")).Text == "1")
            {
                ((Label)((CheckBox)sender).Parent.FindControl("lblIsW")).Text = "0";
            }
            else
            {
                ((Label)((CheckBox)sender).Parent.FindControl("lblIsW")).Text = "1";
            }



            if (cb.Checked && cbb.Checked && cbbb.Checked)
            {
                cb1.Checked = true;
            }
            else
                cb1.Checked = false;
        }

        protected void AchkRow_Parents3_CheckedChanged(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox cb = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents3");
            CheckBox cbb = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents2");
            CheckBox cbbb = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents1");
            CheckBox cb1 = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents");
            ((Label)((CheckBox)sender).Parent.FindControl("lblISChecked")).Text = "1";


            if (((Label)((CheckBox)sender).Parent.FindControl("lblIsC")).Text == "1")
            {
                ((Label)((CheckBox)sender).Parent.FindControl("lblIsC")).Text = "0";
            }
            else
            {
                ((Label)((CheckBox)sender).Parent.FindControl("lblIsC")).Text = "1";
            }

            if (cb.Checked && cbb.Checked && cbbb.Checked)
            {
                cb1.Checked = true;
            }
            else
                cb1.Checked = false;

        }

        protected void Achk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)grdUser.HeaderRow.FindControl("AchkHeader_Parents");
            foreach (GridViewRow row in grdUser.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("AchkRow_Parents");
                CheckBox chk1 = (CheckBox)row.FindControl("AchkRow_Parents1");
                CheckBox chk2 = (CheckBox)row.FindControl("AchkRow_Parents2");
                CheckBox chk3 = (CheckBox)row.FindControl("AchkRow_Parents3");
                if (ChkBoxHeader.Checked == true)
                {
                    //ChkBoxRows.Checked = true;
                    Fullrowchecked(sender, ChkBoxRows, chk1, chk2, chk3, ((Label)row.FindControl("lblISChecked")));

                }
                else
                {
                    //ChkBoxRows.Checked = false;
                    Fullrowchecked(sender, ChkBoxRows, chk1, chk2, chk3, ((Label)row.FindControl("lblISChecked")));
                }
                //((Label)row.FindControl("lblISChecked")).Text = "1";
            }
        }

        protected void AchkRow_Parents_CheckedChanged(object sender, EventArgs e)
        {
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox ChkBoxRow = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents");
            CheckBox chk1 = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents1");
            CheckBox chk2 = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents2");
            CheckBox chk3 = (CheckBox)grdUser.Rows[selRowIndex].FindControl("AchkRow_Parents3");
            if (ChkBoxRow.Checked == true)
            {
                Fullrowchecked(sender, ChkBoxRow, chk1, chk2, chk3, ((Label)grdUser.Rows[selRowIndex].FindControl("lblISChecked")));
            }
            else // Here Some error
            {
                Fullrowchecked(sender, ChkBoxRow, chk1, chk2, chk3, ((Label)grdUser.Rows[selRowIndex].FindControl("lblISChecked")));
            }
            // ((Label)grdUser.Rows[selRowIndex].FindControl("lblISChecked")).Text = "1";
        }

        private void Fullrowchecked(object sender, CheckBox ChkBoxRows, CheckBox chk1, CheckBox chk2, CheckBox chk3, Label lbl)
        {

            dt = (DataTable)ViewState["CategoryList"];
            foreach (DataRow obj in dt.Rows)
            {
                cid = cid + Convert.ToInt32(obj["CategoryId"].ToString()) + ",";
            }
            fcid = cid.TrimEnd(',');
            string[] categoryid = cid.Split(',');
            if (categoryid.Contains("13"))
            {
                chk1.Checked = ChkBoxRows.Checked;
                if (((Label)((CheckBox)sender).Parent.FindControl("lblIsP")).Text == "1")
                {
                    ((Label)((CheckBox)sender).Parent.FindControl("lblIsP")).Text = "0";
                }
                else
                {
                    ((Label)((CheckBox)sender).Parent.FindControl("lblIsP")).Text = "1";
                }
                lbl.Text = "1";
            }
            if (categoryid.Contains("14"))
            {
                chk2.Checked = ChkBoxRows.Checked;
                if (((Label)((CheckBox)sender).Parent.FindControl("lblIsW")).Text == "1")
                {
                    ((Label)((CheckBox)sender).Parent.FindControl("lblIsW")).Text = "0";
                }
                else
                {
                    ((Label)((CheckBox)sender).Parent.FindControl("lblIsW")).Text = "1";
                }
                lbl.Text = "1";
            }
            if (categoryid.Contains("15"))
            {
                chk3.Checked = ChkBoxRows.Checked;
                if (((Label)((CheckBox)sender).Parent.FindControl("lblIsC")).Text == "1")
                {
                    ((Label)((CheckBox)sender).Parent.FindControl("lblIsC")).Text = "0";
                }
                else
                {
                    ((Label)((CheckBox)sender).Parent.FindControl("lblIsC")).Text = "1";
                }
                lbl.Text = "1";
            }

        }
        protected void lnkUnLinkPeanut_Click(object sender, EventArgs e)
        {
            LinkButton lnkbnt = sender as LinkButton;
            Devicesunlink_Click(lnkbnt, 1, "Peanut");
        }

        protected void lnkUnLinkWalnut_Click(object sender, EventArgs e)
        {
            LinkButton lnkbnt = sender as LinkButton;
            Devicesunlink_Click(lnkbnt, 2, "Walnut");
        }

        protected void lnkUnLinkCashew_Click(object sender, EventArgs e)
        {
            LinkButton lnkbnt = sender as LinkButton;
            Devicesunlink_Click(lnkbnt, 3, "Cashewnut");
        }

        public void Devicesunlink_Click(LinkButton lnkbnt, int IsPWC, string Name)
        {
            try
            {
                string res = "";                
                GridViewRow gvr = lnkbnt.NamingContainer as GridViewRow;                
                Label uname = (Label)grdUser.Rows[gvr.RowIndex].FindControl("lblEmailid");
                Label lid = (Label)grdUser.Rows[gvr.RowIndex].FindControl("lblLomentId");
                Label pwd = (Label)grdUser.Rows[gvr.RowIndex].FindControl("lblpwd");
                LomentAPI loment = new LomentAPI();
                loment.username = uname.Text;                
                loment.Postdata = Encoding.UTF8.GetBytes(Server.UrlDecode("password=" + pwd.Text));                
                Dictionary<string, string> dict = loment.GetDevice(Name);
                if (dict.ContainsKey("1") == true)
                {
                    res = DeviceUnlink(dict["1"].ToString(), uname.Text, IsPWC);
                    lblMsg.Text = res;
                    lblMsg.ForeColor = System.Drawing.Color.Green;                   
                }                
                else if (dict.ContainsKey("2") == true)
                {
                    res = dict["2"].ToString();
                    lblMsg.Text = res;
                    if (res == "1")
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else if (dict.ContainsKey("3") == true)
                {
                    res = dict["3"].ToString();
                    lblMsg.Text = res;
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }
                BindGrid();
            }
            catch (Exception)
            {
                lblMsg.Text = "Something Went Wrong";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                BindGrid();
            }
        }

        protected string DeviceUnlink(string device_id, string UserName, int IsPWC)
        {
            LomentAPI loment = new LomentAPI();
            loment.username = UserName;
            loment.device_id = device_id;
            loment.feature_id = IsPWC;
            return loment.DeviceUnlink();
        }

        protected string UnlinkUser(string id, string name, int IsPWC, int count, int IsPeanut, int IsWalnut, int ISCashew, string EmailId, string Password, string MobileNo)
        {
            string res = "";
            lomentbal = new LomentBAL();
            dt = new DataTable();
            lomentbal.clientid = ClientId;
            lomentbal.userid = Convert.ToInt32(id);
            lomentbal.featureid = IsPWC;
            dt = lomentbal.GetLomentAllocateUser();
            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["Keys"].ToString()))
                {
                    string billdetails = dt.Rows[0]["Bill_Id"].ToString();
                    LomentAPI loment = new LomentAPI();
                    loment.bill_id = billdetails;
                    loment.username = dt.Rows[0]["adminuser"].ToString(); ;
                    loment.s_username = dt.Rows[0]["suser"].ToString();
                    loment.keys = dt.Rows[0]["Keys"].ToString();
                    loment.feature_id = IsPWC;
                    loment.lomentUserId = Convert.ToInt32(dt.Rows[0]["LomentUserId"]);
                    loment.AdminUserId = UserId;
                    loment.clientid = ClientId;
                    //int cou = (Convert.ToInt32(dt.Rows[0]["AllocateCount"])-1);
                    //loment.count = cou;
                    loment.count = count;
                    loment.Postdata = Encoding.UTF8.GetBytes(Server.UrlDecode("username=" + name + "&bill_id=" + billdetails + "&s_username=" + dt.Rows[0]["suser"].ToString() + "&feature_id=" + IsPWC + "&key=" + dt.Rows[0]["Keys"].ToString() + "&count=" + count));
                    res = loment.UserUnlink();
                }
                else
                {
                    res = AllocateUser(id, IsPeanut, IsWalnut, ISCashew, EmailId, Password, MobileNo);
                }
                return res;
            }
            else
                return "User not registered.";

        }

        private void sendMsgUsingSMS(string text, string ContactNo, int ClientId)
        {
            sms = new SendSMSBAL();
            sms.sendMsgUsingSMS(text, ContactNo, ClientId);
        }
        private void SendEmailForEmailToSMS(string EmailTo, string subject, string msgBody)
        {
            try
            {
                SendMailBAL mail = new SendMailBAL();
                mail.SendEmail(EmailTo, subject, msgBody, ClientId);
                //mail.SendEmailForEmailToSMS(EmailTo, subject, msgBody);
            }
            catch (Exception) { }
        }
    }
}