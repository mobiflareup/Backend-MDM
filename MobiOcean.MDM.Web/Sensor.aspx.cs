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
    public partial class Sensor : Base
    {
        //SupportBAL support;
        DeptBAL dept;
        int ClientId, UserId, RoleId, DeptId;
        DataTable IncorrectDt, dt;
        string Msgtxt, MobileNo;
        SendSMSBAL sms;
        SensorBAL sensor;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindDropdown();
            }
        }
        private void BindDropdown()
        {
            dept = new DeptBAL();
            try
            {
                ListItem li1 = new ListItem("--- Select Branch ---", "0");
                dtBranch.Items.Clear();
                dtBranch.Items.Add(li1);
                dept.ClientId = ClientId;
                dtBranch.DataSource = dept.GetBranchName();
                dtBranch.DataTextField = "BranchName";
                dtBranch.DataValueField = "BranchId";
                dtBranch.DataBind();


                ListItem li2 = new ListItem("--- Select Department ---", "0");
                dtDepartment.Items.Clear();
                dtDepartment.Items.Add(li2);
                dept.ClientId = ClientId;
                dtDepartment.DataSource = dept.GetDptNameDDL();
                dtDepartment.DataTextField = "DeptName";
                dtDepartment.DataValueField = "DeptId";
                dtDepartment.DataBind();


            }
            catch (Exception)
            {

            }
            finally
            {
                dept = null;

            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (ChkValidation())
            {
                if (dtBranch.SelectedIndex > 0 && dtDepartment.SelectedIndex > 0)
                {
                    int result = savesensor(txtsen.Text, txtdesc.Text, txtBssid.Text.Trim(), txtSsid.Text.Trim(), txtPwd.Text.Trim());
                    if (result > 0)
                    {
                        lblresult.Text = "Added Successfully!";
                        msgalert();
                        lblresult.ForeColor = System.Drawing.Color.Green;
                        Reset();
                    }
                    else
                    {
                        lblresult.Text = "Already registered!";
                        lblresult.ForeColor = System.Drawing.Color.Red;
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
                lblresult.Text = "Fill all mandatory fields.";
                lblresult.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }

        private int savesensor(string SensorName, string Desc, string BSSId, string SSId, string Pwd)
        {
            sensor = new SensorBAL();
            sensor.BranchId = Convert.ToInt32(dtBranch.SelectedValue.ToString());
            sensor.ClientId = ClientId;
            sensor.DepartmentId = Convert.ToInt32(dtDepartment.SelectedValue.ToString());
            sensor.SensorName = SensorName;
            sensor.Descripition = Desc;
            sensor.BSSID = BSSId;
            sensor.SSID = SSId;
            sensor.createdby = UserId.ToString();
            sensor.Password = Pwd;
            int result = Convert.ToInt32(sensor.insertSensor());
            return result;
        }
        protected void btnForm_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            Reset();
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }
        protected void Reset()
        {
            txtBssid.Text = txtdesc.Text = txtPwd.Text = txtsen.Text = txtSsid.Text = string.Empty;
        }
        protected bool ChkValidation()
        {
            if (string.IsNullOrEmpty(txtBssid.Text.Trim()) || string.IsNullOrEmpty(txtPwd.Text.Trim()) || string.IsNullOrEmpty(txtsen.Text.Trim()) || string.IsNullOrEmpty(txtSsid.Text.Trim()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            try
            {
                string value = cell.CellValue.InnerText;
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
                string value = cell.CellValue.InnerText;
                return value;
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
                DataTable dtgv = new DataTable();
                GridView1.DataSource = dtgv;
                GridView1.DataBind();
                if (FileUpload1.HasFile)
                {
                    if (dtBranch.SelectedValue != "0" && dtDepartment.SelectedValue != "0")
                    {
                        string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                        string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);


                        string FilePath = Server.MapPath(Constant.FolderPath + FileName);
                        FileUpload1.SaveAs(FilePath);
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
                        lblresult.Text = "Please select Branch & Department ";
                        lblresult.ForeColor = System.Drawing.Color.Red;
                    }



                }
                else
                {
                    lblresult.Text = "Please Choose file.";
                    lblresult.Visible = true;
                    lblresult.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception)
            {
                lblresult.Text = "Please Change the file name.This file is already exist.";
                lblresult.Visible = true;
                lblresult.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
        }


        protected void chkexcel(DataTable dt)
        {
            try
            {
                IncorrectDt = new DataTable();
                IncorrectDt.Columns.Add("Sensor Name");
                IncorrectDt.Columns.Add("Description");
                IncorrectDt.Columns.Add("BSSID");
                IncorrectDt.Columns.Add("SSID");
                IncorrectDt.Columns.Add("Password");
                IncorrectDt.Columns.Add("Status");

                DataRow[] rows = dt.Select(); //"Mobile_No='" + lblmobileno.Text.Trim() + "'");
                foreach (DataRow row in rows)
                {
                    if (!string.IsNullOrEmpty(row["Sensor Name"].ToString()) && !string.IsNullOrEmpty(row["BSSID"].ToString()) && !string.IsNullOrEmpty(row["SSID"].ToString()) && !string.IsNullOrEmpty(row["Password"].ToString()))
                    {
                        try
                        {
                            int res = savesensor(row["Sensor Name"].ToString(), row["Description"].ToString(), row["BSSID"].ToString(), row["SSID"].ToString(), row["Password"].ToString());
                            if (res > 0)
                            {
                            }
                            else
                            {
                                IncorrectDt.Rows.Add(row["Sensor Name"], row["Description"], row["BSSID"], row["SSID"], row["Password"], "Already exists");
                            }
                        }
                        catch (Exception)
                        {
                            IncorrectDt.Rows.Add(row["Sensor Name"], row["Description"], row["BSSID"], row["SSID"], row["Password"], "Empty field not allowed");
                        }
                    }
                    else
                    {
                        IncorrectDt.Rows.Add(row["Sensor Name"], row["Description"], row["BSSID"], row["SSID"], row["Password"], "Empty field not allowed");
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

                        lblresult.Text = "The following table data not updated.Please check the data and try again";
                        lblresult.Visible = true;
                        lblresult.ForeColor = System.Drawing.Color.Red;
                        GridView1.DataSource = IncorrectDt;
                        GridView1.DataBind();
                        //GridView1.Visible = false;
                    }
                    else
                    {
                        lblresult.Text = "Excel uploaded Successfully";
                        lblresult.Visible = true;
                        lblresult.ForeColor = System.Drawing.Color.Green;
                    }
                }
                catch (Exception)
                { }
            }

            catch (Exception)
            {
                lblresult.Text = "Please upload excel sheet in correct format";
                lblresult.Visible = true;
                lblresult.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                dt = null;
                IncorrectDt = null;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

        }


        protected void btncancelsensor_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = -1;
            Reset();
        }
        private void msgalert()
        {
            dt = new DataTable();
            sensor = new SensorBAL();
            sensor.ClientId = ClientId;
            dt = sensor.GetmobileNos();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MobileNo = dt.Rows[i]["MobileNo"].ToString();

                try
                {
                    if (!string.IsNullOrEmpty(MobileNo))
                    {
                        Msgtxt = "GBox set as WP7";
                        sms = new SendSMSBAL();
                        sms.sendFinalSMS(MobileNo, Msgtxt, ClientId);
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}