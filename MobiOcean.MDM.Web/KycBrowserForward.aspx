<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KycBrowserForward.aspx.cs" Inherits="MobiOcean.MDM.Web.KycBrowserForward" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <%
        try
        {
            string encResult = Request.Params["ECS_WEBAPI_SIMPLE_RES"];
            LogExceptions("KYCBF --> Result", "Input: " + encResult, "Response : 1" + "", "");
            com.ecs.webapi.data.ECSWebApiResponse response = ECSWebApi.ECSWebApiClient.ParseResponse(encResult);
            System.IO.Stream gatewayCertificate = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Web_API.cer")));
            System.IO.Stream signPfx = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Server.MapPath("~/Content/MobiOcean_Mobility.pfx")));
            string signPfxPassword = "!2MobI#4Cean";

            com.ecs.webapi.data.ECSWebApiSimpleResponseData simpleResp =
                ECSWebApi.ECSWebApiClient.GetSimpleResponseData(
                    response,
                    gatewayCertificate,
                    signPfx,
                    signPfxPassword);

            if (simpleResp.Status == com.ecs.webapi.data.TransactionStatus.Cancelled)
            {
    %>

    <div>Transaction <%=simpleResp.Id%> is Cancelled! </div>
    <%  }
        else
        {
            if (Application["KycCache"] == null)
            {
                Application["KycCache"] = new Dictionary<string, com.ecs.webapi.data.ECSWebApiResponseData>();
            }

            Dictionary<string, com.ecs.webapi.data.ECSWebApiResponseData> kycCache = Application["KycCache"] as Dictionary<string, com.ecs.webapi.data.ECSWebApiResponseData>;
            com.ecs.webapi.data.ECSWebApiResponseData resp = kycCache[simpleResp.Id];


            if (simpleResp.Status == com.ecs.webapi.data.TransactionStatus.Failed)
            {
                string errorMessage = resp.ErrorMessage;
    %>
    <div>Transaction <%=resp.Id%> Failed. Error: <%=errorMessage %> </div>

    <%  }
        else
        {
            // --- HTML CODE FOR DISPLAYING THE RESULTS GO HERE --- // 
            ECSUidaiResponseParser.KycResProcessor pro = new ECSUidaiResponseParser.KycResProcessor();
            pro.Parse(System.Text.Encoding.UTF8.GetString(resp.UidaiData));
            In.gov.uidai.kyc.uid_kyc_response._1.KycRes kycRes = pro.GetKycRes();
            In.gov.uidai.kyc.uid_kyc_response._1.UidDataType uidData = kycRes.uidData;
            MobiOcean.MDM.BAL.BAL.EkycBAL EKB = new MobiOcean.MDM.BAL.BAL.EkycBAL();
            EKB.Ins_Ekyc(uidData.poi.name,uidData.uid,uidData.poi.dob,uidData.pht,uidData.poa.co,uidData.poi.gender.ToString().Substring(0,1),uidData.poa.house,uidData.poa.street,uidData.poa.lm,uidData.poa.lc,uidData.poa.vtc,uidData.poa.po,uidData.poa.subdist,uidData.poa.dist,uidData.poa.state,uidData.poa.pc,uidData.poa.country,kycRes.code,kycRes.ts,Convert.ToInt32(Session["ClientId"]),Convert.ToInt32(Session["UserId"]));
    %>
    <table style="padding-top: 15px; margin-left: auto; margin-right: auto">
        <tr>
            <td colspan="2">
                <img src="data:image/jpg;base64,<%=uidData.pht%>">
            </td>
        </tr>
        <tr>
            <td>Name
            </td>
            <td>
                <%=uidData.poi.name%>
            </td>
        </tr>
        <tr>
            <td>Aadhaar Number:
            </td>
            <td>
                <%=uidData.uid%>
            </td>
        </tr>
    </table>
    <%          }
            }
        }
        catch (Exception ex)
        {
            Response.Write(String.Format("Exception Occurred. Ex : {0}", ex));
        }
    %>
</body>
</html>
