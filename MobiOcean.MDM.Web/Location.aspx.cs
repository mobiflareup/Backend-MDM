using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class Location : Base
    {
        int ClientId, UserId, RoleId, DeptId;

        DDLBAL dropbal;
        AnuSearch srch;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindUser();
                txtFrmDt.Text = txtToDt.Text = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy");
                BindGrid();
            }
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUser.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void BindUser()
        {
            try
            {
                ListItem ls = new ListItem("--Select User--", "0");
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
        protected void BindGrid()
        {
            string FrmDateTime = "", ToDateTime = "";
            try
            {

                #region------ Manage From/To Date Time and Duration -----------
                try
                {
                    FrmDateTime = txtFrmDt.Text;

                    if (FrmDateTime.Trim() != "")
                    {
                        FrmDateTime = txtFrmDt.Text.Trim() + " 00:00";
                    }
                }

                catch (Exception)
                {
                    FrmDateTime = txtFrmDt.Text.Trim();
                }
                try
                {
                    ToDateTime = txtToDt.Text;

                    if (ToDateTime.Trim() != "")
                    {
                        ToDateTime = txtToDt.Text.Trim() + " 23:59";
                    }

                }


                catch (Exception)
                {
                    ToDateTime = txtToDt.Text.Trim();
                }



                #endregion
                srch = new AnuSearch();
                if (RoleId == 1 || RoleId == 2)
                {
                    grdUser.DataSource = srch.SrchUserLocationDtls(ClientId, ddlUserName.SelectedValue.ToString(), txtDeviceName.Text.Trim(), FrmDateTime, ToDateTime, 0);
                }
                else if (RoleId == 3)
                {
                    grdUser.DataSource = srch.SrchUserLocationDtls(ClientId, ddlUserName.SelectedValue.ToString(), txtDeviceName.Text.Trim(), FrmDateTime, ToDateTime, 0, DeptId);
                }
                else
                {
                    grdUser.DataSource = srch.SrchUserLocationDtls(ClientId, ddlUserName.SelectedValue.ToString(), txtDeviceName.Text.Trim(), FrmDateTime, ToDateTime, UserId);
                }

                grdUser.DataBind();

            }
            catch (Exception)
            {
            }
            finally
            {
                srch = null;
            }
        }


        protected void PrintAllPages(object sender, EventArgs e)
        {



        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            grdUser.AllowPaging = false;

            BindGrid();

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            grdUser.RenderControl(hw);

            string gridHTML = sw.ToString().Replace("\"", "'")

                .Replace(System.Environment.NewLine, "");

            StringBuilder sb = new StringBuilder();

            sb.Append("<script type = 'text/javascript'>");

            sb.Append("window.onload = new function(){");

            sb.Append("var printWin = window.open('', '', 'left=0");

            sb.Append(",top=0,width=1000,height=600,status=0');");

            sb.Append("printWin.document.write(\"");

            sb.Append(gridHTML);

            sb.Append("\");");

            sb.Append("printWin.document.close();");

            sb.Append("printWin.focus();");

            sb.Append("printWin.print();");

            sb.Append("printWin.close();};");

            sb.Append("</script>");

            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

            grdUser.AllowPaging = true;

            BindGrid();
        }
    }
}