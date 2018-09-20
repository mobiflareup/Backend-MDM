using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class Alert : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        AlertBAL alert;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            alert = new AlertBAL();
            alert.UserId = UserId;
            grdAlert.DataSource = alert.GetAlertDetailsByUserId();
            grdAlert.DataBind();
        }
        protected void grdAlert_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAlert.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void grdAlert_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label IsRead = ((Label)e.Row.FindControl("lblIsRead"));
                CheckBox chkbox = ((CheckBox)e.Row.FindControl("chkbox"));
                if (IsRead.Text == "0")
                {
                    chkbox.Checked = false;
                }
                else
                {
                    chkbox.Checked = true;
                    chkbox.Enabled = false;
                }
            }
        }
        protected void btnApplyChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string AlertList = "";
                for (int idx = 0; idx < grdAlert.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdAlert.Rows[idx].FindControl("chkbox"))).Checked)
                    {
                        AlertList = AlertList + ((Label)grdAlert.Rows[idx].FindControl("lblId")).Text + ",";
                    }
                }
                if (AlertList != "")
                {
                    alert = new AlertBAL();
                    alert.UserId = UserId;
                    alert.AlertIdList = AlertList;
                    int res = alert.MarkAlertDetails();
                    if (res > 0)
                    {
                        lblmsg.Text = "Changes Applied Successfully";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        BindGrid();
                    }
                    else
                    {
                        lblmsg.Text = "Changes Not Saved";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}