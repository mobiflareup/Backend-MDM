using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.Infrastructure;

namespace MobiOcean.MDM.Web.Controller
{
    public class TA_LocationController : ApiController
    {
        CustomerBAL cust;
        LocationBAL gapi;
        DataTable dt, dt1;       
        [ActionName("GetCustomerId")]
        public string Get(int id)
        {
            string visitlat = "0", visitlong = "0", custlat = "0", custlong = "0", custid = "", customerid = "";
            gapi = new LocationBAL();
            try
            {
                cust = new CustomerBAL();
                cust.VisitDetailId = id;
                dt = cust.GetLocationByVisitDetailId();
                foreach (DataRow row in dt.Rows)
                {
                    visitlat = row["Latitude"].ToString();
                    visitlong = row["Longitude"].ToString();
                    cust.ClientId = 266;
                    dt1 = cust.GetCustomerdtl();
                    foreach (DataRow obj in dt1.Rows)
                    {
                        custlat = obj["Latitude"].ToString();
                        custlong = obj["Longitude"].ToString();                        
                        double distance = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(visitlat), Convert.ToDouble(visitlong), Convert.ToDouble(custlat), Convert.ToDouble(custlong));
                        if (distance < 250)
                        {
                            customerid = obj["CustomerId"].ToString();
                            if (!custid.Contains(customerid))
                            {
                                custid += customerid + ",";
                            }
                        }
                        else
                        {
                            //custid = "0";
                        }
                    }
                }
                custid = custid.TrimEnd(',');
                return custid;
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}
