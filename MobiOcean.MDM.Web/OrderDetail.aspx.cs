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
    public partial class OrderDetail : Base
    {
        DDLBAL ddl;
        int ClientId, RoleId, UserId, DeptId;
        AnuSearch srch;
        OrderBAL order;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblmsg.Text = string.Empty;
            lblMsgPop.Text = string.Empty;
            if (!IsPostBack)
            {
                BindUsrDDL();
                BindGrid();
            }
        }
        protected void BindUsrDDL()
        {
            ListItem ls = new ListItem("--- Select ---", "0");
            try
            {
                ddl = new DDLBAL();
                ddl.ClientId = ClientId;
                ddl.UserId = UserId;
                ddl.DeptId = DeptId;
                ddlEmployee.Items.Clear();
                ddlEmployee.Items.Add(ls);
                if (RoleId == 1 || RoleId == 2)
                {
                    ddlEmployee.DataSource = ddl.GetUserByClientId();
                }
                else if (RoleId == 3)
                {
                    ddlEmployee.DataSource = ddl.GetUserByDptHead();
                }
                else
                {
                    ddlEmployee.Items.Clear();
                    ddlEmployee.DataSource = ddl.GetUserByUserId();
                }
                ddlEmployee.DataTextField = "UserName";
                ddlEmployee.DataValueField = "UserId";
                ddlEmployee.DataBind();
            }
            catch (Exception) { }
            finally
            {
                ls = null;
            }
        }
        protected void BindGrid()
        {
            srch = new AnuSearch();
            if (RoleId == 1 || RoleId == 2)
            {
                gdvStudent.DataSource = srch.GetOrderDetails(txtCustName.Text.Trim(), ddlEmployee.SelectedValue.ToString(), ddlpayment.SelectedValue.ToString(), ddlapprove.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim());
            }
            else if (RoleId == 3)
            {
                gdvStudent.DataSource = srch.GetOrderDetails(txtCustName.Text.Trim(), ddlEmployee.SelectedValue.ToString(), ddlpayment.SelectedValue.ToString(), ddlapprove.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim());
            }
            else
            {
                gdvStudent.DataSource = srch.GetOrderDetails(txtCustName.Text.Trim(), ddlEmployee.SelectedValue.ToString(), ddlpayment.SelectedValue.ToString(), ddlapprove.SelectedValue.ToString(), txtFrmDate.Text.Trim(), txtToDate.Text.Trim());
            }
            gdvStudent.DataBind();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void lbtnordernumber_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkbtn = sender as LinkButton;
                GridViewRow gvr = lnkbtn.NamingContainer as GridViewRow;
                Label lblId = (Label)gdvStudent.Rows[gvr.RowIndex].FindControl("lblId");
                lblPopStuId.Text = lblId.Text;
                BindOrder(lnkbtn.Text);
                BindGrid1();
                mpeManFields.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void BindOrder(string OrderNo)
        {
            try
            {
                order = new OrderBAL();
                dt = new DataTable();
                order.orderno = OrderNo;
                dt = order.GetOrderMasterDetails();
                if (dt.Rows.Count > 0)
                {
                    lblPopStuId.Text = dt.Rows[0]["OrderMasterId"].ToString();
                    lblPopStuInfo.Text = dt.Rows[0]["OrderNo"].ToString();
                    mplbldlr.Text = dt.Rows[0]["CustomerName"].ToString();
                    mplblordrdate.Text = dt.Rows[0]["OrderDate"].ToString();
                    mplblordrby.Text = dt.Rows[0]["UserName"].ToString();
                    mplblAprovedby.Text = dt.Rows[0]["ApproverName"].ToString();
                    if (dt.Rows[0]["OrderStatus"].ToString() == "1")
                    {
                        mplblarstatus.Text = "Pending";
                        //lblstatusp.Text = "Not Approved";
                    }
                    else if (dt.Rows[0]["OrderStatus"].ToString() == "2")
                    {
                        mplblarstatus.Text = "Approved";
                        //lblstatusp.Text = "Approved";
                    }
                    else
                    {
                        mplblarstatus.Text = "Rejected";
                        //lblstatusp.Text = "Rejected";
                    }
                    mplblttlamount.Text = dt.Rows[0]["TotalAmount"].ToString();

                }


            }
            catch (Exception)
            { //lblTblhd.Text = ex.Message; 
            }
        }
        protected void BindGrid1()
        {
            try
            {

                order = new OrderBAL();
                dt = new DataTable();
                order.ordermasterid = Convert.ToInt32(lblPopStuId.Text);
                dt = order.GetOrderDetails();
                gdvSelectBin.DataSource = dt;
                gdvSelectBin.DataBind();
                //if (UsrRole == 5)
                //{
                //    gdvSelectBin.Columns[11].Visible = false;
                //}
                //PrintGridView.DataSource = dt;
                //PrintGridView.DataBind();
                mpeManFields.Show();
            }
            //catch (Exception ex) { lblTblhd.Text = ex.Message; }
            finally
            {
                order = null;
                dt = null;
            }
        }
        protected void gdvStudent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gdvStudent.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void gdvStudent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow gvr = gdvStudent.Rows[e.RowIndex];
                lblUser.Text = "Are you sure to delete this order?";
                lblalertprofileid.Text = ((Label)gvr.FindControl("lblId")).Text.Trim();
                lblname.Text = "1";
                mpdelete.Show();
            }
            catch (Exception)
            {

            }
        }
        protected void gdvStudent_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gdvStudent.EditIndex = -1;
            BindGrid();
        }
        protected void gdvStudent_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow gvr = gdvStudent.Rows[e.RowIndex];
                order = new OrderBAL();
                order.ordermasterid = Convert.ToInt32(((Label)(gvr.FindControl("lblId"))).Text);
                order.orderstatus = Convert.ToInt32(((DropDownList)(gvr.FindControl("DDIsApproved"))).SelectedValue);
                order.ispaymentreceived = Convert.ToInt32(((DropDownList)(gvr.FindControl("DDIspaymentreceived"))).SelectedValue);
                int res = order.UpdateOrderMaster();
                if (res > 0)
                {
                    lblmsg.Text = "Updated Successfully";
                    gdvStudent.EditIndex = -1;
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    BindGrid();
                }
                else
                {
                    lblmsg.Text = "Not Updated";
                    gdvStudent.EditIndex = -1;
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    BindGrid();
                }
            }
            catch (Exception)
            {

            }
        }
        protected void gdvStudent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DropDownList orderstatus = (DropDownList)e.Row.FindControl("DDIsApproved");
                DropDownList paymentstatus = (DropDownList)e.Row.FindControl("DDIspaymentreceived");
                Label lblstatus = (Label)e.Row.FindControl("lblEstatus");
                Label lblpayment = (Label)e.Row.FindControl("lblEpaymentstatus");
                Label pay = (Label)e.Row.FindControl("lblpaymentstatusstring");
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                    orderstatus.SelectedValue = lblstatus.Text;
                    paymentstatus.SelectedValue = lblpayment.Text;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label status = (Label)e.Row.FindControl("lblstatus");
                    Label payment = (Label)e.Row.FindControl("lblpaymentstatus");
                    if (status.Text == "3")
                    {
                        payment.Text = "---";
                        payment.Visible = true;
                        pay.Visible = false;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            string id = lblname.Text;
            order = new OrderBAL();
            if (id == "1")
            {
                order.ordermasterid = Convert.ToInt32(lblalertprofileid.Text);
                int res = order.DeleteOrderMaster();
                if (res > 0)
                {
                    lblmsg.Text = "Order deleted successfully!";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    BindGrid();
                }
                else
                {
                    lblmsg.Text = "Order Not deleted.";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    BindGrid();
                }
            }
            else
            {
                order.orderdetailid = Convert.ToInt32(lblalertprofileid.Text);
                int res = order.DeleteOrderDetails();
                if (res > 0)
                {
                    lblMsgPop.Text = "Order deleted successfully!";
                    lblMsgPop.ForeColor = System.Drawing.Color.Green;
                    BindGrid1();
                }
                else
                {
                    lblMsgPop.Text = "Order Not deleted.";
                    lblMsgPop.ForeColor = System.Drawing.Color.Red;
                    BindGrid1();
                }
            }

            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblmsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblalertprofileid.Text = "";
        }
        protected void gdvSelectBin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gdvSelectBin.EditIndex = e.NewEditIndex;
            BindGrid1();

        }
        protected void gdvSelectBin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gdvSelectBin.EditIndex = -1;
            BindGrid1();

        }
        protected void gdvSelectBin_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string salesrate = "", quantity = "";
            try
            {
                GridViewRow gvr = gdvSelectBin.Rows[e.RowIndex];
                order = new OrderBAL();
                order.orderdetailid = Convert.ToInt32(((Label)(gvr.FindControl("lblOrderDetailId"))).Text);
                order.quantity = Convert.ToInt32(((TextBox)gvr.FindControl("mptxtQuantity")).Text);
                quantity = ((TextBox)gvr.FindControl("mptxtQuantity")).Text;
                salesrate = ((Label)gvr.FindControl("mplblsales")).Text;
                order.totalamt = (Convert.ToInt32(quantity) * Convert.ToInt32(salesrate)).ToString();
                int res = order.UpdateOrderDetails();
                if (res > 0)
                {
                    lblMsgPop.Text = "Updated Successfully";
                    gdvSelectBin.EditIndex = -1;
                    lblMsgPop.ForeColor = System.Drawing.Color.Green;
                    BindGrid1();

                }
                else
                {
                    lblMsgPop.Text = "Not Updated";
                    gdvSelectBin.EditIndex = -1;
                    lblMsgPop.ForeColor = System.Drawing.Color.Red;
                    BindGrid1();

                }
            }
            catch (Exception)
            {

            }
        }
        protected void gdvSelectBin_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow gvr = gdvSelectBin.Rows[e.RowIndex];
                lblUser.Text = "Are you sure to delete this order details?";
                lblalertprofileid.Text = ((Label)gvr.FindControl("lblOrderDetailId")).Text.Trim();
                lblname.Text = "2";
                mpeManFields.Show();
                mpdelete.Show();
            }
            catch (Exception)
            {

            }
        }
    }
}