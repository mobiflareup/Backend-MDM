using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class APKUpload : Base
    {

        UploadBAL apk;
        int ClientId, RoleId, UserId, DeptId;


        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void BindGrid()
        {
            try
            {
                apk = new UploadBAL();
                grdAPK.DataSource = apk.getdata();
                grdAPK.DataBind();
            }
            catch (Exception)
            {

            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    if (FileUpload1.HasFile)
                    {
                        string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                        string FilePath = Server.MapPath("~/" + Constant.APKFolderPath + FileName);
                        FileUpload1.SaveAs(FilePath);
                        apk = new UploadBAL();
                        apk.VersionName = txtName.Text.Trim();
                        apk.UserId = UserId;
                        apk.APKPath = FileName;
                        int res = apk.InsertAPK();
                        lblMsg.Text = "Uploaded Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        BindGrid();
                    }
                    else
                    {
                        lblMsg.Text = "Please Upload APK";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "Please Enter Version Name";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void grdAPK_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAPK.PageIndex = e.NewPageIndex;
            grdAPK.EditIndex = -1;
            BindGrid();
        }


    
}
}