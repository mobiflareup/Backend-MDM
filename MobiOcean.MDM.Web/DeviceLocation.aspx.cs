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
    public partial class DeviceLocation : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        DDLBAL ddl;
        AnuSearch srch;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            txtFrmDt.Attributes.Add("readonly", "readonly");
            txtToDt.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                txtFrmDt.Text = txtToDt.Text = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy");
                BindUser();
            }
        }
        protected void BindUser()
        {
            try
            {
                ListItem ls = new ListItem("--- Select User ---", "0");
                ddl = new DDLBAL();
                ddl.ClientId = ClientId;
                ddl.UserId = UserId;
                ddl.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = ddl.GetUserByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = ddl.GetUserWithoutDeptHead();
                }
                else
                {
                    ddlUserName.DataSource = ddl.GetUserByUserId();
                    ddlUserName.SelectedValue = UserId.ToString();
                }
                ddlUserName.DataTextField = "UserName";
                ddlUserName.DataValueField = "UserId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }

        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            if (ddlUserName.SelectedIndex > 0)
            {
                showOnMapEvent();
            }
            else
            {
                lblMsg.Text = "Please Select User whose location you want to see.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void showOnMapEvent()
        {
            if (chkFrmToDatesCorrect())
            {
                ShowOnMap();
            }
            else
            {
                lblMsg.Text = ("Error");
            }
        }
        protected bool chkFrmToDatesCorrect()
        {
            bool isOk = true;

            lblMsg.Text = "";

            if (txtFrmDt.Text.Trim() != "" || txtToDt.Text.Trim() != "")
            {
                if (txtFrmDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
                {
                    if ((Convert.ToDateTime(txtFrmDt.Text)) > (Convert.ToDateTime(txtToDt.Text)))
                    {
                        lblManFields.Text = "Please select the From date should be less than or equal to To date.";
                        isOk = false;
                    }
                }
                else
                {
                    lblManFields.Text = "Please select the From and To date fields correctly.";
                    isOk = false;
                }
            }

            return isOk;
        }
        protected void ShowOnMap()
        {
            srch = new AnuSearch();
            string FrmDateTime = "", ToDateTime = "";
            try
            {
                #region------ Manage From/To Date Time and Duration -----------
                try
                {
                    FrmDateTime = txtFrmDt.Text;
                    if (FrmDateTime.Trim() != "")
                    {
                        FrmDateTime = txtFrmDt.Text.Trim() + " 00:00";
                    }
                }
                catch (Exception)
                {
                    FrmDateTime = txtFrmDt.Text.Trim();
                }
                try
                {
                    ToDateTime = txtToDt.Text;
                    if (ToDateTime.Trim() != "")
                    {
                        ToDateTime = txtToDt.Text.Trim() + " 23:59";
                    }
                }
                catch (Exception)
                {
                    ToDateTime = txtToDt.Text.Trim();
                }
                #endregion

                PrintOnMap(srch.DeviceLocationSrchForMapPointers(ClientId, ddlUserName.SelectedValue.ToString(), txtDeviceName.Text, FrmDateTime, ToDateTime));

            }
            finally
            {
                srch = null;
            }
        }
        protected void PrintOnMap(DataTable dt)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RemoveMarkersFromArray", "RemoveMarkersFromArray();", true);

            if (dt.Rows.Count > 0)
            {
                lblMsg.Text = "";

                string myLogDateTime = "";
                string myLat = "";
                string myLong = "";
                string myLocation = "";
                int isFirstTime = 1;

                for (int index = 0; index < dt.Rows.Count; index++)
                {

                    if (index != 0 && myLocation.Trim() == (dt.Rows[index]["Location"]).ToString().Trim())
                    {
                        myLogDateTime = myLogDateTime + ",<br>" + Convert.ToString(dt.Rows[index]["LogDateTime"]);
                    }
                    else
                    {
                        myLogDateTime = Convert.ToString(dt.Rows[index]["LogDateTime"]);
                    }

                    myLat = Convert.ToString(dt.Rows[index]["Latitude"]);
                    myLong = Convert.ToString(dt.Rows[index]["Longitude"]);
                    myLocation = Convert.ToString(dt.Rows[index]["Location"]);


                    try
                    {
                        if (myLocation.Trim() != Convert.ToString(dt.Rows[index + 1]["Location"]).Trim())
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "My Map" + index.ToString(), "ShowOnMap( '" + isFirstTime + "','"
                                + myLogDateTime + "','" + myLocation + "','"
                            + myLat + "','" + myLong + "',0);", true);

                            isFirstTime = 0;
                        }
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "My Map" + index.ToString(), "ShowOnMap( '" + isFirstTime + "','"
                                    + myLogDateTime + "','" + myLocation + "','"
                                + myLat + "','" + myLong + "',0);", true);
                        isFirstTime = 0;
                    }
                }

                //--------- Now we call the function which show all the markers in windwos size ------
                try
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showAllMarkersOnWindow", "showAllMarkersOnWindow();", true);
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }
            }
            else
            {

                lblMsg.Text = "No location found for your search.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            showOnMapEvent();
        }
    }
}