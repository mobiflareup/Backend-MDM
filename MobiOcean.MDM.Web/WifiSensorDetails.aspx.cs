using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class WifiSensorDetails : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AnuSearch srch;
        SensorBAL sensor;
        string Msgtxt, MobileNo;
        SendSMSBAL sms;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
            }
            if (RoleId == 1)
            {
                grdwifisensor.Columns[7].Visible = false;
            }
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            grdwifisensor.DataSource = srch.GetSensorDetails(txtSName.Text.Trim(), ClientId);
            grdwifisensor.DataBind();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void grdwifisensor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdwifisensor.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void grdwifisensor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdwifisensor.EditIndex = -1;
            BindGrid();
        }
        protected void grdwifisensor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdwifisensor.Rows[e.RowIndex];
            try
            {
                string sensorname = "";
                sensor = new SensorBAL();
                lblalertid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                sensorname = ((Label)gvr.FindControl("lblSensorName")).Text.Trim();
                lblUser.Text = "Are you sure to delete " + sensorname + " details?";
                mpdelete.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void grdwifisensor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdwifisensor.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdwifisensor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdwifisensor.Rows[e.RowIndex];
            try
            {
                sensor = new SensorBAL();
                sensor.SensorId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                sensor.BranchId = Convert.ToInt32(((Label)gvr.FindControl("lblbId")).Text.Trim());
                sensor.DepartmentId = Convert.ToInt32(((Label)gvr.FindControl("lbldId")).Text.Trim());
                sensor.ClientId = ClientId;
                sensor.SensorName = ((Label)gvr.FindControl("lblSensorName")).Text.Trim();
                sensor.Description = ((TextBox)gvr.FindControl("txtDescription")).Text.Trim();
                sensor.BSSID = ((TextBox)gvr.FindControl("txtBSSID")).Text.Trim();
                sensor.SSID = ((TextBox)gvr.FindControl("txtSSID")).Text.Trim();
                sensor.Password = ((TextBox)gvr.FindControl("txtPassword")).Text.Trim();
                sensor.CreatedBy = UserId.ToString();
                int res = sensor.UpdateSensor();
                if (res > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdwifisensor.EditIndex = -1;
                    msgalert();
                    BindGrid();
                }
                else
                {

                    lblMsg.Text = "Not updated";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    grdwifisensor.EditIndex = -1;
                    BindGrid();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            sensor = new SensorBAL();
            sensor.SensorId = Convert.ToInt32(lblalertid.Text);
            int res = sensor.DeleteSensor();
            if (res > 0)
            {
                lblMsg.Text = "Deleted Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                grdwifisensor.EditIndex = -1;
                msgalert();
                BindGrid();
            }
            else
            {

                lblMsg.Text = "Not Deleted";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                grdwifisensor.EditIndex = -1;
                BindGrid();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalertid.Text = "";
        }
        private void msgalert()
        {
            dt = new DataTable();
            sensor = new SensorBAL();
            sensor.ClientId = ClientId;
            dt = sensor.GetmobileNos();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MobileNo = dt.Rows[i]["MobileNo"].ToString();

                try
                {
                    if (!string.IsNullOrEmpty(MobileNo))
                    {
                        Msgtxt = "GBox set as WP7";
                        sms = new SendSMSBAL();
                        sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}