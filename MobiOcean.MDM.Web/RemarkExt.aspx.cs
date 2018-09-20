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
    public partial class RemarkExt : Base
    {
        int ClientId, UserId;
        AnuSearch srch;
        DataTable dt;
        AckBAL ack;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
            }
            lblMsg.Text = string.Empty;
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            dt = new DataTable();
            dt = srch.RemarExtData(ClientId);
            grdMode.DataSource = dt;
            grdMode.DataBind();            
        }
        protected void grdMode_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdMode.PageIndex = e.NewPageIndex;
            grdMode.EditIndex = -1;
            BindGrid();
        }
        protected void grdMode_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdMode.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void grdMode_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow gvr = grdMode.Rows[e.RowIndex];
                Label lbl = ((Label)gvr.FindControl("lblId"));
                lblalerturlid.Text = lbl.Text.Trim();
                grdMode.EditIndex = -1;
                mpdelete.Show();
            }
            catch (Exception)
            {

            }
        }

        protected void grdMode_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdMode.Rows[e.RowIndex];
            try
            {
               
                ack = new AckBAL();
                ack.RemarkId = Convert.ToInt32(((Label)gvr.FindControl("lblId")).Text.Trim());
                ack.ClientId = Convert.ToInt32(Session["ClientId"].ToString());
                ack.UserId = Convert.ToInt32(Session["UserId"].ToString());
                ack.Remark = ((TextBox)gvr.FindControl("txtERemark")).Text.Trim();              

                int res = ack.GetRemarkData();
                if (res > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdMode.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Already Exists!!!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    grdMode.EditIndex = -1;
                    BindGrid();
                }
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            catch (Exception)
            {

            }
        }
        protected void grdMode_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdMode.EditIndex = -1;
            BindGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ack = new AckBAL();
                ack.ClientId = Convert.ToInt32(Session["ClientId"].ToString());
                ack.UserId = Convert.ToInt32(Session["UserId"].ToString());
                ack.Remark = txtRemark.Text.Trim();


                int res = ack.InsertRemarkData();
                if (res > 0)
                {
                    lblMsg.Text = "Inserted Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Rest();
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Already Exists!!!";
                    Rest();
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    BindGrid();
                }
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            catch (Exception ex)
            {

            }
        }

        protected void rest_Click(object sender, EventArgs e)
        {
            Rest();
        }
        private void Rest()
        {
            txtRemark.Text = "";         
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            ack = new AckBAL();
            ack.RemarkId = Convert.ToInt32(lblalerturlid.Text);
            ack.UserId = Convert.ToInt32(Session["UserId"].ToString());
            if(ack.DeleteRemarkData() > 0)
            {
                lblMsg.Text = "Deleted successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Not deleted ";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }

        protected void No_Click(object sender, EventArgs e)
        {
            lblalerturlid.Text = "";
        }




    }
}