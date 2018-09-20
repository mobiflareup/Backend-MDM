using System;
using System.Net;
using System.Text;
using System.IO;

namespace MobiOcean.MDM.Infrastructure
{
    public class GCM
    {
        int waitingTime = 3, TimeToLive;


        public string AndroidPush(string RegistrationID, string SenderID, string appID, string Message)
        {
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", appID));
            tRequest.Headers.Add(string.Format("Sender: id={0}", SenderID));

            //Data post to server 
            TimeToLive = waitingTime * 60;
            string postData = "collapse_key=1&time_to_live=" + TimeToLive + "&delay_while_idle=0&data.message="
                             + Message + "&data.time=" + DateTime.UtcNow.AddMinutes(330).ToString() + "&registration_id="
                             + RegistrationID + "";

            //notification_key
            Console.WriteLine(postData);
            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;
            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();   //Get response from GCM server.
            //Assigning GCM response to Label text 
            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }
        public string AndroidPushForIOS(string RegistrationID, string SenderID, string appID, string PostData)
        {
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", appID));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SenderID));

            //Data post to server 
            TimeToLive = waitingTime * 60;
            //string postData = "collapse_key=1&time_to_live=" + TimeToLive + "&delay_while_idle=0&data.message="
            //                 + value + "&data.time=" + Constant.CurrentDateTime.ToString() + "&registration_id="
            //                 + regId + "";

            //notification_key
            Console.WriteLine(PostData);
            Byte[] byteArray = Encoding.UTF8.GetBytes(PostData);
            tRequest.ContentLength = byteArray.Length;
            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();   //Get response from GCM server.
            //Assigning GCM response to Label text 
            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }

    }
}
