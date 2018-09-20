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
    public partial class RemarkMobileSIM : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        GingerboxSrch srch;
        usrBAL user;        
        UserBAL userBAL;
        DataTable dtUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            txtFrmDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                txtFrmDate.Text = txtToDate.Text = GetCurrentDateTimeByUserId().ToString("dd MMM yyyy");
                if (RoleId == 1)
                {
                    BindClientDDL();
                }
                else
                {
                    divclientddl.Visible = false;
                }
                BindGrid();
            }
        }

        public void BindGrid()
        {
            dtUser = new DataTable();
            userBAL = new UserBAL();
            string MobileStatus = "";
            //string StartTime, EndTime;
            try
            {
                srch = new GingerboxSrch();
                if (RoleId == 1)
                {
                    dtUser = srch.SrchMobileStatusInfo(dtClientId.SelectedValue.ToString(), txtSrchUserName.Text, txtSrchEmailId.Text, txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), 10);
                }
                else
                {
                    dtUser = srch.SrchMobileStatusInfo(ClientId.ToString(), txtSrchUserName.Text, txtSrchEmailId.Text, txtFrmDate.Text.Trim(), txtToDate.Text.Trim(), 16);
                }
                dtUser.Columns.Add("Mobile Status", typeof(System.String));
                foreach (DataRow dr in dtUser.Rows)
                {
                    MobileStatus = userBAL.ChkSimChange(dr["Remarks"].ToString(), Convert.ToInt32(dr["DeviceId"]));
                    dr["Mobile Status"] = MobileStatus;
                }
                grdUsr.DataSource = dtUser;
                //ViewState["dtUser"] = dtUser;
                grdUsr.DataBind();
            }
            catch (Exception)
            {
            }
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            BindGrid();
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
        protected void grdUsr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUsr.PageIndex = e.NewPageIndex;
            BindGrid();

        }

        protected void dtClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}