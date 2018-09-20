using MobiOcean.MDM.BAL.BAL;
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
    public partial class reposrtssms : Base
    {
        ContactBAL contB = new ContactBAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dt = GetData();
                LoadChartData(dt);
                Chart1.Style.Add("width", "100%");
            }
        }
        private void LoadChartData(DataTable initialDataSource)
        {
            for (int i = 3; i < initialDataSource.Columns.Count; i++)
            {
                Series series = new Series();

                foreach (DataRow dr in initialDataSource.Rows)
                {
                    int y = Convert.ToInt32(string.IsNullOrEmpty(dr[i].ToString()) ? "0" : dr[i].ToString());
                    series.Points.AddXY(string.IsNullOrEmpty(dr["date1"].ToString()) ? Convert.ToDateTime(dr["date2"]).ToString("dd MMM yyyy") : Convert.ToDateTime(dr["date1"]).ToString("dd MMM yyyy"), y);

                }
                Chart1.Series.Add(series);
                //Chart1.Series[0].Points[0].Color = Color.PaleGreen;

            }
            foreach (Series charts in Chart1.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    //switch (point)
                    //{
                    //charts.point.Color = Color.PaleGreen; break;
                    // point[1].Color = Color.PaleVioletRed; break;
                    //    case "2": point.Color = Color.RoyalBlue; break;
                    //}
                    //point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

                }
            }
        }
        private DataTable GetData()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            contB = new ContactBAL();
            dt1 = contB.GetChartIncomingOutGoing();
            return dt1;
        }
    }
}