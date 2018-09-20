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
    public partial class EditGeofence : Base
    {
        int ClientId, UserId, RoleId, DeptId, RouteId;
        LocationBAL location;
        DataTable dt;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            try
            {
                if (Request.QueryString["Id"].ToString() == null)
                    Response.Redirect("GeoFenceManagement.aspx");
                else
                    RouteId = Convert.ToInt32(Request.QueryString["Id"].ToString());
            }
            catch (Exception)
            {
                Response.Redirect("GeoFenceManagement.aspx");
            }
            if (!IsPostBack)
            {
                BindOldData();
            }


        }
        protected void BindOldData()
        {
            location = new LocationBAL();
            location.RouteId = RouteId;
            dt = new DataTable();
            dt = location.GetGeoFenceRouteDetail();
            if (dt.Rows.Count > 0)
            {
                txtCode.Text = dt.Rows[0]["RouteCode"].ToString();
                txtDesc.Text = dt.Rows[0]["RouteDesc"].ToString();
                txtMobileNo.Text = dt.Rows[0]["RouteName"].ToString();
                LabelProperty = "";
                foreach (DataRow row1 in dt.Rows)
                {
                    LabelProperty = LabelProperty + row1["Latiude"].ToString() + "," + row1["Longitude"].ToString() + ",";
                }
                string script = "window.onload = function() { initialize(); };";
                ClientScript.RegisterStartupScript(this.GetType(), "initialize", script, true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "initialize()", true);
            }
        }
        protected string getdata()
        {
            location = new LocationBAL();
            location.RouteId = RouteId;
            dt = new DataTable();
            dt = location.GetGeoFenceRouteDetail();
            if (dt.Rows.Count > 0)
            {
                return DataTableToJSONWithJavaScriptSerializer(dt);

            }
            else
            {
                Response.Redirect("GeoFenceManagement.aspx");
            }
            return "0";

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
                string script = "window.onload = function() { initialize(); };";
                ClientScript.RegisterStartupScript(this.GetType(), "initialize", script, true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "initialize()", true);
            }
            else if (txtCode.Text.Trim() == "" || txtMobileNo.Text.Trim() == "")
            {
                lblMsg.Text = "Please fill mandatory field!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                string script = "window.onload = function() { initialize(); };";
                ClientScript.RegisterStartupScript(this.GetType(), "initialize", script, true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "initialize()", true);
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
                location.RouteId = RouteId;
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
                    string script = "window.onload = function() { initialize(); };";
                    ClientScript.RegisterStartupScript(this.GetType(), "initialize", script, true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "initialize()", true);
                }
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("GeoFenceManagement.aspx");
        }
    }
}