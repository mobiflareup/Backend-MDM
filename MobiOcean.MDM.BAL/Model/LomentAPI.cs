using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.Data;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.Infrastructure;
/// <summary>
/// Summary description for LomentAPI
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.Model
{
    public class LomentAPI
    {
        LomentBAL loment;
        DigestHttpWebRequest req;

        private string ServerUrl = "https://api-sthithi.loment.net/"; //https://tapi-sthithi.loment.net
        private string ParentId = "8";
        private string partnerUser = "test";
        private string partnerPwd = "34c285b25ac62f9472265d1e41f8a77f5d2382f6";
        public int IsPeanut { get; set; }
        public int IsCashew { get; set; }
        public int IsWalnut { get; set; }
        public byte[] Postdata { get; set; }        
        public string password { get; set; }
        public string primary_email { get; set; }        
        public int userid { get; set; }
        public int AdminUserId { get; set; }
        public int lomentUserId { get; set; }
        public int clientid { get; set; }
        public int isadmin { get; set; }
        public string keys { get; set; }
        public string username { get; set; }        
        public int noofusers { get; set; }
        public string bill_id { get; set; }
        public string s_username { get; set; }        
        public int feature_id { get; set; }
        public string device_id { get; set; }
        public int count { get; set; }
        public Dictionary<string, string> pfid { get; set; }        
        public LomentAPI()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        string response = string.Empty;

        public string RegisterCompanyAdmin()
        {
            try
            {
                // string data = "name=" + name + "&password=" + password + "&primary_email=" + primary_email + "&primary_mobile_number=" + primary_mobile_number + "&country_abbrev=IN" + "&partner_id=1";
                Uri uri = new Uri(ServerUrl + "user/register/");                
                req = new DigestHttpWebRequest(partnerUser,partnerPwd);
                req.Method = "POST";
                req.ContentType = "application/Json";
                //req.PostData = Encoding.UTF8.GetBytes(data);
                req.PostData = Postdata;
                using (HttpWebResponse webResponse = req.GetResponse(uri))
                {
                    using (Stream responseStream = webResponse.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader streamReader = new StreamReader(responseStream))
                            {
                                response = streamReader.ReadToEnd();
                            }
                        }
                        LomentResponse deserialize = JsonConvert.DeserializeObject<LomentResponse>(response);

                        if (deserialize.status == 0)
                        {
                            int luserid = deserialize.userid;
                            loment = new LomentBAL();
                            loment.userid = userid;
                            loment.clientid = clientid;
                            loment.isadmin = isadmin;
                            loment.lomentuserid = luserid;
                            loment.lomentusername = primary_email;
                            loment.regpassword = password;                            
                            int res = loment.InsertLomentUser();
                            if (res > 0)
                            {
                                return deserialize.comments;

                            }
                            else
                            {
                                return "0";
                            }
                        }
                        else
                        {
                            return deserialize.comments;
                        }
                    }
                }
            }
            catch (Exception)
            { return "0"; }

        }
        public string BuyLicense()
        {
            
            int lomentid = 0;
            string lomentuname = "";
            Uri uri = new Uri(ServerUrl + "user/" + username + "/payment/new/gateway/partner/" + ParentId + "/dopayment");            
            req = new DigestHttpWebRequest(partnerUser, partnerPwd);
            req.Method = "POST";
            req.ContentType = "application/Json";
            req.PostData = Postdata;
            using (HttpWebResponse webResponse = req.GetResponse(uri))
            using (Stream responseStream = webResponse.GetResponseStream())
            {
                if (responseStream != null)
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        response = streamReader.ReadToEnd();
                    }
                }
            }
            RootObject deserialize = JsonConvert.DeserializeObject<RootObject>(response);
            loment = new LomentBAL();
            loment.userid = userid;
            DataTable dt = new DataTable();
            dt = loment.GetLomentUserByUserId();
            if (dt.Rows.Count > 0)
            {
                lomentid = Convert.ToInt32(dt.Rows[0]["LomentId"].ToString());
                lomentuname = dt.Rows[0]["LomentUserName"].ToString();
                loment = new LomentBAL();
                loment.lomentid = lomentid;
                loment.clientid = clientid;
                loment.lomentusername = lomentuname;
                loment.featureid = feature_id;                
                loment.subscriptionstarttime = Convert.ToDateTime(deserialize.payment_details.subscription_start_date);
                loment.subscriptionendtime = Convert.ToDateTime(deserialize.payment_details.subscription_end_date);
                loment.noofusers = noofusers; //no info 
                loment.paymenttype = deserialize.type;
                loment.paymentdetails = deserialize.payment_details.purchase_data.ToString();
                loment.billdetails = deserialize.bill_details.id;
                int res = loment.InsertLomentSubscription();
                if (res > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
        public Dictionary<string, string> AllocateUser()
        {
            string keys = "";

            Dictionary<string, string> dict = new Dictionary<string, string>();
            // string data = "username=" + username + "&bill_id=" + bill_id + "&s_username=" + s_username + "&peanut_devices=" + peanut_devices + "&walnut_devices=" + walnut_devices + "&cashewnut_devices=" + cashewnut_devices + "&walnut_outlook_plugin_devices=" + walnut_outlook_plugin_devices;
            Uri uri = new Uri(ServerUrl + "user/" + username + "/subscription/bill/" + bill_id + "/allocation/" + s_username);
            req = new DigestHttpWebRequest(partnerUser, partnerPwd);
            req.Method = "POST";
            req.ContentType = "application/Json";
            req.PostData = Postdata;
            // req.PostData = Encoding.UTF8.GetBytes(data);
            using (HttpWebResponse webresponse = req.GetResponse(uri))
            using (Stream streamresponse = webresponse.GetResponseStream())
            {
                if (streamresponse != null)
                {
                    using (StreamReader streamReader = new StreamReader(streamresponse))
                    {
                        response = streamReader.ReadToEnd();
                    }
                }
                LomentResponse deserialize = JsonConvert.DeserializeObject<LomentResponse>(response);
                keys = deserialize.key;
                if (deserialize.status == 0)
                {
                    loment = new LomentBAL();
                    loment.keys = keys;
                    loment.adminUserId = AdminUserId;
                    loment.lomentuserid = lomentUserId;
                    loment.lomentusername = s_username;
                    loment.featureid = feature_id;
                    loment.clientid = clientid;
                    int res = 0;
                    if (feature_id == 4)
                    {
                        if (IsPeanut == 1)
                        {
                            loment.featureid = 1;
                            res = loment.InsertLomentUserFeature();
                        }
                        if (IsWalnut == 1)
                        {
                            loment.featureid = 2;
                            res = loment.InsertLomentUserFeature();
                        }
                        if (IsCashew == 1)
                        {
                            loment.featureid = 3;
                            res = loment.InsertLomentUserFeature();
                        }
                    }
                    else
                    {
                        res = loment.InsertLomentUserFeature();
                    }
                    if (res > 0)
                    {
                        dict.Add("1", keys);
                        return dict;
                    }
                    else
                    {
                        dict.Add("0", "Something Went Wrong");
                        return dict;
                    }
                }
                else
                {
                    dict.Add("2", deserialize.comments);
                    return dict;
                }
            }
        }
        public string DeviceUnlink()
        {
            try
            {
                //int lomentid = 0;
                //string data = "username=" + username + "&feature_id=" + feature_id + "&device_id=" + device_id;
                Uri uri = new Uri(ServerUrl + "user/" + username + "/subscription/" + feature_id + "/device/" + device_id + "/unlink");
                req = new DigestHttpWebRequest(partnerUser, partnerPwd);
                req.Method = "GET";
                req.ContentType = "application/Json";
                using (HttpWebResponse webresponse = req.GetResponse(uri))
                using (Stream streamresponse = webresponse.GetResponseStream())
                {
                    if (streamresponse != null)
                    {
                        using (StreamReader streamReader = new StreamReader(streamresponse))
                        {
                            response = streamReader.ReadToEnd();
                        }
                    }                    
                    LomentResponse deserialize = JsonConvert.DeserializeObject<LomentResponse>(response);
                    if (deserialize.status == 0)
                    {
                       
                        return "Device unlinked Successfully.";                       
                    }
                    else
                    {
                        return deserialize.comments;
                    }
                }
            }
            catch
            {
                return "Something went wrong. Please contact to our support team.";
            }
        }
        public string UserUnlink()
        {
            //string data = "username=" + username + "&bill_id=" + bill_id + "&s_username=" + s_username + "&feature_id=" + feature_id + "&key=" + keys + "&count=" + count;
            Uri uri = new Uri(ServerUrl + "user/" + username + "/subscription/bill/" + bill_id + "/" + feature_id + "/allocation/update/" + s_username);
            req = new DigestHttpWebRequest(partnerUser, partnerPwd);
            req.Method = "POST";
            req.ContentType = "application/Json";
            req.PostData = Postdata;
            using (HttpWebResponse webresponse = req.GetResponse(uri))
            using (Stream streamresponse = webresponse.GetResponseStream())
                if (streamresponse != null)
                {
                    using (StreamReader streamReader = new StreamReader(streamresponse))
                    {
                        response = streamReader.ReadToEnd();
                    }
                }
            LomentResponse deserialize = JsonConvert.DeserializeObject<LomentResponse>(response);
            if (deserialize.status == 0)
            {
                loment = new LomentBAL();
                loment.keys = keys;
                loment.adminUserId = AdminUserId;
                loment.lomentuserid = lomentUserId;
                loment.lomentusername = s_username;
                loment.featureid = feature_id;
                loment.clientid = clientid;
                int res = 0;
                if (count == 0)
                {
                    res = loment.UnLinkUser();
                }
                else
                {
                    res = loment.InsertLomentUserFeature();
                }
                if (res > 0)
                {
                    return "Status updated successfully.";
                }
                else
                {
                    return "Something went wrong. Please contact our support team.";
                }
            }
            else
            {
                return deserialize.comments;
            }
        }
        public Dictionary<string, string> GetDevice(string Pname)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {

                Uri uri = new Uri(ServerUrl + "user/" + username + "/subscription/feature/all/all/device/all");
                req = new DigestHttpWebRequest(partnerUser, partnerPwd);
                req.Method = "POST";
                req.ContentType = "application/Json";
                req.PostData = Postdata;
                using (HttpWebResponse webresponse = req.GetResponse(uri))
                using (Stream streamresponse = webresponse.GetResponseStream())
                {
                    if (streamresponse != null)
                    {
                        using (StreamReader streamReader = new StreamReader(streamresponse))
                        {
                            response = streamReader.ReadToEnd();
                        }
                    }                    
                    DeviceList deserialize = JsonConvert.DeserializeObject<DeviceList>(response);
                    if (deserialize.status == 0)
                    {
                        foreach (var obj in deserialize.devices)
                        {
                            foreach (var obj1 in obj.Value.products)
                            {
                                if (Pname == obj1.Key)
                                {
                                    dict.Add("1", obj.Key);
                                    break;
                                }
                            }
                        }
                        return dict;
                    }
                    else
                    {
                        dict.Add("2", deserialize.comments);
                        return dict;
                    }

                }
            }
            catch
            {
                dict.Add("3", "No Device Found.");
                return dict;
            }
        }
    }
}
