using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class KeywordMgmt : Base
    {
        KeywordBAL keybal;
        VikramSearch srch;
        int ClientId, UserId, RoleId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["Role"].ToString());
            lblMsg.Text = string.Empty;
            grdKey.EditIndex = -1;
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        public void BindGrid()
        {
            srch = new VikramSearch();
            grdKey.DataSource = srch.srchKeywordDtls(ClientId, txtSrchKCode.Text.Trim(), txtSrchKName.Text.Trim());
            grdKey.DataBind();

        }
        protected void grdKey_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdKey.EditIndex = -1;
            BindGrid();
        }
        protected void grdKey_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow gvr = grdKey.Rows[e.RowIndex];
            try
            {
                lblkeyid.Text = ((Label)gvr.FindControl("lblUId")).Text.Trim();
                mpdelete.Show();

            }
            catch (Exception)
            {
            }
        }
        protected void grdKey_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdKey.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void grdKey_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = grdKey.Rows[e.RowIndex];
            try
            {
                keybal = new KeywordBAL();
                keybal.ClientId = ClientId;
                keybal.KeywordId = Convert.ToInt32(((Label)gvr.FindControl("lblUId")).Text.Trim());
                keybal.KeywordCode = ((TextBox)gvr.FindControl("txtKeywordCode")).Text.Trim();
                keybal.KeywordName = ((TextBox)gvr.FindControl("txtKeywordName")).Text.Trim();
                keybal.Description = ((TextBox)gvr.FindControl("txtDescription")).Text.Trim();
                string res = keybal.insertKeywordDtl();
                if (int.Parse(res) > 0)
                {
                    lblMsg.Text = "Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    grdKey.EditIndex = -1;
                    BindGrid();
                }
                else
                {
                    lblMsg.Text = "Already exists";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }
            }
            catch (Exception)
            {
            }
            finally
            {
                keybal = null;
            }
        }
        protected void grdKey_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdKey.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAddKeywordCode.Text.Trim() == "" || txtAddKeywordName.Text.Trim() == "" || txtAddDescription.Text.Trim() == "")
                {
                    lblMsg.Text = "Fill all mandatory fields!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    keybal = new KeywordBAL();
                    keybal.KeywordId = 0;
                    keybal.ClientId = ClientId;
                    keybal.KeywordCode = txtAddKeywordCode.Text.Trim();
                    keybal.KeywordName = txtAddKeywordName.Text.Trim();
                    keybal.Description = txtAddDescription.Text.Trim();
                    string res = keybal.insertKeywordDtl();
                    if (Convert.ToInt32(res) > 0)
                    {
                        lblMsg.Text = "Keyword saved successfully.";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Reset();
                        BindGrid();

                    }
                    else
                    {
                        lblMsg.Text = "Keyword already exists.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;

                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                keybal = null;
            }
        }
        public void Reset()
        {
            txtAddKeywordCode.Text = "";
            txtAddKeywordName.Text = "";
            txtAddDescription.Text = "";
        }
        protected void Yes_Click(object sender, EventArgs e)
        {
            keybal = new KeywordBAL();
            string alertid = lblkeyid.Text;
            keybal.KeywordId = Convert.ToInt32(alertid);
            if (keybal.DeletekeyDtls() == 1)
            {
                lblMsg.Text = "Deleted Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                BindGrid();
            }
            else
            {
                lblMsg.Text = "Not Deleted";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
        }
        protected void No_Click(object sender, EventArgs e)
        {
            lblkeyid.Text = "";
        }
    }
}