using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class DeviceInfo : Base
    {
        int ClientId, UserId, RoleId, DeptId, DeviceId = 0;

        UserBAL usr;
        DDLBAL ddlbal;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            DeviceId = !string.IsNullOrEmpty(Request.QueryString["Id"]) ? Convert.ToInt32(Request.QueryString["Id"].ToString()) : 0;
            if (!IsPostBack)
            {
                BindUsrName();
                BindFormView();
            }
        }
        protected void BindFormView()
        {
            try
            {
                usr = new UserBAL();
                if (DeviceId == 0)
                {
                    try
                    {
                        usr.DeviceId = Convert.ToInt32(ddlUserName.SelectedValue);
                    }
                    catch (Exception)
                    {
                        usr.DeviceId = 0;
                    }
                }
                else
                {
                    usr.DeviceId = DeviceId;
                }
                dt = usr.GetUserDeviceInfoByDeviceId();
                if (dt.Rows.Count > 0)
                {
                    fvDeviceInfo.DataSource = dt;
                    fvDeviceInfo.DataBind();
                }
                else
                {
                    lblMsg.Text = "No Data Found!!!";
                    errorMsg.Visible = true;
                }
            }
            catch (Exception)
            { }
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
                //ddlUserName.Items.Add(new ListItem("--Select Device---", "0"));
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
                ddlUserName.SelectedValue = DeviceId.ToString();
            }
            catch (Exception)
            {

            }
            finally
            {
                ddlbal = null;
            }
        }
        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("DeviceInfo.aspx?Id=" + ddlUserName.SelectedValue);
        }
        protected string Format(string data)
        {
            string sname = "", svalue = "", res = "";
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                sensor[] sensor = js.Deserialize<sensor[]>(data);
                foreach (var obj in sensor)
                {
                    sname = obj.sensorname;
                    svalue = obj.sensorvalue;
                    res = res + ", " + sname + "(" + svalue + ")";
                }
                if (res != "" && res.Length > 1)
                    return res.Substring(1);
                else
                    return "---";
            }
            catch (Exception)
            {
                return "---";
            }
        }
    }
}