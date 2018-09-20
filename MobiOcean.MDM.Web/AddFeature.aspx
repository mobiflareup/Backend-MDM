<%@ Page Title="Add Feature" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="AddFeature.aspx.cs" Inherits="MobiOcean.MDM.Web.AddFeature" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 bhoechie-tabscrollbar" id="style-3">
        <div class="content-page">
            <!-- Start content -->
            <div class="content padding-top-none">
                <!-- Page-Title -->
                <div class="container whitebg padding-top-20">
                    <div class="row margin-none">
                        <div class="col-sm-12">
                            <div class="col-sm-10">
                                <h1 class="pull-left page-title">Add Feature</h1>
                            </div>
                            <div class="col-sm-2 pull-right">
                                <h3 style="display: inline-block;">
                                    <button title="" data-placement="left" data-toggle="tooltip" class="btn btn-default circleicon colorgrey" type="button" data-original-title="Add SubAdmin"><i class="fa fa-user-plus"></i></button>
                                </h3>
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
                                                <div class="form-group ">
                                                    <label for="company" class="control-label col-lg-4">Category *:</label>
                                                    <div class="col-lg-6">
                                                        <asp:DropDownList ID="ddlCategoryName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="ddlCategoryName" ErrorMessage="*" ValidationGroup="save" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="firstname" class="control-label col-lg-4">Feature Code *:</label>
                                                    <div class="col-lg-6">
                                                        <asp:TextBox ID="txtFeatureCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                            ControlToValidate="txtFeatureCode" ErrorMessage="Required!" ValidationGroup="save" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group ">
                                                    <label for="username" class="control-label col-lg-4">Feature Name *:</label>
                                                    <div class="col-lg-6">
                                                        <asp:TextBox ID="txtFeatureName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                            ControlToValidate="txtFeatureName" ErrorMessage="Required!" ValidationGroup="save" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group ">

                                                    <div class="col-lg-10">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <label for="email" class="control-label col-lg-4">Feature Description :</label>

                                                    <div class="col-lg-6">
                                                        <asp:TextBox ID="txtFeatureDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group ">

                                                    <div class="col-lg-10">
                                                        <br />
                                                    </div>
                                                </div>

                                                <div class="form-group ">

                                                    <div class="col-lg-10">
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="form-group ">
                                                    <%--<label for="phone" class="control-label col-lg-2">Manager Contact No. :</label>--%>
                                                    <div class="col-lg-10">
                                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <div class="col-lg-offset-2 col-lg-10">
                                                        <asp:Button ID="btnAssign" runat="server" CssClass="btn btnd btncompt" Text="Save" OnClick="btnSave_Click" ValidationGroup="save" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btnd btncompt" Text="Cancel" OnClick="btnCancel_Click" />
                                                    </div>
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

</asp:Content>
