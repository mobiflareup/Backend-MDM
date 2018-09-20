using MobiOcean.MDM.BAL;
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
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        UserBAL userBAL;
        ClientBAL client;
        AlertBAL alert;
        DataTable dt, dtA, dt1;
        SubscribeBAL subscribe;
        usrBAL user;
        GingerboxSrch gsrch;
        int ClientId, UserId, RoleId, DeptId;
        

        string UserName;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            UserName = Session["UserName"].ToString();
            if (!IsPostBack)
            {

                try
                {
                    BindDynamicMenu(RoleId);
                    CheckAPI();
                }
                catch (Exception)
                {
                }

                Bindlogo();
                if (RoleId >= 4)
                {
                    BindImage();
                }
                BindList();
                BindExpiryDay();
                BindNotification();
                if (RoleId == 1)
                    BindClientDDL();
            }
        }

        protected void BindImage()
        {
            userBAL = new UserBAL();
            dt = new DataTable();
            userBAL.UserId = UserId;
            dt = userBAL.GetUserDtlByUserId();

            if (dt.Rows.Count > 0)
            {
                lblUsName.Text = UserName;

                if (!string.IsNullOrEmpty(dt.Rows[0]["ProfileImagePath"].ToString()))
                {
                    Image1.ImageUrl = "~" + dt.Rows[0]["ProfileImagePath"].ToString();
                }
                else
                {
                    Image1.ImageUrl = "~/image/NoPic.png";
                }
            }
            else
            {
                Image1.ImageUrl = "~/image/NoPic.png";
            }
            dt1 = new DataTable();
            dt1 = userBAL.GetProfileName();
            if (dt1.Rows.Count > 0)
            {
                lblPrfName.Text = "Profile Name : " + dt1.Rows[0]["ProfileName"].ToString();
            }
            else
            {
                lblPrfName.Text = "Profile Name : N/A";
            }
        }
        private void ChkPasswordExpiry()
        {
            userBAL = new UserBAL();
            userBAL.UserId = Convert.ToInt32(Session["UserId"].ToString());
            string ExpiryDay = userBAL.GetRemainngDaysOfExpiryPwd();
            if (!string.IsNullOrEmpty(ExpiryDay))
            {
                if (Convert.ToInt32(ExpiryDay) <= 0)
                {
                    Response.Redirect("ChangePassword.aspx");
                }

            }
            else
            {
                Response.Redirect("ChangePassword.aspx");
            }
        }


        protected void lbtnChngPwd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ChangePassword.aspx");
        }

        protected void lbtnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LogOut.aspx");
        }
        public void BindDynamicMenu(int Role)
        {
            try
            {
                if (Session["SelectedIndex"] == null)
                {
                    Session["Role"] = Role;
                }
                else
                {
                    if (Session["SelectedIndex"].ToString() == "0")
                    {
                        Session["Role"] = 1;
                    }
                    else
                    {
                        Session["Role"] = 2;
                    }
                }
            }
            catch (Exception)
            { }
        }


        protected void lbtnChangeRole_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AfterLogin.aspx");
        }

        private void Bindlogo()
        {
            dt = new DataTable();
            try
            {
                client = new ClientBAL();
                client.ClientId = ClientId;
                client.LogoFilepath = "";
                dt = client.updateheaderlogo();
                string imgurldbh = dt.Rows[0]["LogoFilepath"].ToString();
                if (String.IsNullOrEmpty(imgurldbh))
                {
                    header_imageButton.ImageUrl = "~/image/defaultlogo.png";
                }
                else
                {
                    header_imageButton.ImageUrl = imgurldbh;
                }
            }
            catch
            { }
        }



        private void BindNotification()
        {
            alert = new AlertBAL();
            alert.UserId = UserId;
            dtA = new DataTable();
            dtA = alert.GetNotification_1ByUserId();
            NotifiLV.DataSource = dtA;
            NotifiLV.DataBind();
            lblNotifi.Text = (dtA.Rows.Count > 0) ? dtA.Rows.Count.ToString() : "0";
        }
        protected void BindExpiryDay()
        {
            try
            {
                subscribe = new SubscribeBAL();
                subscribe.ClientId = ClientId;
                dt = subscribe.GetAppliedSubscriptionDtl();// GetSubscriptionDtlByCurrentDateTime();
                if (dt.Rows.Count > 0)
                {
                    expireday.DataSource = dt;
                    expireday.DataBind();
                    lblExpiry.Text = string.IsNullOrEmpty(dt.Rows[0]["RemainingTimePeriod"].ToString()) ? "0" : dt.Rows[0]["RemainingTimePeriod"].ToString();
                }
            }
            catch (Exception)
            {

            }

        }

        protected void BindList()
        {
            alert = new AlertBAL();
            alert.UserId = UserId;
            dtA = new DataTable();
            dtA = alert.GetAlertDetailsByUserIdAndIsRead();
            NotificationList.DataSource = dtA;
            NotificationList.DataBind();
            lblNotification1.Text = (dtA.Rows.Count > 0) ? dtA.Rows.Count.ToString() : "0";
        }


        protected void btnuploadheader_Click(object sender, EventArgs e)
        {
            client = new ClientBAL();
            string FileName = string.Empty;
            string ServerFilePath = string.Empty;
            if (FileUpload1header.HasFile)
            {

                if (FileUpload1header.PostedFile.ContentType.Contains("image/"))
                {
                    //FileName = System.IO.Path.GetFileName(FileUpload1header.PostedFile.FileName);
                    string FileExtension = System.IO.Path.GetExtension(FileUpload1header.PostedFile.FileName);
                    FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + FileExtension;
                    string ServerFolder = Server.MapPath("~/Files/Client/");
                    if (!System.IO.Directory.Exists(ServerFolder))
                    {
                        System.IO.Directory.CreateDirectory(ServerFolder);
                    }
                    ServerFilePath = ServerFolder + FileName;
                    if (FileName != string.Empty)
                    {
                        ViewState["FileName"] = "~/Files/Client/" + FileName;
                    }
                    FileUpload1header.SaveAs(ServerFilePath);

                    try
                    {
                        client.ClientId = ClientId;
                        client.LogoFilepath = ViewState["FileName"].ToString();
                        client.updateheaderlogo();
                        lblheadernotification.Text = "Uploaded Successfully";
                        lblheadernotification.ForeColor = System.Drawing.Color.Green;
                        mpheader.Show();
                        //System.Threading.Thread.Sleep(3000);
                        //mpheader.Hide();
                    }
                    catch
                    {
                        lblheadernotification.Text = "Something went worng!..";
                        lblheadernotification.ForeColor = System.Drawing.Color.Red;
                        mpheader.Show();
                    }
                    //lblimagepath.Text = ViewState["FileName"].ToString().Substring(1);
                }
                else
                {
                    lblheadernotification.Text = "Must upload image file only ";
                    lblheadernotification.ForeColor = System.Drawing.Color.Red;
                    mpheader.Show();
                }
                Bindlogo();

            }
            else
            {
                lblheadernotification.Text = "Please upload image file";
                lblheadernotification.ForeColor = System.Drawing.Color.Red;
                mpheader.Show();
            }

        }
        protected void header_imageButton_Click(object sender, ImageClickEventArgs e)
        {
            if (RoleId == 2 || RoleId == 1)
            {
                lblheadernotification.Text = "";
                mpheader.Show();
            }
        }
        protected void btnviewall_Click(object sender, EventArgs e)
        {
            Response.Redirect("Alert.aspx");
        }
        protected void lnkMarkRead_Click(object sender, EventArgs e)
        {
            alert = new AlertBAL();
            alert.UserId = UserId;
            try
            {
                alert.UpdateAlert_1ByUserId();
                BindList();
            }
            catch { }
        }


        private void BindClientDDL()
        {
            try
            {

                user = new usrBAL();
                System.Web.UI.WebControls.ListItem li3 = new System.Web.UI.WebControls.ListItem("Super Admin DashBoard", "0");
                dtClientId.Items.Clear();
                dtClientId.Items.Add(li3);
                user.ClientId = ClientId;
                dtClientId.DataSource = user.GetClientName();
                dtClientId.DataTextField = "ClientName";
                dtClientId.DataValueField = "ClientId";
                dtClientId.DataBind();
                try
                {
                    if (!string.IsNullOrEmpty(Session["SelectedIndex"].ToString()))
                    {
                        dtClientId.SelectedIndex = Convert.ToInt32(Session["SelectedIndex"].ToString());
                        BindImage();
                    }
                }
                catch
                {
                    dtClientId.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                user = null;

            }
        }
        protected void dtClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            gsrch = new GingerboxSrch();
            dt = new DataTable();
            if (Convert.ToInt32(dtClientId.SelectedValue.ToString()) > 0)
            {
                Session["SelectedIndex"] = dtClientId.SelectedIndex;
                Session["ClientId"] = Convert.ToInt32(dtClientId.SelectedValue.ToString());
                Session["Role"] = 2;
                Session["RoleId"] = 2;
                Response.Redirect("~/AdminDashBoard.aspx");
            }
            else
            {
                Session["SelectedIndex"] = dtClientId.SelectedIndex;
                Session["Role"] = 1;
                Session["RoleId"] = 1;
                Response.Redirect("~/SADashBoard.aspx");
            }

        }
        public void CheckAPI()
        {
            userBAL = new UserBAL();
            DataTable dt = userBAL.CheckApiForMapsLoad(ClientId);
            if (dt.Rows.Count>0)
            {
                hiddenclientapi.Value = dt.Rows[0]["API"].ToString().Replace("<API_KEY>", dt.Rows[0]["Key"].ToString());
            }
            else
            {
                hiddenclientapi.Value = "GOOGLE";
            }
        }
    }
}
