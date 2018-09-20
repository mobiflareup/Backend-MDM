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
    public partial class CorporateDataSecurity : Base
    {
        int ClientId, UserId, RoleId, ProfileId, DeptId;
        int CategoryId = 11;
        string  ProfileName;
        ProfileBAL probal;
        VikramSearch srch;        

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
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            srch = new VikramSearch();
            grdDevice.DataSource = srch.srchProfilefeaturebycategoryid(ProfileId, CategoryId);
            grdDevice.DataBind();
        }      
        protected void grdDevice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView grdDevice = (DataRowView)e.Row.DataItem;
                    ImageButton imgbtnyes = (ImageButton)e.Row.FindControl("btnyes");
                    ImageButton imgbtnno = (ImageButton)e.Row.FindControl("btnNo");
                    CheckBox chk3 = (CheckBox)e.Row.FindControl("switchsize");
                    Label lblIsScheduleNeed = (Label)e.Row.FindControl("lblIsScheduleNeed");
                    try
                    {
                        chk3.Checked = Convert.ToBoolean(grdDevice["IsEnable"]);
                    }
                    catch (Exception) { chk3.Checked = false; }
                    if (chk3.Checked)
                    {
                        imgbtnyes.Visible = true;
                    }
                    else
                    {
                        imgbtnno.Visible = true;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        protected void grdDevice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Yes")
            {
                GridViewRow gvr = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                //GridView GridFeature = (GridView)(gvr.Parent.Parent);
                ImageButton imgbtnyes = (ImageButton)gvr.FindControl("btnyes");
                ImageButton imgbtnno = (ImageButton)gvr.FindControl("btnNo");
                // LinkButton lnkSchedule = (LinkButton)gvr.FindControl("Schedule");
                CheckBox chk4 = (CheckBox)gvr.FindControl("switchsize");
                // Label lblIsScheduleNeed = (Label)gvr.FindControl("lblIsScheduleNeed");
                Label lblChanged = (Label)gvr.FindControl("lblChanged");
                //Label lblCId = (Label)gvr.FindControl("lblCId");

                try
                {
                    chk4.Checked = false;
                    imgbtnyes.Visible = false;
                    imgbtnno.Visible = true;
                    if (lblChanged.Text == "0")
                    {
                        lblChanged.Text = "1";
                    }
                    else
                    {
                        lblChanged.Text = "0";
                    }
                }
                catch (Exception)
                {
                    chk4.Checked = false;
                    imgbtnyes.Visible = true;
                    imgbtnno.Visible = false;
                }
            }
            else
            {
                GridViewRow gvr = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                //GridView GridFeature = (GridView)(gvr.Parent.Parent);
                ImageButton imgbtnyes = (ImageButton)gvr.FindControl("btnyes");
                ImageButton imgbtnno = (ImageButton)gvr.FindControl("btnNo");
                // LinkButton lnkSchedule = (LinkButton)gvr.FindControl("Schedule");
                CheckBox chk5 = (CheckBox)gvr.FindControl("switchsize");
                // Label lblIsScheduleNeed = (Label)gvr.FindControl("lblIsScheduleNeed");
                Label lblChanged = (Label)gvr.FindControl("lblChanged");
                //Label lblCId = (Label)gvr.FindControl("lblCId");
                try
                {
                    chk5.Checked = true;
                    imgbtnyes.Visible = true;
                    imgbtnno.Visible = false;
                }
                catch (Exception)
                {
                    chk5.Checked = false;
                    imgbtnyes.Visible = true;
                    imgbtnno.Visible = false;

                }
                if (lblChanged.Text == "0")
                {
                    lblChanged.Text = "1";
                }
                else
                {
                    lblChanged.Text = "0";
                }
            }

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow rows in grdDevice.Rows)
            {
                if (rows.RowType == DataControlRowType.DataRow)
                {
                    if (((Label)rows.FindControl("lblChanged")).Text.Trim() == "1")
                    {
                        try
                        {
                            probal = new ProfileBAL();
                            probal.ClientId = ClientId;
                            probal.ProfileId = ProfileId;
                            probal.FeatureId = Convert.ToInt32(((Label)rows.FindControl("lblId")).Text.Trim());
                            probal.IsEnable = ((CheckBox)rows.FindControl("switchsize")).Checked ? 1 : 0;
                            probal.LoggedBy = UserId.ToString();
                            probal.IU_ProfileFetaureON();
                            probal.MoveDatainOriginalTables();
                        }
                        catch (Exception) { }
                    }
                }
            }
            //Response.Redirect("Feature.aspx");
            MP1.Show();
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            mpcancel.Show();
        }
        protected void ok_Click(object sender, EventArgs e)
        {
            Response.Redirect("Feature.aspx");
        }
        protected void btncancelok_Click(object sender, EventArgs e)
        {
            Response.Redirect("Feature.aspx");
        }
        protected void btncancelcan_Click(object sender, EventArgs e)
        {
            mpcancel.Hide();
        }
    }
}