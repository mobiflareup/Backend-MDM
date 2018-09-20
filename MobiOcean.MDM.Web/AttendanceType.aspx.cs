using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AttendanceType : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        DDLBAL ddlbal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientID"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindUser();
                lblMsg.Text = "";
            }
        }
        protected void btnSrch_Click(object sender, EventArgs e)
        {
            ddlbal = new DDLBAL();
            ddlbal.UserName = txtEmpName.Text;
            BindUser();
        }
        protected void CameraEnbl_header_CheckedChanged(object sender, EventArgs e)
        {
            ddlbal = new DDLBAL();
            ddlbal.ClientId = ClientId;
            ddlbal.UserId = UserId;
            ddlbal.DeptId = DeptId;
            ddlbal.RoleId = RoleId;
            ddlbal.UserName = txtEmpName.Text;
            int check = 0;
            CheckBox chk_hdr = (CheckBox)grdUser.HeaderRow.FindControl("CameraEnbl_header");
            DataTable dt = new DataTable();
            dt = ddlbal.Custom_GetUser();

            for (int idx = 0; idx < dt.Rows.Count; idx++)
            {
                int UserIdTo = Convert.ToInt32(dt.Rows[idx]["UserId"]);
                string AttendanceType = dt.Rows[idx]["AttendanceTypeId"].ToString();
                string Att = "";
                if (chk_hdr.Checked == true)
                {
                    if (AttendanceType.Contains("7"))
                    {
                        Att = AttendanceType;
                    }
                    else
                    {
                        Att += !string.IsNullOrWhiteSpace(AttendanceType) ? AttendanceType + ",7" : "7";
                    }
                }
                else
                {
                    Att = AttendanceType.Replace(",7", "").Replace("7,", "").Replace("7", null);
                    Att = Att == "" ? null : Att;
                }
                bool res = ddlbal.Up_AttendType(UserIdTo, Att, Convert.ToInt32(Session["UserId"]));
            }
            BindUser();
            if (check == 0)
            {
                lblMsg.Text = "SucessFully Updated";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMsg.Text = "Update Failed" + check + " Times ";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void CameraEnbl_CheckedChanged(object sender, EventArgs e)
        {

            ddlbal = new DDLBAL();

            int idx = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)grdUser.Rows[idx].FindControl("CameraEnbl");
            Label UserIdTo = (Label)grdUser.Rows[idx].FindControl("lblId");

            Label AttendanceType = (Label)grdUser.Rows[idx].FindControl("AttendanceType");
            string Att = "";
            if (chk.Checked == true)
            {
                if (AttendanceType.Text.Contains("7"))
                {
                    Att = AttendanceType.Text;
                }
                else
                {
                    Att += !string.IsNullOrWhiteSpace(AttendanceType.Text) ? AttendanceType.Text + ",7" : "7";
                }
            }
            else
            {
                Att = AttendanceType.Text.Replace(",7", "").Replace("7,", "").Replace("7", null);
                Att = Att == "" ? null : Att;
            }

            bool res = ddlbal.Up_AttendType(Convert.ToInt32(UserIdTo.Text), Att, Convert.ToInt32(Session["UserId"]));
            BindUser();
            lblMsg.Text = res ? "SucessFully Updated" : "Update Failed ";
            lblMsg.ForeColor = res ? System.Drawing.Color.Green : System.Drawing.Color.Red;

        }
        protected void grdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdUser.PageIndex = e.NewPageIndex;
            BindUser();
            lblMsg.Text = "";

        }

        protected void BindUser()
        {
            try
            {
                ddlbal = new DDLBAL();
                ddlbal.ClientId = ClientId;
                ddlbal.UserId = UserId;
                ddlbal.DeptId = DeptId;
                ddlbal.RoleId = RoleId;
                ddlbal.UserName = txtEmpName.Text;
                DataTable dt = new DataTable();
                dt = ddlbal.Custom_GetUser();
                int count = ddlbal.Custom_GetUser_For_CheckBox();
                grdUser.DataSource = dt;
                grdUser.DataBind();
                //int cou = 0;
                CheckBox chk_hdr = (CheckBox)grdUser.HeaderRow.FindControl("CameraEnbl_header");
                //Working Perfect
                #region
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string[] s = dt.Rows[i]["AttendanceTypeId"].ToString().Split(',');
                //    if (s.Contains("7"))
                //    {
                //        cou += 1;
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}
                chk_hdr.Checked = dt.Rows.Count == count ? true : false;
                #endregion
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }
    }
}