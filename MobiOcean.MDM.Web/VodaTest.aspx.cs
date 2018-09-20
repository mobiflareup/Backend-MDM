using GoogleMaps.LocationServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
//using MobiOcean.MDM.Web.Vodalocationtracker;
//using Microsoft.Web.Services3;
//using Microsoft.Web.Services3.Security.Tokens;
//using Newtonsoft.Json;
//using System.Web.Script.Serialization;
//using Microsoft.Web.Services3.Security.Tokens;
//using System.Web.Services.Protocols;

namespace MobiOcean.MDM.Web
{
    public partial class VodaTest : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var locationService = new GoogleLocationService();
            var point = locationService.GetAddressFromLatLang(28.4522035, 77.0377251);
            string res= point.ToString();
            Response.Write(res);
        
        //test();
        //ServicePointManager.Expect100Continue = true;
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //BasicHttpBinding mybinding = new BasicHttpBinding();
        //mybinding.Security.Mode = BasicHttpSecurityMode.Transport;
        //EndpointAddress myendpoint = new EndpointAddress("https://locationtracker.vodafone.in/VLTAPI_BULK/MSISDN/");
        ////mybinding.ReceiveTimeout = new TimeSpan(0, 0, 120);
        //ChannelFactory<IRequestChannel> factory = new ChannelFactory<IRequestChannel>(mybinding, myendpoint);
        //IRequestChannel channel1 = factory.CreateChannel();
        //string file = File.ReadAllText(Server.MapPath("~/Content/Client.xml"));
        //XmlReader envelopeReader = XmlReader.Create(new StringReader(file));
        //MessageVersion msg = MessageVersion.CreateVersion(EnvelopeVersion.Soap11, AddressingVersion.None);
        //Message requestMsg = Message.CreateMessage(envelopeReader, int.MaxValue, msg);
        //Message m = channel1.Request(requestMsg, new TimeSpan(0, 0, 120));
        ////string s = m.GetReaderAtBodyContents().MoveToElement().ToString();
        //XmlReader xReader = m.GetReaderAtBodyContents();
        //Response.Write(m.ToString());
        //string data = m.GetBody<string>();
        //bool element = false;
        //string parentElementName = "";
        //string childElementName = "";
        //string childElementValue = "";
        // string jsonText = new JavaScriptSerializer().Serialize(xReader);
        //xReader.MoveToContent();
        //// Parse the file and display each of the nodes.
        //while (xReader.Read())
        //{
        //    switch (xReader.NodeType)
        //    {
        //        case XmlNodeType.Element:
        //            Console.Write("<{0}>", xReader.Name);
        //            break;
        //        case XmlNodeType.Text:
        //            Console.Write(xReader.Value);
        //            break;
        //        case XmlNodeType.CDATA:
        //            Console.Write("<![CDATA[{0}]]>", xReader.Value);
        //            break;
        //        case XmlNodeType.ProcessingInstruction:
        //            Console.Write("<?{0} {1}?>", xReader.Name, xReader.Value);
        //            break;
        //        case XmlNodeType.Comment:
        //            Console.Write("<!--{0}-->", xReader.Value);
        //            break;
        //        case XmlNodeType.XmlDeclaration:
        //            Console.Write("<?xml version='1.0'?>");
        //            break;
        //        case XmlNodeType.Document:
        //            break;
        //        case XmlNodeType.DocumentType:
        //            Console.Write("<!DOCTYPE {0} [{1}]", xReader.Name, xReader.Value);
        //            break;
        //        case XmlNodeType.EntityReference:
        //            Console.Write(xReader.Name);
        //            break;
        //        case XmlNodeType.EndElement:
        //            Console.Write("</{0}>", xReader.Name);
        //            break;
        //    }
        //}
        //while (xReader.Read())
        //{
        //    if (xReader.NodeType == XmlNodeType.Element)
        //    {
        //        if (element)
        //        {
        //            parentElementName = parentElementName + childElementName + "<br>";
        //        }
        //        element = true;
        //        childElementName = xReader.Name;
        //    }
        //    else if (xReader.NodeType == XmlNodeType.Text | xReader.NodeType == XmlNodeType.CDATA)
        //    {
        //        element = false;
        //        childElementValue = xReader.Value;
        //        //if (childElementName == "formatted_address")
        //        //{
        //        //    location = childElementValue;
        //        //    break;
        //        //}
        //    }
        //}
        //SampleWSE.Service1Wse proxy = new SecureWSClient.SampleWSE.Service1Wse();
        //proxy.RequestSoapContext.Security.Tokens.Add(new UsernameToken(name,
        //                         password, PasswordOption.SendHashed));
        //proxy.RequestSoapContext.Security.Timestamp.TtlInSeconds = 300;

