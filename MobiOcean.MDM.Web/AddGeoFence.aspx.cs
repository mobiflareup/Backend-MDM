using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.Infrastructure;
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
    public partial class AddGeoFence : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        LocationBAL location;
        GoogleAPI googleApi;
        DataTable dt, dt1;
        protected String LabelProperty
        {
            get
            {
                return hidden.Value;
            }
            set
            {
                hidden.Value = value;
            }
        }
        protected String StartAddress
        {
            get { return hdnStartAddress.Value; }
            set { hdnStartAddress.Value = value; }
        }
        protected String StartLat
        {
            get
            {
                return hdnStartLat.Value;
            }
            set
            {
                hdnStartLat.Value = value;
            }
        }
        protected String StartLong
        {
            get
            {
                return hdnStartLong.Value;
            }
            set
            {
                hdnStartLong.Value = value;
            }
        }
        protected String EndAddress
        {
            get
            {
                return hdnEndAddress.Value;
            }
            set
            {
                hdnEndAddress.Value = value;
            }
        }
        protected String EndLat
        {
            get
            {
                return hdnEndLat.Value;
            }
            set
            {
                hdnEndLat.Value = value;
            }
        }
        protected String EndLong
        {
            get
            {
                return hdnEndLong.Value;
            }
            set
            {
                hdnEndLong.Value = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {

            }

        }
        protected string getdata()
        {
            //location = new LocationBAL();
            //location.RouteId = RouteId;
            dt = new DataTable();
            dt.Columns.Add("Latiude");
            dt.Columns.Add("Longitude");
            try
            {
                dt.Rows.Add(StartLat.Trim(), StartLong.Trim());
                dt.Rows.Add(EndLat.Trim(), EndLong.Trim());
                googleApi = new GoogleAPI();
                string thrd = googleApi.midPoint(double.Parse(StartLat.Trim()), double.Parse(StartLong.Trim()), double.Parse(EndLat.Trim()), double.Parse(EndLong.Trim()));
                dt.Rows.Add(thrd.Substring(0, thrd.IndexOf(',')), thrd.Substring(thrd.IndexOf(',') + 1));
                LabelProperty = StartLat.Trim() + "," + StartLong.Trim() + "," + (thrd.Substring(0, thrd.IndexOf(','))) + "," + (thrd.Substring(thrd.IndexOf(',') + 1)) + "," + EndLat.Trim() + "," + EndLong.Trim();            //28.6524629, 77.1260591),
                                                                                                                                                                                                                                 //   new google.maps.LatLng(, ),
                                                                                                                                                                                                                                 // new google.maps.LatLng(, 77.1232247
                                                                                                                                                                                                                                 //dt = location.GetGeoFenceRouteDetail();
            }
            catch (Exception)
            {
                dt.Rows.Clear();
                dt.Rows.Add("28.6524629", "77.1260591");
                dt.Rows.Add("28.6527392", "77.1250851");
                dt.Rows.Add("28.6534888", "77.1232247");
                LabelProperty = "28.6524629" + "," + "77.1260591" + "," + "28.6527392" + "," + "77.1250851" + "," + "28.6534888" + "," + "77.1232247";
            }
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Clear();
                dt.Rows.Add("28.6524629", "77.1260591");
                dt.Rows.Add("28.6527392", "77.1250851");
                dt.Rows.Add("28.6534888", "77.1232247");
                LabelProperty = "28.6524629" + "," + "77.1260591" + "," + "28.6527392" + "," + "77.1250851" + "," + "28.6534888" + "," + "77.1232247";
            }
            return DataTableToJSONWithJavaScriptSerializer(dt);
            //return "0";

        }

        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {


            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (LabelProperty.Trim() == "")
            {
                lblMsg.Text = "Please draw a route on Map!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                mapCall();
            }
            else if (txtCode.Text.Trim() == "" || txtMobileNo.Text.Trim() == "")
            {
                lblMsg.Text = "Please fill mandatory field!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                string Latlong = LabelProperty.Trim().Substring(0, LabelProperty.Length - 1);
                dt = new DataTable();
                dt.Columns.Add("Latitude");
                dt.Columns.Add("Longitude");
                string[] latlngary = Latlong.Split(',');
                for (int i = 0; i < latlngary.Length; i = i + 2)
                {
                    dt.Rows.Add(latlngary[i].ToString(), latlngary[i + 1].ToString());
                }
                location = new LocationBAL();
                location.ClientId = ClientId;
                location.RouteName = txtMobileNo.Text.Trim();
                location.RouteCode = txtCode.Text;
                location.LoggedBy = UserId.ToString();
                location.RouteDesc = txtDesc.Text;
                location.RouteId = 0;
                location.LatLong = dt;
                int res = Convert.ToInt32(location.IU_RouteGeoFence());
                if (res > 0)
                {
                    lblMsg.Text = "Saved Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                    Response.Redirect("GeoFenceManagement.aspx");
                }
                else
                {
                    lblMsg.Text = "Not Saved";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddGeoFence.aspx");
        }
        protected void Draw_Click(object sender, EventArgs e)
        {
            location = new LocationBAL();
            dt = new DataTable();
            dt1 = new DataTable();
            if (txtCode.Text.Trim() == "" || txtMobileNo.Text.Trim() == "" || Request.Form["startlocation"].ToString().Trim() == "" || Request.Form["deslocation"].ToString().Trim() == "")
            {
                lblMsg.Text = "Please fill mandatory field!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {

                location.ClientId = ClientId;
                location.RouteName = txtMobileNo.Text.Trim();
                location.RouteCode = txtCode.Text;
                dt = location.IsRoteCodeExists();
                dt1 = location.IsRoteNameExists();
                if (dt.Rows.Count > 0)
                {
                    lblMsg.Text = "Route Code Already Exists";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (dt1.Rows.Count > 0)
                    {
                        lblMsg.Text = "Route Name Already Exists";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblMsg.Text = "";
                        mapCall();
                    }
                }
            }
        }
        private void mapCall()
        {
            if (!string.IsNullOrEmpty(StartLat.Trim()) && !string.IsNullOrEmpty(StartLong.Trim()) && !string.IsNullOrEmpty(EndLat.Trim()) && !string.IsNullOrEmpty(EndLong.Trim()))
            {
                btnAssign.Visible = true;
                btnCanceldraw.Visible = true;
                string script = "window.onload = function() { initialize(); };";
                ClientScript.RegisterStartupScript(this.GetType(), "initialize", script, true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "initialize()", true);
            }
            else
            {
                btnAssign.Visible = false;
                btnCanceldraw.Visible = false;
            }
        }
        protected void btnCancl_Click(object sender, EventArgs e)
        {
            Response.Redirect("GeoFenceManagement.aspx");
        }
    }
}