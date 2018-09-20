using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
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
    public partial class EditArea : Base
    {
        int ClientId, UserId, RoleId, DeptId, AreaId;
        LocationBAL Loc;
        DataTable dt;
        AnuSearch srch;
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
            try
            {
                AreaId = Convert.ToInt32(Session["AreaId"].ToString());
            }
            catch (Exception) { AreaId = 0; }
            if (AreaId == 0)
            {
                Response.Redirect("Area.aspx");
            }
            if (!IsPostBack)
            {
                BindGrid();
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
                    Loc.AreaId = AreaId;
                    Loc.AreaName = txtArea.Text.Trim();
                    Loc.UserId = UserId;
                    Loc.Latitude = StartLat.Trim();
                    Loc.Longitude = StartLong.Trim();
                    Loc.Location = StartAddress.Trim();
                    Loc.Radius = (float)Convert.ToDouble(Radius.Trim());
                    int res = Loc.IU_Area();
                    if (res > 0)
                    {
                        lblMsg.Text = "Updated Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        txtArea.Text = "";
                        StartAddress = "";
                        txtRadius.Text = "";
                        btnSave.Visible = false;
                        btnC.Visible = false;
                        Response.Redirect("Area.aspx");

                    }
                    else
                    {
                        lblMsg.Text = "Already Exists!!!";
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

            dt = new DataTable();
            dt.Columns.Add("Latiude");
            dt.Columns.Add("Longitude");
            dt.Columns.Add("Radius");
            try
            {
                dt.Rows.Add(StartLat.Trim(), StartLong.Trim(), Radius.Trim());
                LabelProperty = StartLat.Trim() + "," + StartLong.Trim();
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
            return DataTableToJSONWithJavaScriptSerializer(dt);

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
            if (!string.IsNullOrEmpty(StartLat.Trim()) && !string.IsNullOrEmpty(StartLong.Trim()) && txtRadius.Text != "")
            {
                hdnRadius.Value = txtRadius.Text.Trim();
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
        protected void BindGrid()
        {
            srch = new AnuSearch();
            dt = srch.GetAreaByAreaId(AreaId);
            txtArea.Text = dt.Rows[0]["AreaName"].ToString();
            StartAddress = dt.Rows[0]["Location"].ToString();
            StartLat = dt.Rows[0]["Latitude"].ToString();
            StartLong = dt.Rows[0]["Longitude"].ToString();
            Radius = dt.Rows[0]["Radius"].ToString();
            txtRadius.Text = dt.Rows[0]["Radius"].ToString();
            getdata();
            string script = "window.onload = function() { initialize(); };";
            ClientScript.RegisterStartupScript(this.GetType(), "initialize", script, true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "initialize()", true);
            btnSave.Visible = true;
            btnC.Visible = true;
        }
        protected void btnC_Click(object sender, EventArgs e)
        {
            Response.Redirect("Area.aspx");
        }
    }
}