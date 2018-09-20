<%@ Page Title="ChangePassword" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ChangePassword.aspx.cs" Inherits="MobiOcean.MDM.Web.ChangePassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10 bhoechie-tab scrollbar" id="style-3">
                <div class="content-page">
                    <!-- Start content -->
                    <div class="content padding-top-none">
                        <!-- Page-Title -->
                        <div class="container whitebg padding-top-20">
                            <div class="row margin-none">
                                <div class="col-sm-12">
                                    <div class="col-sm-10">
                                        <h1 class="pull-left page-title">Change Password</h1>
                                    </div>
                                    <div class="col-sm-2 pull-right">
                                        <h3 style="display: inline-block;"></h3>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="content m-t-20">
                            <div class="container">
                                <div class="panel">
                                    <div class="panel-body">



                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="panel-group panel-group-joined" id="accordion-test">

                                                    <div class=" form">
                                                        <%--<form class="cmxform form-horizontal tasi-form" id="signupForm" method="get" action="#" novalidate="novalidate">--%>
                                                        <div class="col-lg-7">
                                                            <div class="form-group ">
                                                                <label for="bname" class="control-label col-lg-4">Old Password* : </label>
                                                                <div class="col-lg-8">
                                                                    <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                        ControlToValidate="txtOldPassword" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">

                                                                <%-- <div class="col-lg-12">
                                                        <br />
                                                    </div>--%>
                                                            </div>
                                                            <div class="form-group ">
                                                                <label for="firstname" class="control-label col-lg-4">New Password* : </label>
                                                                <div class="col-lg-8">
                                                                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                                    <asp:BalloonPopupExtender ID="BalloonPopupExtender3" TargetControlID="txtNewPassword" UseShadow="true"
                                                                        DisplayOnFocus="true" Position="TopRight" BalloonPopupControlID="Panel3" BalloonStyle="Rectangle"
                                                                        runat="server" />
                                                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator3" runat="server" ValidationGroup="save"
                                                                        ErrorMessage="Password must be Minimum 8 characters long with at least one numeric, one upper case character, One lower case character and one special character."
                                                                        ForeColor="Red" ControlToValidate="txtNewPassword" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}">

                                                                    </asp:RegularExpressionValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                        ControlToValidate="txtNewPassword" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group ">
                                                                <label for="lastname" class="control-label col-lg-4">Confirm Password* : </label>
                                                                <div class="col-lg-8">

                                                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                        ControlToValidate="txtConfirmPassword" ErrorMessage="Required!" ForeColor="Red" ValidationGroup="save"></asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ID="cv1" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword"
                                                                        ErrorMessage="Not Match..!" ForeColor="Red" ValidationGroup="save"></asp:CompareValidator>
                                                                </div>
                                                            </div>


                                                            <div class="form-group">
                                                                <label class="control-label col-lg-4"></label>
                                                                <div class="col-lg-8">
                                                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>

                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-lg-offset-4 col-lg-6">
                                                                    <asp:Button ID="btnChange" runat="server" class="btn btnd btncompt" Text="Change" ValidationGroup="save" OnClick="btnChange_Click" />


                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-5">
                                                        </div>
                                                    </div>
                                                </div>
                                                --%>
											
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end: page -->
                        </div>
                        <!-- end Panel -->
                    </div>
                    <!-- container -->
                </div>
                <!-- content -->
                <!-- container -->
            </div>
            <!-- content -->
            </div>
	</div>
                    </div>
            <asp:Panel ID="Panel3" runat="server">
                <h6><u><b>PASSWORD POLICY</b></u><br />
                    1. Minimum 8 Characters.<br />
                    2. At Least One Numeric.<br />
                    3. One Upper Case Character.<br />
                    4. One Lower Case Character.<br />
                    5. One Special Character.<br />
                </h6>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

