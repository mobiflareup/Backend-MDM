using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class admindashboard : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        DataTable dt;
        UserBAL usrbal;
        AnuSearch srch;
        ProfileBAL profile;
        GingerboxSrch Gbox;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                // GetDataForChart();

                //ChkPasswordExpiry();
                // BindGrid();
                // BindProfileGrid();
            }
            BindChart1();
            BindChart2();
            BindChart3();
            BindChart4();
            BindGrid1();
        }
        protected void BindChart1()
        {
            usrbal = new UserBAL();
            usrbal.ClientId = ClientId;
            DataTable dt = usrbal.GetDeviceRegisterDetail();
            //storing total rows count to loop on each Record  
            string[] XPointMember = new string[dt.Rows.Count];
            int[] YPointMember = new int[dt.Rows.Count];
            int Totalcount = 0;
            for (int count = 0; count < dt.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = dt.Rows[count]["IsAppInstalled"].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToInt32(dt.Rows[count]["Device"]);
                Totalcount += Convert.ToInt32(dt.Rows[count]["Device"]);
            }
            //binding chart control  
            Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //Setting width of line  
            Chart1.Series[0].BorderWidth = 0;
            //setting Chart type   
            Chart1.Series[0].ChartType = SeriesChartType.Pie;

            lbltotal.Text = Totalcount.ToString();
            Chart1.Legends[0].Enabled = true;
            foreach (Series charts in Chart1.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    switch (point.AxisLabel)
                    {
                        case "0": point.Color = Color.PaleGreen; break;
                        case "1": point.Color = Color.PaleVioletRed; break;
                    }
                    point.Label = string.Format("{0:0} - {1}", Convert.ToInt32(point.AxisLabel) == 1 ? "UnInstalled" : "Installed", point.YValues[0]);
                }
            }
            //Enabled 3D  
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
        }
        protected void BindChart2()
        {
            usrbal = new UserBAL();
            usrbal.ClientId = ClientId;
            DataTable dt = usrbal.GetBarnchNameAndNoOfUsers();
            string[] XPointMember = new string[dt.Rows.Count];
            int[] YPointMember = new int[dt.Rows.Count];
            for (int count = 0; count < dt.Rows.Count; count++)
            {
                if (Convert.ToInt32(dt.Rows[count]["NoOfUsers"]) > 0)
                {
                    XPointMember[count] = dt.Rows[count]["BranchName"].ToString();
                    YPointMember[count] = Convert.ToInt32(dt.Rows[count]["NoOfUsers"]);
                }
            }
            Chart2.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            Chart2.Series[0].BorderWidth = 0;
            Chart2.Series[0].ChartType = SeriesChartType.Pie;
            Chart2.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = true;
            Chart2.Legends[0].Enabled = true;
            foreach (Series charts in Chart2.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    point.Label = string.Format("{0:0} - {1}", point.AxisLabel, point.YValues[0]);
                }
            }
            lblBranchCnt.Text = dt.Rows.Count.ToString();// usrbal.GetTotalCountOfBranch().ToString();// dt1.Rows[0]["Count"].ToString();
        }
        protected void BindChart3()
        {

            usrbal = new UserBAL();
            usrbal.ClientId = ClientId;
            DataTable dt = usrbal.GetDepartmentNameAndNoOfUsers();
            string[] XPointMember = new string[dt.Rows.Count];
            int[] YPointMember = new int[dt.Rows.Count];
            for (int count = 0; count < dt.Rows.Count; count++)
            {
                if (Convert.ToInt32(dt.Rows[count]["No_of_Department"]) > 0)
                {
                    XPointMember[count] = dt.Rows[count]["DeptName"].ToString();
                    YPointMember[count] = Convert.ToInt32(dt.Rows[count]["No_of_Department"]);
                }
            }
            Chart3.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            Chart3.Series[0].BorderWidth = 0;
            Chart3.Series[0].ChartType = SeriesChartType.Pie;
            Chart3.ChartAreas["ChartArea3"].Area3DStyle.Enable3D = true;
            Chart3.Legends[0].Enabled = true;
            foreach (Series charts in Chart3.Series)
            {
                foreach (DataPoint point in charts.Points)
                {

                    point.Label = string.Format("{0:0} - {1}", point.AxisLabel, point.YValues[0]);
                }
            }
            lbldepartcnt.Text = dt.Rows.Count.ToString();
        }
        private void BindChart4()
        {
            usrbal = new UserBAL();
            usrbal.ClientId = ClientId;
            DataTable dt = usrbal.GetProfileNameAndNoOfUsers();
            string[] XPointMember = new string[dt.Rows.Count];
            int[] YPointMember = new int[dt.Rows.Count];
            for (int count = 0; count < dt.Rows.Count; count++)
            {
                if (Convert.ToInt32(dt.Rows[count]["No_of_Profile"]) > 0)
                {
                    XPointMember[count] = dt.Rows[count]["ProfileName"].ToString();
                    YPointMember[count] = Convert.ToInt32(dt.Rows[count]["No_of_Profile"]);
                }
            }
            Chart4.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            Chart4.Series[0].BorderWidth = 0;
            Chart4.Series[0].ChartType = SeriesChartType.Pie;
            Chart4.ChartAreas["ChartArea4"].Area3DStyle.Enable3D = true;
            Chart4.Legends[0].Enabled = true;
            foreach (Series charts in Chart4.Series)
            {
                foreach (DataPoint point in charts.Points)
                {
                    //switch (point.AxisLabel)
                    //{
                    //    case "0": point.Color = Color.Chocolate; break;
                    //    case "1": point.Color = Color.DeepPink; break;
                    //}
                    point.Label = string.Format("{0:0} - {1}", point.AxisLabel, point.YValues[0]);
                }
            }
            // dt1 = new DataTable();
            //dt1 = usrbal.GetTotalCountOfProfile();
            //lblProfile.Text = usrbal.GetTotalCountOfProfile().ToString(); //dt1.Rows[0]["Count"].ToString();
            lblProfile.Text = dt.Rows.Count.ToString();
        }
        protected void addToTable_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/profilefeature.aspx");
        }
        void Popup(bool isDisplay)
        {
            // mp.Show();
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            ////if (isDisplay)
            ////{
            //sb.Append(@"<script type='text/javascript'>");
            //sb.Append("$('#myModal').modal('show');");
            //sb.Append(@"</script>");
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), true);       

        }
        protected void grddashboard_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // grddashboard.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            // grddashboard.DataSource = srch.DashBoardDtls(ClientId);
            // grddashboard.DataBind();
        }
        protected void BindProfileGrid()
        {
            profile = new ProfileBAL();
            profile.ClientId = ClientId;
            // grdProfile.DataSource = profile.GetProfileDtls();
            // grdProfile.DataBind();
        }
        protected void grdProfile_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // grdProfile.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected string Format(string CreationDate)
        {
            return Convert.ToDateTime(CreationDate).ToString("dd-MMM-yyyy HH:mm");
        }
        private void ChkPasswordExpiry()
        {
            usrbal = new UserBAL();
            usrbal.UserId = Convert.ToInt32(Session["UserId"].ToString());
            string ExpiryDay = usrbal.GetRemainngDaysOfExpiryPwd();
            if (!string.IsNullOrEmpty(ExpiryDay))
            {
                if (Convert.ToInt32(ExpiryDay) <= 0)
                {
                    Response.Redirect("ChangePassword.aspx");
                }
                else if (Convert.ToInt32(ExpiryDay) <= 15)
                {
                    // lblpwdexpry.Text = "Your password is expiring in " + ExpiryDay + " days. So please <a href=\"ChangePassword.aspx\" >change your password</a> .";
                    Popup(true);
                }
            }
            else
            {
                Response.Redirect("ChangePassword.aspx");
            }
        }
        protected string GetDataForChart()
        {


            string sa = "[{ \"Latitude\": Admin, \"Longitude\": '' } ,  { \"Latitude\": Sub admin1, \"Longitude\": Admin } ,"

                               + "{ \"Latitude\": User1, \"Longitude\": Sub admin1 } " +
            " { \"Latitude\": User2, \"Longitude\": Sub admin1 } ," +
                                   " { \"Latitude\": Sub admin2, \"Longitude\": Admin }     ]";


            //     string sa= @"{
            //  cols: [{id: 'Node', type: 'string'},
            //         {id: 'Parent', type: 'string'},
            //         
            //  ],
            //  rows: [{c:[{v: 'Admin'},
            //             {v: ''}
            //             
            //        ]},
            //        {c:[{v: 'Sub admin1'},
            //             {v: 'Admin'}
            //             
            //        ]},
            //            {c:[{v: 'User1'},
            //             {v: 'Sub admin1'}
            //             
            //        ]},
            //            {c:[{v: 'User2'},
            //             {v: 'Sub admin1'}
            //             
            //        ]},
            //            {c:[{v: 'Sub admin2'},
            //             {v: 'Admin'}
            //             
            //        ]},
            //            {c:[{v: 'User3'},
            //             {v: 'Sub admin2'}
            //             
            //        ]},
            //            {c:[{v: 'User4'},
            //             {v: 'Sub admin2'}
            //             
            //        ]},
            //            {c:[{v: 'Admin'},
            //             {v: ''}
            //             
            //        ]},
            //  ]
            //}";


            //u.UserId,u.RptMngrId,d.DeptName,u.UserName,u.EmpCompanyId
            //  string s = "['Admin', ''],	 ['Sub admin1', 'Admin'],      ['Sub admin2', 'Admin'],      ['Sub admin3', 'Admin'],      ['Sub admin4', 'Admin'],"+
            //"['User', 'Sub admin1'],      ['Profile', 'Sub admin1'],	  ['User2', 'Sub admin2'],	  ['Profile2', 'Sub admin2'],	  ['User3', 'Sub admin3'],"+
            //  "['Profile3', 'Sub admin3'],	  ['User4', 'Sub admin4'],	  ['Profile4', 'Sub admin4']";
            // myhdnFields.Text = s;
            //usrbal = new UserBAL();
            //dt = new DataTable();
            //usrbal.ClientId = ClientId;
            //dt = usrbal.GetDataforChart();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    s = s + "['" + dt.Rows[i]["UserId"].ToString() + "','" + dt.Rows[i]["RptMngrId"].ToString() + "'],";
            //}
            //s = s.TrimEnd(',');

            //string csname1 = "PopupScript";
            //Type cstype = this.GetType();

            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    StringBuilder cstext1 = new StringBuilder();
            //    cstext1.Append("<script>");
            //    cstext1.Append("google.setOnLoadCallback(drawChart);");
            //    cstext1.Append("function drawChart() {");
            //    cstext1.Append("var data = new google.visualization.DataTable();");
            //    cstext1.Append("data.addColumn('string', 'Name'); data.addColumn('string', 'Manager');");
            //    cstext1.Append("data.addRows([" + s + "]);");
            //    cstext1.Append("var chart = new google.visualization.OrgChart(document.getElementById('chart_div'));");
            //    cstext1.Append("chart.draw(data, { allowHtml: true });");
            //    cstext1.Append("}");

            //    cstext1.Append("</script>");

            //    cs.RegisterStartupScript(cstype, csname1, cstext1.ToString(),false);
            // }

            return sa;
        }
        protected void BindGrid1()
        {
            Gbox = new GingerboxSrch();
            //sos = new SOSBAL();
            dt = new DataTable();
            //sos.ClientId = ClientId;
            ////sos.UserId = EmpId;
            //sos.LocReq = "50";// txtRadius.Text.Trim();
            //sos.Duration = Convert.ToInt32(string.IsNullOrEmpty(txtDuration.Text.Trim()) ? "0" : txtDuration.Text.Trim());
            //dt = sos.FindNearestEmp();
            dt = Gbox.DashboardSOS(ClientId, GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy"), GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy"));
            ViewState["Nearest"] = dt;

            //grdUser.DataSource = dt;
            //grdUser.DataBind();
        }
        protected string ShowOnMap()
        {
            srch = new AnuSearch();
            ArrayList al = new ArrayList();
            try
            {
                int imgid, i = 0;
                DataTable dt = new DataTable();
                string msg = "[";
                dt = (DataTable)ViewState["Nearest"];
                foreach (DataRow row in dt.Rows)
                {
                    int currentuser = Convert.ToInt32(row["UserId"].ToString());
                    al.Add(Convert.ToInt32(row["UserId"].ToString()));
                    string marker = "";
                    foreach (int obj in al)
                    {
                        if (currentuser == obj)
                        {
                            i++;
                        }
                    }
                    if (i == 1)
                    {

                        imgid = 1;
                    }

                    else
                    {
                        imgid = 0;
                    }
                    i = 0;
                    if (imgid == 0)
                    {
                        marker = "images/Map_greenMarker.png";
                    }
                    else
                    {
                        marker = "images/Map_RedMarker.png";
                    }
                    msg = @"" + msg + "{\"title\": \"" + row["Location"].ToString() + "\",\"lat\":\"" + row["Latitude"].ToString() + "\",\"lng\": \"" + row["Longitude"].ToString() + "\",\"description\": \"" + row["LogDateTime"].ToString() + "\",\"MobileNo\": \"" + row["MobileNo1"].ToString() + "\",\"marker\": \"" + marker + "\"},";
                }
                msg = @"" + msg + "{\"title\": \"surveillance camera\",\"lat\":\"18.95955\",\"lng\": \"72.81864\",\"description\": \"surveillance camera\",\"MobileNo\": \"surveillance camera\",\"marker\": \"image/Security_Camera-512.png\"},";
                if (msg.Length > 1)
                {
                    msg = msg.Substring(0, msg.Length - 1);
                }
                msg = msg + "]";
                if (dt.Rows.Count <= 0)
                {
                    msg = "[{\"title\": \"surveillance camera\",\"lat\":\"18.95955\",\"lng\": \"72.81864\",\"description\": \"surveillance camera\",\"marker\": \"image/Security_Camera-512.png\"}]";
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
    }
}
