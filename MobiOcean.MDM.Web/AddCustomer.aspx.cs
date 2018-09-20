using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AddCustomer :Base
    {
        CustomerBAL customer;
        int ClientId, UserId, RoleId, DeptId;
        DataTable IncorrectDt, dt1;
        PermisesBAL perm;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                dt1 = new DataTable();
                perm = new PermisesBAL();
                dt1 = perm.GetCountries();
                ViewState["GetCountry"] = dt1;
                BindCountryddl();
            }
        }
        protected void BindCountryddl()
        {
            #region--------- Get School List --------
            try
            {
                ListItem li = new ListItem("--- Select ---", "0");
                ddlCountry.Items.Clear();
                ddlCountry.Items.Add(li);
                ddlCountry.DataSource = (DataTable)ViewState["GetCountry"];
                ddlCountry.DataTextField = "Country";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();

                ListItem li1 = new ListItem("--- Select ---", "0");
                ddlaltcontact.Items.Clear();
                ddlaltcontact.Items.Add(li1);
                ddlaltcontact.DataSource = (DataTable)ViewState["GetCountry"];
                ddlaltcontact.DataTextField = "Country";
                ddlaltcontact.DataValueField = "CountryId";
                ddlaltcontact.DataBind();
            }
            catch (Exception)
            {
            }
            #endregion
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAltmobile.Text))
            {
                if (Convert.ToInt32(ddlaltcontact.SelectedItem.Value) > 0)
                    SaveCustomer();
                else
                {
                    lblpopmsg.Text = "Please Select Country for Alternate Contact ";
                    lblpopmsg.Visible = true;
                    lblpopmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                SaveCustomer();
            }

        }
        private void SaveCustomer()
        {
            customer = new CustomerBAL();
            customer.UserId = UserId;
            customer.ClientId = ClientId;
            customer.CustomerName = txtName.Text.Trim();
            customer.CountryId = ddlCountry.SelectedItem.Value;
            customer.MobileNo = txtmobile.Text.Trim();
            customer.AltCountryId = ddlaltcontact.SelectedItem.Value;
            customer.ALtMobileNo = txtAltmobile.Text.Trim();
            customer.ContactPersion = txtcontact.Text.Trim();
            customer.AltContactPersion = txtaltcontactpersion.Text.Trim();
            customer.EmailId = txtemail.Text.Trim();
            customer.AltEmailId = txtAltEmail.Text.Trim();
            customer.Address = txtAddress.Text.Trim();
            customer.Latitude = txtLat.Text.Trim();
            customer.Longitude = txtLong.Text.Trim();
            customer.City = txtcity.Text.Trim();
            customer.District = txtDist.Text.Trim();
            customer.state = txtState.Text.Trim();
            customer.country = txtCountry.Text.Trim();
            customer.PinCode = txtPin.Text.Trim();
            customer.TinNumber = txttin.Text.Trim();
            customer.IsInsert = 1;
            int res = customer.customerInsertRaj();
            if (res > 0)
            {
                lblpopmsg.Text = "Customer Details Added Successfully ";
                lblpopmsg.Visible = true;
                lblpopmsg.ForeColor = System.Drawing.Color.Green;
                Reset();
            }
            else
            {
                lblpopmsg.Text = "Something Went Wrong.. ";
                lblpopmsg.Visible = true;
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnForm_Click(object sender, EventArgs e)
        {
            lblpopmsg.Text = "";
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            lblpopmsg.Text = "";
            MultiView1.ActiveViewIndex = 1;
        }


        protected void btnupload_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtgv = new DataTable();
                GridView1.DataSource = dtgv;
                GridView1.DataBind();



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
            catch (Exception)
            {
                lblpopmsg.Text = "Please upload file in correct format.";
                lblpopmsg.Visible = true;
                lblpopmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = "";
            try
            {
                value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }
                else if (cell.DataType == null)
                {
                    return Convert.ToDateTime(doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText).ToString("dd-MMM-yyyy HH:mm");

                }
                return value;
            }
            catch (Exception)
            {
                return value;
            }

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
        protected void chkexcel(DataTable dt)
        {
            try
            {
                //correctdt = new DataTable();
                //correctdt.Columns.Add("Name");
                //correctdt.Columns.Add("Employee Id");
                //correctdt.Columns.Add("Email Id");
                //correctdt.Columns.Add("Mobile No");
                //correctdt.Columns.Add("Designation");
                IncorrectDt = new DataTable();
                IncorrectDt.Columns.Add("Company Name");
                IncorrectDt.Columns.Add("Tin No");
                IncorrectDt.Columns.Add("CountryCode for Contact No");
                IncorrectDt.Columns.Add("Contact No");
                IncorrectDt.Columns.Add("CountryCode for Alternate Contact No");
                IncorrectDt.Columns.Add("Alternate Contact No");
                IncorrectDt.Columns.Add("Contact Person");
                IncorrectDt.Columns.Add("Alternate contact Person");
                IncorrectDt.Columns.Add("Email Id");
                IncorrectDt.Columns.Add("Alternate Email Id");
                IncorrectDt.Columns.Add("Address line 1");
                IncorrectDt.Columns.Add("Address line 2");
                IncorrectDt.Columns.Add("city");
                IncorrectDt.Columns.Add("State");
                IncorrectDt.Columns.Add("Country");
                IncorrectDt.Columns.Add("Pin No");
                IncorrectDt.Columns.Add("Latitude");
                IncorrectDt.Columns.Add("Longitude");
                DataRow[] rows = dt.Select(); //"Mobile_No='" + lblmobileno.Text.Trim() + "'");
                foreach (DataRow row in rows)
                {
                    if (!string.IsNullOrEmpty(row["Contact Person"].ToString()) && !string.IsNullOrEmpty(row["Latitude"].ToString()) && !string.IsNullOrEmpty(row["Longitude"].ToString()) && !string.IsNullOrEmpty(row["city"].ToString()) && !string.IsNullOrEmpty(row["Pin No"].ToString()) && !string.IsNullOrEmpty(row["Country"].ToString()) && !string.IsNullOrEmpty(row["Email Id"].ToString()) && !string.IsNullOrEmpty(row["Company Name"].ToString()) && !string.IsNullOrEmpty(row["State"].ToString()) && !string.IsNullOrEmpty(row["Contact No"].ToString()))
                    {
                        if (ChkEmail(row["Email Id"].ToString()))
                        {
                            if (ChkMobileNo(row["Contact No"].ToString()))
                            {
                                try
                                {

                                    customer = new CustomerBAL();
                                    customer.UserId = UserId;
                                    customer.ClientId = ClientId;
                                    customer.CustomerName = row["Company Name"].ToString();
                                    if (!string.IsNullOrEmpty(row["CountryCode for Contact No"].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(row["CountryCode for Contact No"].ToString()))
                                            //customer.CountryId = ContactCountryCode("+06561");
                                            customer.CountryId = ContactCountryCode(row["CountryCode for Contact No"].ToString());
                                        else
                                            customer.CountryId = "1";
                                    }
                                    else
                                    {
                                        customer.CountryId = "1";
                                    }
                                    customer.MobileNo = row["Contact No"].ToString();
                                    if (!string.IsNullOrEmpty(row["Alternate Contact No"].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(row["CountryCode for Alternate Contact No"].ToString()))
                                            //customer.CountryId = ContactCountryCode("+10091");
                                            customer.CountryId = ContactCountryCode(row["CountryCode for Alternate Contact No"].ToString());
                                        else
                                            customer.CountryId = "1";
                                    }
                                    customer.ALtMobileNo = row["Alternate Contact No"].ToString();
                                    customer.ContactPersion = row["Contact Person"].ToString();
                                    customer.AltContactPersion = row["Alternate contact Person"].ToString();
                                    customer.EmailId = row["Email Id"].ToString();
                                    customer.AltEmailId = row["Alternate Email Id"].ToString();
                                    customer.Address = row["Address line 1"].ToString();
                                    customer.District = row["Address line 2"].ToString();
                                    customer.Latitude = row["Latitude"].ToString();
                                    customer.Longitude = row["Longitude"].ToString();
                                    customer.City = row["city"].ToString();

                                    customer.state = row["State"].ToString();
                                    customer.country = row["Country"].ToString();
                                    customer.PinCode = row["Pin No"].ToString();
                                    customer.TinNumber = row["Tin No"].ToString();
                                    customer.IsInsert = 1;
                                    // SaveCustomer();
                                    int res = customer.customerInsertRaj();

                                }
                                catch (Exception ex)
                                {
                                    string a = ex.Message;
                                    IncorrectDt.Rows.Add(row["Contact Person"], row["Address line 1"], row["Latitude"], row["Longitude"], row["city"], row["Address line 2"], row["Tin No"],
                                        row["Pin No"], row["Country"], row["Email Id"], row["Company Name"], row["State"], row["Contact No"], "Empty Field not Allowed");
                                }
                            }
                            else
                            {
                                IncorrectDt.Rows.Add(row["Contact Person"], row["Address line 1"], row["Latitude"], row["Longitude"], row["city"], row["Address line 2"], row["Tin No"],
                                        row["Pin No"], row["Country"], row["Email Id"], row["Company Name"], row["State"], row["Contact No"], "Invalid Contact No");
                            }
                        }
                        else
                        {
                            IncorrectDt.Rows.Add(row["Contact Person"], row["Address line 1"], row["Latitude"], row["Longitude"], row["city"], row["Address line 2"], row["Tin No"],
                                        row["Pin No"], row["Country"], row["Email Id"], row["Company Name"], row["State"], row["Contact No"], "Invalid Email");
                        }
                    }
                    else
                    {
                        IncorrectDt.Rows.Add(row["Contact Person"], row["Address line 1"], row["Latitude"], row["Longitude"], row["city"], row["Address line 2"], row["Tin No"],
                                        row["Pin No"], row["Country"], row["Email Id"], row["Company Name"], row["State"], row["Contact No"], "Empty Field not Allowed");
                    }
                }
                try
                {
                    if (File.Exists(Server.MapPath(Constant.FolderPath)))
                    {
                        Array.ForEach(Directory.GetFiles(Server.MapPath(Constant.FolderPath)), File.Delete);
                    }

                    if (IncorrectDt.Rows.Count > 0)
                    {
                        //   btnSaveExcel.Enabled = false;
                        lblpopmsg.Text = "Data of the the below table not updated.Please check the data and try again";
                        lblpopmsg.Visible = true;
                        lblpopmsg.ForeColor = System.Drawing.Color.Red;
                        GridView1.DataSource = IncorrectDt;
                        GridView1.DataBind();
                        //GridView1.Visible = false;
                    }
                    else
                    {
                        //.Enabled = false;
                        lblpopmsg.Text = "Excel uploaded Successfully";
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
            string MatchMObPattern = @"^([1-9]{1})([0-9]{9})$";
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
        protected void btndwnld_Click(object sender, EventArgs e)
        {
            string filename = "Addcustomer.xlsx";
            Response.ContentType = "application/vnd.xlsx";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + "");
            Response.TransmitFile(Server.MapPath("~/Files/Format/" + filename));
            Response.End();
        }
        private void Reset()
        {
            txtName.Text = "";
            txtmobile.Text = "";
            txtAltmobile.Text = "";
            txtcontact.Text = "";
            txtaltcontactpersion.Text = "";
            txtemail.Text = "";
            txtAltEmail.Text = "";
            txtAddress.Text = "";

            ddlaltcontact.SelectedIndex = 0;
            ddlCountry.SelectedIndex = 0;

            txtLat.Text = "";
            txtLong.Text = "";
            txtcity.Text = "";
            txtDist.Text = "";
            txtState.Text = "";
            txtCountry.Text = "";
            txtPin.Text = "";
            txttin.Text = "";
        }
    }
}