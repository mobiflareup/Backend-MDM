using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace MobiOcean.MDM.Web
{
    public partial class DeviceUserCrntLoc : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AnuSearch srch;
        DDLBAL dropbal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                ddlBind();
                //ShowOnMap();

            }
        }
     
        public void ddlBind()
        {
            try
            {
                System.Web.UI.WebControls.ListItem ls = new System.Web.UI.WebControls.ListItem("--- Select User ---", "0");
                dropbal = new DDLBAL();
                dropbal.ClientId = ClientId;
                dropbal.UserId = UserId;
                dropbal.DeptId = DeptId;
                ddlUserName.Items.Clear();
                ddlUserName.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlUserName.DataSource = dropbal.GetUserByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlUserName.DataSource = dropbal.GetUserWithoutDeptHead();
                }
                else
                {
                    ddlUserName.Items.Clear();
                    ddlUserName.DataSource = dropbal.GetUserByUserId();
                }
                ddlUserName.DataTextField = "UserName";
                ddlUserName.DataValueField = "UserId";
                ddlUserName.DataBind();
            }
            catch (Exception)
            {

            }
        }

        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowOnMap();
        }

        protected void HiddenField1_Load(object sender, EventArgs e)
        {
            ShowOnMap();
        }

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            //if (ddlUserName.SelectedIndex > 0)
            //{
                ShowOnMap();
            //}
            //else
            //{
              //  lblMsg.Text = "Please Select User whose location you want to see.";
                //lblMsg.ForeColor = System.Drawing.Color.Red;
            //}
        }
        [System.Web.Services.WebMethod]
        protected string ShowOnMaps()
        {
            srch = new AnuSearch();
            return JsonConvert.SerializeObject(srch.GetDeviceUserMap(ClientId, txtDeviceName.Text, Convert.ToInt32(ddlUserName.SelectedValue)));
            //return true;
        }
        protected void ShowOnMap()
        {
            srch = new AnuSearch();
            PrintOnMap(srch.GetDeviceUserMap(ClientId, txtDeviceName.Text, Convert.ToInt32(ddlUserName.SelectedValue)));
            
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

                    if (index != 0)
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
                        //if (myLocation.Trim() != (Convert.ToString(dt.Rows[index + 1]["Location"]).Trim()))
                        //{
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "My Map" + index.ToString(), "ShowOnMap( '" + isFirstTime + "','"
                                + myLogDateTime + "','" + myLocation + "','"
                            + myLat + "','" + myLong + "',0);", true);

                            isFirstTime = 0;
                        //}
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
    }
}