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
    public partial class ClientProfile : Base
    {
        int ClientId, UserId, RoleId, DeptId;
        DataTable dt;
        ClientBAL clBal;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                btnEdit.Visible = false;
                BindOldData();
            }
            if (RoleId == 1 || RoleId == 2)
            {
                btnEdit.Visible = true;
            }
        }
        protected void BindOldData()
        {
            try
            {
                dt = new DataTable();
                clBal = new ClientBAL();
                clBal.ClientId = ClientId;
                dt = clBal.GetClientByClientId();
                ViewState["Dt"] = dt;
                UtxtClientcode.Text = dt.Rows[0]["ClientCode"].ToString();
                UtxtClientName.Text = dt.Rows[0]["ClientName"].ToString();
                UtxtFirst.Text = dt.Rows[0]["FirstName"].ToString();
                UtxtLastN.Text = dt.Rows[0]["LastName"].ToString();
                UtxtDesg.Text = dt.Rows[0]["Designation"].ToString();
                UtxtNOE.Text = dt.Rows[0]["EmployeeCount"].ToString();
                UtxtTOIn.Text = dt.Rows[0]["TypeOfClient"].ToString();
                UtxtUId.Text = dt.Rows[0]["UserId"].ToString();
                UtxtEmailid.Text = dt.Rows[0]["EmailId"].ToString();
                UtxtManagerName.Text = dt.Rows[0]["ManagerName"].ToString();
                UtxtManagerContactNo.Text = dt.Rows[0]["ManagerContactNo"].ToString();
                UtxtAddress.Text = dt.Rows[0]["Address"].ToString();
            }
            catch (Exception) { }
            finally
            {
                dt = null;
                clBal = null;
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;
            btncancle.Visible = true;
            btnEdit.Visible = false;
            UtxtFirst.Enabled = true;
            UtxtLastN.Enabled = true;
            UtxtDesg.Enabled = true;
            UtxtNOE.Enabled = true;
            UtxtTOIn.Enabled = true;
            UtxtUId.Enabled = true;
            UtxtEmailid.Enabled = true;
            UtxtManagerContactNo.Enabled = true;
            UtxtAddress.Enabled = true;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            clBal = new ClientBAL();
            try
            {
                clBal.ClientId = ClientId;
                clBal.FirstN = UtxtFirst.Text;
                clBal.LastN = UtxtLastN.Text;
                clBal.Designation = UtxtDesg.Text;
                clBal.NoOfEmployees = Convert.ToInt32(UtxtNOE.Text);
                clBal.TypeOfIndustry = UtxtTOIn.Text;
                clBal.EmpId = UtxtUId.Text;
                clBal.EmailId = UtxtEmailid.Text;
                clBal.MobileNo = UtxtManagerContactNo.Text;
                clBal.Address = UtxtAddress.Text;
                int res = clBal.UpdateClientProfile();
                if (res > 0)
                {
                    lblMsg.Text = "Profile Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    SetEnablefalse();
                    BindOldData();
                }
                else if (res == -1)

                {
                    lblMsg.Text = "Email Id alrady Exist!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    btnEdit.Visible = false;
                }
                else
                {
                    lblMsg.Text = "Profile Not Updated Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    btnEdit.Visible = false;
                }
            }
            catch
            {
                lblMsg.Text = "Something wrong! ";
                btnEdit.Visible = false;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }

        private void SetEnablefalse()
        {
            btnUpdate.Visible = false;
            btncancle.Visible = false;
            btnEdit.Visible = true;
            UtxtFirst.Enabled = false;
            UtxtLastN.Enabled = false;
            UtxtDesg.Enabled = false;
            UtxtNOE.Enabled = false;
            UtxtTOIn.Enabled = false;
            UtxtUId.Enabled = false;
            UtxtEmailid.Enabled = false;
            UtxtManagerContactNo.Enabled = false;
            UtxtAddress.Enabled = false;
        }
        protected void btncancle_Click(object sender, EventArgs e)
        {
            SetEnablefalse();
            BindOldData();
        }
        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    ModeChange(false, true);
        //}
        //private void ModeChange(bool a, bool b)
        //{
        //    UtxtEmailid.ReadOnly = a;
        //    UtxtManagerName.ReadOnly = a;
        //    UtxtManagerContactNo.ReadOnly = a;
        //    UtxtAddress.ReadOnly = a;
        //    //FileUpload1.Visible = b;
        //    //btnupload.Visible = b;
        //    //BtnSave.Visible = b;
        //    //btnUpdate.Visible = a;
        //}
        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    if (RoleId == 1)
        //    {
        //        Response.Redirect("ClientMaster.aspx");
        //    }
        //    else if (RoleId == 2 || RoleId == 3)
        //    {
        //        Response.Redirect("AdminDashBoard.aspx");
        //    }
        //    else
        //    {
        //        Response.Redirect("Userdashboard.aspx");
        //    }
        //}
        //protected void btnupload_Click(object sender, EventArgs e)
        //{
        //    string FileName = string.Empty;
        //    string ServerFilePath = string.Empty;
        //    //if (FileUpload1.HasFile)
        //    //{
        //    //    string imagetype = FileUpload1.PostedFile.ContentType;
        //    //    if (imagetype == "image/jpeg" || imagetype == "image/jpg" || imagetype == "image/png")
        //    //    {
        //    //        FileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
        //    //        string ServerFolder = Server.MapPath("~/Files/Client/");
        //    //        if (!System.IO.Directory.Exists(ServerFolder))
        //    //        {
        //    //            System.IO.Directory.CreateDirectory(ServerFolder);
        //    //        }
        //    //        ServerFilePath = ServerFolder + FileName;
        //    //        if (FileName != string.Empty)
        //    //        {
        //    //            ViewState["FileName"] = "~/Files/Client/" + FileName;
        //    //        }
        //    //        FileUpload1.SaveAs(ServerFilePath);
        //    //        profileImage.ImageUrl = ViewState["FileName"].ToString();
        //    //        lblimagepath.Text = ViewState["FileName"].ToString().Substring(1);
        //    //    }
        //    //    else
        //    //    {
        //    //        lblMsg.ForeColor = System.Drawing.Color.Red;
        //    //        lblMsg.Text = "Please Choose Image(Jpeg/jpg/png) file Only";
        //    //    }
        //    //}
        //}
        //protected void BtnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        clBal = new ClientBAL();
        //        clBal.ClientId = ClientId;
        //        clBal.ClientCode = UtxtClientcode.Text;
        //        clBal.ClientName = UtxtClientName.Text;
        //        clBal.EmailId = UtxtEmailid.Text;
        //        clBal.ManagerName = UtxtManagerName.Text;
        //        clBal.ManagerContactNo = UtxtManagerContactNo.Text;
        //        clBal.Address = UtxtAddress.Text;
        //        //clBal.LogoFilepath = lblimagepath.Text;
        //        string res = clBal.InsertClient();
        //        if (res == "1")
        //        {
        //            lblMsg.Text = "Client Details Updated successfully.";
        //            lblMsg.ForeColor = System.Drawing.Color.Green;
        //            ModeChange(true, false);
        //        }
        //        else
        //        {
        //            lblMsg.Text = "Client Details already exists.";
        //            lblMsg.ForeColor = System.Drawing.Color.Red;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    finally
        //    {
        //        clBal = null;
        //    }
        //}
    }
}