using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class SupportForm : Base
    {
        SupportBAL support;
        UserBAL userBAL;
        DataTable dt;
        int ClientId, UserId, RoleId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            lblMsg.Text = string.Empty;
            if (!IsPostBack)
            {
                reset();
            }
        }
        protected void reset()
        {
            txtDefectName.Text = string.Empty;
            txtErrorScreen.Text = string.Empty;
            txtDefectDesc.Text = string.Empty;
            txtDefectType.Text = string.Empty;
        }
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = string.Empty;
                lblMsg.Text = string.Empty;
                string ServerFilePath = string.Empty;
                if (FileUpload1.HasFile)
                {
                    string imagetype = FileUpload1.PostedFile.ContentType;
                    if (imagetype == "image/jpeg")
                    {
                        //FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                        string FileExtension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                        FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + FileExtension;
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
                        lblMsg.Text = "Please choose image file only";
                    }
                    
                }
                if (string.IsNullOrEmpty(lblMsg.Text.Trim()))
                {
                    
                   
                    userBAL = new UserBAL();
                    dt = new DataTable();
                    int UserId = Convert.ToInt32(Session["UserId"].ToString());
                    userBAL.UserId=UserId;
                    dt = userBAL.GetUserDtlByUserId();

                    support = new SupportBAL();
                    support.SupportId = 0;
                    support.UserId = UserId;
                    support.ClientId = ClientId;
                    support.UserName = dt.Rows[0]["UserName"].ToString(); ;
                    support.MobileNo = dt.Rows[0]["MobileNo"].ToString(); ;
                    support.EmailId = dt.Rows[0]["EmailId"].ToString(); ;
                    support.DefectName = txtDefectName.Text.Trim();
                    support.ErrorURL = txtErrorScreen.Text.Trim();
                    support.DefectDesc = txtDefectDesc.Text.Trim();
                    support.DefectType = txtDefectType.Text.Trim();
                    support.DocPath = lblimagepath.Text;
                    int res = support.Insert_Support();
                    if (res > 0)
                    {
                        lblMsg.Text = "Saved successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        reset();
                    }
                    else
                    {
                        lblMsg.Text = "Something went wrong!";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.ToString();
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            reset();
        }
        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("SupportMaster.aspx");
        }
    }
}
