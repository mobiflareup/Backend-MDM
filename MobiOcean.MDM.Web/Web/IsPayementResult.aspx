<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IsPayementResult.aspx.cs" Inherits="MobiOcean.MDM.Web.Web.IsPayementResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="Generator" content="EditPlus®" />
    <meta name="Author" content="" />
    <meta name="Keywords" content="" />
    <meta name="Description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/custom-script.js"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="css/custom.css" />
    <link rel="stylesheet" href="css/custom-responsive.css" />
    <title>Order Detail</title>
</head>
<body>
    <form runat="server">
        <div class="container-fluid">
            <div class="row">
                <nav class="navbar navbar-inverse custom-navbar">
                    <div class="container">
                        <div class="navbar-header">
                            <a class="navbar-brand" href="#" alt="MobiOcean" title="MobiOcean">
                                <img src="images/logo-beta2.png" class="img-responsive"></a>
                        </div>
                        <ul class="nav navbar-nav pull-right">
                            <h3 class="welcom">Welcome 
                            <asp:Label ID="lblUserName" runat="server"></asp:Label>&nbsp;&nbsp;<small><asp:LinkButton ID="btnlogout" runat="server" class="glyphicon glyphicon-off custom-logout-icon" OnClick="btnlogout_Click" title="logout" /></small></h3>
                        </ul>
                        <div class="clearfix">
                            <br />
                            <br />
                        </div>
                    </div>
                </nav>

            </div>
            <asp:Panel ID="pnlsuccess" runat="server">
                <div class="row custom-row">
                    <div class="success-alert text-center">
                        <h1><span class="glyphicon glyphicon-thumbs-up"></span></h1>
                        <h1>Your payment processed successfully !!!</h1>
                        <h1>
                            <asp:Button ID="btnsuccess" runat="server" CssClass="btn btn-primary  custom-btn-login" Text="Manage your Employee" OnClick="btnsuccess_Click" /><h1>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlfailure" runat="server">
                <div class="row custom-row">
                    <div class="failure-alert text-center">
                        <h1><span class="glyphicon glyphicon-thumbs-down"></span></h1>
                        <h1>Your payment processed failure !!!</h1>
                        <br />
                        <asp:Button ID="btnfailure" runat="server" CssClass="btn btn-primary custom-btn-login" Text="Try Again" OnClick="btnfailure_Click" />
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="footer text-center">
                    <p>Copyright © <%#DateTime.Now.Year %>.All right reserved</p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

