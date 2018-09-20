using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using System.Configuration;
using System.Threading.Tasks;
//using System.Net.Http;

namespace MobiOcean.MDM.Infrastructure
{
    public class SendSMS
    {

        public string sendMsgUsingSMS(string text, string ContactNo, int ClientId, string res)
        {
            string[] country = res.Split('~');
            if (country.Length == 2)
            {
                //text = text.Replace("&", "and");
                text = WebUtility.UrlEncode(text);//HttpContent.Current.Server.UrlEncode(text);
                string Url = country[1].Replace("ContactNo", country[0] + ContactNo).Replace("text", text);
                //string Url = "http://alerts.sinfini.com/api/web2sms.php?workingkey=A408795cc8a6bb77a23dd04d693e30700&to=" + ContactNo + "&sender=Ginger&message=" + text + "";
                var http = WebRequest.Create(Url);
                var response = http.GetResponse();
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                res = content.ToString().Trim();
                return res;
            }
            return "0";

        }
        public async Task sendMsgUsingSMSAsync(string text, string ContactNo, int ClientId, string res)
        {

            //text = text.Replace("&", "and");
            text = WebUtility.UrlEncode(text);
            string Url = res.Replace("ContactNo", ContactNo).Replace("text", text);
            //string Url = "http://alerts.sinfini.com/api/web2sms.php?workingkey=A408795cc8a6bb77a23dd04d693e30700&to=" + ContactNo + "&sender=Ginger&message=" + text + "";
            var http = WebRequest.Create(Url);
            var response = await http.GetResponseAsync();
            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();
            res = content.ToString().Trim();
        }
     

    }
}
