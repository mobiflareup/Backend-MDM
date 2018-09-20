<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KycRequest.aspx.cs" Inherits="MobiOcean.MDM.Web.KycRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <form id="frmMain" runat="server" method="post">
        <%
            string webapiData = null;
            com.ecs.webapi.data.ActionType actionType = com.ecs.webapi.data.ActionType.NONE;
            String urlToPost = "";

            try
            {
                /***** Generate the required values to pass to the e-KYC service  *****/
                string clientId = "MCN-SAU";
                string transactionId = Guid.NewGuid().ToString();// Request.Params["transactionId"];
                string kycTypesToUse = "FMR";
                try
                {
                    kycTypesToUse = Request.Params["kycTypesToUse"].ToString();
                }
                catch (Exception)
                { }
                // string aadhaarNumber = Request.Params["aadhaarNumber"];

                switch (kycTypesToUse)
                {
                    case "IIR":
                        actionType = com.ecs.webapi.data.ActionType.IIR;
                        urlToPost = "https://staging.e-kyc.in/EcsWebApiV20/biometric.jsp"; //kycwebformsclient.GlobalValues.WebApiUrl + "biometric.jsp";
                        break;
                    case "FMR":
                        actionType = com.ecs.webapi.data.ActionType.FMR;
                        urlToPost = "https://staging.e-kyc.in/EcsWebApiV20/biometric.jsp"; //kycwebformsclient.GlobalValues.WebApiUrl + "biometric.jsp";
                        break;
                    case "OTP":
                        actionType = com.ecs.webapi.data.ActionType.OTP;
                        urlToPost = "https://staging.e-kyc.in/EcsWebApiV20/otp.jsp";// kycwebformsclient.GlobalValues.WebApiUrl + "otp.jsp";
                        break;
                    case "IIR, FMR":
                        actionType = com.ecs.webapi.data.ActionType.BIO;
                        urlToPost = "https://staging.e-kyc.in/EcsWebApiV20/biometric.jsp"; //kycwebformsclient.GlobalValues.WebApiUrl + "biometric.jsp";
                        break;
                    default:
                        break;

                }
                string udf1 = null;
                string udf2 = null;
                string udf3 = null;
                string udc = "UDC00000011";
                int maxRetry = 3;

                System.IO.Stream gatewayCertificate = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Web_API.cer")));
                System.IO.Stream signPfx = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Server.MapPath("~/Content/MobiOcean_Mobility.pfx")));
                string signPfxPassword = "!2MobI#4Cean";

                webapiData = ECSWebApi.ECSWebApiClient.PrepareRequestData(
                    transactionId,
                    clientId,
                    com.ecs.webapi.data.Action.EKyc,
                    actionType, udc, udf1, udf2, udf3,
                    maxRetry, 1, "0", "UNKNOWN", null, 10000,
                    gatewayCertificate,
                    signPfx,
                    signPfxPassword);
            }
            catch (Exception ex)
            {
                Response.Write(String.Format("Exception Occurred. Ex : {0}", ex));
            }
        %>
        <input id="ECS_WEBAPI_DATA" name="ECS_WEBAPI_DATA" type="hidden" value='<%=webapiData%>' />
        <input id="UrlToPost" type="hidden" value='<%=urlToPost%>' />
        <%--<input type="submit" id="r" name="r"/>--%>
    </form>
    <script type="text/javascript">
        //submit the form to do kyc
        frmMain.action = $('#UrlToPost').val();
        document.getElementById("frmMain").submit();
    </script>
</body>
</html>
