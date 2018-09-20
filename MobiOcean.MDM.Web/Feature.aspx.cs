using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class Feature : Base
    {
        int ClientId, UserId, RoleId, ProfileId, DeptId;
        AlertBAL Abal;
        UserDeviceBAL usrdevice;
        ProfileBAL probal;        
        DataTable dt;
        SendSMSBAL sms;
        AppBAL app;
        AllowPhNoBAL allowedphno;
        FeatureBAL feature;
        string ProfileName;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            try
            {
                ProfileId = Convert.ToInt32(Session["ProfileId"].ToString());
                ProfileName = Session["ProfileName"].ToString();
            }
            catch (Exception) { ProfileId = 0; }
            if (ProfileId == 0)
            {
                Response.Redirect("ProfileMaster.aspx");
            }
            if (!IsPostBack)
            {
                txtProfileName.Text = ProfileName.ToString();
                //BindProfileDetail();
                MoveDatainTempTableToOriginalTable();
                BindDataGrid();
            }

        }       
        protected void MoveDatainTempTableToOriginalTable()
        {
            try
            {
                probal = new ProfileBAL();
                probal.ClientId = ClientId;
                probal.ProfileId = ProfileId;
                probal.MoveDatainTempTable();

            }
            catch (Exception)
            {
                Response.Redirect("ProfileMaster.aspx");
            }
        }
        protected void AppMgmt_Click(object sender, EventArgs e)
        {
            try
            {
                app = new AppBAL();
                app.ProfileId = ProfileId;
                app.ClientId = ClientId;
                app.GetAppNameByProfileId();
                Response.Redirect("AppManagement.aspx");
            }
            catch (Exception)
            {

            }
        }
        protected void lnkbtnWebsiteUrl_Click(object sender, EventArgs e)
        {
            try
            {
                app = new AppBAL();
                app.ProfileId = ProfileId;
                app.ClientId = ClientId;
                app.GetWebsiteUrlByProfileId();
                Response.Redirect("WebManagement.aspx");
            }
            catch (Exception)
            {

            }
        }
        protected void lnkbtnCallAndSms_Click(object sender, EventArgs e)
        {
            Abal = new AlertBAL();
            Abal.ProfileId = ProfileId;
            Abal.ClientId = ClientId;
            Abal.MoveAlertDatatoProfile();
            Abal.MoveKeywordDatatoProfile();
            allowedphno = new AllowPhNoBAL();
            allowedphno.ProfileId = ProfileId;
            allowedphno.ClientId = ClientId;
            allowedphno.GetAllowedPhNoByProfileId();
            Response.Redirect("CallAndSmsRegulation.aspx");
        }
        protected void btnpush_Click(object sender, EventArgs e)
        {
            SendUpdateMsg();
            Session["ProfileId"] = null;
            //Response.Redirect("ProfileMaster.aspx");
            MP2.Show();
        }
        protected void ok_Click(object sender, EventArgs e)
        {
            Response.Redirect("Feature.aspx");
        }
        protected void SendUpdateMsg()
        {
            usrdevice = new UserDeviceBAL();
            dt = new DataTable();
            usrdevice.ProfileId = ProfileId;
            usrdevice.ClientId = ClientId;
            dt = usrdevice.GetUserDeviceToSendUpdate();
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    sms = new SendSMSBAL();
                    sms.sendFinalSMS(row["MobileNo1"].ToString(), "GBox set as WP7", ClientId);
                }
                catch (Exception)
                { }
            }

        }
        protected void BindDataGrid()
        {
            feature = new FeatureBAL();
            //DataList.DataSource = feature.GetImageUrl();
            feature.ClientId = ClientId;
            feature.SolutionId = "1,5,6";
            rptr1.DataSource = feature.GetNewActiveSolutions();
            //DataList.DataBind();
            rptr1.DataBind();
        }
        protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                Image img = (Image)e.Item.FindControl("img");
                Label lbl = (Label)e.Item.FindControl("lblHover");
                Label lblSim = (Label)e.Item.FindControl("lblSim");
                Label lblSub = (Label)e.Item.FindControl("lblSub");

                img.Attributes.Add("onmouseover", "this.src='" + lbl.Text + "'");
                img.Attributes.Add("onmouseout", "this.src='" + lblSim.Text + "'");

            }
            catch (Exception)
            { }
        }
        protected void DataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Image")
            {
                Label lbl = ((Label)e.Item.FindControl("lblId"));

                if (lbl.Text == "2")
                {
                    try
                    {
                        app = new AppBAL();
                        app.ProfileId = ProfileId;
                        app.ClientId = ClientId;
                        app.GetWebsiteUrlByProfileId();
                        Response.Redirect("WebManagement.aspx");
                    }
                    catch (Exception)
                    {

                    }
                }
                if (lbl.Text == "5")
                {
                    Response.Redirect("DeviceManagement.aspx");
                }
                if (lbl.Text == "6")
                {
                    Abal = new AlertBAL();
                    Abal.ProfileId = ProfileId;
                    Abal.ClientId = ClientId;
                    Abal.MoveAlertDatatoProfile();
                    Abal.MoveKeywordDatatoProfile();
                    allowedphno = new AllowPhNoBAL();
                    allowedphno.ProfileId = ProfileId;
                    allowedphno.ClientId = ClientId;
                    allowedphno.GetAllowedPhNoByProfileId();
                    Response.Redirect("CallAndSmsRegulation.aspx");
                }
                if (lbl.Text == "7")
                {
                    try
                    {
                        app = new AppBAL();
                        app.ClientId = ClientId;
                        app.ProfileId = ProfileId;
                        app.GetSosContactsByProfileId();
                        Response.Redirect("FeatureSosMgmt.aspx");
                    }
                    catch (Exception)
                    {

                    }
                }
                if (lbl.Text == "8")
                {
                    try
                    {
                        app = new AppBAL();
                        app.ProfileId = ProfileId;
                        app.ClientId = ClientId;
                        app.GetAppNameByProfileId();
                        Response.Redirect("AppManagement.aspx");
                    }
                    catch (Exception)
                    {

                    }
                }
                if (lbl.Text == "9")
                {
                    Response.Redirect("LocationManagement.aspx");
                }
                if (lbl.Text == "10")
                {
                    Response.Redirect("DeviceSecurity.aspx");
                }
                if (lbl.Text == "11")
                {
                    Response.Redirect("CorporateDataSecurity.aspx");
                }
            }
        }
        protected void rptr1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Image img = (Image)e.Item.FindControl("imgr");
                ImageButton imgbtn = (ImageButton)e.Item.FindControl("imgr");
                //Label lbl = (Label)e.Item.FindControl("lblHoverr");
                //Label lblSim = (Label)e.Item.FindControl("lblSimr");
                Label lblSub = (Label)e.Item.FindControl("lblSub");
                Label lblstatus = (Label)e.Item.FindControl("lblstatus");
                if (!string.IsNullOrEmpty(lblSub.Text.Trim()) && lblstatus.Text == "0")
                {
                    //imgbtn.Enabled = true;
                    //img.Attributes.Add("onmouseover", "this.src='" + lbl.Text + "'");
                    //img.Attributes.Add("onmouseout", "this.src='" + lblSim.Text + "'");

                }
                else
                {
                    imgbtn.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }
        protected void rptr1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Image")
            {
                Label lbl = ((Label)e.Item.FindControl("lblIdr"));

                if (lbl.Text == "2")
                {
                    try
                    {
                        app = new AppBAL();
                        app.ProfileId = ProfileId;
                        app.ClientId = ClientId;
                        app.GetWebsiteUrlByProfileId();
                        Response.Redirect("WebManagement.aspx");
                    }
                    catch (Exception)
                    {

                    }
                }
                if (lbl.Text == "5")
                {
                    Response.Redirect("DeviceManagement.aspx");
                }
                if (lbl.Text == "6")
                {
                    Abal = new AlertBAL();
                    Abal.ProfileId = ProfileId;
                    Abal.ClientId = ClientId;
                    Abal.MoveAlertDatatoProfile();
                    Abal.MoveKeywordDatatoProfile();
                    allowedphno = new AllowPhNoBAL();
                    allowedphno.ProfileId = ProfileId;
                    allowedphno.ClientId = ClientId;
                    allowedphno.GetAllowedPhNoByProfileId();
                    Response.Redirect("CallAndSmsRegulation.aspx");
                }
                if (lbl.Text == "7")
                {
                    try
                    {
                        app = new AppBAL();
                        app.ClientId = ClientId;
                        app.ProfileId = ProfileId;
                        app.GetSosContactsByProfileId();
                        Response.Redirect("FeatureSosMgmt.aspx");
                    }
                    catch (Exception)
                    {

                    }
                }
                if (lbl.Text == "8")
                {
                    try
                    {
                        app = new AppBAL();
                        app.ProfileId = ProfileId;
                        app.ClientId = ClientId;
                        app.GetAppNameByProfileId();
                        Response.Redirect("AppManagement.aspx");
                    }
                    catch (Exception)
                    {

                    }
                }
                if (lbl.Text == "24")
                {
                    Response.Redirect("GeoFenceMgmt.aspx");
                }

                if (lbl.Text == "25")
                {
                    Response.Redirect("LocationManagement.aspx");
                }
                if (lbl.Text == "10")
                {
                    Response.Redirect("DeviceSecurity.aspx");
                }
                if (lbl.Text == "11")
                {
                    Response.Redirect("CorporateDataSecurity.aspx");
                }
            }
        }
    }
}
