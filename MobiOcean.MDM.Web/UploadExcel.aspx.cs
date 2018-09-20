using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class UploadExcel : Base
    {
        static string connstr = "";
        DataTable correctdt, IncorrectDt;
        int ClientId, UserId, RoleId, DeptId;

        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            lblmsg.Text = string.Empty;
            if (!IsPostBack)
            {
                connstr = "";
                btnSave.Enabled = false;
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);


                    string FilePath = Server.MapPath(Constant.FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension, "Yes");
                    btnSave.Enabled = true;
                }
            }
            catch (Exception)
            {
                lblmsg.Text = "Please Change the file name.This file is already exist.";
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                connstr = conStr;
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                ddlSheet.DataSource = dtExcelSchema;
                ddlSheet.DataTextField = "TABLE_NAME";
                ddlSheet.DataValueField = "TABLE_NAME";
                ddlSheet.DataBind();
            }
            catch (Exception)
            {
                lblmsg.Text = "Something went wrong. Please try again with different file name.";
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            OleDbConnection connExcel = new OleDbConnection(connstr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            string SheetName = ddlSheet.SelectedValue.ToString();
            //connExcel.Close();

            //Read Data from First Sheet
            // connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                chkexcel(dt);
            }
            else
            {
                lblmsg.Text = "Sheet has no data";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            connExcel.Close();

            //Bind Data to GridView
            //GridView1.Caption = Path.GetFileName(FilePath);
            //chkexcel(dt);
        }
        protected void chkexcel(DataTable dt)
        {
            try
            {
                correctdt = new DataTable();
                correctdt.Columns.Add("EmpCompanyId");
                correctdt.Columns.Add("UserName");
                correctdt.Columns.Add("DeviceName");
                correctdt.Columns.Add("EmailId");
                correctdt.Columns.Add("MobileNo");
                correctdt.Columns.Add("Gender");
                correctdt.Columns.Add("Address");
                IncorrectDt = new DataTable();
                IncorrectDt.Columns.Add("EmpCompanyId");
                IncorrectDt.Columns.Add("UserName");
                IncorrectDt.Columns.Add("DeviceName");
                IncorrectDt.Columns.Add("Email");
                IncorrectDt.Columns.Add("MobileNo");
                IncorrectDt.Columns.Add("Gender");
                IncorrectDt.Columns.Add("Address");
                DataRow[] rows = dt.Select(); //"Mobile_No='" + lblmobileno.Text.Trim() + "'");
                foreach (DataRow row in rows)
                {
                    if (!string.IsNullOrEmpty(row["Email Id"].ToString()) && !string.IsNullOrEmpty(row["Mobile No"].ToString()))
                    {
                        try
                        {
                            correctdt.Rows.Add(row["Emp Company Id"], row["User Name"], row["Device Name"], row["Email Id"], row["Mobile No"],
                                 row["Gender"], row["Address"]);
                        }
                        catch (Exception)
                        {
                            IncorrectDt.Rows.Add(row["Emp Company Id"], row["User Name"], row["Device Name"], row["Email Id"], row["Mobile No"]
                                , row["Gender"], row["Address"]);
                        }
                    }
                    else
                    {
                        IncorrectDt.Rows.Add(row["Emp Company Id"], row["User Name"], row["Device Name"], row["Email Id"], row["Mobile No"]
                                 , row["Gender"], row["Address"]);
                    }
                }
                try
                {

                    //upload = new UploadBAL();
                    //upload.ExcelFile = correctdt;
                    //upload.ClientId = ClientId;
                    //upload.CheckUploadExcelDetails();              
                    if (File.Exists(Server.MapPath(Constant.FolderPath)))
                    {
                        Array.ForEach(Directory.GetFiles(Server.MapPath(Constant.FolderPath)), File.Delete);
                    }
                    connstr = string.Empty;
                    btnSave.Enabled = false;
                    ddlSheet.Items.Clear();
                    if (IncorrectDt.Rows.Count > 0)
                    {
                        btnSave.Enabled = false;
                        lblmsg.Text = "Some Of rows not updated.Please check the data and try again";
                        lblmsg.Visible = true;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        GridView1.DataSource = IncorrectDt;
                        GridView1.DataBind();
                        GridView1.Visible = false;
                    }
                    else
                    {
                        btnSave.Enabled = false;
                        lblmsg.Text = "Excel uploaded Successfully";
                        lblmsg.Visible = true;
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                    }
                }
                finally
                {

                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;// "Please upload excel sheet in correct format";
                lblmsg.Visible = true;
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                dt = null;
                correctdt = null;
                IncorrectDt = null;
            }


        }
    }
}