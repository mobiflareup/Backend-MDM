using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MobiOcean.MDM.Web
{
    public class LoginBase : Page
    {
        public void MaintainSession(DataTable dt, string IsPayment = "", string Payment = "")
        {

            try
            {
                Session["UserId"] = dt.Rows[0]["UserId"].ToString();
                Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                Session["Role"] = dt.Rows[0]["RoleId"];
                Session["ClientId"] = dt.Rows[0]["ClientId"];
                if (!string.IsNullOrEmpty(dt.Rows[0]["DeptId"].ToString()))
                {
                    Session["DeptId"] = dt.Rows[0]["DeptId"];
                }
                else
                {
                    Session["DeptId"] = 0;
                }
                try
                {
                    if (string.IsNullOrEmpty(IsPayment))
                    {
                        bool paySession = false;
                        try
                        {
                            paySession = string.IsNullOrEmpty(Session["MOOrderDetail"].ToString()) ? false : true;
                        }
                        catch (Exception)
                        {
                            paySession = false;
                        }
                        if (!paySession)
                        {
                            string redirectpath = "";
                            if ((dt.Rows[0]["RoleId"]).ToString() == "1")  //------ Super Admin -------
                            {
                                redirectpath = "/SADashBoard";
                            }
                            else if ((dt.Rows[0]["RoleId"]).ToString() == "3" || (dt.Rows[0]["RoleId"]).ToString() == "2")  //------  Admin -------
                            {
                                redirectpath = "/AdminDashBoard";
                            }
                            else if ((dt.Rows[0]["RoleId"]).ToString() == "4")  //------  User -------
                            {
                                redirectpath = "/UserDashBoard";
                            }
                            Response.Redirect("~" + redirectpath);
                        }
                        else
                        {
                            Response.Redirect("/web/MobiPayment");
                        }
                    }
                    else
                    {
                        Response.Redirect("/web/FinalPayment?" + Payment);
                    }

                }
                finally
                {
                }
            }
            finally
            {
            }
        }
    }
}