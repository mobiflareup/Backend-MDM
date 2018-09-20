using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class UserDashBoard : Base
    {
        int ClientId, UserId, RoleId, DeptId, help = 0;

        AnuSearch srch;
        AlertBAL alert;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
                help = 1;
                LoadChartData(GetDataCall());
                help = 2;
                LoadChartData(GetDataMessage());
                LoadChartDataApp(GetDataApp());
            }

        }

        private void LoadChartData(DataTable initialDataSource)
        {
            for (int i = 2; i < initialDataSource.Columns.Count; i++)
            {
                Series series = new Series();

                foreach (DataRow dr in initialDataSource.Rows)
                {
                    try
                    {
                        int y = Convert.ToInt32(string.IsNullOrEmpty(dr[i].ToString()) ? "0" : dr[i].ToString());
                        series.Points.AddXY(string.IsNullOrEmpty(dr["date1"].ToString()) ? Convert.ToDateTime(dr["date2"]).ToString("dd MMM yyyy") : Convert.ToDateTime(dr["date1"]).ToString("dd MMM yyyy"), y);
                    }
                    catch (Exception)
                    { }
                }
                if (help == 1)
                {
                    Chart1.Series.Add(series);
                }
                if (help == 2)
                {
                    Chart2.Series.Add(series);
                }
            }
        }
        private void LoadChartDataApp(DataTable initialDataSource)
        {
            for (int i = 1; i < initialDataSource.Columns.Count; i++)
            {
                Series series = new Series();

                foreach (DataRow dr in initialDataSource.Rows)
                {
                    int y = Convert.ToInt32(string.IsNullOrEmpty(dr[i].ToString()) ? "0" : dr[i].ToString());
                    series.Points.AddXY(Convert.ToDateTime(dr["date"]).ToString("dd MMM yyyy"), y);
                }
                Chart3.Series.Add(series);
            }
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            grddashboard.DataSource = srch.UserDashBoardDtls(UserId);
            grddashboard.DataBind();

            //Alert
            alert = new AlertBAL();
            alert.UserId = UserId;
            grdAlert.DataSource = alert.GetAlertDetailsByUserId();
            grdAlert.DataBind();
        }
        protected DataTable GetDataCall()
        {
            DataTable dt1 = new DataTable();
            srch = new AnuSearch();
            dt1 = srch.UserDashCallChart(ClientId, UserId);
            return dt1;
        }
        protected DataTable GetDataMessage()
        {
            DataTable dt1 = new DataTable();
            srch = new AnuSearch();
            dt1 = srch.UserDashSmsChart(ClientId, UserId);
            return dt1;
        }
        protected DataTable GetDataApp()
        {
            DataTable dt1 = new DataTable();
            srch = new AnuSearch();
            dt1 = srch.UserDashAppChart(ClientId, UserId);
            return dt1;
        }
        protected void grdAlert_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAlert.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void grdAlert_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label IsRead = ((Label)e.Row.FindControl("lblIsRead"));
                CheckBox chkbox = ((CheckBox)e.Row.FindControl("chkbox"));
                if (IsRead.Text == "0")
                {
                    chkbox.Checked = false;
                }
                else
                {
                    chkbox.Checked = true;
                    chkbox.Enabled = false;
                }
            }
        }
        protected void grddashboard_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grddashboard.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void lbView_Click(object sender, EventArgs e)
        {
            LinkButton lbView = sender as LinkButton;
            GridViewRow gvr = lbView.NamingContainer as GridViewRow;
            Label lbl = ((Label)grddashboard.Rows[gvr.RowIndex].FindControl("lblId"));
            Response.Redirect("DeviceInfo.aspx?Id=" + lbl.Text + "&IS=0");
        }
        protected string ShowOnMap()
        {
            srch = new AnuSearch();

            try
            {
                DataTable dt = new DataTable();
                string msg = "[";
                dt = srch.UserDashBoardDtls(UserId);
                foreach (DataRow row in dt.Rows)
                {
                    msg = @"" + msg + "{\"title\": \"" + row["Location"].ToString() + "\",\"lat\":\"" + row["Latitude"].ToString() + "\",\"lng\": \"" + row["Longitude"].ToString() + "\",\"description\": \"" + row["LogDateTime"].ToString() + "\"},";
                }
                if (msg.Length > 1)
                {
                    msg = msg.Substring(0, msg.Length - 1);
                }
                msg = msg + "]";
                if (dt.Rows.Count <= 0)
                {
                    msg = "[{\"title\": \"No Location Found\",\"lat\":\"0\",\"lng\": \"0\",\"description\": \"\"}]";
                }
                return msg;

            }
            finally
            {
                srch = null;
            }
        }
        protected void PrintOnMap(DataTable dt)
        {
            string script = "window.onload = function() { initialize(); };";
            ClientScript.RegisterStartupScript(this.GetType(), "initialize", script, true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Call", "initialize();", true);

            ClientScript.RegisterStartupScript(this.GetType(), "RemoveMarkersFromArray", "RemoveMarkersFromArray();", true);

            if (dt.Rows.Count > 0)
            {
                lblMsg.Text = "";

                string myLogDateTime = "";
                string myLat = "";
                string myLong = "";
                string myLocation = "";
                int isFirstTime = 1;

                for (int index = 0; index < dt.Rows.Count; index++)
                {

                    if (index != 0 && myLocation.Trim() == (dt.Rows[index]["Location"]).ToString().Trim())
                    {
                        myLogDateTime = myLogDateTime + ",<br>" + Convert.ToString(dt.Rows[index]["LogDateTime"]);
                    }
                    else
                    {
                        myLogDateTime = Convert.ToString(dt.Rows[index]["LogDateTime"]);
                    }

                    myLat = Convert.ToString(dt.Rows[index]["Latitude"]);
                    myLong = Convert.ToString(dt.Rows[index]["Longitude"]);
                    myLocation = Convert.ToString(dt.Rows[index]["Location"]);


                    try
                    {
                        if (myLocation.Trim() != Convert.ToString(dt.Rows[index + 1]["Location"]).Trim())
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "My Map" + index.ToString(), "ShowOnMap( '" + isFirstTime + "','"
                                + myLogDateTime + "','" + myLocation + "','"
                            + myLat + "','" + myLong + "',0);", true);

                            isFirstTime = 0;
                        }
                    }
                    catch (Exception)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "My Map" + index.ToString(), "ShowOnMap( '" + isFirstTime + "','"
                                    + myLogDateTime + "','" + myLocation + "','"
                                + myLat + "','" + myLong + "',0);", true);
                        isFirstTime = 0;
                    }
                }

                //--------- Now we call the function which show all the markers in windwos size ------
                try
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "showAllMarkersOnWindow", "showAllMarkersOnWindow();", true);
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }
            }
            else
            {

                lblMsg.Text = "No location found for your search.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            //ShowOnMap();
        }
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                alert = new AlertBAL();
                string alertid = "", Removealertid = "";
                for (int idx = 0; idx < grdAlert.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdAlert.Rows[idx].FindControl("chkbox"))).Checked)
                    {
                        alertid = alertid + ((Label)grdAlert.Rows[idx].FindControl("lblId")).Text + ",";
                    }
                    else
                    {
                        Removealertid = Removealertid + ((Label)grdAlert.Rows[idx].FindControl("lblId")).Text + ",";
                    }
                }
                if (alertid != "")
                {
                    alertid = alertid.Substring(0, (alertid.Length) - 1);
                }
                alert.AlertIdList = alertid;
                alert.UserId = UserId;
                int res = alert.MarkAlertDetails();
                if (res > 0)
                {
                    lblmessage.Text = "Changes Applied Successfully";
                    lblmessage.ForeColor = System.Drawing.Color.Green;
                    BindGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                }
                else
                {
                    lblmessage.Text = "Not Applied";
                    lblmessage.ForeColor = System.Drawing.Color.Red;
                    BindGrid();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                }
            }
            catch (Exception)
            {

            }

        }
    }
}