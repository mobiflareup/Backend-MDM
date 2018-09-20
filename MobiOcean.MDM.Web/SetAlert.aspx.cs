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
    public partial class SetAlert : Base
    {
        AlertBAL sprt;
        GingerboxSrch sr;
        int UserId, ClientId, DeptId, RoleId;

        DataTable dtt;

        protected void Page_Load(object sender, EventArgs e)
        {

            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                griddd();
            }
        }
        protected void griddd()
        {

            sr = new GingerboxSrch();
            dtt = new DataTable();
            dtt = sr.AlertDetails(UserId);
            GridDetails.DataSource = dtt;
            GridDetails.DataBind();

        }
        protected void GridDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView GridDetails = (DataRowView)e.Row.DataItem;

                    ImageButton imgbtnyes = (ImageButton)e.Row.FindControl("btnyes");
                    ImageButton imgbtnno = (ImageButton)e.Row.FindControl("btnNo");
                    CheckBox chk3 = (CheckBox)e.Row.FindControl("IsEmailChkBx");
                    Label AlertTypeId = (Label)e.Row.FindControl("labAlertTypeId");

                    try
                    {
                        chk3.Checked = Convert.ToBoolean(GridDetails["IsEmail"]);
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
        protected void GridDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Yes")
            {
                GridViewRow gvr = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                //GridView GridFeature = (GridView)(gvr.Parent.Parent);
                ImageButton imgbtnyes = (ImageButton)gvr.FindControl("btnyes");
                ImageButton imgbtnno = (ImageButton)gvr.FindControl("btnNo");
                CheckBox chk4 = (CheckBox)gvr.FindControl("IsEmailChkBx");
                Label lblenable = (Label)gvr.FindControl("lblenable");
                try
                {
                    chk4.Checked = false;
                    imgbtnyes.Visible = false;
                    imgbtnno.Visible = true;
                    if (lblenable.Text == "0")
                    {
                        lblenable.Text = "1";
                    }
                    else
                    {
                        lblenable.Text = "0";
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
                CheckBox chk5 = (CheckBox)gvr.FindControl("IsEmailChkBx");
                Label lblenable = (Label)gvr.FindControl("lblenable");
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
                if (lblenable.Text == "0")
                {
                    lblenable.Text = "1";
                }
                else
                {
                    lblenable.Text = "0";
                }

            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow rows in GridDetails.Rows)
            {

                if (rows.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        sprt = new AlertBAL();
                        sprt.UserId = UserId;
                        Label label = ((Label)rows.FindControl("labAlertTypeId"));
                        sprt.AlerttypeId = Convert.ToInt32(label.Text);
                        sprt.IsEmail = ((CheckBox)rows.FindControl("IsEmailChkBx")).Checked ? 1 : 0;
                        sprt.EmailStatusUp1();

                    }
                    catch (Exception)
                    {
                        //
                    }



                }

            }

        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            //Session["ProfileId"] = null;
            // Response.Redirect("AdminDashBoard.aspx");
        }
    }
}