<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ccavRequestHandler.aspx.cs" 
    Inherits="MobiOcean.MDM.Web.ccavRequestHandler" %>

<!DOCTYPE html>
  
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>  
        <script type="text/javascript">
            $(document).ready(function () {
                $("#nonseamless").submit();
            });
       </script>
    <title>
    </title>
</head>
<body>
    <form id="nonseamless" method="post" name="redirect" action="<%=paymentUrl%>"> 
        <input type="hidden" id="encRequest" name="encRequest" value="<%=strEncRequest%>"/>
        <input type="hidden" name="access_code" id="Hidden1" value="<%=strAccessCode%>"/>
    </form>
</body>
</html>

