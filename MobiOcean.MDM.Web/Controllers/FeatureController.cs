using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;

namespace MobiOcean.MDM.Web.Controller
{
    public class FeatureController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;
        FeatureBAL featurebal, functionnamelist;

        /************Webservicename*******************/
        [ActionName("featureList")]
        public List<FeatureBAL> Get(string AppId, int IsForUpdate = 0)
        {
            List<FeatureBAL> lstf = new List<FeatureBAL>();
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
                else
                {
                    DeviceId = 0;
                }
            }
            catch (Exception)
            {
                DeviceId = 0;
            }
            if (DeviceId > 0)
            {
                featurebal = new FeatureBAL();
                featurebal.APPId = AppId;
                if (IsForUpdate == 0)
                {
                    dt = featurebal.GetFeature();
                }
                else
                {
                    dt = featurebal.GetFeatureUpdate();
                }

                foreach (DataRow rows in dt.Rows)
                {
                    functionnamelist = new FeatureBAL
                    {
                        FeatureId = Convert.ToInt32(rows["FeatureId"]),
                        FeatureCode = rows["FeatureCode"].ToString(),
                        FeatureName = rows["FeatureName"].ToString(),
                        FeatureDesc = rows["FeatureDesc"].ToString(),
                        Status = Convert.ToInt32(rows["Status"]),
                        CategoryId = Convert.ToInt32(rows["CategoryId"]),
                        CategoryName = rows["CategoryName"].ToString(),
                        CategoryCode = rows["CategoryCode"].ToString(),
                    };
                    lstf.Add(functionnamelist);
                }
            }
            return lstf;

        }//end of try keyword


    }
}
 
    
    
