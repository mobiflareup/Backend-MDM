using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class FirstLoginChanges : System.Web.UI.Page
    {
        AnuSearch srch;
        DataTable dt, dt1;
        ClientBAL clienttbl;
        int ClientId, UserId, RoleId, DeptId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["ClientId"] == null || Session["UserId"] == null || Session["Role"] == null)
            {
                Response.Redirect(Constant.MobiURL);
                //Response.Redirect("~/login.aspx");
            }
            else
            {

                ClientId = Convert.ToInt32(Session["ClientId"].ToString());
                UserId = Convert.ToInt32(Session["UserId"].ToString());
                RoleId = Convert.ToInt32(Session["Role"].ToString());
                DeptId = Convert.ToInt32(Session["DeptId"].ToString());
                clienttbl = new ClientBAL();
                dt = new DataTable();
                clienttbl.ClientId = ClientId;
                dt = clienttbl.GetClientByClientId();
                if (dt.Rows[0]["IsFirstLogin"].ToString() != "1")
                {
                    Response.Redirect("~/Subscribe");
                }
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }


        }

        protected void BindGrid()
        {
            srch = new AnuSearch();
            dt = new DataTable();
            dt = srch.LoginChanges();
            CategoryLt.DataSource = dt;
            CategoryLt.DataBind();
        }

        protected void ApplyChanges_Click(object sender, EventArgs e)
        {
            srch = new AnuSearch();
            dt = new DataTable();
            dt = srch.LoginChanges();
            string CategoryId = "";
            for (int idx = 0; idx < dt.Rows.Count; idx++)
            {
                CheckBox chk = (CheckBox)CategoryLt.Rows[idx].FindControl("CatyEnbl");
                Label CatyId = (Label)CategoryLt.Rows[idx].FindControl("lblCatyId");
                if (chk.Checked == true)
                {
                    CategoryId = CategoryId + CatyId.Text + ",";
                }
            }
            clienttbl = new ClientBAL();
            string ProductKey = "";
            string ExpiryDate = DateTime.UtcNow.AddDays(Constant.addTrialDays).ToString(Constant.DateFormat);
            clienttbl.UpdateProductKey(ClientId, out ProductKey);

            dt1 = new DataTable();
            clienttbl.ClientId = ClientId;
            dt1 = clienttbl.GetClientByClientId();
            SubscribeBAL Subscribe = new SubscribeBAL();
            Subscribe.CategoryIdList = CategoryId.TrimEnd(',');
            Subscribe.ClientId = ClientId;
            Subscribe.ProductKey = ProductKey;
            Subscribe.TotalAmount = 0;
            Subscribe.TotalPaid = 0;
            Subscribe.UserId = UserId;
            Subscribe.EmailId = dt1.Rows[0]["EmailId"].ToString();
            Subscribe.Address = dt1.Rows[0]["Address"].ToString();
            foreach (string obj in CategoryId.TrimEnd(',').Split(','))// Constant.CategoryIds.
            {
                Subscribe.categoryDuration += Constant.addTrialDays + ",";
                Subscribe.categoryNoOfLicense += 10 + ",";
                Subscribe.CategoryTotalAmount += 0.00 + ",";
                Subscribe.PricePerUnit += 0.00 + ",";
            }
            Subscribe.categoryDuration = Subscribe.categoryDuration.TrimEnd(',');
            Subscribe.categoryNoOfLicense = Subscribe.categoryNoOfLicense.TrimEnd(',');
            Subscribe.CategoryTotalAmount = Subscribe.CategoryTotalAmount.TrimEnd(',');
            Subscribe.PricePerUnit = Subscribe.PricePerUnit.TrimEnd(',');
            int res = Subscribe.InsertSubscriptionDtls();
            if (res > 0)
            {
                res = clienttbl.ChkProductKeybysub3(ClientId, ProductKey);
            }
            Response.Redirect("~/AdminDashBoard");
        }
    }
}