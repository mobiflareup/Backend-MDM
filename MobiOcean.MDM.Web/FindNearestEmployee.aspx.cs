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
    public partial class FindNearestEmployee : Base
    {
        int ClientId, UserId, RoleId, DeptId, EmpId;
        AnuSearch srch;
        DataTable dt;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            try
            {
                EmpId = Convert.ToInt32(Session["CurrentUserId"].ToString());
            }
            catch (Exception) { EmpId = 0; }
            if (EmpId == 0)
            {
                Response.Redirect("SosReport.aspx");
            }
            if (!IsPostBack)
            {
                BindGrid();
                //ShowOnMap();
            }

        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
            //ShowOnMap();
        }
        //protected void ShowOnMap()
        //{
        //    PrintOnMap((DataTable)ViewState["Nearest"]);        
        //}
        protected string ShowOnMap()
        {
            srch = new AnuSearch();

            try
            {
                int otheruserid, imgid;
                DataTable dt = new DataTable();
                string msg = "[";
                dt = (DataTable)ViewState["Nearest"];
                foreach (DataRow row in dt.Rows)
                {
                    string marker = "";
                    otheruserid = Convert.ToInt32(row["UserId"].ToString());
                    if (EmpId == otheruserid)
                    {
                        imgid = 1;
                    }
                    else
                    {
                        imgid = 0;
                    }
                    if (imgid == 0)
                    {
                        marker = "images/Map_greenMarker.png";
                    }
                    else
                    {
                        marker = "images/Map_RedMarker.png";
                    }
                    msg = @"" + msg + "{\"title\": \"" + row["Location"].ToString() + "\",\"lat\":\"" + row["Latitude"].ToString() + "\",\"lng\": \"" + row["Longitude"].ToString() + "\",\"description\": \"" + row["LogDateTime"].ToString() + "\",\"MobileNo\": \"" + row["MobileNo"].ToString() + "\",\"Name\": \"" + row["UserName"].ToString() + "\",\"marker\": \"" + marker + "\"},";
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



                //}
                // PrintOnMap(srch.DeviceLocationSrchForMapPointers(ClientId, UserId.ToString(), "", "27 Mar 2016", "27 Apr 2016"));//.UserDashBoardDtls(UserId));

            }
            finally
            {
                srch = null;
            }
        }
        protected void PrintOnMap(DataTable dt)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RemoveMarkersFromArray", "RemoveMarkersFromArray();", true);

            if (dt.Rows.Count > 0)
            {
                lblMsg.Text = "";
                string myLogDateTime = "";
                string myLat = "";
                string myLong = "";
                string myLocation = "";
                int isFirstTime = 1;
                int myuser, imgid;
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
                    myuser = Convert.ToInt32((dt.Rows[index]["UserId"]));
                    if (EmpId == myuser)
                    {
                        imgid = 1;
                    }
                    else
                    {
                        imgid = 0;
                    }

                    try
                    {
                        if (myLocation.Trim() != Convert.ToString(dt.Rows[index + 1]["Location"]).Trim())
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "My Map" + index.ToString(), "ShowOnMap( '" + isFirstTime + "','"
                                + myLogDateTime + "','" + myLocation + "','"
                            + myLat + "','" + myLong + "','" + imgid + "');", true);

                            isFirstTime = 0;
                        }
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "My Map" + index.ToString(), "ShowOnMap( '" + isFirstTime + "','"
                                    + myLogDateTime + "','" + myLocation + "','"
                                + myLat + "','" + myLong + "','" + imgid + "');", true);
                        isFirstTime = 0;
                    }
                }

                //--------- Now we call the function which show all the markers in windwos size ------
                try
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showAllMarkersOnWindow", "showAllMarkersOnWindow();", true);
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
        protected string MyFormat(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                return Convert.ToDateTime(date).ToString("dd-MMM-yyyy HH:mm");
            }
            return date;
        }
        protected void BindGrid()
        {
            try
            {


                
                dt = new DataTable();               
                int Duration = Convert.ToInt32(string.IsNullOrEmpty(txtDuration.Text.Trim()) ? "0" : txtDuration.Text.Trim());
                string LocReq = string.IsNullOrEmpty(txtRadius.Text.Trim()) ? "50" : txtRadius.Text.Trim();// "50";// txtRadius.Text.Trim();
                dt = FindNearestEmp(EmpId, LocReq, Duration);
                ViewState["Nearest"] = dt;
                grdUser.DataSource = dt;
                grdUser.DataBind();
            }
            catch (Exception)
            {
                lblMsg.Text = "Please insert the correct Duration or Radius";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }


        private DataTable FindNearestEmp(int empId,string LocReq, int Duration)
        {
            DataTable dtFinal = new DataTable();
            try
            {
                srch = new AnuSearch();
                dt = new DataTable();
                DataTable dtUserLastLoc =  srch.SrchUserLastLocationByUserId(empId, ClientId, "0", 0, 1);
                if (dtUserLastLoc.Rows.Count > 0)
                {
                    dt = srch.SrchUserLastLocationByUserId(empId, ClientId, LocReq, Duration, 0, GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy HH:mm"));
                    dtFinal = dt.Clone();
                    dtFinal.Clear();
                    if (!(string.IsNullOrEmpty(LocReq) || LocReq == "0"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            double Distance;
                            LocationBAL gglapi = new LocationBAL();
                            Distance = 0;
                            Distance = gglapi.getDistanceFromLatLonInMtr(double.Parse(dtUserLastLoc.Rows[0]["Latitude"].ToString()), double.Parse(dtUserLastLoc.Rows[0]["Longitude"].ToString()), double.Parse(row["Latitude"].ToString()), double.Parse(row["Longitude"].ToString()));
                            if ((Distance / 1000) <= double.Parse(LocReq))
                            {
                                dtFinal.Rows.Add(row["UserId"], row["UserName"], row["MobileNo"], row["DeviceName"], row["LocReq"], row["Location"], row["Latitude"], row["longitude"], row["LogDateTime"]);
                            }
                        }
                    }
                    dtFinal.Rows.Add(dtUserLastLoc.Rows[0]["UserId"], dtUserLastLoc.Rows[0]["UserName"], dtUserLastLoc.Rows[0]["MobileNo"], dtUserLastLoc.Rows[0]["DeviceName"], dtUserLastLoc.Rows[0]["LocReq"], dtUserLastLoc.Rows[0]["Location"], dtUserLastLoc.Rows[0]["Latitude"], dtUserLastLoc.Rows[0]["longitude"], dtUserLastLoc.Rows[0]["LogDateTime"]);
                }
                return dtFinal;
            }
            catch (Exception)
            {
                return dtFinal;
            }
        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView grdUser = (DataRowView)e.Row.DataItem;
                    Label lblId = (Label)e.Row.FindControl("lblId");
                    if (lblId.Text == EmpId.ToString())
                    {
                        e.Row.Attributes["style"] = "background-color: #f16a24";
                    }
                }
            }
            catch (Exception) { }
        }
    }
}