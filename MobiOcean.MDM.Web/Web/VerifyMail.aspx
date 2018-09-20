<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerifyMail.aspx.cs" Inherits="MobiOcean.MDM.Web.Web.VerifyMail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Verify Email</title>
    <meta charset="UTF-8" />
    <meta name="Generator" content="EditPlus®" />
    <meta name="Author" content="" />
    <meta name="Keywords" content="" />
    <meta name="Description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/custom1.css" />
    <link rel="stylesheet" href="css/custom-responsive.css" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/respond.js"></script>
    <script src="js/custom-script.js"></script>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>


    <!-- google fonts -->
    <link href="https://fonts.googleapis.com/css?family=PT+Sans" rel="stylesheet" type="text/css" />

    <style>
        .tpmenu li a {
            color: #f16a27;
            background: none;
            padding: 0px 4px !important;
        }

        .tpmenu li {
            border-right: 1px solid #ddd;
        }

            .tpmenu li a:hover {
                color: #f16a27;
                text-decoration: underline;
                background: none;
            }

        ul.tpmenu li:last-child {
            border-right: none;
        }

        .navbar-brand {
            height: auto;
        }

        .custom-navber {
            background: none;
        }

        .freetrialbtn:hover {
            color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ScriptManager>

        <nav class="navbar navbar-default custom-navber">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="<%=MobiOcean.MDM.BAL.Model.Constant.MobiURL %>">
                        <img src="images/logo.png" style="width: 292px; height: 71px" /></a>
                </div>
                <div class="pull-right">
                    <ul class="nav nav-pills nav-top tpmenu">
                        <li>
                            <a class="mobiorange arialbold" href="<%=MobiOcean.MDM.BAL.Model.Constant.MobiURL %>contact.php">Contact Us</a>
                        </li>
                        <li class="phone">
                            <div><a class="mobiorange arialbold" href="<%=MobiOcean.MDM.BAL.Model.Constant.MobiURL %>login.php">Login</a></div>
                        </li>
                        <li>
                            <a class="mobiorange arialbold" href="<%=MobiOcean.MDM.BAL.Model.Constant.MobiURL %>">Back To Home</a>
                        </li>
                    </ul>

                </div>
                <div class="clearfix"></div>
            </div>
        </nav>

        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <div class="mycontents" style="background: #EEE; text-align: center; padding: 15% 0%;">
                    <p class="abt_cont" style="text-align: center">
                    </p>
                    <h2>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label></h2>
                    <p></p>
                    <br /><br />
                    <div class="container margin-top-25">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="freetrialrow"><a class="freetrialbtn  bg-primary" href="<%=MobiOcean.MDM.BAL.Model.Constant.MobiURL%>login.php" style="padding: 7px 18px; border-radius: 4px;">Login</a></div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
    <footer class="foot">
        <div class="container-fluid" style="background: #222; color: #fff; padding: 20px;">
            <div class="row">
                <p class="text-center">Copyright &copy; <%=DateTime.Now.Year%></p>
            </div>
        </div>
    </footer>
</body>
</html>