        //MobiOcean.MDM.VodaLocationService.getLocation getLocation = new MobiOcean.MDM.VodaLocationService.getLocation();
        //getLocation.address = "9643700150";
        //getLocation.realTime = "N";

        //VodaLocation.TerminalLocationClient lc = new TerminalLocationClient();
        //lc.ClientCredentials.UserName = "account@mobiocean.com";
        //getLocation gl = new VodaLocation.getLocation();
        //gl.address = "9643700150";
        //gl.realTime = "N";
        //getLocationRequest glr = new getLocationRequest();
        //glr.getLocation( getLocation(gl);





    }       

        //public void test()
        //{
        //    ServicePointManager.Expect100Continue = true;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    string MSISDNs = "9643822820,9643700150";
        //    TerminalLocationImplService tloc = new TerminalLocationImplService();
        //    tloc.RequestSoapContext.Security.Tokens.Add(new UsernameToken("account@mobiocean.com", "MobiOcean@123", PasswordOption.SendPlainText));
        //    //tloc.RequestSoapContext.Security.Timestamp.TtlInSeconds = 120;
        //    SoapEnvelope env = tloc.RequestSoapContext.Envelope;
        //    //tloc.RequestSoapContext.Envelope = ProcessMessage(env);
        //    //*tloc.RequestSoapContext.Addressing = new Microsoft.Web.Services3.Addressing.AddressingHeader); ;// Microsoft.Web.Services3.Addressing.Action("http://www.csapi.org/schema/parlayx/c*/ommon/v3_1/TerminalLocation/getLocationRequest");
        //    getLocationForGroup getlocation = new getLocationForGroup();
        //    getlocation.addresses = MSISDNs.Split(',');
        //    getlocation.isRealTime = "N";
        //    tloc.RequestSoapContext.Addressing.Action = new Microsoft.Web.Services3.Addressing.Action("http://www.w3.org/2005/08/addressing");
        //    //tloc.RequestSoapContext.Addressing = EnvelopeVersion.Soap11;// "http://www.w3.org/2005/08/addressing";
        //    //NetworkCredential creds = new NetworkCredential("account@mobiocean.com", "MobiOcean@123", "https://locationtracker.vodafone.in/VLTAPI_BULK/MSISDN/");
        //    // tloc.Credentials = creds;
        //    tloc.Timeout = 120000;
        //    //tloc.RequestSoapContext.Addressing=
        //    LocationData[] locdata = tloc.getLocationForGroup(getlocation);

        //    string res = "gfhfg";

        //}
        ////private void Maingfh()
        ////{
        ////    using (var client = new ServiceClient())
        ////    using (var scope = new OperationContextScope(client.InnerChannel))
        ////    {
        ////        MessageHeader usernameTokenHeader = MessageHeader.CreateHeader("UsernameToken",
        ////            "http://test.com/webservices", "username");
        ////        OperationContext.Current.OutgoingMessageHeaders.Add(usernameTokenHeader);

        ////        MessageHeader passwordTextHeader = MessageHeader.CreateHeader("PasswordText",
        ////            "http://test.com/webservices", "password");
        ////        OperationContext.Current.OutgoingMessageHeaders.Add(passwordTextHeader);

        ////        MessageHeader sessionTypeHeader = MessageHeader.CreateHeader("SessionType",
        ////            "http://test.com/webservices", "None");
        ////        OperationContext.Current.OutgoingMessageHeaders.Add(sessionTypeHeader);

        ////        string result = client.GetData(1);
        ////        Console.WriteLine(result);
        ////    }
        ////    Console.ReadKey();
        ////}
        //public SoapEnvelope ProcessMessage(SoapEnvelope envelope)

        //{

        //    //creating the <wsse:Security> element in the outgoing message
        //    XmlNode ActionNode = envelope.CreateNode(XmlNodeType.Element, "wsa:Action","");
        //    ActionNode.InnerXml = "http://www.csapi.org/schema/parlayx/common/v3_1/TerminalLocation/getLocationRequest";
        //    XmlNode securityNode = envelope.CreateNode(XmlNodeType.Element, "wsse:Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

        //    XmlAttribute securityAttr = envelope.CreateAttribute("soap:mustunderstand");

        //    securityAttr.Value = "1";

        //    //creating the <wsse:usernameToken> element

        //    XmlNode usernameTokenNode = envelope.CreateNode(XmlNodeType.Element, "wsse:UsernameToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

        //    XmlElement userElement = usernameTokenNode as XmlElement;

        //    userElement.SetAttribute("xmlns:wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");



        //    //creating the <wsse:Username> element

        //    XmlNode userNameNode = envelope.CreateNode(XmlNodeType.Element, "wsse:Username", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

        //    userNameNode.InnerXml = "account@mobiocean.com";



        //    //creating the <wsse:password> element

        //    XmlNode pwdNode = envelope.CreateNode(XmlNodeType.Element, "wsse:Password", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

        //    XmlElement pwdElement = pwdNode as XmlElement;

        //    pwdElement.SetAttribute("Type", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText");

        //    pwdNode.InnerXml = "MobiOcean@123";



        //    usernameTokenNode.AppendChild(userNameNode);

        //    usernameTokenNode.AppendChild(pwdNode);



        //    securityNode.AppendChild(usernameTokenNode);



        //    envelope.ImportNode(securityNode, true);



        //    XmlNode node = envelope.Header;



        //    node.AppendChild(securityNode);

        //    node.AppendChild(ActionNode);

        //    //removing Addressing headers from the outgoing request



        //    //XmlNode actionNode = envelope.Header["wsa:Action"];

        //    //envelope.Header.RemoveChild(actionNode);



        //    XmlNode messageNode = envelope.Header["wsa:MessageID"];

        //    envelope.Header.RemoveChild(messageNode);



        //    XmlNode replyToNode = envelope.Header["wsa:ReplyTo"];

        //    envelope.Header.RemoveChild(replyToNode);



        //    XmlNode toNode = envelope.Header["wsa:To"];

        //    envelope.Header.RemoveChild(toNode);

        //    return envelope;

        //}
    }
}
//namespace WSE2.Custom.OutputFilters

