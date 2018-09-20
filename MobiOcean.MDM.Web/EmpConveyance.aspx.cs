using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class EmpConveyance : Base
    {
        int ClientId, UserId, RoleId, DeptId;

        ConveyanceBAL coKmbal;



        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
            }

        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            int c = 0;
            string km = "";
            coKmbal = new ConveyanceBAL();
            c = ConveyKM.Rows.Count;
            string regexexpression = @"^[0-9]+(\.([0-9]{1,2})?)?$";
            foreach (GridViewRow rows in ConveyKM.Rows)
            {
                if (rows.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        km = ((TextBox)rows.FindControl("txtkm")).Text.Trim();
                        if (Regex.IsMatch(km, regexexpression))
                        {
                            //i = i + 1;
                            coKmbal.ClientId = ClientId;
                            coKmbal.UserId = Convert.ToInt32(((Label)rows.FindControl("lblId")).Text.Trim());
                            coKmbal.CreatedBy = UserId.ToString();
                            coKmbal.KM = Convert.ToDouble(km);
                            coKmbal.insertConveyanceKM();
                            MP1.Show();
                        }
                        else
                        {
                            lblerror.Text = "Some Rows have invalid data, Please change that datas!!!";
                            lblerror.Visible = true;
                            lblerror.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch (Exception) { }
                }
            }
        }

        protected void BindGrid()
        {
            coKmbal = new ConveyanceBAL();
            coKmbal.ClientId = ClientId;
            ConveyKM.DataSource = coKmbal.getfromConveyanceKm();
            ConveyKM.DataBind();
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            mpcancel.Show();
        }
        protected void btncancelok_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDashBoard.aspx");
        }
        protected void btncancelcan_Click(object sender, EventArgs e)
        {
            mpcancel.Hide();
        }
        protected void ok_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpConveyance.aspx");
        }
    }
}