using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class FileUpload : Base
    {
        int ClientId, RoleId, UserId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            //lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                BindAllData();
            }
        }

        protected void btnSaveForm_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txtName.Text))
                //{
                if (FileUpload1.HasFile)
                {
                    string FileName = Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string[] Extensions = Extension.Split('.');
                    decimal Size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                    string TempfileName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + Extension;
                    string FilePath = Server.MapPath("~/" + Constant.FilePath + TempfileName);
                    FileUpload1.SaveAs(FilePath);
                    FileUploadBAL fub = new FileUploadBAL();
                    fub.UserFileName = FileName.ToString();
                    fub.ApplicatioFileName = TempfileName;
                    fub.FileType = Extensions[1];
                    fub.FileSize = Size + "KB".ToString();
                    fub.FilePath = Constant.FilePath + TempfileName;
                    fub.ClientId = ClientId;
                    fub.Status = 0;
                    fub.CreatedBy = UserId.ToString();
                    FileUploadBAL fubs = new FileUploadBAL();
                    int res = fubs.InsertFileUpload(fub);
                    lblMsg.Text = "Uploaded Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    //txtName.Text = "";
                    BindAllData();

                }
                else
                {
                    lblMsg.Text = "Please upload file";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    BindAllData();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void BindAllData()
        {
            AnuSearch fa = new AnuSearch();

            grdAPKs.DataSource = fa.GetFileUploadSearchList(FileName.Text, Type.Text, ClientId);
            grdAPKs.DataBind();
        }
        protected void grdAPKs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAPKs.PageIndex = e.NewPageIndex;
            grdAPKs.EditIndex = -1;
            BindAllData();
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            try
            {
                filePath = "/" + filePath;
                System.IO.FileStream fs = null;
                fs = System.IO.File.Open(Server.MapPath("~" + filePath), System.IO.FileMode.Open);
                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.ContentType = "application/octet-stream";//" ContentType;// "application/apk";
                Response.BinaryWrite(btFile);
                Response.End();               
            }
            catch (Exception ex)
            {
                lblMsg.Text = "There is no file available to download!" + ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void asgnUser_Click(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument;
            int Id = Convert.ToInt32(id);
            Response.Redirect("AssignUser.aspx?Id=" + Id);

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            BindAllData();
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDashBoard.aspx");
        }
    }
}