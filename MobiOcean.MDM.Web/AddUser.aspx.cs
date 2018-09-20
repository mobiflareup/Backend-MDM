using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddUser : Base
    {

        RoleBAL role;
        usrBAL user;
        DeptBAL Dept;
        ClientBAL clientbal;
        DataTable dt, dtclient, dt1;
        LoginBAL lgn;
        SendSMSBAL sms;
        int ClientId, UserId, RoleId, DeptId;
        DataTable IncorrectDt;
        //AckBAL ackbal;
        PermisesBAL perm;
        //string Activity1;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            //lblpopmsg.Text = "";
            if (!IsPostBack)
            {
                dt1 = new DataTable();
                perm = new PermisesBAL();
                dt1 = perm.GetCountries();
                ViewState["GetCountry"] = dt1;
                dt = new DataTable();
                dt.Columns.Add("CountryId");
                dt.Columns.Add("CountryCode");
                dt.Columns.Add("MobileNo");
                ViewState["MobileNos"] = dt;
                //btnSaveExcel.Enabled = false;
                if (MultiView1.ActiveViewIndex == -1)
                {
                    pswd_info.Visible = false;
                }

                if (RoleId != 1)
                {
                    lblClient.Visible = ddlClient.Visible = false;
                }
                BindClientddl();
                BindBranchDropdown();
                BindDeptDDL();
                BindCountryddl();
            }
        }
        protected void BindClientddl()
        {
            #region--------- Get School List --------
            try
            {
                clientbal = new ClientBAL();
                ListItem li = new ListItem("--- Select Client ---", "0");
                ddlClient.Items.Clear();
                ddlClient.Items.Add(li);
                ddlClient.DataSource = clientbal.getdata();
                ddlClient.DataTextField = "ClientName";
                ddlClient.DataValueField = "ClientId";
                ddlClient.DataBind();
            }
            catch (Exception)
            {
            }
            finally
            {
                clientbal = null;
            }
            #endregion
        }
        protected void BindCountryddl()
        {
            #region--------- Get School List --------
            try
            {
                ListItem li = new ListItem("--- Select Country ---", "0");
                ddlCountry.Items.Clear();
                ddlCountry.Items.Add(li);
                ddlCountry.DataSource = (DataTable)ViewState["GetCountry"];
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
            }
            catch (Exception)
            {
            }

            #endregion
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            int CountryId = Convert.ToInt32(ddlCountry.SelectedItem.Value);
            dt = (DataTable)ViewState["GetCountry"];
            txtCode.Text = dt.Rows[CountryId - 1]["PhoneCode"].ToString();
            lblCountryId.Text = CountryId.ToString();
        }
        private void BindBranchDropdown()
        {
            Dept = new DeptBAL();
            try
            {
                try
                {
                    if (ddlClient.Visible)
                    {
                        ClientId = Convert.ToInt32(ddlClient.SelectedValue.ToString());
                    }
                }
                catch (Exception) { }
                ListItem li1 = new ListItem("--- Select Branch ---", "0");
                dtBranch.Items.Clear();
                dtBranch.Items.Add(li1);
                Dept.ClientId = ClientId;
                dtBranch.DataSource = Dept.GetBranchName();
                dtBranch.DataTextField = "BranchName";
                dtBranch.DataValueField = "BranchId";
                dtBranch.DataBind();
            }
            catch (Exception)
            {

            }
            finally
            {
                Dept = null;

            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkValidation())
                {
                    if (dtBranch.SelectedIndex > 0 && dtDepartment.SelectedIndex > 0)
                    {
                        if (((DataTable)ViewState["MobileNos"]).Rows.Count > 0 || MobileNoIsThere())
                        {

                            int ClId;
                            string MobileNoList = "", CountryIdList = "", MobileNo = "", CountryId = "", Password;
                            DataTable dtmovb = (DataTable)ViewState["MobileNos"];
                            if (!string.IsNullOrEmpty(txtmob.Text.Trim()))
                            {
                                bool canadd = true;
                                foreach (DataRow row in dtmovb.Rows)
                                {
                                    if (row["MobileNo"].ToString() == txtmob.Text.Trim())
                                    {
                                        canadd = false;
                                        break;
                                    }
                                }
                                if (canadd)
                                {
                                    dtmovb.Rows.Add(lblCountryId.Text, txtCode.Text, txtmob.Text.Trim());
                                }
                            }
                            foreach (DataRow row in dtmovb.Rows)
                            {
                                MobileNoList = MobileNoList + "," + row["MobileNo"].ToString();
                                CountryIdList = CountryIdList + "," + row["CountryId"].ToString();
                            }
                            if (MobileNoList.IndexOf(',') == 0)
                            {
                                MobileNoList = MobileNoList.Substring(1);
                                CountryIdList = CountryIdList.Substring(1);
                            }
                            MobileNo = MobileNoList.IndexOf(',') > 0 ? MobileNoList.Substring(0, MobileNoList.IndexOf(',')) : MobileNoList;
                            CountryId = CountryIdList.IndexOf(',') > 0 ? CountryIdList.Substring(0, CountryIdList.IndexOf(',')) : CountryIdList;
                            if (Convert.ToInt32(droprole.SelectedValue.ToString()) == 4)
                            {
                                Password = GenPass(8, "");//  GenRandomPassword("Password",8);
                            }
                            else
                            {
                                Password = txtpwd.Text.Trim();
                            }
                            if (!ddlClient.Visible)
                            {
                                ClId = ClientId;
                            }
                            else
                            {
                                ClId = Convert.ToInt32(ddlClient.SelectedValue.ToString());
                            }
                            if (ClId > 0)
                            {
                                user = new usrBAL();
                                user.ClientId = ClientId;
                                int result = user.CheckCountNoOfEmployees();
                                if (result > 0)
                                {

                                    string res = SaveUserAndDevice(0, ClId, txtEId.Text.Trim(), txtName.Text.Trim(), MobileNoList, CountryIdList, MobileNo, CountryId, txtemail.Text.Trim(),
                                    Convert.ToInt32(droprole.SelectedValue.ToString()), txtdst.Text.Trim(), Password, Convert.ToInt32(ddlRportngMngr.SelectedValue.ToString()), Convert.ToInt32(drpOwner.SelectedValue.ToString()));

                                    if (int.Parse(res) == -1)
                                    {
                                        lblpopmsg.Text = "One of the Phone No already exists!";
                                        lblpopmsg.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else if (int.Parse(res) == 0)
                                    {
                                        lblpopmsg.Text = "Email ID or Employee ID already exists!";
                                        lblpopmsg.ForeColor = System.Drawing.Color.Red;
                                    }
                                    else
                                    {
                                        lblpopmsg.Text = "User saved successfully.";
                                        lblpopmsg.ForeColor = System.Drawing.Color.Green;
                                        BindClientddl();
                                        BindBranchDropdown();
                                        BindDeptDDL();
                                        reset();
                                    }
                                }
                                else
                                {
                                    lblpopmsg.Text = "You don't have license to add more user.Please purchase more license.";
                                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                                }

                            }
                            else
                            {
                                lblpopmsg.Text = "Please select client";
                                lblpopmsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            lblpopmsg.Text = "Please add Phone No.";
                            lblpopmsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblpopmsg.Text = "Please select Branch & Department ";
                        lblpopmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblpopmsg.Text = "Please fill all the mandatory field";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception) { }
            dt = new DataTable();
            dt.Columns.Add("CountryId");
            dt.Columns.Add("CountryCode");
            dt.Columns.Add("MobileNo");
            ViewState["MobileNos"] = dt;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }

        private bool MobileNoIsThere()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtmob.Text.Trim()))
            {
                return false;
            }
            return true;
        }

        protected string SaveUserAndDevice(int help, int ClientId, string UserCode, string UserName, string MobileNoList, string CountryIdList, string MobileNo, string CountryId, string EmailId, int UserRoleId, string Designation, string Password, int RptMngrId, int Ownership)
        {
            if (help == 1)
            {
                string Countryidlst = "";
                string[] CountryIds1 = CountryIdList.Split(',');
                if (CountryIds1.Count() > 0)
                {
                    foreach (string country in CountryIds1)
                    {
                        Countryidlst += ContactCountryCode(country) + ",";
                    }
                    CountryIdList = Countryidlst.TrimEnd(',');
                }
                CountryId = ContactCountryCode(CountryId);
            }
            user = new usrBAL();
            user.UserId = 0;
            user.ClientId = ClientId;
            user.UserCode = UserCode;
            user.UserName = UserName;
            user.MobileNoList = MobileNoList;
            user.MobileNo = MobileNo;
            user.CountryId = CountryId;
            user.EmailId = EmailId;
            user.DeptId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
            user.RoleId = UserRoleId;
            user.EmpCompanyId = UserCode;
            user.Branch = Convert.ToInt32(dtBranch.SelectedValue.ToString());
            user.Designation = Designation;
            user.Password = Password;
            user.RptMngrId = RptMngrId;
            user.DeviceOwnerShip = Ownership;
            user.Country = CountryId;
            user.LoggedBy = UserId.ToString();
            dt = new DataTable();
            dt = user.InsertUserWithMultipleDevice();
            if (int.Parse(dt.Rows[0]["UserId"].ToString()) == -1)
            {
                return "-1";
            }
            else if (int.Parse(dt.Rows[0]["UserId"].ToString()) == 0)
            {
                return "0";
            }
            else
            {
                lgn = new LoginBAL();
                lgn.ClientId = ClientId;
                lgn.UserID = int.Parse(dt.Rows[0]["UserId"].ToString());
                lgn.RoleId = user.RoleId;
                lgn.DeptId = user.DeptId;
                lgn.UserName = user.UserName;
                lgn.LoginKey = GenPass(28, "");// GenRandomPassword("LoginKey",28);
                lgn.currentDateTime = GetCurrentDateTimeByUserId();
                lgn.InsertFirstLoginData();
                clientbal = new ClientBAL();
                dtclient = new DataTable();
                clientbal.ClientId = user.ClientId;
                dtclient = clientbal.GetClientByClientId();
                if (UserRoleId <= 3 || Convert.ToInt32(dtclient.Rows[0]["IsPasswordVisible"].ToString()) > 0)
                {
                    SendMailBAL mail = new SendMailBAL();
                    mail.UserRegister(lgn.UserName, 1, EmailId, lgn.LoginKey, dtclient.Rows[0]["ProductKey"].ToString(), dtclient.Rows[0]["ExpiryDate"].ToString(), ClientId, RoleId);
                }
                string[] MobileNos = MobileNoList.Split(',');
                string[] CountryIds = CountryIdList.Split(',');
                string downloadlink =  Constant.CommonAppUrl;//: Constant.AppDownloadUrl;ClientId == 399 ? Constant.LTAppDownloadUrl :
                sms = new SendSMSBAL();
                for (int i = 0; i < MobileNos.Count(); i++)
                {
                    user = new usrBAL();
                    user.DeviceId = 0;
                    user.UserId = lgn.UserID;
                    user.DeviceName = MobileNos[i];
                    user.MobileNo1 = MobileNos[i];
                    user.CountryId = CountryIds[i];
                    if (ClientId == 298)
                    {
                        KSWDBAL kswd = new KSWDBAL();
                        kswd.MobileNo = MobileNos[i];
                        string appid = kswd.GetAppIdByMobileNo();
                        if (appid != null)
                        {
                            user.APPIdKSWD = appid;
                        }
                    }
                    user.ClientId = ClientId;
                    user.DeviceOwnerShip = Ownership;
                    int DeviceId = Convert.ToInt32(user.InsertUserDeviceDataRaj1());
                    if (DeviceId > 0)
                    {
                        sms.sendMsgUsingSMS("Dear " + txtName.Text.Trim() + ", You have been registered on MobiOcean by " + Session["UserName"] + ". Please use the below URL to download the MobiOcean APP: " + downloadlink + " . After downloading, please use the below info to activate the APP: Mobile No:" + MobileNos[i] + " ", MobileNos[i], ClientId);//Client Code:" + dtclient.Rows[0]["ClientCode"].ToString() + "   User Code: " + UserCode + "
                    }
                }
                return "1";
            }
        }

        private string ContactCountryCode(string ContryCode)
        {
            if (!(ContryCode.IndexOf("+") == 0))
            {
                ContryCode = ContryCode.Replace("+", "");
                while (ContryCode.IndexOf("0") == 0 && ContryCode.Length > 1)
                {
                    ContryCode = ContryCode.Substring(1);
                }
                ContryCode = "+" + ContryCode;
            }
            DataRow dataRow = ((DataTable)ViewState["GetCountry"]).AsEnumerable().FirstOrDefault(r => r["PhoneCode"].ToString() == ContryCode);
            return (dataRow != null) ? dataRow["CountryId"].ToString() : "1";
        }

        protected bool ChkValidation()
        {
            if (ClientId <= 0 || txtName.Text.Trim() == "" || txtEId.Text.Trim() == "" || txtemail.Text.Trim() == "" || droprole.SelectedIndex <= 0)//|| txtpwd.Text.Trim() == "" || txtCnfrmPwd.Text.Trim() == ""
            {
                return false;
            }
            else
            {
                if (ddlRportngMngr.Items.Count > 1 && ddlRportngMngr.SelectedIndex <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public void reset()
        {
            txtName.Text = string.Empty;
            txtEId.Text = string.Empty;
            txtemail.Text = string.Empty;
            txtmob.Text = string.Empty;
            txtdst.Text = string.Empty;
            txtpwd.Text = string.Empty;
            txtCnfrmPwd.Text = string.Empty;
            dt = new DataTable();
            dt.Columns.Add("CountryId");
            dt.Columns.Add("CountryCode");
            dt.Columns.Add("MobileNo");
            ViewState["MobileNos"] = dt;
            gdvMobile.DataSource = dt;
            gdvMobile.DataBind();
            ddlCountry.SelectedIndex = 0;
            txtCode.Text = string.Empty;
            droprole.SelectedIndex = 0;
            ddlRportngMngr.SelectedIndex = 0;
            drpOwner.SelectedIndex = 0;
            lblpwd.Visible = true;
            txtpwd.Visible = true;
            lblCnfmPwd.Visible = true;
            txtCnfrmPwd.Visible = true;
            pswd_info.Visible = true;
            RegularExpressionValidatorpwd.ValidationGroup = "save";
            RequiredFieldValidatorcnfrm.ValidationGroup = "save";
        }
        protected void BindDeptDDL()
        {
            ListItem ls = new ListItem("--Select Department--", "0");
            try
            {
                try
                {
                    if (ddlClient.Visible)
                    {
                        ClientId = Convert.ToInt32(ddlClient.SelectedValue.ToString());
                    }
                }
                catch (Exception) { }
                Dept = new DeptBAL();
                Dept.ClientId = ClientId;
                dtDepartment.Items.Clear();
                dtDepartment.Items.Add(ls);
                dtDepartment.DataSource = Dept.GetDptNameDDL();
                dtDepartment.DataTextField = "DeptName";
                dtDepartment.DataValueField = "DeptId";
                dtDepartment.DataBind();
            }
            catch (Exception) { }
            finally
            {
                Dept = null;
                ls = null;
            }

        }
        protected void BindUserRoleDDL()
        {
            ListItem ls = new ListItem("----Select---", "0");
            try
            {
                role = new RoleBAL();
                role.ClientId = ClientId;
                droprole.Items.Clear();
                droprole.Items.Add(ls);
                droprole.DataSource = role.GetRoleDDL();
                droprole.DataTextField = "RoleName";
                droprole.DataValueField = "RoleId";
                droprole.DataBind();
                if (!ddlClient.Visible)
                {
                    droprole.Items.Remove(new ListItem("Super Admin", "1"));
                }
            }
            catch (Exception) { }
            finally
            {
                role = null;
                ls = null;
            }

        }
        protected void BindReptMgrDDL(DropDownList ddl)
        {
            ListItem ls = new ListItem("---Select---", "0");
            try
            {
                try
                {
                    if (ddlClient.Visible)
                    {
                        ClientId = Convert.ToInt32(ddlClient.SelectedValue.ToString());
                    }
                }
                catch (Exception) { }

                user = new usrBAL();
                user.ClientId = ClientId;
                ddl.Items.Clear();
                ddl.Items.Add(ls);
                ddl.DataSource = user.GetReportingMgr();
                ddl.DataTextField = "UserName";
                ddl.DataValueField = "UserId";
                ddl.DataBind();
            }
            catch (Exception) { }
            finally
            {
                user = null;
                ls = null;
            }

        }
        protected void BindUserOwnerShip(DropDownList ddl)
        {
            ListItem ls = new ListItem("--- Select ---", "0");
            try
            {
                role = new RoleBAL();
                ddl.Items.Clear();
                ddl.Items.Add(ls);
                ddl.DataSource = role.GetOwnerShipName();
                ddl.DataTextField = "OwnerName";
                ddl.DataValueField = "OwnerId";
                ddl.DataBind();
            }
            catch (Exception) { }
            finally
            {
                role = null;
                ls = null;
            }

        }
        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBranchDropdown();
            BindDeptDDL();
            BindReptMgrDDL(ddlRportngMngr);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            reset();
        }
        protected void btnForm_Click(object sender, EventArgs e)
        {
            CompareValidator4.ValidationGroup = "save";
            CompareValidator3.ValidationGroup = "save";
            BindUserRoleDDL();
            BindReptMgrDDL(ddlRportngMngr);
            BindUserOwnerShip(drpOwner);
            pswd_info.Visible = true;
            MultiView1.ActiveViewIndex = 0;
            reset();
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            CompareValidator4.ValidationGroup = "upload";
            CompareValidator3.ValidationGroup = "upload";
            BindReptMgrDDL(dddlExcelRptMngr);
            BindUserOwnerShip(dddlExcelOwner);
            pswd_info.Visible = false;
            MultiView1.ActiveViewIndex = 1;
        }
        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = "";
            try
            {
                value = cell.CellValue.InnerText.Trim();
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText.Trim();
                }
                else if (cell.DataType == null)
                {
                    return Convert.ToDateTime(doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText).ToString("dd-MMM-yyyy HH:mm");

                }
                return value;
            }
            catch (Exception)
            {
                return value.Trim();
            }

        }
        private static List<char> Letters = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ' };

        /// <summary>
        /// Given a cell name, parses the specified cell to get the column name.
        /// </summary>
        /// <param name="cellReference">Address of the cell (ie. B2)</param>
        /// <returns>Column Name (ie. B)</returns>
        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);

            return match.Value;
        }

        /// <summary>
        /// Given just the column name (no row index), it will return the zero based column index.
        /// Note: This method will only handle columns with a length of up to two (ie. A to Z and AA to ZZ). 
        /// A length of three can be implemented when needed.
        /// </summary>
        /// <param name="columnName">Column Name (ie. A or AB)</param>
        /// <returns>Zero based index if the conversion was successful; otherwise null</returns>
        public static int? GetColumnIndexFromName(string columnName)
        {
            int? columnIndex = null;

            string[] colLetters = Regex.Split(columnName, "([A-Z]+)");
            colLetters = colLetters.Where(s => !string.IsNullOrEmpty(s)).ToArray();

            if (colLetters.Count() <= 2)
            {
                int index = 0;
                foreach (string col in colLetters)
                {
                    List<char> col1 = colLetters.ElementAt(index).ToCharArray().ToList();
                    int? indexValue = Letters.IndexOf(col1.ElementAt(index));

                    if (indexValue != -1)
                    {
                        // The first letter of a two digit column needs some extra calculations
                        if (index == 0 && colLetters.Count() == 2)
                        {
                            columnIndex = columnIndex == null ? (indexValue + 1) * 26 : columnIndex + ((indexValue + 1) * 26);
                        }
                        else
                        {
                            columnIndex = columnIndex == null ? indexValue : columnIndex + indexValue;
                        }
                    }

                    index++;
                }
            }

            return columnIndex;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable dtgv = new DataTable();
                //GridView1.DataSource = dtgv;
                //GridView1.DataBind();
                if (Convert.ToInt32(dddlExcelRptMngr.SelectedValue.ToString()) != 0)
                {
                    if (dddlExcelOwner.SelectedValue != "0")
                    {
                        if (dtBranch.SelectedValue != "0" && dtDepartment.SelectedValue != "0")
                        {
                            if (FileUpload1.HasFile)
                            {
                                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                                string FilePath = Server.MapPath(Constant.FolderPath + FileName);
                                FileUpload1.SaveAs(FilePath);
                                //Import_To_Grid(FilePath, Extension, "Yes");



                                //Open the Excel file in Read Mode using OpenXml.
                                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                                {
                                    //Read the first Sheet from Excel file.                        
                                    Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                                    //Get the Worksheet instance.
                                    Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                                    //Fetch all the rows present in the Worksheet.
                                    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                                    //Create a new DataTable.
                                    DataTable dt = new DataTable();
                                    //Loop through the Worksheet rows.
                                    foreach (Row row in rows)
                                    {
                                        //Use the first row to add columns to DataTable.
                                        if (row.RowIndex.Value == 1)
                                        {
                                            foreach (Cell cell in row.Descendants<Cell>())
                                            {
                                                dt.Columns.Add(GetValue(doc, cell));
                                            }
                                        }
                                        else
                                        {
                                            //Add rows to DataTable.
                                            try
                                            {
                                                dt.Rows.Add();
                                                //int i = 0;
                                                int columnIndex = 0;
                                                foreach (Cell cell in row.Descendants<Cell>())
                                                {
                                                    int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cell.CellReference));
                                                    if (columnIndex < cellColumnIndex)
                                                    {
                                                        do
                                                        {
                                                            dt.Rows[dt.Rows.Count - 1][columnIndex] = string.Empty; //Insert blank data here;
                                                            columnIndex++;
                                                        }
                                                        while (columnIndex < cellColumnIndex);
                                                    }
                                                    dt.Rows[dt.Rows.Count - 1][columnIndex] = GetValue(doc, cell);
                                                    columnIndex++;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    //GridView1.DataSource = dt;
                                    //GridView1.DataBind();
                                    chkexcel(dt);
                                }
                            }
                            else
                            {
                                lblpopmsg.Text = "Please choose file.";
                                lblpopmsg.Visible = true;
                                lblpopmsg.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            lblpopmsg.Text = "Please select device ownership.";
                            lblpopmsg.Visible = true;
                            lblpopmsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblpopmsg.Text = "Please select Branch & Department ";
                        lblpopmsg.Visible = true;
                        lblpopmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblpopmsg.Text = "Please select Reporting Manager.";
                    lblpopmsg.Visible = true;
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                lblpopmsg.Text = "Please upload file in correct format.";
                lblpopmsg.Visible = true;
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void chkexcel(DataTable dt)
        {
            try
            {
                IncorrectDt = new DataTable();
                IncorrectDt.Columns.Add("Name");
                IncorrectDt.Columns.Add("Employee Id");
                IncorrectDt.Columns.Add("Email Id");
                IncorrectDt.Columns.Add("CountryCode for Mobile No");
                IncorrectDt.Columns.Add("Mobile No");
                IncorrectDt.Columns.Add("Designation");
                IncorrectDt.Columns.Add("Status");

                DataRow[] rows = dt.Select(); //"Mobile_No='" + lblmobileno.Text.Trim() + "'");
                foreach (DataRow row in rows)
                {
                    if (!string.IsNullOrEmpty(row["Email Id"].ToString()) && !string.IsNullOrEmpty(row["Name"].ToString()) && !string.IsNullOrEmpty(row["Employee Id"].ToString()) && !string.IsNullOrEmpty(row["Mobile No"].ToString()))
                    {
                        if (ChkEmail(row["Email Id"].ToString()))
                        {
                            if (ChkMobileNo(row["Mobile No"].ToString()))
                            {
                                try
                                {
                                    string Mobno = row["Mobile No"].ToString().IndexOf(',') > 0 ? row["Mobile No"].ToString().Substring(0, row["Mobile No"].ToString().IndexOf(',')) : row["Mobile No"].ToString();
                                    string CountryId = row["CountryCode for Mobile No"].ToString().IndexOf(',') > 0 ? row["CountryCode for Mobile No"].ToString().Substring(0, row["CountryCode for Mobile No"].ToString().IndexOf(',')) : row["CountryCode for Mobile No"].ToString();
                                    string pwd = GenPass(8, "");// GenRandomPassword("Password", 8);
                                    user = new usrBAL();
                                    user.ClientId = ClientId;
                                    int result = user.CheckCountNoOfEmployees();
                                    if (result > 0)
                                    {
                                        string res = SaveUserAndDevice(1, ClientId, row["Employee Id"].ToString(), row["Name"].ToString(), row["Mobile No"].ToString(), row["CountryCode for Mobile No"].ToString(), Mobno, CountryId, row["Email Id"].ToString(),
                                           4, row["Designation"].ToString(), pwd, Convert.ToInt32(dddlExcelRptMngr.SelectedValue.ToString()), Convert.ToInt32(dddlExcelOwner.SelectedValue.ToString()));
                                        if (int.Parse(res) == -1)
                                        {
                                            IncorrectDt.Rows.Add(row["Name"], row["Employee Id"], row["Email Id"], row["Mobile No"],
                                              row["Designation"], "One of the Mobile No already exists!");
                                        }
                                        else if (int.Parse(res) == 0)
                                        {
                                            IncorrectDt.Rows.Add(row["Name"], row["Employee Id"], row["Email Id"], row["Mobile No"],
                                               row["Designation"], "Email Id or Employee Id already exists!");
                                        }
                                        else
                                        {
                                            //lblpopmsg.Text = "User saved successfully.";
                                            //lblpopmsg.ForeColor = System.Drawing.Color.Green;
                                            //reset();
                                        }
                                    }
                                    else
                                    {
                                        IncorrectDt.Rows.Add(row["Name"], row["Employee Id"], row["Email Id"], row["Mobile No"],
                                               row["Designation"], "You don't have license to add more user.Please Purchase More License.");
                                    }
                                }
                                catch (Exception)
                                {
                                    IncorrectDt.Rows.Add(row["Name"], row["Employee Id"], row["Email Id"], row["Mobile No"],
                                         row["Designation"], "Empty Field not Allowed");
                                }
                            }
                            else
                            {
                                IncorrectDt.Rows.Add(row["Name"], row["Employee Id"], row["Email Id"], row["Mobile No"],
                                  row["Designation"], "Invalid Mobile No");
                            }
                        }
                        else
                        {
                            IncorrectDt.Rows.Add(row["Name"], row["Employee Id"], row["Email Id"], row["Mobile No"],
                                  row["Designation"], "Invalid Email");
                        }
                    }
                    else
                    {
                        IncorrectDt.Rows.Add(row["Name"], row["Employee Id"], row["Email Id"], row["Mobile No"],
                                  row["Designation"], "Empty Field not Allowed");
                    }
                }
                try
                {
                    if (File.Exists(Server.MapPath(Constant.FolderPath)))
                    {
                        Array.ForEach(Directory.GetFiles(Server.MapPath(Constant.FolderPath)), File.Delete);
                    }
                    dddlExcelOwner.SelectedIndex = 0;
                    dddlExcelRptMngr.SelectedIndex = 0;
                    if (IncorrectDt.Rows.Count > 0)
                    {
                        //   btnSaveExcel.Enabled = false;
                        lblpopmsg.Text = "Data of the the below table not updated. Please check the data and try again.";
                        lblpopmsg.Visible = true;
                        lblpopmsg.ForeColor = System.Drawing.Color.Red;
                        GridView1.DataSource = IncorrectDt;
                        GridView1.DataBind();
                        //GridView1.Visible = false;
                    }
                    else
                    {
                        //.Enabled = false;
                        lblpopmsg.Text = "Excel uploaded Successfully.";
                        lblpopmsg.Visible = true;
                        lblpopmsg.ForeColor = System.Drawing.Color.Green;
                    }
                }
                finally
                { }
            }
            catch (Exception)
            {
                lblpopmsg.Text = "Please upload excel sheet in correct format";
                lblpopmsg.Visible = true;
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                dt = null;
                IncorrectDt = null;
            }

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        protected void droprole_SelectedIndexChanged(object sender, EventArgs e)
        {
            int roleid = Convert.ToInt32(droprole.SelectedValue.ToString());
            if (roleid == 4)
            {
                lblpwd.Visible = false;
                txtpwd.Visible = false;
                lblCnfmPwd.Visible = false;
                txtCnfrmPwd.Visible = false;
                pswd_info.Visible = false;
                string Pwd = GenPass(8, "");// GenRandomPassword("Password", 8);
                txtpwd.Text = Pwd;
                txtCnfrmPwd.Text = Pwd;
                RegularExpressionValidatorpwd.ValidationGroup = "";
                RequiredFieldValidatorcnfrm.ValidationGroup = "";
                req.ValidationGroup = "";
            }
            else
            {
                lblpwd.Visible = true;
                txtpwd.Visible = true;
                lblCnfmPwd.Visible = true;
                txtCnfrmPwd.Visible = true;
                pswd_info.Visible = true;
                RegularExpressionValidatorpwd.ValidationGroup = "save";
                RequiredFieldValidatorcnfrm.ValidationGroup = "save";
                req.ValidationGroup = "save";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtmob.Text.Trim() != "")
            {
                if (txtmob.Text.Length > 2 && txtmob.Text.Length < 16)
                {

                    dt = new DataTable();
                    dt = (DataTable)ViewState["MobileNos"];
                    bool canadd = true;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["MobileNo"].ToString() == txtmob.Text.Trim())
                        {
                            canadd = false;
                            break;
                        }
                    }
                    if (canadd)
                    {
                        dt.Rows.Add(lblCountryId.Text, txtCode.Text, txtmob.Text.Trim());
                        gdvMobile.DataSource = dt;
                        gdvMobile.DataBind();
                        txtmob.Text = txtCode.Text = string.Empty;
                        ddlCountry.SelectedIndex = 0;
                    }
                    else
                    {
                        lblpopmsg.Text = "Phone No already exists.";
                        lblpopmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblpopmsg.Text = "Enter correct Phone No";
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                lblpopmsg.Text = "Enter Phone No";
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }

        }
        protected void gdvMobile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            dt = new DataTable();
            dt = (DataTable)ViewState["MobileNos"];
            dt.Rows.Remove(dt.Rows[e.RowIndex]);
            //DataRow[] rows = dt.Rows.Select("MobileNo="+txtmob.Text.Trim());
            ViewState["MobileNos"] = dt;
            gdvMobile.DataSource = dt;
            gdvMobile.DataBind();
            //txtmob.Text = string.Empty;
        }
        protected bool ChkEmail(string ChkEmail)
        {
            string MatchEmailPattern =
                @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
         + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
         + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
         + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

            if (ChkEmail != null) return Regex.IsMatch(ChkEmail, MatchEmailPattern);
            else return false;
            //return true;
        }
        protected bool ChkMobileNo(string ChkMob)
        {
            string MatchMObPattern = @"[0-9]{3,14}";
            if (ChkMob != null)
            {
                string[] ChkMoblist = ChkMob.Split(',');
                foreach (string mob in ChkMoblist)
                {
                    if (!Regex.IsMatch(mob, MatchMObPattern))
                    {
                        break;
                    }
                    return true;
                }
                return false;
            }
            return false;
        }
        protected void btncanclerset_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
        }
        protected void btndwnld_Click(object sender, EventArgs e)
        {
            string filename = "AddUser.xlsx";
            Response.ContentType = "application/vnd.xlsx";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + "");
            Response.TransmitFile(Server.MapPath("~/Files/Format/" + filename));
            Response.End();
        }
        protected void btndwnld_Click1(object sender, EventArgs e)
        {
            string filename = "AddUser.xlsx";
            Response.ContentType = "application/vnd.xlsx";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + "");
            Response.TransmitFile(Server.MapPath("~/Files/Format/" + filename));
            Response.End();
        }
    }
}
