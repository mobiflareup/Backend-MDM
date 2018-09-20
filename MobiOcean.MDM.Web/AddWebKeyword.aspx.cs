using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddWebKeyword : Base
    {
        KeywordBAL keybal;
        int ClientId, UserId, RoleId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                Reset();
            }
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKCode.Text.Trim() == "" || txtKName.Text.Trim() == "" || txtKDesc.Text.Trim() == "")
                {
                    lblMsg.Text = "Fill all mandatory fields!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    keybal = new KeywordBAL();
                    keybal.KeywordId = 0;
                    keybal.ClientId = ClientId;
                    keybal.KeywordCode = txtKCode.Text.Trim();
                    keybal.KeywordName = txtKName.Text.Trim();
                    keybal.Description = txtKDesc.Text.Trim();
                    string res = keybal.IU_WebKeyword();
                    if (Convert.ToInt32(res) > 0)
                    {
                        lblMsg.Text = "Keyword saved successfully.";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("WebKeyword.aspx");

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
            txtKName.Text = "";
            txtKCode.Text = "";
            txtKDesc.Text = "";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebKeyword.aspx");
        }
    }
}