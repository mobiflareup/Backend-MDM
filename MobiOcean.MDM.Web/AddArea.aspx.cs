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
    public partial class AddArea : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        LocationBAL Loc;
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

        protected String Radius
        {
            get
            {
                return hdnRadius.Value;
            }
            set
            {
                hdnRadius.Value = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                //BindGrid();
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkValidation())
                {
                    getdata();
                    Loc = new LocationBAL();
                    Loc.ClientId = ClientId;
                    Loc.AreaName = txtArea.Text.Trim();
                    Loc.UserId = UserId;
                    Loc.Latitude = StartLat.Trim();
                    Loc.Longitude = StartLong.Trim();
                    Loc.Location = StartAddress.Trim();
                    Loc.Radius = (float)Convert.ToDouble(Radius.Trim());
                    int res = Loc.IU_Area();
                    if (res > 0)
                    {
                        lblMsg.Text = "Saved Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        txtArea.Text = "";
                        StartAddress = "";
                        btnSave.Visible = false;
                        btnC.Visible = false;
                        Response.Redirect("Area");

                    }
                    else
                    {
                        lblMsg.Text = "Area Name Already Exists!!!";
                        lblMsg.ForeColor = System.Drawing.Color.Red;

                    }

                }
                else
                {
                    lblMsg.Text = "Please Fill Mandatory Fields";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    btnSave.Visible = false;
                    btnC.Visible = false;

                }

            }
            catch (Exception)
            {

            }
        }
        protected bool ChkValidation()
        {
            if (txtArea.Text.Trim() == "" || StartAddress.Trim() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected string getdata()
        {
            //location = new LocationBAL();
            //location.RouteId = RouteId;
            dt = new DataTable();
            dt.Columns.Add("Latiude");
            dt.Columns.Add("Longitude");
            dt.Columns.Add("Radius");
            try
            {
                //hdnRadius.Value = txtRadius.Text.Trim();
                //float radius=Convert.ToInt32(txtRadius.Text.Trim())*1000;
                dt.Rows.Add(StartLat.Trim(), StartLong.Trim(), Radius.Trim());
                LabelProperty = StartLat.Trim() + "," + StartLong.Trim();

                //28.6524629, 77.1260591),
                //   new google.maps.LatLng(, ),
                // new google.maps.LatLng(, 77.1232247
                //dt = location.GetGeoFenceRouteDetail();
            }

            catch (Exception)
            {
                dt.Rows.Clear();
                dt.Rows.Add("28.6524629", "77.1260591");
                LabelProperty = "28.6524629" + "," + "77.1260591";
            }
            if (dt.Rows.Count <= 0)
            {
                dt.Rows.Clear();
                dt.Rows.Add("28.6524629", "77.1260591");
                LabelProperty = "28.6524629" + "," + "77.1260591";
            }
            //LabelProperty = StartAddress.Trim() + "," + StartLat.Trim() + "," + StartLong.Trim() + "," + EndAddress.Trim()+","+EndLat.Trim() + "," + EndLong.Trim();
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
        protected void Draw_Click(object sender, EventArgs e)
        {
            if (txtArea.Text.Trim() != "" && Request.Form["AreaLocation"] != "" && txtRadius.Text != "")
            {
                hdnRadius.Value = txtRadius.Text.Trim();
                dt = new DataTable();
                Loc = new LocationBAL();
                Loc.ClientId = ClientId;
                Loc.AreaName = txtArea.Text.Trim();
                dt = Loc.IsAreaNameExists();
                if (dt.Rows.Count > 0)
                {
                    lblMsg.Text = "Area Name Already Exists!!!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    btnSave.Visible = false;
                    btnC.Visible = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(StartLat.Trim()) || !string.IsNullOrEmpty(StartLong.Trim()) || !string.IsNullOrEmpty(Radius.Trim()))
                    {
                        btnSave.Visible = true;
                        btnC.Visible = true;
                        string script = "window.onload = function() { initialize(); };";
                        ClientScript.RegisterStartupScript(this.GetType(), "initialize", script, true);
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "initialize()", true);
                    }
                    else
                    {
                        btnSave.Visible = false;
                        btnC.Visible = false;
                    }
                }
            }
            else
            {
                lblMsg.Text = "Please Fill All Field";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

        }
        protected void btnC_Click(object sender, EventArgs e)
        {
            Response.Redirect("Area");
        }
    }
}