﻿
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
    public partial class AddWebsites : Base
    {
        int ClientId, RoleId, UserId, DeptId;
        WebsiteLogsBAL WebBal;
        VikramSearch Srch;
        DataTable dt;
        //LoginBAL client;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (RoleId > 3)
            {
                Response.Redirect("UserDashBoard.aspx");
            }
            if (!IsPostBack)
            {
                WebBal = new WebsiteLogsBAL();
                WebBal.ClientId = ClientId;
                //client = new LoginBAL();
                //client.ClientId = ClientId;
                dt = new DataTable();
                dt = WebBal.VPNDetailsByClientId();
                if (dt.Rows.Count > 0)
                {
                    txtIpAddress.Text = dt.Rows[0]["IPAddress"].ToString();
                    if (Convert.ToInt16(dt.Rows[0]["IsWhiteList"]) == 1)
                        RDBWhiteList.Checked = true;
                    else
                        RDBBlackList.Checked = true;
                    if (Convert.ToInt16(dt.Rows[0]["IsEnabled"]) == 1)
                        RDBEnabled.Checked = true;
                    else
                        RDBNotEnabled.Checked = true;
                }

                BindGroupDDL(ddlSrchGroup, 1);
                BindGroupDDL(ddlGroupName, 0);
                Reset();
                BindGrid();
            }
        }
        protected void BindGroupDDL(DropDownList ddl, int IsSearch)
        {
            ListItem ls = new ListItem("--- Select ---", "-1");
            ListItem ls1 = new ListItem("White List", "1");
            ListItem lsall = new ListItem("Black List", "0");
            ddl.Items.Add(ls);
            ddl.Items.Add(ls1);
            ddl.Items.Add(lsall);

        }
        protected void BindGrid()
        {

            Srch = new VikramSearch();
            grdBlackList.DataSource = Srch.SrchWebsitesRaj(ClientId, Convert.ToInt32(ddlSrchGroup.SelectedValue.ToString()), txtSrchApplication.Text.Trim());
            grdBlackList.DataBind();

        }
        protected void grdBlackList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdBlackList.PageIndex = e.NewPageIndex;
            grdBlackList.EditIndex = -1;
            BindGrid();

        }
        protected void grdBlackList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow gvr = grdBlackList.Rows[e.RowIndex];
                Label lblUrl = (Label)gvr.FindControl("lblUrlId");
                WebBal = new WebsiteLogsBAL();
                lblalerturlid.Text = lblUrl.Text.Trim();
                //string re = WebBal.DeleteUrl();
                mpdelete.Show();
            }
            catch (Exception) { }
            finally
            {

            }
        }

        public bool ChkUrlISOk(string url)
        {
            bool isUrlOk = true;

            url = url.Replace("www.", "").Trim();

            if (url.IndexOf(".") == 0 || (url.IndexOf(".") == url.Length - 1))
            {
                isUrlOk = false;
            }
            else if (url.IndexOf(".") == -1)
            {
                isUrlOk = false;
            }
            return isUrlOk;
        }
        protected void Reset()
        {
            ddlGroupName.SelectedIndex = 0;
            txtApplicationName.Text = string.Empty;
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            grdBlackList.EditIndex = -1;
            BindGrid();
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkUrlISOk(txtApplicationName.Text.Trim()))
                {
                    if (ddlGroupName.SelectedIndex > 0)
                    {
                        WebBal = new WebsiteLogsBAL();
                        WebBal.UrlId = 0;
                        WebBal.ClientId = ClientId;
                        WebBal.Url = txtApplicationName.Text.Trim();
                        WebBal.IsWhiteList = Convert.ToInt32(ddlGroupName.SelectedValue.ToString());
                        WebBal.Status = 0;
                        int res = WebBal.IU_tblBlacklisturlRaj();
                        if (res > 0)
                        {
                            lblMsg.Text = "Website added successfully";
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                            BindGrid();
                            Reset();
                        }
                        else
                        {
                            lblMsg.Text = "Already Exists!";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please choose IsWhiteList!";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "Please enter the Correct URL!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "Something went wrong!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void grdBlackList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            grdBlackList.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdBlackList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdBlackList.EditIndex = -1;
            BindGrid();
        }
        protected void grdBlackList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                ListItem ls = new ListItem("Select", "0");
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList EddlGroupName = (DropDownList)e.Row.FindControl("EddlGroupName");
                    Label ElblGroupName = (Label)e.Row.FindControl("ElblGroupName");
                    BindGroupDDL(EddlGroupName, 0);
                    EddlGroupName.SelectedValue = ElblGroupName.Text;
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "Something Went Wrong!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void grdBlackList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdBlackList.Rows[e.RowIndex];
            try
            {
                if (((DropDownList)gvr.FindControl("EddlGroupName")).SelectedIndex > 0)
                {

                    WebBal = new WebsiteLogsBAL();
                    WebBal.UrlId = Convert.ToInt32(((Label)gvr.FindControl("lblUrlId")).Text.Trim());
                    WebBal.ClientId = ClientId;
                    //WebBal.Url = txtApplicationName.Text.Trim();
                    WebBal.IsWhiteList = Convert.ToInt32(((DropDownList)gvr.FindControl("EddlGroupName")).SelectedValue.ToString());
                    WebBal.Status = 0;
                    int res = WebBal.IU_tblBlacklisturlRaj();
                    if (res > 0)
                    {
                        lblMsg.Text = "Updated Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                        grdBlackList.EditIndex = -1;
                        BindGrid();
                    }
                    else
                    {
                        lblMsg.Text = "Details already exists";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                    }
                }
                else
                {
                    lblMsg.Text = "Please select IsWhiteList.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                }
            }
            finally
            {
                WebBal = null;
            }
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            WebBal = new WebsiteLogsBAL();
            string urlid = lblalerturlid.Text;
            WebBal.UrlId = Convert.ToInt32(urlid);
            WebBal.LoggedBy = UserId.ToString();
            if (WebBal.DeleteUrlRaj() > 0)
            {
                lblMsg.Text = "Website deleted successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                //lblIsChange.Text = "1";
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                BindGrid();
                //txtNo.Text = string.Empty;
            }
            else
            {
                lblMsg.Text = "Website not deleted ";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalerturlid.Text = "";
        }

        protected void Setting_Click(object sender, EventArgs e)
        {
            WebBal = new WebsiteLogsBAL();
            if (RDBEnabled.Checked == true)
            {
                WebBal.IsEnabled = 1;
            }
            else
            {
                WebBal.IsEnabled = 0;
            }
            if (RDBWhiteList.Checked == true)
            {
                WebBal.IsWhiteList = 1;
            }
            else
            {
                WebBal.IsWhiteList = 0;
            }
            WebBal.IPAddress = txtIpAddress.Text;
            WebBal.ClientId = ClientId;
            WebBal.LoggedBy = UserId.ToString();
            if (WebBal.VPNUpdate() > 0)
            {
                SendUpdateMsg();
                lblMsg.Text = "Setting Updated successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Someting went wrong";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
        }
        protected void SendUpdateMsg()
        {
            UserDeviceBAL usrdevice = new UserDeviceBAL();
            dt = new DataTable();
            usrdevice.ClientId = ClientId;
            dt = usrdevice.GetDeviceWithMDM();
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    SendSMSBAL sms = new SendSMSBAL();
                    sms.sendFinalSMS(row["MobileNo1"].ToString(), "GBox set as WP7", ClientId);
                }
                catch (Exception)
                { }
            }

        }
    }
}