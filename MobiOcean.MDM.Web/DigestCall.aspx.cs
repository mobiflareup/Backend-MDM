using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobiOcean.MDM.Web
{
    public partial class DigestCall : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //private string WebServiceCall(string methodname)
            //    {
            try
            {
                //string response = string.Empty;

                //Uri uri = new Uri("https://tapi-sthithi.loment.net/user/register/");
                ////Uri uri = new Uri("https://tapi-sthithi.loment.net/user/sourabh.agg2009@gmail.com/authenticate");
                ////Uri uri = new Uri("https://tapi-sthithi.loment.net/user/saurabh@gingerboxmobility.com/payment/new/gateway/partner/1/dopayment");
                //DigestHttpWebRequest req = new DigestHttpWebRequest("test", "34c285b25ac62f9472265d1e41f8a77f5d2382f6");
                //req.Method = "POST";
                //req.ContentType = "application/Json";
                //req.PostData = Encoding.UTF8.GetBytes("name=Saurabh&password=Admin@123&primary_email=saurabh@gingerboxmobility.com&primary_mobile_number=919602904048&country_abbrev=IN&partner_id=1");
                ////req.PostData = Encoding.UTF8.GetBytes("password=Admin@123");
                ////req.PostData = Encoding.UTF8.GetBytes(Server.UrlDecode("subscription_start_date=2016-08-26&subscription_end_date=2016-10-30&purchase_data={\"1\":\"1\",\"2\":\"1\",\"3\":\"1\"}&type=trial"));
                ////                         + regId + "";
                ////            {"name":"arun", "password":"123456", "primary_email":"arun@gmail.com", "primary_mobile_number":"1234567890",
                //// "country_abbrev" : "IN",
                ////"partner_id": "1"
                ////}
                //using (HttpWebResponse webResponse = req.GetResponse(uri))
                //using (Stream responseStream = webResponse.GetResponseStream())
                //{
                //    if (responseStream != null)
                //    {
                //        using (StreamReader streamReader = new StreamReader(responseStream))
                //        {
                //            response = streamReader.ReadToEnd();
                //            Response.Write(response);
                //        }
                //    }
                //}
                //return response;
            }
            catch (WebException caught)
            {
                throw new WebException(string.Format("Exception in WebServiceCall: {0}", caught.Message));
            }
            catch (Exception caught)
            {
                throw new Exception(string.Format("Exception in WebServiceCall: {0}", caught.Message));
            }
            //    }
        }
    }
}