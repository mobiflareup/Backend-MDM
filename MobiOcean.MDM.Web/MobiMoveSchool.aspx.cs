using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class MobiMoveSchool1 : Base
    {
        int ClientId, UserId, RoleId, DeptId, CategoryId = 16;
        FeatureBAL feature;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClientId = Convert.ToInt32(Session["ClientId"].ToString());
            UserId = Convert.ToInt32(Session["UserId"].ToString());
            RoleId = Convert.ToInt32(Session["Role"].ToString());
            DeptId = Convert.ToInt32(Session["DeptId"].ToString());
            if (chkIsApplicable())
            {
                if (Request.QueryString["Id"] != null && Request.QueryString["Id"].ToString() == "Invalid")
                {
                    lblMsg.Text = "You are not a authorized person to access.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (RoleId < 3)
                    {
                        DataTable dt = new DataTable();
                        UserBAL UsrBal = new UserBAL();
                        UsrBal.UserId = UserId;
                        dt = UsrBal.GetUserDtlByUserId();
                        Credentials credentials = new Credentials();
                        credentials.EmailID = dt.Rows[0]["EmailID"].ToString(); //"info@gingerbox.com";
                        credentials.Password = dt.Rows[0]["Password"].ToString(); //"Admin@123";
                        var jsondata = new JavaScriptSerializer().Serialize(credentials);
                        var client = new RestClient(Constant.MobiMoveSchool + "api/User/GenerateAuthenticationKey", HttpVerb.POST, jsondata.ToString(), 1);
                        var json = client.MakeRequest();
                        if (json.IndexOf('"') == 0)
                        {
                            json = json.Substring(1);
                        }
                        if (json.IndexOf('"') > 0)
                        {
                            json = json.Substring(0, json.IndexOf('"'));
                        }
                        if (json != "Sorry! authentication failed.")
                        {
                            Response.Redirect(Constant.MobiMoveSchool + "Account/CheckAuthentication?authkey=" + json);
                        }
                        else
                        {
                            lblMsg.Text = json;
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "You are not a authorized person to access.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                lblMsg.Text = string.Empty;
            }

        }

        protected bool chkIsApplicable()
        {
            feature = new FeatureBAL();
            dt = new DataTable();
            feature.ClientId = ClientId;
            feature.SolutionId = "3";
            dt = feature.GetActiveSolutions();
            if (dt != null)
            {
                int categoryid;
                foreach (DataRow row in dt.Rows)
                {
                    categoryid = Convert.ToInt32(row["CategoryId"].ToString());
                    if (CategoryId == categoryid && !string.IsNullOrEmpty(row["SubscriptionId"].ToString()))
                    {
                        return true;
                    }
                }
            }
            return true;
        }
    }
}
