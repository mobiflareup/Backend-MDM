
using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddUserDevice : Base
    {
        DDLBAL ddl;
        UserDeviceBAL udbl;
        SendSMSBAL sms;
        DataTable dt;
        int ClientId, RoleId, UserId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                BindUserDeviceDDL(ddlUserName);
                Reset();
            }

        }
        protected void BindUserDeviceDDL(DropDownList ddlSrchUser)
        {
            ListItem ls = new ListItem("Select", "0");
            try
            {
                ddl = new DDLBAL();
                ddl.ClientId = ClientId;
                ddl.UserId = UserId;
                ddl.DeptId = DeptId;
                ddlSrchUser.Items.Clear();
                ddlSrchUser.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlSrchUser.DataSource = ddl.GetUserByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlSrchUser.DataSource = ddl.GetUserByDptHead();
                }
                else
                {
                    ddlSrchUser.Items.Clear();
                    ddlSrchUser.DataSource = ddl.GetUserByUserId();
                }
                ddlSrchUser.DataTextField = "UserName";
                ddlSrchUser.DataValueField = "UserId";
                ddlSrchUser.DataBind();
            }
            catch (Exception) { }
            finally
            {
                ls = null;
                ddl = null;
            }
        }
        protected void Reset()
        {
            ddlUserName.SelectedIndex = 0;
            txtMobileNo.Text = string.Empty;
            txtDeviceName.Text = string.Empty;
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            string ClientCode = "", UserCode = "";
            try
            {
                if (ChkValidation())
                {
                    udbl = new UserDeviceBAL();
                    udbl.ClientId = ClientId;
                    udbl.UserId = Convert.ToInt32(ddlUserName.SelectedValue.ToString());
                    udbl.UserName = ddlUserName.SelectedItem.Text.Trim();
                    udbl.MobileNo1 = txtMobileNo.Text.Trim();
                    udbl.DeviceName = txtDeviceName.Text.Trim();
                    string result = udbl.GetUserdevice();
                    if (int.Parse(result) > 0)
                    {
                        lblMsg.Text = "User Device Add successfully";
                        dt = new DataTable();
                        dt = udbl.GetUserCodeClientCodeByUserId();
                        if (dt.Rows.Count > 0)
                        {
                            ClientCode = dt.Rows[0]["ClientCode"].ToString();
                            UserCode = dt.Rows[0]["UserCode"].ToString();
                        }
                        sms = new SendSMSBAL();
                        sms.sendFinalSMS(txtMobileNo.Text.Trim(), "Dear " + ddlUserName.SelectedItem.Text.Trim() + " , You have registerd on MobiOcean by " + Session["UserName"] + ". Please use the below URL to download the MobiOcean APP: https://Mobiocean.com. After download you use the below info to activate the app. Client Code:" + ClientCode + "   User Code: " + UserCode + " Mobile No:" + txtMobileNo.Text.Trim() + " ", ClientId);
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("UsrDevicemstr.aspx");
                    }
                    else
                    {
                        lblMsg.Text = "Device already exists";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "Please fill all the fields";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception) { }
            finally
            {
                udbl = null;
            }
        }
        protected bool ChkValidation()
        {
            if (ddlUserName.SelectedIndex < 0 || Convert.ToInt32(ddlUserName.SelectedValue) <= 0 || txtDeviceName.Text.Trim() == "" || txtMobileNo.Text.Trim() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsrDevicemstr.aspx");
        }
    }
}