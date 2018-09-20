using System;
using System.Xml;
using System.Data;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json.Linq;
using GoogleMaps.LocationServices;

namespace MobiOcean.MDM.Infrastructure
{
    public class GoogleAPI
    {

        XmlTextReader xReader;
        bool element = false;
        string Latlng;
        string parentElementName = "";
        string childElementName = "";
        string childElementValue = "";


        public string getGoogleLocationByLogLat(string url, DataTable googleKeys, bool IsGoogleAPI)//DataTable dt, 
        {
            string location = "";
            if (IsGoogleAPI)
            {
                
                if (googleKeys.Rows.Count > 0)
                {
                    for (int i = 0; i < googleKeys.Rows.Count; i++)
                    {
                        string url1 = url;
                        url1 = url1.Replace("<API_KEY>", googleKeys.Rows[i]["GoogleKey"].ToString());
                        #region------ Get location from google API -----------------
                        xReader = new XmlTextReader(url);
                        while (xReader.Read())
                        {
                            if (xReader.NodeType == XmlNodeType.Element)
                            {
                                if (element)
                                {
                                    parentElementName = parentElementName + childElementName + "<br>";
                                }
                                element = true;
                                childElementName = xReader.Name;
                            }
                            else if (xReader.NodeType == XmlNodeType.Text | xReader.NodeType == XmlNodeType.CDATA)
                            {
                                element = false;
                                childElementValue = xReader.Value;
                                if (childElementName == "formatted_address")
                                {
                                    location = childElementValue;
                                    break;
                                }
                            }
                        }
                        if (location.Trim() != "")
                        {
                            break;
                        }
                        #endregion
                    }
                }
            }
            else
            {
                var client = new RestClient(url, HttpVerb.GET);
                var json = client.MakeRequest();
                dynamic stuff = JObject.Parse(json);
                location = stuff.results[0].formatted_address;
            }
            return location;
        }        
        public void GetLng_Lat_FromAddress(string address, out string lat, out string lng)
        {
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);
            lng = point.Longitude.ToString();
            lat = point.Latitude.ToString();
        }
        public string GetAddressFromLatLong(double lat, double lng)
        {
            var locationService = new GoogleLocationService();
            var point = locationService.GetAddressFromLatLang(lat,lng);
            return point.ToString();
        }
        
        protected double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
        protected double rad2deg(double rad)
        {
            return rad * (180 / Math.PI);
        }
        public string CellId_To_Lat(long cellId1, long locationAreaCode1, string mobileCountryCode1, string mobileNetworkCode1, string url, DataTable googleKeys, bool IsGoogleAPI)
        {
            if (IsGoogleAPI)
            {
                if (googleKeys.Rows.Count > 0)
                {
                    for (int i = 0; i < googleKeys.Rows.Count; i++)
                    {
                        string url1 = url;
                        url1 = url1.Replace("<API_KEY>", googleKeys.Rows[i]["GoogleKey"].ToString());
                        Latlng = CellId_To_Lat1(cellId1, locationAreaCode1, mobileCountryCode1, mobileNetworkCode1, url1);
                        if (Latlng != "0")
                        {
                            break;
                        }

                    }
                }
            }
            else
            {
                //var client = new RestClient(url, HttpVerb.GET);
                //var json = client.MakeRequest();
                //dynamic stuff = JObject.Parse(json);
                //Latlng = stuff.results[0].formatted_address;
            }
            return Latlng;

        }
        public string CellId_To_Lat1(long cellId1, long locationAreaCode1, string mobileCountryCode1, string mobileNetworkCode1, string url)
        {
            try
            {
                var x = new
                {
                    cellTowers = new[]
            {
            new
            {
                cellId = cellId1, locationAreaCode = locationAreaCode1, mobileCountryCode = mobileCountryCode1,mobileNetworkCode=mobileNetworkCode1
            }
        }
                };



                JavaScriptSerializer js = new JavaScriptSerializer();
                string xmlMessage = js.Serialize(x);

                //string url = "https://www.googleapis.com/geolocation/v1/geolocate?key=" + key;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                byte[] requestInFormOfBytes = new UTF8Encoding(false).GetBytes(xmlMessage);
                ((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
                request.Method = "POST";
                request.Credentials = CredentialCache.DefaultCredentials;
                request.ContentType = "application/json;charset=utf-8";
                request.ContentLength = requestInFormOfBytes.Length;

                Stream requestStream = request.GetRequestStream();


                requestStream.Write(requestInFormOfBytes, 0, requestInFormOfBytes.Length);

                requestStream.Close();
                request.GetRequestStream();


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string a = (((HttpWebResponse)response).StatusDescription);

                StreamReader respStream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);

                string receivedResponse = respStream.ReadToEnd();
                respStream.Close();
                response.Close();
                //receivedResponse = CellId_To_Lat1(cellId1, locationAreaCode1, mobileCountryCode1, mobileNetworkCode1);
                JObject json = JObject.Parse(receivedResponse);
                JObject url1 = (JObject)json.SelectToken("location");
                if (url1 != null)
                {
                    Latlng = (string)url1.SelectToken("lat") + "," + (string)url1.SelectToken("lng");
                }
                else
                {
                    Latlng = "0";
                }
                return Latlng;


            }

            catch (Exception)
            {
                return "0";
            }


        }        
        public string midPoint(double lat1, double lon1, double lat2, double lon2)
        {
            double dLon = deg2rad(lon2 - lon1);
            //convert to radians
            lat1 = deg2rad(lat1);
            lat2 = deg2rad(lat2);
            lon1 = deg2rad(lon1);
            double Bx = Math.Cos(lat2) * Math.Cos(dLon);
            double By = Math.Cos(lat2) * Math.Sin(dLon);
            double lat3 = Math.Atan2(Math.Sin(lat1) + Math.Sin(lat2), Math.Sqrt((Math.Cos(lat1) + Bx) * (Math.Cos(lat1) + Bx) + By * By));
            double lon3 = lon1 + Math.Atan2(By, Math.Cos(lat1) + Bx);
            return OffSetLatlong(lat3, lon3); // rad2deg(lat3) + "," + rad2deg(lon3); //; // 
        }
        private string OffSetLatlong(double Lat, double Lng)
        {
            double angle = deg2rad(60);
            int distance = 2;
            double distanceNorth = Math.Sin(angle) * distance;
            double distanceEast = Math.Cos(angle) * distance;
            double earthRadius = 6371000;
            double newLat = Lat + (distanceNorth / earthRadius) * 180 / Math.PI;
            double newLon = Lng + (distanceEast / (earthRadius * Math.Cos(newLat * 180 / Math.PI))) * 180 / Math.PI;
            return rad2deg(newLat) + "," + rad2deg(newLon);
        }
    }
}
