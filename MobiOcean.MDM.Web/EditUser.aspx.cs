using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
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
    public partial class EditUser : Base
    {
        UserBAL user;
        usrBAL usr;
        DataTable dt, dt1;
        DeptBAL Dept;
        RoleBAL role;
        //ClientBAL clientbal;
        //UserDeviceBAL udbl;
        SendSMSBAL sms;
        PermisesBAL perm;
        int ClientId, UserId, RoleId, DeptId, EmpId;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblpopmsg.Text = string.Empty;
            EmpId = Convert.ToInt32(Request.QueryString["Id"]);
            if (EmpId == 0)
            {
                Response.Redirect("~/UserMaster.aspx");
            }
            if (!IsPostBack)
            {
                dt1 = new DataTable();
                perm = new PermisesBAL();
                dt1 = perm.GetCountries();
                ViewState["GetCountry"] = dt1;
                if (RoleId == 3 || RoleId == 4)
                {
                    DDLrole.Enabled = false;
                }
                BindBranchDropdown();
                BindDeptDDL();
                BindUserRoleDDL();
                BindUserOwnerShip(drpOwner);
                BindReptMgrDDL();
                BindOldData();
                BindGrid();
                BindCountryddl(1);
            }
        }
        protected void BindDeptDDL()
        {
            ListItem ls = new ListItem("--- Select ---", "0");
            try
            {
                Dept = new DeptBAL();
                Dept.ClientId = ClientId;
                ddlDept.Items.Clear();
                ddlDept.Items.Add(ls);
                ddlDept.DataSource = Dept.GetDptNameDDL();
                ddlDept.DataTextField = "DeptName";
                ddlDept.DataValueField = "DeptId";
                ddlDept.DataBind();
            }
            catch (Exception) { }
            finally
            {
                Dept = null;
                ls = null;
            }

        }
        protected void BindUserRoleDDL()
        {
            ListItem ls = new ListItem("--- Select ---", "0");
            try
            {
                role = new RoleBAL();
                role.ClientId = ClientId;
                DDLrole.Items.Clear();
                DDLrole.Items.Add(ls);
                DDLrole.DataSource = role.GetRoleDDL();
                DDLrole.DataTextField = "RoleName";
                DDLrole.DataValueField = "RoleId";
                DDLrole.DataBind();
                if (RoleId > 1)
                {
                    DDLrole.Items.Remove(new ListItem("Super Admin", "1"));
                }
            }
            catch (Exception) { }
            finally
            {
                role = null;
                ls = null;
            }

        }
        protected void BindReptMgrDDL()
        {
            ListItem ls = new ListItem("--- Select ---", "0");
            try
            {

                usr = new usrBAL();
                usr.ClientId = ClientId;
                ddlRportngMngr.Items.Clear();
                ddlRportngMngr.Items.Add(ls);
                ddlRportngMngr.DataSource = usr.GetReportingMgr();
                ddlRportngMngr.DataTextField = "UserName";
                ddlRportngMngr.DataValueField = "UserId";
                ddlRportngMngr.DataBind();
            }
            catch (Exception) { }
            finally
            {
                usr = null;
                ls = null;
            }

        }
        protected void BindUserOwnerShip(DropDownList ddl)
        {
            ListItem ls = new ListItem("--- Select ---", "0");
            try
            {
                role = new RoleBAL();
                ddl.Items.Clear();
                ddl.Items.Add(ls);
                ddl.DataSource = role.GetOwnerShipName();
                ddl.DataTextField = "OwnerName";
                ddl.DataValueField = "OwnerId";
                ddl.DataBind();
            }
            catch (Exception) { }
            finally
            {
                role = null;
                ls = null;
            }

        }
        private void BindBranchDropdown()
        {
            Dept = new DeptBAL();
            try
            {
                ListItem li1 = new ListItem("--- Select Branch ---", "0");
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(li1);
                Dept.ClientId = ClientId;
                ddlBranch.DataSource = Dept.GetBranchName();
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                Dept = null;

            }
        }
        protected void BindOldData()
        {
            try
            {
                user = new UserBAL();
                dt = new DataTable();
                user.UserId = EmpId;
                dt = user.GetUserDtlByUserId();
                ViewState["Dt"] = dt;
                UTextBox1.Text = dt.Rows[0]["EmpCompanyId"].ToString();
                UTextBox2.Text = dt.Rows[0]["UserName"].ToString();
                //UTextBox3.Text = dt.Rows[0]["MobileNo"].ToString();
                txtdst.Text = dt.Rows[0]["Designation"].ToString();
                UTextBox4.Text = dt.Rows[0]["EmailId"].ToString();
                try
                {
                    ddlBranch.SelectedValue = dt.Rows[0]["BranchId"].ToString();
                }
                catch (Exception)
                {
                    ddlBranch.SelectedValue = "0";
                }
                try
                {
                    ddlDept.SelectedValue = dt.Rows[0]["DeptId"].ToString();
                }
                catch (Exception)
                {
                    ddlDept.SelectedValue = "0";
                }
                try
                {
                    DDLrole.SelectedValue = dt.Rows[0]["RoleId"].ToString();
                }
                catch (Exception)
                {
                    DDLrole.SelectedValue = "4";
                }
                try
                {
                    ddlRportngMngr.SelectedValue = dt.Rows[0]["RptMngrId"].ToString();
                }
                catch (Exception)
                {
                    ddlRportngMngr.SelectedValue = "0";
                }
                //BindGrid();




            }
            catch (Exception) { }
            finally
            {
                dt = null;
                user = null;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkValidation())
                {
                    if (ddlBranch.SelectedValue != "0" && ddlDept.SelectedValue != "0")
                    {
                        if (((DataTable)ViewState["MobileNos"]).Rows.Count > 0 || !string.IsNullOrEmpty(txtmob.Text.Trim()))
                        {

                            string MobileNoList = "", MobileNo = "";
                            DataTable dtmovb = (DataTable)ViewState["MobileNos"];
                            if (!string.IsNullOrEmpty(txtmob.Text.Trim()))
                            {
                                bool canadd = true;
                                foreach (DataRow row in dtmovb.Rows)
                                {
                                    if (row["MobileNo1"].ToString() == txtmob.Text.Trim())
                                    {
                                        canadd = false;
                                        break;
                                    }
                                }
                                if (canadd)
                                {

                                    dtmovb.Rows.Add(0, txtmob.Text.Trim(), 0);
                                }
                            }
                            foreach (DataRow row in dtmovb.Rows)
                            {
                                MobileNoList = MobileNoList + "," + row["MobileNo1"].ToString();
                            }
                            if (MobileNoList.IndexOf(',') == 0)
                            {
                                MobileNoList = MobileNoList.Substring(1);
                            }
                            MobileNo = MobileNoList.IndexOf(',') > 0 ? MobileNoList.Substring(0, MobileNoList.IndexOf(',')) : MobileNoList;
                            string res = SaveUserAndDevice(ClientId, UTextBox1.Text.Trim(), UTextBox2.Text.Trim(), MobileNoList, MobileNo, UTextBox4.Text.Trim(),
                                    Convert.ToInt32(DDLrole.SelectedValue.ToString()), txtdst.Text.Trim(), Convert.ToInt32(ddlRportngMngr.SelectedValue.ToString()), Convert.ToInt32(drpOwner.SelectedValue.ToString()), dtmovb);
                            if (int.Parse(res) == -1)
                            {
                                lblpopmsg.Text = "One of the Mobile No already exists!";
                                lblpopmsg.ForeColor = System.Drawing.Color.Red;
                            }
                            else if (int.Parse(res) == 0)
                            {
                                lblpopmsg.Text = "Email Id or Employee Id already exists!";
                                lblpopmsg.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                if (UserId == EmpId)
                                {
                                    Session["UserName"] = UTextBox2.Text.Trim();
                                }
                                MP1.Show();
                                //lblpopmsg.Text = "User saved successfully.";
                                //lblpopmsg.ForeColor = System.Drawing.Color.Green;

                            }

                        }
                        else
                        {
                            lblpopmsg.Text = "Please add mobile no.";
                            lblpopmsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblpopmsg.Text = "Please select branch and department";
                        lblpopmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblpopmsg.Text = "Please fill all the mandatory field";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "OpenAgain();", true);
                }
            }
            catch (Exception) { }
        }
        protected void ok_Click(object sender, EventArgs e)
        {
            if (RoleId <= 3)
            {
                Response.Redirect("UserMaster.aspx");
            }
            else
            {
                Response.Redirect("Userprofile.aspx");
            }
        }
        protected string SaveUserAndDevice(int ClientId, string UserCode, string UserName, string MobileNoList, string MobileNo, string EmailId, int RoleId, string Designation, int RptMngrId, int Ownership, DataTable dtmovb)
        {
            usr = new usrBAL();
            usr.UserId = EmpId;
            usr.ClientId = ClientId;
            usr.UserCode = UserCode;
            usr.UserName = UserName;
            usr.MobileNoList = MobileNoList;
            usr.MobileNo = MobileNo;
            usr.EmailId = EmailId;
            usr.DeptId = Convert.ToInt32(ddlDept.SelectedValue.ToString());
            usr.RoleId = RoleId;
            usr.EmpCompanyId = UserCode;
            usr.Branch = Convert.ToInt32(ddlBranch.SelectedValue.ToString());
            usr.Designation = Designation;
            //user.PreferredContactNo = DDLContactNo.SelectedValue.ToString();
            //usr.Password = Password;
            usr.RptMngrId = RptMngrId;

            //user.TempAddress = UTextBox7.Text.Trim();
            // user.Gender = ddlgender0.SelectedValue.ToString();
            usr.Country = "1";
            usr.LoggedBy = UserId.ToString();
            //user.ProfileImagePath = lblimagepath.Text.Trim();
            dt = new DataTable();
            dt = usr.InsertUserWithMultipleDevice();
            if (int.Parse(dt.Rows[0]["UserId"].ToString()) == -1)
            {
                return "-1";
            }
            else if (int.Parse(dt.Rows[0]["UserId"].ToString()) == 0)
            {
                return "0";
            }
            else
            {
                DataTable dtold = new DataTable();
                dtold = (DataTable)ViewState["Dt"];
                try
                {
                    if (Convert.ToInt32(dtold.Rows[0]["RoleId"].ToString()) == 4 && Convert.ToInt32(dtold.Rows[0]["RoleId"].ToString()) > RoleId)
                    {
                        LoginBAL lgn = new LoginBAL();
                        lgn.ClientId = ClientId;
                        lgn.UserID = int.Parse(dtold.Rows[0]["UserId"].ToString());
                        lgn.RoleId = RoleId;
                        lgn.DeptId = Convert.ToInt32(ddlDept.SelectedValue.ToString());
                        lgn.UserName = UserName;
                        lgn.LoginKey = GenPass(28,"");
                        lgn.currentDateTime = GetCurrentDateTimeByUserId();
                        lgn.InsertFirstLoginData();
                        SendRoleUpgradtionMail(UserName, dtold.Rows[0]["Password"].ToString(), EmailId, Session["UserName"].ToString(), lgn.LoginKey);
                    }
                }
                catch (Exception)
                { }
                //DataRow[] rows = dtmovb.Select("DeviceId=0");
                usr.DeviceOwnerShip = Ownership;
                usr.UserId = EmpId;
                usr.UpdateOwnerShip();
                //foreach (DataRow row in rows)
                //{
                //    usr = new usrBAL();
                //    usr.DeviceId = 0;
                //    usr.UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
                //    usr.DeviceName = row["MobileNo1"].ToString();
                //    usr.MobileNo1 = row["MobileNo1"].ToString();
                //    usr.ClientId = ClientId;
                //    usr.DeviceOwnerShip = Ownership;
                //    int DeviceId = Convert.ToInt32(usr.InsertUserDeviceData());
                //    if (DeviceId > 0)
                //    {
                //        SendMsgToDevice(DeviceId, int.Parse(dt.Rows[0]["UserId"].ToString()), row["MobileNo1"].ToString());
                //    }
                //}
                return "1";
            }
        }       
        private void SendRoleUpgradtionMail(string UserName, string Password, string EmailId, string ByUser, string LoginKey)
        {
            SendMailBAL mail = new SendMailBAL();
            mail.UserUpgradation(UserName, Password, EmailId, ByUser, LoginKey, ClientId);
        }       
        private void SendMsgToDevice(int DeviceId, int UserId, string MObileNO)
        {
            sms = new SendSMSBAL();
            string ClientCode, UserCode;
            DataTable dtdevice = new DataTable();
            UserDeviceBAL udbl = new UserDeviceBAL();
            udbl.UserId = UserId;
            dtdevice = udbl.GetUserCodeClientCodeByUserId();
            if (dtdevice.Rows.Count > 0)
            {
                ClientCode = dtdevice.Rows[0]["ClientCode"].ToString();
                UserCode = dtdevice.Rows[0]["UserCode"].ToString();
                string downloadlink = ClientId == 399 ? Constant.LTAppDownloadUrl : Constant.CommonAppUrl;//: Constant.AppDownloadUrl;
                sms.sendMsgUsingSMS("Dear " + UTextBox2.Text.Trim() + ", You have been registered on MobiOcean by " + Session["UserName"] + ". Please use the below URL to download the MobiOcean APP: " + downloadlink + " . After downloading, please use the below info to activate the APP. Mobile No:" + MObileNO + " ", MObileNO, ClientId);//Client Code:" + ClientCode + "   User Code: " + UserCode + " 
                //sms.sendMsgUsingSMS("Dear " + UTextBox2.Text.Trim() + ", You have been registered on MobiOcean by " + Session["UserName"] + ". Please use the below URL to download the MobiOcean APP: " + Constant.AppDownloadUrl + " . After downloading, please use the below information to activate the APP: Client Code:" + ClientCode + "   User Code: " + UserCode + " Mobile No:" + MObileNO + " ", MObileNO, ClientId);
            }
        }
        protected bool ChkValidation()
        {
            if (ClientId <= 0 || UTextBox2.Text.Trim() == "" || UTextBox1.Text.Trim() == "" || UTextBox4.Text.Trim() == "" || DDLrole.SelectedIndex <= 0)//|| txtpwd.Text.Trim() == "" || txtCnfrmPwd.Text.Trim() == ""
            {
                return false;
            }
            else
            {
                if (ddlRportngMngr.Items.Count > 1 && ddlRportngMngr.SelectedIndex <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (RoleId <= 3)
            {
                Response.Redirect("UserMaster.aspx");
            }
            else
            {
                Response.Redirect("Userprofile.aspx");
            }
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            int CountryId = Convert.ToInt32(ddlCountry.SelectedItem.Value);
            dt = (DataTable)ViewState["GetCountry"];
            txtCode.Text = dt.Rows[CountryId - 1]["PhoneCode"].ToString();
        }
        protected void BindGrid()
        {
            DataTable dtmobile = new DataTable();
            DataTable dtrslt = new DataTable();
            dtmobile.Columns.Add("DeviceId");
            dtmobile.Columns.Add("MobileNo1");
            dtmobile.Columns.Add("CountryCode");
            dtmobile.Columns.Add("CountryId");
            dtmobile.Columns.Add("Status");
            user = new UserBAL();
            user.UserId = EmpId;
            dtrslt = user.GetMobileNoByUserIdRaj();
            if (dtrslt.Rows.Count > 0)
            {
                try
                {
                    drpOwner.SelectedValue = dtrslt.Rows[0]["OwnershipId"].ToString();
                }
                catch (Exception)
                {
                    drpOwner.SelectedValue = "0";
                }
                foreach (DataRow row in dtrslt.Rows)
                {
                    dtmobile.Rows.Add(row["DeviceId"].ToString(), row["MobileNo1"].ToString(), row["PhoneCode"].ToString(), row["CountryId"].ToString(), row["Status"].ToString());
                }
            }
            ViewState["MobileNos"] = dtmobile;
            grdNo.DataSource = dtrslt;
            grdNo.DataBind();
        }
        protected void BindCountryddl(int i)
        {
            #region--------- Get School List --------
            try
            {
                ListItem li = new ListItem("--- Select Country ---", "0");
                ddlCountry.Items.Clear();
                ddlCountry.Items.Add(li);
                ddlCountry.DataSource = (DataTable)ViewState["GetCountry"];
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
            }
            catch (Exception)
            {
            }
            #endregion
        }
        protected void btnAddMobileNo_Click(object sender, EventArgs e)
        {
        }
        protected void btnAdd12_Click(object sender, EventArgs e)
        {
            if (txtmob.Text.Trim() != "")
            {
                if (txtmob.Text.Length > 2 && txtmob.Text.Length < 16)
                {
                    try
                    {
                        usr = new usrBAL();
                        usr.DeviceId = 0;
                        usr.ClientId = ClientId;
                        usr.UserId = EmpId;
                        usr.UserName = null;
                        usr.DeviceName = null;
                        usr.MobileNo1 = txtmob.Text;
                        usr.CountryId = ddlCountry.SelectedItem.Value;
                        usr.DeviceOwnerShip = Convert.ToInt32(drpOwner.SelectedValue.ToString());
                        int res = Convert.ToInt32(usr.InsertUserDeviceDataRaj());
                        if (res > 0)
                        {
                            BindGrid();
                            SendMsgToDevice(res, EmpId, txtmob.Text);
                            ddlCountry.SelectedIndex = 0;
                            txtCode.Text = string.Empty;
                            txtmob.Text = string.Empty;
                            lblMobin.Text = "Mobile No. added sucessfully";
                            lblMobin.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMobin.Text = "Mobile No. already registered";
                            lblMobin.ForeColor = System.Drawing.Color.Red;
                        }

                    }
                    catch
                    {
                        lblMobin.Text = "Something went wrong!";
                        lblMobin.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMobin.Text = "Only allowed 3-15 Digit Mobile No";
                    lblMobin.ForeColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                lblMobin.Text = "Enter Mobile No.";
                lblMobin.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void grdNo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (grdNo.Rows.Count > 1)
            {
                GridViewRow gvr = grdNo.Rows[e.RowIndex];
                string lblDtlId = ((Label)gvr.FindControl("lblId")).Text;
                string MobNo = ((TextBox)gvr.FindControl("txtgdvn")).Text;
                user = new UserBAL();
                try
                {


                    user.ClientId = ClientId;
                    user.DeviceId = Convert.ToInt32(lblDtlId);
                    int res = user.DeleteUserDeviceDtlsMobNo();
                    if (res > 0)
                    {

                        lblMobin.Text = "Mobile No. Deleted sucessfully";
                        lblMobin.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMobin.Text = "Mobile No. not deleted";
                        lblMobin.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch
                {
                    lblMobin.Text = "Something wrong!";
                    lblMobin.ForeColor = System.Drawing.Color.Red;
                }
                //dt = new DataTable();
                //dt = (DataTable)ViewState["MobileNos"];
                //DataRow[] rows = dt.Select("DeviceId=" + lblDtlId + "");
                //foreach (DataRow row in rows)
                //{
                //    row["Status"] = 1;
                //}
                //ViewState["MobileNos"] = dt;
                BindGrid();
                //grdNo.DataSource = dt;
                //grdNo.DataBind();
            }
            else
            {
                lblMobin.Text = "Must need a number. You Can't Delete ";
                lblMobin.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void grdNo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdNo.EditIndex = e.NewEditIndex;

            BindGrid();
        }
        protected void grdNo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdNo.EditIndex = -1;
            BindGrid();
        }
        protected void grdNo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow gvr = grdNo.Rows[e.RowIndex];
            string lblDtlId = ((Label)gvr.FindControl("lblId")).Text;
            string MobNo = ((TextBox)gvr.FindControl("txtgdvedit")).Text;
            string CountryId = ((DropDownList)gvr.FindControl("ddlCountryedit")).SelectedItem.Value;
            usr = new usrBAL();
            try
            {
                if (MobNo.Trim() != "")
                {
                    if (MobNo.Length > 2 && MobNo.Length < 16)
                    {
                        usr.CountryId = CountryId;
                        usr.UserId = EmpId;
                        usr.ClientId = ClientId;
                        usr.MobileNo1 = MobNo;
                        usr.DeviceId = Convert.ToInt32(lblDtlId);
                        int res = Convert.ToInt32(usr.InsertUserDeviceDataRaj());
                        if (res > 0)
                        {
                            SendMsgToDevice(Convert.ToInt32(lblDtlId), UserId, MobNo);
                            lblMobin.Text = "Mobile No. updated sucessfully";
                            lblMobin.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblMobin.Text = "Mobile No. already registered!";
                            lblMobin.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblMobin.Text = "Only allowed 3-15 Digit Mobile No";
                        lblMobin.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else
                {
                    lblMobin.Text = "Enter Mobile No.";
                    lblMobin.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch
            {
                lblMobin.Text = "Something went wrong!";
                lblMobin.ForeColor = System.Drawing.Color.Red;
            }
            grdNo.EditIndex = -1;
            BindGrid();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

        }

        protected void grdNo_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlCountryedit");
                    ListItem li = new ListItem("--- Select Country ---", "0");
                    ddList.Items.Clear();
                    ddList.Items.Add(li);
                    //return DataTable havinf department data
                    DataTable dt = (DataTable)ViewState["GetCountry"];
                    ddList.DataSource = dt;
                    ddList.DataTextField = "CountryName";
                    ddList.DataValueField = "CountryId";
                    ddList.DataBind();

                    DataRowView dr = e.Row.DataItem as DataRowView;
                    ddList.SelectedValue = dr["CountryId"].ToString();
                    TextBox txt = (TextBox)e.Row.FindControl("txtCodeedit");
                    txt.Text = dt.Rows[Convert.ToInt32(ddList.SelectedValue) - 1]["PhoneCode"].ToString();
                    //ddList.Items.FindByValue((e.Row.FindControl("txtCodeedit") as TextBox).Text).Selected = true;
                }
            }
        }

        protected void ddlCountryedit_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = new DataTable();

            dt = (DataTable)ViewState["GetCountry"];
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            int CountryId = Convert.ToInt32(ddl.SelectedItem.Value);
            TextBox txtName = (TextBox)row.FindControl("txtCodeedit");
            txtName.Text = dt.Rows[CountryId - 1]["PhoneCode"].ToString();
        }
    }
}