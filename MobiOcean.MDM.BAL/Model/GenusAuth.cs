using MobiOcean.MDM.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for GenusAuth
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class GenusAuth
    {
        public string UserCode { get; set; }

        public string MobileNo { get; set; }
        public string otp { get; set; }
        public string BaseUrl = "https://genus.mobiocean.com/api/";
        public GenusAuth()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string Registration()
        {
            try
            {
                GenusAuthCredentials credentials = new GenusAuthCredentials();
                credentials.clientcode = "Genus"; //"info@gingerbox.com";
                credentials.usercode = UserCode; //"Admin@123";
                credentials.mobileno = MobileNo;
                var jsondata = new JavaScriptSerializer().Serialize(credentials);
                var client = new RestClient(BaseUrl + "Registration/Emp", HttpVerb.POST, jsondata.ToString(), 1);
                var json = client.MakeRequest();
                GenusAuthResult deserialize = JsonConvert.DeserializeObject<GenusAuthResult>(json);
                if (deserialize.responseCode == "1")
                {
                    return deserialize.appID;
                }
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public string ChkOTP()
        {
            try
            {
                ValidateOTP credentials = new ValidateOTP();
                credentials.mobileno = MobileNo;
                credentials.otp = otp;
                var jsondata = new JavaScriptSerializer().Serialize(credentials);
                var client = new RestClient(BaseUrl + "Registration/ValidateOTP", HttpVerb.POST, jsondata.ToString(), 1);
                var json = client.MakeRequest();
                GenusAuthResult deserialize = JsonConvert.DeserializeObject<GenusAuthResult>(json);
                if (deserialize.responseCode == "1")
                {
                    return deserialize.appID;
                }
                return "0";
            }
            catch (Exception)
            {
                return "0";
            }
        }


    }
    public class GenusAuthCredentials
    {

        public string clientcode { get; set; }

        public string usercode { get; set; }

        public string mobileno { get; set; }

    }
    public class GenusAuthResult
    {

        public string responseCode { get; set; }
        public string appID { get; set; }
    }
    public class ValidateOTP
    {
        public string mobileno { get; set; }
        public string otp { get; set; }
    }
}