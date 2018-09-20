<%@ Page Title="Subscribe" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="SubscribePeanut.aspx.cs" Inherits="MobiOcean.MDM.Web.SubscribePeanut" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">
    <style>
        .module {
            text-align: center;
        }

        .form-module {
            position: relative;
            background: #ffffff;
            max-width: 330px;
            width: 100%;
            /* border: 3px solid #2B3588; */
            border: 3px solid #ccc;
            box-shadow: 0 0 3px rgba(0, 0, 0, 0.1);
            border-radius: 4px;
            margin: 0 auto;
        }

            .form-module .toggle {
                cursor: pointer;
                position: absolute;
                top: 0;
                right: 0;
                background: #2B3588;
                width: 30px;
                height: 30px;
                margin: -5px 0 0;
                color: #ffffff;
                font-size: 12px;
                line-height: 30px;
                text-align: center;
            }

                .form-module .toggle .tooltip {
                    position: absolute;
                    top: 5px;
                    right: -65px;
                    display: block;
                    background: rgba(0, 0, 0, 0.6);
                    width: auto;
                    padding: 5px;
                    font-size: 10px;
                    line-height: 1;
                    text-transform: uppercase;
                }

                    .form-module .toggle .tooltip:before {
                        content: '';
                        position: absolute;
                        top: 5px;
                        left: -5px;
                        display: block;
                        border-top: 5px solid transparent;
                        border-bottom: 5px solid transparent;
                        border-right: 5px solid rgba(0, 0, 0, 0.6);
                    }

            .form-module .form {
                display: block;
                padding: 40px;
            }

                .form-module .form:nth-child(2) {
                    display: block;
                }

            .form-module h2 {
                margin: 0 0 10px;
                color: #8c8c8c;
                font-size: 18px;
                font-weight: 400;
                line-height: 1;
                padding-top: 20px;
            }

            .form-module input {
                outline: none;
                display: block;
                width: 100%;
                border: 1px solid #d9d9d9;
                margin: 0 0 20px;
                padding: 10px 15px;
                box-sizing: border-box;
                font-weight: 400;
                -webkit-transition: 0.3s ease;
                transition: 0.3s ease;
            }

                .form-module input:focus {
                    border: 1px solid #2B3588;
                    color: #333333;
                }

            .form-module button {
                cursor: pointer;
                background: #2B3588;
                border: 0;
                padding: 7px 30px;
                color: #ffffff;
                -webkit-transition: 0.3s ease;
                transition: 0.3s ease;
            }

                .form-module button:hover {
                    background: #178ab4;
                }

            .form-module .cta {
                background: #fff;
                width: 100%;
                padding: 15px 40px;
                box-sizing: border-box;
                color: #666666;
                font-size: 12px;
                text-align: center;
            }

            .form-module .cta {
                color: #333333;
                text-decoration: none;
            }

        .form-modulee {
            background: none;
            border: 0px solid #ccc;
            margin: 0 auto;
            max-width: 330px;
            position: relative;
            width: 100%;
        }

            .form-modulee h3 {
                color: #666;
                text-align: left;
            }

        .form-moduleee {
            background: #ffffff none repeat scroll 0 0;
            border: 3px solid #ccc;
            border-radius: 4px;
            box-shadow: 0 0 3px rgba(0, 0, 0, 0.1);
            margin: 0 auto;
            max-width: 330px;
            position: relative;
            width: 100%;
        }

            .form-moduleee h3 {
                color: #666;
                text-align: left;
            }

        .paddingbot_10 {
            padding-bottom: 30px !important;
            background-color: #e5e5e5 !important;
            border: 3px solid #e5e5e5 !important;
            max-width: 300px;
            padding-top: 25px;
            height: 180px;
        }
    </style>
     <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ScriptManager>--%>
        <!-- ============================================================== -->
        <!-- Start right Content here -->
        <!-- ============================================================== -->
        <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>

        <!-- flight section -->

       


        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">
            <div class="force-overflow">

                <div class="bhoechie-tab-content active div">

                    <center>
                      <h1 style="font-size:50px;color:#55518a"></h1><!--glyphicon-plane-->
                      <h1 style="font-size:400%;color:#55518a"><b>Welcome to Mobiocean Secure Communication</b></h1>
                      <h3 style="margin-top: 0;color:#55518a">Mobility-Managed Regulated Secured</h3>
                      <h2 style="margin-top: 0;color:#000000;"><i>Let's get started<br></i></h2>
                    </center>


                    <div class="row">
                        <div class="col-sm-12" style="text-align: center">
                            <h4>
                                <asp:Label ID="lblverify" runat="server" Font-Size="Medium"></asp:Label></h4>
                        </div>
                        <div class="col-sm-12" style="text-align: center">
                            <br />
                        </div>
                        <div class="col-lg-12" style="text-align: center">
                            <label>
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon" Font-Size="Large"></asp:Label>
                            </label>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                           
                            <div class="col-sm-12">

                                <div class="module form-module paddingbot_10">

                                    <div class="formm">
                                        <div class="col-sm-12">
                                            <!--<h2>Login to your account</h2>-->

                                            <h2>CHOOSE SUBSCRIPTION</h2>

                                            <a class="btn btnd btncompt waves-effect waves-light" target="_blank" href="<%=Constant.MobiURL%>cloud-management.php">Subscribe</a>

                                        </div>
                                    </div>
                                    <div class="clear "></div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
           
        </div>
     <asp:Button ID="dummysubscribe" runat="server" style="display:none;"/>

                <asp:ModalPopupExtender ID="MpSubscribe" runat="server" PopupControlID="SubscribePnl"
                    TargetControlID="dummysubscribe" CancelControlID="btncclsub" 
                    BackgroundCssClass="modalbackground">
                </asp:ModalPopupExtender>

                <asp:Panel runat="server" ID="SubscribePnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">
                              
                     <div class="modal-body" style="text-align:center;color:green;">
                                    <asp:Button ID="btncclsub" runat="server" Text="x" class="close btn btnd btncompt" style="display:none;" />
                                    <asp:Label ID="message" runat="server" Text="Applied Successfully"></asp:Label>
                         </div>
                        <div class="modal-footer">
                            <asp:Button ID="ok" runat="server" Text="OK" OnClick="ok_Click" />
                        </div>
                    

                </asp:Panel>

     <asp:Button ID="dummysuccess" runat="server" style="display:none;"/>

                <asp:ModalPopupExtender ID="mpsuccess" runat="server" PopupControlID="successpnl"
                    TargetControlID="dummysuccess" CancelControlID="btnsuccan" 
                    BackgroundCssClass="modalbackground">
                </asp:ModalPopupExtender>

                <asp:Panel runat="server" ID="successpnl" TabIndex="-1" role="dialog" aria-labelledby="myModalLabel" CssClass="msgpopup" aria-hidden="true">
                              
                     <div class="modal-body" style="text-align:center;color:green;">
                                    <asp:Button ID="btnsuccan" runat="server" Text="x" class="close btn btnd btncompt" style="display:none;" />
                                    <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                         </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnsucok" runat="server" Text="OK" OnClick="btnsucok_Click" />
                        </div>
                    

                </asp:Panel>
                                       
           
             
    <%--</form>--%>
</asp:Content>