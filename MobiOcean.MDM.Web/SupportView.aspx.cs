using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class SupportView : Base
    {
        int ClientId, UserId, RoleId, SupportId;
        DataTable dt;
        SupportBAL support;        
        StringBuilder msg = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            SupportId = Convert.ToInt32(Request.QueryString["Id"]);
            if (!IsPostBack)
            {
                BindOldData();
            }
        }
        protected void BindOldData()
        {
            try
            {
                dt = new DataTable();
                support = new SupportBAL();
                support.SupportId = SupportId;
                dt = support.GetSupportDtlsBySupportId();
                ViewState["Dt"] = dt;
                UtxtUserName.Text = dt.Rows[0]["UserName"].ToString();
                UtxtDefectName.Text = dt.Rows[0]["DefectName"].ToString();
                UtxtErrorURL.Text = dt.Rows[0]["ErrorURL"].ToString();
                txtDefectDesc.Text = dt.Rows[0]["DefectDesc"].ToString();
                UtxtReqStatus.Text = (dt.Rows[0]["RequestStatus"].ToString()) == "0" ? "Open" : "Closed";
                txtResponse.Text = dt.Rows[0]["Response"].ToString();
                if (string.IsNullOrEmpty(dt.Rows[0]["DocPath"].ToString()))
                {
                    profileImage.ImageUrl = "~/images/Nopic.png";
                }
                else
                {
                    profileImage.ImageUrl = "~" + dt.Rows[0]["DocPath"].ToString();
                    lblimagepath.Text = dt.Rows[0]["DocPath"].ToString();
                }
            }
            catch (Exception) { }
            finally
            {
                dt = null;
                support = null;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dt = new DataTable();
                support = new SupportBAL();
                support.SupportId = SupportId;
                dt = support.GetSupportDtlsBySupportId();
                int UserId = Convert.ToInt32(dt.Rows[0][2].ToString());
                string MobileNo = dt.Rows[0][4].ToString();
                string EmailId = dt.Rows[0][5].ToString();
                //string DefectDesc = dt.Rows[0][17].ToString();
                string DefectType = dt.Rows[0][18].ToString();
                support.UserId = UserId;
                support.MobileNo = MobileNo;
                support.EmailId = EmailId;
                support.DefectType = DefectType;
                support.UserName = UtxtUserName.Text;
                support.DefectName = UtxtDefectName.Text;
                support.ErrorURL = UtxtErrorURL.Text;
                support.Response = txtResponse.Text;
                support.DefectDesc = txtDefectDesc.Text;
                support.RequestStatus = Convert.ToInt32(ddlReqStatus.SelectedValue.ToString());
                support.DocPath = lblimagepath.Text;
                if (RoleId == 1)
                {
                    support.ResponseDate = (GetCurrentDateTimeByUserId()).ToString();
                }
                int res = support.Insert_Support();
                if (res > 0)
                {
                    lblMsg.Text = "Updated successfully.";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
                }
            }
            catch (Exception)
            {
                lblMsg.Text = "Something went wrong!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",5000)</script>");
            }
            UtxtUserName.ReadOnly = true;
            UtxtDefectName.ReadOnly = true;
            UtxtErrorURL.ReadOnly = true;
            btnUpdate.Visible = true;
            BtnSave.Visible = false;
            UtxtReqStatus.ReadOnly = true;
            UtxtReqStatus.Visible = true;
            txtDefectDesc.ReadOnly = true;
            txtResponse.ReadOnly = true;
            ddlReqStatus.Visible = false;
            btnupload.Visible = false;
            FileUpload1.Visible = false;
        }
        public void MailBody()
        {
            msg.Append("Dear Sir/Madam");
            msg.AppendLine(); msg.AppendLine();
            msg.Append("<b>The below table has the details for Support request</b>");
            msg.AppendLine(); msg.AppendLine();


            msg.Append("Defect Name : " + UtxtDefectName.Text + "<br/>");
            msg.Append("Defect Description : " + txtDefectDesc.Text + "<br/>");
            msg.Append("Response : " + txtResponse.Text + "<br/>");
            msg.Append("Request Status : " + UtxtReqStatus.Text);

            msg.AppendLine();
            msg.AppendLine();

            msg.Append("Have a nice day :)");
            msg.AppendLine(); msg.AppendLine(); msg.AppendLine();

            msg.Append("Regards");
            msg.AppendLine();
            msg.Append("MobiOcean Team");




        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ModeChange(false, true);
        }
        private void ModeChange(bool a, bool b)
        {
            UtxtUserName.ReadOnly = b;
            btnUpdate.Visible = a;
            BtnSave.Visible = b;
            if (RoleId != 1)
            {
                UtxtDefectName.ReadOnly = a;
                UtxtErrorURL.ReadOnly = a;
                txtDefectDesc.ReadOnly = a;
                txtResponse.ReadOnly = b;
                FileUpload1.Visible = b;
                btnupload.Visible = b;
            }
            else
            {
                UtxtReqStatus.ReadOnly = a;
                UtxtReqStatus.Visible = a;
                txtDefectDesc.ReadOnly = b;
                txtResponse.ReadOnly = a;
                ddlReqStatus.Visible = b;
                BtnSave.Visible = b;

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupportMaster.aspx");
        }
        protected void btnupload_Click(object sender, EventArgs e)
        {
            string FileName = string.Empty;
            string ServerFilePath = string.Empty;
            if (FileUpload1.HasFile)
            {
                string imagetype = FileUpload1.PostedFile.ContentType;
                if (imagetype == "image/jpeg" || imagetype == "image/jpg" || imagetype == "image/png")
                {
                    FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string ServerFolder = Server.MapPath("~/Files/Client/");
                    if (!System.IO.Directory.Exists(ServerFolder))
                    {
                        System.IO.Directory.CreateDirectory(ServerFolder);
                    }
                    ServerFilePath = ServerFolder + FileName;
                    if (FileName != string.Empty)
                    {
                        ViewState["FileName"] = "~/Files/Client/" + FileName;
                    }
                    FileUpload1.SaveAs(ServerFilePath);
                    profileImage.ImageUrl = ViewState["FileName"].ToString();
                    lblimagepath.Text = ViewState["FileName"].ToString().Substring(1);
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Please choose image(jpeg/jpg/png) file only!";
                }
            }
            else
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please select file!";
            }
        }
    }
}