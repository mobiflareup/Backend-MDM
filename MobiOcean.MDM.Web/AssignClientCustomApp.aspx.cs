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
    public partial class AssignClientCustomApp : Base
    {
        int ClientId, UserId, RoleId;
        DataTable dt;
        ClientBAL clientBal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            if (!IsPostBack)
            {
                ViewState["userid"] = Convert.ToInt32(Request.QueryString["Id"]);
                HiddenField.Text = Request.QueryString["Id"].ToString();
                dt = new DataTable();
                clientBal = new ClientBAL();
                clientBal.ClientId = Convert.ToInt32(HiddenField.Text);
                dt = clientBal.GetClientByClientId();
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblClientCode.Text = dt.Rows[0]["ClientCode"].ToString();
                    lblClientName.Text = dt.Rows[0]["ClientName"].ToString();
                }
                BindGrid();
            }
        }

        private void BindGrid()
        {
            dt = new DataTable();
            clientBal = new ClientBAL();
            clientBal.ClientId = Convert.ToInt32(HiddenField.Text);
            dt = clientBal.GetMobioceanAppTypes();
            grdClient.DataSource = dt;
            grdClient.DataBind();
        }


        protected void btnAssigned_Click(object sender, EventArgs e)
        {
            int res = 0;
            string UserIdList = "";
            for (int idx = 0; idx < grdClient.Rows.Count; idx++)
            {
                if (((CheckBox)(grdClient.Rows[idx].FindControl("AchkRow_Parents"))).Checked)
                {
                    UserIdList = UserIdList + ((Label)grdClient.Rows[idx].FindControl("lblAppTypeId")).Text + ",";
                }
            }
            UserIdList = UserIdList.Trim(',');
            clientBal = new ClientBAL();
            clientBal.ClientId = Convert.ToInt32(HiddenField.Text);
            clientBal.AppTypeIdList = UserIdList;
            res = clientBal.AssignCustomAppToClient();
            if (res > 0)
            {
                lblMsg.Text = "App Successfully Assigned to Client!!!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                Response.Redirect("ClientMaster?Res=" + res);
            }
            else
            {
                lblMsg.Text = "Something went wrong!!!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                Response.Redirect("ClientMaster?Res=" + res);
            }
            BindGrid();

        }

        protected void grdClient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int count = grdClient.Rows.Count;
                int c = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView grdUser1 = (DataRowView)e.Row.DataItem;
                    CheckBox cb = (CheckBox)e.Row.FindControl("AchkRow_Parents");
                    Label lblStatus = (Label)e.Row.FindControl("lblIsActive");

                    if (lblStatus.Text.Trim() == "1")
                    {
                        cb.Checked = true;
                    }
                    else
                        cb.Checked = false;
                }
                for (int idx = 0; idx < grdClient.Rows.Count; idx++)
                {
                    if (((CheckBox)(grdClient.Rows[idx].FindControl("AchkRow_Parents"))).Checked)
                    {
                        c++;
                    }
                }
                CheckBox chkheader = (CheckBox)grdClient.HeaderRow.FindControl("AchkHeader_Parents");
                if (c == count)
                {

                    chkheader.Checked = true;
                }
                else
                {
                    chkheader.Checked = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ClientMaster.aspx");
        }
        protected void AchkHeader_Parents_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkheader = (CheckBox)grdClient.HeaderRow.FindControl("AchkHeader_Parents");
            foreach (GridViewRow row in grdClient.Rows)
            {
                CheckBox chkrow = (CheckBox)row.FindControl("AchkRow_Parents");
                if (chkheader.Checked == true)
                {
                    chkrow.Checked = true;
                }
                else
                {
                    chkrow.Checked = false;
                }
            }
        }

        protected void AchkRow_Parents_CheckedChanged(object sender, EventArgs e)
        {
            int count = grdClient.Rows.Count;
            int c = 0;
            GridViewRow gvr = ((GridViewRow)((CheckBox)sender).NamingContainer);
            for (int idx = 0; idx < grdClient.Rows.Count; idx++)
            {
                if (((CheckBox)(grdClient.Rows[idx].FindControl("AchkRow_Parents"))).Checked)
                {
                    c++;
                }
            }
            CheckBox chkheader = (CheckBox)grdClient.HeaderRow.FindControl("AchkHeader_Parents");
            if (c == count)
            {
                chkheader.Checked = true;
            }
            else
            {
                chkheader.Checked = false;
            }
        }
    }
}