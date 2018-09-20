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
    public partial class utrigger : Base
    {
        DDLBAL ddlbal;
        UserDeviceBAL devicebal;
        DataTable dt, dt1;
        SendSMSBAL sms;
        int ClientId, UserId, RoleId, DeptId;
        string Pin, MobileNo = "", Msgtxt = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                Disablebtn();
                BindUsrName();
                //GetPin();
                EnableFeature();
            }
        }
        protected void BindUsrName()
        {
            try
            {
                ddlbal = new DDLBAL();
                ddlbal.UserId = UserId;
                ddlbal.ClientId = ClientId;
                ddlbal.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(new ListItem("--- Select Device ---", "0"));
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = ddlbal.GetUserDeviceByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = ddlbal.GetUsrDeviceByDeptHead();
                }
                else
                {
                    ddlUserName.DataSource = ddlbal.GetUserDeviceByUserId();
                }
                ddlUserName.DataTextField = "DeviceName";
                ddlUserName.DataValueField = "DeviceId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                ddlbal = null;
            }
        }
        protected void btnPin_Click(object sender, EventArgs e)
        {
        }
        protected void btnGetLoc_Click(object sender, EventArgs e)
        {
            try
            {
                string MobileNo = "";
                GetMobileNoByDeviceId();
                string Msgtxt = "GBox set as";
                sms = new SendSMSBAL();
                sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
            }
            catch (Exception)
            {

            }
        }
        protected void btnLock_Click(object sender, EventArgs e)
        {
            try
            {
                MobileNo = GetMobileNoByDeviceId();
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    Msgtxt = "GBox set as WP4";
                    Trigger("Remote Lock", Msgtxt);
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnSiren_Click(object sender, EventArgs e)
        {
            try
            {
                MobileNo = GetMobileNoByDeviceId();
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    Msgtxt = "GBox set as WP6";
                    Trigger("Remote Trigger", Msgtxt);
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnDeleteDeviceData_Click(object sender, EventArgs e)
        {
            try
            {
                MobileNo = GetMobileNoByDeviceId();
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    Msgtxt = "GBox set as WP5";
                    Trigger("Memory Wipe Out", Msgtxt);
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                }
            }
            catch (Exception)
            {

            }
        }
        protected string GetMobileNoByDeviceId()
        {
            try
            {
                devicebal = new UserDeviceBAL();
                devicebal.DeviceId = Convert.ToInt32(ddlUserName.SelectedValue.ToString());
                dt = new DataTable();
                dt = devicebal.GetDevicefromDeviceID();
                if (dt.Rows.Count > 0)
                {
                    MobileNo = dt.Rows[0]["MobileNo1"].ToString();
                }
            }
            catch (Exception)
            {

            }
            return MobileNo;
        }
        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Disablebtn();
            if (ddlUserName.SelectedIndex != 0)
            {
                txtPin.Visible = true;
                EnableFeature();
                GetPin();
                btnPin.Enabled = true;
                btnforcesync.Enabled = true;
                btnPin.Visible = true;
                txtPin.Enabled = false;
                btnChangePin.Visible = false;
                lblLastLocation.Text = "";
                lblLasttime.Text = "";
            }
        }
        private void EnableFeature()
        {
            devicebal = new UserDeviceBAL();
            devicebal.DeviceId = Convert.ToInt32(ddlUserName.SelectedValue.ToString());
            dt = devicebal.GetFeatureIdAndIsEnable();

            foreach (DataRow row in dt.Rows)
            {
                switch (Convert.ToInt32(row["FeatureId"].ToString()))
                {
                    case 2:
                        if (row["IsEnable"].ToString() != "0")
                        {
                            btnFactoryReset.Enabled = true;
                        }
                        break;
                    case 46:
                        if (row["IsEnable"].ToString() != "0")
                        {
                            btnLock.Enabled = true;
                        }
                        break;
                    //case 37:
                    //    btnGetLoc.Enabled = true;
                    //    break;
                    case 47:
                        if (row["IsEnable"].ToString() != "0")
                        {
                            btnSiren.Enabled = true;
                        }
                        break;
                    case 49:
                        if (row["IsEnable"].ToString() != "0")
                        {
                            btnDeleteDeviceData.Enabled = true;
                            btncontact.Enabled = true;
                            btnSMS.Enabled = true;
                        }
                        break;
                    case 37:
                        if (row["IsEnable"].ToString() != "0")
                        {
                            btnLoction.Enabled = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private void Disablebtn()
        {
            btnPin.Enabled = false;
            btnLock.Enabled = false;
            btnFactoryReset.Enabled = false;
            btnSiren.Enabled = false;
            btnDeleteDeviceData.Enabled = false;
            btnSMS.Enabled = false;
            btncontact.Enabled = false;
            btnforcesync.Enabled = false;
            btnLoction.Enabled = false;
            txtPin.Visible = false;
        }
        protected void GetPin()
        {
            devicebal = new UserDeviceBAL();
            devicebal.DeviceId = Convert.ToInt32(ddlUserName.SelectedValue.ToString());
            dt = devicebal.GetDevicefromDeviceID();
            if (dt.Rows.Count > 0)
            {
                Pin = dt.Rows[0]["PIN"].ToString();
            }
            //btnPin.Text = "Your Pin: "+Pin;        
            txtPin.Text = Pin;
        }
        protected void btnFactoryReset_Click(object sender, EventArgs e)
        {
            try
            {
                MobileNo = GetMobileNoByDeviceId();
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    Msgtxt = "GBox set as WP3";
                    Trigger("Factory Reset", Msgtxt);
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btncontact_Click(object sender, EventArgs e)
        {
            try
            {
                MobileNo = GetMobileNoByDeviceId();
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    Msgtxt = "GBox set as WP0";
                    Trigger("Contact Wipe", Msgtxt);
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnSMS_Click(object sender, EventArgs e)
        {
            try
            {
                MobileNo = GetMobileNoByDeviceId();
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    Msgtxt = "GBox set as WP1";
                    Trigger("Message Wipe", Msgtxt);
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                }
            }
            catch (Exception)
            {

            }
        }
        protected void Trigger(string TriggerType, string MsgTxt)
        {
            devicebal = new UserDeviceBAL();
            devicebal.TriggerType = TriggerType;
            devicebal.MsgTxt = MsgTxt;
            devicebal.UserId = UserId;
            devicebal.LogDateTime = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy HH:mm");
            devicebal.DeviceId = Convert.ToInt32(ddlUserName.SelectedValue.ToString());
            int res = devicebal.InsertIntoTblTrigger();
            if (res > 0)
            {
                lblMsg.Text = "The command has been sent Successfully!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "The command has not been sent!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void forcesync_Click(object sender, EventArgs e)
        {
            try
            {
                MobileNo = GetMobileNoByDeviceId();
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    Msgtxt = "GBox set as WP8";
                    Trigger("Message Wipe", Msgtxt);
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnLoction_Click(object sender, EventArgs e)
        {
            devicebal = new UserDeviceBAL();
            devicebal.DeviceId = Convert.ToInt32(ddlUserName.SelectedValue.ToString());
            dt1 = devicebal.GetLastDeviceLocation();
            try
            {
                lblLastLocation.Text = "( Last Location : " + dt1.Rows[0]["Location"].ToString();
                lblLasttime.Text = "Logged Time : " + Convert.ToDateTime(dt1.Rows[0]["CreationDate"]).ToString("dd MMM yyyy HH MM tt") + " )";
            }
            catch
            {
                lblLastLocation.Text = "No Data";
            }//LocationLV.DataSource = dt1;
             //LocationLV.DataBind();
            try
            {
                MobileNo = GetMobileNoByDeviceId();
                if (!string.IsNullOrEmpty(MobileNo))
                {
                    Msgtxt = "GBox set as WP9";
                    Trigger("Message Wipe", Msgtxt);
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                }
            }
            catch (Exception)
            {

            }
        }
        protected void btnChangePin_Click(object sender, EventArgs e)
        {
            if (ddlUserName.SelectedIndex > 0)
            {
                string Encpin = "";
                devicebal = new UserDeviceBAL();
                devicebal.DeviceId = Convert.ToInt32(ddlUserName.SelectedValue.ToString());
                devicebal.UserId = UserId;
                devicebal.PIN = txtPin.Text.Trim();
                int res = devicebal.UpdatePIN();
                if (res > 0)
                {
                    MobileNo = GetMobileNoByDeviceId();
                    sms = new SendSMSBAL();
                    Encpin = sms.encrypt(txtPin.Text.Trim());
                    Msgtxt = "GBox set as WP10 " + Encpin;
                    Trigger("Message Wipe", Msgtxt);

                    sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                }
                GetPin();
                btnPin.Visible = true;
                btnChangePin.Visible = false;
                txtPin.Enabled = false;
            }
        }
        protected void btnPin_Click1(object sender, EventArgs e)
        {
            if (ddlUserName.SelectedIndex > 0)
            {
                btnPin.Visible = false;
                btnChangePin.Visible = true;
                txtPin.Enabled = true;
                //rfv.Visible = true;
                //rexNumber.Visible = true;
            }
        }
    }
}