using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LomentResponse
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class LomentResponse
    {
        public int status { get; set; }       
        public string comments { get; set; }       
        public int userid { get; set; }            
        public string key { get; set; }
    }

   
    public class DeviceList
    {
        public int status { get; set; }
        public string comments { get; set; }
        public Dictionary<string, devicename> devices { get; set; }
    }

    public class devicename
    {
        public string name { get; set; }
        public Dictionary<string, products> products { get; set; }
    }
    public class products
    {
        public string subscription_type { get; set; }
        public string status { get; set; }
        public string subscription_start_date { get; set; }
        public string subscription_end_date { get; set; }
        public string amount { get; set; }
        public Dictionary<string, string> purchase_data { get; set; }
        public string type { get; set; }
        public long utc_timestamp { get; set; }        
    }    

    public class PaymentDetails
    {
        public string id { get; set; }
        public int payment_gateway_id { get; set; }
        public string creation_date { get; set; }
        public string last_update_date { get; set; }
        public int status { get; set; }
        public string user_id { get; set; }
        public string subscription_start_date { get; set; }
        public string subscription_end_date { get; set; }
        public string amount { get; set; }
        public object purchase_data { get; set; }        
    }

    public class BillDetails
    {
        public string BillId { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string user_id { get; set; }
        public string payment_id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string creation_date { get; set; }
        public string last_update_date { get; set; }
        public int status { get; set; }
        public string renew_with_bill_id { get; set; }
        public string type { get; set; }
    }

    public class RootObject
    {
        public int status { get; set; }
        public string comments { get; set; }
        public PaymentDetails payment_details { get; set; }
        public BillDetails bill_details { get; set; }
        public string subscription_start_date { get; set; }
        public string subscription_end_date { get; set; }
        public object purchase_data { get; set; }       
        public string type { get; set; }
        public int utc_timestamp { get; set; }
    }   
}

