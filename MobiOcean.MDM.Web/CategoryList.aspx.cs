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
    public partial class CategoryList : Base
    {
        AnuSearch srch;
        DataTable dt;
        AckBAL ack;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int client = Convert.ToInt32(Session["ClientId"]);
                srch = new AnuSearch();
                dt = new DataTable();
                dt = srch.CategoryGrp(client);
                CategoryLt.DataSource = dt;
                CategoryLt.DataTextField = "CategoryName";
                CategoryLt.DataValueField = "CategoryId";
                CategoryLt.Items.Clear();
                CategoryLt.Items.Add(new ListItem("--- Select Category Name ---", ""));
                CategoryLt.DataBind();
                lblMsg.Text = "";
                //BindGrid();
            }

        }
        protected void BindGrid()
        {

            srch = new AnuSearch();
            dt = new DataTable();
            int CategoryId = string.IsNullOrWhiteSpace(CategoryLt.SelectedValue) ? 0 : Convert.ToInt32(CategoryLt.SelectedValue);
            if (CategoryId > 0)
            {
                UserList.Visible = true;
                dt = srch.GetCategoryUserList(Convert.ToInt32(Session["ClientId"]), CategoryId);
                int count = srch.GetCategoryUserListCount(Convert.ToInt32(Session["ClientId"]), CategoryId);
                UserList.DataSource = dt;
                UserList.DataBind();
                CheckBox chk_hdr = (CheckBox)UserList.HeaderRow.FindControl("UserEnbl_header");
                chk_hdr.Checked = dt.Rows.Count == count ? true : false;
            }
            else
            {
                UserList.Visible = false;
            }
        }
        protected void CategoryLt_SelectedIndexChanged(object sender, EventArgs e)
        {

            dt = new DataTable();            
            srch = new AnuSearch();
            if (CategoryLt.SelectedValue != "")
            {
                dt = srch.GetCategoryInfo(Convert.ToInt32(CategoryLt.SelectedValue), Convert.ToInt32(Session["ClientId"]));
                if (dt.Rows.Count > 0)
                {
                    txtLicense.Text = dt.Rows[0]["License"].ToString();
                    txtExpDate.Text = dt.Rows[0]["ExpiryDateTime"].ToString();
                    BindGrid();
                }
                else
                    ClearGrid();
            }
            else
                ClearGrid();
        }
        private void ClearGrid()
        {
            txtLicense.Text = "";
            txtExpDate.Text = "";
            BindGrid();
        }
        protected void UserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            UserList.PageIndex = e.NewPageIndex;
            UserList.EditIndex = -1;
            BindGrid();
        }
        protected void UserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void UserEnbl_header_CheckedChanged(object sender, EventArgs e)
        {
            if (CategoryLt.SelectedValue != "")
            {
                int ClientId = Convert.ToInt32(Session["ClientId"]);
                int CategoryId = Convert.ToInt32(CategoryLt.SelectedValue);
                ack = new AckBAL();
                int Status;
                dt = new DataTable();
                srch = new AnuSearch();
                CheckBox chk_hdr = (CheckBox)UserList.HeaderRow.FindControl("UserEnbl_header");
                dt = srch.GetCategoryUserList(ClientId, CategoryId);
                if (dt.Rows.Count <= Convert.ToInt32(txtLicense.Text) || !chk_hdr.Checked)
                {
                    for (int idx = 0; idx < dt.Rows.Count; idx++)
                    {
                        int UserId = Convert.ToInt32(dt.Rows[idx]["UserId"]);
                        Status = chk_hdr.Checked == true? 0:1;                       
                        bool res = ack.InsertUserCaty(CategoryId, UserId, ClientId, Status);
                    }
                    BindGrid();
                    lblMsg.Text = "SucessFully Updated";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    chk_hdr.Checked = false;
                    lblMsg.Text = "You subscribed only " + txtLicense.Text + " licenses! ";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        protected void UserEnbl_CheckedChanged(object sender, EventArgs e)
        {
            if (CategoryLt.SelectedValue != "")
            {
                srch = new AnuSearch();
                ack = new AckBAL();
                int Status;
                bool res;

                int idx = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
                int ClientId = Convert.ToInt32(Session["ClientId"]);
                int CategoryId = Convert.ToInt32(CategoryLt.SelectedValue);
                CheckBox chk = (CheckBox)UserList.Rows[idx].FindControl("UserEnbl");                
                int UserId = Convert.ToInt32(((Label)UserList.Rows[idx].FindControl("lblUserId")).Text);
                int count = srch.GetCategoryUserListCount(ClientId, CategoryId);
                if (count < Convert.ToInt32(txtLicense.Text) || !chk.Checked)
                {
                    Status = chk.Checked == true ? 0 : 1;
                    res = ack.InsertUserCaty(CategoryId, UserId, ClientId, Status);
                    BindGrid();
                    lblMsg.Text = "SucessFully Updated";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    chk.Checked = false;
                    lblMsg.Text = "Only " + txtLicense.Text + " license is accepted ";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

    }
}