using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddClient : Base
    {
        ClientBAL client;
        int ClientId, UserId, RoleId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            lblMsg.Text = string.Empty;
            if (RoleId == 2 || RoleId == 3)
            {
                Response.Redirect("AdminDashBoard.aspx");
            }
            else if (RoleId == 4)
            {
                Response.Redirect("userDashBoard.aspx");
            }
            else
            { }
            if (!IsPostBack)
            {
                reset();
                //profileImage.ImageUrl = "~/images/NoPic.png";
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                client = new ClientBAL();
                client.ClientCode = txtClientcode.Text.Trim();
                client.ClientName = txtClientName.Text.Trim();
                client.Address = txtAddress.Text.Trim();
                client.EmailId = txtEmailid.Text.Trim();
                client.ManagerName = txtManagerName.Text.Trim();
                client.ManagerContactNo = txtManagerContactNo.Text.Trim();
                client.LogoFilepath = lblimagepath.Text.Trim();
                client.UserName = txtManagerName.Text.Trim();
                client.MobileNo = txtManagerContactNo.Text.Trim();
                client.EmpCompanyId = txtUserId.Text.Trim();
                client.Password = txtpass.Text.Trim();
                client.NoOfEmployees = Convert.ToInt32(txtempNo.Text.Trim());
                client.TypeOfIndustry = txtcmpytype.Text.Trim();
                client.Designation = txtdsgn.Text.Trim();
                client.CountryId = 1;
                client.IsAgreeTerms = 1;
                client.PartnerId = 1;
                string res = client.InsertClientManager();
                //string res = client.InsertClient();
                //if (int.Parse(res) > 0)
                //{
                //client.ClientId = int.Parse(res);
                //client.currentDateTime = GetCurrentDateTimeByUserId();
                //client.GenerateProductKey(int.Parse(res), 0);
                lblMsg.Text = res;//"Client Details saved successfully.";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                Response.Redirect("ClientMaster.aspx");

                //}
                //else
                //{
                //    lblMsg.Text = "Client Details already exists.";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //    //reset();
                //}
            }
            catch (Exception)
            {
            }
            finally
            {
                client = null;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientMaster.aspx");
        }
        public void reset()
        {
            txtClientcode.Text = "";
            txtClientName.Text = "";
            txtAddress.Text = "";
            txtEmailid.Text = "";
            txtManagerName.Text = "";
            txtManagerContactNo.Text = "";
        }
        protected void btnupload_Click(object sender, EventArgs e)
        {
            string FileName = string.Empty;
            string ServerFilePath = string.Empty;
            if (FileUpload1.HasFile)
            {

                string imagetype = FileUpload1.PostedFile.ContentType;
                if (imagetype == "image/jpeg")
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
                    //profileImage.ImageUrl = ViewState["FileName"].ToString();
                    lblimagepath.Text = ViewState["FileName"].ToString().Substring(1);
                }
                else
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Please Choose Image file Only";

                }
            }

        }
    }
}
