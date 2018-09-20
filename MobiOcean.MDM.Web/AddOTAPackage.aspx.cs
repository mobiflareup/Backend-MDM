using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddOTAPackage : Base
    {
        AppBAL apbal;
        int ClientId, UserId, RoleId, DeptId, OsId;
        DataTable dt;
        GingerboxSrch srch;


        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            OsId = Convert.ToInt32((Request.QueryString["Id"]));
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                // BindDeviceTypeDropdown();
                if (OsId > 0)
                {
                    lblAppName.Text = "Edit";
                    BindGrid();
                    edvis.Visible = false;
                }
                else
                {
                    lblAppName.Text = "Add";
                }

            }

        }
        public void BindGrid()
        {

            dt = new DataTable();

            try
            {
                srch = new GingerboxSrch();
                dt = srch.SrchOTGPackageData(OsId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtPackageName.Text = dt.Rows[0]["AppPackage"].ToString();
                    txtVersionNo.Text = dt.Rows[0]["AppVersionNo"].ToString();
                    //dtAllowDownload.SelectedValue = dt.Rows[0]["AllowDownload"].ToString();
                    //dtMandatoryUpdate.SelectedValue = dt.Rows[0]["ApplyDeviceType"].ToString();
                    //dtAllowDownload.SelectedValue = dt.Rows[0]["AllowDownload"].ToString();
                    txtVersion.Text = dt.Rows[0]["AppVersion"].ToString();
                    txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                }
            }
            catch (Exception)
            {
            }
        }
        //private void BindDeviceTypeDropdown()
        //{
        //    apbal = new AppBAL();
        //    try
        //    {
        //        ListItem li1 = new ListItem("Apply All Device Type", "0");
        //        //dtDeviceType.Items.Clear();
        //        //dtDeviceType.Items.Add(li1);
        //        //dtDeviceType.DataSource = apbal.GetDeviceType();
        //        //dtDeviceType.DataTextField = "DeviceType";
        //        //dtDeviceType.DataValueField = "DeviceTypeId";
        //        //dtDeviceType.DataBind();
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    finally
        //    {
        //        apbal = null;
        //    }
        //}  
        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            if (OsId != 0)
            {
                InsertData("");
            }
            else
            {
                if (OsUpload.HasFile)
                {
                    InsertData(img());
                } //    }
                else
                {
                    lblMsg.Text = "Please Upload OS File";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void InsertData(string FileName)
        {
            apbal = new AppBAL();
            apbal.OsId = OsId;
            apbal.AppPackage = txtPackageName.Text;
            apbal.UserId = UserId;
            apbal.AppVersion = txtVersion.Text;
            apbal.AppVersionNo = txtVersionNo.Text;
            apbal.Os_path = FileName;
            //apbal.MandatoryUpdate = Convert.ToBoolean(Convert.ToInt16(null));
            //apbal.AllowDownload = Convert.ToBoolean(Convert.ToInt16(null));
            apbal.Remark = txtRemark.Text;
            apbal.CreationDate = DateTime.UtcNow.AddMinutes(330);
            apbal.ClientId = ClientId;
            if (apbal.InsertOTAPackage() > 0)
            {
                //lblMsg.Text = "Successfully save";
                //lblMsg.ForeColor = System.Drawing.Color.Green;
                mpsuccess.Show();
                lblSuccess.Text = "Saved Sucessfully";
                lblSuccess.ForeColor = System.Drawing.Color.White;

                //Reset();
            }
            else
            {
                lblMsg.Text = "Something went wrong";
                lblMsg.ForeColor = System.Drawing.Color.Red;

            }
        }

        public string img()
        {

            string FileName = "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + Path.GetFileName(OsUpload.PostedFile.FileName);
            FileName = FileName.Replace(" ", "_");
            string Extension = Path.GetExtension(OsUpload.PostedFile.FileName);
            //if (Extension == ".asec" || Extension == ".ASEC")
            //{
            string FilePath = Server.MapPath("~/" + Constant.PublicAPKFolderPath + "/Os/" + FileName);
            OsUpload.SaveAs(FilePath);
            //}
            return FileName;

        }

        protected void btnsucok_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ApplicationManageOTG.aspx?sucess=1", false);
        }
        private void Reset()
        {

            txtPackageName.Text = string.Empty;
            txtVersion.Text = string.Empty;
            txtRemark.Text = string.Empty;
            txtVersionNo.Text = string.Empty;

            //dtMandatoryUpdate.SelectedIndex = 0;
            //dtAllowDownload.SelectedIndex = 0;
            //dtDeviceType.SelectedIndex = 0;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/ApplicationManageOTG.aspx", false);

        }
    }
}