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
    public partial class ConveyanceLocation : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        ConveyanceBAL conveyance;
        int conveyanceId = 0;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            try
            {
                conveyanceId = Convert.ToInt32(Request.QueryString["Id"].ToString());
            }
            catch (Exception) { conveyanceId = 0; }
            if (conveyanceId == 0)
            {
                Response.Redirect("ConveyanceReport.aspx");
            }
        }
        protected string getData()
        {
            conveyance = new ConveyanceBAL();
            conveyance.ConveyanceId = Convert.ToInt32(conveyanceId);
            dt = conveyance.GetConveyanceLocationByConveyanceId();
            return DataTableToJSONWithJavaScriptSerializer(dt);
        }
        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            int skipped = 0, skip = 0;
            if (table.Rows.Count > 60)
            {
                skip = (int)table.Rows.Count / 60;
            }
            foreach (DataRow row in table.Rows)
            {
                if (skipped == skip)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in table.Columns)
                    {
                        childRow.Add(col.ColumnName, row[col]);
                    }
                    parentRow.Add(childRow);
                    skipped = 0;
                }
                else
                {
                    skipped++;
                }

            }
            return jsSerializer.Serialize(parentRow);
        }
    }
}