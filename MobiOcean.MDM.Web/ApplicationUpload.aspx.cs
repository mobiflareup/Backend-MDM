using Iteedee.ApkReader;
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
    public partial class ApplicationUpload : Base
    {
        AppBAL apbal;
        GingerboxSrch srch;
        DataTable dt;
        int ClientId, RoleId, UserId, DeptId, AppMarketId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            AppMarketId = Convert.ToInt32((Request.QueryString["Id"]));
            if (!IsPostBack)
            {

                if (AppMarketId > 0)
                {
                    lblAppName.Text = "Edit";
                    DivImageUpload.Visible = false;
                    DivApkUpload.Visible = false;
                    BindGrid();
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
                dt = srch.SrchApplicationMarketData(AppMarketId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtAppName.Text = dt.Rows[0]["AppName"].ToString();                   
                    AutoPushCheck.Checked = (bool)dt.Rows[0]["AutoPush"];
                    txtDeveloper.Text = dt.Rows[0]["Developer"].ToString();
                    app_cate_id.SelectedValue = dt.Rows[0]["AppType"].ToString();
                    txtIntroduce.Text = dt.Rows[0]["AppIntroduce"].ToString();
                    txtAppPackage.Text = dt.Rows[0]["AppPackage"].ToString();
                    txtAppVersion.Text = dt.Rows[0]["AppVersion"].ToString();
                    txtAppSize.Text = dt.Rows[0]["AppSize"].ToString();
                    txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                }
            }
            catch (Exception)
            {
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            apbal = new AppBAL();
            if (AppMarketId == 0)
            {
                if (!string.IsNullOrEmpty(FileNameHidden.Value))
                {
                    //if (APKUpload.HasFile)
                    //{
                    string FileName = ""; //Path.GetFileName(APKUpload.PostedFile.FileName);
                                          //    FileName = FileName.Replace(" ", "_");
                                          //    string Extension = Path.GetExtension(APKUpload.PostedFile.FileName);
                                          //if (Extension == ".APK" || Extension == ".apk")
                                          //{
                                          //    string FilePath = Server.MapPath("~/" + Constant.PublicAPKFolderPath + FileName);
                                          //    /*Read Apk */

                    //    /*End Here */
                    //    APKUpload.SaveAs(FilePath);
                    if (ImageUpload.HasFile)
                    {
                        string FileName1 = "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + Path.GetFileName(ImageUpload.PostedFile.FileName);
                        FileName1 = FileName1.Replace(" ", "_");
                        string Extension1 = Path.GetExtension(ImageUpload.PostedFile.FileName);
                        if (Extension1 == ".jpg" || Extension1 == ".JPG" || Extension1 == ".jpeg" || Extension1 == ".JPEG" || Extension1 == ".gif" || Extension1 == ".GIF" || Extension1 == ".png" || Extension1 == ".PNG")
                        {
                            if (apbal.AppVersionExists() != null)
                            {
                                string FilePath1 = Server.MapPath("~/" + Constant.PublicAPKFolderPath + "/Images/" + FileName1);
                                ImageUpload.SaveAs(FilePath1);
                                FileName = FileNameHidden.Value;
                                NewMethod(FileName, FileName1);
                                Reset();
                            }
                            else
                            {
                                lblversion.Text = "App Version Already Exists";
                                lblversion.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Only image formats (jpg, png, gif) are accepted ";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                        }
                        //}
                        //else
                        //{
                        //    lblMsg.Text = "Plz Upload Image";
                        //    lblMsg.ForeColor = System.Drawing.Color.Red;
                        //}
                    }
                    else
                    {
                        lblMsg.Text = "Only Android APK formats (.apk) are accepted";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "Please Upload APK File";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                NewMethod("", "");
            }
        }

        private void NewMethod(string FileName, string FileName1)
        {
            apbal.AppMarketId = AppMarketId;
            apbal.AppName = txtAppName.Text.Trim();
            //apbal.UseStatus = UseStatusEnable.Checked == true ? true : false;
            //apbal.ChargeStatus = ChargeStatusFree.Checked == true ? true : false;
            //apbal.AppPrice = txtAppPrice.Text == null ? 0 : Convert.ToInt32(txtAppPrice.Text);
            apbal.ApkPath = FileName.Trim();
            apbal.AutoPush = AutoPushCheck.Checked == true;
            apbal.ImagesPath = FileName1;
            apbal.Developer = txtDeveloper.Text;
            apbal.AppType = app_cate_id.SelectedValue.ToString();
            apbal.AppIntroduce = txtIntroduce.Text.Trim();
            apbal.AppPackage = txtAppPackage.Text.Trim();
            apbal.AppVersion = txtAppVersion.Text.Trim();
            apbal.AppSize = txtAppSize.Text.Trim();
            apbal.Remark = txtRemark.Text.Trim();
            apbal.UserId = UserId;
            apbal.CreationDate = DateTime.UtcNow.AddMinutes(330);
            apbal.ClientId = ClientId;
            apbal.InsertAppMarket();
            lblMsg.Text = "Successfully Updated";
            lblMsg.ForeColor = System.Drawing.Color.Green;
            Response.Redirect("ApplicationMarketList.aspx");
        }

        private void Reset()
        {
            txtAppName.Text = string.Empty;
            //UseStatusEnable.Checked = true;
            //ChargeStatusFree.Checked = true;
            //txtAppPrice.Text = string.Empty;
            AutoPushCheck.Checked = false;
            txtDeveloper.Text = string.Empty;
            app_cate_id.SelectedIndex = 0;
            txtIntroduce.Text = string.Empty;
            txtAppPackage.Text = string.Empty;
            txtAppVersion.Text = string.Empty;
            txtAppSize.Text = string.Empty;
            txtRemark.Text = string.Empty;
            lblversion.Text = "";
        }

        protected void txtAppVersion_TextChanged(object sender, EventArgs e)
        {
            apbal = new AppBAL();
            apbal.AppPackage = txtAppPackage.Text;
            apbal.AppVersion = txtAppVersion.Text;
            if (apbal.AppVersionExists() == null)
            {

                lblversion.Text = "App Version Already Exists";
                lblversion.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblversion.Text = "";
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ApplicationMarketList.aspx", false);
        }

        protected void StartUpload_Click(object sender, EventArgs e)
        {
            string FileName = "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + Path.GetFileName(APKUpload.PostedFile.FileName);
            FileName = FileName.Replace(" ", "_");
            FileNameHidden.Value = FileName;
            string Extension = Path.GetExtension(APKUpload.PostedFile.FileName);
            if (Extension == ".APK" || Extension == ".apk")
            {
                string FilePath = Server.MapPath("~/" + Constant.PublicAPKFolderPath + "/APK/" + FileName);
                APKUpload.SaveAs(FilePath);
                /*Read Apk */
                byte[] manifestData = null;
                byte[] resourcesData = null;
                using (ICSharpCode.SharpZipLib.Zip.ZipInputStream zip = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(File.OpenRead(FilePath)))
                {
                    using (var filestream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                    {
                        ICSharpCode.SharpZipLib.Zip.ZipFile zipfile = new ICSharpCode.SharpZipLib.Zip.ZipFile(filestream);
                        ICSharpCode.SharpZipLib.Zip.ZipEntry item;
                        try
                        {
                            while ((item = zip.GetNextEntry()) != null)
                            {
                                if (item.Name.ToLower() == "androidmanifest.xml")
                                {
                                    manifestData = new byte[50 * 1024];
                                    using (Stream strm = zipfile.GetInputStream(item))
                                    {
                                        strm.Read(manifestData, 0, manifestData.Length);

                                    }

                                }
                                if (item.Name.ToLower() == "resources.arsc")
                                {
                                    using (Stream strm = zipfile.GetInputStream(item))
                                    {
                                        using (BinaryReader s = new BinaryReader(strm))
                                        {
                                            resourcesData = s.ReadBytes((int)item.Size);

                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                ApkReader apkReader = new ApkReader();
                ApkInfo info = apkReader.extractInfo(manifestData, resourcesData);
                txtAppVersion.Text = info.versionName.ToString();
                txtAppPackage.Text = info.packageName.ToString();
                txtAppName.Text = info.label.ToString();
                decimal Size = Math.Round(((decimal)APKUpload.PostedFile.ContentLength / (decimal)1024), 2);
                txtAppSize.Text = Size.ToString();
                /*End Here */

            }
            else
            {
                lblMsg.Text = "Only Android APK formats (.apk) are accepted";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}