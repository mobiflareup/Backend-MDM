using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class AssignDailyCustomerByExcel : Base
    {
        CustomerBAL customer;
        int ClientId, UserId, RoleId, DeptId;
        DataTable IncorrectDt,dt1;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            txtFrDt.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
               // txtFrDt.Text = GetCurrentDateTimeByUserId().ToString("dd-MMM-yyyy");
            }
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
                    lblMsg.Text = "Please choose file.";
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }



            }
            catch (Exception)
            {
                lblMsg.Text = "Please upload file in correct format.";
                lblMsg.Visible = true;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = "";
            try
            {
                value = cell.CellValue.InnerText.Trim();
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


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ManageCustomer.aspx");
        }

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
                Boolean columnExists = dt.Columns.Contains("Task Detail");
                IncorrectDt = new DataTable();
                IncorrectDt.Columns.Add("Employee Name");
                IncorrectDt.Columns.Add("Employee Mobile No");
                IncorrectDt.Columns.Add("Customer Name");
                IncorrectDt.Columns.Add("Time");
                if (columnExists)
                {
                    IncorrectDt.Columns.Add("Task Detail");
                }
                IncorrectDt.Columns.Add("Error");
                DataRow[] rows = dt.Select();
                foreach (DataRow row in rows)
                {
                    if (!string.IsNullOrEmpty(row["Employee Name"].ToString()) && !string.IsNullOrEmpty(row["Customer Name"].ToString()) && !string.IsNullOrEmpty(row["Employee Mobile No"].ToString()) && !string.IsNullOrEmpty(row["Time"].ToString()))
                    {
                        if (ChkMobileNo(row["Employee Mobile No"].ToString()))
                        {
                            if (ChkDateTime(row["Time"].ToString()))
                            {
                                try
                                {
                                    customer = new CustomerBAL();
                                    customer.ClientId = ClientId;
                                    customer.EmployeeName = row["Employee Name"].ToString();
                                    customer.CustomerName = row["Customer Name"].ToString();
                                    customer.MobileNo = row["Employee Mobile No"].ToString();
                                    customer.Date = txtFrDt.Text;
                                    customer.Time = DateTime.FromOADate(Convert.ToDouble(row["Time"].ToString())).ToString("HH:mm:ss");
                                    customer.CreatedBy = UserId;
                                    customer.currentDateTime = GetCurrentDateTimeByUserId();
                                    if (columnExists)
                                    {
                                        customer.TaskDetail = row["Task Detail"].ToString();
                                    }
                                    dt = new DataTable();
                                    dt = customer.GetEmployeeidByContactNo();
                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < dt.Rows.Count; j++)
                                        {
                                            customer.UserId = Convert.ToInt32(dt.Rows[j]["UserId"]);
                                            dt1 = new DataTable();
                                            dt1 = customer.GetCustomeridByName();
                                            if (dt1 != null && dt1.Rows.Count > 0)
                                            {
                                                for (int i = 0; i < dt1.Rows.Count; i++)
                                                {
                                                    customer.CustomerId = Convert.ToInt32(dt1.Rows[i]["CustomerId"]);
                                                    customer.status = 0;
                                                    customer.AssignCustomerDaily();
                                                }
                                            }
                                            else
                                            {
                                                if (columnExists)
                                                {
                                                    IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Customer Name"], row["Time"], row["Task Detail"].ToString(), "Invalid Customer");
                                                }
                                                else
                                                {
                                                    IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Customer Name"], row["Time"], "Invalid Customer");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (columnExists)
                                        {
                                            IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Customer Name"], row["Time"], row["Task Detail"].ToString(), "Invalid Employee");
                                        }
                                        else
                                        {
                                            IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Customer Name"], row["Time"], "Invalid Employee");
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string a = ex.Message + " " + ex.StackTrace;
                                    if (columnExists)
                                    {
                                        IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Customer Name"], row["Time"], row["Task Detail"].ToString(), "Something went wrong");
                                    }
                                    else
                                    {
                                        IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Customer Name"], row["Time"], "Something went wrong");
                                    }
                                }
                            }
                            else
                            {
                                if (columnExists)
                                {
                                    IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Company Name"], row["Time"], row["Task Detail"].ToString(), "Invalid Date or Time");
                                }
                                else
                                {
                                    IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Company Name"], row["Time"], "Invalid Date or Time");
                                }
                            }
                        }
                        else
                        {
                            if (columnExists)
                            {
                                IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Customer Name"], row["Time"], row["Task Detail"].ToString(), "Invalid Contact No");
                            }
                            else
                            {
                                IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Customer Name"], row["Time"], "Invalid Contact No");
                            }
                        }
                    }
                    else
                    {
                        if (columnExists)
                        {
                            IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Customer Name"], row["Time"], row["Task Detail"].ToString(), "Empty Field not Allowed");
                        }
                        else
                        {
                            IncorrectDt.Rows.Add(row["Employee Name"], row["Employee Mobile No"], row["Customer Name"], row["Time"], "Empty Field not Allowed");
                        }
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
                        //btnSaveExcel.Enabled = false;
                        lblMsg.Text = "Data of the the below table not updated.Please check the data and try again";
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        GridView1.DataSource = IncorrectDt;
                        GridView1.DataBind();
                        //GridView1.Visible = false;
                    }
                    else
                    {
                        //.Enabled = false;
                        lblMsg.Text = "Excel uploaded Successfully";
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                }
                finally
                { }
            }
            catch (Exception)
            {
                lblMsg.Text = "Please upload excel sheet in correct format";
                lblMsg.Visible = true;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                dt = null;
                IncorrectDt = null;
            }

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }
        private bool ChkDateTime(string Time)
        {
            try
            {
                DateTime dt = DateTime.FromOADate(Convert.ToDouble(Time));
                return true;
            }
            catch
            {
                return false;
            }
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
            string filename = "AssignDailyCustomer.xlsx";
            Response.ContentType = "application/vnd.xlsx";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + "");
            Response.TransmitFile(Server.MapPath("~/Files/Format/" + filename));
            Response.End();
        }
    }
}