//{

//    public class ModifyUsernameToken : SoapOutputFilter //inherit the custom class from SoapOutputFilter inside we want to modify a SOAP Request

//    {

//        public ModifyUsernameToken()

//        { }



//        public override void ProcessMessage(SoapEnvelope envelope)

//        {

//            //creating the <wsse:Security> element in the outgoing message

//            XmlNode securityNode = envelope.CreateNode(XmlNodeType.Element, "wsse:Security", "http://docs.oasis-open.org/wss/2004/01/oasis-

//            200401 - wss - wssecurity - secext - 1.0.xsd");

//            XmlAttribute securityAttr = envelope.CreateAttribute("soap:mustunderstand");

//            securityAttr.Value = "1";

//            //creating the <wsse:usernameToken> element

//            XmlNode usernameTokenNode = envelope.CreateNode(XmlNodeType.Element, "wsse:UsernameToken", "http://docs.oasis-

//            open.org / wss / 2004 / 01 / oasis - 200401 - wss - wssecurity - secext - 1.0.xsd");

//            XmlElement userElement = usernameTokenNode as XmlElement;

//            userElement.SetAttribute("xmlns:wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");



//            //creating the <wsse:Username> element

//            XmlNode userNameNode = envelope.CreateNode(XmlNodeType.Element, "wsse:Username", "http://docs.oasis-

//            open.org / wss / 2004 / 01 / oasis - 200401 - wss - wssecurity - secext - 1.0.xsd");

//            userNameNode.InnerXml = "testuser";



//            //creating the <wsse:password> element

//            XmlNode pwdNode = envelope.CreateNode(XmlNodeType.Element, "wsse:Password", "http://docs.oasis-open.org/wss/2004/01/oasis-

//            200401 - wss - wssecurity - secext - 1.0.xsd");

//            XmlElement pwdElement = pwdNode as XmlElement;

//            pwdElement.SetAttribute("Type", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText");

//            pwdNode.InnerXml = "test password";



//            usernameTokenNode.AppendChild(userNameNode);

//            usernameTokenNode.AppendChild(pwdNode);



//            securityNode.AppendChild(usernameTokenNode);



//            envelope.ImportNode(securityNode, true);



//            XmlNode node = envelope.Header;



//            node.AppendChild(securityNode);



//            //removing Addressing headers from the outgoing request



//            XmlNode actionNode = envelope.Header["wsa:Action"];

//            envelope.Header.RemoveChild(actionNode);



//            XmlNode messageNode = envelope.Header["wsa:MessageID"];

//            envelope.Header.RemoveChild(messageNode);



//            XmlNode replyToNode = envelope.Header["wsa:ReplyTo"];

//            envelope.Header.RemoveChild(replyToNode);



//            XmlNode toNode = envelope.Header["wsa:To"];

//            envelope.Header.RemoveChild(toNode);

//        }

//    }

//}