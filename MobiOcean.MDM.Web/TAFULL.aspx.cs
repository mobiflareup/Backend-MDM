using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class TAFULL : Base
    {
        GingerboxSrch gsrch;
        usrBAL user;
        DataTable dt;

        int ClientId, UserId, RoleId, DeptId;

        protected void Page_Load(object sender, EventArgs e)
        {

            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());

            txtFrmDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                txtFrmDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString(Constant.DateFormat);
                if (RoleId == 1)
                {
                    BindClientDDL();
                    BindGrid();
                }
                else
                {
                    divclientddl.Visible = false;
                }
                Session["dtTA"] = null;

            }
        }
        private void BindClientDDL()
        {
            try
            {
                user = new usrBAL();
                System.Web.UI.WebControls.ListItem li3 = new System.Web.UI.WebControls.ListItem("All", "0");
                dtClientId.Items.Clear();
                dtClientId.Items.Add(li3);
                user.ClientId = ClientId;
                dtClientId.DataSource = user.GetClientName();
                dtClientId.DataTextField = "ClientName";
                dtClientId.DataValueField = "ClientId";
                dtClientId.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                user = null;

            }
        }
        protected void dtClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {

            gsrch = new GingerboxSrch();
            dt = new DataTable();
            string FrmDateTime = "", ToDateTime = "";
            try
            {
                #region------ Manage From/To Date Time and Duration -----------
                try
                {
                    FrmDateTime = txtFrmDate.Text;

                    if (FrmDateTime.Trim() != "")
                    {
                        FrmDateTime = txtFrmDate.Text.Trim() + " 00:00";
                    }
                }

                catch (Exception)
                {
                    FrmDateTime = txtFrmDate.Text.Trim();
                }
                try
                {
                    ToDateTime = txtToDate.Text;

                    if (ToDateTime.Trim() != "")
                    {
                        ToDateTime = txtToDate.Text.Trim() + " 23:59";
                    }

                }


                catch (Exception)
                {
                    ToDateTime = txtToDate.Text.Trim();
                }



                #endregion
                if (RoleId == 1)
                {
                    dt = gsrch.GetFullTAMasterdtlsWithLocation(dtClientId.SelectedValue.ToString(), txtUsername.Text, FrmDateTime, ToDateTime);
                }

                tamaster.DataSource = dt;
                tamaster.DataBind();
                ViewState["dtUser"] = dt;
            }
            catch (Exception)
            {
            }
            finally
            {
                gsrch = null;
            }
        }
        protected void tamaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            tamaster.PageIndex = e.NewPageIndex;
            tamaster.EditIndex = -1;
            BindGrid();
        }

        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = (sender as LinkButton).CommandArgument;
                Response.ContentType = "image";// ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                //Response.WriteFile("~/Files/Android_Files/" + filePath);
                Response.TransmitFile(Server.MapPath("~/Files/Android_Files/" + filePath));
                Response.End();
            }
            catch (Exception)
            { }
        }
    }
}